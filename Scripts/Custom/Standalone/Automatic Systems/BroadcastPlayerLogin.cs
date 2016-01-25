using Server;

namespace Felladrin.Automations
{
    public static class BroadcastPlayerLogin
    {
        public static void Initialize()
        {
            EventSink.Login += OnLogin;
        }

        static void OnLogin(LoginEventArgs e)
        {
            World.Broadcast(0x482, false, "{0} has joined our world.", e.Mobile.Name);
        }
    }
}