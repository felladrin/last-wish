using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
    public class LBBguard : BaseCreature
    {
        public LBBguard()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Fame = 6000;
            Karma = 3000;

            RangeHome = 0;

            SpeechHue = Utility.RandomDyedHue();

            Female = Utility.RandomBool();

            Hue = Utility.RandomSkinHue();

            Utility.AssignRandomHair(this);

            if (Female)
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");

                if (Utility.RandomBool())
                {
                    Utility.AssignRandomFacialHair(this, HairHue);
                }
            }

            Title = "- Royal Guard";

            SetSkill(SkillName.Fencing, 75.0, 85.0);
            SetSkill(SkillName.Macing, 75.0, 85.0);
            SetSkill(SkillName.Swords, 75.0, 85.0);
            SetSkill(SkillName.Chivalry, 100.0);

            SetDamageType(ResistanceType.Physical, 100);
            SetResistance(ResistanceType.Physical, 55, 70);
            SetResistance(ResistanceType.Fire, 55, 70);
            SetResistance(ResistanceType.Cold, 55, 70);
            SetResistance(ResistanceType.Poison, 55, 70);
            SetResistance(ResistanceType.Energy, 30, 70);

            AddItem(new PlateArms());
            AddItem(new PlateChest());
            AddItem(new PlateGloves());
            AddItem(new StuddedGorget());
            AddItem(new PlateLegs());
            AddItem(new Broadsword());
            AddItem(new MetalKiteShield());
            AddItem(new Boots());

            PackGold(100, 200);
        }

        public override double WeaponAbilityChance { get { return 0.7; } }

        public override bool IsEnemy(Mobile m)
        {
            if ( m is PlayerMobile && m.CanSwim)
            {
                return true;
            }

            return false;
        }

        public override bool BardImmune { get { return true; } }

        public LBBguard(Serial serial)
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