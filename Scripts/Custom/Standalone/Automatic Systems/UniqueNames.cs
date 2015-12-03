// Scripted by Malaperth and Optimized by A_Li_N in 2006.
// Edited by Felladrin in 2013 to:
// - use a copy of the NameChangeDeedGump;
// - verify if the character is newer then the other who has the same name;
// - disallow choosing the same name on the gump.

using System;
using System.Collections;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Misc
{
    public class UniqueNames
    {
        private static ArrayList AllowedDupes = new ArrayList(new string[]
        {
			"Your",
			"allowed duplicate",
			"names",
			"Here",
			"like this",
			"case is ignored"
		});

        private static ArrayList Disallowed = new ArrayList(new string[]
        {
			"Your",
			"disallowed",
			"names",
			"Here",
			"like this"
		});

        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(Check);
        }

        private static void Check(LoginEventArgs args)
        {
            Mobile from = args.Mobile;
            if (AlreadyInUse(from, from.RawName))
            {
                from.CloseGump(typeof(NameChangeGump));
                from.SendGump(new NameChangeGump());
                from.SendMessage(33, "Your chosen name {0} is already in use or is unacceptable for use on this shard. Please choose a new one.", from.Name);
                from.Name = "Generic Player";
            }
        }

        public static bool AlreadyInUse(Mobile m, string name)
        {
            if (m == null || name == null || name.Length == 0)
                return true;

            string nameToLower = name.ToLower();

            if (nameToLower == "generic player")
                return true;

            /*
            if (AllowedDupes.Contains(nameToLower))
                return false;

            if (Disallowed.Contains(nameToLower))
                return true;
            */

            if (!NameVerification.Validate(name, 2, 16, true, true, true, 1, NameVerification.SpaceDashPeriodQuote))
                return true;

            foreach (Mobile mob in World.Mobiles.Values)
                if (mob is PlayerMobile && mob != m && mob.RawName != null && mob.RawName.ToLower() == nameToLower && ((PlayerMobile)m).CreationTime > ((PlayerMobile)mob).CreationTime)
                    return true;

            return false;
        }

        public class NameChangeGump : Gump
        {
            Item m_Sender;

            public void AddBlackAlpha(int x, int y, int width, int height)
            {
                AddImageTiled(x, y, width, height, 2624);
                AddAlphaRegion(x, y, width, height);
            }

            public void AddTextField(int x, int y, int width, int height, int index)
            {
                AddBackground(x - 2, y - 2, width + 4, height + 4, 0x2486);
                AddTextEntry(x + 2, y + 2, width - 4, height - 4, 0, index, "");
            }

            public string Center(string text)
            {
                return String.Format("<CENTER>{0}</CENTER>", text);
            }

            public string Color(string text, int color)
            {
                return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
            }

            public void AddButtonLabeled(int x, int y, int buttonID, string text)
            {
                AddButton(x, y - 1, 4005, 4007, buttonID, GumpButtonType.Reply, 0);
                AddHtml(x + 35, y, 240, 20, Color(text, 0xFFFFFF), false, false);
            }

            public NameChangeGump()
                : base(50, 50)
            {
                Closable = true;
                Dragable = true;
                Resizable = false;

                AddPage(0);

                AddBlackAlpha(10, 120, 250, 85);
                AddHtml(10, 125, 250, 20, Color(Center("Name Change"), 0xFFFFFF), false, false);

                AddLabel(73, 15, 1152, "");
                AddLabel(20, 150, 0x480, "New Name:");
                AddTextField(100, 150, 150, 20, 0);

                AddButtonLabeled(75, 180, 1, "Submit");
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (info.ButtonID != 1)
                    return;

                Mobile m = sender.Mobile;
                TextRelay nameEntry = info.GetTextEntry(0);

                string newName = (nameEntry == null ? null : nameEntry.Text.Trim());

                if (AlreadyInUse(m, newName))
                {
                    m.SendMessage(33, "You can't use that name. Please choose a new one.");
                    m.CloseGump(typeof(NameChangeGump));
                    m.SendGump(new NameChangeGump());
                    return;
                }
                else
                {
                    m.RawName = newName;
                    m.SendMessage(67, "Your name has been changed!");
                    m.SendMessage(66, String.Format("You are now known as {0}.", newName));
                }
            }
        }
    }
}