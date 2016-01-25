using Server;

namespace Felladrin.Automations
{
    public static class BroadcastPlayerLogout
    {
        public static void Initialize()
        {
            EventSink.Logout += OnLogout;
        }

        static void OnLogout(LogoutEventArgs e)
        {
            World.Broadcast(0x482, false, "{0} has left our world.", e.Mobile.Name);
        }
    }
}