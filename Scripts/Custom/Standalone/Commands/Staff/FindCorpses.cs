//Original Version 1 - 12/27/2004 By Igon.   Updated to Version 1.1 by Shai'Tan Malkier 10/6/2007.

using System;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using Server.Items;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class FindCorpsesGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("FindCorpses", AccessLevel.GameMaster, new CommandEventHandler(FindCorpses_OnCommand));
        }

        [Usage("FindCorpses")]
        [Description("Target a player to find all of their corpses")]
        public static void FindCorpses_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
                e.Mobile.Target = new playerTarget();
                e.Mobile.SendMessage("Whose corpse would you like to find?");
            }
        }

        public static bool OldStyle = PropsConfig.OldStyle;

        public static readonly int GumpOffsetX = PropsConfig.GumpOffsetX;
        public static readonly int GumpOffsetY = PropsConfig.GumpOffsetY;

        public static readonly int TextHue = PropsConfig.TextHue;
        public static readonly int TextOffsetX = PropsConfig.TextOffsetX;

        public static readonly int OffsetGumpID = PropsConfig.OffsetGumpID;
        public static readonly int HeaderGumpID = PropsConfig.HeaderGumpID;
        public static readonly int EntryGumpID = PropsConfig.EntryGumpID;
        public static readonly int BackGumpID = PropsConfig.BackGumpID;
        public static readonly int SetGumpID = PropsConfig.SetGumpID;

        public static readonly int SetWidth = PropsConfig.SetWidth;
        public static readonly int SetOffsetX = PropsConfig.SetOffsetX, SetOffsetY = PropsConfig.SetOffsetY;
        public static readonly int SetButtonID1 = PropsConfig.SetButtonID1;
        public static readonly int SetButtonID2 = PropsConfig.SetButtonID2;

        public static readonly int PrevWidth = PropsConfig.PrevWidth;
        public static readonly int PrevOffsetX = PropsConfig.PrevOffsetX, PrevOffsetY = PropsConfig.PrevOffsetY;
        public static readonly int PrevButtonID1 = PropsConfig.PrevButtonID1;
        public static readonly int PrevButtonID2 = PropsConfig.PrevButtonID2;

        public static readonly int NextWidth = PropsConfig.NextWidth;
        public static readonly int NextOffsetX = PropsConfig.NextOffsetX, NextOffsetY = PropsConfig.NextOffsetY;
        public static readonly int NextButtonID1 = PropsConfig.NextButtonID1;
        public static readonly int NextButtonID2 = PropsConfig.NextButtonID2;

        public static readonly int OffsetSize = PropsConfig.OffsetSize;

        public static readonly int EntryHeight = PropsConfig.EntryHeight;
        public static readonly int BorderSize = PropsConfig.BorderSize;

        private static bool PrevLabel = false, NextLabel = false;

        private static readonly int PrevLabelOffsetX = PrevWidth + 1;
        private static readonly int PrevLabelOffsetY = 0;

        private static readonly int NextLabelOffsetX = -29;
        private static readonly int NextLabelOffsetY = 0;

        private static readonly int EntryWidth = 180;
        private static readonly int EntryCount = 15;

        private static readonly int TotalWidth = OffsetSize + EntryWidth + OffsetSize + SetWidth + OffsetSize;
        private static readonly int TotalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (EntryCount + 1));

        private static readonly int BackWidth = BorderSize + TotalWidth + BorderSize;
        private static readonly int BackHeight = BorderSize + TotalHeight + BorderSize;

        private Mobile m_Owner;
        private Mobile m_Targeted;
        private ArrayList m_Mobiles;

        private int m_Page;
        public class playerTarget : Target
        {
            public playerTarget()
                : base(-1, true, TargetFlags.None)
            {
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (from is PlayerMobile && targeted is PlayerMobile)
                {
                    Mobile m = (Mobile)targeted;
                    from.SendGump(new FindCorpsesGump(from, m));
                }
                else if (from is PlayerMobile && targeted is Corpse)
                {
                    if (((Corpse)targeted).Owner is PlayerMobile)
                    {
                        Mobile m = ((Corpse)targeted).Owner;
                        from.SendGump(new FindCorpsesGump(from, m));
                    }
                }
            }
        }

        public enum SortDirection
        {
            Ascending,
            Descending
        }
        public class InternalComparer : IComparer
        {
            private SortDirection m_direction = SortDirection.Ascending;
            public InternalComparer() : base() { }
            public InternalComparer(SortDirection direction)
            {
                this.m_direction = direction;
            }

            int IComparer.Compare(object x, object y)
            {
                Corpse corpseX = (Corpse)x;
                Corpse corpseY = (Corpse)y;

                if (corpseX == null && corpseY == null)
                {
                    return 0;
                }
                else if (corpseX == null && corpseY != null)
                {
                    return (this.m_direction == SortDirection.Ascending) ? -1 : 1;
                }
                else if (corpseX != null && corpseY == null)
                {
                    return (this.m_direction == SortDirection.Ascending) ? 1 : -1;
                }
                else
                {
                    return (this.m_direction == SortDirection.Ascending) ?
                    corpseX.TimeOfDeath.CompareTo(corpseY.TimeOfDeath) :
                    corpseY.TimeOfDeath.CompareTo(corpseX.TimeOfDeath);
                }
            }
        }

        public FindCorpsesGump(Mobile owner, Mobile targeted)
            : this(owner, BuildList(owner, targeted), 0)
        {
        }

        public FindCorpsesGump(Mobile owner, ArrayList list, int page)
            : base(GumpOffsetX, GumpOffsetY)
        {
            owner.CloseGump(typeof(FindCorpsesGump));

            m_Owner = owner;
            m_Mobiles = list;
            if (m_Mobiles.Count > 0)
            {
                Initialize(page);
            }
            else
            {
                owner.SendMessage("That player has no corpses.");
            }
        }

        public static ArrayList BuildList(Mobile owner, Mobile targeted)
        {
            ArrayList m_Corpse = new ArrayList();

            foreach (Item item in World.Items.Values)
            {
                if (item is Corpse)
                {
                    Corpse c = ((Corpse)item);
                    Container pack = ((Container)item);
                    int totalitems = pack.TotalItems;
                    if (c.Owner == targeted)
                    {
                        m_Corpse.Add(c);
                    }
                }
            }
            m_Corpse.Sort(new InternalComparer(SortDirection.Descending));
            return m_Corpse;
        }

        public void Initialize(int page)
        {
            m_Page = page;
            int count = m_Mobiles.Count - (page * EntryCount);

            if (count < 0)
                count = 0;
            else if (count > EntryCount)
                count = EntryCount;

            int totalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (count + 1));

            AddPage(0);

            AddBackground(0, 0, BackWidth, BorderSize + totalHeight + BorderSize + 20, BackGumpID);
            AddImageTiled(BorderSize, BorderSize, TotalWidth - (OldStyle ? SetWidth + OffsetSize : 0), totalHeight, OffsetGumpID);

            int x = BorderSize + OffsetSize;
            int y = BorderSize + OffsetSize;

            int emptyWidth = TotalWidth - PrevWidth - NextWidth - (OffsetSize * 4) - (OldStyle ? SetWidth + OffsetSize : 0);

            if (!OldStyle)
                AddImageTiled(x - (OldStyle ? OffsetSize : 0), y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0), EntryHeight, EntryGumpID);

            AddLabel(x + TextOffsetX, y, TextHue, String.Format("Page {0} of {1} ({2})", page + 1, (m_Mobiles.Count + EntryCount - 1) / EntryCount, m_Mobiles.Count));

            x += emptyWidth + OffsetSize;

            if (OldStyle)
                AddImageTiled(x, y, TotalWidth - (OffsetSize * 3) - SetWidth, EntryHeight, HeaderGumpID);
            else
                AddImageTiled(x, y, PrevWidth, EntryHeight, HeaderGumpID);

            if (page > 0)
            {
                AddButton(x + PrevOffsetX, y + PrevOffsetY, PrevButtonID1, PrevButtonID2, 1, GumpButtonType.Reply, 0);

                if (PrevLabel)
                    AddLabel(x + PrevLabelOffsetX, y + PrevLabelOffsetY, TextHue, "Previous");
            }

            x += PrevWidth + OffsetSize;

            if (!OldStyle)
                AddImageTiled(x, y, NextWidth, EntryHeight, HeaderGumpID);

            if ((page + 1) * EntryCount < m_Mobiles.Count)
            {
                AddButton(x + NextOffsetX, y + NextOffsetY, NextButtonID1, NextButtonID2, 2, GumpButtonType.Reply, 1);

                if (NextLabel)
                    AddLabel(x + NextLabelOffsetX, y + NextLabelOffsetY, TextHue, "Next");
            }
            //Item Count Start
            AddLabel(5, BorderSize + totalHeight + BorderSize - 12, 1153, String.Format("Items"));
            //Item Count Stop

            //Goto & Get Player Start
            AddLabel(10, BorderSize + totalHeight + BorderSize + 0, 88, String.Format("Player"));

            AddButton(55, BorderSize + totalHeight + BorderSize + 2, 1209, 1210, 3, GumpButtonType.Reply, 0);
            AddLabel(72, BorderSize + totalHeight + BorderSize + 0, 1153, String.Format("Goto"));

            AddButton(105, BorderSize + totalHeight + BorderSize + 2, 1209, 1210, 4, GumpButtonType.Reply, 0);
            AddLabel(122, BorderSize + totalHeight + BorderSize + 0, 1153, "Get");
            //Goto & Get Player Stop

            //Goto & Get Corpse Label Start
            AddLabel(158, BorderSize + totalHeight + BorderSize - 12, 1153, String.Format("Goto"));
            AddLabel(195, BorderSize + totalHeight + BorderSize - 12, 1153, String.Format("Get"));
            //Goto & Get Corpse Label Stop

            //m_Owner is the Mobile using [FindCorpse command

            for (int i = 0, index = page * EntryCount; i < EntryCount && index < m_Mobiles.Count; ++i, ++index)
            {
                x = BorderSize + OffsetSize;
                y += EntryHeight + OffsetSize;

                Corpse m = (Corpse)m_Mobiles[index];
                m_Targeted = m.Owner;
                AddImageTiled(x, y, EntryWidth, EntryHeight, EntryGumpID);//White background behind Name

                if (!m.Deleted)
                {
                    AddLabelCropped(x + TextOffsetX, y, EntryWidth - TextOffsetX, EntryHeight, GetHueFor(m.Owner), m.Deleted ? "(deleted)" : (m.TotalItems - 1) + " : " + m.Owner.Name.ToString());
                    //Start Goto Corpse Button
                    AddButton(x + 160, y + SetOffsetY, 1209, 1210, 100000 + i + 10, GumpButtonType.Reply, 0);
                    //Stop Goto Corpse Button

                    x += EntryWidth + OffsetSize;

                    if (SetGumpID != 0)
                        AddImageTiled(x, y, SetWidth, EntryHeight, SetGumpID);
                    //Start Get Corpse Button
                    AddButton(x + SetOffsetX, y + SetOffsetY, 1209, 1210, i + 10, GumpButtonType.Reply, 0);
                    //Stop Get Corpse Button
                }
            }
        }
        private static int GetHueFor(Mobile m)
        {
            switch (m.AccessLevel)
            {
                case AccessLevel.Administrator: return 0x516;
                case AccessLevel.Seer: return 0x144;
                case AccessLevel.GameMaster: return 0x21;
                case AccessLevel.Counselor: return 0x2;
                case AccessLevel.Player:
                default:
                    {
                        if (m.Kills >= 5)
                            return 0x21;
                        else if (m.Criminal)
                            return 0x3B1;

                        return 0x58;
                    }
            }
        }
        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: // Closed
                    {
                        return;
                    }
                case 1: // Previous
                    {
                        if (m_Page > 0)
                            from.SendGump(new FindCorpsesGump(from, m_Mobiles, m_Page - 1));

                        break;
                    }
                case 2: // Next
                    {
                        if ((m_Page + 1) * EntryCount < m_Mobiles.Count)
                            from.SendGump(new FindCorpsesGump(from, m_Mobiles, m_Page + 1));

                        break;
                    }
                case 3: // Goto Player
                    {
                        Map map = m_Targeted.Map;
                        Point3D loc = m_Targeted.Location;

                        if (map == null || map == Map.Internal)
                        {
                            map = m_Targeted.LogoutMap;
                            loc = m_Targeted.LogoutLocation;
                            from.SendGump(new FindCorpsesGump(from, m_Mobiles, m_Page));
                        }

                        if (map != null && map != Map.Internal)
                        {
                            from.MoveToWorld(loc, map);
                            from.SendMessage("You have been teleported to their location.");
                            from.SendGump(new FindCorpsesGump(from, m_Mobiles, m_Page));
                        }

                        break;
                    }
                case 4: // Get Player
                    {
                        Map map = from.Map;
                        Point3D loc = from.Location;

                        if (map != null && map != Map.Internal)
                        {
                            m_Targeted.MoveToWorld(loc, map);
                            from.SendMessage("They have been teleported to your location");
                            from.SendGump(new FindCorpsesGump(from, m_Mobiles, m_Page));
                        }

                        break;
                    }

                default:
                    {
                        int index;

                        if (info.ButtonID < 100000)//Get Corpse
                        {
                            index = (m_Page * EntryCount) + (info.ButtonID - 10);
                            Corpse m1 = (Corpse)m_Mobiles[index];
                            Map map = from.Map;
                            Point3D loc = from.Location;

                            if (map != null && map != Map.Internal)
                            {
                                m1.MoveToWorld(loc, map);
                                from.SendMessage("You have retrieved their corpse.");
                                from.SendGump(new FindCorpsesGump(from, m_Mobiles, m_Page));
                            }
                        }

                        else if (info.ButtonID >= 100000)//Goto Corpse
                        {
                            index = (m_Page * EntryCount) + (info.ButtonID - 10) - 100000;
                            Corpse m1 = (Corpse)m_Mobiles[index];
                            Map map = m1.Map;
                            Point3D loc = m1.Location;

                            if (map != null && map != Map.Internal)
                            {
                                from.MoveToWorld(loc, map);
                                from.SendMessage("You have been teleported to their corpse.");
                                from.SendGump(new FindCorpsesGump(from, m_Mobiles, m_Page));
                            }
                        }
                        break;
                    }
            }
        }
    }
}