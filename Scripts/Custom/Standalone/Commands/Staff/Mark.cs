/*
 * Created by SharpDevelop.
 * User: alexanderfb
 * Date: 2/6/2005
 * Time: 12:09 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using Server.Items;
using Server.Targeting;

namespace Server.Commands
{
    public class MarkCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("Mark", AccessLevel.GameMaster, new CommandEventHandler(Mark_OnCommand));
        }

        [Usage("Mark")]
        [Description("Marks a targetted rune quickly and easily.")]
        public static void Mark_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new RuneTarget();
        }

        public class RuneTarget : Target
        {

            public RuneTarget()
                : base(12, false, TargetFlags.None)
            {
            }

            private void MarkRune(RecallRune r, Mobile from)
            {
                r.Marked = true;
                r.TargetMap = from.Map;
                r.Target = from.Location;
                r.House = null;
                from.PlaySound(0x1FA);
                if (r.IsChildOf(from.Backpack))
                {
                    Effects.SendLocationEffect(from, from.Map, 14201, 16);
                }
                else
                {
                    Effects.SendLocationEffect(r, r.Map, 14201, 16);
                }
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is RecallRune)
                {
                    MarkRune((RecallRune)o, from);
                }
                else
                {
                    from.SendLocalizedMessage(501797); // I cannot mark that object.
                }
            }
        }
    }
}
