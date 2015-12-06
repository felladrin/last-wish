using System;
using System.Collections;

namespace Server.Commands
{
    public class WipeItemsOfType
    {
        public static void Initialize()
        {
            CommandSystem.Register("WipeItemsOfType", AccessLevel.GameMaster, new CommandEventHandler(WipeMobilesOfType_OnCommand));
        }

        private static Mobile commanduser;

        [Usage("WipeItemsOfType <Type>")]
        [Description("Deletes all items of specific type from the game.")]

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
                from.SendMessage("Usage: [WipeItemsOfType <ItemType>");
        }

        private static void GetItems(string name, Mobile from)
        {
            if (ScriptCompiler.FindTypeByName(name) == null)
            {
                from.SendMessage("No type with that name was found.");
                return;
            }

            Type type = ScriptCompiler.FindTypeByName(name);

            BuildItemList(type);
        }

        public static ArrayList BuildItemList(Type type)
        {
            int itemcount = 0;

            ArrayList itemlist = new ArrayList();

            foreach (Item i in World.Items.Values)
            {
                if (i.GetType() == type)
                {
                    ++itemcount;
                    itemlist.Add(i);
                }
            }
            WipeItems(type, itemlist, itemcount);
            return itemlist;
        }

        private static void WipeItems(Type type, ArrayList itemlist, int itemCount)
        {
            commanduser.SendMessage("Wiping All Items Of Type: {0}", type);
            Console.WriteLine("Wiping All Items Of Type: {0}", type);

            foreach (Item i in itemlist)
                i.Delete();

            SendMessages(itemCount, type);
        }

        public static void SendMessages(int itemCount, Type type)
        {
            if (itemCount >= 1)
            {
                commanduser.SendMessage("Done - {0} Items of Type {1} Wiped...", itemCount, type);
                Console.WriteLine("Done - {0} Items of Type {1} Wiped...", itemCount, type);
                Timer.DelayCall(TimeSpan.FromSeconds(1), new TimerCallback(SaveWipe));
            }
            else
            {
                commanduser.SendMessage("Done - There Were No Items Of Type {1} to Wipe...", itemCount, type);
                Console.WriteLine("Done - There Were No Items Of Type {1} to Wipe...", itemCount, type);
            }
        }

        private static void SaveWipe()
        {
            World.Save();
        }
    }
}