using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Forums
{
	public class AccountListingGump : Gump
	{
        private int m_Page;
        private ArrayList m_List;
        private ArrayList m_CurrentList;

 		public AccountListingGump( ArrayList list, int page ) : base( 0, 0 )
		{
            m_Page = page;
            m_List = list;
            m_CurrentList = new ArrayList();
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
            this.AddLabel(32, 30, 0, @"Account Management");
			this.AddBackground(19, 22, 321, 65 + ( list.Count * 25 ), 9200);
            this.AddButton(305, 29, 2708, 248, (int)Buttons.Close, GumpButtonType.Reply, 0);

            bool pages = ( list.Count > 10 );
            bool more = false;

            int index =  m_Page * 10;

            if( index < 0 )
                index = 0;

            int maxcount = index + 10;
            int offset = 0;

            for (int i = index; i < list.Count; i++)
            {
                if (i >= maxcount)
                {
                    more = true;
                    break;
                }
                AuthorStatistics ast = (AuthorStatistics)list[i];
                m_CurrentList.Add(ast);

                this.AddButton(30, (((22 * (i - index)) + 54) - offset), 4005, 4005, (i - (maxcount - (((m_Page + 1) * 10))) - index), GumpButtonType.Reply, 0);
                this.AddLabel(66, (((22 * (i - index)) + 54) - offset), 0, ast.Name );
            }

            if (pages)
            {
                if (more)
                    this.AddButton(308, 57 + (list.Count * 25), 5541, 5541, (int)Buttons.Next, GumpButtonType.Reply, 0);
            
                if (m_Page > 0)
                    this.AddButton(288, 57 + (list.Count * 25), 5538, 5538, (int)Buttons.Previous, GumpButtonType.Reply, 0);
            }
		}
		
		public enum Buttons
		{
            Close = 11,
            Next = 12,
            Previous = 13,
		}

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile pm = ( Mobile )sender.Mobile;

            if( pm == null )
                return;

            switch (info.ButtonID)
            {
                case 11:
                    {
                        pm.CloseGump(typeof(AccountSearch));
                        pm.SendGump(new AccountSearch());
                        break;
                    }
                case 12:
                    {
                        int page = m_Page + 1;
                        pm.CloseGump(typeof(AccountListingGump));
                        pm.SendGump( new AccountListingGump( m_List, page ) );
                        break;
                    }
                case 13:
                    {
                        int page = m_Page - 1;
                        pm.CloseGump(typeof(AccountListingGump));
                        pm.SendGump( new AccountListingGump( m_List, page ) );
                        break;
                    }
                default:
                    {
                        AuthorStatistics ast = (AuthorStatistics)m_CurrentList[info.ButtonID];
                        pm.CloseGump(typeof(AccountManagementGump));
                        pm.SendGump( new AccountManagementGump( ast ) );
                        break;
                    }
            }
        }
	}
}