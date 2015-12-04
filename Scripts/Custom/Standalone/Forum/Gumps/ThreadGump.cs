using System;

using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Forums
{
	public class ThreadGump : Gump
	{
        private Mobile m_Player;
        private ThreadEntry m_ThreadEntry;
        private int LabelColor = 0xFFFFFF;
        
        public enum Buttons
        {
            Close = 0,
            Reply = 1,
            Delete = 2,
            Lock = 3,
        }

        public ThreadGump( Mobile pm, ThreadEntry te ) : base( 0, 0 )
		{
            m_ThreadEntry = te;
            m_Player = pm;
            if( m_ThreadEntry != null && !m_ThreadEntry.Deleted )
                m_ThreadEntry.AddViewer( pm );

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;
            AddPage( 0 );
            AddBackground( 9, 15, 477, 412, 9200 );
            AddButton( 450, 20, 1151, 1151, ( int )Buttons.Close, GumpButtonType.Reply, 0 );
            AddLabel( 22, 24, 0, @"Subject:" );

            if( m_ThreadEntry != null && !m_ThreadEntry.Deleted )
                AddLabel(22, 64, 0, @"Author: " + ((m_ThreadEntry.ThreadCreator == null) ? "Unknown Author" : m_ThreadEntry.ThreadCreator.Name));

            if( m_ThreadEntry != null && !m_ThreadEntry.Deleted )
                AddLabel( 300, 64, 0, @"Date: " + m_ThreadEntry.CreationTime.ToShortDateString() + " at " + m_ThreadEntry.CreationTime.ToShortTimeString() );

            AddImageTiled( 21, 44, 447, 21, 2624 );
            AddAlphaRegion( 21, 44, 446, 20 );

            if( m_ThreadEntry != null && !m_ThreadEntry.Deleted )
                AddHtml( 23, 46, 446, 20, Color( m_ThreadEntry.Subject, LabelColor ), false, false );

            AddImageTiled( 22, 88, 446, 302, 2624 );
            AddAlphaRegion( 22, 87, 446, 302 );

            if( m_ThreadEntry != null && !m_ThreadEntry.Deleted )
                AddHtml( 24, 89, 446, 302, Color( m_ThreadEntry.GetThreadInfo(), LabelColor ), false, true );

            AddButton( 22, 395, 4029, 4029, ( int )Buttons.Reply, GumpButtonType.Reply, 0 );
            AddLabel( 54, 395, 0, @"Reply" );
            if( m_Player.AccessLevel >= ForumCore.ThreadDeleteAccessLevel || ForumCore.Moderators.Contains( m_Player ) )
            {
                AddLabel( 149, 395, 0, @"Delete" );
                AddButton( 117, 395, 4020, 4020, ( int )Buttons.Delete, GumpButtonType.Reply, 0 );
            }
            if( m_Player.AccessLevel >= ForumCore.ThreadLockAccesLevel || ForumCore.Moderators.Contains( m_Player ) )
            {
                AddButton( 219, 395, 4017, 4017, ( int )Buttons.Lock, GumpButtonType.Reply, 0 );
                AddLabel( 253, 396, 0, m_ThreadEntry.Locked ? "Unlock" : "Lock" );
            }
		}

        public string Color( string text, int color )
        {
            return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
        }

        public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
        {
            Mobile m_Player = (Mobile)sender.Mobile;

            if( m_Player == null )
                return;

            switch( info.ButtonID )
            {
                case 0:
                {
                    m_Player.CloseGump( typeof( ForumGump ) );
                    m_Player.SendGump( new ForumGump( m_Player, 0 ) );
                    break;
                }
                case 1://Reply
                {
                    if( m_ThreadEntry.Locked && m_ThreadEntry != null && !m_ThreadEntry.Deleted )
                    {
                        m_Player.SendMessage("This thread has been locked and may not be edited!");
                        break;
                    }

                    m_Player.CloseGump( typeof( PostGump ) );
                    m_Player.SendGump( new PostGump( m_Player, false, m_ThreadEntry, "" ) );
                    break;
                }
                case 2://Delete
                {
                    if( ( m_Player.AccessLevel >= ForumCore.ThreadDeleteAccessLevel || ForumCore.Moderators.Contains( m_Player ) ) && m_ThreadEntry != null && !m_ThreadEntry.Deleted )
                    {
                        m_ThreadEntry.Deleted = true;
                        m_Player.SendMessage( "This thread has been queued for deletion!" );
                    }
                    m_Player.CloseGump( typeof( ForumGump ) );
                    m_Player.SendGump( new ForumGump( m_Player, 0 ) );
                    break;
                }
                case 3://Lock
                {
                    if( ( m_Player.AccessLevel >= ForumCore.ThreadLockAccesLevel || ForumCore.Moderators.Contains( m_Player ) ) && m_ThreadEntry != null && !m_ThreadEntry.Deleted )
                    {
                        if( !m_ThreadEntry.Locked )
                        {
                            m_ThreadEntry.Locked = true;
                            m_Player.SendMessage( "The thread has been locked!" );
                        }
                        else
                        {
                            m_ThreadEntry.Locked = false;
                            m_Player.SendMessage( "The thread has been unlocked!" );
                        }
                    }
                    else
                        m_Player.SendMessage( "You do not have sufficient rights to use this function!" );

                    m_Player.CloseGump( typeof( ForumGump ) );
                    m_Player.SendGump( new ForumGump( m_Player, 0 ) );
                    break;
                }
            }
        }
	}
}
