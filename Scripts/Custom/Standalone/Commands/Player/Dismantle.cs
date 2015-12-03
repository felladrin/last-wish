//   ___|========================|___
//   \  |  Written by Felladrin  |  /
//    > |      January 2008      | <
//   /__|========================|__\

using Server.Items;
using Server.Targeting;

namespace Server.Commands
{
    public class DismantleCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("Dismantle", AccessLevel.Player, new CommandEventHandler(Dismantle_OnCommand));
        }

        [Usage("Dismantle")]
        [Description("Dismantles an addon from your house.")]
        public static void Dismantle_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new DismantleTarget();
            e.Mobile.SendMessage("What do you want to dismantle?");
        }

        private class DismantleTarget : Target
        {
            public DismantleTarget()
                : base(2, false, TargetFlags.None)
            {
            }

            public virtual BaseAddonDeed Deed { get { return null; } }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IChopable)
                    ((IChopable)o).OnChop(from);
                else if (o is Item && FurnitureAttribute.Check((Item)o))
                    DismantleItem(from, (Item)o);
            }

            private void DismantleItem(Mobile from, Item item)
            {
                if (!from.InRange(item.GetWorldLocation(), 2))
                {
                    from.SendMessage("You need to be closer to it.");
                    return;
                }
                else if (!item.IsChildOf(from.Backpack) && !item.Movable)
                {
                    from.SendMessage("It can't be dismantled.");
                    return;
                }

                from.SendMessage("You dismantle the object.");
                Effects.PlaySound(item.GetWorldLocation(), item.Map, 0x3B3);

                if (item is Container)
                {
                    if (item is TrapableContainer)
                        (item as TrapableContainer).ExecuteTrap(from);

                    ((Container)item).Destroy();
                }
                else
                {
                    item.Delete();
                }
            }
        }
    }
}
