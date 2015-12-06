using Server.Engines.Harvest;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class FishingNet : Item
    {
        [Constructable]
        public FishingNet() : base(0x0DCA)
        {
            Weight = 1.0;
            Hue = 0x2E7;
            Name = "Fishing Net";
        }

        public override void OnDoubleClick(Mobile from)
        {
            Fishing.System.BeginHarvesting(from, this);
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            BaseHarvestTool.AddContextMenuEntries(from, this, list, Fishing.System);
        }

        public FishingNet(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}