//   ___|========================|___
//   \  |  Written by Felladrin  |  /
//    > |      January 2011      | <
//   /__|========================|__\

using Server.Targeting;

namespace Server.Commands
{
    public class Rest
    {
        public static void Initialize()
        {
            CommandSystem.Register("Rest", AccessLevel.GameMaster, new CommandEventHandler(Restore_OnCommand));
        }

        [Usage("Rest")]
        [Description("Completely restore a character (life, mana, stam, hunger, thirst).")]
        public static void Restore_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new restTarget();
        }

        public class restTarget : Target
        {
            public restTarget() : base(-1, false, TargetFlags.None) { }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile)
                {
                    Mobile targ = (Mobile)targeted;
                    targ.Hits = targ.HitsMax;
                    targ.Mana = targ.ManaMax;
                    targ.Stam = targ.StamMax;
                    targ.Hunger = 20;
                    targ.Thirst = 20;
                }
            }
        }
    }
}
