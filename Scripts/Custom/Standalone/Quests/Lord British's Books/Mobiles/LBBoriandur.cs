using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBoriandur : BaseCreature
    {
        [Constructable]
        public LBBoriandur()
            : base(AIType.AI_Mage, FightMode.Weakest, 10, 10, 0.2, 0.4)
        {
            Name = "Oriandur";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x41F;
            HairItemID = 0x203C; // Long
            HairHue = 0x47D;
            FacialHairItemID = 0x204B; // MediumShortBeard
            FacialHairHue = 0x47D;
            RangeHome = 0;

            Robe backRobe = new Robe(0x001);
            backRobe.Movable = false;
            AddItem(backRobe);
            AddItem(new Shirt(0x025));
            AddItem(new Boots(0x967));
            AddItem(new LongPants(0x025));

            SetStr(900, 1000);
            SetDex(125, 135);
            SetInt(1000, 1200);

            Fame = 32500;
            Karma = -32500;

            VirtualArmor = 60;

            SetHits(3000);
            SetStam(103, 250);

            SetDamage(35, 50);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 60, 80);
            SetResistance(ResistanceType.Cold, 60, 80);
            SetResistance(ResistanceType.Poison, 60, 80);
            SetResistance(ResistanceType.Energy, 60, 80);

            SetSkill(SkillName.Wrestling, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 90.2, 110.0);
            SetSkill(SkillName.MagicResist, 120.2, 160.0);
            SetSkill(SkillName.Magery, 120.0);
            SetSkill(SkillName.EvalInt, 120.0);
            SetSkill(SkillName.Meditation, 120.0);

            PackItem(new LBBbookOfChaos1());
            PackItem(new LBBbookOfChaos2());
            PackItem(new LBBbookOfChaos3());
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.SuperBoss, 2);
            AddLoot(LootPack.Meager);
        }

        public override bool ClickTitle { get { return false; } }
        public override bool ShowFameTitle { get { return false; } }
        public override bool AlwaysMurderer { get { return true; } }

        public LBBoriandur(Serial serial)
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
