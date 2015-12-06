using System;
using System.Collections;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Network;

namespace Server.Custom
{
    public class LinkedBag : Bag
    {
        private LinkedBag mate;
        
        [CommandProperty(AccessLevel.GameMaster)]
        public LinkedBag Mate { get { return mate; } set { mate = value; } }

        [Constructable]
        public LinkedBag() : base()
        {
            Name = "Linked Bag";
            Weight = 2.0;
        }

        public LinkedBag(Serial serial) : base(serial)
        {
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            return OnDragDropInto(from, dropped, new Point3D(20, 100, 0));
        }

        public override bool OnDragDropInto(Mobile from, Item item, Point3D p)
        {
            if (item is LinkedBag)
				return false;
				
            if (mate == null)
				return base.OnDragDropInto(from, item, p);
				
            if (!mate.CheckHold(from, item, true, true))
                return false;
                
            try
            {
                mate.DropItem(item);
            }
            catch
            {
                from.SendMessage("Unable to do that.");
                return false;
            }
            
            return true;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write(mate);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            mate = reader.ReadItem() as LinkedBag;
        }
    }

    public class LinkedBagsBag : Bag
    {
        public override string DefaultName
        {
            get { return "A Bag of Linked Bags"; }
        }

        [Constructable]
        public LinkedBagsBag() : base()
        {
            Movable = true;
            Hue = Utility.RandomBlueHue();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Items.Count == 0 && from.AccessLevel >= AccessLevel.Counselor)
            {
                LinkedBag bagA = new LinkedBag();
                LinkedBag bagB = new LinkedBag();
                bagA.Mate = bagB;
                bagB.Mate = bagA;
                bagA.Name = string.Format("{0}'s Linked Bag", from.Name);
                bagB.Name = string.Format("{0}'s Linked Bag", from.Name);
                DropItem(bagA);
                DropItem(bagB);
            }
            
            base.OnDoubleClick(from);
        }

        public LinkedBagsBag(Serial serial) : base(serial)
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
