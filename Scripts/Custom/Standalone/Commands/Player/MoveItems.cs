// Script: MoveItems.cs
// Version: 1.1
// Author: Oak (ssalter)
// Servers: RunUO 2.0
// Date: 7/7/2006
// Purpose: 
// Player Command. [moveitems allows a player to move specific items or item types from one container to another
// [moveitems exact   will move only specific items. [moveitems by itself will move items of the targetted type
// History: 
//  Written for RunUO 1.0 shard in February 2005.
//  Modified for RunUO 2.0
// If you have XmlSpawner2, find the if(xx is Questholder) blocks that are commented out and uncomment them or players will use this command to store items in questholder (if you use questholders) and thus have a blessed container.

using System;
using Server;
using Server.Multis;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Accounting;
using System.Collections;
using Server.Network;


namespace Server.Commands
{
    public class MoveItems
    {
        private Type MoveItemsType;

        public static void Initialize()
        {
            CommandSystem.Register("MoveItems", AccessLevel.Player, new CommandEventHandler(MoveItems_OnCommand));
        }

        [Usage("MoveItems")]
        [Description("Allows you to move items of a specific type from one container to another. For example, you want to move all spools of thread from your main pack to another backpack. Type [moveitems, target a spool of thread, then target the backpack you want the spools of thread to be moved to. The TO container normally is outside your main backpack. If you type ' exact' after the command [moveitems it will only move very specific types. For example, [moveitems and targeting a yumi bow will move ALL baseranged items. [moveitems exact and targetting a yumi will only move yumis.")]
        public static void MoveItems_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            // if we have a command after just the word "MoveItems"...
            if (e.Length != 0)
            {
                switch (e.GetString(0).ToLower())
                {
                    case "exact":
                        from.Target = new PackFromTarget(from, true);
                        break;
                    default:
                        from.LocalOverheadMessage(MessageType.Regular, 1150, true, "MoveItems will allow you to move items of a specific type from one container to another. For example, you want to move all spools of thread from your main pack to another backpack. Type [moveitems, target a spool of thread, then target the backpack you want the spools of thread to be moved to. The TO container normally is outside your main backpack. If you type ' exact' after the command [moveitems it will only move very specific types. For example, [moveitems and targeting a yumi bow will move ALL baseranged items. [moveitems exact and targetting a yumi will only move yumis.");
                        break;
                }
            }
            else
                from.Target = new PackFromTarget(from, false);
        }

        public static bool InOwnHouse(Mobile from)
        {
            BaseHouse house = BaseHouse.FindHouseAt(from);
            return (house != null && house.IsOwner(from));
        }

        private class PackFromTarget : Target
        {
            private bool zackly;
            private Type MoveItemsType;

            public PackFromTarget(Mobile from, bool isex)
                : base(-1, true, TargetFlags.None)
            {
                zackly = isex;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (!(o is Item) || o is Container)
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x22, true, "You cannot move that, only items. Try again.");
                    return;
                }

                Type ItemType = o.GetType();
                Type BType = ItemType.BaseType;

                Item theitem = o as Item;
                Container xx = (Container)theitem.Parent;

                // if item not in a container, just say no.
                if (xx == null)
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x33, true, "This command is intended to be used to move items from one container to another. Please target an item in a container");
                    return;
                }

                // if item is in a container not in their backpack
                if (!(xx.IsChildOf(from.Backpack)) && (xx != from.Backpack))
                {
                    // if  player is not in their own house
                    if (!InOwnHouse(from))
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x33, true, "You can only move items that are originally in your backpack or in a sub-container in your backpack or in a container in YOUR house.");
                        return;
                    }
                }

                // don't move all items if the base is Item, go back to the specific item type
                // ie: gold base item is ITEM. So, trying to move gold would move all items of any kind. Not good.
                if (BType == typeof(Item) || zackly)
                {
                    BType = o.GetType();
                }

                from.LocalOverheadMessage(MessageType.Regular, 0x33, true, "Now select the container you want to move the items INTO.");
                from.Target = new PackToTarget(from, xx, BType);
            }
        }
        private class PackToTarget : Target
        {
            private Container FromCont;
            private Type MyItem;

            public PackToTarget(Mobile from, Container cont, Type anitem)
                : base(-1, true, TargetFlags.None)
            {
                MyItem = anitem;
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
                        from.LocalOverheadMessage(MessageType.Regular, 0x22, true, "The container you are moving an item INTO must be different from the one you are moving an item FROM.");
                        return;
                    }

                    //make sure it isn't a container in their main backpack
                    if (FromCont.IsChildOf(from.Backpack) && (xx.IsChildOf(from.Backpack) || xx == from.Backpack))
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x22, true, "You cannot move items between containers in your backpack or from a container in your backpack TO your backpack. Just put one of them on the ground. Trust me, it is simpler. ");
                        return;
                    }
                    if (xx.IsChildOf(FromCont))
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x22, true, "You cannot move items INTO a subcontainer of the container you are moving items FROM. Just put one of them on the ground. Trust me, it is simpler. ");
                        return;
                    }

                    Item[] items = FromCont.FindItemsByType(MyItem, true);
                    foreach (Item item in items)
                    {
                        if (!(xx.TryDropItem(from, item, false)))
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