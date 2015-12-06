//   ___|========================|___
//   \  |  Written by Felladrin  |  /   This script was released on RunUO Community under the GPL licensing terms.
//    > |      August 2013       | <
//   /__|========================|__\   [Boundless Bag] - Current version: 1.0 (August 17, 2013)

using System;

namespace Server.Items
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

        public override bool OnDragDrop(Mobile from, Item item)
        {
            if (InTypeList(item, AllowedItemTypes))
                return base.OnDragDrop(from, item);

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

            if (this.Parent == from.Backpack)
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
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
