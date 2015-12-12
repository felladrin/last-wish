/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Timers/TimeoutTimer.cs#2 $

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

namespace Server.Engines.Dueling
{
    public class TimeoutTimer : Timer
    {
        internal Mobile m_Mobile;
        internal Duel m_Duel;

        public TimeoutTimer( Duel duel, Mobile m ) : base( TimeSpan.FromSeconds( 15.0 ) )
        {
            m_Mobile = m;
            m_Duel = duel;
        }

        protected override void OnTick()
        {
            m_Mobile.CloseGump( typeof( DuelAcceptGump ) );
            m_Mobile.SendMessage("You've decided not to join the duel.");
            m_Duel.SpotsRemaing++;
            m_Duel.Broadcast( m_Mobile.Name + " declined to join the duel." );
            CheckTarget();
            Stop();
        }

        private void CheckTarget()
        {
            if( !( m_Duel.Creator.Target is DuelTarget ) )
            {
                m_Duel.Creator.SendMessage( "Please select another player to join the duel." );
                m_Duel.Creator.Target = new DuelTarget( m_Duel.Creator, m_Duel );
            }
        }
    }
}
