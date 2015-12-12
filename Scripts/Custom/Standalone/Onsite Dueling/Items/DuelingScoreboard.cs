/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Items/DuelingScoreboard.cs#2 $

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
using Server.Engines.Dueling;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	[Flipable(0x2311, 0x2312)]
	public class DuelingScoreboard : Item
	{
		public override string DefaultName { get { return "Dueling Scoreboard"; } }
		public override double DefaultWeight { get { return 0; } }

		[Constructable]
		public DuelingScoreboard()
			: base(0x2311)
		{
			Hue = 33;
			Movable = false;
		}

        public override void GetProperties(ObjectPropertyList list)
        {
            list.Add(Name);
            list.Add("A Listing Of Top 10 Players");
        }

		public override void OnDoubleClick(Mobile from)
		{
			if (from != null && from is PlayerMobile)
			{
				from.CloseGump(typeof(DuelPlayerInfoGump));
				from.CloseGump(typeof(DuelScoreBoardGump));
				from.SendGump(new DuelScoreBoardGump());
			}
		}

		#region Serialization
		public DuelingScoreboard(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.WriteEncodedInt(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadEncodedInt();
		}
		#endregion
	}
}
