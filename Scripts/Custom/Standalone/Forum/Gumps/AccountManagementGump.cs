using Server.Gumps;

namespace Server.Forums
{
    public class AccountManagementGump : Gump
	{
        private AuthorStatistics m_Ast;

		public AccountManagementGump( AuthorStatistics ast ) : base( 0, 0 )
		{
            m_Ast = ast;
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(13, 17, 549, 174, 9200);
			this.AddLabel(32, 28, 0, @"Account Management - User: " + ast.Name + " - Rank: " + /*ast.RankTitle*/"<NOT IMPLEMENTED>" + ( ast.CustomRankAllowed ? " - Custom" : " - Not Custom" ) );
			this.AddLabel(66, 80, 0, @"Ban/Unban - Current: " + (ast.Banned ? "BANNED" : "ACTIVE") );
			this.AddLabel(66, 102, 0, @"Give Warning <NOT IMPLEMENTED>");
            this.AddLabel( 66, 124, 0, @"Change Signature <NOT IMPLEMENTED>" );
			this.AddLabel(66, 146, 0, @"Allow Custom Rank - Current: " + (ast.CustomRankAllowed ? "ALLOWED" : "NOT ALLOWED") );
            this.AddButton( 29, 78, 4005, 4005, ( int )Buttons.BanButton, GumpButtonType.Reply, 0 );
            this.AddButton( 29, 146, 4005, 4005, ( int )Buttons.AllowCustomRank, GumpButtonType.Reply, 0 );
            this.AddButton( 29, 123, 4005, 4005, ( int )Buttons.ChangeSigButton, GumpButtonType.Reply, 0 );
            this.AddButton( 29, 100, 4005, 4005, ( int )Buttons.WarningButton, GumpButtonType.Reply, 0 );
            this.AddLabel( 32, 48, 0, @"Status: " + ( ast.Banned ? " BANNED " : " ACTIVE " ) + "Posts: " + ast.PostCount );

		}
		
		public enum Buttons
		{
			BanButton = 1,
			AllowCustomRank = 2,
			ChangeSigButton = 3,
			WarningButton = 4,
		}

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile pm = ( Mobile )sender.Mobile;

            if( pm == null )
                return;

            switch (info.ButtonID)
            {
                case 1:
                    {
                        if (m_Ast.Banned)
                            m_Ast.Banned = false;
                        else
                            m_Ast.Banned = true;

                        pm.CloseGump(typeof(AccountManagementGump));
                        pm.SendGump(new AccountManagementGump(m_Ast));
                        break;
                    }
                case 2:
                    {
                        if (m_Ast.CustomRankAllowed)
                            m_Ast.CustomRankAllowed = false;
                        else
                            m_Ast.CustomRankAllowed = true;

                        pm.CloseGump(typeof(AccountManagementGump));
                        pm.SendGump( new AccountManagementGump( m_Ast ) );
                        break;
                    }
                case 3:
                    {
                        pm.CloseGump(typeof(AccountManagementGump));
                        pm.SendGump( new AccountManagementGump( m_Ast ) );
                        break;
                    }
                case 4:
                    {
                        pm.CloseGump(typeof(AccountManagementGump));
                        pm.SendGump( new AccountManagementGump( m_Ast ) );
                        break;
                    }
            }
        }
	}
}