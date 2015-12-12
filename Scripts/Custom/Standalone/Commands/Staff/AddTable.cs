namespace Server.Commands
{
    public class AddTable
    {
        public static void Initialize()
        {
            CommandSystem.Register("AddTable", AccessLevel.GameMaster, new CommandEventHandler(AddTable_OnCom));
        }

        [Usage("AddTable")]
        [Description("Allows user to create a table.")]
        private static void AddTable_OnCom(CommandEventArgs e)
        {
            BoundingBoxPicker.Begin(e.Mobile, new BoundingBoxCallback(AddTable_Callback), 0);
        }

        public static void AddTable_Callback(Mobile m, Map map, Point3D start, Point3D end, object state)
        {
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

                    if (xcord == end.X)
                    {
                        if (ycord == start.Y)
                            item.ItemID = 0xB72;
                        else if (ycord == end.Y)
                            item.ItemID = 0xB71;
                        else
                            item.ItemID = 0xB73;
                    }
                    else if (ycord == end.Y)
                    {
                        if (xcord == start.X)
                            item.ItemID = 0xB70;
                        else
                            item.ItemID = 0xB74;
                    }
                    else
                        item.ItemID = 0xB73;

                    item.Movable = false;
                    item.MoveToWorld(new Point3D(xcord, ycord, start.Z), map);
                }
            }
        }
    }
}
