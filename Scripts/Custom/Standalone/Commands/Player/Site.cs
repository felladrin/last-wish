//   ___|========================|___
//   \  |  Written by Felladrin  |  /   Site Command - Current version: 1.0 (August 31, 2013)
//    > |      August 2013       | <
//   /__|========================|__\   Description: Opens the shard website.

namespace Server.Commands
{
    public class SiteCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("Site", AccessLevel.Player, new CommandEventHandler(OpenWebSite));
        }

        [Usage("Site")]
        [Description("Opens the shard website in your browser.")]
        private static void OpenWebSite(CommandEventArgs e)
        {
            e.Mobile.LaunchBrowser("http://felladrin.github.io/last-wish/");
        }
    }
}