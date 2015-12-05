using System;
using System.Collections.Generic;
using System.Text;
using Server.Gumps;

namespace Server.Engines.Tracker
{
	public class ViewTrackerEntryGump : Gump
	{
		public static readonly string HeaderColor = "#FFFFFF";
		public static readonly int Hue = 254;

		private int m_Page;
		private TrackerEntry m_Entry;

		public ViewTrackerEntryGump( Mobile from, TrackerEntry entry, int page )
			: base( 50, 50 )
		{
			TrackerSystem.CloseGumps( from );

			m_Page = page;
			m_Entry = entry;

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage( 0 );

			AddBackground( 0, 0, 600, 600, 9200 );

			int x = 10;
			int y = 10;

			// add html area
			AddHtml( x, y, 285, 75, String.Format( "<BASEFONT COLOR={0}>Created By: {1}<BR>Added On: {2}<BR>Last Updated: {3}</BASEFONT>", HeaderColor, entry.Submitter, entry.CreationTime.ToShortDateString(), entry.LastUpdatedTime.ToShortDateString() ), false, false );
			AddHtml( x + 195, y, 285, 75, String.Format( "<BASEFONT COLOR={0}>Assigned To: {1}<BR>Status: {2}<BR>Priority: {3}</BASEFONT>", HeaderColor, entry.AssignedTo.Name, entry.Status.ToString(), entry.Priority.ToString() ), false, false );

			y += 70;

			AddHtml( x, y, 580, 200, entry.Message, true, true ); // Message

			y += 205;

			AddHtml( x, y, 580, 275, CreateComments( entry ), true, true ); // Comments

			y += 285;
			x += 5;
			AddLabel( x, y, Hue, @"Add Comment" );
			x += 85;
			AddButton( x, y, 4029, 4030, 1, GumpButtonType.Reply, 0 );

			if ( from.AccessLevel >= AccessLevel.Administrator )
			{
				x += 60;
				AddLabel( x, y, Hue, @"Assign" );
				x += 40;
				AddButton( x, y, 4026, 4027, 2, GumpButtonType.Reply, 0 );

				x += 35;
				AddLabel( x, y, Hue, @"Status" );
				x += 45;
				AddButton( x, y, 4026, 4027, 3, GumpButtonType.Reply, 0 );

				x += 35;
				AddLabel( x, y, Hue, @"Priority" );
				x += 50;
				AddButton( x, y, 4026, 4027, 4, GumpButtonType.Reply, 0 );
			}

			AddLabel( 490, y, Hue, @"Main Menu" );
			AddButton( 555, y, 4011, 4012, 1000, GumpButtonType.Reply, 0 );
		}

		public static string CreateComments( TrackerEntry entry )
		{
			StringBuilder sb = new StringBuilder();

			sb.Append( "<center>Comments<br>----------------------------------------------------------------------------</center>" );

			List<CommentEntry> entries = entry.Comments;

			if ( entries == null )
				entries = new List<CommentEntry>();

			for ( int i = 0; i < entries.Count; i++ )
			{
				CommentEntry c = entries[i];
				if ( c != null )
					sb.AppendFormat( "Comment by: {0} - {1} {2}<br><br>{3}<center><br>----------------------------------------------------------------------------<br></center>",
						c.Submitter, c.Created.ToShortDateString(), c.Created.ToShortTimeString(), c.Comment );
			}

			return sb.ToString();
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Mobile m = sender.Mobile;

			if ( m == null )
				return;

			if ( info.ButtonID == 0 )
			{
				m.SendGump( new TrackerGump( m, m_Page ) );
			}
			else if ( info.ButtonID == 1000 ) // main menu
			{
				m.SendGump( new TrackerGump( m, 0 ) );
			}
			else if ( info.ButtonID == 1 ) // add comment
			{
				m.SendGump( new AddCommentGump( m, m_Entry, m_Page ) );
			}
			else if ( info.ButtonID == 2 ) // assign to
			{
				m.SendGump( new AssignGump( m, m_Entry, 0, m_Page ) );
			}
			else if ( info.ButtonID == 3 ) // set status
			{
				m.SendGump( new SetStatusGump( m, m_Entry, 0, m_Page ) );
			}
			else if ( info.ButtonID == 4 ) // set priority
			{
				m.SendGump( new SetPriorityGump( m, m_Entry, 0, m_Page ) );
			}
		}
	}
}
