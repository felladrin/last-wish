/*
Script Name: AllyChat.cs
Author: Shadow1980
Version: 1.0
Private Release: 07/11/05
Purpose:  Proper Functioning of In-game Alliance Chat without modifications to stock files.

Description: 		This basically makes alliance chat work on [ally command.


Acknowledgements: 	Based on GuildAllyChat by Eddie Gorgels released 10 November 2004 V1.0.

Installation:		Put AllyChat.cs in your Custom folder.
*/
using System;
using Server.Network;
using Server.Guilds;

namespace Server.Commands
{
    public class ally
    {
        public static void Initialize()
        {
            CommandSystem.Register("A", AccessLevel.Player, new CommandEventHandler(ally_OnCommand));
        }

        [Usage("A <text>")]
        [Description("Broadcasts a message to all online guildmembers and allies.")]
        private static void ally_OnCommand(CommandEventArgs e)
        {
            switch (e.ArgString.ToLower())
            {
                case "":
                    e.Mobile.SendMessage("Alliance Chat. usage: [a <text>");
                    break;
                default:
                    Msga(e);
                    break;
            }
        }

        private static void Msga(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            Guild FromGuild = from.Guild as Guild;
            if (FromGuild == null)
            {
                from.SendMessage("First you need to be in a guild!");
            }
            else
            {
                if (FromGuild.Allies.Count == 0)
                {
                    from.SendMessage("Your guild does not have any allies!");
                }
                else
                {
                    string AbbreviationOrName;
                    foreach (NetState state in NetState.Instances)
                    {
                        Mobile m = state.Mobile;
                        bool found = false;
                        int i = 0;
                        while ((!found) && (i <= (FromGuild.Allies.Count - 1)))
                        {
                            if (m != null && ((m.Guild == FromGuild.Allies[i]) || (FromGuild.IsMember(m))))
                            {//an empty abbreviationname will allways be show as "none"at the server. no need for an if statement
                                AbbreviationOrName = (from.Guild as Guild).Abbreviation;
                                m.SendMessage(0x3C, String.Format("* {0} [{1}]: {2}", from.Name, AbbreviationOrName, e.ArgString));
                                found = true;
                            }
                            i++;

                        }
                    }
                }
            }
        }
    }
}
