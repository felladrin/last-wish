using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Forums
{
	public class AccessLevelGump : Gump
	{

        public enum Buttons
        {
            Player = 1,
            Counselor = 2,
            GameMaster = 3,
            Seer = 4,
            Admin = 5,
        }

        private AccessLevel m_CurrentLevel;
        private string m_CurrentProperty;

		public AccessLevelGump( AccessLevel level, string property ) : base( 0, 0 )
		{
            m_CurrentLevel = level;
            m_CurrentProperty = property;
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(19, 22, 321, 187, 9200);
            AddLabel( 29, 34, 0, @"Current Property: " + m_CurrentProperty);
			AddLabel(29, 56, 0, @"Current Value: " + GetAccessLevelString ( m_CurrentLevel ) );
            AddButton( 30, 82, 4005, 4005, ( int )Buttons.Player, GumpButtonType.Reply, 0 );
            AddButton( 30, 170, 4005, 4005, ( int )Buttons.Admin, GumpButtonType.Reply, 0 );
            AddButton( 30, 148, 4005, 4005, ( int )Buttons.Seer, GumpButtonType.Reply, 0 );
            AddButton( 30, 126, 4005, 4005, ( int )Buttons.GameMaster, GumpButtonType.Reply, 0 );
            AddButton( 30, 103, 4005, 4005, ( int )Buttons.Counselor, GumpButtonType.Reply, 0 );
			AddLabel(66, 84, 0, @"Player");
			AddLabel(66, 172, 0, @"Administrator");
			AddLabel(66, 150, 0, @"Seer");
			AddLabel(66, 128, 0, @"Game Master");
			AddLabel(66, 106, 0, @"Counselor");
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
                        switch( m_CurrentProperty.ToLower() )
                        {
                            case "lock access":
                                {
                                    ForumCore.ThreadLockAccesLevel = AccessLevel.Player;
                                    break;
                                }
                            case "delete access":
                                {
                                    ForumCore.ThreadDeleteAccessLevel = AccessLevel.Player;
                                    break;
                                }
                        }
                        m_Player.CloseGump( typeof( AdministrationGump ) );
                        m_Player.SendGump( new AdministrationGump() );
                        break;
                    }
                case 2:
                    {
                        switch( m_CurrentProperty.ToLower() )
                        {
                            case "lock access":
                                {
                                    ForumCore.ThreadLockAccesLevel = AccessLevel.Counselor;
                                    break;
                                }
                            case "delete access":
                                {
                                    ForumCore.ThreadDeleteAccessLevel = AccessLevel.Counselor;
                                    break;
                                }
                        }
                        m_Player.CloseGump( typeof( AdministrationGump ) );
                        m_Player.SendGump( new AdministrationGump( ) );
                        break;
                    }
                case 3:
                    {
                        switch( m_CurrentProperty.ToLower() )
                        {
                            case "lock access":
                                {
                                    ForumCore.ThreadLockAccesLevel = AccessLevel.GameMaster;
                                    break;
                                }
                            case "delete access":
                                {
                                    ForumCore.ThreadDeleteAccessLevel = AccessLevel.GameMaster;
                                    break;
                                }
                        }
                        m_Player.CloseGump( typeof( AdministrationGump ) );
                        m_Player.SendGump( new AdministrationGump( ) );
                        break;
                    }
                case 4:
                    {
                        switch( m_CurrentProperty.ToLower() )
                        {
                            case "lock access":
                                {
                                    ForumCore.ThreadLockAccesLevel = AccessLevel.Seer;
                                    break;
                                }
                            case "delete access":
                                {
                                    ForumCore.ThreadDeleteAccessLevel = AccessLevel.Seer;
                                    break;
                                }
                        }

                        m_Player.CloseGump( typeof( AdministrationGump ) );
                        m_Player.SendGump( new AdministrationGump( ) );
                        break;
                    }
                case 5:
                    {
                        switch( m_CurrentProperty.ToLower() )
                        {
                            case "lock access":
                                {
                                    ForumCore.ThreadLockAccesLevel = AccessLevel.Administrator;
                                    break;
                                }
                            case "delete access":
                                {
                                    ForumCore.ThreadDeleteAccessLevel = AccessLevel.Administrator;
                                    break;
                                }
                        }
                        m_Player.CloseGump( typeof( AdministrationGump ) );
                        m_Player.SendGump( new AdministrationGump( ) );
                        break;
                    }
            }
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
	}
}