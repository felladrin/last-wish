/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Gumps/DuelScoreBoardGump.cs#5 $

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
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Server;
using Server.Gumps;

namespace Server.Engines.Dueling
{
	public class DuelScoreBoardGump : Gump
	{
		private const int LabelHue = 61;
		private List<DuelRankData> m_DuelRankData = new List<DuelRankData>();
		private int m_Contestants;

		public class DuelRankData : IComparer<DuelRankData>
		{
			private bool m_IsValid;
			private Mobile m_Mobile;
			private int m_ClassWins;
			private int m_ClassLoses;
			private int m_TotalWins;
			private int m_TotalLoses;
			private TimeSpan m_FastestDuel;
			private TimeSpan m_LongestDuel;

			public bool IsValid { get { return m_IsValid; } }
			public Mobile Mobile { get { return m_Mobile; } }
			public int ClassWins { get { return m_ClassWins; } }
			public int ClassLoses { get { return m_ClassLoses; } }
			public int TotalWins { get { return m_TotalWins; } }
			public int TotalLoses { get { return m_TotalLoses; } }
			public TimeSpan FastestDuel { get { return m_FastestDuel; } }
			public TimeSpan LongestDuel { get { return m_LongestDuel; } }

			public int ClassRatio
			{
				get
				{
					int total = ClassWins + ClassLoses;
					if (total > 0)
					{
						double ratio = ((double)ClassWins / (double)total) * 100;
						return (int)Math.Floor(ratio);
					}
					else
						return 0;
				}
			}

			public int Ratio
			{
				get
				{
					int total = TotalWins + TotalLoses;
					if (total > 0)
					{
						double ratio = (TotalWins / total) * 100;
						return (int)Math.Floor(ratio);
					}
					else
						return 0;
				}
			}

			public DuelRankData()
			{
			}

			public DuelRankData(int vs, Mobile m)
			{
				if (m != null)
				{
					m_Mobile = m;
					DuelPoints dp;
					if (DuelController.PointsTable.TryGetValue(m.Serial, out dp))
					{
						dp.Wins.TryGetValue(vs, out m_ClassWins);
						dp.Loses.TryGetValue(vs, out m_ClassLoses);
						if (m_ClassWins != 0 || m_ClassLoses != 0)
						{
							m_IsValid = true;

							m_TotalWins = 0;
							IDictionaryEnumerator myEnum = dp.Wins.GetEnumerator();
							while (myEnum.MoveNext())
							{
								m_TotalWins += (int)myEnum.Value;
							}

							m_TotalLoses = 0;
							myEnum = dp.Loses.GetEnumerator();
							while (myEnum.MoveNext())
							{
								m_TotalLoses += (int)myEnum.Value;
							}

							DuelInfo di;
							if (dp.LongestWins.TryGetValue(vs, out di))
								m_LongestDuel = di.DuelTime;
							else
								m_LongestDuel = TimeSpan.Zero;
							if (dp.LongestLoses.TryGetValue(vs, out di))
								if (di.DuelTime > m_LongestDuel)
									m_LongestDuel = di.DuelTime;

							if (dp.FastestWins.TryGetValue(vs, out di))
								m_FastestDuel = di.DuelTime;
							else
								m_FastestDuel = TimeSpan.Zero;
							if (dp.FastestLoses.TryGetValue(vs, out di))
								if (di.DuelTime < m_FastestDuel || m_FastestDuel == TimeSpan.Zero)
									m_FastestDuel = di.DuelTime;
						}
					}
				}
			}

			#region IComparer<DuelRankData> Members

			public int Compare(DuelRankData x, DuelRankData y)
			{
				return (y.ClassRatio - x.ClassRatio);
			}

			#endregion
		}

		public DuelScoreBoardGump()
			: this(2)
		{
		}

