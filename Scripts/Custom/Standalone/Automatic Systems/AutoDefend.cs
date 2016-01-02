// AutoDefend v1.1.0
// Author: Felladrin
// Created at 2013-10-14
// Updated at 2016-01-02

using System.Collections.Generic;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Mobiles;

namespace Felladrin.Misc
{
    public static class AutoDefend
    {
        public static class Config
        {
            public static bool Enabled = true;              // Is this system enabled?
            public static bool AllowPlayerToggle = true;    // Should we allow player to use a command to toggle the auto-defend?
        }

        public static void Initialize()
        {
            if (Config.Enabled)
            {
                EventSink.AggressiveAction += AggressiveActionEvent;
                EventSink.Login += LoginEvent;

                if (Config.AllowPlayerToggle)
                    CommandSystem.Register("AutoDefend", AccessLevel.Player, new CommandEventHandler(OnToggleAutoDefend));
            }
        }

        public static void AggressiveActionEvent(AggressiveActionEventArgs e)
        {
            if (e.Aggressed.Player && e.Aggressor != e.Aggressed.Combatant && !DisabledPlayers.Contains(e.Aggressed.Serial.Value))
            {
                if (e.Aggressed.Combatant == null)
                {
                    e.Aggressed.Warmode = true;
                    e.Aggressed.Combatant = e.Aggressor;
                }
                else if (e.Aggressor.GetDistanceToSqrt(e.Aggressed) < e.Aggressed.Combatant.GetDistanceToSqrt(e.Aggressed))
                {
                    e.Aggressed.Warmode = true;
                    e.Aggressed.Combatant = e.Aggressor;
                }
            }
        }

        public static void LoginEvent(LoginEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;
            Account acc = pm.Account as Account;

            if (acc.GetTag("AutoDefend") == "Disabled")
            {
                DisabledPlayers.Add(pm.Serial.Value);
            }
        }

        [Usage("AutoDefend")]
        [Description("Enables or disables the auto-defend feature.")]
        static void OnToggleAutoDefend(CommandEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;
            Account acc = pm.Account as Account;

            if (acc.GetTag("AutoDefend") == null || acc.GetTag("AutoDefend") == "Enabled")
            {
                DisabledPlayers.Add(pm.Serial.Value);
                acc.SetTag("AutoDefend", "Disabled");
                pm.SendMessage(38, "You have disabled the auto-defend feature for your account.");
            }
            else
            {
                DisabledPlayers.Remove(pm.Serial.Value);
                acc.SetTag("AutoDefend", "Enabled");
                pm.SendMessage(68, "You have enabled the auto-defend feature for your account.");
            }
        }

        static List<int> DisabledPlayers = new List<int>();
    }
}