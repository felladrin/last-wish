using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class AddStairsGump : Gump
    {
        private int m_Type;

        public void AddBlueBack(int width, int height)
        {
            AddBackground(0, 0, width - 00, height - 00, 0xE10);
            AddBackground(8, 5, width - 16, height - 11, 0x053);
            AddImageTiled(15, 14, width - 29, height - 29, 0xE14);
            AddAlphaRegion(15, 14, width - 29, height - 29);
        }

        public AddStairsGump() : this(-1) { }

        public AddStairsGump(int type) : this(type, 4) { }
        public AddStairsGump(int type, int level)
            : base(50, 40)
        {
            m_Type = type;

            AddPage(0);

            if (m_Type >= 0 && m_Type < m_Types.Length)
            {
                AddBlueBack(44 + (49 * 4), 150);

                if (m_Types[m_Type].m_Type == typeof(StoneStairs))
                {
                    AddItem(22, 28, 0x071F);
                    AddItem(22 + 49, 28, 0x0736);
                    AddItem(22 + (2 * 49), 28, 0x0737);
                    AddItem(22 + (3 * 49), 28, 0x0749);
                }
                else
                {
                    int baseID = m_Types[m_Type].m_BaseID;

                    for (int i = 0; i < 4; ++i)
                    {
                        AddItem(22 + (i * 49), 28, baseID + i + 1);
                    }
                }
                for (int i = 0; i < 4; ++i)
                {
                    AddButton(35 + (i * 49), 13, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0);
                }

                AddLabel(50, 100, 0x480, "Levels:");
                AddBackground(98, 98, 54, 24, 0x2486);
                AddTextEntry(102, 102, 50, 16, 0, 0, String.Format("{0}", level));
            }
            else
            {
                AddBlueBack(44 + (49 * m_Types.Length), 110);

                for (int i = 0; i < m_Types.Length; ++i)
                {
                    AddButton(35 + (i * 49), 13, 0x2624, 0x2625, i + 1, GumpButtonType.Reply, 0);
                    AddItem(22 + (i * 49), 30, m_Types[i].m_BaseID);
                }
            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            int button = info.ButtonID - 1;

            if (m_Type == -1)
            {
                if (button >= 0 && button < m_Types.Length)
                    from.SendGump(new AddStairsGump(button));
            }
            else
            {
                if (button >= 0 && button < 4)
                {
                    TextRelay levelx = info.GetTextEntry(0);
                    string slevel = (levelx == null ? null : levelx.Text.Trim());
                    int level = 0;

                    if (slevel != null && slevel.Length != 0)
                        int.TryParse(slevel, out level);

                    if (level < 1) level = 1;
                    if (level > 20) level = 20;

                    from.SendGump(new AddStairsGump(m_Type, level));
                    CommandSystem.Handle(from, String.Format("{0}Add {1} {2} {3}", CommandSystem.Prefix, m_Types[m_Type].m_Type.Name, (Facing)button, level));
                }
                else
                {
                    from.SendGump(new AddStairsGump());
                }
            }
        }

        public static void Initialize()
        {
            CommandSystem.Register("AddStairs", AccessLevel.GameMaster, new CommandEventHandler(AddStairs_OnCommand));
        }

        [Usage("AddStairs")]
        [Description("Displays a menu from which you can interactively add stairs.")]
        public static void AddStairs_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new AddStairsGump());
        }

        public static StairInfo[] m_Types = new StairInfo[]
			{
				new StairInfo( typeof( WornSandStoneStairs ), 0x03EE ),
                new StairInfo( typeof( MarbleStairs ), 0x0709 ),
				new StairInfo( typeof( StoneStairs ), 0x071E ),
				new StairInfo( typeof( LightWoodStairs ), 0x0721 ),
				new StairInfo( typeof( WoodStairs ), 0x0738 ),
				new StairInfo( typeof( LightStoneStairs ), 0x0750 ),
				new StairInfo( typeof( SandStoneStairs ), 0x076C ),
				new StairInfo( typeof( DarkStoneStairs ), 0x0788 ),
				new StairInfo( typeof( BrickStairs ), 0x07A3 )
			};
    }

    public class StairInfo
    {
        public Type m_Type;
        public int m_BaseID;

        public StairInfo(Type type, int baseID)
        {
            m_Type = type;
            m_BaseID = baseID;
        }
    }
}

