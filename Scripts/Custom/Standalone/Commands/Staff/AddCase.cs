namespace Server.Commands
{
    public class AddCase
    {
        public static void Initialize()
        {
            CommandSystem.Register("AddCase", AccessLevel.GameMaster, new CommandEventHandler(AddTable_OnCom));
        }

        [Usage("AddCase")]
        [Description("Allows user to create a display case.")]
        private static void AddTable_OnCom(CommandEventArgs e)
        {
            BoundingBoxPicker.Begin(e.Mobile, new BoundingBoxCallback(AddCase_Callback), 0);
        }

        public static void AddCase_Callback(Mobile m, Map map, Point3D start, Point3D end, object state)
        {
            int height = end.Y - start.Y + 1;
            int width = end.X - start.X + 1;

            // Height = 1
            // Top = 0xB02
            // Bottom = 0xB00
            // Center = 0xB01
            // Rail
            // Top = 0xAFF
            // Bottom = 0xAFD
            // Center = 0xAFE

            // Width = 1
            // Top = 0xB08
            // Bottom = 0xB06
            // Center = 0xB07
            // Rail
            // Top = 0xB05
            // Bottom = 0xB03
            // Center = 0xB04

            Item shelf;
            Item rail = null;

            for (int x = 0; x < width; x++)
            {
                int xcord = start.X + x;

                if (height == 1)
                {
                    shelf = new Item();
                    rail = new Item();

                    if (xcord == start.X)
                    {
                        shelf.ItemID = 0xB02;
                        rail.ItemID = 0xAFF;
                    }
                    else if (xcord == end.X)
                    {
                        shelf.ItemID = 0xB00;
                        rail.ItemID = 0xAFD;
                    }
                    else
                    {
                        shelf.ItemID = 0xB01;
                        rail.ItemID = 0xAFE;
                    }

                    shelf.Movable = rail.Movable = false;
                    shelf.MoveToWorld(new Point3D(xcord, start.Y, start.Z), map);
                    rail.MoveToWorld(new Point3D(xcord, start.Y, start.Z + 3), map);
                }
                else
                {
                    for (int y = 0; y < height; y++)
                    {
                        int ycord = start.Y + y;

                        shelf = new Item();
                        if (width == 1)
                        {
                            rail = new Item();
                            if (ycord == start.Y)
                            {
                                shelf.ItemID = 0xB08;
                                rail.ItemID = 0xB05;
                            }
                            else if (ycord == end.Y)
                            {
                                shelf.ItemID = 0xB06;
                                rail.ItemID = 0xB03;
                            }
                            else
                            {
                                shelf.ItemID = 0xB07;
                                rail.ItemID = 0xB04;
                            }
                        }
                        else
                        {
                            if (xcord == start.X)
                            {
                                rail = new Item();
                                if (ycord == start.Y)
                                {
                                    shelf.ItemID = 0xB10;
                                    rail.ItemID = 0xAA3;
                                }
                                else if (ycord == end.Y)
                                {
                                    shelf.ItemID = 0xB12;
                                    rail.ItemID = 0xAA5;
                                }
                                else
                                {
                                    shelf.ItemID = 0xB16;
                                    rail.ItemID = 0xAA1;
                                }
                            }
                            else if (ycord == start.Y)
                            {
                                rail = new Item();
                                if (xcord == end.X)
                                {
                                    shelf.ItemID = 0xB13;
                                    rail.ItemID = 0xAA4;
                                }
                                else
                                {
                                    shelf.ItemID = 0xB17;
                                    rail.ItemID = 0xAA2;
                                }
                            }
                            else if (xcord == end.X)
                            {
                                rail = new Item();
                                if (ycord == end.Y)
                                {
                                    shelf.ItemID = 0xB11;
                                    rail.ItemID = 0xB18;
                                }
                                else
                                {
                                    shelf.ItemID = 0xB14;
                                    rail.ItemID = 0xA9F;
                                }
                            }
                            else if (ycord == end.Y)
                            {
                                rail = new Item();
                                shelf.ItemID = 0xB15;
                                rail.ItemID = 0xAA0;
                            }
                            else
                                shelf.ItemID = 0xB0F;
                        }


                        shelf.Movable = false;
                        shelf.MoveToWorld(new Point3D(xcord, ycord, start.Z), map);

                        if (rail != null)
                        {
                            rail.Movable = false;
                            rail.MoveToWorld(new Point3D(xcord, ycord, start.Z + 3), map);
                            rail = null;
                        }
                    }
                }
            }
        }
    }
}
