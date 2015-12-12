//   ___|========================|___
//   \  |  Written by Felladrin  |  /
//    > |       July 2013        | <    [TinyChat] - Current version: 1.2 (December 5, 2015)
//   /__|========================|__\

using System.Collections.Generic;
using Server.Network;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Commands
{
    public class TinyChat
    {
        private static List<string> history;

        public static int historySize = 50;

        private static bool reverseMode = true;

        private static bool openOnLogin = true;

        public static void Initialize()
        {
            CommandSystem.Register("ChatToggle", AccessLevel.Player, new CommandEventHandler(toggleChat_OnCommand));
            CommandSystem.Register("ChatHistory", AccessLevel.Player, new CommandEventHandler(displayChat_OnCommand));
            CommandSystem.Register("C", AccessLevel.Player, new CommandEventHandler(chat_OnCommand));

            if (openOnLogin)
            {
                EventSink.Login += new LoginEventHandler(EventSink_Login);
            }

            history = new List<string>();
        }

        [Usage("ChatToggle")]
        [Description("Enables or Disables the Chat.")]
        public static void toggleChat_OnCommand(CommandEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;

            if (ProTag.Get(pm, "Chat") == null)
            {
                ProTag.Set(pm, "Chat", "ENABLED");
                pm.SendMessage(67, "Chat: ENABLED");
            }
            else
            {
                if (ProTag.Get(pm, "Chat") == "ENABLED")
                {
                    ProTag.Set(pm, "Chat", "DISABLED");
                    pm.SendMessage(33, "Chat: DISABLED");
                }
                else if (ProTag.Get(pm, "Chat") == "DISABLED")
                {
                    ProTag.Set(pm, "Chat", "ENABLED");
                    pm.SendMessage(67, "Chat: ENABLED");
                }
            }
        }

        [Usage("ChatHistory")]
        [Description("Opens the Chat History.")]
        public static void displayChat_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile.HasGump(typeof(ChatHistoryGump)))
            {
                e.Mobile.CloseGump(typeof(ChatHistoryGump));
            }

            e.Mobile.SendGump(new ChatHistoryGump(generateHistoryHTML()));
        }

        [Usage("C <message>")]
        [Description("Broadcasts a message to all players online. If no message is provided, it opens the Chat History.")]
        private static void chat_OnCommand(CommandEventArgs e)
        {
            if (e.ArgString.Length == 0)
            {
                if (e.Mobile.HasGump(typeof(ChatHistoryGump)))
                {
                    e.Mobile.CloseGump(typeof(ChatHistoryGump));
                }

                e.Mobile.SendGump(new ChatHistoryGump(generateHistoryHTML()));
            }
            else
            {
                Broadcast(e.Mobile, e.ArgString);
            }
        }

        private static void Broadcast(Mobile sender, string message)
        {
            if (history.Count > historySize)
            {
                history.RemoveAt(0);
            }

            history.Add(string.Format("[{0}] {1}: {2}", System.DateTime.UtcNow.ToString("HH:mm"), sender.Name, message));

            string historyHTML = generateHistoryHTML();

            foreach (NetState ns in NetState.Instances)
            {
                PlayerMobile player = ns.Mobile as PlayerMobile;

                if (player == null || ProTag.Get(player, "Chat") == "DISABLED")
                    continue;

                player.SendMessage(sender.SpeechHue, string.Format("<{0}> {1}", sender.Name, message));

                if (player.HasGump(typeof(ChatHistoryGump)))
                {
                    player.CloseGump(typeof(ChatHistoryGump));
                    player.SendGump(new ChatHistoryGump(historyHTML));
                }
            }
        }

        private static string generateHistoryHTML()
        {
            string HTML = "";

            foreach (string msg in history)
            {
                if (reverseMode)
                {
                    HTML = Utility.FixHtml(msg) + "<br/>" + HTML;
                }
                else
                {
                    HTML += Utility.FixHtml(msg) + "<br/>";
                }
            }

            if (HTML == "")
            {
                HTML = "No messages were sent since the last restart.";
            }

            return HTML;
        }

        private static void EventSink_Login(LoginEventArgs e)
        {
            PlayerMobile player = e.Mobile as PlayerMobile;

            if (ProTag.Get(player, "Chat") != "DISABLED")
            {
                player.SendGump(new ChatHistoryGump(generateHistoryHTML()));
            }
        }
    }

    public class ChatHistoryGump : Gump
    {
        public ChatHistoryGump(string historyHTML)
            : base(110, 100)
        {
            Closable = true;
            Dragable = true;
            Disposable = true;
            Resizable = false;

            AddPage(0);

            AddBackground(0, 0, 420, 250, 5054);

            AddImageTiled(10, 10, 400, 20, 2624);
            AddAlphaRegion(10, 10, 400, 20);
            AddLabel(15, 10, 73, Misc.ServerList.ServerName + " Chat History - Viewing the last " + TinyChat.historySize + " messages");

            AddImageTiled(10, 40, 400, 200, 2624);
            AddAlphaRegion(10, 40, 400, 200);
            AddHtml(15, 40, 395, 200, historyHTML, false, true);
        }
    }
}