using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using System.Collections;

namespace Server.Commands
{
    public class GlobalMessageCommands
    {
        public static void Initialize()
        {
            CommandSystem.Register("GlobalMessage", AccessLevel.Seer, new CommandEventHandler(On_GlobalMessage));
        }

        [Usage("GlobalMessage")]
        [Description("Sends a message and/or link to all players online.")]
        private static void On_GlobalMessage(CommandEventArgs e)
        {
            Mobile from = (Mobile)e.Mobile;

            from.CloseGump(typeof(MessageComposeGump));
            from.SendGump(new MessageComposeGump());
        }
    }
}

namespace Server.Gumps
{
    public class GlobalMessageGump : Gump
    {
        string url;
        public GlobalMessageGump(string name, string message, bool hasurl, string urlb) : base(0, 0)
        {
            Closable = true;
            Dragable = true;
            Resizable = false;

            url = urlb;

            if (hasurl)
                AddBackground(209, 132, 274, 197, 9200);
            else
                AddBackground(209, 132, 274, 172, 9200);

            AddLabel(212, 134, 999, "A Message From GM " + name);
            AddAlphaRegion(217, 163, 256, 128);
            AddHtml(217, 163, 256, 128, "" + message, false, true);

            if (hasurl)
            {
                AddAlphaRegion(237, 297, 236, 25);
                AddLabel(240, 300, 3, "" + url);
                AddButton(216, 302, 1209, 1210, 1, GumpButtonType.Reply, 0);
            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        sender.Send(new LaunchBrowser(url));
                        break;
                    }
            }
        }
    }
}


namespace Server.Gumps
{
    public class MessageComposeGump : Gump
    {
        public MessageComposeGump()
            : base(0, 0)
        {
            Closable = true;
            Dragable = true;
            Resizable = false;

            AddBackground(23, 15, 293, 463, 9200);

            // Message
            AddLabel(54, 24, 0, "Message");
            AddRadio(30, 25, 208, 209, true, 2); // Message
            AddAlphaRegion(57, 49, 246, 73);
            AddTextEntry(57, 49, 246, 73, 70, 5, ""); // Message

            // Web Url
            AddLabel(56, 133, 0, "Web Url");
            AddRadio(30, 134, 208, 209, false, 3);  // Web Url
            AddAlphaRegion(57, 153, 246, 21);
            AddTextEntry(57, 153, 246, 21, 3, 6, "http://"); // Web Url

            // Long Message
            AddLabel(58, 182, 0, "Long Message");
            AddRadio(30, 182, 208, 209, false, 4); // Long Message
            AddAlphaRegion(57, 204, 246, 73);
            AddLabel(42, 229, 999, "1");
            AddTextEntry(57, 204, 246, 73, 70, 7, ""); // Long Message
            AddAlphaRegion(57, 294, 246, 73);
            AddLabel(42, 318, 999, "2");
            AddTextEntry(57, 294, 246, 73, 70, 8, "");  // Long Message

            // Plus Web Url
            AddLabel(34, 381, 999, "If you are sending a message and want");
            AddLabel(35, 398, 999, "to also send a web url enter one here");
            AddAlphaRegion(28, 419, 282, 25);
            AddTextEntry(28, 419, 282, 25, 3, 9, "http://"); // Plus Web Url

            AddButton(133, 451, 247, 248, 1, GumpButtonType.Reply, 0); // Okay
        }

        /*
         * 1 = Okay
         * 2 = Message Radio
         * 3 = Web Url Radio
         * 4 = Long Message Radio
         * 5 = Message Text
         * 6 = Web Url Text
         * 7 = Long Message Text
         * 8 = Long Message Text
         * 9 = Plus Web Url Text
         */

        public ArrayList BuildPlayerList()
        {
            ArrayList list = new ArrayList();

            foreach (Mobile m in World.Mobiles.Values)
            {
                if (m is PlayerMobile)
                {
                    PlayerMobile pm = (PlayerMobile)m;

                    if (pm.NetState != null)
                        list.Add(pm);
                }
            }

            return list;
        }

        public void SendMessageGumps(Mobile sender, int i, string msg, string url, ArrayList list)
        {
            bool hasurl = false;

            if (url != "nourl")
                hasurl = true;

            for (int ii = 0; ii < list.Count; ++ii)
            {
                PlayerMobile pm = (PlayerMobile)list[ii];

                pm.CloseGump(typeof(GlobalMessageGump));
                pm.SendGump(new GlobalMessageGump(sender.Name, msg, hasurl, url));
            }
            list.Clear();
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            ArrayList players = (ArrayList)BuildPlayerList();

            switch (info.ButtonID)
            {
                case 0:
                    {
                        from.SendMessage("You decide not to send a message.");
                        break;
                    }
                case 1:
                    {
                        bool plusurl = false;

                        TextRelay entryu = info.GetTextEntry(9);
                        string plus = (entryu == null ? "" : entryu.Text);

                        if (plus != "http://")
                            plusurl = true;

                        if (info.IsSwitched(2)) // Message
                        {
                            TextRelay entry = info.GetTextEntry(5);
                            string text = (entry == null ? "" : entry.Text);

                            if (plusurl)
                                SendMessageGumps(from, 1, text, plus, players);
                            else
                                SendMessageGumps(from, 1, text, "nourl", players);
                        }
                        else if (info.IsSwitched(3)) // Web Url
                        {
                            TextRelay entry = info.GetTextEntry(6);
                            string text = (entry == null ? "" : entry.Text);

                            string msg = "A GM is reque sting you open a web page click the button to the left to open it.";

                            if (plusurl)
                                SendMessageGumps(from, 2, msg, text, players);
                            else
                                SendMessageGumps(from, 2, msg, text, players);
                        }
                        else if (info.IsSwitched(4)) // Long Message
                        {
                            TextRelay entry1 = info.GetTextEntry(7);
                            string text1 = (entry1 == null ? "" : entry1.Text);

                            TextRelay entry2 = info.GetTextEntry(8);
                            string text2 = (entry2 == null ? "" : entry2.Text);

                            string msg = text1 + " " + text2;

                            if (plusurl)
                                SendMessageGumps(from, 3, msg, plus, players);
                            else
                                SendMessageGumps(from, 3, msg, "nourl", players);
                        }
                        else
                            return;

                        break;
                    }
            }
        }
    }
}
