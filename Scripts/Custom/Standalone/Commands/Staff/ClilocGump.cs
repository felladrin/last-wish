// Created by Lokai.
using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public static class ClilocGumpCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("ClilocGump", AccessLevel.Administrator, new CommandEventHandler(ClilocGump_OnCommand));
        }

        [Usage("ClilocGump")]
        [Description("Begins the Cliloc Gump.")]
        private static void ClilocGump_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            try
            {
                int arg = Int32.Parse(e.Arguments[0]);
                if (arg >= 500000 && arg <= 3011032)
                    m.SendGump(new ClilocGump(m, arg));
                else
                    m.SendGump(new ClilocGump(m));
            }
            catch
            {
                m.SendGump(new ClilocGump(m));
            }
            
        }
    }

    public class ClilocGump : Gump
    {
        private Mobile m_From;
        private int m_OldIndex;
        private int m_LastIndex;
        private const int NUM_PER_PAGE = 8;
        private static List<int> m_List = new List<int>();
        private int m_Theme = 7;

        public ClilocGump(Mobile from)
            : this(from, 0, GenerateList(), 7)
        {
        }

        public ClilocGump(Mobile from, int temp)
            : this(from, GenerateList().LastIndexOf(temp), GenerateList(), 7)
        {
        }

        public ClilocGump(Mobile from, int index, int theme)
            : this(from, index, m_List, theme)
        {
        }

        private static List<int> GenerateList()
        {
            List<int> tempList = new List<int>();
            for (int x = 500000; x < 3150000; x++)
            {
                if (InRange(x))
                {
                    tempList.Add(x);
                }
            }
            return tempList;
        }

        public ClilocGump(Mobile from, int index, List<int> list, int theme)
            : base(20, 40)
        {
            m_From = from;
            m_OldIndex = index;
            m_LastIndex = index;
            m_List = list;
            m_Theme = theme;
            bool t7 = theme == 7;
            bool t8 = theme == 8;
            bool t9 = theme == 9;
            bool t10 = theme == 10;
            bool t11 = theme == 11;
            int bground = t7 ? 9200 : t8 ? 3500 : t9 ? 3600 : t10 ? 9400 : 9300;

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = true;

            AddPage(0);
            AddBackground(20, 40, 740, 505, bground);
            AddBackground(20, 551, 618, 39, bground);
            AddBackground(621, 551, 139, 39, bground);
            AddBackground(600, 90, 130, 60, 9200);
            AddHtml(615, 105, 80, 30, t7 ? "*Current*" : "Select This One", true, false);
            if (!t7)
                AddButton(700, 108, 1210, 1210, 7, GumpButtonType.Reply, 0);
            AddBackground(600, 165, 130, 60, 3500);
            AddHtml(615, 180, 80, 30, t8 ? "*Current*" : "Select This One", true, false);
            if (!t8)
                AddButton(700, 183, 1210, 1210, 8, GumpButtonType.Reply, 0);
            AddBackground(600, 240, 130, 60, 3600);
            AddHtml(615, 255, 80, 30, t9 ? "*Current*" : "Select This One", true, false);
            if (!t9)
                AddButton(700, 258, 1210, 1210, 9, GumpButtonType.Reply, 0);
            AddBackground(600, 315, 130, 60, 9400);
            AddHtml(615, 330, 80, 30, t10 ? "*Current*" : "Select This One", true, false);
            if (!t10)
                AddButton(700, 333, 1210, 1210, 10, GumpButtonType.Reply, 0);
            AddBackground(600, 390, 130, 60, 9300);
            AddHtml(615, 405, 80, 30, t11 ? "*Current*" : "Select This One", true, false);
            if (!t11)
                AddButton(700, 408, 1210, 1210, 11, GumpButtonType.Reply, 0);
            int y = 45;
            int found = 0;

            for (int z = index; found < NUM_PER_PAGE; z++)
            {
                try
                {
                    AddHtml(33, y + 3, 80, 22, m_List[z].ToString(), false, false);
                    AddHtmlLocalized(110, y, 480, (int)(500 / NUM_PER_PAGE) - 5, m_List[z], true, true);
                    y += (int)(500 / NUM_PER_PAGE);
                    found += 1;
                }
                catch
                {
                }
                m_LastIndex = z;
            }

            AddLabel(490, 560, 380, "Search by number:");
            AddTextEntry(630, 560, 90, 20, 32, 2, m_List[index].ToString());
            AddButton(715, 560, 4015, 4016, 5, GumpButtonType.Reply, 0);
            if (m_List[index] - NUM_PER_PAGE >= 500000)
                AddButton(601, 45, 0x1519, 0x1519, 3, GumpButtonType.Reply, 0); // Previous Page
            if (m_List[index] + NUM_PER_PAGE <= 3011032)
                AddButton(601, 522, 0x151A, 0x151A, 4, GumpButtonType.Reply, 0); // Next Page
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            int x = info.ButtonID;
            TextRelay tr2 = info.GetTextEntry(2);
            m_From.CloseGump(typeof(ClilocGump));
            if (x == 3)
                m_From.SendGump(new ClilocGump(m_From, m_OldIndex - NUM_PER_PAGE, m_Theme)); //Previous Page
            else if (x == 4)
                m_From.SendGump(new ClilocGump(m_From, m_LastIndex + 1, m_Theme)); //Next Page
            else if (x == 5 && tr2 != null)
            {
                //Try to interpret the number typed in the browse box.
                int temp = 0;
                try
                {
                    temp = Convert.ToInt32(tr2.Text, 10);
                }
                catch
                {
                }

                if (m_List.Contains(temp))
                {
                    m_From.SendGump(new ClilocGump(m_From, m_List.LastIndexOf(temp), m_Theme));
                }
                else
                //If out of range, send an error, and re-display the gump.
                {
                    m_From.SendMessage("That number is out of range.");
                    m_From.SendGump(new ClilocGump(m_From, m_OldIndex, m_Theme));
                }
            }
            else if (x == 7)
                m_From.SendGump(new ClilocGump(m_From, m_OldIndex, x)); //Grey Stone Theme
            else if (x == 8)
                m_From.SendGump(new ClilocGump(m_From, m_OldIndex, x)); //Paper Theme
            else if (x == 9)
                m_From.SendGump(new ClilocGump(m_From, m_OldIndex, x)); //Dark Box Theme
            else if (x == 10)
                m_From.SendGump(new ClilocGump(m_From, m_OldIndex, x)); //Shiny Grey Theme
            else if (x == 11)
                m_From.SendGump(new ClilocGump(m_From, m_OldIndex, x)); //Tan Parchment Theme
        }

        private static bool InRange(int index)
        {
            if (index <= 503405 && index >= 500000)
                return true;
            if (index <= 1000076 && index >= 1000000)
                return true;
            if (index <= 1001018 && index >= 1001000)
                return true;
            if (index <= 1002171 && index >= 1002000)
                return true;
            if (index <= 1003000 && index >= 1003000)
                return true;
            if (index <= 1004056 && index >= 1004000)
                return true;
            if (index <= 1005702 && index >= 1005000)
                return true;
            if (index <= 1006052 && index >= 1006000)
                return true;
            if (index <= 1007172 && index >= 1007000)
                return true;
            if (index <= 1008161 && index >= 1008000)
                return true;
            if (index <= 1009018 && index >= 1009000)
                return true;
            if (index <= 1010655 && index >= 1010000)
                return true;
            if (index <= 1011580 && index >= 1011000)
                return true;
            if (index <= 1012022 && index >= 1012000)
                return true;
            if (index <= 1013077 && index >= 1013000)
                return true;
            if (index <= 1014570 && index >= 1014000)
                return true;
            if (index <= 1015328 && index >= 1015000)
                return true;
            if (index <= 1016518 && index >= 1016000)
                return true;
            if (index <= 1017413 && index >= 1017000)
                return true;
            if (index <= 1018364 && index >= 1018000)
                return true;
            if (index <= 1019078 && index >= 1019000)
                return true;
            if (index <= 1036383 && index >= 1020000)
                return true;
            if (index <= 1037014 && index >= 1037000)
                return true;
            if (index <= 1038328 && index >= 1038000)
                return true;
            if (index <= 1039015 && index >= 1039000)
                return true;
            if (index <= 1040028 && index >= 1040000)
                return true;
            if (index <= 1041645 && index >= 1041000)
                return true;
            if (index <= 1043355 && index >= 1042000)
                return true;
            if (index <= 1044636 && index >= 1044000)
                return true;
            if (index <= 1045170 && index >= 1045000)
                return true;
            if (index <= 1046482 && index >= 1046000)
                return true;
            if (index <= 1047031 && index >= 1047000)
                return true;
            if (index <= 1048176 && index >= 1048000)
                return true;
            if (index <= 1049787 && index >= 1049000)
                return true;
            if (index <= 1050057 && index >= 1050000)
                return true;
            if (index <= 1051031 && index >= 1051000)
                return true;
            if (index <= 1052084 && index >= 1052000)
                return true;
            if (index <= 1053183 && index >= 1053000)
                return true;
            if (index <= 1054160 && index >= 1054000)
                return true;
            if (index <= 1055143 && index >= 1055000)
                return true;
            if (index <= 1063489 && index >= 1060000)
                return true;
            if (index <= 1071563 && index >= 1070635)
                return true;
            if (index <= 1071999 && index >= 1071878)
                return true;
            if (index <= 1080550 && index >= 1072042)
                return true;
            if (index <= 1116791 && index >= 1094689)
                return true;
            if (index <= 1123240 && index >= 1123237)
                return true;
            if (index <= 1123358 && index >= 1123299)
                return true;
            if (index <= 1154722 && index >= 1149560)
                return true;
            if (index <= 1154761 && index >= 1154761)
                return true;
            if (index <= 1154899 && index >= 1154899)
                return true;
            if (index <= 1154916 && index >= 1154911)
                return true;
            if (index <= 3000619 && index >= 3000000)
                return true;
            if (index <= 3001033 && index >= 3001000)
                return true;
            if (index <= 3002149 && index >= 3002000)
                return true;
            if (index <= 3005146 && index >= 3005000)
                return true;
            if (index <= 3006057 && index >= 3006000)
                return true;
            if (index <= 3006301 && index >= 3006099)
                return true;
            if (index <= 3010197 && index >= 3010000)
                return true;
            if (index <= 3011032 && index >= 3011000)
                return true;

            return false;
        }
    }
}