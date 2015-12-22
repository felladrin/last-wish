using System;
using System.Collections;
using Server.Gumps;
using Server.Network;

namespace Server.Forums
{
    public class ForumGump : Gump
    {
    	public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(Open_OnLogin);
        }
    	
    	private static void Open_OnLogin(LoginEventArgs e)
        {
            e.Mobile.SendGump(new ForumGump(e.Mobile, 0));
        }
    	
        public enum Buttons
        {
            CreditsButton = 11,
            HelpButton = 12,
            NewPostButton = 13,
            NextPage = 14,
            PreviousPage = 15,
            Cancel = 16
        }

        private int LabelColor = 0x7FFF;
        private Mobile m_Player;
        private ArrayList m_PagedThreads;
        private int m_Page;

        public ForumGump(Mobile pm, int page)
            : base(0, 0)
        {
            m_Player = pm;
            m_Page = page;
            m_PagedThreads = new ArrayList();
            Closable = false;
            Disposable = true;
            Dragable = true;
            Resizable = false;
            AddPage(0);
            AddBackground(71, 53, 618, 489, 9200);
            AddImage(631, 9, 5536);
            AddBlackAlpha(80, 63, 549, 63);
            AddImage(655, 368, 10411);
            AddImage(657, 181, 10410);
            AddImage(21, 481, 10402);
            AddButton(579, 160, 4008, 4008, (int)Buttons.CreditsButton, GumpButtonType.Reply, 0);
            AddButton(580, 187, 4014, 4014, (int)Buttons.HelpButton, GumpButtonType.Reply, 0);
            AddButton(94, 507, 4029, 4029, (int)Buttons.NewPostButton, GumpButtonType.Reply, 0);
            AddButton(400, 507, 4029, 4029, (int)Buttons.Cancel, GumpButtonType.Reply, 0);
            AddLabel(435, 507, 49, "Close");
            AddLabel(131, 507, 49, "New Topic");
            AddLabel(613, 161, 499, "Credits");
            AddLabel(614, 187, 499, "Help");
            AddLabel(87, 68, 499, "FORUM - Topics: " + ForumCore.Threads.Count.ToString() + " - Moderators: " + ForumCore.Moderators.Count.ToString());
            AddLabel(87, 93, 499, "There are currently " + NetState.Instances.Count + " users online.");
            AddLabel(535, 101, 499, "Version " + ForumCore.Version);
            AddLabel(80, 124, 499, " Status    Topic Title                            Visits/Replies");
            AddBlackAlpha(80, 144, 491, 354);

            bool pages = (ForumCore.Threads.Count > 10);
            bool more = false;

            int index = m_Page * 10;

            if (index < 0)
                index = 0;

            int maxcount = index + 10;
            int offset = 0;

            for (int i = index; i < ForumCore.Threads.Count; i++)
            {
                if (i >= maxcount)
                {
                    more = true;
                    break;
                }

                ThreadEntry te = (ThreadEntry)ForumCore.Threads[i];
                if (!te.Deleted && !(te.StaffMessage && m_Player.AccessLevel < AccessLevel.Counselor))
                {
                    bool viewed = te.IsViewer(m_Player);
                    bool poster = te.IsPoster(m_Player);
                    bool viewedUpdate = te.IsViewerSinceUpdate(m_Player);
                    m_PagedThreads.Add(te);

                    int buttonID;

                    if (te.IsPoster(m_Player) && te.IsViewerSinceUpdate(m_Player))
                        buttonID = 4011;
                    else if (te.IsPoster(m_Player))
                        buttonID = 4012;
                    else if (te.IsViewerSinceUpdate(m_Player))
                        buttonID = 4014;
                    else if (te.IsViewer(m_Player))
                        buttonID = 4015;
                    else
                        buttonID = 4014;

                    if (te.Locked)
                        buttonID = 4017;

                    AddButtonLabeled(100, (((35 * (i - index)) + 155) - offset), (i - (maxcount - (((m_Page + 1) * 10))) - index), buttonID, te.Subject, te.NumberOfViews.ToString(), te.NumberOfReplys.ToString(), te.StaffMessage, te.EntryType);
                }
                else
                {
                    maxcount++;
                    offset += 35;
                }
            }

            if (pages)
            {
                if (more)
                    AddButton(644, 507, 5541, 5541, (int)Buttons.NextPage, GumpButtonType.Reply, 0);

                if (m_Page > 0)
                    AddButton(621, 507, 5538, 5538, (int)Buttons.PreviousPage, GumpButtonType.Reply, 0);
            }
        }

        public void AddBlackAlpha(int x, int y, int width, int height)
        {
            AddImageTiled(x, y, width, height, 2624);
            AddAlphaRegion(x, y, width, height);
        }

        public string Color(string text, int color)
        {
            return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
        }

        public void AddButtonLabeled(int x, int y, int buttonID, int buttonValue, string text, string views, string replys, bool staff, ThreadType type)
        {
            AddImage(655, 368, 10411);
            AddButton(x, y, buttonValue, buttonValue, buttonID, GumpButtonType.Reply, 0);
            if (type == ThreadType.RegularThread)
                AddHtml(x + 40, y, 300, 20, Color(staff ? "Staff: " + text : text, staff ? LabelColor - 500 : LabelColor), false, false);
            if (type == ThreadType.Sticky)
                AddHtml(x + 40, y, 300, 20, Color("Fixed: " + text, LabelColor - 450), false, false);
            if (type == ThreadType.Announcement)
                AddHtml(x + 40, y, 300, 20, Color("Announcement: " + text, LabelColor - 350), false, false);

            AddLabel(x + 370, y, 38, views + "/" + replys);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            m_Player = (Mobile)sender.Mobile;

            if (m_Player == null)
                return;

            switch (info.ButtonID)
            {
                default://Any Thread
                    {
                        ThreadEntry te = (ThreadEntry)m_PagedThreads[info.ButtonID];
                        if (te != null && !te.Deleted)
                        {
                            if (te.Deleted)
                            {
                                m_Player.SendMessage("That thread has been queued for deletion and may not be viewed!");
                                break;
                            }
                            te.AddViewerSinceUpdate(m_Player);
                            te.AddViewer(m_Player);

                            m_Player.CloseGump(typeof(ThreadGump));
                            m_Player.SendGump(new ThreadGump(m_Player, te));
                        }
                        break;
                    }
                case 11://Credits
                    {
                        m_Player.CloseGump(typeof(ForumGump));
                        m_Player.SendGump(new ForumGump(m_Player, m_Page));
                        m_Player.CloseGump(typeof(CreditsGump));
                        m_Player.SendGump(new CreditsGump());
                        break;
                    }
                case 12://Help
                    {
                        m_Player.CloseGump(typeof(ForumGump));
                        m_Player.SendGump(new ForumGump(m_Player, m_Page));
                        m_Player.CloseGump(typeof(HelpGump));
                        m_Player.SendGump(new HelpGump());
                        break;
                    }
                case 13://New Post
                    {
                        m_Player.CloseGump(typeof(PostGump));
                        m_Player.SendGump(new PostGump(m_Player));
                        break;
                    }
                case 14://Next Page
                    {
                        int page = m_Page + 1;
                        m_Player.CloseGump(typeof(ForumGump));
                        m_Player.SendGump(new ForumGump(m_Player, page));
                        break;
                    }
                case 15://Previous Page
                    {
                        int page = m_Page - 1;
                        m_Player.CloseGump(typeof(ForumGump));
                        m_Player.SendGump(new ForumGump(m_Player, page));
                        break;
                    }
                case 16://Cancel
                    {
                        break;
                    }
            }
        }
    }
}
