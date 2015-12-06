namespace Server.Items
{
    public class BaseRegentPlant : Item
    {

        public Item item;
        public Item item2;
        public Mobile m;
        public int held;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Held
        {
            get { return held; }
            set { held = value; }
        }

        public void HarvestPlant(Mobile from, Item i_item, Item i_item2)
        {
            item = i_item;
            item2 = i_item2;
            m = from;

            if (m.InRange(this.GetWorldLocation(), 2))
            {

                m.SendMessage("You harvest the plant.");

                item.Amount = held;

                AddHarvestToPack(m, item);

                item2.Delete();
            }
        }

        private void AddHarvestToPack(Mobile m_mob, Item i)
        {
            Container pack = m_mob.Backpack;

            m = m_mob;

            NewPackItem(i);
        }


        public void NewPackItem(Item item)
        {
            Container pack = m.Backpack;

            if (pack != null)
                pack.DropItem(item);
            else
                item.Delete();
        }

        public BaseRegentPlant(int itemID)
            : base(itemID)
        {
            Movable = false;
            held = Utility.RandomMinMax(1, 5);

            GrowthTimer newtmr = new GrowthTimer(this);
            newtmr.Start();
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("Quantity: " + held);
        }


        public BaseRegentPlant(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(held);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            held = reader.ReadInt();
        }
    }
}