using System;
using Server.Gumps;

namespace Server.Engines.Tracker
{
	public class AddCommentGump : Gump
	{
		private int m_Page;
		private TrackerEntry m_Entry;

		public AddCommentGump( Mobile from, TrackerEntry entry, int page )
			: base( 30, 20 )
		{
			TrackerSystem.CloseGumps( from );

			m_Page = page;
			m_Entry = entry;

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage( 0 );
			AddBackground( 6, 22, 423, 171, 9200 );
			AddAlphaRegion( 17, 48, 399, 114 );
			AddTextEntry( 21, 52, 394, 108, 254, 0, "" );
			AddButton( 383, 166, 4014, 4015, 1, GumpButtonType.Reply, 0 );
			AddLabel( 18, 26, 254, "Please fill out the box below." );
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Mobile m = sender.Mobile;

			if ( m == null )
				return;

			if ( info.TextEntries[0].Text == "" )
			{
				m.SendMessage( "You did not fill out all the required information. Please try again." );
				m.SendGump( new ViewTrackerEntryGump( m, m_Entry, m_Page ) );
				return;
			}

			if ( info.ButtonID == 0 )
			{
				m.SendGump( new AddCommentGump( m, m_Entry, m_Page ) );
			}
			if ( info.ButtonID == 1 )
			{
				m_Entry.AddComment( m.Name, info.TextEntries[0].Text );
				m.SendMessage( "Your comment was added." );
				m.SendGump( new ViewTrackerEntryGump( m, m_Entry, m_Page ) );
			}
		}
	}
}
