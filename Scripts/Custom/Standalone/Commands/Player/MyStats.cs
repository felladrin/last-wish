// Created by test444.
using System;
using Server.Network;
using Server.Items;
using Server.Commands;

namespace Server.Gumps
{
    public class StatsGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("MyStats", AccessLevel.Player, new CommandEventHandler(MyStats_OnCommand));
        }

        [Usage("MyStats")]
        [Description("Displays a gump with your character stats.")]
        public static void MyStats_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            from.CloseGump(typeof(StatsGump));
            from.SendGump(new StatsGump(from));
				
        }

        public StatsGump(Mobile from)
            : base(100, 100)
        {
            // configuration
            const int hue = 1149;
            const int LRCCap = 100;
            const int LMCCap = 40;
            const double BandageSpeedCap = 2.0;
            const int SwingSpeedCap = 100;
            const int HCICap = 45;
            const int DCICap = 45;
            const int FCCap = 4;
            const int FCRCap = 4;
            const int DamageIncreaseCap = 100;
            const int SDICap = 100;
            const int ReflectDamageCap = 100;
            const int SSICap = 100;
            
            int LRC = AosAttributes.GetValue(from, AosAttribute.LowerRegCost) > LRCCap ? LRCCap : AosAttributes.GetValue(from, AosAttribute.LowerRegCost);
            int LMC = AosAttributes.GetValue(from, AosAttribute.LowerManaCost) > LMCCap ? LMCCap : AosAttributes.GetValue(from, AosAttribute.LowerManaCost);
            double BandageSpeed = (2.0 + (0.5 * ((double)(205 - from.Dex) / 10))) < BandageSpeedCap ? BandageSpeedCap : (2.0 + (0.5 * ((double)(205 - from.Dex) / 10)));
            TimeSpan SwingSpeed = (from.Weapon as BaseWeapon).GetDelay(from) > TimeSpan.FromSeconds(SwingSpeedCap) ? TimeSpan.FromSeconds(SwingSpeedCap) : (from.Weapon as BaseWeapon).GetDelay(from);
            int HCI = AosAttributes.GetValue(from, AosAttribute.AttackChance) > HCICap ? HCICap : AosAttributes.GetValue(from, AosAttribute.AttackChance);
            int DCI = AosAttributes.GetValue(from, AosAttribute.DefendChance) > DCICap ? DCICap : AosAttributes.GetValue(from, AosAttribute.DefendChance);
            int FC = AosAttributes.GetValue(from, AosAttribute.CastSpeed) > FCCap ? FCCap : AosAttributes.GetValue(from, AosAttribute.CastSpeed);
            int FCR = AosAttributes.GetValue(from, AosAttribute.CastRecovery) > FCRCap ? FCRCap : AosAttributes.GetValue(from, AosAttribute.CastRecovery);
            int DamageIncrease = AosAttributes.GetValue(from, AosAttribute.WeaponDamage) > DamageIncreaseCap ? DamageIncreaseCap : AosAttributes.GetValue(from, AosAttribute.WeaponDamage);
            int SDI = AosAttributes.GetValue(from, AosAttribute.SpellDamage) > SDICap ? SDICap : AosAttributes.GetValue(from, AosAttribute.SpellDamage);
            int ReflectDamage = AosAttributes.GetValue(from, AosAttribute.ReflectPhysical) > ReflectDamageCap ? ReflectDamageCap : AosAttributes.GetValue(from, AosAttribute.ReflectPhysical);
            int SSI = AosAttributes.GetValue(from, AosAttribute.WeaponSpeed) > SSICap ? SSICap : AosAttributes.GetValue(from, AosAttribute.WeaponSpeed);
            
            AddPage(0);

            AddImage(0, 0, 30500); //Background
            AddButton(348, 400, 247, 248, 0, GumpButtonType.Reply, 1); //Ok
            AddLabel(69, 400, hue, String.Format("{0}", from.Name)); //Name
            AddLabel(69, 43, hue, "Character Stats");
            AddImage(150 - 10, 78, 12); //Model
            AddImage(150 - 10, 78, 50970, 47); //Robe

            AddPage(1);

            AddLabel(69, 72, hue, "Str");
            AddLabel(69, 132, hue, "Dex");
            AddLabel(69, 192, hue, "Int");
            AddLabel(69, 252, hue, "Fame");
            AddLabel(69, 312, hue, "Karma");

            AddImageTiled(69, 100, 111 - 10, 20, 9264);
            AddImageTiled(69, 160, 111 - 10, 20, 9264);
            AddImageTiled(69, 220, 111 - 10, 20, 9264);
            AddImageTiled(69, 280, 111 - 10, 20, 9264);
            AddImageTiled(69, 340, 111 - 10, 20, 9264);
   		
            AddLabel(69, 100, hue, String.Format(" {0} + {1}", from.RawStr, from.Str - from.RawStr)); //str
            AddLabel(69, 160, hue, String.Format(" {0} + {1}", from.RawDex, from.Dex - from.RawDex)); //dex
            AddLabel(69, 220, hue, String.Format(" {0} + {1}", from.RawInt, from.Int - from.RawInt)); //int
            AddLabel(69, 280, hue, String.Format(" {0} ", from.Fame)); //fame
            AddLabel(69, 340, hue, String.Format(" {0} ", from.Karma)); //karma

            AddLabel(190, 72, hue, "Tithing Points");
            AddLabel(190, 312, hue, "Kills");

            AddImageTiled(190, 100, 111 - 10, 20, 9264); 
            AddImageTiled(190, 340, 111 - 10, 20, 9264); 
 		
            AddLabel(190, 100, hue, String.Format(" {0} ", from.TithingPoints)); //tith points
            AddLabel(190, 340, hue, String.Format(" {0} ", from.Kills)); //kills

            AddLabel(310, 72, hue, "LRC");
            AddLabel(310, 132, hue, "LMC");
            AddLabel(310, 192, hue, "Bandage Speed");
            AddLabel(310, 252, hue, "Swing Speed");
            AddLabel(310, 312, hue, "HCI / DCI");

            AddImageTiled(310, 100, 111 - 10, 20, 9264);
            AddImageTiled(310, 160, 111 - 10, 20, 9264);
            AddImageTiled(310, 220, 111 - 10, 20, 9264);
            AddImageTiled(310, 280, 111 - 10, 20, 9264);
            AddImageTiled(310, 340, 111 - 10, 20, 9264);

            AddLabel(310, 100, hue, String.Format(" {0} %", LRC)); //lrc
            AddLabel(310, 160, hue, String.Format(" {0} %", LMC)); //lmc
            AddLabel(310, 220, hue, String.Format(" {0:0.0}s", new DateTime(TimeSpan.FromSeconds(BandageSpeed).Ticks).ToString("s.ff"))); //bandage speed
            AddLabel(310, 280, hue, String.Format(" {0}s", new DateTime(SwingSpeed.Ticks).ToString("s.ff"))); //swing speed
            AddLabel(310, 340, hue, String.Format(" {0} / {1}", HCI, DCI)); //hci/dci
   		
            AddButton(338, 46, 5601, 5605, 0, GumpButtonType.Page, 2);
            AddLabel(360, 43, hue, "Next Page");

            AddPage(2);

            AddLabel(69, 72, hue, "Hits");
            AddLabel(69, 132, hue, "Stamina");
            AddLabel(69, 192, hue, "Mana");
            AddLabel(69, 252, hue, "FC");
            AddLabel(69, 312, hue, "FCR");

            AddImageTiled(69, 100, 111 - 10, 20, 9264);
            AddImageTiled(69, 160, 111 - 10, 20, 9264);
            AddImageTiled(69, 220, 111 - 10, 20, 9264);
            AddImageTiled(69, 280, 111 - 10, 20, 9264);
            AddImageTiled(69, 340, 111 - 10, 20, 9264);
   		
            AddLabel(69, 100, hue, String.Format(" {0} + {1}", from.Hits - AosAttributes.GetValue(from, AosAttribute.BonusHits), AosAttributes.GetValue(from, AosAttribute.BonusHits))); //hits
            AddLabel(69, 160, hue, String.Format(" {0} + {1}", from.Stam - AosAttributes.GetValue(from, AosAttribute.BonusStam), AosAttributes.GetValue(from, AosAttribute.BonusStam))); //stamina
            AddLabel(69, 220, hue, String.Format(" {0} + {1}", from.Mana - AosAttributes.GetValue(from, AosAttribute.BonusMana), AosAttributes.GetValue(from, AosAttribute.BonusMana))); //mana
            AddLabel(69, 280, hue, String.Format(" {0}", FC)); //fc
            AddLabel(69, 340, hue, String.Format(" {0}", FCR)); //fcr

            AddLabel(190, 72, hue, "Damage Increase");
            AddLabel(190, 312, hue, "SDI");

            AddImageTiled(190, 100, 111 - 10, 20, 9264); 
            AddImageTiled(190, 340, 111 - 10, 20, 9264); 
 		
            AddLabel(190, 100, hue, String.Format(" {0} %", DamageIncrease)); //di
            AddLabel(190, 340, hue, String.Format(" {0} %", SDI)); //sdi

            AddLabel(310, 72, hue, "Hits regen");
            AddLabel(310, 132, hue, "Stam regen");
            AddLabel(310, 192, hue, "Mana regen");
            AddLabel(310, 252, hue, "Reflect dmg");
            AddLabel(310, 312, hue, "SSI");

            AddImageTiled(310, 100, 111 - 10, 20, 9264);
            AddImageTiled(310, 160, 111 - 10, 20, 9264);
            AddImageTiled(310, 220, 111 - 10, 20, 9264);
            AddImageTiled(310, 280, 111 - 10, 20, 9264);
            AddImageTiled(310, 340, 111 - 10, 20, 9264);

            AddLabel(310, 100, hue, String.Format(" {0}", AosAttributes.GetValue(from, AosAttribute.RegenHits))); //hp regen
            AddLabel(310, 160, hue, String.Format(" {0}", AosAttributes.GetValue(from, AosAttribute.RegenStam))); //s regen
            AddLabel(310, 220, hue, String.Format(" {0}", AosAttributes.GetValue(from, AosAttribute.RegenMana))); //m regen
            AddLabel(310, 280, hue, String.Format(" {0} %", ReflectDamage)); //reflect
            AddLabel(310, 340, hue, String.Format(" {0} %", SSI)); //ssi

            AddButton(338, 46, 5603, 5607, 0, GumpButtonType.Page, 1);
            AddLabel(360, 43, hue, "Prev Page");
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            switch (info.ButtonID)
            {
                case 1:
                    {
                        break;
                    }
            }
        }
    }
}