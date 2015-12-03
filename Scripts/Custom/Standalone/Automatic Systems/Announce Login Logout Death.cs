using System;
using System.Collections;
using Server.Network;

namespace Server
{
    public class Announce
    {
        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(World_Login);
            EventSink.Logout += new LogoutEventHandler(World_Logout);
            EventSink.PlayerDeath += new PlayerDeathEventHandler(OnDeath);
        }

        private static void World_Login(LoginEventArgs args)
        {
            World.Broadcast(0x482, false, "{0} has joined our world.", args.Mobile.Name);
        }

        private static void World_Logout(LogoutEventArgs args)
        {
            World.Broadcast(0x482, false, "{0} has left our world.", args.Mobile.Name);
        }

        public static void OnDeath(PlayerDeathEventArgs args)
        {
            Mobile mob = args.Mobile.LastKiller;

            if ((mob != null) && (mob == args.Mobile))
            {
                World.Broadcast(0x482, false, "{0} has killed themself!", args.Mobile.Name);
            }
            else if (mob != null)
            {
                World.Broadcast(0x482, false, "{0} has been killed by {1}!", args.Mobile.Name, mob.Name);
            }
            else
            {
                World.Broadcast(0x482, false, "{0} has been killed!", args.Mobile.Name);
            }
        }
    }
}