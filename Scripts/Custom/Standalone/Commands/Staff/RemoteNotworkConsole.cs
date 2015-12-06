using System.Collections.Generic;
using Server.Gumps;
using Server.RemoteAdmin;

namespace Server.Commands
{
    public class RemoteNotworkConsole
    {
        private static List<string> m_Strings = new List<string>();
        private static List<Mobile> m_Mobiles = new List<Mobile>();

        public static void Initialize()
        {
            EventSink.Logout += new LogoutEventHandler(OnLogout);

            CommandSystem.Register("ConsoleON", AccessLevel.Administrator, new CommandEventHandler(ConsoleOn_OnCommand));
            CommandSystem.Register("ConsoleOFF", AccessLevel.Administrator, new CommandEventHandler(ConsoleOff_OnCommand));
        }

        public static void OnLogout(LogoutEventArgs e)
        {
            m_Mobiles.Remove(e.Mobile);
        }

        [Usage("ConsoleON")]
        [Description("Shows the console.")]
        public static void ConsoleOn_OnCommand(CommandEventArgs e)
        {
            m_Mobiles.Remove(e.Mobile);
            m_Mobiles.Add(e.Mobile);
            e.Mobile.SendGump(new ConsoleGump(e.Mobile, m_Strings));
        }

        [Usage("ConsoleOFF")]
        [Description("Hides the console.")]
        public static void ConsoleOff_OnCommand(CommandEventArgs e)
        {
            m_Mobiles.Remove(e.Mobile);
            e.Mobile.CloseGump(typeof(ConsoleGump));
        }

        public static void Configure()
        {
            Core.MultiConsoleOut.Add(new EventTextWriter(new EventTextWriter.OnConsoleChar(OnConsoleChar), new EventTextWriter.OnConsoleLine(OnConsoleLine), new EventTextWriter.OnConsoleStr(OnConsoleString)));
        }

        public static void OnConsoleString(string str)
        {
        }

        public static void OnConsoleChar(char ch)
        {
        }

        public static void OnConsoleLine(string line)
        {
            if (m_Strings.Count < 100)
            {
                m_Strings.Add(line);
            }
            else
            {
                for (int ii = 1; ii <= 99; ++ii)
                {
                    m_Strings[ii - 1] = m_Strings[ii];
                }
                m_Strings[99] = line;
            }

            for (int i = 0; i < m_Mobiles.Count; ++i)
            {
                Mobile m = m_Mobiles[i] as Mobile;
                m.SendGump(new ConsoleGump(m, m_Strings));
            }
        }
    }
    public class ConsoleGump : Gump
    {
        public ConsoleGump(Mobile from, List<string> list)
            : base(0, 0)
        {
            from.CloseGump(typeof(ConsoleGump));
            string HTML = "";

            for (int i = 0; i < list.Count; ++i)
            {
                HTML = list[i] + "<br>" + HTML;
            }

            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(9, 8, 671, 338, 9200);
            this.AddHtml(17, 16, 650, 319, HTML, (bool)true, (bool)true);
        }
    }
}
