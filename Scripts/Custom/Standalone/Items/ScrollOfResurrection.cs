//   ___|========================|___
//   \  |  Written by Felladrin  |  /
//    > |       July 2007        | <
//   /__|========================|__\

using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public class ScrollOfResurrection : Item
    {
        [Constructable]
        public ScrollOfResurrection() : this(1) { }

        [Constructable]
        public ScrollOfResurrection(int amount) : base(0x227B)
        {
            Name = "Scroll of Resurrection";
            Stackable = false;
            Weight = 1.0;
            Amount = amount;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.BeginTarget(2, false, TargetFlags.Beneficial, new TargetCallback(OnTarget));
                from.SendMessage("Who would you like to resurrect?");
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        public virtual void OnTarget(Mobile from, object obj)
        {
            Mobile mob;

            if (obj is Corpse)
            {
                Corpse corpse = obj as Corpse;
                mob = corpse.Owner;
            }
            else
            {
                mob = obj as Mobile;
            }

            if (mob == null)
            {
                from.SendMessage("This is not a living being!");
                return;
            }

            if (mob is BaseCreature && mob.IsDeadBondedPet)
            {
                BaseCreature bc = mob as BaseCreature;
                bc.PlaySound(0x214);
                bc.FixedEffect(0x376A, 10, 16);
                bc.ResurrectPet();

                this.Consume();
                from.Emote("*recites a scroll of resurrection*");
                from.SendMessage("The creature was brought back to life.");
            }

            if (mob is PlayerMobile)
            {
                if (mob.Alive)
                {
                    from.SendMessage("This creature is not dead!");
                    return;
                }
                else
                {
                    mob.PlaySound(0x214);
                    mob.FixedEffect(0x376A, 10, 16);
                    mob.Resurrect();
                }

                this.Consume();
                from.Emote("*recites a scroll of resurrection*");
                from.SendMessage("This person was brought back to life.");
            }
        }

        public ScrollOfResurrection(Serial serial) : base(serial) { }

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
