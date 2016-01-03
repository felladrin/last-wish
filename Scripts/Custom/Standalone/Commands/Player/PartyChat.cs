// Party Chat Command v1.0.1
// Description: Sends a message to the party.
// Author: Felladrin
// Started: 2015-12-19
// Updated: 2016-01-02

using Server;
using Server.Commands;
using Server.Engines.PartySystem;
using System.Collections.Generic;

namespace Felladrin.Commands
{
    public static class PartyChatCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("P", AccessLevel.Player, new CommandEventHandler(OnCommand));
        }

        [Usage("P <text>")]
        [Description("Sends a message to your party. If no message is set, lists the party members names.")]
        static void OnCommand(CommandEventArgs e)
        {
            string text = e.ArgString.Trim();
            Mobile from = e.Mobile;
            Party p = Party.Get(from);

            if (p == null)
            {
                from.SendLocalizedMessage(3000211); // You are not in a party.
                return;
            }

            if (text.Length == 0)
            {
                List<string> names = new List<string>();

                foreach (PartyMemberInfo pmi in p.Members)
                {
                    if (from == pmi.Mobile)
                        continue;
                    
                    names.Add(pmi.Mobile.Name);
                }

                string leaderInfo = "You are the leader.";

                if (from != p.Leader)
                {
                    leaderInfo = string.Format("{0} is the leader.", p.Leader.Name);
                }

                from.SendMessage("Your have {0} fellow{1} in your party: {2}. {3}", names.Count, (names.Count > 1 ? "s" : ""), string.Join(", ", names), leaderInfo);
            }
            else
            {
                p.SendPublicMessage(from, text);
            }
        }
    }
}