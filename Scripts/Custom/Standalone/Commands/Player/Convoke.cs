//   ___|========================|___
//   \  |  Written by Felladrin  |  /
//    > |     October 2013       | <
//   /__|========================|__\

using System;
using System.Collections.Generic;
using Server.Commands;
using Server.Gumps;
using Server.Network;
using Server.Spells;

namespace Server.Gumps
{
    public class ConvokeGumpList : Gump
    {
        public static class Config
        {
            public static TimeSpan UseDelay = TimeSpan.FromMinutes(30); // How long players need to wait before using the command again?
        }

        public static void Initialize()
        {
            CommandSystem.Register("Convoke", AccessLevel.Player, new CommandEventHandler(OnConvokeCommand));
        }

        private static Dictionary<int, DateTime> LastUsed = new Dictionary<int, DateTime>();

        [Usage("Convoke [filter]")]
        [Description("Convokes a player to join you.")]
        private static void OnConvokeCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            int key = from.Serial.Value;

            if (SpellHelper.CheckCombat(from))
			{
                from.SendMessage(38, "You can't do this while in middle a combat.");
				return;
			}

            if (LastUsed.ContainsKey(key) && (DateTime.Now - Config.UseDelay) < LastUsed[key])
            {
                TimeSpan timeToWait = Config.UseDelay - (DateTime.Now - LastUsed[key]);
                from.SendMessage(38, "You still need to wait {0:N0} seconds before convoking another player.", timeToWait.TotalSeconds);
                return;
            }

            from.CloseGump(typeof(ConvokeGumpList));
            from.SendGump(new ConvokeGumpList(from, e.ArgString));
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
        private List<Mobile> m_Mobiles;
        private int m_Page;

        private class InternalComparer : IComparer<Mobile>
        {
            public static readonly IComparer<Mobile> Instance = new InternalComparer();

            public InternalComparer()
            {
            }

            public int Compare(Mobile x, Mobile y)
            {
                if (x == null || y == null)
                    throw new ArgumentException();

                if (x.AccessLevel > y.AccessLevel)
                    return -1;
                else if (x.AccessLevel < y.AccessLevel)
                    return 1;
                else
                    return Insensitive.Compare(x.Name, y.Name);
            }
        }

        public ConvokeGumpList(Mobile owner, string filter)
            : this(owner, BuildList(owner, filter), 0)
        {
        }

        public ConvokeGumpList(Mobile owner, List<Mobile> list, int page)
            : base(GumpOffsetX, GumpOffsetY)
        {
            owner.CloseGump(typeof(ConvokeGumpList));

            m_Owner = owner;
            m_Mobiles = list;

            Initialize(page);
        }

        public static List<Mobile> BuildList(Mobile owner, string filter)
        {
            if (filter != null && (filter = filter.Trim()).Length == 0)
                filter = null;
            else
                filter = filter.ToLower();

            List<Mobile> list = new List<Mobile>();
            List<NetState> states = NetState.Instances;

            for (int i = 0; i < states.Count; ++i)
            {
                Mobile m = states[i].Mobile;

                if (m != null)
                {
                    if (filter != null && (m.Name == null || m.Name.ToLower().IndexOf(filter) < 0))
                        continue;

                    if (m == owner || m.AccessLevel > AccessLevel.Player || !m.Alive)
                        continue;

                    list.Add(m);
                }
            }

            list.Sort(InternalComparer.Instance);

            return list;
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

            AddBackground(0, 0, BackWidth, BorderSize + totalHeight + BorderSize, BackGumpID);
            AddImageTiled(BorderSize, BorderSize, TotalWidth - (OldStyle ? SetWidth + OffsetSize : 0), totalHeight, OffsetGumpID);

            int x = BorderSize + OffsetSize;
            int y = BorderSize + OffsetSize;

            int emptyWidth = TotalWidth - PrevWidth - NextWidth - (OffsetSize * 4) - (OldStyle ? SetWidth + OffsetSize : 0);

            if (!OldStyle)
                AddImageTiled(x - (OldStyle ? OffsetSize : 0), y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0), EntryHeight, EntryGumpID);

