// Homeowner Tools editions by Djervy.
using Server;
using Server.Network;
using Server.Multis;
using Server.Spells;
using Server.Gumps;
using Server.Targeting;

namespace Server.Items
{
    public enum DecorateCommand
    {
        None,
        Turn,
        Up,
        Down,
        North,
        East,
        South,
        West,
        Lock,
        Secure,
        Release,
        Trash,
        Close
    }

    public class InteriorDecorator : Item
    {
        private DecorateCommand m_Command;

        [CommandProperty(AccessLevel.GameMaster)]
        public DecorateCommand Command
        {
            get { return m_Command; }
            set
            {
                m_Command = value;
                InvalidateProperties();
            }
        }

        [Constructable]
        public InteriorDecorator()
            : base(0x1EBA)
        {
            Name = "Homeowner Tools";
            Weight = 1.0;
        }

        public InteriorDecorator(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            list.Add(Name);
            list.Add("For Easier Home Management");
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!CheckUse(this, from))
                return;

            from.CloseGump(typeof(InternalGump));
            from.SendGump(new InternalGump(this));
        }

        public static bool InHouse(Mobile from)
        {
            BaseHouse house = BaseHouse.FindHouseAt(from);
            return (house != null && house.IsCoOwner(from));
        }

        public static bool CheckUse(InteriorDecorator tool, Mobile from)
        {
            if (tool == null)
                return false;
            
            if (!InHouse(from))
            {
                from.SendLocalizedMessage(502092); // You must be in your house to do this.
                return false;
            }

            return true;
        }

        private class InternalGump : Gump
        {
            private InteriorDecorator m_Decorator;

            public InternalGump(InteriorDecorator decorator)
                : base(150, 50)
            {
                m_Decorator = decorator;

                AddBackground(0, 0, 170, 555, 2600);

                AddButton(40, 45, 2151, 2152, 1, GumpButtonType.Reply, 0);
                AddHtmlLocalized(80, 50, 70, 40, 1018323, false, false); // Turn

                AddButton(40, 85, 2151, 2152, 2, GumpButtonType.Reply, 0);
                AddHtmlLocalized(80, 90, 70, 40, 1018324, false, false); // Up

                AddButton(40, 125, 2151, 2152, 3, GumpButtonType.Reply, 0);
                AddHtmlLocalized(80, 130, 70, 40, 1018325, false, false); // Down

                AddButton(40, 165, 2151, 2152, 4, GumpButtonType.Reply, 0);
                AddHtmlLocalized(80, 170, 70, 40, 1075389, false, false); // north

                AddButton(40, 205, 2151, 2152, 5, GumpButtonType.Reply, 0);
                AddHtmlLocalized(80, 210, 70, 40, 1075387, false, false); // east

                AddButton(40, 245, 2151, 2152, 6, GumpButtonType.Reply, 0);
                AddHtmlLocalized(80, 250, 70, 40, 1075386, false, false); // south

                AddButton(40, 285, 2151, 2152, 7, GumpButtonType.Reply, 0);
                AddHtmlLocalized(80, 290, 70, 40, 1075390, false, false); // west

                AddButton(40, 325, 2151, 2152, 8, GumpButtonType.Reply, 0);
                AddHtml(80, 330, 70, 40, "Lock", false, false); // lock

                AddButton(40, 365, 2151, 2152, 9, GumpButtonType.Reply, 0);
                AddHtml(80, 370, 70, 40, "Secure", false, false); // secure

                AddButton(40, 405, 2151, 2152, 10, GumpButtonType.Reply, 0);
                AddHtml(80, 410, 70, 40, "Release", false, false); // release

                AddButton(40, 445, 2151, 2152, 11, GumpButtonType.Reply, 0);
                AddHtml(80, 450, 70, 40, "Trash", false, false); // trash

                AddButton(40, 485, 2472, 2472, 12, GumpButtonType.Reply, 0);
                AddHtml(80, 490, 70, 40, "Close", false, false); // close
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                Mobile from = sender.Mobile;

                DecorateCommand command = DecorateCommand.None;

                switch (info.ButtonID)
                {
                    case 1:
                        command = DecorateCommand.Turn;
                        break;
                    case 2:
                        command = DecorateCommand.Up;
                        break;
                    case 3:
                        command = DecorateCommand.Down;
                        break;
                    case 4:
                        command = DecorateCommand.North;
                        break;
                    case 5:
                        command = DecorateCommand.East;
                        break;
                    case 6:
                        command = DecorateCommand.South;
                        break;
                    case 7:
                        command = DecorateCommand.West;
                        break;
                    case 8:
                        command = DecorateCommand.Lock;
                        break;
                    case 9:
                        command = DecorateCommand.Secure;
                        break;
                    case 10:
                        command = DecorateCommand.Release;
                        break;
                    case 11:
                        command = DecorateCommand.Trash;
                        break;
                    case 12:
                        command = DecorateCommand.Close;
                        break;
                }

                if (command == DecorateCommand.Lock)
                {
                    int[] commandi = { 0x0023 };
                    from.DoSpeech("", commandi, 0, 52);
                }
                else if (command == DecorateCommand.Secure)
                {
                    int[] commandi = { 0x0025 };
                    from.DoSpeech("", commandi, 0, 52);
                }
                else if (command == DecorateCommand.Release)
                {
                    int[] commandi = { 0x0024 };
                    from.DoSpeech("", commandi, 0, 52);
                }
                else if (command == DecorateCommand.Trash)
                {
                    int[] commandi = { 0x0028 };
                    from.DoSpeech("", commandi, 0, 52);
                }
                else if (command == DecorateCommand.Close)
                {
                    from.CloseGump(typeof(InternalGump));
                }
                else if (command != DecorateCommand.None)
                {
                    m_Decorator.Command = command;
                    sender.Mobile.Target = new InternalTarget(m_Decorator);
                }

                if (command != DecorateCommand.Close)
                {
                    from.CloseGump(typeof(InternalGump));
                    from.SendGump(new InternalGump(m_Decorator));
                }
            }
        }

