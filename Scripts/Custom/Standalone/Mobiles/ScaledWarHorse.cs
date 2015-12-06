//   ___|========================|___
//   \  |  Written by Felladrin  |  /
//    > |        May 2008        | <
//   /__|========================|__\

using Server.Mobiles;

namespace Server.Custom.Mobiles
{
    [CorpseName("a war horse corpse")]
    public class ScaledWarHorse : BaseWarHorse
    {
        [Constructable]
        public ScaledWarHorse() : base(284, 0x3E92, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a scaled war horse";
        }

        public ScaledWarHorse(Serial serial) : base(serial) { }

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
