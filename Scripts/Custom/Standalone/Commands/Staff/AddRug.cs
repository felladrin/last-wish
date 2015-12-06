using System;
using Server.Targeting;

namespace Server.Commands
{
    public enum RugType
    {
        None,
        Blue,
        Red,
        DRed,
        GBlue,
        GRed,
        RBlue
    }

    public class AddRug
    {
        public class Rug
        {
            public int Top, Bottom, Left, Right, North, South, West, East, Center;

            public Rug(int top, int bottom, int left, int right, int north, int south, int west, int east, int center)
            {
                Top = top;
                Bottom = bottom;
                Left = left;
                Right = right;
                North = north;
                South = south;
                West = west;
                East = east;
                Center = center;
            }

            public static Rug Red = new Rug(0xACA, 0xAC9, 0xACB, 0xACC, 0xACE, 0xAD0, 0xACD, 0xACF, 0xAC8);
            public static Rug Blue = new Rug(0xAC3, 0xAC2, 0xAC4, 0xAC5, 0xAF7, 0xAF9, 0xAF6, 0xAF8, 0xABE);
            public static Rug DRed = new Rug(0xAE4, 0xAE3, 0xAE5, 0xAE6, 0xAE8, 0xAEA, 0xAE7, 0xAE9, 0xAEB);
            public static Rug GRed = new Rug(0xADC, 0xADB, 0xADD, 0xADE, 0xAE0, 0xAE2, 0xADF, 0xAE1, 0xADA);
            public static Rug GBlue = new Rug(0xAD3, 0xAD2, 0xAD4, 0xAD5, 0xAD7, 0xAD9, 0xAD6, 0xAD8, 0xAD1);
            public static Rug RBlue = new Rug(0xAEF, 0xAEE, 0xAF0, 0xAF1, 0xAF3, 0xAF5, 0xAF2, 0xAF4, 0xAEC);

            public static Rug GetRug(RugType typ)
            {
                switch (typ)
                {
                    default:
                    case RugType.Red: return Red;
                    case RugType.Blue: return Blue;
                    case RugType.DRed: return DRed;
                    case RugType.GRed: return GRed;
                    case RugType.GBlue: return GBlue;
                    case RugType.RBlue: return RBlue;
                }
            }
        }

        public static void Initialize()
        {
            CommandSystem.Register("AddRug", AccessLevel.GameMaster, new CommandEventHandler(AddRug_OnCommand));
        }

        [Usage("AddRug <rug type>")]
        [Description("Allows a user to add a rug! Types: Blue, Red, DRed, GBlue, GRed, RBlue")]
        private static void AddRug_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            string arg = e.ArgString;

            RugType type = RugType.None;

            try
            {
                type = (RugType)Enum.Parse(typeof(RugType), arg, true);
            }
            catch
            {
            }

            if (type != RugType.None)
                GenRug(m, type);
            else
                m.SendMessage("You have not specified a rug type. Blue, Red, DRed, GBlue, GRed, RBlue");
        }

        private static void GenRug(Mobile m, RugType typ)
        {
            Rug rug = Rug.GetRug(typ);

            BoundingBoxPicker.Begin(m, new BoundingBoxCallback(AddRug_Callback), rug);
        }

        public static void AddRug_Callback(Mobile from, Map map, Point3D start, Point3D end, object state)
        {
            Rug tr = (Rug)state;

            int height = end.Y - start.Y + 1;
            int width = end.X - start.X + 1;

            Item item;

            for (int x = 0; x < width; x++)
            {
                int xcord = start.X + x;

                for (int y = 0; y < height; y++)
                {
                    int ycord = start.Y + y;

                    item = new Item();

                    if (xcord == start.X)
                    {
                        if (ycord == start.Y)
                            item.ItemID = tr.Top;
                        else if (ycord == end.Y)
                            item.ItemID = tr.Left;
                        else
                            item.ItemID = tr.West;
                    }
                    else if (ycord == start.Y)
                    {
                        if (xcord == end.X)
                            item.ItemID = tr.Right;
                        else
                            item.ItemID = tr.North;
                    }
                    else if (xcord == end.X)
                    {
                        if (ycord == end.Y)
                            item.ItemID = tr.Bottom;
                        else
                            item.ItemID = tr.East;
                    }
                    else if (ycord == end.Y)
                        item.ItemID = tr.South;
                    else
                        item.ItemID = tr.Center;

                    item.Movable = false;
                    item.MoveToWorld(new Point3D(xcord, ycord, start.Z), map);
                }
            }
        }
    }
}



