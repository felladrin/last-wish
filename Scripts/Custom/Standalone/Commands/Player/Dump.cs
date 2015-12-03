// Script: Dump.cs
// Version: 1.1
// Author: Oak (ssalter)
// Servers: RunUO 2.0
// Date: 7/7/2006
// Purpose: 
// Player Command. [dump allows a player to dump everything from one container to another.
//   Allowed containers to dump FROM are main backpack or a subcontainer thereof.
// History: 
//  Written for RunUO 1.0 shard, Sylvan Dreams,  in February 2005.
//  Modified for RunUO 2.0, removed shard specific customizations (wing layers, etc.), commented out xmlspawner questholder check.
// If you have XmlSpawner2, find the if(xx is Questholder) blocks that are commented out and uncomment them or players will use this command to store items in questholder (if you use questholders) and thus have a blessed container.

using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Accounting;
using System.Collections;
using Server.Network;


namespace Server.Commands
{
    public class Dump
    {
        public static void Initialize()
        {
            CommandSystem.Register("Dump", AccessLevel.Player, new CommandEventHandler(Dump_OnCommand));
        }

        [Usage("Dump")]
        [Description("Allows you to dump everything from one container to another.")]
        public static void Dump_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            from.LocalOverheadMessage(MessageType.Regular, 0x1150, true, "Select the container you want to dump items FROM.");
            from.Target = new PackFromTarget(from);
        }
        private class PackFromTarget : Target
        {
            public PackFromTarget(Mobile from)
                : base(-1, true, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Container)
                {
                    Container xx = o as Container;

                    // Container that is either in a pack or a child thereof
                    if (xx.IsChildOf(from.Backpack) || xx == from.Backpack)
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x33, true, "Select the container you want to dump items INTO.");
                        from.Target = new PackToTarget(from, xx);
                    }
                    else
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x22, true, "The container to dump FROM must be in your main backpack or BE your main backpack.");
                    }
                }
                else
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x22, true, "That is not a container!");
                }
            }
        }
        private class PackToTarget : Target
        {
            private Container FromCont;

            public PackToTarget(Mobile from, Container cont)
                : base(-1, true, TargetFlags.None)
            {
                FromCont = cont;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Container)
                {
                    Container xx = o as Container;

                    //make sure they aren't targeting same container
                    if (xx == FromCont)
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x22, true, "The container to dump INTO must be different from the one you are dumping FROM.");
                        return;
                    }
                    if (xx == FromCont || xx.IsChildOf(FromCont))
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x22, true, "You cannot sort INTO a subcontainer of the same container you are sorting FROM.");
                        return;
                    }
                    Item[] items = FromCont.FindItemsByType(typeof(Item), true);
                    foreach (Item item in items)
                    {
                        if (!(xx.TryDropItem(from, item, true)))
                            from.SendMessage("That container is too full!");
                    }
                }
                else
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x22, true, "That is not a container!");
                }
            }
        }
    }
}