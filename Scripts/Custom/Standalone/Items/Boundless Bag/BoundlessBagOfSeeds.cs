// Boundless Bag Of Seeds v1.0.2
// Author: Felladrin
// Started: 2013-08-17
// Updated: 2016-01-24

using System;
using Server;
using Server.Engines.Plants;

namespace Felladrin.Items
{
    class BoundlessBagOfSeeds : BoundlessBag
    {
        [Constructable]
        public BoundlessBagOfSeeds()
        {
            Name = "Boundless Bag of Seeds";
            Hue = 378;
        }

        public override Type[] AllowedItemTypes { get { return new [] { typeof(Seed) }; } }

        public BoundlessBagOfSeeds(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}
