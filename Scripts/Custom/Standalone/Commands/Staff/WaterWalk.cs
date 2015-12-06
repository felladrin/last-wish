/*
 * Waterwalk by Kleanthes@eurebia.net
 *
 * Version 1.1 
 *
 * Just a little system to allow GMs to fly (kind of) or walk over water. 
 * Simply copy this files into your custom scripts folder and use the
 * [waterwalk command. 
 */

using Server.Commands;

namespace Server.Items
{
    public class WaterWalk : Item
    {

        public static void Initialize()
        {
            CommandSystem.Register("WaterWalk", AccessLevel.GameMaster, new CommandEventHandler(WaterWalk_onCommand));
        }

        public static void WaterWalk_onCommand(CommandEventArgs e)
        {
            WaterWalk w = new WaterWalk();
            w.Map = e.Mobile.Map;
            w.Location = e.Mobile.Location;
        }

        public WaterWalk[] others = new WaterWalk[4];
        public WaterWalk parent = null;

        [Constructable]
        public WaterWalk()
            : base(0x0519)
        {
            this.Movable = false;
            this.Visible = false;
            if (parent == null)
            {
                for (int i = 0; i < others.Length; i++)
                {
                    others[i] = new WaterWalk(this);
                }
            }

        }

        [Constructable]
        public WaterWalk(WaterWalk myParent)
            : base(0x0519)
        {
            parent = myParent;
            this.Visible = false;
            this.Movable = false;
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
            base.OnLocationChange(oldLocation);

            if (parent == null)
            {
                others[0].Location = new Point3D(X - 1, Y, Z);
                others[0].Map = Map;
                others[1].Location = new Point3D(X + 1, Y, Z);
                others[1].Map = Map;
                others[2].Location = new Point3D(X, Y + 1, Z);
                others[2].Map = Map;
                others[3].Location = new Point3D(X, Y - 1, Z);
                others[3].Map = Map;
            }
        }

        public override void Delete()
        {
            if (parent == null)
            {
                for (int i = 0; i < others.Length; i++)
                {
                    if (others[0] != null)
                    {
                        others[i].Delete();
                    }
                }
            }
            base.Delete();
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (parent != null)
            {
                if (this == parent.others[0])
                {
                    parent.X -= 1;
                }
                else if (this == parent.others[1])
                {
                    parent.X += 1;
                }
                else if (this == parent.others[2])
                {
                    parent.Y += 1;
                }
                else if (this == parent.others[3])
                {
                    parent.Y -= 1;
                }
            }
            return true;
        }

        public WaterWalk(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            // Do nothing, because it isn't serialized!
        }

        public override void Deserialize(GenericReader reader)
        {
            // Dito.
        }

    }
}
