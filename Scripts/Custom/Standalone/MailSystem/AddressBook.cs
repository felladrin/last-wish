using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Menus;
using Server.Menus.Questions;
using Server.Mobiles;
using System.Collections;
using Server.Commands;
using Server.Targeting;

namespace Server.Items
{
    public class AddressBook : Item
    {
        private ArrayList m_Entries;
        [CommandProperty(AccessLevel.GameMaster)]
        public ArrayList Entries { get { return m_Entries; } set { m_Entries = value; InvalidateProperties(); } }
        private int m_Scrolls;
        private int m_Wax;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Scrolls
        {
            get { return m_Scrolls; }
            set { m_Scrolls = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Wax
        {
            get { return m_Wax; }
            set { m_Wax = value; InvalidateProperties(); }
        }

        public static void Initialize()
        {
            CommandSystem.Register("AllNames", AccessLevel.GameMaster, new CommandEventHandler(AllNames_OnCommand));
        }

        [Usage("AllNames")]
        [Description("Completely fills a targeted Address Book with names.")]
        private static void AllNames_OnCommand(CommandEventArgs e)
        {
            e.Mobile.BeginTarget(-1, false, TargetFlags.None, new TargetCallback(AllNames_OnTarget));
            e.Mobile.SendMessage("Target the Address Book to fill.");
        }

        private static void AllNames_OnTarget(Mobile from, object obj)
        {
            if (obj is AddressBook)
            {
                int count = 0;
                AddressBook book = (AddressBook)obj;

                if (book.Entries != null)

                    book.Entries.Clear();
                else
                    book.Entries = new ArrayList();
                foreach (Mobile m in World.Mobiles.Values)
                {
                    PlayerMobile mp = m as PlayerMobile;
                    if (mp != null)
                    {
                        count += 1;
                        book.AddEntry(mp);
                    }
                }

                from.SendMessage("The Address Book has been filled with " + count + " entries");

                CommandLogging.WriteLine(from, "{0} {1} filling Address Book {2}", from.AccessLevel, CommandLogging.Format(from), CommandLogging.Format(book));
            }
            else
            {
                from.BeginTarget(-1, false, TargetFlags.None, new TargetCallback(AllNames_OnTarget));
                from.SendMessage("That is not a Address Book. Try again.");
            }
        }

        [Constructable]
        public AddressBook()
            : base(0x2254)
        {
            Weight = 1.0;
            LootType = LootType.Blessed;
            Movable = true;
            Name = "Address Book";
        }

        public AddressBook(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add(1060658, "{0}\t{1}", "Sealing Wax", m_Wax); // Display from
            list.Add(1060659, "{0}\t{1}", "Scrolls", m_Scrolls); // Display to
            if (Entries != null)
                list.Add(1060660, "{0}\t{1}", "Entries", Entries.Count); // Display to
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (m_Scrolls != 0 && m_Wax != 0)
            {
                ArrayList entries = this.Entries;
                if (Entries == null)
                    Entries = new ArrayList();
                from.CloseGump(typeof(AddressBookGump));
                from.SendGump(new AddressBookGump(from, 0, entries, null, this));
            }
            else if (m_Scrolls <= 1)
            {
                m_Scrolls = 0;
                from.SendMessage("This book has no more writing paper in it. Add another scroll.");
            }
            else if (m_Wax <= 1)
            {
                m_Wax = 0;
                from.SendMessage("This book has no more wax in it. Add another block of sealing wax.");
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is BlankScroll)
            {
                from.SendMessage("You place an extra scroll in the book");
                m_Scrolls += dropped.Amount;
                dropped.Delete();
                this.Name = this.Name;
                return true;
            }
            else if (dropped is SealingWax)
            {
                from.SendMessage("You place an extra lump of wax in the book");
                m_Wax += dropped.Amount;
                dropped.Delete();
                this.Name = this.Name;
                return true;
            }
            return false;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2);
            if (m_Entries == null)
            {
                m_Entries = new ArrayList();
            }
            writer.Write(m_Entries.Count);
            for (int i = 0; i < m_Entries.Count; ++i)
                ((AdresseeEntry)m_Entries[i]).Serialize(writer);

            writer.Write(m_Scrolls);
            writer.Write(m_Wax);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            int count = reader.ReadInt();

            m_Entries = new ArrayList(count);

            for (int i = 0; i < count; ++i)
                m_Entries.Add(new AdresseeEntry(reader));
            
            m_Scrolls = reader.ReadInt();
            m_Wax = reader.ReadInt();
        }

        AdresseeEntry entry;

        public AdresseeEntry AddEntry(Mobile Owner)
        {

            AdresseeEntry be = new AdresseeEntry(Owner);
            InvalidateProperties();
            m_Entries.Add(be);
            return be;
        }

        public void AddEntry(AdresseeEntry be)
        {
            m_Entries.Add(be);
        }
    }

    public class AdresseeEntry
    {
        private Mobile m_Adressee;

        public Mobile Adressee { get { return m_Adressee; } }

        public AdresseeEntry(Mobile Owner)
        {
            m_Adressee = Owner;
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write((byte)0); // version

            writer.Write(m_Adressee);
        }

        public AdresseeEntry(GenericReader reader)
        {
            int version = reader.ReadByte();

            m_Adressee = reader.ReadMobile();
        }
    }
}
