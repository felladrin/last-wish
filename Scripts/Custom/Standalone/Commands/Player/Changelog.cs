//   ___|========================|___
//   \  |  Written by Felladrin  |  /   Site Command - Current version: 1.0 (August 31, 2013)
//    > |      August 2013       | <
//   /__|========================|__\   Description: Opens the shard website.

namespace Server.Commands
{
    public class ChangelogCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("Changelog", AccessLevel.Player, new CommandEventHandler(OpenWebSite));
        }

        [Usage("Changelog")]
        [Description("Opens the shard changelog in your browser.")]
        private static void OpenWebSite(CommandEventArgs e)
        {
            e.Mobile.LaunchBrowser("https://github.com/felladrin/last-wish/commits/master");
        }
    }
}