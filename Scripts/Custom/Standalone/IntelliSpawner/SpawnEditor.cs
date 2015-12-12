/////////////////////////////////////
//          SPAWN EDITOR           //
// This SpawnEditor is a huge mod  //
// of "ZenArcher's SpawnEditor v2" //
//            By Nerun             //
/////////////////////////////////////

// Edited by Felladrin to work with IntelliSpawner

using System;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class SpawnEditorGump : Gump
    {
        private int m_page;
        private ArrayList m_tempList;
        public Item m_selIntelliSpawner;

        public int page
        {
            get { return m_page; }
            set { m_page = value; }
        }

        public Item selIntelliSpawner
        {
            get { return m_selIntelliSpawner; }
            set { m_selIntelliSpawner = value; }
        }

        public ArrayList tempList
        {
            get { return m_tempList; }
            set { m_tempList = value; }
        }

        public static void Initialize()
        {
            CommandSystem.Register("SpawnEditor", AccessLevel.GameMaster, new CommandEventHandler(SpawnEditor_OnCommand));
        }

        public static void Register(string command, AccessLevel access, CommandEventHandler handler)
        {
            CommandSystem.Register(command, access, handler);
        }

        [Usage("SpawnEditor")]
        [Description("Used to find and edit spawns")]
        public static void SpawnEditor_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            SpawnEditor_OnCommand(from);
        }

        public static void SpawnEditor_OnCommand(Mobile from)
        {
            ArrayList worldList = new ArrayList();
            ArrayList facetList = new ArrayList();

            Type type = ScriptCompiler.FindTypeByName("IntelliSpawner", true);

            if (type == typeof(Item) || type.IsSubclassOf(typeof(Item)))
            {
                bool isAbstract = type.IsAbstract;

                foreach (Item item in World.Items.Values)
                {
                    if (isAbstract ? item.GetType().IsSubclassOf(type) : item.GetType() == type)
                        worldList.Add(item);
                }
            }

            foreach (IntelliSpawner worldSpnr in worldList)
            {
                if (worldSpnr.Map == from.Map)
                    facetList.Add(worldSpnr);
            }

            SpawnEditor_OnCommand(from, 0, facetList);
        }

        public static void SpawnEditor_OnCommand(Mobile from, int page, ArrayList currentList)
        {
            SpawnEditor_OnCommand(from, page, currentList, 0);
        }

        public static void SpawnEditor_OnCommand(Mobile from, int page, ArrayList currentList, int selected)
        {
            SpawnEditor_OnCommand(from, page, currentList, selected, null);
        }

        public static void SpawnEditor_OnCommand(Mobile from, int page, ArrayList currentList, int selected, Item selIntelliSpawner)
        {
            from.SendGump(new SpawnEditorGump(from, page, currentList, selected, selIntelliSpawner));
        }

        public SpawnEditorGump(Mobile from, int page, ArrayList currentList, int selected, Item spwnr)
            : base(50, 40)
        {
            tempList = new ArrayList();
            Mobile m = from;
            m_page = page;
            Region r = from.Region;
            Map map = from.Map;
            int buttony = 60;
            int buttonID = 1;
            int listnum = 0;

            tempList = currentList;

            selIntelliSpawner = spwnr;

            AddPage(0);

            AddBackground(0, 0, 600, 450, 5054);
            AddImageTiled(8, 8, 584, 40, 2624);
            AddAlphaRegion(8, 8, 584, 40);
            AddImageTiled(8, 50, 250, 396, 2624);
            AddAlphaRegion(8, 50, 250, 396);
            AddImageTiled(260, 50, 332, 396, 2624);
            AddAlphaRegion(260, 50, 332, 396);
            AddLabel(220, 20, 52, "SPAWNER EDITOR");
            AddButton(550, 405, 0x158A, 0x158B, 10002, GumpButtonType.Reply, 1); //Quit Button
            AddButton(275, 412, 0x845, 0x846, 10008, GumpButtonType.Reply, 0);
            AddLabel(300, 410, 52, "Restart");

            if (currentList.Count == 0)
                AddLabel(50, 210, 52, "No IntelliSpawners Found");
            else
            {
                if (page == 0)
                {
                    if (currentList.Count < 15)
                        listnum = currentList.Count;
                    else
                        listnum = 15;

                    for (int x = 0; x < listnum; x++)
                    {
                        Item spawnr = null;

                        if (currentList[x] is Item)
                            spawnr = currentList[x] as Item;

                        string gumpMsg = "";

                        Point3D spawnr3D = new Point3D((new Point2D(spawnr.X, spawnr.Y)), spawnr.Z);
                        Region spawnrRegion = Region.Find(spawnr3D, map);

                        if (spawnrRegion.ToString() == "")
                            gumpMsg = "IntelliSpawner at " + spawnr.X.ToString() + ", " + spawnr.Y.ToString();
                        else
                            gumpMsg = spawnrRegion.ToString();

                        AddButton(25, buttony, 0x845, 0x846, buttonID, GumpButtonType.Reply, 0);
                        AddLabel(55, buttony, 52, gumpMsg);
                        buttony += 25;
                        buttonID += 1;
                    }
                }

                else if (page > 0)
                {
                    if (currentList.Count < 15 + (15 * page))
                        listnum = currentList.Count;
                    else
                        listnum = 15 + (15 * page);

                    for (int x = 15 * page; x < listnum; x++)
                    {
                        Item spawnr = null;
                        buttonID = x + 1;

                        if (currentList[x] is Item)
                            spawnr = currentList[x] as Item;

                        string gumpMsg = "";

                        Point3D spawnr3D = new Point3D((new Point2D(spawnr.X, spawnr.Y)), spawnr.Z);
                        Region spawnrRegion = Region.Find(spawnr3D, map);

                        if (spawnrRegion.ToString() == "")
                            gumpMsg = "IntelliSpawner at " + spawnr.X.ToString() + ", " + spawnr.Y.ToString();
                        else
                            gumpMsg = spawnrRegion.ToString();

                        AddButton(25, buttony, 0x845, 0x846, buttonID, GumpButtonType.Reply, 0);
                        AddLabel(55, buttony, 52, gumpMsg);
                        buttony += 25;
                    }
                }
            }

            if (page == 0 && currentList.Count > 15)
                AddButton(450, 20, 0x15E1, 0x15E5, 10000, GumpButtonType.Reply, 0);
            else if (page > 0 && currentList.Count > 15 + (page * 15))
                AddButton(450, 20, 0x15E1, 0x15E5, 10000, GumpButtonType.Reply, 0);

            if (page != 0)
                AddButton(150, 20, 0x15E3, 0x15E7, 10001, GumpButtonType.Reply, 0);

            int pageNum = (int)currentList.Count / 15;
            int rem = currentList.Count % 15;
            int totPages = 0;

            string stotPages = "";

            if (rem > 0)
            {
                totPages = pageNum + 1;
                stotPages = totPages.ToString();
            }
            else
                stotPages = pageNum.ToString();

            string pageText = "Page " + (page + 1) + " of " + stotPages;

            AddLabel(40, 20, 52, pageText);

            if (selected == 0)
                InitializeStartingRightPanel();
            else if (selected == 1)
                InitializeSelectedRightPanel();
        }

        public void InitializeStartingRightPanel()
        {
            AddLabel(275, 65, 52, "Filter to current region only");
            AddButton(500, 65, 0x15E1, 0x15E5, 10003, GumpButtonType.Reply, 0);

            AddTextField(275, 140, 50, 20, 0);
            AddLabel(275, 115, 52, "Filter by Distance");
            AddButton(500, 115, 0x15E1, 0x15E5, 10004, GumpButtonType.Reply, 0);

            AddTextField(275, 190, 120, 20, 1);
            AddLabel(275, 165, 52, "Search Spawners by Creature");
            AddButton(500, 165, 0x15E1, 0x15E5, 10009, GumpButtonType.Reply, 0);
        }

        public void InitializeSelectedRightPanel()
        {
            string spX = selIntelliSpawner.X.ToString();
            string spY = selIntelliSpawner.Y.ToString();
            string spnText = "IntelliSpawner at " + spX + ", " + spY;

            AddLabel(350, 65, 52, spnText);

            IntelliSpawner initSpn = selIntelliSpawner as IntelliSpawner;
            int strNum = 0;
            string spns = "Containing: ";
            string spnsNEW = "";
            string spns1 = "";
            string spns2 = "";
            string spns3 = "";

            for (int i = 0; i < initSpn.SpawnNames.Count; i++)
            {
                if (strNum == 0)
                {
                    if (i < initSpn.SpawnNames.Count - 1)
                    {
                        if (spns.Length + initSpn.SpawnNames[i].ToString().Length < 50)
                            spnsNEW += (string)initSpn.SpawnNames[i] + ", ";
                        else
                        {
                            strNum = 1;
                            spns1 += (string)initSpn.SpawnNames[i] + ", ";
                        }
                    }
                    else
                        spnsNEW += (string)initSpn.SpawnNames[i];
                }
                else if (strNum == 1)
                {
                    if (i < initSpn.SpawnNames.Count - 1)
                    {
                        if (spns1.Length + initSpn.SpawnNames[i].ToString().Length < 50)
                            spns1 += (string)initSpn.SpawnNames[i] + ", ";
                        else
                        {
                            strNum = 2;
                            spns2 += (string)initSpn.SpawnNames[i] + ", ";
                        }
                    }
                    else
                    {
                        if (spns1.Length + initSpn.SpawnNames[i].ToString().Length < 50)
                            spns1 += (string)initSpn.SpawnNames[i];
                        else
                        {
                            strNum = 3;
                            spns2 += (string)initSpn.SpawnNames[i];
                        }
                    }
                }
                else if (strNum == 2)
                {
                    if (i < initSpn.SpawnNames.Count - 1)
                    {
                        if (spns2.Length + initSpn.SpawnNames[i].ToString().Length < 50)
                            spns2 += (string)initSpn.SpawnNames[i] + ", ";
                        else
                        {
                            strNum = 3;
                            spns3 += (string)initSpn.SpawnNames[i] + ", ";
                        }
                    }
                    else
                    {
                        if (spns2.Length + initSpn.SpawnNames[i].ToString().Length < 50)
                            spns2 += (string)initSpn.SpawnNames[i];
                        else
                        {
                            strNum = 4;
                            spns3 += (string)initSpn.SpawnNames[i];
                        }
                    }
                }
                else if (strNum == 3)
                {
                    if (i < initSpn.SpawnNames.Count - 1)
                        spns3 += (string)initSpn.SpawnNames[i] + ", ";
                    else
                        spns3 += (string)initSpn.SpawnNames[i];
                }
            }

            AddLabel(275, 85, 52, spns);
            AddHtml(280, 110, 300, 200, spnsNEW, true, true);
            if (spns1 != "")
                AddLabel(275, 105, 200, spns1);

            if (spns2 != "")
                AddLabel(275, 125, 200, spns2);

            if (spns3 != "")
                AddLabel(275, 145, 200, spns3);

            AddLabel(320, 320, 52, "Go to IntelliSpawner");
            AddButton(525, 320, 0x15E1, 0x15E5, 10005, GumpButtonType.Reply, 1);
            AddLabel(320, 345, 52, "Delete Selected IntelliSpawner");
            AddButton(525, 345, 0x15E1, 0x15E5, 10006, GumpButtonType.Reply, 0);
            AddLabel(320, 370, 52, "Edit Spawns");
            AddButton(525, 370, 0x15E1, 0x15E5, 10007, GumpButtonType.Reply, 0);
        }

        public List<string> CreateArray(RelayInfo info, Mobile from)
        {
            List<string> creaturesName = new List<string>();

            for (int i = 0; i < 13; i++)
            {
                TextRelay te = info.GetTextEntry(i);

                if (te != null)
                {
                    string str = te.Text;

                    if (str.Length > 0)
                    {
                        str = str.Trim();

                        Type type = ScriptCompiler.FindTypeByName(str);

                        if (type != null)
                            creaturesName.Add(str);
                        else
                            AddLabel(70, 230, 39, "Invalid Search String");
                    }
                }
            }

            return creaturesName;
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            int buttonNum = 0;
            ArrayList currentList = new ArrayList(tempList);
            int page = m_page;

            if (info.ButtonID > 0 && info.ButtonID < 10000)
                buttonNum = 1;
            else if (info.ButtonID > 20004)
                buttonNum = 30000;
            else
                buttonNum = info.ButtonID;

            switch (buttonNum)
            {
                case 0:
                    {
                        //Close
                        break;
                    }
                case 1:
                    {
                        selIntelliSpawner = currentList[info.ButtonID - 1] as Item;
                        SpawnEditor_OnCommand(from, page, currentList, 1, selIntelliSpawner);
                        break;
                    }
                case 10000:
                    {
                        if (m_page * 10 < currentList.Count)
                        {
                            page = m_page += 1;
                            SpawnEditor_OnCommand(from, page, currentList);
                        }
                        break;
                    }
                case 10001:
                    {
                        if (m_page != 0)
                        {
                            page = m_page -= 1;
                            SpawnEditor_OnCommand(from, page, currentList);
                        }
                        break;
                    }
                case 10002:
                    {
                        //Close
                        break;
                    }
                case 10003:
                    {
                        FilterByRegion(from, tempList, from.Region, from.Map, page);
                        break;
                    }
                case 10004:
                    {
                        TextRelay oDis = info.GetTextEntry(0);
                        string sDis = (oDis == null ? "" : oDis.Text.Trim());
                        if (sDis != "")
                        {
                            try
                            {
                                int distance = Convert.ToInt32(sDis);
                                FilterByDistance(tempList, from, distance, page);
                            }
                            catch
                            {
                                from.SendMessage("Distance must be a number");
                                SpawnEditor_OnCommand(from, page, currentList);
                            }
                        }
                        else
                        {
                            from.SendMessage("You must specify a distance");
                            SpawnEditor_OnCommand(from, page, currentList);
                        }
                        break;
                    }
                case 10005:
                    {
                        from.Location = new Point3D(selIntelliSpawner.X, selIntelliSpawner.Y, selIntelliSpawner.Z);
                        SpawnEditor_OnCommand(from, page, currentList, 1, selIntelliSpawner);
                        break;
                    }
                case 10006:
                    {
                        selIntelliSpawner.Delete();
                        SpawnEditor_OnCommand(from);
                        break;
                    }
                case 10007:
                    {
                        from.SendGump(new IntelliSpawnerGump(selIntelliSpawner as IntelliSpawner));
                        SpawnEditor_OnCommand(from, page, currentList, 1, selIntelliSpawner);
                        break;
                    }
                case 10008:
                    {
                        SpawnEditor_OnCommand(from);
                        break;
                    }
                case 10009:
                    {
                        TextRelay oSearch = info.GetTextEntry(1);
                        string sSearch = (oSearch == null ? null : oSearch.Text.Trim());
                        SearchByName(tempList, from, sSearch, page);
                        break;
                    }
                case 20000:
                    {
                        IntelliSpawner spawner = selIntelliSpawner as IntelliSpawner;
                        spawner.SpawnNames = CreateArray(info, state.Mobile);
                        break;
                    }
                case 20001:
                    {
                        IntelliSpawner spawner = selIntelliSpawner as IntelliSpawner;
                        SpawnEditor_OnCommand(from, page, currentList, 2, selIntelliSpawner);
                        spawner.BringToHome();
                        break;
                    }
                case 20002:
                    {
                        IntelliSpawner spawner = selIntelliSpawner as IntelliSpawner;
                        SpawnEditor_OnCommand(from, page, currentList, 2, selIntelliSpawner);
                        spawner.Respawn();
                        break;
                    }
                case 20003:
                    {
                        IntelliSpawner spawner = selIntelliSpawner as IntelliSpawner;
                        SpawnEditor_OnCommand(from, page, currentList, 2, selIntelliSpawner);
                        state.Mobile.SendGump(new PropertiesGump(state.Mobile, spawner));
                        break;
                    }
                case 30000:
                    {
                        int buttonID = info.ButtonID - 20004;
                        int index = buttonID / 2;
                        int type = buttonID % 2;

                        IntelliSpawner spawner = selIntelliSpawner as IntelliSpawner;

                        TextRelay entry = info.GetTextEntry(index);

                        if (entry != null && entry.Text.Length > 0)
                        {
                            if (type == 0) // Spawn creature
                                spawner.Spawn(entry.Text);

                            spawner.SpawnNames = CreateArray(info, state.Mobile);
                        }

                        break;
                    }
            }
        }


        public static void FilterByRegion(Mobile from, ArrayList facetList, Region regr, Map regmap, int page)
        {
            ArrayList filregList = new ArrayList();

            foreach (Item regItem in facetList)
            {
                Point2D p2 = new Point2D(regItem.X, regItem.Y);
                Point3D p = new Point3D(p2, regItem.Z);

                if (Region.Find(p, regmap) == regr)
                    filregList.Add(regItem);
            }

            from.SendGump(new SpawnEditorGump(from, 0, filregList, 0, null));
        }

        public static void FilterByDistance(ArrayList currentList, Mobile m, int dis, int page)
        {
            ArrayList fildisList = new ArrayList();

            for (int z = 0; z < currentList.Count; z++)
            {
                Item disItem = currentList[z] as Item;

                if (disItem.X >= m.X - dis && disItem.X <= m.X + dis && disItem.Y >= m.Y - dis && disItem.Y <= m.Y + dis)
                    fildisList.Add(disItem);
            }

            m.SendGump(new SpawnEditorGump(m, 0, fildisList, 0, null));
        }

        public static void SearchByName(ArrayList currentList, Mobile from, string search, int page)
        {
            ArrayList searchList = new ArrayList();

            foreach (IntelliSpawner spn in currentList)
            {
                foreach (string str in spn.SpawnNames)
                {
                    if (str.ToLower().IndexOf(search) >= 0)
                        searchList.Add(spn);
                }
            }

            from.SendGump(new SpawnEditorGump(from, 0, searchList, 0, null));
        }

        public void AddTextField(int x, int y, int width, int height, int index)
        {
            AddBackground(x - 2, y - 2, width + 4, height + 4, 0x2486);
            AddTextEntry(x + 2, y + 2, width - 4, height - 4, 0, index, "");
        }
    }
}
