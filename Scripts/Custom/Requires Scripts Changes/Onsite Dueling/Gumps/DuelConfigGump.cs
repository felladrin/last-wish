/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Gumps/DuelConfigGump.cs#2 $

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
    public class DuelConfigGump : Gump
	{
		private Duel m_Duel;
		private bool m_DeclinesDuels;

		public DuelConfigGump(Duel duel)
			: base(200, 00)
		{
			m_Duel = duel;
			m_DeclinesDuels = DuelController.DeclineDuelList.Contains(m_Duel.Creator);

			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;
			this.AddPage(0);
			this.AddBackground(36, 25, 402, 426, 3500);
			this.AddItem(172, 276, 7410);
			this.AddItem(127, 179, 7414);
			this.AddItem(261, 143, 7420);
			this.AddItem(284, 164, 7421);
			this.AddItem(194, 291, 7409);
			this.AddItem(238, 138, 7419);
			this.AddItem(240, 236, 7427);
			this.AddItem(194, 137, 7417);
			this.AddItem(171, 143, 7416);
			this.AddItem(149, 158, 7415);
			this.AddItem(104, 214, 7413);
			this.AddItem(127, 223, 7412);
			this.AddItem(306, 223, 7424);
			this.AddItem(150, 209, 7431);
			this.AddItem(172, 231, 7430);
			this.AddItem(195, 246, 7429);
			this.AddItem(217, 272, 7428);
			this.AddItem(240, 291, 7427);
			this.AddItem(262, 271, 7426);
			this.AddItem(284, 251, 7425);
			this.AddItem(216, 180, 7441);
			this.AddItem(193, 203, 7440);
			this.AddItem(217, 236, 7439);
			this.AddItem(328, 214, 7423);
			this.AddItem(150, 245, 7411);
			this.AddItem(284, 205, 7436);
			this.AddItem(267, 176, 7435);
			this.AddItem(239, 156, 7434);
			this.AddItem(193, 156, 7433);
			this.AddItem(172, 179, 7432);
			this.AddItem(245, 203, 7442);
			this.AddItem(215, 138, 7418);
			this.AddItem(306, 178, 7422);
			this.AddItem(262, 222, 7437);
			this.AddItem(236, 251, 7438);
			this.AddItem(212, 204, 7960);
			this.AddLabel(166, 40, 36, @"Onsite Duel System " + DuelController.Version);
			this.AddLabel(166, 39, 36, @"Onsite Duel System " + DuelController.Version);
			this.AddLabel(136, 62, 36, @"Please choose the size of the duel.");
			this.AddLabel(136, 61, 36, @"Please choose the size of the duel.");
			this.AddLabel(154, 116, 36, @"1vs1");
			this.AddLabel(154, 115, 36, @"1vs1");
			this.AddLabel(287, 116, 36, @"2vs2");
			this.AddLabel(287, 115, 36, @"2vs2");
			this.AddLabel(98, 227, 36, @"3vs3");
			this.AddLabel(98, 226, 36, @"3vs3");
			this.AddLabel(348, 227, 36, @"4vs4");
			this.AddLabel(348, 226, 36, @"4vs4");
			this.AddLabel(223, 307, 36, @"5vs5");
			this.AddLabel(223, 306, 36, @"5vs5");
			this.AddLabel(65, 424, 36, @"ver. " + DuelController.Version);
			this.AddLabel(65, 423, 36, @"ver. " + DuelController.Version);
			this.AddLabel(79, 392, 36, @"Allow Duel Invites");
			this.AddLabel(79, 391, 36, @"Allow Duel Invites");
			this.AddButton(401, 46, 3, 4, (int)Buttons.closeBtn, GumpButtonType.Reply, 0);
			this.AddButton(159, 137, 4034, 4034, (int)Buttons.oneBtn, GumpButtonType.Reply, 0);
			this.AddButton(294, 137, 4034, 4034, (int)Buttons.twoBtn, GumpButtonType.Reply, 0);
			this.AddButton(115, 246, 4034, 4034, (int)Buttons.threeBtn, GumpButtonType.Reply, 0);
			this.AddButton(344, 246, 4034, 4034, (int)Buttons.fourBtn, GumpButtonType.Reply, 0);
			this.AddButton(230, 327, 4034, 4034, (int)Buttons.fiveBtn, GumpButtonType.Reply, 0);

			this.AddButton(54, 387, m_DeclinesDuels ? 9792 : 9793, m_DeclinesDuels ? 9792 : 9793, (int)Buttons.toggleBtn, GumpButtonType.Reply, 0);
		}

		private enum Buttons
		{
			closeBtn,
			oneBtn,
			twoBtn,
			threeBtn,
			fourBtn,
			fiveBtn,
			toggleBtn,
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			Mobile m = sender.Mobile;
			Buttons button = (Buttons)info.ButtonID;

			if (m == null || m_Duel == null)
				return;

			if (m_DeclinesDuels && button != Buttons.toggleBtn && button != Buttons.closeBtn)
			{
				m.SendMessage("You can't start a duel if you have chosen to not accept duel invites.");
				m.SendMessage("Check the allow duel invites box before you start a duel.");
				m.SendGump(new DuelConfigGump(m_Duel));
				return;
			}

			switch (button)
			{
				case Buttons.closeBtn:
					{
						DuelController.DestroyDuel(m_Duel);
						m.SendMessage("The duel was not created.");
						break;
					}
				case Buttons.oneBtn:
					{
						m_Duel.SpotsRemaing = 1;
						m_Duel.Configure(1);
						m_Duel.Contestants.Add(m_Duel.Creator);

						m.SendMessage("Please select the player you wish to duel.");
						m.Target = new DuelTarget(m, m_Duel);

						break;
					}
				case Buttons.twoBtn:
					{
						m_Duel.SpotsRemaing = 3;
						m_Duel.Configure(2);
						m_Duel.Contestants.Add(m_Duel.Creator);

						m.SendMessage("Please select a player to add to the duel.");
						m.SendMessage("Do not be concerned with what side they will be on at this point.");
						m.Target = new DuelTarget(m, m_Duel);
						break;
					}
				case Buttons.threeBtn:
					{
						m_Duel.SpotsRemaing = 5;
						m_Duel.Configure(3);
						m_Duel.Contestants.Add(m_Duel.Creator);

						m.SendMessage("Please select a player to add to the duel.");
						m.SendMessage("Do not be concerned with what side they will be on at this point.");
						m.Target = new DuelTarget(m, m_Duel);
						break;
					}
				case Buttons.fourBtn:
					{
						m_Duel.SpotsRemaing = 7;
						m_Duel.Configure(4);
						m_Duel.Contestants.Add(m_Duel.Creator);

						m.SendMessage("Please select a player to add to the duel.");
						m.SendMessage("Do not be concerned with what side they will be on at this point.");
						m.Target = new DuelTarget(m, m_Duel);
						break;
					}
				case Buttons.fiveBtn:
					{
						m_Duel.SpotsRemaing = 9;
						m_Duel.Configure(5);
						m_Duel.Contestants.Add(m_Duel.Creator);

						m.SendMessage("Please select a player to add to the duel.");
						m.SendMessage("Do not be concerned with what side they will be on at this point.");
						m.Target = new DuelTarget(m, m_Duel);
						break;
					}
				case Buttons.toggleBtn:
					{
						if (m_DeclinesDuels)
							DuelController.DeclineDuelList.Remove(m_Duel.Creator);
						else
							DuelController.DeclineDuelList.Add(m_Duel.Creator);

						m.CloseGump(typeof(DuelConfigGump));
						m.SendGump(new DuelConfigGump(m_Duel));

						break;
					}
			}
		}

	}
}
