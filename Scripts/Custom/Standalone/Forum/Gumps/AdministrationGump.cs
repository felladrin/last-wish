using Server.Gumps;

namespace Server.Forums
{
    public class AdministrationGump : Gump
	{
        private string m_Break = "<br>";

		public AdministrationGump()	: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(21, 32, 442, 515, 9200);
            AddButton( 38, 80, 4005, 4005, ( int )Buttons.LockAccessButton, GumpButtonType.Reply, 0 );
            AddButton( 38, 234, 4005, 4005, ( int )Buttons.AccountManagement, GumpButtonType.Reply, 0 );
            AddButton( 38, 212, 4005, 4005, ( int )Buttons.ModeratorsButton, GumpButtonType.Reply, 0 );
            AddButton( 38, 190, 4005, 4005, ( int )Buttons.PurgeEnableButton, GumpButtonType.Reply, 0 );
            AddButton( 38, 168, 4005, 4005, ( int )Buttons.PurgeDaysButton, GumpButtonType.Reply, 0 );
            AddButton( 38, 146, 4005, 4005, ( int )Buttons.MinCharButton, GumpButtonType.Reply, 0 );
            AddButton( 38, 124, 4005, 4005, ( int )Buttons.MaxCharButton, GumpButtonType.Reply, 0 );
            AddButton( 38, 102, 4005, 4005, ( int )Buttons.DeleteAccessButton, GumpButtonType.Reply, 0 );
			AddLabel(41, 52, 55, @"Sorious' In-game Forum Administration Panel");
			AddLabel(77, 81, 0, @"Change lock access level");
            AddLabel( 77, 235, 0, @"Account Management" );
			AddLabel(77, 213, 0, @"Manage moderators");
			AddLabel(77, 191, 0, @"Turn thread purging on/off");
			AddLabel(77, 169, 0, @"Set days for old thread purging");
			AddLabel(77, 147, 0, @"Change minimum characters per post");
			AddLabel(77, 125, 0, @"Change maximum characters per post");
			AddLabel(77, 103, 0, @"Change delete access level");
			AddImageTiled(43, 350, 395, 5, 9201);
            AddHtml( 36, 364, 409, 160, GetStatus(), false, true );
		}
		
		public enum Buttons
		{
			LockAccessButton = 1,
            ModeratorsButton = 2,
            PurgeEnableButton = 3,
            PurgeDaysButton = 4,
            MinCharButton = 5,
            MaxCharButton = 6,
            DeleteAccessButton = 7,
            AccountManagement = 8,
		}

        private string GetStatus()
        {
            string status = "<p>";
            status = status + "Lock access: \t\t\t" + GetAccessLevelString( ForumCore.ThreadLockAccesLevel ) + m_Break;
            status = status + "Delete access: \t\t\t" + GetAccessLevelString( ForumCore.ThreadDeleteAccessLevel ) + m_Break;
            status = status + "Min char per post: \t\t" + ForumCore.MinPostCharactersCount.ToString() + m_Break;
            status = status + "Max char per post: \t\t" + ForumCore.MaxPostCharactersCount.ToString() + m_Break;
            status = status + "Old thread purge enabled: \t" + ( ForumCore.AutoCleanup ? "Enabled" : "Disabled" ) + m_Break;
            status = status + "Days till purge: \t\t" + ForumCore.AutoCleanupDays.ToString() + "</p>";
            return status;
        }

        private string GetAccessLevelString( AccessLevel access )
        {
            switch( access )
            {
                case AccessLevel.Player: { return "Player"; }
                case AccessLevel.Counselor: { return "Counselor"; }
                case AccessLevel.GameMaster: { return "Game Master"; }
                case AccessLevel.Seer: { return "Seer"; }
                case AccessLevel.Administrator: { return "Administrator"; }
                default: { return "Error"; }//Should never get this
            }
        }

        public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
        {
            Mobile m_Player = ( Mobile )sender.Mobile;

            if( m_Player == null )
                return;

            switch( info.ButtonID )
            {
                case 1://Lock access
                    {
                        m_Player.CloseGump( typeof( AccessLevelGump ) );
                        m_Player.SendGump( new AccessLevelGump(  ForumCore.ThreadLockAccesLevel, "Lock Access" ) );
                        break;
                    }
                case 2://Moderators
                    {
                        m_Player.CloseGump( typeof( ModManagementGump ) );
                        m_Player.SendGump( new ModManagementGump( 0 ) );
                        break;
                    }
                case 3://Purge Enable
                    {
                        m_Player.CloseGump( typeof( BoolGump ) );
                        m_Player.SendGump( new BoolGump( ) );
                        break;
                    }
                case 4://Purge Days
                    {
                        m_Player.CloseGump( typeof( InputGump ) );
                        m_Player.SendGump( new InputGump( ForumCore.AutoCleanupDays, "Purge Days" ) );
                        break;
                    }
                case 5://Min Char
                    {
                        m_Player.CloseGump( typeof( InputGump ) );
                        m_Player.SendGump( new InputGump( ForumCore.MinPostCharactersCount, "Minimum Characters" ) );
                        break;
                    }
                case 6://Max Char
                    {
                        m_Player.CloseGump( typeof( InputGump ) );
                        m_Player.SendGump( new InputGump( ForumCore.MaxPostCharactersCount, "Maximum CHaracters" ) );
                        break;
                    }
                case 7://Delete Acces
                    {
                        m_Player.CloseGump( typeof( AccessLevelGump ) );
                        m_Player.SendGump( new AccessLevelGump(  ForumCore.ThreadDeleteAccessLevel, "Delete Access" ) );
                        break;
                    }
                case 8://Account Search
                    {
                        m_Player.CloseGump(typeof(AccountSearch));
                        m_Player.SendGump(new AccountSearch());                        
                        break;
                    }
            }
        }
	}
}