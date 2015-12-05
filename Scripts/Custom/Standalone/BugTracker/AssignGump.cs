using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.Tracker
{
	public sealed class AssignGump : TrackerTemplateGump<ITrackerUser>
	{
		private static TrackerColumn[] m_Columns = new TrackerColumn[]
			{
				new TrackerColumn( "Name",		200,	"#113399" )
			};
		public override TrackerColumn[] Columns { get { return m_Columns; } }
		public override bool GoBackToMainPageOnCancel
		{
			get
			{
				return true;
			}
		}
		public override int MaxRows
		{
			get
			{
				return 10;
			}
		}

		private TrackerEntry m_Entry;
		private int m_MainPage;

		public override string GetTitle( int page )
		{
			return String.Format( "Assign User (Page {0})", page );
		}

		public override List<ITrackerUser> TrackerEntries
		{
			get
			{
				return new List<ITrackerUser>( TrackerSystem.AllUsers );
			}
		}

		public AssignGump( Mobile from, TrackerEntry entry, int page, int mainpage )
			: base( from, page )
		{
			m_Entry = entry;
			m_MainPage = mainpage;
		}

		public override void DisplayLayout( int index, ref int x, int y, ITrackerUser entry )
		{
			int column = 0;

			AddColumn( index, ref x, y, column++, entry.Name.ToString() );
		}

		public override void OnFailResponse( Mobile from, int page )
		{
			from.SendGump( new ViewTrackerEntryGump( from, m_Entry, m_MainPage ) );
		}

		public override void OnPreviousResponse( Mobile from, int prevpage )
		{
			from.SendGump( new AssignGump( from, m_Entry, prevpage, m_MainPage ) );
		}

		public override void OnNextResponse( Mobile from, int nextpage )
		{
			from.SendGump( new AssignGump( from, m_Entry, nextpage, m_MainPage ) );
		}

		public override void OnEntryResponse( Mobile from, ITrackerUser entry, int page )
		{
			m_Entry.Assign( entry );
			from.SendMessage( "The Assigned To for this entry has been updated to {0}.", entry.Name );

			from.SendGump( new ViewTrackerEntryGump( from, m_Entry, m_MainPage ) );
		}
	}
}