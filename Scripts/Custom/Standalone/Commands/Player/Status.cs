// created by BondD
using System;
using System.Diagnostics;
using System.Collections;
using System.Net;

using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using Server.Guilds;
using Server.Items;
using Server.Misc;
using Server.Commands;

namespace Server.Gumps
{
    public class StatusGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("Status", AccessLevel.Player, new CommandEventHandler(Status_OnCommand));
        }

        [Usage("Status")]
        [Description("Show the server status, including a list of all players online")]
        private static void Status_OnCommand(CommandEventArgs e)
        {
            e.Mobile.CloseGump(typeof(StatusGump));
            e.Mobile.SendGump(new StatusGump(e.Mobile, 0, null, null));
        }

        public void AddBlackAlpha(int x, int y, int width, int height)
        {
            AddImageTiled(x, y, width, height, 2624);
            AddAlphaRegion(x, y, width, height);
        }

        public static string FormatTimeSpan( TimeSpan ts )
		{
			return String.Format( "{0:D2}:{1:D2}:{2:D2}:{3:D2}", ts.Days, ts.Hours % 24, ts.Minutes % 60, ts.Seconds % 60 );
		}

        public static string FormatByteAmount(long totalBytes)
        {
            if (totalBytes > 1000000000)
                return String.Format("{0:F1} GB", (double)totalBytes / 1073741824);

            if (totalBytes > 1000000)
                return String.Format("{0:F1} MB", (double)totalBytes / 1048576);

            if (totalBytes > 1000)
                return String.Format("{0:F1} KB", (double)totalBytes / 1024);

            return String.Format("{0} Bytes", totalBytes);
        }

        private ArrayList m_List;
        private int m_ListPage;
        private int[] m_CountList;

        public StatusGump(Mobile from, int listPage, ArrayList list, int[] count)
            : base(140, 80)
        {
            from.CloseGump(typeof(StatusGump));

            m_List = list;
            m_ListPage = listPage;
            m_CountList = count;
            AddPage(0);

            AddBackground(0, 0, 800, 600, 0x53);

            AddImageTiled(15, 15, 770, 17, 5154);
            AddHtml(15, 15, 770, 17, "<div align=\"center\" color=\"2100\">" + ServerList.ServerName + " Status</div>", false, false);

            AddImageTiled(15, 37, 190, 17, 5154);
            AddLabel(17, 36, 2100, "Players Online :");
            AddHtml(160, 37, 30, 17, "<div align=\"right\" color=\"2100\">" + (NetState.Instances.Count).ToString() + "</div>", false, false);

            AddImageTiled(210, 37, 190, 17, 5154);
            AddLabel(212, 36, 2100, "Active Accounts :");
            AddHtml(357, 37, 30, 17, "<div align=\"right\" color=\"2100\">" + (Accounts.Count).ToString() + "</div>", false, false);

            AddImageTiled(405, 37, 190, 17, 5154);
            AddLabel(407, 36, 2100, "Uptime :");
            AddHtml(485, 37, 109, 17, "<div align=\"right\" color=\"2100\">" + FormatTimeSpan(DateTime.UtcNow - Clock.ServerStart) + "</div>", false, false);

            AddImageTiled(600, 37, 185, 17, 5154);
            AddLabel(602, 36, 2100, "Memory in use :");
            AddHtml(700, 37, 75, 17, "<div align=\"right\" color=\"2100\">" + FormatByteAmount(GC.GetTotalMemory(false)) + "</div>", false, false);
            AddBackground(15, 59, 770, 526, 0x2454);
            AddBlackAlpha(18, 62, 763, 520);
            AddLabelCropped(20, 60, 220, 20, 2100, "CHARACTER");
            AddLabelCropped(212, 60, 60, 20, 2100, "GUILD");

            // if (from.AccessLevel > AccessLevel.Player)
            // {
            AddLabelCropped(403, 60, 60, 20, 2100, "STATS");
            AddLabelCropped(465, 60, 60, 20, 2100, "SKILL");
            AddLabelCropped(527, 60, 60, 20, 2100, "KARMA");
            AddLabelCropped(589, 60, 60, 20, 2100, "FAME");
            AddLabelCropped(651, 60, 60, 20, 2100, "REGION");
            // }

            if (m_List == null)
                m_List = new ArrayList(NetState.Instances);

            if ((m_CountList == null) && (m_List.Count > 25))
                m_CountList = new int[(int)(m_List.Count / 25)];

            if (listPage > 0)
                AddButton(744, 62, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0);
            else
                AddImage(744, 62, 0x25EA);

            if ((listPage + 1) * 25 < m_List.Count)
                AddButton(761, 62, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0);
            else
                AddImage(761, 62, 0x25E6);

            if (m_List.Count == 0)
                AddLabel(20, 80, 0x25, "No players online.");

            int k = 0;

            if (listPage > 0)
            {
                for (int z = 0; z < (listPage - 1); ++z)
                {
                    k = k + Convert.ToInt32(m_CountList[z]);
                }
            }

            for (int i = 0, j = 0, index = ((listPage * 25) + k); i < 25 && index >= 0 && index < m_List.Count && j >= 0; ++i, ++j, ++index)
            {
                NetState ns = m_List[index] as NetState;

                if (ns == null)
                    continue;

                Mobile m = ns.Mobile;

                int offset = 80 + (i * 20);

                if (m == null)
                {
                    if (RemoteAdmin.AdminNetwork.IsAuth(ns))
                        AddLabelCropped(20, offset, 220, 20, 2100, "(Remote Admin)");
                    else
                        AddLabelCropped(20, offset, 220, 20, 2100, "(Connecting)");
                }
                else
                {
                    AddLabelCropped(20, offset, 220, 20, GetHueFor(m), m.RawName);

                    Guild g = m.Guild as Guild;

                    if (g != null)
                    {
                        string Ftitle;
                        Ftitle = "[";

                        Ftitle = Ftitle + g.Abbreviation + "] " + g.Name;

                        AddLabelCropped(212, offset, 209, 20, 2100, Ftitle);
                    }

                    // if (from.AccessLevel > AccessLevel.Player)
                    // {
                    AddLabelCropped(403, offset, 60, 20, 2100, m.RawStatTotal.ToString());
                    String skill = m.SkillsTotal.ToString();
                    AddLabelCropped(465, offset, 60, 20, 2100, skill.Substring(0, skill.Length - 1));
                    AddLabelCropped(527, offset, 60, 20, 2100, m.Karma.ToString());
                    AddLabelCropped(589, offset, 60, 20, 2100, m.Fame.ToString());
                    AddLabelCropped(651, offset, 60, 20, 2100, (m.Region.Name == null) ? "Unknown" : m.Region.Name);
                    // }
                }
                if (i == 25)
                {
                    m_CountList[listPage] = (j - 25);
                }

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

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            if (info.ButtonID == 0) // Cancel
                return;
            else if (from.Deleted || from.Map == null || from == null)
                return;


            switch (info.ButtonID)
            {
                case 1:
                    {
                        if (m_List != null && m_ListPage > 0)
                            from.SendGump(new StatusGump(from, m_ListPage - 1, m_List, m_CountList));

                        break;
                    }
                case 2:
                    {
                        if (m_List != null && ((m_ListPage + 1) * 25 < m_List.Count))
                            from.SendGump(new StatusGump(from, m_ListPage + 1, m_List, m_CountList));

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
