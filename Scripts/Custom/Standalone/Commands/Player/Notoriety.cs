//   ___|========================|___
//   \  |  Written by Felladrin  |  /   This script was released on RunUO Forums under the GPL licensing terms.
//    > |      August 2013       | <
//   /__|========================|__\   [Notoriety Command] - Current version: 1.0 (August 18, 2013)

namespace Server.Commands 
{
    public class NotorietyCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("Notoriety", AccessLevel.Player, new CommandEventHandler(Notoriety_OnCommand));
        }

        [Usage("Notoriety")]
        [Description("Tells your fame and karma.")]
        public static void Notoriety_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Fame: {0}", e.Mobile.Fame);
            e.Mobile.SendMessage("Karma: {0}", e.Mobile.Karma);
        }
    }
}