/*
   Written Sunday, 18th July, 2004 by Ken. If you require help with this script and you cannot work it out yourself,
   feel free to e-mail me at ken@runuo.com or aenima@runuo.com. You can also contact me on the RunUO.com forums.
   My username is Aenima. Hope you enjoy.

 * Modified and extended in November 2006, to work with RunUO 2.0 [current SVN is 108] by 'Sotho Tal Ker'
 * Now the script deletes all items inside houses and playervendors.
*/
using System;
using System.Collections.Generic;
using Server.Accounting;
using Server.Commands;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;

namespace Server.Scripts.Commands
{
    public class DeleteCharacter
    {
        public static void Initialize()
        {
            CommandSystem.Register("DelChar", AccessLevel.Administrator, new CommandEventHandler(DeleteCharacter_OnCommand));
        }

        [Usage("DelChar")]
        [Description("Deletes a targeted character.")]
        public static void DeleteCharacter_OnCommand(CommandEventArgs arg)
        {
            arg.Mobile.SendMessage("Which character to delete?");
            arg.Mobile.Target = new DeleteCharTarget();
        }
    }

    public class DeleteCharTarget : Target
    {
        public DeleteCharTarget()
            : base(-1, false, TargetFlags.None)
        {
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (targeted is PlayerMobile)
            {
                Mobile mob = (Mobile)targeted;
                Account mobAccount = (Account)mob.Account;
                from.SendGump(new WarningGump(1060635, 30720, String.Format("You are about to delete the character <u>{0}</u> on account <u>{1}</u>. Any items on this character's person or bankbox will be removed, as will any pets and playervendors in their ownership. If this character owns a house, this will also be removed from the game, including all items inside the house.<br>Are you sure you wish to continue?", mob.Name, mobAccount.Username), 0xFFC000, 320, 240, new WarningGumpCallback(DeleteChar_WarningGumpCallback), targeted));
            }
            else
                from.SendMessage("You can only remove player characters using this command. Try {0}remove or {0}delete instead.", CommandSystem.Prefix);
        }

        public static void DeleteChar_WarningGumpCallback(Mobile from, bool okay, object state)
        {
            Mobile mob = (Mobile)state;
            NetState ns = mob.NetState;
            List<BaseHouse> houselist = BaseHouse.GetHouses(mob);
            List<Mobile> mobs = new List<Mobile>();

            int mobCount, houseCount;
            int itemCount = 0;

            if (!okay)
                return;

            CommandLogging.WriteLine(from, "{0} {1} deleting character {2}.", from.AccessLevel, CommandLogging.Format(from), CommandLogging.Format(mob));

            foreach (Mobile m in World.Mobiles.Values)
                if (m is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)m;
                    if ((bc.Controlled && bc.ControlMaster == mob) || (bc.Summoned && bc.SummonMaster == mob))
                        mobs.Add(bc);
                }
                else if (m is PlayerVendor)
                {
                    PlayerVendor pv = (PlayerVendor)m;
                    if (pv.Owner == mob)
                        mobs.Add(pv);
                }

            mobCount = mobs.Count;
            for (int i = 0; i < mobs.Count; ++i)
                mobs[i].Delete();
            mobs.Clear();

            houseCount = houselist.Count;
            for (int j = 0; j < houselist.Count; ++j)
            {
                BaseHouse house = (BaseHouse)houselist[j];

                List<Item> itemlist = house.GetItems();

                for (int k = 0; k < itemlist.Count; ++k)
                {
                    Item item = (Item)itemlist[k];

                    if (item.IsLockedDown)
                    {
                        itemCount++;
                        item.Delete();
                    }
                    else if (item.IsSecure && item is BaseContainer)
                    {
                        BaseContainer con = (BaseContainer)itemlist[k];
                        itemCount += con.GetTotal(TotalType.Items) + 1; // +1 is the container itself
                        con.Delete();
                    }
                }
                itemlist.Clear();

                house.Delete();
            }
            houselist.Clear();

            from.SendMessage("{0} pet{3} and playervendor{3} and {1} house{4} with {2} item{5} inside deleted.", mobCount, houseCount, itemCount, mobCount != 1 ? "s" : "", houseCount != 1 ? "s" : "", itemCount != 1 ? "s" : "");
            mob.Say("I've been deleted!");

            if (ns != null)
                ns.Dispose();

            mob.Delete();

            from.SendMessage("Character has been disposed of thoughtfully.");
        }
    }
}