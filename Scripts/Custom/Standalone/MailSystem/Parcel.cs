using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    public class Parcel : TrapableContainer
    {
        private Mobile m_To;
        private Mobile m_From;

        public override int DefaultGumpID { get { return 0x102; } }
        public override int DefaultDropSound { get { return 0x48; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile To
        {
            get { return m_To; }
            set { m_To = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile From
        {
            get { return m_From; }
            set { m_From = value; InvalidateProperties(); }
        }

        public override Rectangle2D Bounds
        {
            get { return new Rectangle2D(44, 65, 142, 94); }
        }

        [Constructable]
        public Parcel()
            : base(0x232A)
        {
            Weight = 1.0;
        }

        public Parcel(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (m_To == from)
                base.OnDoubleClick(from);
            else
                from.SendMessage("This parcel is not for you!");
        }
        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (From != null)
                list.Add(1060658, "{0}\t{1}", "From", m_From.Name); // Display from
            if (To != null)
                list.Add(1060659, "{0}\t{1}", "To", m_To.Name); // Display to
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            writer.Write(m_To);
            writer.Write(m_From);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_To = reader.ReadMobile();
            m_From = reader.ReadMobile();
        }
    }
}
