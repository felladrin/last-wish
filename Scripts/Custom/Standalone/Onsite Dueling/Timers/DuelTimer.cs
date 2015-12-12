/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Timers/DuelTimer.cs#3 $

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

using System;
using System.Collections.Generic;

using Server;
using Server.Mobiles;

namespace Server.Engines.Dueling
{
    public class DuelTimer : Timer
    {
        internal Duel m_Duel;
        private int m_Seconds;
        private int m_Countdown = 10;

        public int SecondsRemaining { get { return m_Seconds; } }

        public DuelTimer( Duel duel, int seconds ) : base( TimeSpan.FromSeconds( 0 ), TimeSpan.FromSeconds( 1.0 ) )
        {
            m_Seconds = seconds;
            m_Duel = duel;
        }

        protected override void OnTick()
        {
            if( m_Countdown == 0 )
            {
				//if( m_Seconds == DuelController.Instance.DuelLengthInSeconds )
				//{
				//    m_Duel.Started = true;
				//    m_Duel.Broadcast( "The duel has begun!" );
				//    foreach (Mobile m in m_Duel.Contestants)
				//        m.SendEverything();
				//}
                if( m_Seconds % 300 == 0 )
                {
                    int min = m_Seconds / 60;

                    m_Duel.Broadcast( String.Format( "{0} minutes remaining in the duel.", min ) );
                }

                if( m_Seconds == 0 )
                {
					m_Duel.Broadcast("The duel has timed out!");
                    m_Duel.EndDuel();
                    Stop();
                }

                m_Seconds--;
            }
            else
            {
				m_Duel.Broadcast(String.Format("Duel will begin in {0} second{1}...", m_Countdown, m_Countdown == 1 ? "" : "s"));
                m_Countdown--;

                if (m_Countdown == 0)
                {
					m_Duel.Started = true;
                    for (int i = 0; i < m_Duel.Attackers.Count; i++)
                    {
						m_Duel.Attackers[i].Frozen = false;
						m_Duel.Defenders[i].Frozen = false;
                        m_Duel.Attackers[i].Delta(MobileDelta.Noto);
                        m_Duel.Defenders[i].Delta(MobileDelta.Noto);
                        m_Duel.Attackers[i].InvalidateProperties();
                        m_Duel.Defenders[i].InvalidateProperties();
                    }
					m_Duel.Broadcast("Fight!");
				}
            }
        }
    }
}
