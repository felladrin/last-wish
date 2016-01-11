// WalkTo Command v1.1.0
// Author: Felladrin
// Started: 2013-08-21
// Updated: 2016-01-10

using System;
using Server;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Felladrin.Commands
{
    public static class WalkTo
    {
        public static class Config
        {
            public static bool Enabled = true;                                   // Is this command enabled?
            public static TimeSpan AutoDeleteDelay = TimeSpan.FromMinutes(1);    // What's the delay to auto-delete waypoints created by this command?
        }

        public static void Initialize()
        {
            if (Config.Enabled)
                CommandSystem.Register("WalkTo", AccessLevel.GameMaster, new CommandEventHandler(OnCommand));
        }

        [Usage("WalkTo")]
        [Description("Commands a NPC to walk to a targeted spot. If Multiple Waypoints Mode is enabled, you can sequentially target spots to make the creature follow a complex path.")]
        static void OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new WalkToTarget();
            e.Mobile.SendMessage(68, "What creature you want to make walk?");
        }

        class WalkToTarget : Target
        {
            public WalkToTarget() : base(-1, true, TargetFlags.None) { }

            protected override void OnTarget(Mobile from, object targeted)
            {
                BaseCreature baseCreature = targeted as BaseCreature;

                if (baseCreature == null)
                {
                    from.SendMessage(38, "You can only move NPCs!");
                    return;
                }

                from.SendMessage(68, "Now select where it should walk to.");
                from.Target = new AddWalkWayPointTarget(baseCreature);
            }
        }

        class AddWalkWayPointTarget : Target
        {
            WayPoint prevWayPoint;
            BaseCreature baseCreature;

            public AddWalkWayPointTarget(BaseCreature bc, WayPoint wp = null) : base(-1, true, TargetFlags.None)
            {
                prevWayPoint = wp;
                baseCreature = bc;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                IPoint3D spot = targeted as IPoint3D;

                if (spot == null)
                    return;

                SpellHelper.GetSurfaceTop(ref spot);

                Point3D location = new Point3D(spot);

                WayPoint wp = (prevWayPoint != null) ? new WalkWayPoint(prevWayPoint) : new WalkWayPoint();

                wp.MoveToWorld(location, from.Map);

                baseCreature.Frozen = false;
                baseCreature.CantWalk = false;
                baseCreature.CurrentSpeed = 0.2;

                if (baseCreature.CurrentWayPoint == null)
                    baseCreature.CurrentWayPoint = wp;

                Timer.DelayCall(Config.AutoDeleteDelay, delegate
                    {
                        wp.Delete();

                        if (baseCreature.CurrentWayPoint == null)
                            baseCreature.CurrentSpeed = baseCreature.PassiveSpeed;
                    }
                );

                from.Target = new AddWalkWayPointTarget(baseCreature, wp);
            }
        }

        class WalkWayPoint : WayPoint
        {
            public override string DefaultName { get { return "Walk Way Point"; } }

            public override bool DisplayWeight { get { return false; } }

            public WalkWayPoint() { }

            public WalkWayPoint(WayPoint prev) : base(prev) { }

            public WalkWayPoint(Serial serial) : base(serial) { }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                reader.ReadInt();
                Delete();
            }
        }
    }
}