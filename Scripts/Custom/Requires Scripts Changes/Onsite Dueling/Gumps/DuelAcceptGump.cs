/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Gumps/DuelAcceptGump.cs#2 $

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

using Server.Gumps;

namespace Server.Engines.Dueling
{
    public class DuelAcceptGump : Gump
	{
		private Mobile m_Mobile;
		private Duel m_Duel;
		private TimeoutTimer m_Timer;

		public DuelAcceptGump(Mobile m, Duel duel)
			: base(200, 200)
		{
			m_Mobile = m;
			m_Duel = duel;

			m_Timer = new TimeoutTimer(duel, m);
			m_Timer.Start();

			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;
			this.AddPage(0);
			this.AddBackground(40, 25, 324, 167, 3500);
			this.AddLabel(130, 40, 36, @"Onsite Duel System " + DuelController.Version);
			this.AddLabel(130, 39, 36, @"Onsite Duel System " + DuelController.Version);
			this.AddLabel(60, 73, 36, m_Duel.Creator.Name + " has invited you to their duel!");
			this.AddLabel(60, 72, 36, m_Duel.Creator.Name + " has invited you to their duel!");
			this.AddLabel(60, 96, 36, @"Do you wish to join?");
			this.AddLabel(60, 95, 36, @"Do you wish to join?");
			this.AddLabel(162, 146, 36, @"Yes");
			this.AddLabel(162, 145, 36, @"Yes");
			this.AddLabel(222, 146, 36, @"No");
			this.AddLabel(222, 145, 36, @"No");
			this.AddButton(327, 44, 3, 4, (int)Buttons.closeBtn, GumpButtonType.Reply, 0);
			this.AddButton(144, 149, 4034, 4034, (int)Buttons.yesBtn, GumpButtonType.Reply, 0);
			this.AddButton(204, 149, 4034, 4034, (int)Buttons.noBtn, GumpButtonType.Reply, 0);
		}

		private enum Buttons
		{
			closeBtn,
			yesBtn,
			noBtn,
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			m_Timer.Stop();

			Mobile m = sender.Mobile;

			if (m == null || m_Duel == null)
				return;

			switch ((Buttons)info.ButtonID)
			{
				case Buttons.closeBtn:
				case Buttons.noBtn:
					{
						m_Duel.SpotsRemaing++;
						m_Duel.Broadcast(m.Name + " declined to join the duel.");
						CheckTarget();
						break;
					}
				case Buttons.yesBtn:
					{
						m_Duel.Contestants.Add(m);
						DuelController.DuelTable.Add(m.Serial, m_Duel);
						m_Duel.Broadcast(m.Name + " has joined the duel.");
						m_Duel.CheckBegin();
						break;
					}
			}
		}

		private void CheckTarget()
		{
			if (!(m_Duel.Creator.Target is DuelTarget))
			{
				m_Duel.Creator.SendMessage("Please select another player to join the duel.");
				m_Duel.Creator.Target = new DuelTarget(m_Duel.Creator, m_Duel);
			}
		}
	}
}
