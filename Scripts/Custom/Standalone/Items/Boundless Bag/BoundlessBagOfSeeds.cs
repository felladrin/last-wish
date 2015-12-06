//   ___|========================|___
//   \  |  Written by Felladrin  |  /   This script was released on RunUO Community under the GPL licensing terms.
//    > |      August 2013       | <
//   /__|========================|__\   [Boundless Bag of Seeds] - Current version: 1.0 (August 17, 2013)

using System;
using Server.Engines.Plants;

namespace Server.Items
{
    class BoundlessBagOfSeeds : BoundlessBag
    {
        [Constructable]
        public BoundlessBagOfSeeds() : base()
        {
            Name = "Boundless Bag of Seeds";
            Hue = 378;
        }

        public override Type[] AllowedItemTypes
        {
            get
            {
                return new Type[]
                {
                    typeof(Seed)
                };
            }
        }

        public BoundlessBagOfSeeds(Serial serial) : base(serial) { }

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
