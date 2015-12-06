/*_____________________________________   __________________________________
./|       +         .         :      * \./                                  |\.
|||.        *     :        +--------+   :  ___|=======================|___  |||
||| : *  .    .  /\  +     |Hegemony| . :  \  |       Written By      |  /  |||
|||    . .      /&&\   .   +--------+   :   > |       Felladrin       | <   |||
|||  +      *  /&&&&\  . .    __  __|   :  /__|=======================|__\  |||
|||      .    /&&&&&&\   /\  [::][::]   :                                   |||
|||     *    /&&&&&&&&\ /&&\ \-=-=-=/   :                                   |||
|||  .      /&&&&&&&&&&\&&&&\ |::::|    :                                   |||
|||      +  |-=-=-=-=-=|    |_| __ |__  :           "Peacemaker"            |||
|||         |::[]::[]::|   [::][::][::] :                                   |||
|||  __  __ |__::::::::|    \-=-=-=-=/  :                                   |||
||| [::][::][::]:::::::|  ___|:::[]:|   :                                   |||
||| |-=-=-=-=-=|:::::::| ////|::::::|   :                                   |||
||| |:[]:[]:[]:|=============|:[]:::|   :                                   |||
||| |::::::::::|=|=|=|=|=|=|=|::::::|   :                                   |||
||| |::+----+::|:::::::::::::|::::::|   :        Created: 08/07/2007        |||
||| |::|\XX/|::|:::::::::::::|::::::|   :                                   |||
||| |::|XXXX|::|:::::::::::::|::::::|   :        Updated: 04/08/2013        |||
||| |::|/XX\|::|____________/._.._.._\  :                                   |||
|||____________________________________ : __________________________________|||
||/====================================\:/==================================\||
''-----------------------------------'___'---------------------------------''
*/

