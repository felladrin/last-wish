namespace Server.Items
{
    public class BloodMossPlant : BaseRegentPlant
    {
        [Constructable]
        public BloodMossPlant()
            : base(0xD13)
        {
            Name = "Blood Moss Plant";
            Hue = 32;
        }

        public override void OnDoubleClick(Mobile from)
        {
            HarvestPlant(from, new Bloodmoss(), this);
        }

        public BloodMossPlant(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

    }
}