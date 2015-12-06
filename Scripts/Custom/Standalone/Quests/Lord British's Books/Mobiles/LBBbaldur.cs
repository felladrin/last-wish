using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBbaldur : BaseCreature
    {
        [Constructable]
        public LBBbaldur()
            : base(AIType.AI_Mage, FightMode.Weakest, 10, 10, 0.2, 0.4)
        {
            Name = "Baldur";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x415;
            RangeHome = 0;

            Robe backRobe = new Robe(0x001);
            backRobe.Movable = false;
            AddItem(backRobe);
            AddItem(new Shirt(0x025));
            AddItem(new Boots(0x967));
            AddItem(new LongPants(0x025));

            SetStr(502, 600);
            SetDex(102, 200);
            SetInt(601, 750);

            SetHits(1500);
            SetStam(103, 250);

            SetDamage(29, 35);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Fire, 25);

            SetResistance(ResistanceType.Physical, 20, 30);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 20, 30);
            SetResistance(ResistanceType.Energy, 10, 20);

            SetSkill(SkillName.EvalInt, 95.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 105.0);
            SetSkill(SkillName.Meditation, 95.1, 100.0);
            SetSkill(SkillName.MagicResist, 120.2, 140.0);
            SetSkill(SkillName.Tactics, 90.1, 105.0);
            SetSkill(SkillName.Wrestling, 90.1, 105.0);

            Fame = 24000;
            Karma = -24000;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 4);
            AddLoot(LootPack.FilthyRich);
        }

        public override bool ClickTitle { get { return false; } }
        public override bool ShowFameTitle { get { return false; } }
        public override bool AlwaysMurderer { get { return true; } }

        public LBBbaldur(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
