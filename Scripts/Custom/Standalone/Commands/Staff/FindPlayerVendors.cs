/************************************************
*              PlayerVendorSys v1.0             *
*                Distro files: None             *
*       Made by Sidious at Renovatio UO         *
*  Contact Sidious: admin.renovatio@hotmail.com *
*                  02/08/2006                   *
*                                               *
*      Anyone can modify/redistribute this      *
*       DO NOT REMOVE/CHANGE THIS HEADER!       *
*************************************************/

using System;
using System.Collections;
using Server.Network;
using Server.Accounting;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{
    public class FindPlayerVendorsGump : Gump
    {
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;

        public static void Initialize()
        {
            CommandSystem.Register("FindPlayerVendors", AccessLevel.GameMaster, new CommandEventHandler(FindPlayerVendors_OnCommand));
        }

        [Usage("FindPlayerVendors")]
        [Description("Lists all player vendors in the world.")]
        public static void FindPlayerVendors_OnCommand(CommandEventArgs e)
        {
            ArrayList list = new ArrayList();

            foreach (Mobile mob in World.Mobiles.Values)
            {
                if (mob is PlayerVendor)
                {
                    PlayerVendor pv = mob as PlayerVendor;
                    list.Add(pv);
                }
            }
            e.Mobile.SendGump(new FindPlayerVendorsGump(e.Mobile, list, 1));
        }
        private ArrayList m_List;
        private int m_Page;
        private Mobile m_From;

        public void AddBlackAlpha(int x, int y, int width, int height)
        {
            AddImageTiled(x, y, width, height, 2624);
            AddAlphaRegion(x, y, width, height);
        }

        public FindPlayerVendorsGump(Mobile from, ArrayList list, int page)
            : base(50, 40)
        {
            from.CloseGump(typeof(FindPlayerVendorsGump));
            int pvs = 0;
            m_Page = page;
            m_From = from;
            int pageCount = 0;
            m_List = list;

            AddPage(0);
            AddBackground(0, 0, 645, 325, 3500);
            AddBlackAlpha(20, 20, 604, 277);

            if (m_List == null)
            {
                return;
            }
            else
            {
                pvs = list.Count;
                if (list.Count % 12 == 0)
                {
                    pageCount = (list.Count / 12);
                }
                else
                {
                    pageCount = (list.Count / 12) + 1;
                }
            }

            AddLabelCropped(32, 20, 100, 20, 1152, "Shop Name");
            AddLabelCropped(202, 20, 120, 20, 1152, "Owner");
            AddLabelCropped(305, 20, 120, 20, 1152, "Account");
            AddLabelCropped(415, 20, 120, 20, 1152, "Location");
            AddLabel(27, 298, 32, String.Format("Made By Sidious    Version 1.0.0                       There are {0} vendors in the world.", pvs));

            if (page > 1)
                AddButton(573, 22, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0);
            else
                AddImage(573, 22, 0x25EA);

            if (pageCount > page)
                AddButton(590, 22, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0);
            else
                AddImage(590, 22, 0x25E6);

            if (m_List.Count == 0)
                AddLabel(180, 115, 1152, ".....::: There are no Vendors in world :::.....");

            if (page == pageCount)
            {
                for (int i = (page * 12) - 12; i < pvs; ++i)
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
            try
            {
                if (index < m_List.Count)
                {
                    int btn;
                    int row;
                    btn = (index) + 101;
                    row = index % 12;
                    PlayerVendor pv = m_List[index] as PlayerVendor;
                    Account a = pv.Owner.Account as Account;

                    AddLabel(32, 46 + (row * 20), 1152, String.Format("{0}", pv.ShopName));
                    AddLabel(202, 46 + (row * 20), 1152, String.Format("{0}", pv.Owner.Name));
                    AddLabel(415, 46 + (row * 20), 1152, String.Format("{0} {1}", pv.Location.ToString(), pv.Map));

                    AddButton(585, 51 + (row * 20), 2437, 2438, btn, GumpButtonType.Reply, 0);
                    if (pv == null)
                    {
                        Console.WriteLine("No Vendors In Shard...");
                        return;
                    }
                    else if (pv.Owner == null)
                        AddLabel(305, 46 + (row * 20), RedHue, String.Format("UnOwned"));
                    else if (a.Banned)
                        AddLabel(305, 46 + (row * 20), RedHue, String.Format("{0} ( Banned )", pv.Owner.Account));
                    else if (pv.Owner.NetState == null)
                        AddLabel(305, 46 + (row * 20), RedHue, String.Format("{0}", pv.Owner.Account));
                    else if (pv.Owner.NetState != null)
                        AddLabel(305, 46 + (row * 20), GreenHue, String.Format("{0}", pv.Owner.Account));
                    else
                        AddLabel(305, 46 + (row * 20), RedHue, String.Format("{0}", pv.Owner.Account));
                }
            }
            catch { }
        }
        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            int buttonID = info.ButtonID;
            if (buttonID == 2)
            {
                m_Page++;
                from.CloseGump(typeof(FindPlayerVendorsGump));
                from.SendGump(new FindPlayerVendorsGump(from, m_List, m_Page));
            }
            if (buttonID == 1)
            {
                m_Page--;
                from.CloseGump(typeof(FindPlayerVendorsGump));
                from.SendGump(new FindPlayerVendorsGump(from, m_List, m_Page));
            }
            if (buttonID > 100)
            {
                int index = buttonID - 101;
                PlayerVendor pv = m_List[index] as PlayerVendor;
                Point3D xyz = pv.Location;
                int x = xyz.X;
                int y = xyz.Y;
                int z = xyz.Z;

                Point3D dest = new Point3D(x, y, z);
                from.MoveToWorld(dest, pv.Map);
                from.SendGump(new FindPlayerVendorsGump(from, m_List, m_Page));

            }
        }
    }
}