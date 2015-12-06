using Server.Mobiles;
using Server.Targeting;

namespace Server.Commands
{
    public class Tame
    {
        public static void Initialize()
        {
            CommandSystem.Register("Tame", AccessLevel.GameMaster, new CommandEventHandler(Tame_OnCommand));
        }

        [Usage("Tame")]
        [Description("Select the creature to tame.")]

        public static void Tame_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new TameTarget();
        }

        private class TameTarget : Target
        {
            public TameTarget()
                : base(-1, true, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BaseCreature)
                {
                    BaseCreature creature = o as BaseCreature;
                    creature.Controlled = true;
                    creature.ControlMaster = from;
                    creature.ControlOrder = OrderType.Come;
                }
                else
                {
                    from.SendMessage(32, "This is not a creature.");
                }
            }
        }
    }
}