/*
Script Name: GuildChat.cs
Author: Shadow1980
Version: 1.0
Private Release: 07/08/05
Purpose:  Proper Functioning of In-game Guild Chat without modifications to stock files.

Description: 		This basically makes the guildchat work on [guild command.


Acknowledgements: 	Based on GuildAllyChat by Eddie Gorgels released 10 November 2004 V1.0.

Installation:		Put GuildChat.cs in your Custom folder.
*/
using System;
using System.Text;
using System.Collections;
using Server;
using Server.Network;
using Server.Guilds;
using Server.Mobiles;

namespace Server.Commands
{
    public class guild
    {
        public static void Initialize()
        {
            CommandSystem.Register("G", AccessLevel.Player, new CommandEventHandler(guild_OnCommand));
        }

        [Usage("G <text>")]
        [Description("Broadcasts a message to all online guildmembers.")]
        private static void guild_OnCommand(CommandEventArgs e)
        {
            switch (e.ArgString.ToLower())
            {
                case "":
                    e.Mobile.SendMessage("Guild Chat. usage: [g <text>");
                    break;
                default:
                    Msg(e);
                    break;
            }
        }

        private static void Msg(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            Guild theguild = from.Guild as Guild;
            if (theguild == null)
            {
                from.SendMessage("You are not in a guild!");
            }
            else
            {
                string AbbreviationOrName;
                foreach (NetState state in NetState.Instances)
                {
                    Mobile m = state.Mobile;
                    bool found = false;
                    int i = 0;
                    while ((!found) && (i <= (theguild.Members.Count - 1)))
                    {
                        if (m != null && (theguild.IsMember(m)))
                        {//an empty abbreviationname will allways be show as "none"at the server. no need for an if statement
                            AbbreviationOrName = (from.Guild as Guild).Abbreviation;
                            m.SendMessage(0x3C, String.Format("[Guild][{1}]: {2}", from.Name, e.ArgString));
                            found = true;
                        }
                        i++;
                    }
                }
            }
        }
    }
}
