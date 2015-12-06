//  Written by Haazen June 2005
//  Edited by Busty in October 2005 to find houses

using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Prompts;
using Server.Multis;
using Server.Targeting;
using Server.Accounting;
using Server.Commands;

namespace Server.Gumps
{
    public class FindHouseGump : Gump
    {

        public static void Initialize()
        {
            CommandSystem.Register("FindHouse", AccessLevel.Counselor, new CommandEventHandler(FindHouse_OnCommand));
        }

        [Usage("FindHouse")]
        [Description("List all player houses of the world.")]
        public static void FindHouse_OnCommand(CommandEventArgs e)
        {
            ArrayList list = new ArrayList();

            foreach (Item item in World.Items.Values)
            {
                if (item is BaseHouse)
                {
                    BaseHouse House = item as BaseHouse;
                    list.Add(House);
                }
            }
            e.Mobile.SendGump(new FindHouseGump(e.Mobile, list, 1));
        }

        private ArrayList m_List;
        private int m_DefaultIndex;
        private int m_Page;
        private Mobile m_From;

        public void AddBlackAlpha(int x, int y, int width, int height)
        {
            AddImageTiled(x, y, width, height, 2624);
            AddAlphaRegion(x, y, width, height);
        }

        public FindHouseGump(Mobile from, ArrayList list, int page)
            : base(50, 40)
        {
            from.CloseGump(typeof(FindHouseGump));

            int Houses = 0;
            m_Page = page;
            m_From = from;
            int pageCount = 0;
            m_List = list;

            AddPage(0);

            AddBackground(0, 0, 520, 315, 5054);

            AddBlackAlpha(10, 10, 500, 280);

            if (m_List == null)
            {
                return;
            }
            else
            {
                Houses = list.Count;

                if (list.Count % 12 == 0)
                {
                    pageCount = (list.Count / 12);
                }
                else
                {
                    pageCount = (list.Count / 12) + 1;
                }
            }

            AddLabelCropped(32, 16, 120, 20, 1152, "Owner");
            AddLabelCropped(175, 16, 120, 20, 1152, "Account");
            AddLabelCropped(300, 16, 120, 20, 1152, "Where");
            AddLabel(80, 290, 93, String.Format("Haazen's House Locator      {0} Houses in the world.", Houses));

            if (page > 1)
                AddButton(470, 18, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0);
            else
                AddImage(470, 18, 0x25EA);

            if (pageCount > page)
                AddButton(487, 18, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0);
            else
                AddImage(487, 18, 0x25E6);

            if (m_List.Count == 0)
                AddLabel(135, 80, 1152, "There are not houses in the world.");

            if (page == pageCount)
            {
                for (int i = (page * 12) - 12; i < Houses; ++i)
                    AddDetails(i);
            }
            else
            {
                for (int i = (page * 12) - 12; i < page * 12; ++i)
                    AddDetails(i);
            }
        }

        private void AddDetails(int index)
        {
            string owner;
            if (index < m_List.Count)
            {
                try
                {
                    int btn;
                    int row;
                    btn = (index) + 101;
                    row = index % 12;

                    BaseHouse House = m_List[index] as BaseHouse;
                    Account acct = House.Owner.Account as Account;

                    Mobile houseOwner = House.Owner;
                    Point3D loc = House.GetWorldLocation();

                    Map map = House.Map;

                    if (houseOwner == null)
                        owner = "Nobody";
                    else
                        owner = houseOwner.Name;

                    AddLabel(32, 40 + (row * 20), 1152, String.Format("{0}", owner));
                    AddLabel(175, 40 + (row * 20), 1152, String.Format("{0}", acct));
                    AddLabel(300, 40 + (row * 20), 1152, String.Format("{0} {1}", loc, map));

                    AddButton(480, 45 + (row * 20), 2437, 2438, btn, GumpButtonType.Reply, 0);
                }
                catch { }

            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            int buttonID = info.ButtonID;
            if (buttonID == 2)
            {
                m_Page++;
                from.CloseGump(typeof(FindHouseGump));
                from.SendGump(new FindHouseGump(from, m_List, m_Page));
            }
            if (buttonID == 1)
            {
                m_Page--;
                from.CloseGump(typeof(FindHouseGump));
                from.SendGump(new FindHouseGump(from, m_List, m_Page));
            }
            if (buttonID > 100)
            {
                int index = buttonID - 101;
                BaseHouse House = m_List[index] as BaseHouse;
                Point3D xyz = House.GetWorldLocation();
                int x = xyz.X;
                int y = xyz.Y;
                int z = xyz.Z + 7;

                Point3D dest = new Point3D(x, y, z);
                from.MoveToWorld(dest, House.Map);
            }
        }
    }
}