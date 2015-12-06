namespace Server.Items
{
    public class GarlicPlant : BaseRegentPlant
    {
        [Constructable]
        public GarlicPlant()
            : base(0x18E1)
        {
            Name = "Garlic Plant";
        }

        public override void OnDoubleClick(Mobile from)
        {
            HarvestPlant(from, new Garlic(), this);
        }

        public GarlicPlant(Serial serial)
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