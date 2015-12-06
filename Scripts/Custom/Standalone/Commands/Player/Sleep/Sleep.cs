//   ___|========================|___
//   \  |  Written by Felladrin  |  /
//    > |     December 2010      | <
//   /__|========================|__\

using System.Collections;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;

namespace Server.Commands
{
    public class DeitarComando
    {
        public static bool EscolheOndeDeitar = false;

        public static void Initialize()
        {
            CommandSystem.Register("Sleep", AccessLevel.Player, new CommandEventHandler(Deitar_OnCommand));
            CommandSystem.Register("Wake", AccessLevel.Player, new CommandEventHandler(Levantar_OnCommand));
        }

        [Usage("Sleep")]
        [Description("Yeah, everybody needs a break.")]
        public static void Deitar_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile as PlayerMobile;

            if (m.Hidden == false && m.Frozen == false)
            {
                if (Server.Spells.SpellHelper.CheckCombat(m))
                    m.SendMessage("Are you crazy!? Wanna sleep in the middle of the battle!?");
                else if (m.Spell != null)
                    m.SendMessage("You are too busy to sleep now.");
                else
                {
                    if (EscolheOndeDeitar)
                        m.Target = new OndeDeitar();
                    else
                    {
                        SleeperBedBody body = new SleeperBedBody(m, false, false, null);
                        body.MoveToWorld(m.Location, m.Map);

                        switch (m.Direction)
                        {
                            case Direction.North: body.Y += 1; body.Z += 2; break;
                            case Direction.South: body.Y -= 1; body.Z += 2; break;
                            case Direction.West: body.X += 1; body.Z += 2; break;
                            case Direction.East: body.X -= 1; body.Z += 2; break;
                            case Direction.Right: body.X -= 1; body.Y += 1; body.Z += 2; break;
                            case Direction.Left: body.X += 1; body.Y -= 1; body.Z += 2; break;
                            case Direction.Down: body.X -= 1; body.Y -= 1; body.Z += 2; break;
                            case Direction.Up: body.X += 1; body.Y += 1; body.Z += 2; break;
                        }

                        m.SendMessage("You prepare to sleep.");
                        m.Hidden = true;
                        m.Frozen = true;
                    }
                }
            }
            else
            {
                m.SendMessage("You dream you are sleeping.");
            }
        }

        [Usage("Wake")]
        [Description("Wake up, if you are sleeping.")]
        public static void Levantar_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile as PlayerMobile;

            ArrayList bodylist = new ArrayList();

            foreach (Item item in World.Items.Values)
            {
                if (item is SleeperBedBody)
                    bodylist.Add(item);
            }

            foreach (SleeperBedBody body in bodylist)
            {
                if (from.Equals(body.Owner))
                {
                    body.MoveToWorld(from.Location, from.Map);
                    from.Blessed = false;
                    from.Hidden = false;
                    from.Frozen = false;
                    from.Squelched = false;
                    from.Direction = body.Direction;
                    from.Animate(21, 6, 1, false, false, 0);
                    body.Delete();
                }
            }

            bodylist.Clear();
        }
    }

    public class OndeDeitar : Target
    {
        public OndeDeitar() : base(-1, true, TargetFlags.None) { }

        protected override void OnTarget(Mobile from, object targeted)
        {
            IPoint3D p = targeted as IPoint3D;

            if (p != null)
            {
                Point3D m_point = new Point3D(p);
                SleeperBedBody body = new SleeperBedBody(from, false, false, null);
                body.MoveToWorld(m_point, from.Map);
                from.SendMessage("You start sleeping.");
                from.Hidden = true;
                from.Frozen = true;
            }
            else
            {
                from.SendMessage("For some reason you can't sleep there!");
            }
        }
    }
}