		public DuelScoreBoardGump(int contestants)
			: base(200, 200)
		{
			m_Contestants = contestants;

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage(0);
			AddBackground(36, 25, 732, 375, 3500);
			AddButton(733, 46, 3, 4, (int)Buttons.CloseBtn, GumpButtonType.Reply, 0);
			AddLabel(292, 39, LabelHue, @"Onsite Duel System Top 10 Scoreboard");
			AddLabel(292, 38, LabelHue, @"Onsite Duel System Top 10 Scoreboard");
			AddLabel(104, 82, LabelHue, @"Name");
			AddLabel(104, 81, LabelHue, @"Name");
			AddLabel(207, 82, LabelHue, @"Wins");
			AddLabel(207, 81, LabelHue, @"Wins");
			AddLabel(270, 82, LabelHue, @"Loses");
			AddLabel(270, 81, LabelHue, @"Loses");
            AddLabel(343, 82, LabelHue, @"Longest Duel");
            AddLabel(343, 81, LabelHue, @"Longest Duel");
            AddLabel(476, 82, LabelHue, @"Quickest Duel");
            AddLabel(476, 81, LabelHue, @"Quickest Duel");
			AddLabel(590, 82, LabelHue, @"Total Wins");
			AddLabel(590, 81, LabelHue, @"Total Wins");
			AddLabel(674, 82, LabelHue, @"Total Loses");
			AddLabel(674, 81, LabelHue, @"Total Loses");
			string duelType = string.Format("Duel Type: {0}vs{0}", m_Contestants / 2);
			AddLabel(371, 60, LabelHue, duelType);
			AddLabel(371, 59, LabelHue, duelType);

			AddButton(291, 368, 5603, 5607, (int)Buttons.prevBtn, GumpButtonType.Reply, 0);
			AddButton(494, 368, 5601, 5605, (int)Buttons.nextBtn, GumpButtonType.Reply, 0);
			AddLabel(314, 366, LabelHue, @"Previous");
			AddLabel(461, 366, LabelHue, @"Next");

			// fill in the data
			try
			{
				IDictionaryEnumerator myEnum = DuelController.PointsTable.GetEnumerator();

				while (myEnum.MoveNext())
				{
					DuelPoints dp = (DuelPoints)myEnum.Value;
					m_DuelRankData.Add(new DuelRankData(m_Contestants / 2, dp.Mobile));
				}

				List<DuelRankData> badEntries = new List<DuelRankData>();
				foreach (DuelRankData d in m_DuelRankData)
					if (!d.IsValid)
						badEntries.Add(d);

				foreach (DuelRankData d in badEntries)
					m_DuelRankData.Remove(d);

				badEntries.Clear();

				m_DuelRankData.Sort(new DuelRankData());

				if (m_DuelRankData.Count > 0)
				{
					for (int i = 0; i < 10 && i < m_DuelRankData.Count; i++)
						AddLine(i, m_DuelRankData[i]);
				}
				else
					AddLabel(319, 202, 53, "No data for this dueling class.");
			}
			catch (Exception e)
			{
				AddLabel(200, 202, 53, "An error occured creating the scoreboard, please try again in a moment.");
				Console.WriteLine("Caught an exception creating Dueling scoreboard!");
				Console.WriteLine(e.ToString());
			}
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			Buttons btn = (Buttons)info.ButtonID;

			switch (btn)
			{
				case Buttons.CloseBtn:
					break;
				case Buttons.nextBtn:
					m_Contestants += 2;
					if (m_Contestants > 10)
						m_Contestants = 2;
					sender.Mobile.SendGump(new DuelScoreBoardGump(m_Contestants));
					break;
				case Buttons.prevBtn:
					m_Contestants -= 2;
					if (m_Contestants < 2)
						m_Contestants = 10;
					sender.Mobile.SendGump(new DuelScoreBoardGump(m_Contestants));
					break;
			}
		}

		private string FormatTimeSpan(TimeSpan ts)
		{
			string s;
			if (ts.Minutes > 0 && ts.Seconds > 0)
				s = string.Format("{0} min{1} {2}sec{3}",
					ts.Minutes, ts.Minutes == 1 ? "" : "s",
					ts.Seconds, ts.Seconds == 1 ? "" : "s");
			else if (ts.Minutes > 0)
				s = string.Format("{0} min{1}",
					ts.Minutes, ts.Minutes == 1 ? "" : "s");
			else
				s = string.Format("{0} sec{1}",
					ts.Seconds, ts.Seconds == 1 ? "" : "s");
			return s;
		}

		private void AddLine(int offset, DuelRankData data)
		{
			AddHtml(49, 102 + (offset * 25), 142, 23, data.Mobile.Name, (bool)true, (bool)false); // name
			AddHtml(193, 102 + (offset * 25), 61, 23, data.ClassWins.ToString("N0"), (bool)true, (bool)false); // wins
			AddHtml(256, 102 + (offset * 25), 61, 23, data.ClassLoses.ToString("N0"), (bool)true, (bool)false); // loses
			AddHtml(319, 102 + (offset * 25), 130, 23, FormatTimeSpan(data.FastestDuel), (bool)true, (bool)false); // fastest
			AddHtml(450, 102 + (offset * 25), 130, 23, FormatTimeSpan(data.LongestDuel), (bool)true, (bool)false); // longest
			AddHtml(582, 102 + (offset * 25), 85, 23, data.TotalWins.ToString("N0"), (bool)true, (bool)false); // total wins
			AddHtml(668, 102 + (offset * 25), 85, 23, data.TotalLoses.ToString("N0"), (bool)true, (bool)false); // total loses
		}

		private enum Buttons
		{
			CloseBtn,
			prevBtn,
			nextBtn
		}
	}
}
