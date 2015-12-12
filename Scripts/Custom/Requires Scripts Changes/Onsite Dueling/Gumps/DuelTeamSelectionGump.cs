/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Gumps/DuelTeamSelectionGump.cs#3 $

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

*/

using System.Collections.Generic;
using Server.Gumps;

namespace Server.Engines.Dueling
{
    public class DuelTeamSelectionGump : Gump
	{
        private List<MobileEntry> m_MobileEntries;
        private Duel m_Duel;

		public DuelTeamSelectionGump( Duel duel )
			: base( 200, 200 )
		{
            m_Duel = duel;
            m_MobileEntries = new List<MobileEntry>();

			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(36, 25, 402, 375, 3500);
            this.AddLabel( 166, 40, 36, @"Onsite Duel System "+DuelController.Version );
            this.AddLabel( 166, 39, 36, @"Onsite Duel System "+DuelController.Version );
			this.AddButton(401, 46, 3, 4, (int)Buttons.closeBtn, GumpButtonType.Reply, 0);
            this.AddLabel( 129, 56, 36, @"Please select the team for each player" );
            this.AddLabel( 129, 55, 36, @"Please select the team for each player" );
            this.AddLabel( 100, 80, 36, @"Players" );
            this.AddLabel( 100, 79, 36, @"Players" );
            this.AddLabel( 263, 80, 36, @"Team 1" );
            this.AddLabel( 263, 79, 36, @"Team 1" );
            this.AddLabel( 327, 80, 36, @"Team 2" );
            this.AddLabel( 327, 79, 36, @"Team 2" );
            this.AddLabel( 121, 360, 36, @"Start" );
            this.AddLabel( 121, 359, 36, @"Start" );
            this.AddButton( 103, 363, 4034, 4034, ( int )Buttons.startBtn, GumpButtonType.Reply, 0 );

            for( int i = 0; i < duel.Contestants.Count; i++ )
                AddEntry( duel.Contestants[i], i );         
		}

        private void AddEntry( Mobile mobile, int i )
        {
            int y = ( i * 25 ) + 108;
            int x = 100;

            int buttonOne = mobile.Serial.Value;
            int buttonTwo = mobile.Serial.Value + 1;

            MobileEntry mEntry = new MobileEntry( mobile, buttonOne, buttonTwo );
            m_MobileEntries.Add( mEntry );

            AddGroup( i );
            AddLabel( x, y, 36, mobile.Name );     
            
            y = ( i * 25 ) + 104;
            x = 272;

            AddRadio( x, y, 9792, 9793, ( i < 5 ), buttonOne );  
               
            x = 338;
            AddRadio( x, y, 9792, 9793, ( i > 4 ), buttonTwo );
       }
		
		private enum Buttons
		{
			closeBtn,
            startBtn
		}

        public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
        {
            Mobile m = sender.Mobile;

            if( m == null || m_Duel == null )
                return;

            int[] sw = info.Switches;

            if( sw.Length != ( m_Duel.Attackers.Capacity + m_Duel.Defenders.Capacity ) )
            {
                m_Duel.Creator.SendMessage( "Some players were not assigned to a team. Please try again." );
                m_Duel.Creator.CloseGump( typeof( DuelTeamSelectionGump ) );
                m_Duel.Creator.SendGump( new DuelTeamSelectionGump( m_Duel ) );
                return;
            }

            List<int> switches = new List<int>(sw);

            switch( (Buttons)info.ButtonID )
            {
                case Buttons.closeBtn:
                    {
                        m_Duel.Broadcast( "The duel was canceled." );
                        DuelController.DestroyDuel( m_Duel );
                        break;
                    }
                case Buttons.startBtn:
                    {
                        int teamCheckOne = 0;
                        int teamCheckTwo = 0;

                        for( int i = 0; i < m_MobileEntries.Count; i++ )
                            if( switches.Contains( m_MobileEntries[i].TeamOne ) )
                                teamCheckOne++;
                            else
                                teamCheckTwo++;                     

                        if( teamCheckOne != teamCheckTwo )
                        {
                            m_Duel.Creator.SendMessage( "The two teams were not even, please try again." );
                            m_Duel.Creator.CloseGump( typeof( DuelTeamSelectionGump ) );
                            m_Duel.Creator.SendGump( new DuelTeamSelectionGump( m_Duel ) );
                            return;
                        }
                        else
                        {
                            for( int i = 0; i < m_MobileEntries.Count; i++ )
                                if( switches.Contains( m_MobileEntries[i].TeamOne ) )
                                    m_Duel.Attackers.Add( m_MobileEntries[i].Mobile );
                                else
                                    m_Duel.Defenders.Add( m_MobileEntries[i].Mobile );

                            //m_Duel.Contestants.Clear();

                            m_Duel.CheckBegin();
                        }

                        break;
                    }
            }
        }
	}

    public class MobileEntry
    {
        private int _TeamOne;
        private int _TeamTwo;
        private Mobile _Mobile;

        public int TeamOne { get { return _TeamOne; } }
        public int TeamTwo { get { return _TeamTwo; } }
        public Mobile Mobile { get { return _Mobile; } }

        public MobileEntry( Mobile mobile, int teamOne, int teamTwo )
        {
            _Mobile = mobile;
            _TeamOne = teamOne;
            _TeamTwo = teamTwo;
        }
    }
}