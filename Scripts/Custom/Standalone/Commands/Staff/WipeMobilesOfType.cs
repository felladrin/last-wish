using System;
using System.Collections;

namespace Server.Commands
{
    public class WipeMobilesOfType
    {
        public static void Initialize()
        {
            CommandSystem.Register("WipeMobilesOfType", AccessLevel.Administrator, new CommandEventHandler(WipeMobilesOfType_OnCommand));
        }

        private static Mobile commanduser;

        [Usage("WipeMobilesOfType <Type>")]
        [Description("Wipes all creates of specified type from the world.")]
        public static void WipeMobilesOfType_OnCommand(CommandEventArgs e)
        {
            string m_Args;

            m_Args = e.ArgString;

            string name = m_Args;

            Mobile from = e.Mobile;

            commanduser = from;

            if (name != null && name.Length >= 1)
            {
                GetItems(name, from);
            }
            else
                from.SendMessage("Usage: .WipeMobilesOfType <MobileType>");
        }

        private static void GetItems(string name, Mobile from)
        {
            if (ScriptCompiler.FindTypeByName(name) == null)
            {
                from.SendMessage("No type with that name was found.");
                return;
            }

            Type type = ScriptCompiler.FindTypeByName(name);

            BuildMobileList(type);
        }

        public static ArrayList BuildMobileList(Type type)
        {
            int mobcount = 0;

            ArrayList moblist = new ArrayList();

            foreach (Mobile m in World.Mobiles.Values)
            {
                if (m.GetType() == type)
                {
                    ++mobcount;
                    moblist.Add(m);
                }
            }
            WipeItems(type, moblist, mobcount);
            return moblist;
        }

        private static void WipeItems(Type type, ArrayList moblist, int mobcount)
        {
            commanduser.SendMessage("Wiping All Mobiles Of Type: {0}", type);
            Console.WriteLine("Wiping All Mobiles Of Type: {0}", type);

            foreach (Mobile m in moblist)
                m.Delete();

            SendMessages(mobcount, type);
        }

        public static void SendMessages(int mobcount, Type type)
        {
            if (mobcount >= 1)
            {
                commanduser.SendMessage("Done - {0} Mobiles of Type {1} Wiped...", mobcount, type);
                Console.WriteLine("Done - {0} Mobiles of Type {1} Wiped...", mobcount, type);
                Timer.DelayCall(TimeSpan.FromSeconds(1), new TimerCallback(SaveWipe));
            }
            else
            {
                commanduser.SendMessage("Done - There Were No Mobiles Of Type {1} to Wipe...", mobcount, type);
                Console.WriteLine("Done - There Were No Mobiles Of Type {1} to Wipe...", mobcount, type);
            }
        }

        private static void SaveWipe()
        {
            World.Save();
        }
    }
}