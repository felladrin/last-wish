//   ___|========================|___
//   \  |  Written by Felladrin  |  /	This script was released on RunUO Forums under the GPL licensing terms.
//    > |       June 2010        | <
//   /__|========================|__\	[Recall Command] - Current version: 1.2 (July 8, 2013)

using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Commands
{
    public class Recall
    {
        public static void Initialize()
        {
            CommandSystem.Register("Recall", AccessLevel.Player, new CommandEventHandler(Recall_OnCommand));
        }

        [Usage("Recall")]
        [Description("Takes you back to where it all began.")]
        private static void Recall_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

            if (Server.Misc.WeightOverloading.IsOverloaded(m))
            {
                m.SendMessage("You can't recall because you are carrying too much weight!");
                return;
            }

            SendEffects(m);

            CityInfo city = new CityInfo("Lake Shire", "Center", 1202, 1116, -25, Map.Ilshenar);

            m.MoveToWorld(city.Location, city.Map);

            m.Direction = Direction.North;

            PlayerMobile master = (PlayerMobile)m;
            List<Mobile> pets = master.AllFollowers;

            if (pets.Count > 0)
            {
                for (int i = 0; i < pets.Count; ++i)
                {
                    Mobile pet = (Mobile)pets[i];

                    if (pet is IMount)
                    {
                        if (((IMount)pet).Rider != master)
                        {
                            ((IMount)pet).Rider = null; // make sure it's dismounted
                        }
                        else
                        {
                            continue;
                        }
                    }

                    SendEffects(pet);
                    pet.MoveToWorld(master.Location, master.Map);
                }
            }

            SendEffects(m);

            m.Emote("*" + m.Name + " appears in a puff of smoke*");
        }

        private static void SendEffects(Mobile m)
        {
            Effects.SendLocationEffect(new Point3D(m.X + 1, m.Y, m.Z + 4), m.Map, 0x3728, 13);
            Effects.SendLocationEffect(new Point3D(m.X + 1, m.Y, m.Z), m.Map, 0x3728, 13);
            Effects.SendLocationEffect(new Point3D(m.X + 1, m.Y, m.Z - 4), m.Map, 0x3728, 13);
            Effects.SendLocationEffect(new Point3D(m.X, m.Y + 1, m.Z + 4), m.Map, 0x3728, 13);
            Effects.SendLocationEffect(new Point3D(m.X, m.Y + 1, m.Z), m.Map, 0x3728, 13);
            Effects.SendLocationEffect(new Point3D(m.X, m.Y + 1, m.Z - 4), m.Map, 0x3728, 13);
            Effects.SendLocationEffect(new Point3D(m.X + 1, m.Y + 1, m.Z + 11), m.Map, 0x3728, 13);
            Effects.SendLocationEffect(new Point3D(m.X + 1, m.Y + 1, m.Z + 7), m.Map, 0x3728, 13);
            Effects.SendLocationEffect(new Point3D(m.X + 1, m.Y + 1, m.Z + 3), m.Map, 0x3728, 13);
            Effects.SendLocationEffect(new Point3D(m.X + 1, m.Y + 1, m.Z - 1), m.Map, 0x3728, 13);

            m.PlaySound(0x228);
        }
    }
}