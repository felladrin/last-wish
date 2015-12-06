using System;
using Server.Items;
using Server.Gumps;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBsouvenir : Item
    {
        [Constructable]
        public LBBsouvenir()
            : base(0x14EE)
        {
            Name = "Lord British's Books Souvenir";
            LootType = LootType.Blessed;
            Weight = 1;
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.CloseGump(typeof(SouvenirGump));
            from.SendGump(new SouvenirGump());
        }

        public LBBsouvenir(Serial serial)
            : base(serial)
        {
        }

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

        private class SouvenirGump : Gump
        {
            public SouvenirGump()
                : base(0, 0)
            {
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;

                AddPage(0);
                AddBackground(251,  137, 281, 313, 9380);
                AddLabel(302, 140,  238, @"Lord British's Books Souvenir");
                AddLabel(280, 176, 1410, @"Thank you very much for fighting at");
                AddLabel(280, 200, 1410, @"the quest Lord British's Books.");
                AddLabel(280, 224, 1410, @"This event would be nothing without");
                AddLabel(280, 248, 1410, @"your presence.");
                AddLabel(280, 272, 1410, @"You had an extremely great");
                AddLabel(280, 296, 1410, @"performance, and for this reason, I");
                AddLabel(280, 344, 1410, @"adventures.");
                AddLabel(280, 320, 1410, @"would be glad to send you in future");
                AddLabel(423, 392, 1333, @"Lord British");
            }
        }
    }
}

