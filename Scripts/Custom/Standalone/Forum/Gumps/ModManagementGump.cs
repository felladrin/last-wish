using System.Collections;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Forums
{
    public class ModManagementGump : Gump
	{
        public enum Buttons
        {
            Add = 11,
            ClearAll = 12,
            NextPage = 13,
            PreviousPage = 14,
            CloseButton = 15,
        }

        private int m_Page;
        private ArrayList m_List;

		public ModManagementGump( int page ) : base( 0, 0 )
		{
            m_Page = page;
            m_List = new ArrayList();
			Closable=false;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(19, 22, 321, 294, 9200);
  
			AddLabel(32, 30, 0, @"Moderator Management");
            AddButton( 30, 52, 4005, 4005, ( int )Buttons.Add, GumpButtonType.Reply, 0 );
			AddLabel(66, 54, 0, @"Add Moderator");
            AddButton( 186, 52, 4005, 4005, ( int )Buttons.ClearAll, GumpButtonType.Reply, 0 );
			AddLabel(222, 54, 0, @"Clear All Mods");
            AddButton( 304, 30, 1151, 1151, ( int )Buttons.CloseButton, GumpButtonType.Reply, 0 );

            bool pages = ( ForumCore.Moderators.Count > 10 );
            bool more = false;

            int index =  m_Page * 10;

            if( index < 0 )
                index = 0;

            int maxcount = index + 10;
            int offset = 0;
            
            for( int i = index; i < ForumCore.Moderators.Count; i++ )
            {
                if( i >= maxcount )
                {
                    more = true;
                    break;
                }

                Mobile m = ( Mobile )ForumCore.Moderators[i];
                if( !m.Deleted && m != null )
                {
                    m_List.Add( m );
                    AddButton( 30, ( ( ( 22 * ( i - index ) ) + 82 ) - offset ), 4005, 4005, ( i - ( maxcount - ( ( ( m_Page + 1 ) * 10 ) ) ) - index ), GumpButtonType.Reply, 0 );
			        AddLabel( 66, ( ( ( 22 * ( i - index ) ) + 84 ) - offset ), 0, m.Name);                    
                }
                else
                {
                    maxcount++;
                    offset += 35;
                }
            }

            if( pages )
            {
                if( more )
                    AddButton( 308, 284, 5541, 5541, ( int )Buttons.NextPage, GumpButtonType.Reply, 0 );

                if( m_Page > 0 )
                    AddButton( 286, 284, 5538, 5538, ( int )Buttons.PreviousPage, GumpButtonType.Reply, 0 );
            }

		}

        public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
        {
            Mobile m_Player = ( Mobile )sender.Mobile;

            if( m_Player == null )
                return;

            switch( info.ButtonID )
            {
                default://Moderator
                    {
                        Mobile m = ( Mobile )ForumCore.Moderators[info.ButtonID];

                        if( !m.Deleted && m != null )
                        {
                            m_Player.CloseGump( typeof( ModDeleteCheckGump ) );
                            m_Player.SendGump( new ModDeleteCheckGump( m ) );
                        }
                        break;
                    }
                case 11://Add
                    {
                        m_Player.Target = new AddModTarget( m_Player );
                        break;
                    }
                case 12://Clear All
                    {
                        ForumCore.Moderators.Clear();
                        m_Player.CloseGump( typeof( ModManagementGump ) );
                        m_Player.SendGump( new ModManagementGump( m_Page ) ); 
                        break; 
                    }
                case 13:// Next Page
                    {
                        int page = m_Page + 1;
                        m_Player.CloseGump( typeof( ModManagementGump ) );
                        m_Player.SendGump( new ModManagementGump( m_Page ) ); 
                        break;
                    }
                case 14://Previous Page
                    {
                        int page = m_Page - 1;
                        m_Player.CloseGump( typeof( ModManagementGump ) );
                        m_Player.SendGump( new ModManagementGump( m_Page ) ); 
                        break;
                    }
                case 15://Close
                    {
                        m_Player.CloseGump( typeof( ModManagementGump ) );
                        m_Player.CloseGump( typeof( AdministrationGump ) );
                        m_Player.SendGump( new AdministrationGump( ) );
                        break;
                    }
            }
        }        
	}

    public class AddModTarget : Target
    {
        private Mobile m_Targeter;

        public AddModTarget( Mobile from )
            : base( 100, false, TargetFlags.None )
        {
            m_Targeter = from;
        }

        protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
        {
            base.OnTargetCancel( from, cancelType );

            m_Targeter.CloseGump( typeof( ModManagementGump ) );
            m_Targeter.SendGump( new ModManagementGump( 0 ) );
        }

        protected override void OnTarget( Mobile from, object target )
        {
            if( target is PlayerMobile )
            {
                PlayerMobile pm = ( PlayerMobile )target;
                if( !ForumCore.Moderators.Contains( pm ) )
                {
                    ForumCore.Moderators.Add( pm );
                    m_Targeter.SendMessage( "That player has is now a moderator." );
                    m_Targeter.CloseGump( typeof( ModManagementGump ) );
                    m_Targeter.SendGump( new ModManagementGump( 0 ) );
                }
                else
                {
                    m_Targeter.SendMessage( "That player is already a moderator." );
                }
            }
            else
            {
                m_Targeter.SendMessage( "Only players may be made moderators." );
            }
        }
    }
}