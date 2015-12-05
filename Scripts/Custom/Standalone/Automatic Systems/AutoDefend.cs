//   ___|========================|___
//   \  |  Written by Felladrin  |  /
//    > |     October 2013       | <
//   /__|========================|__\

using Server.Commands;
using System.Collections.Generic;

namespace Server.Items
{
    public class AutoDefend
    {
        public static class Config
        {
            public static bool AllowPlayerToggle = true; // Should we allow player to use a command to toggle the auto-defend?
        }
        
        public static void Initialize()
        {
            EventSink.AggressiveAction += new AggressiveActionEventHandler(EventSink_AggressiveAction);

            if (Config.AllowPlayerToggle)
            {
                CommandSystem.Register("AutoDefend", AccessLevel.Player, new CommandEventHandler(OnToggleAutoDefend));
                EventSink.Logout += new LogoutEventHandler(OnPlayerLogout);
            }
        }

        private static List<int> DisabledPlayers = new List<int>();

        public static void EventSink_AggressiveAction(AggressiveActionEventArgs e)
        {
            if (e.Aggressed.Player && Config.AllowPlayerToggle && !DisabledPlayers.Contains(e.Aggressed.Serial.Value))
            {
                e.Aggressed.Warmode = true;
                e.Aggressed.Combatant = e.Aggressor;
            }
        }

        private static void OnPlayerLogout(LogoutEventArgs e)
        {
            Mobile m = e.Mobile;

            int key = m.Serial.Value;

            if (DisabledPlayers.Contains(key))
                DisabledPlayers.Remove(key);
        }

        [Usage("AutoDefend")]
        [Description("Enables or disables the auto-defend feature.")]
        private static void OnToggleAutoDefend(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

            int key = m.Serial.Value;

            if (DisabledPlayers.Contains(key))
            {
                DisabledPlayers.Remove(key);
                m.SendMessage(68, "You have enabled the auto-defend feature.");
            }
            else
            {
                DisabledPlayers.Add(key);
                m.SendMessage(38, "You have disabled the auto-defend feature.");
            }
        }
    }
}