            AddLabel(x + TextOffsetX, y, TextHue, String.Format("Convoke who? (Page {1}/{2})", m_Mobiles.Count, page + 1, (m_Mobiles.Count + EntryCount - 1) / EntryCount));

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

            for (int i = 0, index = page * EntryCount; i < EntryCount && index < m_Mobiles.Count; ++i, ++index)
            {
                x = BorderSize + OffsetSize;
                y += EntryHeight + OffsetSize;

                Mobile m = m_Mobiles[index];

                AddImageTiled(x, y, EntryWidth, EntryHeight, EntryGumpID);
                AddLabelCropped(x + TextOffsetX, y, EntryWidth - TextOffsetX, EntryHeight, GetHueFor(m), m.Deleted ? "(deleted)" : m.Name);

                x += EntryWidth + OffsetSize;

                if (SetGumpID != 0)
                    AddImageTiled(x, y, SetWidth, EntryHeight, SetGumpID);

                if (m.NetState != null && !m.Deleted)
                    AddButton(x + SetOffsetX, y + SetOffsetY, SetButtonID1, SetButtonID2, i + 3, GumpButtonType.Reply, 0);
            }
        }

        private static int GetHueFor(Mobile m)
        {
            switch (m.AccessLevel)
            {
                case AccessLevel.Owner:
                case AccessLevel.Developer:
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
                            from.SendGump(new ConvokeGumpList(from, m_Mobiles, m_Page - 1));

                        break;
                    }
                case 2: // Next
                    {
                        if ((m_Page + 1) * EntryCount < m_Mobiles.Count)
                            from.SendGump(new ConvokeGumpList(from, m_Mobiles, m_Page + 1));

                        break;
                    }
                default:
                    {
                        int index = (m_Page * EntryCount) + (info.ButtonID - 3);

                        if (index >= 0 && index < m_Mobiles.Count)
                        {
                            Mobile m = m_Mobiles[index];

                            if (m.Deleted)
                            {
                                from.SendMessage("That player has deleted their character.");
                                from.SendGump(new ConvokeGumpList(from, m_Mobiles, m_Page));
                            }
                            else if (m.NetState == null)
                            {
                                from.SendMessage("That player is no longer online.");
                                from.SendGump(new ConvokeGumpList(from, m_Mobiles, m_Page));
                            }
                            else
                            {
                                m.SendGump(new ConvokedGump(from));
                                object[] arg = new object[] { m };
                                Timer.DelayCall(TimeSpan.FromMinutes(2.0), new TimerStateCallback(CloseConvokedGump), arg);

                                from.SendMessage(68, "Invitation sent to {0}.", m.Name);
                                LastUsed[from.Serial.Value] = DateTime.Now;
                            }
                        }

                        break;
                    }
            }
        }

        public static void CloseConvokedGump(object state)
        {
            object[] states = (object[])state;
            Mobile m = (Mobile)states[0];
            m.CloseGump(typeof(ConvokedGump));
        }
    }

    public class ConvokedGump : Gump
    {
        private Mobile m_Summoner;

        public ConvokedGump(Mobile from)
            : base(0, 0)
        {
            m_Summoner = from;
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(180, 57, 396, 100, 2600);
            AddImage(264, 35, 1419);
            AddImageTiled(338, 34, 86, 43, 2601);
            AddLabel(355, 47, 0, "Invitation");
            AddLabel(278, 76, 0, String.Format("You are being convoked by {0}.", from.Name));
            AddLabel(290, 90, 0, "Do you want to join them?");
            AddButton(304, 113, 247, 248, 1, GumpButtonType.Reply, 0);
            AddButton(385, 114, 243, 241, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            if (info.ButtonID == 1)
            {
                from.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
                from.MoveToWorld(m_Summoner.Location, m_Summoner.Map);
                from.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
            }
        }
    }
}