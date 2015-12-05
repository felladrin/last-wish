using System;
using Server.Gumps;

namespace Server.Engines.Tracker
{
	public class AddIssueGump : Gump
	{
		private int m_Page;

		public AddIssueGump( Mobile from )
			: this( from, 0 )
		{
		}

		public AddIssueGump( Mobile from, int page )
			: base( 20, 20 )
		{
			m_Page = page;

			TrackerSystem.CloseGumps( from );

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage( 0 );
			AddBackground( 12, 63, 493, 277, 9200 );
			AddLabel( 18, 68, 254, "Please make sure the issue isnt already listed in [issue before you create" );
			AddLabel( 15, 83, 254, " a new issue entry." );
			AddLabel( 20, 109, 254, "Title" );
			AddAlphaRegion( 20, 133, 471, 20 );
			AddTextEntry( 20, 133, 471, 20, 254, 0, "" );
			AddLabel( 20, 157, 254, "Description" );
			AddAlphaRegion( 20, 178, 469, 129 );
			AddTextEntry( 20, 178, 469, 129, 254, 1, "" );
			AddButton( 427, 310, 247, 248, 3, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Mobile m = sender.Mobile;

			if ( m == null )
				return;

			TextRelay[] relays = info.TextEntries;

			if ( info.ButtonID == 0 )
			{
				m.SendGump( new TrackerGump( m, m_Page ) );
			}
			else if ( info.ButtonID == 3 )
			{
				if ( String.IsNullOrEmpty( relays[0].Text ) || String.IsNullOrEmpty( relays[1].Text ) || ( relays[0].Text == " " || relays[1].Text == " " ) )
				{
					m.SendMessage( "You did not fill out all the required information." );
					m.SendGump( new AddIssueGump( m ) );
					return;
				}
				else
				{
					new TrackerEntry( m.Name, relays[0].Text, relays[1].Text );
					m.SendMessage( "The tracker was added" );
                    World.Broadcast(64, false, "{0} has just reported an issue. Type [issue to check it out!", m.Name);
					m.SendGump( new TrackerGump( m, m_Page ) );
				}
			}
		}
	}
}
