using System;
using System.Collections.Generic;
using Server.Commands;
using Server.Misc;
using System.IO;

namespace Server.Engines.Tracker
{
	public sealed class TrackerPersistance : Item
	{
		private static TrackerPersistance m_Instance;

		public static TrackerPersistance Instance
		{
			get
			{
				if ( m_Instance == null || m_Instance.Deleted )
					new TrackerPersistance();

				return m_Instance;
			}
		}

		private Dictionary<int, TrackerEntry> m_AllIssues;
		private Dictionary<int, CommentEntry> m_AllComments;

		private int m_LastIssueID = 0;
		private int m_LastCommentID = 0;

		public static int LastIssueID { get { return m_Instance.m_LastIssueID; } }
		public static int LastCommentID { get { return m_Instance.m_LastCommentID; } }

		public static Dictionary<int, TrackerEntry> AllIssues { get { return m_Instance.m_AllIssues; } }
		public static Dictionary<int, CommentEntry> AllComments { get { return m_Instance.m_AllComments; } }

		public static int NewIssue
		{
			get
			{
				while ( FindIssue( m_Instance.m_LastIssueID = (m_Instance.m_LastIssueID + 1) ) != null ) ;

				return m_Instance.m_LastIssueID;
			}
		}

		public static int NewComment
		{
			get
			{
				while ( FindComment( m_Instance.m_LastCommentID = (m_Instance.m_LastCommentID + 1) ) != null ) ;

				return m_Instance.m_LastCommentID;
			}
		}

		public static void AddIssue( TrackerEntry entry )
		{
			m_Instance.m_AllIssues[entry.IssueID] = entry;
		}

		public static void AddComment( CommentEntry entry )
		{
			m_Instance.m_AllComments[entry.CommentID] = entry;
		}

		public static TrackerEntry FindIssue( int id )
		{
			TrackerEntry entry;
			m_Instance.m_AllIssues.TryGetValue( id, out entry );
			return entry;
		}

		public static CommentEntry FindComment( int id )
		{
			CommentEntry entry;
			m_Instance.m_AllComments.TryGetValue( id, out entry );
			return entry;
		}

		public TrackerPersistance()
			: base( 1 )
		{
			m_AllIssues = new Dictionary<int, TrackerEntry>();
			m_AllComments = new Dictionary<int, CommentEntry>();

			Movable = false;

			if ( m_Instance == null || m_Instance.Deleted )
				m_Instance = this;
			else
				base.Delete();
		}

		public override string DefaultName
		{
			get { return "Tracker Persistance - Internal"; }
		}

		public TrackerPersistance( Serial serial )
			: base( serial )
		{
			m_AllIssues = new Dictionary<int, TrackerEntry>();
			m_AllComments = new Dictionary<int, CommentEntry>();

			m_Instance = this;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version

			List<TrackerEntry> toDelete = new List<TrackerEntry>();

			foreach ( TrackerEntry issue in m_AllIssues.Values )
			{
				issue.Invalidate();

				if( issue.Expired )
					toDelete.Add( issue );
			}

			for ( int i = 0; i < toDelete.Count; i++ )
				toDelete[i].Delete();

			int count = m_AllIssues.Count;

			writer.Write( (int)count );

			foreach ( TrackerEntry entry in m_AllIssues.Values )
				entry.Serilize( writer );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version >= 0 )
			{
				int count = reader.ReadInt();

				m_AllIssues = new Dictionary<int, TrackerEntry>( count );
				for ( int i = 0; i < count; i++ )
				{
					TrackerEntry entry = new TrackerEntry( reader );
					m_AllIssues[entry.IssueID] = entry;
				}
			}
		}

