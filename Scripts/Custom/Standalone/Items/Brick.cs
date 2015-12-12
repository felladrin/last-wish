//   ___|========================|___
//   \  |  Written by Felladrin  |  /	Based on the script created by Gacoperz in 2007.
//    > |      August 2013       | <
//   /__|========================|__\	[Bricks] - Current version: 1.1 (August 04, 2013)

using System.Collections;
using Server.Items;
using Server.Commands;
using Server.Targeting;

namespace Server.Custom.Engines.Bricks
{
    public class RemoveBricksCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("Demolition", AccessLevel.GameMaster, new CommandEventHandler(RemoveBricks_OnCommand));
        }

        [Usage("Demolition")]
        [Description("Removes all Bricks from the world.")]
        public static void RemoveBricks_OnCommand(CommandEventArgs e)
        {
            ArrayList itemList = new ArrayList();

            foreach (Item i in World.Items.Values)
                if (i is Brick || i is BaseProtoBrick || i is ProtoBrickBox || i is BrickLifter || i is BrickFillister)
                    itemList.Add(i);

            int itemCount = itemList.Count;

            foreach (Item i in itemList)
                i.Delete();

            itemList.Clear();

            e.Mobile.SendMessage(66, "All the {0} Bricks have been removed from the world.", itemCount);
        }
    }

    public class BaseProtoBrick : Item, IDyable
    {
        private int MinItemID;
        private int MaxItemID;

        public override bool Decays { get { return false; } }

        public BaseProtoBrick(string name, int itemID)
            : base(itemID)
        {
            Name = name;
            MinItemID = itemID;
            MaxItemID = itemID + 20;
            Weight = 0;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            list.Add(Name);
            list.Add("Single Click to Transform & Double Click to Duplicate");
        }

        public override void OnAosSingleClick(Mobile from)
        {
            ItemID++;

            if (ItemID > MaxItemID)
                ItemID = MinItemID;
        }

        public override void OnSingleClick(Mobile from)
        {
            OnAosSingleClick(from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.AddToBackpack(new Brick(ItemID, Hue, from));
        }

        public override bool OnDroppedToWorld(Mobile from, Point3D p)
        {
            if (from.Region.Name == null)
            {
                return base.OnDroppedToWorld(from, p);
            }
            else
            {
                from.SendMessage(33, "No. Not here.");
                return false;
            }
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Parent != null)
            {
                Hue = sender.DyedHue;
                return true;
            }
            else
            {
                from.SendMessage("You cannot dye bricks from the ground.");
                return false;
            }
        }

        public BaseProtoBrick(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.WriteEncodedInt((int)MinItemID);
            writer.WriteEncodedInt((int)MaxItemID);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            MinItemID = reader.ReadEncodedInt();
            MaxItemID = reader.ReadEncodedInt();
        }
    }

    public class WornSandProtoBrick : BaseProtoBrick
    {
        [Constructable]
        public WornSandProtoBrick() : base("Worn Sand Brick", 0x03EE) { }
        public WornSandProtoBrick(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class MarbleProtoBrick : BaseProtoBrick
    {
        [Constructable]
        public MarbleProtoBrick() : base("Marble Brick", 0x0709) { }
        public MarbleProtoBrick(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class StoneProtoBrick : BaseProtoBrick
    {
        [Constructable]
        public StoneProtoBrick() : base("Stone Brick", 0x071E) { }
        public StoneProtoBrick(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class LightWoodProtoBrick : BaseProtoBrick
    {
        [Constructable]
        public LightWoodProtoBrick() : base("Light Wood Brick", 0x0721) { }
        public LightWoodProtoBrick(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class WoodProtoBrick : BaseProtoBrick
    {
        [Constructable]
        public WoodProtoBrick() : base("Wood Brick", 0x0738) { }
        public WoodProtoBrick(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class LightStoneProtoBrick : BaseProtoBrick
    {
        [Constructable]
        public LightStoneProtoBrick() : base("Light Stone Brick", 0x0750) { }
        public LightStoneProtoBrick(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class SandStoneProtoBrick : BaseProtoBrick
    {
        [Constructable]
        public SandStoneProtoBrick() : base("Sand Stone Brick", 0x076C) { }
        public SandStoneProtoBrick(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class DarkStoneProtoBrick : BaseProtoBrick
    {
        [Constructable]
        public DarkStoneProtoBrick() : base("Dark Stone Brick", 0x0788) { }
        public DarkStoneProtoBrick(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class BrickProtoBrick : BaseProtoBrick
    {
        [Constructable]
        public BrickProtoBrick() : base("Brick Brick", 0x07A3) { }
        public BrickProtoBrick(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class Brick : Item, IDyable
    {
        public Mobile m_Owner;

        public override bool Decays { get { return false; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }

        public Brick(int itemID, int hue, Mobile owner)
        {
            Name = "Brick";
            ItemID = itemID;
            m_Owner = owner;
            Hue = hue;
            Weight = 0;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            list.Add(Name);
            list.Add("Single Click to Delete & Double Click to Lockdown");
        }

        public override void OnAosSingleClick(Mobile from)
        {
            if (Parent != null)
            {
                Delete();
            }
            else
            {
                from.SendMessage("You cannot delete bricks from the ground.");
            }
        }

        public override void OnSingleClick(Mobile from)
        {
            OnAosSingleClick(from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Parent == null)
            {
                if (from == m_Owner)
                {
                    if (Movable)
                    {
                        Movable = false;
                    }
                    else
                    {
                        Movable = true;
                    }
                }
                else
                {
                    from.SendMessage("Only the creator of this brick can interact with it.");
                }
            }
            else
            {
                from.SendMessage("That must be on the ground for you to lock it down.");
            }
        }

        public override bool OnDroppedToWorld(Mobile from, Point3D p)
        {
            if (from.Region.Name == null)
            {
                return base.OnDroppedToWorld(from, p);
            }
            else
            {
                from.SendMessage(33, "No. Not here.");
                return false;
            }
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
            {
                return false;
            }
            else if (RootParent is Mobile && from != RootParent)
            {
                return false;
            }
            else if (Movable == true || (from == m_Owner && Movable == false))
            {
                Hue = sender.DyedHue;
                return true;
            }

            return false;
        }

        public Brick(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write((Mobile)m_Owner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Owner = reader.ReadMobile();
        }
    }

    public class ProtoBrickBox : LargeCrate
    {
        [Constructable]
        public ProtoBrickBox()
        {
            Name = "Box of Bricks";
            Weight = 0;

            DropItem(new WornSandProtoBrick());
            DropItem(new MarbleProtoBrick());
            DropItem(new StoneProtoBrick());
            DropItem(new LightWoodProtoBrick());
            DropItem(new WoodProtoBrick());
            DropItem(new LightStoneProtoBrick());
            DropItem(new SandStoneProtoBrick());
            DropItem(new DarkStoneProtoBrick());
            DropItem(new BrickProtoBrick());
            DropItem(new BrickLifter());
            DropItem(new BrickFillister());
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            list.Add(Name);
            list.Add("Event Construction Set");
        }

        public override bool OnDroppedToWorld(Mobile from, Point3D p)
        {
            if (from.Region.Name == null)
            {
                return base.OnDroppedToWorld(from, p);
            }
            else
            {
                from.SendMessage(33, "No. Not here.");
                return false;
            }
        }

        public override bool Decays { get { return false; } }

        public ProtoBrickBox(Serial serial) : base(serial) { }

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

    public class BrickLifter : Item
    {
        [Constructable]
        public BrickLifter()
        {
            Name = "Brick Lifter";
            ItemID = 7867;
            Weight = 0;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            list.Add(Name);
            list.Add("Use it to move a brick up");
        }

        public override bool OnDroppedToWorld(Mobile from, Point3D p)
        {
            if (from.Region.Name == null)
            {
                return base.OnDroppedToWorld(from, p);
            }
            else
            {
                from.SendMessage(33, "No. Not here.");
                return false;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.Target = new InternalTarget();
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(-1, false, TargetFlags.None) { }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Brick && ((Brick)targeted).Owner == from)
                {
                    Brick brick = targeted as Brick;
                    brick.Location = new Point3D(brick.Location, brick.Z + 1);
                }
                else
                {
                    from.SendMessage("You can only use it on bricks, and only on those you have built.");
                }

                from.Target = new InternalTarget();
            }
        }

        public override bool Decays { get { return false; } }

        public BrickLifter(Serial serial) : base(serial) { }

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

    public class BrickFillister : Item
    {
        [Constructable]
        public BrickFillister()
        {
            Name = "Brick Fillister";
            ItemID = 13048;
            Weight = 0;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            list.Add(Name);
            list.Add("Use it to move a brick down");
        }

        public override bool OnDroppedToWorld(Mobile from, Point3D p)
        {
            if (from.Region.Name == null)
            {
                return base.OnDroppedToWorld(from, p);
            }
            else
            {
                from.SendMessage(33, "No. Not here.");
                return false;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.Target = new InternalTarget();
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(-1, false, TargetFlags.None) { }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Brick && ((Brick)targeted).Owner == from)
                {
                    Brick brick = targeted as Brick;
                    brick.Location = new Point3D(brick.Location, brick.Z - 1);
                }
                else
                {
                    from.SendMessage("You can only use it on bricks, and only on those you have built.");
                }

                from.Target = new InternalTarget();
            }
        }

        public override bool Decays { get { return false; } }

        public BrickFillister(Serial serial) : base(serial) { }

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

    [Flipable(0x2811, 0x2812)]
    public class BricklayerBox : Item
    {
        [Constructable]
        public BricklayerBox()
        {
            Name = "Bricklayer's Box";
            Movable = false;
            ItemID = 0x2811;
            Weight = 0;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            list.Add(Name);
            list.Add("Get your Box of Bricks here!");
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Backpack.FindItemByType(typeof(ProtoBrickBox)) == null)
            {
                from.AddToBackpack(new ProtoBrickBox());
            }
            else
            {
                from.SendMessage(67, "You've already got your box!");
            }
        }

        public override bool Decays { get { return false; } }

        public BricklayerBox(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}