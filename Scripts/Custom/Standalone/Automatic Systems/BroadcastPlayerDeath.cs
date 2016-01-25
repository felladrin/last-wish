using Server;

namespace Felladrin.Automations
{
    public static class BroadcastPlayerDeath
    {
        public static void Initialize()
        {
            EventSink.PlayerDeath += OnDeath;
        }

        static void OnDeath(PlayerDeathEventArgs e)
        {
            var killed = e.Mobile;
            var killer = e.Mobile.LastKiller;

            if (killer == killed)
            {
                World.Broadcast(0x482, false, "{0} has committed suicide!", killed.Name);
            }
            else if (killer != null)
            {
                World.Broadcast(0x482, false, "{0} has been killed by {1}!", killed.Name, killer.Name);
            }
            else
            {
                World.Broadcast(0x482, false, "{0} has been killed!", killed.Name);
            }
        }
    }
}