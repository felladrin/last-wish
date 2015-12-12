using System;
using Server.Gumps;

namespace Server.Forums
{
    public class InputGump : Gump
	{
        public enum Buttons
        {
            TextEntry2 = 1,
            Set = 2,
        }

        private int m_Value;
        private string m_Property;

		public InputGump( int value, string property ) : base( 0, 0 )
		{
            m_Value = value;
            m_Property = property;
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(19, 22, 321, 117, 9200);
			AddLabel(29, 34, 0, @"Current Property: " + m_Property );
			AddLabel(29, 56, 0, @"Current Value: " + m_Value.ToString() );
			AddImageTiled(30, 81, 290, 21, 2624);
			AddAlphaRegion(30, 81, 290, 20);
            AddTextEntry( 32, 83, 288, 17, 38, ( int )Buttons.TextEntry2, value.ToString() );
            AddButton( 29, 108, 4005, 4005, ( int )Buttons.Set, GumpButtonType.Reply, 0 );
			AddLabel(64, 110, 0, @"Set");

		}

        public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
        {
            Mobile m_Player = ( Mobile )sender.Mobile;

            if( m_Player == null )
                return;

            switch( info.ButtonID )
            {
                case 2:
                    {
                        TextRelay value = info.GetTextEntry( 1 );

                        int number;
                        try
                        {
                            number = Convert.ToInt32( value.Text );
                        }
                        catch
                        {
                            m_Player.SendMessage( "That value was not valid please try again." );
                            m_Player.CloseGump( typeof( InputGump ) );
                            m_Player.SendGump( new InputGump( m_Value, m_Property ) );
                            break;
                        }

                        if( number > 2147483647 || number < 0 )
                        {
                            m_Player.SendMessage( "This value may not be below 0 or above 2147483647. Please try again." );
                            m_Player.CloseGump( typeof( InputGump ) );
                            m_Player.SendGump( new InputGump( m_Value, m_Property ) );
                            break;
                        }

                        switch( m_Property.ToLower() )
                        {
                            case "purge days":
                                {
                                    ForumCore.AutoCleanupDays = number;
                                    break;
                                }
                            case "minimum characters":
                                {
                                    ForumCore.MinPostCharactersCount = number;
                                    break;
                                }
                            case "maximum characters":
                                {
                                    ForumCore.MaxPostCharactersCount = number;
                                    break;
                                }
                        }

                        m_Player.CloseGump( typeof( AdministrationGump ) );
                        m_Player.SendGump( new AdministrationGump( ) );
                        break;
                    }
            }
        }
    }
}