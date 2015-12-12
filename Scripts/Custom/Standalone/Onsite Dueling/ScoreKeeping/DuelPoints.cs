/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/ScoreKeeping/DuelPoints.cs#3 $

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
	public class DuelPoints
	{
		private Mobile m_Mobile;
		private Dictionary<int, DuelInfo> m_FastestWins;
		private Dictionary<int, DuelInfo> m_LongestWins;
		private Dictionary<int, DuelInfo> m_FastestLoses;
		private Dictionary<int, DuelInfo> m_LongestLoses;
		private Dictionary<int, int> m_Wins;
		private Dictionary<int, int> m_Loses;

		public Mobile Mobile { get { return m_Mobile; } }
		public Dictionary<int, DuelInfo> FastestWins { get { return m_FastestWins; } }
		public Dictionary<int, DuelInfo> LongestWins { get { return m_LongestWins; } }
		public Dictionary<int, DuelInfo> FastestLoses { get { return m_FastestLoses; } }
		public Dictionary<int, DuelInfo> LongestLoses { get { return m_LongestLoses; } }
		public Dictionary<int, int> Wins { get { return m_Wins; } }
		public Dictionary<int, int> Loses { get { return m_Loses; } }

		public DuelPoints(Mobile mobile)
		{
			m_Mobile = mobile;

			m_Wins = new Dictionary<int, int>();
			m_Loses = new Dictionary<int, int>();

			m_FastestWins = new Dictionary<int, DuelInfo>();
			m_LongestWins = new Dictionary<int, DuelInfo>();

			m_FastestLoses = new Dictionary<int, DuelInfo>();
			m_LongestLoses = new Dictionary<int, DuelInfo>();
		}

		public DuelPoints(GenericReader reader)
		{
			int version = reader.ReadEncodedInt();

			switch (version)
			{
				case 0:
					{
						m_Mobile = reader.ReadMobile();
						m_FastestWins = DuelController.ReadPointsDictionary(reader);
						m_LongestWins = DuelController.ReadPointsDictionary(reader);
						m_FastestLoses = DuelController.ReadPointsDictionary(reader);
						m_LongestLoses = DuelController.ReadPointsDictionary(reader);
						m_Wins = DuelController.ReadIntDictionary(reader);
						m_Loses = DuelController.ReadIntDictionary(reader);

						break;
					}
			}
		}

		public void AddWin(int numPerTeam, DuelInfo duelInfo)
		{
			if (m_Wins.ContainsKey(numPerTeam))
				m_Wins[numPerTeam]++;
			else
				m_Wins.Add(numPerTeam, 1);

			if (m_FastestWins.ContainsKey(numPerTeam) && m_FastestWins[numPerTeam].DuelTime.Seconds > duelInfo.DuelTime.Seconds)
				m_FastestWins[numPerTeam] = duelInfo;
			else if (!m_FastestWins.ContainsKey(numPerTeam))
				m_FastestWins.Add(numPerTeam, duelInfo);

			if (m_LongestWins.ContainsKey(numPerTeam) && m_LongestWins[numPerTeam].DuelTime.Seconds < duelInfo.DuelTime.Seconds)
				m_LongestWins[numPerTeam] = duelInfo;
			else if (!m_LongestWins.ContainsKey(numPerTeam))
				m_LongestWins.Add(numPerTeam, duelInfo);
		}

		public void AddLoss(int numPerTeam, DuelInfo duelInfo)
		{
			if (m_Loses.ContainsKey(numPerTeam))
				m_Loses[numPerTeam]++;
			else
				m_Loses.Add(numPerTeam, 1);

			if (m_FastestLoses.ContainsKey(numPerTeam) && m_FastestLoses[numPerTeam].DuelTime.Seconds > duelInfo.DuelTime.Seconds)
				m_FastestLoses[numPerTeam] = duelInfo;
			else if (!m_FastestLoses.ContainsKey(numPerTeam))
				m_FastestLoses.Add(numPerTeam, duelInfo);

			if (m_LongestLoses.ContainsKey(numPerTeam) && m_LongestLoses[numPerTeam].DuelTime.Seconds < duelInfo.DuelTime.Seconds)
				m_LongestLoses[numPerTeam] = duelInfo;
			else if (!m_LongestLoses.ContainsKey(numPerTeam))
				m_LongestLoses.Add(numPerTeam, duelInfo);
		}

		internal void Serialize(GenericWriter writer)
		{
			writer.WriteEncodedInt(0); // version

			writer.Write((Server.Mobile)m_Mobile);

			DuelController.WritePointsDictionary(m_FastestWins, writer);
			DuelController.WritePointsDictionary(m_LongestWins, writer);
			DuelController.WritePointsDictionary(m_FastestLoses, writer);
			DuelController.WritePointsDictionary(m_LongestLoses, writer);
			DuelController.WriteIntDictionary(m_Wins, writer);
			DuelController.WriteIntDictionary(m_Loses, writer);
		}
	}
}
