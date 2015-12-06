//   ___|========================|___
//   \  |  Written by Felladrin  |  /   This script was released on RunUO Forums under the GPL licensing terms.
//    > |      August 2013       | <
//   /__|========================|__\   [ShardLiveShow] - Current version: 1.0 (August 11, 2013)

using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;

namespace Server.Commands
{
    public class ShardLiveShow
    {
        private static TimeSpan Delay = TimeSpan.FromSeconds(5); // How much time should we keep showing each player?

        public static void Initialize()
        {
            CommandSystem.Register("StartShow", AccessLevel.Counselor, new CommandEventHandler(StartShow_OnCommand));
            CommandSystem.Register("StopShow", AccessLevel.Counselor, new CommandEventHandler(StopShow_OnCommand));
        }

        private static List<Mobile> m_TargetList = new List<Mobile>();

        [Usage("StartShow")]
        [Description("Show off the players online.")]
        private static void StartShow_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            
            CollectTargets();

            if (m_TargetList.Count == 0)
            {
                m.SendMessage(33, "To use this command, there must be at least one player online. Staff members don't count.");
            }
            else
            {
                m.SendMessage(67, "You start the show. To stop it, just [StopShow command.");
                m.BodyMod = 897;
                ShowOff(m);
            }
        }

        [Usage("StopShow")]
        [Description("Stops the show off.")]
        private static void StopShow_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            m.SendMessage(67, "You stop the show.");
            m.BodyMod = 0;
        }

        private static void ShowOff(Mobile staff)
        {
            if (staff.BodyMod != 897)
                return;
            
            if (m_TargetList.Count == 0)
            {
                CollectTargets();

                if (m_TargetList.Count == 0)
                {
                    staff.SendMessage(33, "No more players online to follow.");
                    staff.BodyMod = 0;
                    return;
                }
            }

            Mobile player = m_TargetList[Utility.Random(m_TargetList.Count)];

            m_TargetList.Remove(player);

            if (!player.Deleted && player.NetState != null && player.NetState.Running)
            {
                staff.Hidden = true;
                
                staff.MoveToWorld(player.Location, player.Map);

                Timer.DelayCall(Delay, delegate
                {
                    if (!staff.Deleted && staff.NetState != null && staff.NetState.Running)
                        ShowOff(staff);
                });
            }
            else
            {
                ShowOff(staff);
            }
        }

        private static void CollectTargets()
        {
            foreach (NetState ns in NetState.Instances)
            {
                Mobile m = ns.Mobile;

                if (m != null && m is PlayerMobile && m.AccessLevel == AccessLevel.Player)
                {
                    m_TargetList.Add(m);
                }
            }
        }
    }
}
