/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Components/Duel.cs#3 $

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
using Server.Items;
using Server.Mobiles;
using Server.Spells.Necromancy;

namespace Server.Engines.Dueling
{
    public class Duel
	{
		private bool m_Started;
		public bool Started { get { return m_Started; } set { m_Started = value; } }

		private int m_SpotsRemaining = 0;
		public int SpotsRemaing { get { return m_SpotsRemaining; } set { m_SpotsRemaining = value; } }

		public static int MaxDistance { get { return DuelController.Instance.MaxDistance; } }

		private Mobile m_Creator;
		public Mobile Creator { get { return m_Creator; } set { m_Creator = value; } }

		private List<Mobile> m_Contestants;
		private List<Mobile> m_Attackers;
		private List<Mobile> m_Defenders;
		public List<Mobile> Attackers { get { return m_Attackers; } }
		public List<Mobile> Defenders { get { return m_Defenders; } }
		public List<Mobile> Contestants { get { return m_Contestants; } }

		private bool m_Statemate;

		private DuelTimer m_DuelTimer;
		public DuelTimer DuelTimer { get { return m_DuelTimer; } }

		public Duel()
		{
		}

		public Duel(Mobile creator)
		{
			m_Creator = creator;
		}

		public Duel(Mobile creator, int playerCountPerTeam)
		{
			Configure(playerCountPerTeam);
			m_Contestants.Add(creator);
			m_Creator = creator;
		}

		public void Configure(int playerCountPerTeam)
		{
			m_Attackers = new List<Mobile>(playerCountPerTeam);
			m_Defenders = new List<Mobile>(playerCountPerTeam);
			m_Contestants = new List<Mobile>(playerCountPerTeam * 2);
		}

		public void HandleDeath(Mobile m)
		{
			InternalHandleDeath(m);
		}

		private void InternalHandleDeath(Mobile m)
		{
			if (CheckIfComplete())
			{
				EndDuel();
			}
		}

		private bool CheckIfComplete()
		{
			return ((DeathCheck(m_Attackers) == m_Attackers.Capacity) ||
				(DeathCheck(m_Defenders) == m_Defenders.Capacity));
		}

		private int DeathCheck(List<Mobile> mobiles)
		{
			int deadCount = 0;

			for (int i = 0; i < mobiles.Count; i++)
				if (!mobiles[i].Alive)
					deadCount++;

			return deadCount;
		}

		private void AttackersWin()
		{
			if (!m_Statemate)
			{
				List<Mobile> winners = m_Attackers;
				List<Mobile> losers = m_Defenders;
				string winnersStr = String.Empty;

				for (int i = 0; i < winners.Count; i++)
				{
					HandlePoints(winners[i], true);
					HandlePoints(losers[i], false);
					if (i < (winners.Count - 1))
						winnersStr += winners[i].Name + ", ";
					else if (winners.Count > 1)
						winnersStr += " and " + winners[i].Name;
					else
						winnersStr = winners[i].Name;
				}
				winnersStr += (winners.Count == 1 ? " has " : " have ") + "won the duel.";

				Broadcast(78, winnersStr);
			}
		}

		private void CompleteDuel()
		{
			List<Mobile> contestants = new List<Mobile>();

			contestants.AddRange(m_Attackers);
			contestants.AddRange(m_Defenders);

			for (int i = 0; i < contestants.Count; i++)
				FixMobile(contestants[i]);

			DuelController.DestroyDuel(this);
		}

		private void HandlePoints(Mobile m, bool winner)
		{
			if (winner)
			{
				DuelController.AddWin(this, m);
			}
			else
			{
				DuelController.AddLoss(this, m);
			}
		}

		private void DefendersWin()
		{
			if (!m_Statemate)
			{
				List<Mobile> winners = m_Defenders;
				List<Mobile> losers = m_Attackers;
				string winnersStr = string.Empty;

				for (int i = 0; i < winners.Count; i++)
				{
					HandlePoints(winners[i], true);
					HandlePoints(losers[i], false);
					if (i < (winners.Count - 1))
						winnersStr += winners[i].Name + ", ";
					else if (winners.Count > 1)
						winnersStr += " and " + winners[i].Name;
					else
						winnersStr = winners[i].Name;
				}
				winnersStr += (winners.Count == 1 ? " has " : " have ") + "won the duel.";

				Broadcast(78, winnersStr);
			}
		}

