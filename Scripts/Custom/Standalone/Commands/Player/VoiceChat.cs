//   ___|========================|___
//   \  |  Written by Felladrin  |  /   Site Command - Current version: 1.0 (August 31, 2013)
//    > |      August 2013       | <
//   /__|========================|__\   Description: Opens a website.

namespace Server.Commands
{
    public class VoiceChatCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("VoiceChat", AccessLevel.Player, new CommandEventHandler(OpenWebSite));
        }

        [Usage("VoiceChat")]
        [Description("Opens the shard voice chat in your browser.")]
        private static void OpenWebSite(CommandEventArgs e)
        {
            e.Mobile.LaunchBrowser("https://www.talk.gg/last-wish");
        }
    }
}