//   ___|========================|___
//   \  |  Written by Felladrin  |  /   This script was released on RunUO Forums under the GPL licensing terms.
//    > |      August 2013       | <
//   /__|========================|__\   [WalkTo Command] - Current version: 1.0 (August 21, 2013)

using System;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Commands
{
    public class WalkTo
    {
        public class Config
        {
            public static TimeSpan AutoDeleteDelay = TimeSpan.FromSeconds(30); // Time to auto-delete each waypoint added.
            public static bool MultipleWaypoints = true; // Allows to make a complex path for the mobile to follow.
        }

        public static void Initialize()
        {
            CommandSystem.Register("WalkTo", AccessLevel.GameMaster, new CommandEventHandler(WalkTo_OnCommand));
        }

        [Usage("WalkTo")]
        [Description("Commands a NPC to walk to target.")]
        private static void WalkTo_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new CreatureTarget();
            e.Mobile.SendMessage(68, "What creature you want to make walk?");
        }

        private class CreatureTarget : Target
        {
            public CreatureTarget() : base(-1, true, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (target is BaseCreature)
                {
                    BaseCreature bc = target as BaseCreature;
                    bc.CurrentSpeed = 0.2;
                    bc.Frozen = false;
                    from.SendMessage(68, "Now select where it should walk to.");
                    from.Target = new WalkToTarget(bc);
                }
                else
                {
                    from.SendMessage(38, "You can only move NPCs!");
                }
            }
        }

        private class WalkToTarget : Target
        {
            private BaseCreature creature;

            public WalkToTarget(BaseCreature bc) : base(-1, true, TargetFlags.None)
            {
                creature = bc;
            }

            protected override void OnTarget(Mobile from, object target)
            {
                IPoint3D p = target as IPoint3D;

                if (p == null)
                    return;

                Spells.SpellHelper.GetSurfaceTop(ref p);

                Point3D toLoc = new Point3D(p);

                WayPoint wp = new WalkWayPoint();

                wp.MoveToWorld(toLoc, from.Map);

                creature.CurrentWayPoint = wp;

                if (Config.MultipleWaypoints)
                {
                    from.SendMessage(68, "Select another spot before it reaches the last way point!");
                    from.Target = new AddWayPointTarget(wp);
                }
                else
                {
                    from.SendMessage(68, "You can now select another spot to change the its destination.");
                    from.Target = new WalkToTarget(creature);
                }
            }
        }

        private class AddWayPointTarget : Target
        {
            private WayPoint prevWayPoint;

            public AddWayPointTarget(WayPoint wp) : base(-1, true, TargetFlags.None)
            {
                prevWayPoint = wp;
            }

            protected override void OnTarget(Mobile from, object target)
            {
                IPoint3D p = target as IPoint3D;

                if (p == null)
                    return;

                Spells.SpellHelper.GetSurfaceTop(ref p);

                Point3D toLoc = new Point3D(p);

                WayPoint wp = new WalkWayPoint(prevWayPoint);

                wp.MoveToWorld(toLoc, from.Map);

                from.Target = new AddWayPointTarget(wp);
            }
        }

        private class WalkWayPoint : WayPoint
        {
            public WalkWayPoint() : base()
            {
                Name = "Walk Way Point";
                Timer.DelayCall(Config.AutoDeleteDelay, delegate { Delete(); });
            }

            public WalkWayPoint(WayPoint prev) : base(prev)
            {
                Name = "Walk Way Point";
                Timer.DelayCall(Config.AutoDeleteDelay, delegate { Delete(); });
            }

            public WalkWayPoint(Serial serial) : base(serial) { }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();
            }
        }
    }
}