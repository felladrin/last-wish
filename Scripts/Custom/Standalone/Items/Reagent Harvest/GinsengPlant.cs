namespace Server.Items
{
    public class GinsengPlant : BaseRegentPlant
    {
        [Constructable]
        public GinsengPlant()
            : base(0x18EA)
        {
            Name = "Ginseng Plant";
        }

        public override void OnDoubleClick(Mobile from)
        {
            HarvestPlant(from, new Ginseng(), this);
        }

        public GinsengPlant(Serial serial)
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