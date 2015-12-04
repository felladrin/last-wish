using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Forums
{
	public class AccountSearch : Gump
	{
		public AccountSearch()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(19, 22, 321, 102, 9200);
			this.AddLabel(29, 34, 0, @"Account Search - Input the player's name");
			this.AddImageTiled(30, 61, 290, 21, 2624);
			this.AddAlphaRegion(30, 61, 290, 20);
			this.AddTextEntry(32, 63, 288, 17, 38, (int)Buttons.TextEntry2, @"");
            this.AddButton( 29, 88, 4005, 4005, ( int )Buttons.Button2, GumpButtonType.Reply, 0 );
			this.AddLabel(64, 90, 0, @"Search");

		}
		
		public enum Buttons
		{
			TextEntry2 = 1,
			Button2 = 2,
		}

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile pm = ( Mobile )sender.Mobile;

            if( pm == null )
                return;

            switch (info.ButtonID)
            {
                case 2:
                    {
                        TextRelay text = info.GetTextEntry( 1 );

                        if (text == null || text.Text == "")
                        {
                            pm.CloseGump(typeof(AccountSearch));
                            pm.SendGump(new AccountSearch());
                            return;
                        }

                        ArrayList authors = new ArrayList();

                        if (ForumCore.AuthorExists( out authors, text.Text))
                        {
                            pm.CloseGump( typeof(AccountListingGump));
                            pm.SendGump( new AccountListingGump( authors, 0 ) );
                        }
                        else
                        {
                            pm.SendMessage("Either that player does not exist, or no posts have been made by that player.");
                        }
                        break;
                    }
            }
        }

	}
}