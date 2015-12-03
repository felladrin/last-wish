// AllRelease.cs by Shadow1980
// Released on July 21, 2005
// Description: By using the "all release" command ingame, players can release all their tamed pets at once.
// This is especially useful when training taming. Note that the mount the player is riding, will NOT be released.

using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Misc
{
    public class AllRelease
    {
        public static void Initialize()
        {
            EventSink.Speech += new SpeechEventHandler(EventSink_Speech);
        }

        public static void EventSink_Speech(SpeechEventArgs args)
        {
            Mobile from = args.Mobile;

            if (args.Speech.ToLower().IndexOf("all release") >= 0)
            {
                foreach (Mobile m in from.GetMobilesInRange(15))
                {
                    if (m is BaseCreature && from != m && from.InLOS(m))
                    {
                        BaseCreature p = m as BaseCreature;

                        if (p.Controlled && p.Commandable)
                        {
                            bool isOwner = (from == p.ControlMaster);
                            bool isFriend = (!isOwner && p.IsPetFriend(from));

                            if (!p.Deleted && from.Alive && (isOwner || isFriend) && p.CheckControlChance(from))
                            {
                                p.ControlTarget = null;
                                p.ControlOrder = OrderType.Release;
                            }
                        }
                    }
                }
            }
        }
    }
}