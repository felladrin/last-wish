/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Gumps/DuelPlayerInfoGump.cs#4 $

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
using System.Text;
using Server.Gumps;
using Server.Commands;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Engines.Dueling
{
    public class DuelPlayerInfoGump : Gump
	{
		public static void Initialize()
		{
			CommandSystem.Register("DuelStats", AccessLevel.Player, DuelStats_OnCommand);
		}

		[Usage("DuelStats")]
		[Description("Shows dueling statistics for a targetted player, or yourself.")]
		public static void DuelStats_OnCommand(CommandEventArgs e)
		{
			if (e.Mobile != null)
			{
				e.Mobile.SendMessage(53, "Target a player to see dueling statistics for, or hit ESC to see your own statistics.");
				e.Mobile.Target = new DuelStatsTarget();
			}
		}

		private class DuelStatsTarget : Target
		{
			public DuelStatsTarget()
				: base(12, false, TargetFlags.None)
			{
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (from != null)
				{
					if (targeted != null && targeted is PlayerMobile)
					{
						PlayerMobile pm = (PlayerMobile)targeted;
						DuelPoints dp = null;

						if (DuelController.PointsTable.TryGetValue(pm.Serial, out dp))
						{
							from.SendGump(new DuelPlayerInfoGump(dp));
						}
						else
						{
							if (from == pm)
								from.SendMessage(53, "You have no dueling record.");
							else
								from.SendMessage(53, "That player has no dueling record.");
						}
					}
					else
					{
						from.SendMessage(32, "That's not a player!");
					}
				}
			}

			protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
			{
				if (from != null && cancelType == TargetCancelType.Canceled && from is PlayerMobile)
				{
					PlayerMobile pm = (PlayerMobile)from;
					DuelPoints dp = null;

					if (DuelController.PointsTable.TryGetValue(from.Serial, out dp))
					{
						from.SendGump(new DuelPlayerInfoGump(dp));
					}
					else
						from.SendMessage(53, "You have no dueling record.");
				}
			}
		}

		public DuelPlayerInfoGump( DuelPoints duelPoints )
			: base( 200, 00 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(36, 25, 402, 480, 3500);
            this.AddButton(401, 46, 3, 4, (int)Buttons.closeBtn, GumpButtonType.Reply, 0);
            this.AddLabel( 105, 43, 36, @"Onsite Duel System Player Duel Information" );
            this.AddLabel( 105, 42, 36, @"Onsite Duel System Player Duel Information" );
            this.AddLabel( 168, 91, 36, @"Wins:" );
            this.AddLabel( 168, 90, 36, @"Wins:" );
            this.AddLabel( 122, 69, 36, @"Player Name: " + duelPoints.Mobile.Name );
            this.AddLabel( 122, 68, 36, @"Player Name: " + duelPoints.Mobile.Name );
            this.AddLabel( 163, 189, 36, @"Loses:" );
            this.AddLabel( 163, 188, 36, @"Loses:" );
            this.AddLabel( 81, 289, 36, @"Quickest Duel Time:" );
            this.AddLabel( 81, 288, 36, @"Quickest Duel Time:" );
            this.AddLabel( 87, 383, 36, @"Longest Duel Time:" );
            this.AddLabel( 87, 382, 36, @"Longest Duel Time:" );
            this.AddHtml(206, 91, 215, 93, GetWins( duelPoints ), (bool)true, (bool)true);
            this.AddHtml(206, 189, 215, 93, GetLoses( duelPoints ), (bool)true, (bool)true);
            this.AddHtml(206, 287, 215, 93, GetQuickestTimes(duelPoints), (bool)true, (bool)true);
            this.AddHtml(206, 385, 215, 93, GetLongestTimes(duelPoints), (bool)true, (bool)true);
        }

        public static string GetLongestTimes(DuelPoints duelPoints)
        {
            if (duelPoints == null || (duelPoints.LongestLoses.Count == 0 && duelPoints.LongestWins.Count == 0))
                return "No records";

            StringBuilder sb = new StringBuilder();

            if (duelPoints.LongestWins.Count > 0)
            {
                sb.Append("Longest Wins" + br);
                IDictionaryEnumerator myEnum = duelPoints.LongestWins.GetEnumerator();

                while (myEnum.MoveNext())
                {
                    int players = (int)myEnum.Key;
                    DuelInfo dInfo = (DuelInfo)myEnum.Value;

					if (dInfo.DuelTime.Minutes == 0)
						sb.Append(String.Format("{0}vs{0} - {1} seconds{2}", players, dInfo.DuelTime.Seconds, br));
					else
						sb.Append(String.Format("{0}vs{0} - {1} minutes {2} seconds{3}", players, dInfo.DuelTime.Minutes, dInfo.DuelTime.Seconds, br));
                }
            }

            if (duelPoints.LongestLoses.Count > 0)
            {
                sb.Append(br);
                sb.Append("Longest Loses" + br);
                IDictionaryEnumerator myEnum = duelPoints.LongestLoses.GetEnumerator();

                while (myEnum.MoveNext())
                {
                    int players = (int)myEnum.Key;
                    DuelInfo dInfo = (DuelInfo)myEnum.Value;


					if (dInfo.DuelTime.Minutes == 0)
						sb.Append(String.Format("{0}vs{0} - {1} seconds{2}", players, dInfo.DuelTime.Seconds, br));
					else
						sb.Append(String.Format("{0}vs{0} - {1} minutes {2} seconds{3}", players, dInfo.DuelTime.Minutes, dInfo.DuelTime.Seconds, br));
                }
            }

            return sb.ToString();
        }

        public static string br { get { return "<br>"; } }

        public static string GetQuickestTimes(DuelPoints duelPoints)
        {
            if (duelPoints == null || (duelPoints.FastestWins.Count == 0 && duelPoints.FastestLoses.Count == 0))
                return "No records";

            StringBuilder sb = new StringBuilder();

            if (duelPoints.FastestWins.Count > 0)
            {
                sb.Append("Fastest Wins" + br);
                IDictionaryEnumerator myEnum = duelPoints.FastestWins.GetEnumerator();

                while (myEnum.MoveNext())
                {
                    int players = (int)myEnum.Key;
                    DuelInfo dInfo = (DuelInfo)myEnum.Value;

					if (dInfo.DuelTime.Minutes == 0)
						sb.Append(String.Format("{0}vs{0} - {1} seconds{2}", players, dInfo.DuelTime.Seconds, br));
					else
						sb.Append(String.Format("{0}vs{0} - {1} minutes {2} seconds{3}", players, dInfo.DuelTime.Minutes, dInfo.DuelTime.Seconds, br));
                }
            }

            if (duelPoints.FastestLoses.Count > 0)
            {
                sb.Append(br);
                sb.Append("Fastest Loses" + br);
                IDictionaryEnumerator myEnum = duelPoints.FastestLoses.GetEnumerator();

                while (myEnum.MoveNext())
                {
                    int players = (int)myEnum.Key;
                    DuelInfo dInfo = (DuelInfo)myEnum.Value;

					if (dInfo.DuelTime.Minutes == 0)
						sb.Append(String.Format("{0}vs{0} - {1} seconds{2}", players, dInfo.DuelTime.Seconds, br));
					else
						sb.Append(String.Format("{0}vs{0} - {1} minutes {2} seconds{3}", players, dInfo.DuelTime.Minutes, dInfo.DuelTime.Seconds, br));
                }
            }

            return sb.ToString();
        }

        public static string GetLoses(DuelPoints duelPoints)
        {
            if (duelPoints == null || duelPoints.Loses.Count == 0 )
                return "No records";

            StringBuilder sb = new StringBuilder();

            IDictionaryEnumerator myEnum = duelPoints.Loses.GetEnumerator();

            while (myEnum.MoveNext())
            {
                int players = (int)myEnum.Key;
                int loses = (int)myEnum.Value;

                sb.Append(String.Format("{0}vs{0} - {1} loses{2}", players, loses, br));
            }

            return sb.ToString();
        }

        public static string GetWins(DuelPoints duelPoints)
        {
            if (duelPoints == null || duelPoints.Wins.Count == 0)
                return "No records";

            StringBuilder sb = new StringBuilder();

            IDictionaryEnumerator myEnum = duelPoints.Wins.GetEnumerator();

            while (myEnum.MoveNext())
            {
                int players = (int)myEnum.Key;
                int loses = (int)myEnum.Value;

                sb.Append(String.Format("{0}vs{0} - {1} wins{2}", players, loses, br));
            }

            return sb.ToString();
        }
		
		private enum Buttons
		{
			closeBtn = 0,
		}

	}
}