		private void FixMobile(Mobile m)
		{
			if (!m.Alive)
				m.Resurrect();

			HandleCorpse(m);

			m.Aggressed.Clear();
			m.Aggressors.Clear();
			m.Hits = m.HitsMax;
			m.Stam = m.StamMax;
			m.Mana = m.ManaMax;
			m.DamageEntries.Clear();
			m.Combatant = null;
			m.InvalidateProperties();

            m.Criminal = false;

			StatMod mod;

			mod = m.GetStatMod("[Magic] Str Offset");
			if (mod != null && mod.Offset < 0)
				m.RemoveStatMod("[Magic] Str Offset");

			mod = m.GetStatMod("[Magic] Dex Offset");
			if (mod != null && mod.Offset < 0)
				m.RemoveStatMod("[Magic] Dex Offset");

			mod = m.GetStatMod("[Magic] Int Offset");
			if (mod != null && mod.Offset < 0)
				m.RemoveStatMod("[Magic] Int Offset");

			m.Paralyzed = false;
			m.CurePoison(m);

			// EvilOmenSpell.CheckEffect(m);
			StrangleSpell.RemoveCurse(m);
			CorpseSkinSpell.RemoveCurse(m);

			#region Buff Icons
			if (m is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)m;
				pm.RemoveBuff(BuffIcon.Clumsy);
				pm.RemoveBuff(BuffIcon.CorpseSkin);
				pm.RemoveBuff(BuffIcon.EvilOmen);
				pm.RemoveBuff(BuffIcon.Curse);
				pm.RemoveBuff(BuffIcon.FeebleMind);
				pm.RemoveBuff(BuffIcon.MassCurse);
				pm.RemoveBuff(BuffIcon.Paralyze);
				pm.RemoveBuff(BuffIcon.Poison);
				pm.RemoveBuff(BuffIcon.Strangle);
				pm.RemoveBuff(BuffIcon.Weaken);
			}
			#endregion

			m.SendMessage("The duel has ended.");
		}

		public void Begin()
		{
			InternalBegin();

			m_DuelTimer = new DuelTimer(this, DuelController.Instance.DuelLengthInSeconds);
			m_DuelTimer.Start();
		}

		private void InternalBegin()
		{
			DuelStartTimer timer;
			if (DuelController.DuelStartTimeoutTable.TryGetValue(Creator.Serial, out timer))
			{
				timer.Stop();
				DuelController.DuelStartTimeoutTable.Remove(Creator.Serial);
			}

			for (int i = 0; i < m_Attackers.Count; i++)
			{
				m_Attackers[i].Delta(MobileDelta.Noto);
				m_Defenders[i].Delta(MobileDelta.Noto);
				m_Attackers[i].InvalidateProperties();
				m_Defenders[i].InvalidateProperties();
			}
		}

		public void Stalemate()
		{
			m_Statemate = true;

			CompleteDuel();
		}

		public bool CheckDistance()
		{
			bool toFar = false;

			for (int i = 0; i < m_Attackers.Count; i++)
			{
				for (int j = 0; j < m_Defenders.Count; j++)
					if (GetDistance(m_Attackers[i], m_Defenders[i]) > MaxDistance)
					{
						toFar = true;
						break;
					}

				if (toFar)
					break;
			}

			return toFar;
		}

		public void HandleCorpse(Mobile from)
		{
			if (from.Corpse != null)
			{
				Corpse c = (Corpse)from.Corpse;
				Container b = from.Backpack;
				List<Item> toAdd = new List<Item>();


				foreach (Item i in c.Items)
				{
					if (i.Map != Map.Internal)
						toAdd.Add(i);
				}

				foreach (Item i in toAdd)
				{
					b.AddItem(i);
				}

				toAdd = null;

				c.Delete();

				from.SendMessage(1161, "The contents of your corpse have been safely placed into your backpack.");
			}
		}

		private int GetDistance(Mobile to, Mobile from)
		{
			int minX = Math.Min(to.Location.X, from.Location.X);
			int minY = Math.Min(to.Location.Y, from.Location.Y);
			int maxX = Math.Max(to.Location.X, from.Location.X);
			int maxY = Math.Max(to.Location.Y, from.Location.Y);

			return Math.Max(maxX - minX, maxY - minY);
		}

		public void EndDuel()
		{
			m_Started = false;
			m_DuelTimer.Stop();

			if (DeathCheck(m_Defenders) < DeathCheck(m_Attackers))
				DefendersWin();
			else if (DeathCheck(m_Attackers) < DeathCheck(m_Defenders))
				AttackersWin();
			else
			{
				int attackersHealth = 0;
				int defendersHealth = 0;

				for (int i = 0; i < m_Attackers.Count; i++)
					attackersHealth += m_Attackers[i].Hits;

				for (int i = 0; i < m_Defenders.Count; i++)
					defendersHealth += m_Defenders[i].Hits;

				if (attackersHealth > defendersHealth)
					AttackersWin();
				else if (defendersHealth > attackersHealth)
					DefendersWin();
				else
				{
					m_Statemate = true;

					Broadcast("It's a draw!!!");
				}
			}

			CompleteDuel();
		}

		public void CheckBegin()
		{
			if (m_Contestants.Count == m_Contestants.Capacity)
			{
				if (m_Contestants.Capacity > 2)
				{
					Broadcast("Please wait while the duel creator sets the teams.");
					m_Creator.SendGump(new DuelTeamSelectionGump(this));
				}
				else
				{
					m_Attackers.Add(m_Contestants[0]);
					m_Defenders.Add(m_Contestants[1]);
				}
			}

			if (m_Defenders.Count == m_Defenders.Capacity && m_Attackers.Count == m_Attackers.Capacity)
			{
				Broadcast("All contestants are now frozen until duel begins!");
				foreach (Mobile m in m_Contestants)
					m.Frozen = true;
				Begin();
			}
		}

		public void Broadcast(string msg)
		{
			DuelController.Broadcast(msg, Contestants);
		}

		public void Broadcast(int hue, string msg)
		{
			DuelController.Broadcast(hue, msg, Contestants);
		}
	}
}
