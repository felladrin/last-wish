using Server.Gumps;

namespace Server.Forums
{
    public class ModDeleteCheckGump : Gump
    {
        public enum Buttons
        {
            YesButton = 1,
            NoButton = 2,
        }

        private Mobile m_Mod;

		public ModDeleteCheckGump( Mobile moderator ) : base( 0, 0 )
		{
            m_Mod = moderator;
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(27, 24, 336, 116, 9200);
			AddLabel(40, 35, 0, @"Are you sure you wish to remove moderator?");
			AddButton(105, 97, 4005, 4005, (int)Buttons.YesButton, GumpButtonType.Reply, 0);
			AddLabel(40, 54, 0, @"Moderator: ");
			AddLabel(137, 98, 0, @"Yes");
			AddButton(208, 97, 4005, 4005, (int)Buttons.NoButton, GumpButtonType.Reply, 0);
			AddLabel(240, 98, 0, @"No");

		}

        public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
        {
            Mobile m_Player = ( Mobile )sender.Mobile;

            if( m_Player == null )
                return;

            switch( info.ButtonID )
            {
                case 1://Yes
                    {
                        if( ForumCore.Moderators.Contains( m_Mod ) )
                        {
                            ForumCore.Moderators.Remove( m_Mod );
                            m_Player.SendMessage( "{0} has been removed from the moderator list.", m_Mod.Name );
                        }

                        m_Player.CloseGump( typeof( ModManagementGump ) );
                        m_Player.SendGump( new ModManagementGump( 0 ) ); 
                        break;
                    }
                case 2://No
                    {
                        m_Player.CloseGump( typeof( ModManagementGump ) );
                        m_Player.SendGump( new ModManagementGump( 0 ) );
                        break;
                    }
            }
        }		
	}
}