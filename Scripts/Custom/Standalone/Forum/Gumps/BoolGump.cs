using Server.Gumps;

namespace Server.Forums
{
    public class BoolGump : Gump
    {
        public enum Buttons
        {
            Enable = 1,
            Disable = 2,
        }

		public BoolGump() : base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(19, 22, 321, 117, 9200);
			this.AddLabel(29, 34, 0, @"Current Property: Purge System");
			this.AddLabel(29, 56, 0, @"Current Value: " + GetValue( ForumCore.AutoCleanup ) );
            this.AddButton( 30, 82, 4005, 4005, ( int )Buttons.Enable, GumpButtonType.Reply, 0 );
            this.AddButton( 30, 103, 4005, 4005, ( int )Buttons.Disable, GumpButtonType.Reply, 0 );
			this.AddLabel(66, 84, 0, @"Enable");
			this.AddLabel(66, 106, 0, @"Disable");
		}

        public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
        {
            Mobile m_Player = ( Mobile )sender.Mobile;

            if( m_Player == null )
                return;

            switch( info.ButtonID )
            {
                case 1:
                    {
                        ForumCore.AutoCleanup = true;
                        m_Player.CloseGump( typeof( AdministrationGump ) );
                        m_Player.SendGump( new AdministrationGump( ) );
                        break;
                    }
                case 2:
                    {
                        ForumCore.AutoCleanup = false;
                        m_Player.CloseGump( typeof( AdministrationGump ) );
                        m_Player.SendGump( new AdministrationGump( ) );
                        break;
                    }
            }
        }

        private string GetValue( bool value )
        {
            if( value )
                return "Enabled";

            return "Disabled";
        }
	}
}