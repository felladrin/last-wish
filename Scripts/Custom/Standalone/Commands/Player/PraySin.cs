// Brought to you by Mark01970 of the RunUO forums
using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Misc;
using Server.Factions;

namespace Server.Commands
{
    public class pray
    {
        public static void Initialize()
        {
            CommandSystem.Register("Pray", AccessLevel.Player, new CommandEventHandler(pray_OnCommand));
        }

        [Usage("Pray")]
        [Description("Resurrects you if you have positive karma.")]
        public static void pray_OnCommand(CommandEventArgs e)
        {
            if (!(e.Mobile.Alive))
            {
                e.Mobile.SendMessage("You begin to for another chance to continue your journey...");
                if (e.Mobile.Karma < 1000)
                {
                    e.Mobile.SendMessage("Your karma is not strong enough in the eyes of the gods!");
                }
                else
                {
                    e.Mobile.SendMessage("The Gods guarantee you the gift of life for your good nature.");
                    e.Mobile.Karma = e.Mobile.Karma - 1000;
                    e.Mobile.SendMessage("You karma was decreased by 1000.");
                    e.Mobile.Resurrect();
                }
            }
            else
            {
                e.Mobile.SendMessage("How can you receive the gift of life if you are already alive?");
            }
        }
    }

    public class sin
    {
        public static void Initialize()
        {
            CommandSystem.Register("Sin", AccessLevel.Player, new CommandEventHandler(sin_OnCommand));
        }

        [Usage("Sin")]
        [Description("Resurrects you if you have negative karma.")]

        public static void sin_OnCommand(CommandEventArgs e)
        {
            if (!(e.Mobile.Alive))
            {
                e.Mobile.SendMessage("You cry for another chance to continue your way of destruction...");
                if (e.Mobile.Karma > -1000)
                {
                    e.Mobile.SendMessage("You do not have enough evil to exchange your soul for life!");
                }
                else
                {
                    e.Mobile.SendMessage("The gates of underworld are open. You have a new chance to continue your journey of devastation!");
                    e.Mobile.Karma = e.Mobile.Karma + 1000;
                    e.Mobile.SendMessage("You karma was increased by 1000.");
                    e.Mobile.Resurrect();
                }
            }
            else
            {
                e.Mobile.SendMessage("You're not dead, you fool!");
            }
        }
    }
}
