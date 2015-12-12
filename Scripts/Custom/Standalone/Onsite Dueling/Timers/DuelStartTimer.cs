/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Timers/DuelStartTimer.cs#3 $

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
	public class DuelStartTimer : Timer
	{
		internal Duel m_Duel;

		public DuelStartTimer(Duel duel)
			: base(TimeSpan.FromSeconds(120.0))
		{
			m_Duel = duel;
		}

		protected override void OnTick()
		{
			m_Duel.Broadcast(32, "The duel has timed out...");

			if (m_Duel != null)
				DuelController.DestroyDuel(m_Duel);

			if (DuelController.DuelStartTimeoutTable.ContainsKey(m_Duel.Creator.Serial))
				DuelController.DuelStartTimeoutTable.Remove(m_Duel.Creator.Serial);

			Stop();
		}
	}
}
