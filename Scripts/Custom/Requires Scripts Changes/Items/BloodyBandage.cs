/**************************************
*Script Name: Bloody Bandages         *
*Author: Joeku AKA Demortris          *
*For use with RunUO 2.0               *
*Client Tested with: 5.0.7.1          *
*Version: 3.2                         *
*Initial Release: 11/11/05            *
*Revision Date: 02/28/06              *
**************************************/
/*
Open "Scripts/Items/Skill Items/Misc/Bandage.cs" and add at the end of the EndHeal() method:

if( m_Healer.Alive )
	BloodyBandage.GiveBandage( m_Healer, m_Patient );
*/

using System;
using Server.Targeting;

namespace Server.Items
{
    public class BloodyBandage : Item
    {
        private static bool ConsumeBeverages = true;    // consume quantity of beverages when used?
        private static int ConsumeRatio = 10;           // every <ConsumeRatio> bandages to 1 quantity of beverage
        private static int ChanceToRecover = 0;         // Overrides ScaledChanceToRecover. Not based on skill. Set to 0 to deactivate.
        private static int ScaledChanceToRecover = 75;  // Percent chance to recover at GM Primary/Secondary skill (scales along with skill value)

        private static int[,] WaterTiles_Land = new int[3, 2]
        {   { 168, 171, }, { 310, 311, }, { 16368, 16371 }      };
        private static int[,] WaterTiles_Static = new int[18, 2]
        {   { 2881, 2884 }, { 4088, 4089 }, { 4090, 4090 },
            { 4104, 4104 }, { 5453, 5453 }, { 5456, 5462 },
            { 5464, 5465 }, { 5937, 5978 }, { 6038, 6066 },
            { 6595, 6636 }, { 8081, 8084 }, { 8093, 8094 },
            { 8099, 8138 }, { 9299, 9309 }, { 13422, 13445 },
            { 13456, 13483 }, { 13493, 13525 }, { 13549, 13616 }};

        [Constructable]
        public BloodyBandage() : this(1) { }

        [Constructable]
        public BloodyBandage(int amt) : base(0xE20)
        {
            this.Stackable = true;
            this.Weight = 0.1;
            this.Amount = amt;
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (this.Amount > 1)
                list.Add(String.Format("{0} bloody bandages", this.Amount));
            else
                list.Add("a bloody bandage");
        }

        public static void GiveBandage(Mobile m, Mobile targ)
        {
            if (ChanceToRecover > 0)
            {
                if (Utility.Random(100) >= ChanceToRecover - 1)
                {
                    m.AddToBackpack(new BloodyBandage());
                    m.SendMessage("You were able to recover a soiled bandage.");
                }
            }
            else
            {
                double primary = m.Skills[BandageContext.GetPrimarySkill(targ)].Base;
                double secondary = m.Skills[BandageContext.GetSecondarySkill(targ)].Base;

                double skillz = (primary * (2.0 / 3.0)) + (secondary * (1.0 / 3.0));    // primary and secondary GM should = 100
                double chance = (((double)ScaledChanceToRecover / 100) * skillz);

                // TEST VALUES
                // -----------
                // 100 healing and anatomy = 100 skillz
                // 50 ScaledChanceToRecover = 50 chance
                // -----------
                // 100 healing and 50 anatomy = ~83.3 skillz
                // 50 ScaledChanceToRecover = ~41.6 chance
                // -----------
                // 50 healing and 100 anatomy = ~66.6 skillz
                // 50 ScaledChanceToRecover = ~33.3 chance

                if (chance >= Utility.Random(100))
                    FinishGiveBandage(m);
            }
        }

        public static void FinishGiveBandage(Mobile m)
        {
            m.AddToBackpack(new BloodyBandage());
            m.SendMessage("You were able to recover a soiled bandage.");
        }

        public override void OnDoubleClick(Mobile m)
        {
            m.SendMessage("What will you wash the bloody bandage{0} in?", this.Amount > 1 ? "s" : "");
            m.Target = new InternalTarget(this);
        }

        public void OnAfterTarget(Mobile m, bool found, BaseBeverage bev)
        {
            if (found)
            {
                int amt = this.Amount;

                if (bev != null && bev.Content == BeverageType.Water && bev.Poison != null)
                {
                    --bev.Quantity;
                    m.ApplyPoison(bev.Poisoner, bev.Poison);
                    m.SendMessage("The water was poisoned!");
                    return;
                }
                else if (bev != null && bev.Content == BeverageType.Water && ConsumeBeverages)
                {
                    if (amt > bev.Quantity * ConsumeRatio)
                        amt = bev.Quantity * ConsumeRatio;

                    int toConsume = Math.Min(bev.Quantity, (amt - 1) / 10 + 1) * 10;
                    bev.Quantity -= toConsume / 10;
                    int bandageCount = Math.Min(toConsume, amt);
                    m.AddToBackpack(new Bandage(bandageCount));
                    m.SendMessage("You wash {0} bloody bandage{1} and put the clean bandage{1} in your pack.", bandageCount > 1 ? bandageCount.ToString() : "the", bandageCount > 1 ? "s" : "");
                    if ((this.Amount -= bandageCount) <= 0)
                        this.Delete();
                }
                else
                {
                    m.AddToBackpack(new Bandage(amt));
                    m.SendMessage("You wash {0} bloody bandage{1} and put the clean bandage{1} in your pack.", amt > 1 ? amt.ToString() : "the", amt > 1 ? "s" : "");
                    this.Delete();
                }
            }
            else
                m.SendMessage("You can only wash bloody bandages in water.");
        }

        private class InternalTarget : Target
        {
            private BloodyBandage p_Bandage;

            public InternalTarget(BloodyBandage bandage) : base(3, true, TargetFlags.None)
            { p_Bandage = bandage; }

            protected override void OnTarget(Mobile m, object targ)
            {
                if (p_Bandage.Deleted || p_Bandage.Amount < 1)
                    return;

                m.RevealingAction();

                int id;
                bool found = false;

                if (targ is LandTarget)
                {
                    id = (targ as LandTarget).TileID;

                    for (int i = 0; i < WaterTiles_Land.Length / 2; i++)
                    {
                        if (id >= WaterTiles_Land[i, 0] && id <= WaterTiles_Land[i, 1])
                        {
                            found = true;
                            break;
                        }
                    }
                }
                else if (targ is Item || targ is StaticTarget)
                {
                    if (targ is Item)
                        id = (targ as Item).ItemID;
                    else
                        id = (targ as StaticTarget).ItemID;

                    for (int i = 0; i < WaterTiles_Static.Length / 2; i++)
                    {
                        if (id >= WaterTiles_Static[i, 0] && id <= WaterTiles_Static[i, 1])
                        {
                            found = true;
                            break;
                        }
                    }
                }

                if (targ is BaseBeverage)
                    p_Bandage.OnAfterTarget(m, found, (targ as BaseBeverage));
                else
                    p_Bandage.OnAfterTarget(m, found, null);
            }
        }

        public BloodyBandage(Serial serial) : base(serial) { }

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