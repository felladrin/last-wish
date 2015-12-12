using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Guilds;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Khazman.Utilities
{
    public class GuildAdminGump : Gump
    {
        public enum Page
        {
            Information,
            Guilds,
            GuildInfo,
            GuildMemberList,
            Alliances,
            AllianceDetails,
            Wars,
            WarDetails
        }

        private const string Version = "1.2.1";

        private Mobile m_From;
        private Page m_PageType;
        private int m_ListPage;
        private object m_State;

        private List<Guild> m_List;
        private Dictionary<AllianceInfo, List<Guild>> m_AllianceTable;
        private Dictionary<Guild, WarDeclaration> m_WarTable;

        private const int LabelColor = 0x7FFF;
        private const int SelectedColor = 0x421F;
        private const int DisabledColor = 0x4210;

        private const int LabelColor32 = 0xFFFFFF;
        private const int SelectedColor32 = 0x8080FF;
        private const int DisabledColor32 = 0x808080;

        private const int LabelHue = 0x480;
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;

        public static void Initialize()
        {
            CommandSystem.Register("GuildAdmin", AccessLevel.GameMaster, new CommandEventHandler(GuildAdmin_OnCommand));
        }

        [Usage("GuildAdmin")]
        [Description("Opens the Guild Administration Gump.")]
        public static void GuildAdmin_OnCommand(CommandEventArgs args)
        {
            args.Mobile.SendGump(new GuildAdminGump(args.Mobile, Page.Information, 0, null, null, null));
        }

        #region Collection Population
        private void PopulateGuildList()
        {
            if (m_List != null)
                m_List.Clear();
            else
                m_List = new List<Guild>();

            foreach (KeyValuePair<int, BaseGuild> kvp in Guild.List)
            {
                if (kvp.Value is Guild)
                    m_List.Add((Guild)kvp.Value);
            }

            m_List.Sort(GuildComparer.Instance);
        }

        private void PopulateAllianceTable()
        {
            if (m_AllianceTable != null)
                m_AllianceTable.Clear();
            else
                m_AllianceTable = new Dictionary<AllianceInfo, List<Guild>>();

            foreach (KeyValuePair<string, AllianceInfo> kvp in AllianceInfo.Alliances)
            {
                if (kvp.Value != null)
                {
                    List<Guild> memberList = new List<Guild>();

                    foreach (BaseGuild bg in Guild.List.Values)
                    {
                        Guild g = (Guild)bg;

                        if (kvp.Value.IsMember(g) && !memberList.Contains(g))
                        {
                            memberList.Add(g);
                        }
                    }

                    memberList.Sort(GuildComparer.Instance);
                    m_AllianceTable.Add(kvp.Value, memberList);
                }
            }
        }

        private void PopulateWarTable()
        {
            if (m_WarTable != null)
                m_WarTable.Clear();
            else
                m_WarTable = new Dictionary<Guild, WarDeclaration>();

            foreach (KeyValuePair<int, BaseGuild> kvp in Guild.List)
            {
                if (kvp.Value is Guild && ((Guild)kvp.Value).AcceptedWars != null)
                {
                    Guild g = (Guild)kvp.Value;

                    for (int i = 0; i < g.AcceptedWars.Count; i++)
                    {
                        if (!m_WarTable.ContainsKey(g) && !m_WarTable.ContainsValue(g.AcceptedWars[i]))
                        {
                            if (!m_WarTable.ContainsKey(g.AcceptedWars[i].Guild) && !m_WarTable.ContainsKey(g.AcceptedWars[i].Opponent))
                                m_WarTable.Add(g, g.AcceptedWars[i]);
                        }
                    }
                }
            }
        }
        #endregion

        #region Gump Layout Helpers
        private void AddPageButton(int x, int y, int buttonID, string text, Page page, params Page[] subPages)
        {
            bool isSelection = (m_PageType == page);

            for (int i = 0; !isSelection && i < subPages.Length; i++)
                isSelection = (m_PageType == subPages[i]);

            AddSelectedButton(x, y, buttonID, text, isSelection);
        }

        private void AddSelectedButton(int x, int y, int buttonID, string text, bool isSelection)
        {
            AddButton(x, y, 4005, 4007, buttonID, GumpButtonType.Reply, 0);
            AddHtml(x + 35, y, 200, 20, Color(text, isSelection ? SelectedColor32 : LabelColor32), false, false);
        }

        private void AddButtonLabeled(int x, int y, int buttonID, string text)
        {
            AddButton(x, y - 1, 4005, 4007, buttonID, GumpButtonType.Reply, 0);
            AddHtml(x + 35, y, 240, 20, Color(text, LabelColor32), false, false);
        }

        private string Center(string text)
        {
            return String.Format("<CENTER>{0}</CENTER>", text);
        }

        private string Color(string text, int color)
        {
            return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
        }

        private void AddBlackAlpha(int x, int y, int width, int height)
        {
            AddImageTiled(x, y, width, height, 2624);
            AddAlphaRegion(x, y, width, height);
        }

        private int GetButtonID(int type, int index)
        {
            return (1 + (index * 10) + type);
        }

        private void AddTextField(int x, int y, int width, int height, int index)
        {
            AddBackground(x - 2, y - 2, width + 4, height + 4, 0x2486);
            AddTextEntry(x + 2, y + 2, width - 4, height - 4, 0, index, "");
        }

        private void AddGuildHeader()
        {
            AddTextField(200, 20, 200, 20, 0);
            AddButtonLabeled(200, 50, GetButtonID(4, 0), "Search by Name");
            AddButtonLabeled(200, 80, GetButtonID(4, 1), "Search by Abbreviation");
        }
        #endregion

        private string GetStatus(WarStatus status)
        {
            string str = "";

            switch (status)
            {
                case WarStatus.InProgress: str = "In Progress"; break;
                case WarStatus.Win: str = "Winning / Won"; break;
                case WarStatus.Lose: str = "Losing / Lost"; break;
                case WarStatus.Draw: str = "Draw"; break;
                case WarStatus.Pending: str = "Status Pending"; break;
            }

            return str;
        }

        private string GetRemainingTime(WarDeclaration warDecl)
        {
            TimeSpan timeRemaining = TimeSpan.Zero;

            if (warDecl.WarLength != TimeSpan.Zero && (warDecl.WarBeginning + warDecl.WarLength) > DateTime.Now)
                timeRemaining = (warDecl.WarBeginning + warDecl.WarLength) - DateTime.Now;

            return String.Format("{0:D2}:{1:mm}", timeRemaining.Hours, DateTime.MinValue + timeRemaining);
        }

        public GuildAdminGump(Mobile from, Page page, int listPage, string notice, List<Guild> list, object state)
            : base(50, 40)
        {
            from.CloseGump(typeof(GuildAdminGump));

            if (list == null)
                PopulateGuildList();
            else
                m_List = list;

            PopulateAllianceTable();
            PopulateWarTable();

            m_From = from;
            m_PageType = page;
            m_ListPage = listPage;
            m_State = state;

            AddPage(0);
            AddBackground(0, 0, 420, 440, 5054);

            AddBlackAlpha(10, 10, 170, 100);
            AddBlackAlpha(190, 10, 220, 100);
            AddBlackAlpha(10, 120, 400, 260);
            AddBlackAlpha(10, 390, 400, 40);

            if (!Guild.NewGuildSystem)
            {
                AddLabel(20, 130, LabelHue, "This menu does not support old guild systems.");
                return;
            }

            AddPageButton(10, 10, GetButtonID(0, 0), "INFORMATION", Page.Information);
            AddPageButton(10, 30, GetButtonID(0, 1), "GUILD LIST", Page.Guilds, Page.GuildInfo);
            AddPageButton(10, 50, GetButtonID(0, 2), "ALLIANCES", Page.Alliances, Page.AllianceDetails);

            if (notice != null)
                AddHtml(12, 392, 396, 36, Color(notice, LabelColor32), false, false);

            switch (page)
            {
                case Page.Information:
                    {
                        AddGuildHeader();

                        AddLabel(20, 130, LabelHue, "GuildAdmin Version:");
                        AddLabel(150, 130, LabelHue, Version);

                        AddLabel(20, 150, LabelHue, "Registration Fee:");
                        AddLabel(150, 150, LabelHue, Guild.RegistrationFee.ToString("#,0") + " gp");

                        AddLabel(20, 170, LabelHue, "Total Guilds:");
                        AddLabel(150, 170, LabelHue, m_List.Count.ToString());

                        AddLabel(20, 190, LabelHue, " Active Alliances:");
                        AddLabel(150, 190, LabelHue, m_AllianceTable.Count.ToString());

                        AddLabel(20, 210, LabelHue, " Active Wars:");
                        AddLabel(150, 210, LabelHue, m_WarTable.Count.ToString());

                        break;
                    }
                case Page.Guilds:
                    {
                        AddGuildHeader();

                        if (m_List == null)
                        {
                            AddHtml(12, 140, 250, 60, Color("There was a problem building the list of guilds on the server. This page cannot be displayed.", LabelColor32), false, false);
                            break;
                        }

                        AddLabelCropped(12, 120, 81, 20, LabelHue, "Guild ID");
                        AddLabelCropped(95, 120, 81, 20, LabelHue, "Abbreviaton");
                        AddLabelCropped(178, 120, 172, 20, LabelHue, "Name");

                        if (listPage > 0)
                            AddButton(375, 122, 0x15E3, 0x15E7, GetButtonID(1, 0), GumpButtonType.Reply, 0);
                        else
                            AddImage(375, 122, 0x25EA);

                        if ((listPage + 1) * 12 < m_List.Count)
                            AddButton(392, 122, 0x15E1, 0x15E5, GetButtonID(1, 1), GumpButtonType.Reply, 0);
                        else
                            AddImage(392, 122, 0x25E6);

                        if (m_List.Count == 0)
                            AddLabel(12, 140, LabelHue, "There are no guilds to display.");

                        for (int i = 0, index = (listPage * 12); i < 12 && index >= 0 && index < m_List.Count; i++, index++)
                        {
                            Guild g = m_List[index];

                            if (g == null)
                                continue;

                            int offset = (140 + (i * 20));

                            AddLabelCropped(12, offset, 81, 20, LabelHue, g.Id.ToString());
                            AddLabelCropped(95, offset, 81, 20, LabelHue, g.Abbreviation);
                            AddLabelCropped(178, offset, 172, 20, LabelHue, g.Name);

                            AddButton(380, offset - 1, 0xFA5, 0xFA7, GetButtonID(4, index + 2), GumpButtonType.Reply, 0);
                        }

                        break;
                    }
                case Page.GuildInfo:
                    {
                        Guild g = state as Guild;

                        if (g == null)
                            break;

                        AddGuildHeader();
                        AddHtml(10, 125, 400, 20, Color(Center("Guild Information"), LabelColor32), false, false);

                        int y = 146;

                        AddLabel(20, y, LabelHue, "Name:");
                        AddLabel(200, y, LabelHue, g.Name);
                        y += 20;

                        AddLabel(20, y, LabelHue, "Abbreviation:");
                        AddLabel(200, y, LabelHue, g.Abbreviation);
                        y += 20;

                        AddLabel(20, y, LabelHue, "Guild Leader:");
                        if (g.Leader.Account != null)
                            AddLabelCropped(200, y, 150, 20, LabelHue, String.Format("{0} [{1}]", g.Leader.RawName,
                                ((Server.Accounting.Account)g.Leader.Account).Username));
                        else
                            AddLabelCropped(200, y, 150, 20, LabelHue, g.Leader.RawName);
                        y += 20;

                        AddLabel(20, y, LabelHue, "Active Members:");
                        AddLabel(200, y, LabelHue, g.Members.Count.ToString());
                        y += 44;

                        AddButtonLabeled(20, y, GetButtonID(7, 0), "Disband");
                        AddButtonLabeled(200, y, GetButtonID(7, 1), "Active Alliance");
                        y += 20;

                        AddButtonLabeled(20, y, GetButtonID(7, 2), "Member List");
                        AddButtonLabeled(200, y, GetButtonID(7, 3), "Active Wars");
                        y += 20;

                        AddButtonLabeled(20, y, GetButtonID(7, 4), "Add Member");
                        AddButtonLabeled(200, y, GetButtonID(7, 5), "Guild Properties");

                        break;
                    }
                case Page.GuildMemberList:
                    {
                        Guild g = state as Guild;

                        if (g == null)
                            break;

                        AddGuildHeader();
                        AddLabelCropped(12, 120, 120, 20, LabelHue, "Player Name");
                        AddLabelCropped(132, 120, 120, 20, LabelHue, "Account Username");
                        AddLabelCropped(252, 120, 120, 20, LabelHue, "Status");

                        if (listPage > 0)
                            AddButton(375, 122, 0x15E3, 0x15E7, GetButtonID(1, 0), GumpButtonType.Reply, 0);
                        else
                            AddImage(375, 122, 0x25EA);

                        if ((listPage + 1) * 12 < g.Members.Count)
                            AddButton(392, 122, 0x15E1, 0x15E5, GetButtonID(1, 1), GumpButtonType.Reply, 0);
                        else
                            AddImage(392, 122, 0x25E6);

                        if (g.Members.Count == 0)
                            AddLabel(12, 140, LabelHue, "This guild has no members.");

                        for (int i = 0, index = (listPage * 12); i < 12 && index >= 0 && index < g.Members.Count; i++, index++)
                        {
                            Mobile m = g.Members[index];

                            if (m == null || m.Account == null)
                                continue;

                            int offset = (140 + (i * 20));

                            AddLabelCropped(12, offset, 120, 20, LabelHue, m.RawName);
                            AddLabelCropped(132, offset, 120, 20, LabelHue, ((Server.Accounting.Account)m.Account).Username);

                            if (m.NetState != null)
                                AddLabelCropped(252, offset, 120, 20, 0x40, "Online");
                            else
                                AddLabelCropped(252, offset, 120, 20, 0x20, "Offline");
                        }

                        break;
                    }
                case Page.Alliances:
                    {
                        AddGuildHeader();

                        if (m_AllianceTable == null)
                        {
                            AddHtml(12, 140, 250, 60, Color("There was a problem building the table of alliances. This page cannot be displayed.", LabelColor32), false, false);
                            break;
                        }

                        AddLabelCropped(12, 120, 170, 20, LabelHue, "Name");
                        AddLabelCropped(184, 120, 81, 20, LabelHue, "Member Count");
                        AddLabelCropped(291, 120, 61, 20, LabelHue, "Leader");

                        if (listPage > 0)
                            AddButton(375, 122, 0x15E3, 0x15E7, GetButtonID(1, 0), GumpButtonType.Reply, 0);
                        else
                            AddImage(375, 122, 0x25EA);

                        if ((listPage + 1) * 12 < m_AllianceTable.Count)
                            AddButton(392, 122, 0x15E1, 0x15E5, GetButtonID(1, 1), GumpButtonType.Reply, 0);
                        else
                            AddImage(392, 122, 0x25E6);

                        if (m_AllianceTable.Count == 0)
                            AddLabel(12, 140, LabelHue, "There are no alliances to display.");

                        List<AllianceInfo> allianceList = new List<AllianceInfo>(m_AllianceTable.Keys);

                        for (int i = 0, index = (listPage * 12); i < 12 && index >= 0 && index < allianceList.Count; i++, index++)
                        {
                            AllianceInfo info = allianceList[index];

                            if (info == null)
                                continue;

                            List<Guild> tempList = null;
                            m_AllianceTable.TryGetValue(info, out tempList);

                            int offset = (140 + (i * 20));

                            AddLabelCropped(12, offset, 170, 20, LabelHue, info.Name);
                            AddLabelCropped(204, offset, 61, 20, LabelHue, (tempList == null ? "N/A" : tempList.Count.ToString()));
                            AddLabelCropped(291, offset, 61, 20, LabelHue, info.Leader.Abbreviation);

                            AddButton(380, (offset - 1), 0xFA5, 0xFA7, GetButtonID(5, index), GumpButtonType.Reply, 0);
                        }

                        break;
                    }
                case Page.AllianceDetails:
                    {
                        AllianceInfo info = state as AllianceInfo;

                        if (info == null || !m_AllianceTable.ContainsKey(info))
                            break;

                        AddGuildHeader();
                        AddHtml(10, 125, 400, 20, Color(Center("Alliance Details"), LabelColor32), false, false);

                        int y = 146;

                        AddLabel(20, y, LabelHue, "Name:");
                        AddLabel(200, y, LabelHue, info.Name);
                        y += 20;

                        AddLabel(20, y, LabelHue, "Leader:");
                        AddLabelCropped(200, y, 180, 20, LabelHue, String.Format("[{0}] {1}", info.Leader.Abbreviation, info.Leader.Name));
                        y += 20;

                        AddLabel(20, y, LabelHue, "Member Count:");
                        AddLabel(200, y, LabelHue, m_AllianceTable[info].Count.ToString());
                        y += 20;

                        AddLabel(20, y, LabelHue, "Status:");
                        AddLabel(200, y, LabelHue, (m_AllianceTable[info].Count < 2 ? "Pending Acceptance" : "Active"));
                        y += 20;

                        y = 270;

                        AddButtonLabeled(20, y, GetButtonID(8, 0), "Disband");
                        AddButtonLabeled(200, y, GetButtonID(8, 1), "Member List");

                        break;
                    }
                case Page.Wars:
                    {
                        Guild g = state as Guild;

                        if (g == null)
                            break;

                        AddGuildHeader();

                        AddLabelCropped(12, 120, 120, 20, LabelHue, "Guild Name");
                        AddLabelCropped(134, 120, 120, 20, LabelHue, "Opponent Guild");
                        AddLabelCropped(246, 120, 90, 20, LabelHue, "Date Started");

                        if (listPage > 0)
                            AddButton(375, 122, 0x15E3, 0x15E7, GetButtonID(1, 0), GumpButtonType.Reply, 0);
                        else
                            AddImage(375, 122, 0x25EA);

                        if ((listPage + 1) * 12 < g.AcceptedWars.Count)
                            AddButton(392, 122, 0x15E1, 0x15E5, GetButtonID(1, 1), GumpButtonType.Reply, 0);
                        else
                            AddImage(392, 122, 0x25E6);

                        if (g.AcceptedWars.Count == 0)
                            AddLabel(12, 140, LabelHue, "This guild has not accepted any war declarations.");

                        for (int i = 0, index = (listPage * 12); i < 12 && index >= 0 && index < g.AcceptedWars.Count; i++, index++)
                        {
                            WarDeclaration warDecl = g.AcceptedWars[index];

                            if (warDecl == null)
                                continue;

                            int offset = (140 + (i * 20));

                            AddLabelCropped(12, offset, 120, 20, LabelHue, g.Name);
                            AddLabelCropped(134, offset, 120, 20, LabelHue, warDecl.Opponent.Name);
                            AddLabelCropped(246, offset, 120, 20, LabelHue, warDecl.WarBeginning.ToShortDateString());

                            AddButton(380, (offset - 1), 0xFA5, 0xFA7, GetButtonID(6, index), GumpButtonType.Reply, 0);
                        }

                        break;
                    }
                case Page.WarDetails:
                    {
                        WarDeclaration warDecl = state as WarDeclaration;

                        if (warDecl == null)
                            break;

                        AddGuildHeader();
                        AddHtml(10, 125, 400, 20, Color(Center(String.Format("War Details for {0}", warDecl.Guild.Abbreviation)), LabelColor32), false, false);

                        int y = 146;

                        AddLabel(20, y, LabelHue, "Current Status:");
                        AddLabel(200, y, LabelHue, GetStatus(warDecl.Status));
                        y += 20;

                        AddLabel(20, y, LabelHue, "Initiated at:");
                        AddLabel(200, y, LabelHue, warDecl.WarBeginning.ToShortTimeString() + " on " + warDecl.WarBeginning.ToShortDateString());
                        y += 40;

                        AddHtml(10, y, 400, 20, Color(Center("Conditions of War:"), LabelColor32), false, false);
                        y += 30;

                        AddLabel(20, y, LabelHue, "Kills [Current/Max]:");
                        AddLabel(200, y, LabelHue, String.Format("{0}/{1}", warDecl.Kills.ToString(), warDecl.MaxKills.ToString()));
                        y += 20;

                        AddLabel(20, y, LabelHue, "Time [Remaining/Length]:");
                        AddLabel(200, y, LabelHue, String.Format("{0}/{1}", GetRemainingTime(warDecl), String.Format("{0:D2}:{1:mm}", warDecl.WarLength.Hours, DateTime.MinValue + warDecl.WarLength)));

                        y = 290;

                        AddButtonLabeled(20, y, GetButtonID(9, 0), "Guild Details");
                        AddButtonLabeled(200, y, GetButtonID(9, 1), "Opponent Details");

                        break;
                    }
            }
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            int val = (info.ButtonID - 1);

            if (val < 0)
                return;

            int type = (val % 10);
            int index = (val / 10);

            switch (type)
            {
                case 0:
                    {
                        Page page;

                        switch (index)
                        {
                            case 0: page = Page.Information; break;
                            case 1: page = Page.Guilds; break;
                            case 2: page = Page.Alliances; break;
                            default: return;
                        }

                        m_From.SendGump(new GuildAdminGump(m_From, page, 0, null, null, null));
                    } break;
                case 1: //change page
                    {
                        switch (index)
                        {
                            case 0:
                                {
                                    if (m_List != null && m_ListPage > 0)
                                        m_From.SendGump(new GuildAdminGump(m_From, m_PageType, (m_ListPage - 1), null, null, m_State));
                                } break;
                            case 1:
                                {
                                    if (m_List != null)
                                        m_From.SendGump(new GuildAdminGump(m_From, m_PageType, (m_ListPage + 1), null, null, m_State));
                                } break;
                        }
                    } break;
                case 4: //search or show guild details
                    {
                        switch (index)
                        {
                            case 0:
                            case 1:
                                {
                                    bool nameSearch = (index == 0);

                                    List<Guild> foundList = new List<Guild>();

                                    TextRelay relay = info.GetTextEntry(0);
                                    string toFind = (relay == null ? null : relay.Text.Trim().ToLower());
                                    string notice = null;

                                    if (toFind == null || toFind.Length == 0)
                                    {
                                        notice = String.Format("You must enter {0} to search for.", nameSearch ? "a guild name" : "a guild abbreviation");
                                    }
                                    else
                                    {
                                        foreach (KeyValuePair<int, BaseGuild> kvp in Guild.List)
                                        {
                                            bool isMatch = false;

                                            if (nameSearch)
                                            {
                                                if (kvp.Value.Name.ToLower().IndexOf(toFind) >= 0)
                                                    isMatch = true;
                                            }
                                            else
                                            {
                                                if (kvp.Value.Abbreviation.ToLower().IndexOf(toFind) >= 0)
                                                    isMatch = true;
                                            }

                                            if (isMatch && kvp.Value is Guild)
                                                foundList.Add((Guild)kvp.Value);
                                        }

                                        foundList.Sort(GuildComparer.Instance);
                                    }

                                    if (foundList.Count == 1)
                                        m_From.SendGump(new GuildAdminGump(m_From, Page.GuildInfo, 0, "One match found.", null, foundList[0]));
                                    else
                                        m_From.SendGump(new GuildAdminGump(m_From, Page.Guilds, 0, (notice == null ? (foundList.Count == 0 ? "Nothing matched your search terms." : null) : notice), foundList, null));

                                    break;
                                }
                            default:
                                {
                                    index -= 2;

                                    if (m_List != null && index >= 0 && index < m_List.Count)
                                    {
                                        Guild g = m_List[index];

                                        if (g == null)
                                            break;

                                        m_From.SendGump(new GuildAdminGump(m_From, Page.GuildInfo, 0, null, null, g));
                                    }

                                    break;
                                }
                        }
                    } break;
                case 5: //show alliance details
                    {
                        List<AllianceInfo> allianceList = new List<AllianceInfo>(m_AllianceTable.Keys);

                        if (allianceList != null && index >= 0 && index < allianceList.Count)
                        {
                            AllianceInfo aInfo = allianceList[index];

                            if (aInfo == null)
                                break;

                            m_From.SendGump(new GuildAdminGump(m_From, Page.AllianceDetails, 0, null, null, aInfo));
                        }
                    } break;
                case 6: //show war details
                    {
                        if (m_State is Guild)
                            m_From.SendGump(new GuildAdminGump(m_From, Page.WarDetails, 0, null, null, ((Guild)m_State).AcceptedWars[index]));
                    } break;
                case 7: //show guild details
                    {
                        switch (index)
                        {
                            case 0: //disband
                                {
                                    string warning = String.Format("You are about to disband the guild \"{0}.\" Are you sure you want to do this?", ((Guild)m_State).Name);
                                    m_From.SendGump(new WarningGump(1060635, 30720, warning, 0xFFC000, 420, 200, new WarningGumpCallback(DisbandGuild_Callback), m_State));

                                    break;
                                }
                            case 1: //alliance details
                                {
                                    if (((Guild)m_State).Alliance == null)
                                        m_From.SendGump(new GuildAdminGump(m_From, Page.GuildInfo, 0, "This guild is not a member of an alliance.", null, m_State));
                                    else
                                        m_From.SendGump(new GuildAdminGump(m_From, Page.AllianceDetails, 0, null, null, ((Guild)m_State).Alliance));

                                    break;
                                }
                            case 2: //member list
                                {
                                    m_From.SendGump(new GuildAdminGump(m_From, Page.GuildMemberList, 0, null, null, (Guild)m_State));

                                    break;
                                }
                            case 3: //war list
                                {
                                    if (((Guild)m_State).AcceptedWars == null || ((Guild)m_State).AcceptedWars.Count == 0)
                                        m_From.SendGump(new GuildAdminGump(m_From, Page.GuildInfo, 0, "This guild is not involved in any wars.", null, m_State));
                                    else
                                        m_From.SendGump(new GuildAdminGump(m_From, Page.Wars, 0, null, null, m_State));

                                    break;
                                }
                            case 4: //add member
                                {
                                    m_From.Target = new InternalRecruitTarget((Guild)m_State);
                                    m_From.SendMessage("Select the player to recruit into \"{0}.\"", ((Guild)m_State).Name);

                                    m_From.SendGump(new GuildAdminGump(m_From, m_PageType, 0, null, null, m_State));

                                    break;
                                }
                            case 5: //guild properties [same as the GuildProps command]
                                {
                                    m_From.SendGump(new PropertiesGump(m_From, (Guild)m_State));
                                    m_From.SendGump(new GuildInfoGump((Server.Mobiles.PlayerMobile)m_From, (Guild)m_State));

                                    m_From.SendGump(new GuildAdminGump(m_From, m_PageType, 0, null, null, m_State));

                                    break;
                                }
                        }
                    } break;
                case 8: //alliance details
                    {
                        switch (index)
                        {
                            case 0: //disband
                                {
                                    string warning = String.Format("You are about to disband the alliance \"{0}.\" Are you sure you want to do this?", ((AllianceInfo)m_State).Name);
                                    m_From.SendGump(new WarningGump(1060635, 30720, warning, 0xFFC000, 420, 200, new WarningGumpCallback(DisbandAlliance_Callback), m_State));

                                    break;
                                }
                            case 1: //member list
                                {
                                    AllianceInfo aInfo = m_State as AllianceInfo;

                                    if (m_AllianceTable.ContainsKey(aInfo))
                                    {
                                        m_From.SendGump(new GuildAdminGump(m_From, Page.Guilds, 0, null, m_AllianceTable[aInfo], null));
                                    }
                                    else
                                    {
                                        m_From.SendGump(new GuildAdminGump(m_From, m_PageType, 0, "The list of members for this alliance could not be found.", null, m_State));
                                    }

                                    break;
                                }
                        }
                    } break;
                case 9: //war details
                    {
                        switch (index)
                        {
                            case 0: m_From.SendGump(new GuildAdminGump(m_From, Page.GuildInfo, 0, null, null, ((WarDeclaration)m_State).Guild)); break;
                            case 1: m_From.SendGump(new GuildAdminGump(m_From, Page.GuildInfo, 0, null, null, ((WarDeclaration)m_State).Opponent)); break;
                        }
                    } break;
            }
        }

        #region WarningGump Callbacks
        private static void DisbandGuild_Callback(Mobile from, bool okay, object state)
        {
            string notice = "";

            if (okay)
            {
                ((Guild)state).Disband();

                notice = String.Format("\"{0}\" has been disbanded.", ((Guild)state).Name);
            }
            else
            {
                notice = String.Format("You have chosen not to disband \"{0}.\"", ((Guild)state).Name);
            }

            from.SendGump(new GuildAdminGump(from, (okay ? Page.Guilds : Page.GuildInfo), 0, notice, null, (okay ? null : state)));
        }

        private static void DisbandAlliance_Callback(Mobile from, bool okay, object state)
        {
            string notice = "";

            if (okay)
            {
                ((AllianceInfo)state).Disband();

                notice = String.Format("The alliance, \"{0},\" has been disbanded.", ((AllianceInfo)state).Name);
            }
            else
            {
                notice = String.Format("You have chosen not to disband \"{0}.\"", ((AllianceInfo)state).Name);
            }

            from.SendGump(new GuildAdminGump(from, (okay ? Page.Alliances : Page.AllianceDetails), 0, notice, null, (okay ? null : state)));
        }
        #endregion

        private class InternalRecruitTarget : Target
        {
            private Guild m_Guild;

            public InternalRecruitTarget(Guild guild)
                : base(16, false, TargetFlags.None)
            {
                m_Guild = guild;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is PlayerMobile)
                {
                    PlayerMobile m = targeted as PlayerMobile;

                    if (!m.Alive)
                    {
                        from.SendLocalizedMessage(501162); // Only the living may be recruited.
                    }
                    else if (m_Guild.IsMember(m))
                    {
                        from.SendLocalizedMessage(501163); // They are already a guildmember!
                    }
                    else if (m_Guild.Candidates.Contains(m))
                    {
                        from.SendLocalizedMessage(501164); // They are already a candidate.
                    }
                    else if (m_Guild.Accepted.Contains(m))
                    {
                        from.SendLocalizedMessage(501165); // They have already been accepted for membership, and merely need to use the Guildstone to gain full membership.
                    }
                    else if (m.Guild != null)
                    {
                        from.SendLocalizedMessage(501166); // You can only recruit candidates who are not already in a guild.
                    }
                    else
                    {
                        m_Guild.AddMember(m);

                        from.SendMessage("{0} has been recruited into the guild, \"{1}.\"", m.RawName, m_Guild.Name);
                        m.SendMessage("You have been recruited into the guild, \"{0}!\"", m_Guild.Name);
                    }
                }
                else
                {
                    from.SendMessage("Only players can belong to a guild!");
                }
            }
        }
    }

    public class GuildComparer : IComparer<Guild>
    {
        public static readonly IComparer<Guild> Instance = new GuildComparer();

        public int Compare(Guild x, Guild y)
        {
            if (x == null && y == null)
                return 0;
            else if (x == null)
                return -1;
            else if (y == null)
                return 1;

            if (x.Id == y.Id)
                return 0;
            else if (x.Id > y.Id)
                return 1;
            else if (x.Id < y.Id)
                return -1;
            else
                return Insensitive.Compare(x.Name, y.Name);
        }
    }
}