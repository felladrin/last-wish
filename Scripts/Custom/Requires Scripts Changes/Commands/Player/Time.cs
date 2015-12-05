//   ___|========================|___
//   \  |  Written by Felladrin  |  /   This script was released on RunUO Forums under the GPL licensing terms.
//    > |      August 2013       | <
//   /__|========================|__\   [Time Command] - Current version: 1.0 (August 18, 2013)

namespace Server.Commands
{
    public class TimeCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("Time", AccessLevel.Player, new CommandEventHandler(Time_OnCommand));
        }

        [Usage("Time")]
        [Description("Returns the game's time and the server's local time.")]
        public static void Time_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            int currentHour, currentMinute;
            Items.Clock.GetTime(from.Map, from.X, from.Y, out currentHour, out currentMinute);
            from.SendMessage("Game current time is {0}", System.DateTime.Parse(currentHour + ":" + currentMinute).ToString("HH:mm"));
            from.SendMessage("Server local time is {0}", System.DateTime.UtcNow.ToString("HH:mm"));
        }
    }
}