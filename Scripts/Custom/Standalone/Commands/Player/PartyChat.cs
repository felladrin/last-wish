//   ___|========================|___   Party Chat Command - Semantic Version: 1.0.0
//   \  |  Written by Felladrin  |  /   Created at: 2015-12-19 (Felladrin)
//    > |     December 2015      | <    Updated at: 2015-12-19 (Felladrin)
//   /__|========================|__\   Description: Sends a message to the party.

using Server.Engines.PartySystem;

namespace Server.Commands
{
    public class PartyChatCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("P", AccessLevel.Player, new CommandEventHandler(OnCommand));
        }

        [Usage("P <text>")]
        [Description("Sends a message to your party.")]
        private static void OnCommand(CommandEventArgs e)
        {
            string text = e.ArgString.Trim();
            Mobile from = e.Mobile;

            if (text.Length == 0 || text.Length > 128)
            {
                from.SendMessage("Command usage: [p <text>");
                return;
            }

            Party p = Party.Get(from);

            if (p != null)
                p.SendPublicMessage(from, text);
            else
                from.SendLocalizedMessage(3000211); // You are not in a party.
        }
    }
}