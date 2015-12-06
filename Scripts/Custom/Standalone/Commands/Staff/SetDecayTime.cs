//Coded by ArteGordon on RunUO Forums.
using System;

namespace Server.Commands
{
    public class SetDecay
    {
        public static void Initialize()
        {
            CommandSystem.Register("SetDecay", AccessLevel.Administrator, new CommandEventHandler(SetDecay_OnCommand));

            // this will assign the default decay time for items automatically on server restarts.
            // This can be overridden by using the SetDecay command, but the overridden value will only last until the next restart.
            // Item.DefaultDecayTime = TimeSpan.FromDays(1);
        }

        [Usage("SetDecay [minutes]")]
        [Description("Sets/reports the default decay time for items in minutes.")]
        public static void SetDecay_OnCommand(CommandEventArgs e)
        {
            if (e.Arguments.Length > 0)
            {
                try
                {
                    int minutes = Convert.ToInt32(e.Arguments[0]);

                    Item.DefaultDecayTime = TimeSpan.FromMinutes(minutes);
                }
                catch { }
            }
            e.Mobile.SendMessage("Default decay time set to {0}", Item.DefaultDecayTime);
        }
    }
}
