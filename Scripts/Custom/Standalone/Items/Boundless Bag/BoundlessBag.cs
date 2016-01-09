// Boundless Bag v1.0.1
// Author: Felladrin
// Started: 2013-08-17
// Updated: 2016-01-09

using System;
using Server;
using Server.Items;

namespace Felladrin.Items
{
    class BoundlessBag : BaseContainer, IDyable
    {
        public BoundlessBag() : base(0xE76) { }

        public virtual Type[] AllowedItemTypes { get { return new Type[] { }; } }

        public override void GetProperties(ObjectPropertyList list)
        {
            list.Add(Name);
            list.Add("Items Inside: {0}", GetAmount(AllowedItemTypes));
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (InTypeList(dropped, AllowedItemTypes))
                return base.OnDragDrop(from, dropped);

            from.SendMessage(38, "It does not fit in this bag.");
            return false;
        }

        public override bool OnDragDropInto(Mobile from, Item item, Point3D p)
        {
            if (InTypeList(item, AllowedItemTypes))
                return base.OnDragDropInto(from, item, p);

            from.SendMessage(38, "It does not fit in this bag.");
            return false;
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;

            if (Parent == from.Backpack)
            {
                Hue = sender.DyedHue;
                return true;
            }
            else
            {
                from.SendMessage(38, "It must be in your backpack for you to dye it.");
                return false;
            }
        }

        public static bool InTypeList(Item item, Type[] types)
        {
            Type t = item.GetType();

            for (int i = 0; i < types.Length; ++i)
                if (types[i].IsAssignableFrom(t))
                    return true;

            return false;
        }

        public override int DefaultMaxWeight { get { return 0; } }

        public override int DefaultMaxItems { get { return 0; } }

        public override bool DisplayWeight { get { return false; } }

        public override bool CheckHold(Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight) { return true; }

        public BoundlessBag(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}
