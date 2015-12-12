/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/ScoreKeeping/DuelInfo.cs#2 $

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

namespace Server.Engines.Dueling
{
    public class DuelInfo
	{
		private TimeSpan m_DuelTime;
		private List<Mobile> m_AgainstList;

		public TimeSpan DuelTime { get { return m_DuelTime; } }
		public List<Mobile> AgainstList { get { return m_AgainstList; } }

		public DuelInfo(List<Mobile> team, TimeSpan duelTime)
		{
			m_AgainstList = team;
			m_DuelTime = duelTime;
		}

		public DuelInfo(GenericReader reader)
		{
			int version = reader.ReadEncodedInt();

			switch (version)
			{
				case 0:
					{
						m_DuelTime = reader.ReadTimeSpan();
						m_AgainstList = reader.ReadStrongMobileList();
						break;
					}
			}
		}

		internal void Serialize(GenericWriter writer)
		{
			writer.WriteEncodedInt(0); // version

			writer.Write((TimeSpan)m_DuelTime);
			writer.WriteMobileList<Mobile>(m_AgainstList);
		}
	}
}