using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("the corpse of a peacemaker")]
    public class BasePeacemaker : BaseCreature
    {
        public BasePeacemaker(AIType aiType, int rangeFight)
            : base(aiType, FightMode.Closest, 10, rangeFight, 0.2, 0.4)
        {
            Title = "the peacemaker";

            Fame = 6000;
            Karma = 3000;

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

            SetResistance(ResistanceType.Physical, 55, 70);
            SetResistance(ResistanceType.Fire, 55, 70);
            SetResistance(ResistanceType.Cold, 55, 70);
            SetResistance(ResistanceType.Poison, 55, 70);
            SetResistance(ResistanceType.Energy, 55, 70);

            AddItem(new StuddedChest());
            AddItem(new StuddedArms());
            AddItem(new StuddedGloves());
            AddItem(new StuddedGorget());
            AddItem(new StuddedLegs());
            AddItem(new Sandals());

            PackGold(100, 200);
        }

        public override double WeaponAbilityChance { get { return 0.7; } }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Combatant == null || (m is BaseCreature && !((BaseCreature)m).AlwaysMurderer && !m.Criminal) || m is PlayerMobile || m is BasePeacemaker)
            {
                return false;
            }

            return true;
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            return true;
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.Mobile.InRange(this.Location, 18))
            {
                if (e.Speech.ToLower().Contains("guard") || e.Speech.ToLower().Contains("peacemaker") || e.Speech.ToLower().Contains("help"))
                {
                    this.Direction = GetDirectionTo(e.Mobile);
                    if (e.Mobile.Combatant is BaseVendor || e.Mobile.Combatant is BasePeacemaker || e.Mobile.Combatant is PlayerMobile)
                    {
                        this.Warmode = false;
                        this.Combatant = null;
                    }
                    else if (e.Mobile.Combatant == null)
                    {
                        this.Warmode = false;
                        this.Combatant = null;
                    }
                    else
                    {
                        this.Say(speech[Utility.Random(speech.Length)]);
                        this.Warmode = true;
                        this.Combatant = e.Mobile.Combatant;
                    }
                }
                base.OnSpeech(e);
            }
        }

        public override bool BardImmune { get { return true; } }

        private static string[] speech = new string[]
		{
			"To the fight!",
			"To arms!",
			"Attack!",
			"The battle is on!",
			"To your weapons!",
			"I have my eye on my enemy!",
			"Time to die!",
			"Nothing walks away!",
			"I have sight of my enemy!",
			"You will not prevail!",
			"To my side!",
			"We must defend our land!",
			"Fight for our people!",
			"I see them!",
			"Destroy them all!"
		};

        #region Serialization
        public BasePeacemaker(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
        #endregion
    }

    public class FighterPeacemaker : BasePeacemaker
    {
        [Constructable]
        public FighterPeacemaker()
            : base(AIType.AI_Melee, 1)
        {
            SetStr(220, 300);
            SetDex(40, 60);
            SetInt(40, 60);

            Item weapon;
            switch (Utility.Random(6))
            {
                case 0: weapon = new Broadsword(); break;
                case 1: weapon = new Cutlass(); break;
                case 2: weapon = new Katana(); break;
                case 3: weapon = new Longsword(); break;
                case 4: weapon = new Scimitar(); break;
                default: weapon = new VikingSword(); break;
            }
            AddItem(weapon);

            AddItem(new MetalShield());

            SetDamageType(ResistanceType.Physical, 100);

            SetSkill(SkillName.Tactics, 70.1, 95.0);
            SetSkill(SkillName.Swords, 70.1, 100.0);
            SetSkill(SkillName.Fencing, 65.1, 100.0);
            SetSkill(SkillName.MagicResist, 80.1, 110.0);
            SetSkill(SkillName.Macing, 75.1, 100.0);
            SetSkill(SkillName.Wrestling, 65.1, 100.0);
            SetSkill(SkillName.Parry, 70.1, 100.0);
            SetSkill(SkillName.Healing, 65.1, 75.0);
            SetSkill(SkillName.Anatomy, 80.1, 90.0);
        }

        #region Serialization
        public FighterPeacemaker(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
        #endregion
    }

    public class ArcherPeacemaker : BasePeacemaker
    {
        [Constructable]
        public ArcherPeacemaker()
            : base(AIType.AI_Archer, 8)
        {
            SetStr(70, 90);
            SetDex(100, 150);
            SetInt(20, 35);

            Item weapon;
            switch (Utility.Random(4))
            {
                case 0: weapon = new BarbedLongbow(); break;
                case 1: weapon = new CompositeBow(); break;
                case 2: weapon = new JukaBow(); break;
                default: weapon = new Bow(); break;
            }
            AddItem(weapon);

            AddItem(new Quiver());

            Container pack = new Backpack();
            pack.Movable = false;
            Arrow arrows = new Arrow(50);
            pack.DropItem(arrows);

            SetSkill(SkillName.Tactics, 70.1, 95.0);
            SetSkill(SkillName.Archery, 70.1, 100.0);
            SetSkill(SkillName.Fencing, 65.1, 100.0);
            SetSkill(SkillName.MagicResist, 80.1, 110.0);
            SetSkill(SkillName.Macing, 75.1, 100.0);
            SetSkill(SkillName.Wrestling, 65.1, 100.0);
            // SetSkill(SkillName.DetectHidden, 70.1, 100.0);
            SetSkill(SkillName.Healing, 65.1, 75.0);
            SetSkill(SkillName.Anatomy, 80.1, 90.0);
        }

        #region Serialization
        public ArcherPeacemaker(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
        #endregion
    }

    public class MagePeacemaker : BasePeacemaker
    {
        [Constructable]
        public MagePeacemaker()
            : base(AIType.AI_Mage, 5)
        {
            SetStr(40, 60);
            SetDex(40, 60);
            SetInt(220, 300);

            Item weapon;
            switch (Utility.Random(2))
            {
                case 0: weapon = new Scepter(); break;
                default: weapon = new MagicWand(); break;
            }
            ((BaseWeapon)weapon).Attributes.SpellChanneling = 1;
            AddItem(weapon);

            SetDamageType(ResistanceType.Physical, 0);

            if (Utility.RandomBool())
                SetDamageType(ResistanceType.Cold, 60);
            else
                SetDamageType(ResistanceType.Fire, 60);

            if (Utility.RandomBool())
                SetDamageType(ResistanceType.Energy, 40);
            else
                SetDamageType(ResistanceType.Poison, 40);

            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 90.1, 100.0);
            SetSkill(SkillName.Necromancy, 0, 110.0);
            SetSkill(SkillName.SpiritSpeak, 90.0, 110.0);
            SetSkill(SkillName.MagicResist, 150.5, 200.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Macing, 60.1, 80.0);
        }

        #region Serialization
        public MagePeacemaker(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
        #endregion
    }
}