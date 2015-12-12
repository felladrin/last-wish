/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Targets/DuelTarget.cs#2 $

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


using Server.Mobiles;
using Server.Targeting;

namespace Server.Engines.Dueling
{
    public class DuelTarget : Target
	{
		private Mobile m_Mobile;
		private Duel m_Duel;

		public DuelTarget(Mobile mobile, Duel duel)
			: base(15, false, TargetFlags.None)
		{
			m_Mobile = mobile;
			m_Duel = duel;
		}

		protected override void OnTargetOutOfLOS(Mobile from, object targeted)
		{
			from.SendMessage("You cannot see that.");
			from.SendMessage("Select a new target.");
			from.Target = new DuelTarget(m_Mobile, m_Duel);
			return;
		}

		protected override void OnTargetNotAccessible(Mobile from, object targeted)
		{
			from.SendMessage("You cannot see that.");
			from.SendMessage("Select a new target.");
			from.Target = new DuelTarget(m_Mobile, m_Duel);
			return;
		}

		protected override void OnTargetUntargetable(Mobile from, object targeted)
		{
			from.SendMessage("You cannot see that.");
			from.SendMessage("Select a new target.");
			from.Target = new DuelTarget(m_Mobile, m_Duel);
			return;
		}

		protected override void OnTargetDeleted(Mobile from, object targeted)
		{
			from.SendMessage("You cannot see that.");
			from.SendMessage("Select a new target.");
			from.Target = new DuelTarget(m_Mobile, m_Duel);
			return;
		}

		protected override void OnCantSeeTarget(Mobile from, object targeted)
		{
			from.SendMessage("You cannot see that.");
			from.SendMessage("Select a new target.");
			from.Target = new DuelTarget(m_Mobile, m_Duel);
			return;
		}

		protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
		{
			base.OnTargetCancel(from, cancelType);

			m_Duel.Broadcast("The duel was canceled.");
			DuelController.DestroyDuel(m_Duel);

			from.SendMessage("You have cancelled the duel request.");
		}

		protected override void OnTarget(Mobile from, object target)
		{
			if (target is PlayerMobile)
			{
				Mobile m = (Mobile)target;

				if (m.NetState == null)
				{
					from.SendMessage("That player is not online.");
					from.SendMessage("Select a new target.");
					from.Target = new DuelTarget(m_Mobile, m_Duel);
					return;
				}
				else if (DuelController.DeclineDuelList.Contains(m))
				{
					from.SendMessage("This person has elected to decline all duels.");
					from.SendMessage("Select a new target.");
					from.Target = new DuelTarget(m_Mobile, m_Duel);
					return;
				}
				else if (m == from)
				{
					from.SendMessage("You slap yourself across the face and declare yourself the winner.");
					from.SendMessage("Select a new target.");
					from.Target = new DuelTarget(m_Mobile, m_Duel);
					return;
				}
				if (m.Criminal)
				{
					m.SendMessage("You may not start a duel with someone who is flagged criminal.");
					from.SendMessage("Select a new target.");
					from.Target = new DuelTarget(m_Mobile, m_Duel);
					return;
				}
				else if (Spells.SpellHelper.CheckCombat(m))
				{
					from.SendMessage("That person is currently in combat. You must wait to duel them.");
					from.SendMessage("Select a new target.");
					from.Target = new DuelTarget(m_Mobile, m_Duel);
					return;
				}
				else if (DuelController.IsInDuel(m))
				{
					from.SendMessage("That person is currently in a duel. You must wait to duel them.");
					from.SendMessage("Select a new target.");
					from.Target = new DuelTarget(m_Mobile, m_Duel);
					return;
				}
				else
				{
					if (m_Duel != null)
					{
						m_Duel.SpotsRemaing--;
						m.CloseGump(typeof(DuelAcceptGump));
						m.SendGump(new DuelAcceptGump(m, m_Duel));
						from.SendMessage("{0} has been invited to join the duel.", m.Name);

						if (m_Duel.SpotsRemaing != 0)
						{
							m_Duel.Creator.SendMessage("Select another player to join the duel.");
							m_Duel.Creator.Target = new DuelTarget(m_Duel.Creator, m_Duel);
						}
					}
				}
			}
			else
			{
				from.SendMessage("That is not a player.");
				from.SendMessage("Select a new target.");
				from.Target = new DuelTarget(m_Mobile, m_Duel);
				return;
			}
		}
	}
}