namespace Server.Items
{
    public enum Facing
    {
        South,
        West,
        North,
        East
    }

    public class BaseStairAddon : BaseAddon
    {
        public BaseStairAddon(Facing f, int block, int step, int levels)
        {
            AddComponent(new AddonComponent(step), 0, 0, 0);

            switch (f)
            {
                case Facing.South:
                    {
                        for (int i = 0; i < levels; i++)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                AddComponent(new AddonComponent(block), 0, i * -1, j * 5);
                            }
                            AddComponent(new AddonComponent(step), 0, i * -1, i * 5);
                        }

                        break;
                    }
                case Facing.West:
                    {
                        for (int i = 0; i < levels; i++)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                AddComponent(new AddonComponent(block), i * -1, 0, j * 5);
                            }
                            AddComponent(new AddonComponent(step), i * -1, 0, i * 5);
                        }

                        break;
                    }
                case Facing.North:
                    {
                        for (int i = 0; i < levels; i++)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                AddComponent(new AddonComponent(block), 0, i, j * 5);
                            }
                            AddComponent(new AddonComponent(step), 0, i, i * 5);
                        }

                        break;
                    }
                case Facing.East:
                    {
                        for (int i = 0; i < levels; i++)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                AddComponent(new AddonComponent(block), i, 0, j * 5);
                            }
                            AddComponent(new AddonComponent(step), i, 0, i * 5);
                        }

                        break;
                    }
            }
        }

        public BaseStairAddon(Serial serial)
            : base(serial)
        {
        }

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

    public class WornSandStoneStairs : BaseStairAddon
    {
        [Constructable]
        public WornSandStoneStairs(Facing f, int levels)
            : base(f, 0x03EE, 0x03EF + (int)f, levels)
        { }

        public WornSandStoneStairs(Serial serial)
            : base(serial)
        {
        }

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

    public class MarbleStairs : BaseStairAddon
    {
        [Constructable]
        public MarbleStairs(Facing f, int levels)
            : base(f, 0x0709, 0x070A + (int)f, levels)
        { }

        public MarbleStairs(Serial serial)
            : base(serial)
        {
        }

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

    public class StoneStairs : BaseStairAddon
    {
        [Constructable]
        public StoneStairs(Facing f, int levels)
            : base(f, 0x071E, f == Facing.South ? 0x071F : f == Facing.West ? 0x0736 : f == Facing.North ? 0x0737 : 0x0749, levels)
        { }

        public StoneStairs(Serial serial)
            : base(serial)
        {
        }

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

    public class LightWoodStairs : BaseStairAddon
    {
        [Constructable]
        public LightWoodStairs(Facing f, int levels)
            : base(f, 0x0721, 0x0722 + (int)f, levels)
        { }

        public LightWoodStairs(Serial serial)
            : base(serial)
        {
        }

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

    public class WoodStairs : BaseStairAddon
    {
        [Constructable]
        public WoodStairs(Facing f, int levels)
            : base(f, 0x0738, 0x0739 + (int)f, levels)
        { }

        public WoodStairs(Serial serial)
            : base(serial)
        {
        }

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

    public class LightStoneStairs : BaseStairAddon
    {
        [Constructable]
        public LightStoneStairs(Facing f, int levels)
            : base(f, 0x0750, 0x0751 + (int)f, levels)
        { }

        public LightStoneStairs(Serial serial)
            : base(serial)
        {
        }

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

    public class SandStoneStairs : BaseStairAddon
    {
        [Constructable]
        public SandStoneStairs(Facing f, int levels)
            : base(f, 0x076C, 0x076D + (int)f, levels)
        { }

        public SandStoneStairs(Serial serial)
            : base(serial)
        {
        }

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

    public class DarkStoneStairs : BaseStairAddon
    {
        [Constructable]
        public DarkStoneStairs(Facing f, int levels)
            : base(f, 0x0788, 0x0789 + (int)f, levels)
        { }

        public DarkStoneStairs(Serial serial)
            : base(serial)
        {
        }

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

    public class BrickStairs : BaseStairAddon
    {
        [Constructable]
        public BrickStairs(Facing f, int levels)
            : base(f, 0x07A3, 0x07A4 + (int)f, levels)
        { }

        public BrickStairs(Serial serial)
            : base(serial)
        {
        }

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