        private class InternalTarget : Target
        {
            private InteriorDecorator m_Decorator;

            public InternalTarget(InteriorDecorator decorator)
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
                m_Decorator = decorator;
            }

            protected override void OnTargetNotAccessible(Mobile from, object targeted)
            {
                OnTarget(from, targeted);
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Item && InteriorDecorator.CheckUse(m_Decorator, from))
                {
                    BaseHouse house = BaseHouse.FindHouseAt(from);
                    Item item = (Item)targeted;

                    if (house == null || !house.IsCoOwner(from))
                    {
                        from.SendLocalizedMessage(502092); // You must be in your house to do this.
                    }
                    else if (item.Parent != null || !house.IsInside(item))
                    {
                        from.SendLocalizedMessage(1042270); // That is not in your house.
                    }
                    else if (item is VendorRentalContract)
                    {
                        from.SendLocalizedMessage(1062491); // You cannot use the house decorator on that object.
                    }
                    else
                    {
                        switch (m_Decorator.Command)
                        {
                            case DecorateCommand.Up:
                                Up(item, from);
                                break;
                            case DecorateCommand.Down:
                                Down(item, from);
                                break;
                            case DecorateCommand.Turn:
                                Turn(item, from);
                                break;
                            case DecorateCommand.North:
                                North(item, from, house);
                                break;
                            case DecorateCommand.East:
                                East(item, from, house);
                                break;
                            case DecorateCommand.South:
                                South(item, from, house);
                                break;
                            case DecorateCommand.West:
                                West(item, from, house);
                                break;
                        }
                    }
                }

                from.Target = new InternalTarget(m_Decorator);
            }

            private static void Turn(Item item, Mobile from)
            {
                FlipableAttribute[] attributes = (FlipableAttribute[])item.GetType().GetCustomAttributes(typeof(FlipableAttribute), false);

                if (attributes.Length > 0)
                    attributes[0].Flip(item);
                else
                    from.SendLocalizedMessage(1042273); // You cannot turn that.
            }

            private static void Up(Item item, Mobile from)
            {
                int floorZ = GetFloorZ(item);

                if (floorZ > int.MinValue && item.Z < (floorZ + 15)) // Confirmed : no height checks here
                    item.Location = new Point3D(item.Location, item.Z + 1);
                else
                    from.SendLocalizedMessage(1042274); // You cannot raise it up any higher.
            }

            private static void Down(Item item, Mobile from)
            {
                int floorZ = GetFloorZ(item);

                if (floorZ > int.MinValue && item.Z > GetFloorZ(item))
                    item.Location = new Point3D(item.Location, item.Z - 1);
                else
                    from.SendLocalizedMessage(1042275); // You cannot lower it down any further.
            }

            private static void North(Item item, Mobile from, BaseHouse house)
            {
                Point3D m_PointDest = new Point3D(item.X, item.Y - 1, item.Z);

                if (!SpellHelper.CheckMulti(m_PointDest, from.Map))
                    from.SendMessage("You cannot move it north any further.");
                else if (house.IsInside(new Point3D(item.X, item.Y - 1, item.Z), item.ItemData.Height))
                    item.Y = (item.Y - 1);
                else
                    from.SendMessage("You cannot move it north any further.");
            }

            private static void East(Item item, Mobile from, BaseHouse house)
            {
                Point3D m_PointDest = new Point3D(item.X + 1, item.Y, item.Z);

                if (!SpellHelper.CheckMulti(m_PointDest, from.Map))
                    from.SendMessage("You cannot move it east any further.");
                else if (house.IsInside(new Point3D(item.X + 1, item.Y, item.Z), item.ItemData.Height))
                    item.X = (item.X + 1);
                else
                    from.SendMessage("You cannot move it east any further.");
            }

            private static void South(Item item, Mobile from, BaseHouse house)
            {
                Point3D m_PointDest = new Point3D(item.X, item.Y + 1, item.Z);

                if (!SpellHelper.CheckMulti(m_PointDest, from.Map))
                    from.SendMessage("You cannot move it south any further.");
                else if (house.IsInside(new Point3D(item.X, item.Y + 1, item.Z), item.ItemData.Height))
                    item.Y = (item.Y + 1);
                else
                    from.SendMessage("You cannot move it south any further.");
            }

            private static void West(Item item, Mobile from, BaseHouse house)
            {
                Point3D m_PointDest = new Point3D(item.X - 1, item.Y, item.Z);

                if (!SpellHelper.CheckMulti(m_PointDest, from.Map))
                    from.SendMessage("You cannot move it west any further.");
                else if (house.IsInside(new Point3D(item.X - 1, item.Y, item.Z), item.ItemData.Height))
                    item.X = (item.X - 1);
                else
                    from.SendMessage("You cannot move it west any further.");
            }

            private static int GetFloorZ(Item item)
            {
                Map map = item.Map;

                if (map == null)
                    return int.MinValue;

                StaticTile[] tiles = map.Tiles.GetStaticTiles(item.X, item.Y, true);

                int z = int.MinValue;

                for (int i = 0; i < tiles.Length; ++i)
                {
                    StaticTile tile = tiles[i];
                    ItemData id = TileData.ItemTable[tile.ID & TileData.MaxItemValue];

                    int top = tile.Z; // Confirmed : no height checks here

                    if (id.Surface && !id.Impassable && top > z && top <= item.Z)
                        z = top;
                }

                return z;
            }
        }
    }
}