		public override void Delete()
		{
		}
	}

	public class TrackerSystem
	{
		public static readonly bool Enabled = true;

		public static void CloseGumps( Mobile from )
		{
			from.CloseGump( typeof( AddIssueGump ) );
			from.CloseGump( typeof( AssignGump ) );
			from.CloseGump( typeof( TrackerGump ) );
			from.CloseGump( typeof( SetStatusGump ) );
			from.CloseGump( typeof( SetPriorityGump ) );
			from.CloseGump( typeof( ViewTrackerEntryGump ) );
		}

		private static List<ITrackerUser> m_AllUsers;
		private static Dictionary<int, ITrackerUser> m_Users;

		public static List<ITrackerUser> AllUsers { get { return m_AllUsers; } }

		public static void Configure()
		{
			m_Users = new Dictionary<int, ITrackerUser>();
			m_AllUsers = new List<ITrackerUser>();

			RegisterMember( 0, "Unassigned" );
			RegisterMember( 1, "Staff Name 1" );
			RegisterMember( 2, "Staff Name 2" );
			RegisterMember( 3, "Staff Name 3" );
			RegisterMember( 4, "Staff Name 4" );
			RegisterMember( 5, "Staff Name 5" );

			RegisterGroup( 10000, "Development Team" );
			RegisterGroup( 10001, "Spawn Team" );
			RegisterGroup( 10002, "World Designer Team" );
			RegisterGroup( 10003, "Decoration Team" );
		}

		public static readonly string SystemVersion = "1.1";

		public static void Initialize()
		{
			if( TrackerPersistance.Instance == null )
				new TrackerPersistance(); ;

			if ( TrackerSystem.Enabled )
			{
                CommandSystem.Register("AddIssue", AccessLevel.Administrator, new CommandEventHandler(OnCommand_AddIssue));
				CommandSystem.Register( "Issue", AccessLevel.Player, new CommandEventHandler( OnCommand_ViewIssues ) );
				CommandSystem.Register( "Bug", AccessLevel.Player, new CommandEventHandler( OnCommand_ViewIssues ) );
				CommandSystem.Register( "RemoveIssue", AccessLevel.Administrator, new CommandEventHandler( OnCommand_RemoveIssue ) );
			
				Utility.PushColor( ConsoleColor.Cyan );
				Console.WriteLine( "Distro: Tracker System - {0} is enabled on this server.", SystemVersion );
				Utility.PopColor();
			}
		}

		private static void RegisterGroup( int id, string name )
		{
			AssignGroup grp = new AssignGroup( id, name );
			m_AllUsers.Add( grp );
			m_Users[id] = grp;
		}

		private static void RegisterMember( int id, string name )
		{
			AssignMember mem = new AssignMember( id, name );
			m_AllUsers.Add( mem );
			m_Users[id] = mem;
		}

		public static ITrackerUser FindUser( int id )
		{
			ITrackerUser u;
			m_Users.TryGetValue( id, out u );
			return u;
		}

		[Usage( "RemoveIssue <IssueID>" )]
		[Description( "Removes an issue from the tracker" )]
		private static void OnCommand_RemoveIssue( CommandEventArgs e )
		{
			Mobile m = e.Mobile;

			if ( m == null )
				return;

			if ( !TrackerSystem.Enabled && m.AccessLevel == AccessLevel.Player )
			{
				m.SendMessage( "The tracker is currently offline." );
				return;
			}

			if ( e.Length == 1 )
			{
				int id = e.GetInt32( 0 );

				TrackerEntry entry = TrackerPersistance.FindIssue( id );
				if ( entry != null )
				{
					entry.Delete();
					e.Mobile.SendMessage( "The tracker entry has been removed." );
				}
				else
				{
					e.Mobile.SendMessage( "There was no tracker entry by that id." );
				}
			}
			else
			{
				e.Mobile.SendMessage( "Usage: [RemoveIssue <IssueID>" );
			}
		}

		[Usage( "AddIssue" )]
		[Description( "Adds an issue to the tracker." )]
		private static void OnCommand_AddIssue( CommandEventArgs e )
		{
			Mobile m = e.Mobile;

			if ( m == null )
				return;

			if ( !TrackerSystem.Enabled && m.AccessLevel == AccessLevel.Player )
			{
				m.SendMessage( "The tracker is currently offline." );
				return;
			}

			m.SendGump( new AddIssueGump( m ) );
		}

        [Usage("Issue")]
        [Aliases("Bug")]
        [Description( "Views a list of the current issues." )]
		private static void OnCommand_ViewIssues( CommandEventArgs e )
		{
			Mobile m = e.Mobile;

			if ( m == null )
				return;

			if ( !TrackerSystem.Enabled && m.AccessLevel == AccessLevel.Player )
			{
				m.SendMessage( "The tracker is currently offline." );
				return;
			}

			m.SendGump( new TrackerGump( m, 0 ) );
		}
	}
}
