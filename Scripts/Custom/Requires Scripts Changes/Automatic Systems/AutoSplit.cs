//   ___|========================|___
//   \  |  Written by Felladrin  |  /
//    > |       July 2013        | <
//   /__|========================|__\
//
// Istallation:
// 
// On Scripts/Items/Misc/Corpses/Corpse.cs find the method:
// CheckLift( Mobile from, Item item, ref LRReason reject )
// 
// Then, above its last 'return' statement, add the following line:
// if (Server.Custom.AutoSplit.Gold(from, item)) return false;

using Server.Engines.PartySystem;
using Server.Items;
using Server.Misc;
using Server.Mobiles;

namespace Server.Custom
{
    public class AutoSplit
    {
        public static bool Gold(Mobile from, Item item)
        {
            if (item is Gold)
            {
                Party party = Party.Get(from);

                if (party != null)
                {
                    int dividedGold = item.Amount / party.Members.Count;

                    if (dividedGold > 0)
                    {
                        item.Delete();

                        for (int j = 0; j < party.Members.Count; j++)
                        {
                            PartyMemberInfo info = party.Members[j] as PartyMemberInfo;

                            if (info != null && info.Mobile != null && info.Mobile is PlayerMobile)
                            {
                                PlayerMobile pm = info.Mobile as PlayerMobile;

                                if (pm.Backpack != null)
                                {
                                    if (pm != from)
                                    {
                                        pm.SendMessage("{0} takes gold from the corpse and divides equally among the party members: {1} for each.", from.Name, dividedGold);
                                    }
                                    else
                                    {
                                        pm.SendMessage("You take gold from the corpse and divide equally among the party members: {0} for each.", dividedGold);
                                    }

                                    Item playerGold = pm.Backpack.FindItemByType(typeof(Gold));

                                    Gold gold = new Gold(dividedGold);

                                    if (playerGold != null)
                                    {
                                        playerGold.Amount += gold.Amount;
                                    }
                                    else
                                    {
                                        pm.Backpack.AddItem(gold);
                                    }

                                    if (pm != from && WeightOverloading.IsOverloaded(pm))
                                    {
                                        pm.SendMessage("{0} keeps your share, cause you are overloaded.", from.Name);
                                        from.SendMessage("You keep {0}'s share, who is overloaded.", pm.Name);
                                        Item goldAdded = pm.Backpack.FindItemByType(typeof(Gold));
                                        goldAdded.Amount -= gold.Amount;
                                        Item goldReturned = from.Backpack.FindItemByType(typeof(Gold));
                                        goldReturned.Amount += gold.Amount;
                                    }
                                    else
                                    {
                                        pm.PlaySound(gold.GetDropSound());
                                    }
                                }
                            }
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
