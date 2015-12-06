namespace Server.Items
{
    public class MandrakePlant : BaseRegentPlant
    {
        [Constructable]
        public MandrakePlant()
            : base(0x18DF)
        {
            Name = "Mandrake Plant";
        }

        public override void OnDoubleClick(Mobile from)
        {
            HarvestPlant(from, new MandrakeRoot(), this);
        }

        public MandrakePlant(Serial serial)
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