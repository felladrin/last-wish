using System;

namespace Server.Items
{
    public class AnkhReadBook : BaseReadBook
    {
        [Constructable]
        public AnkhReadBook()
            : base(ReadBooks.ANKH)
        {
        }

        [Constructable]
        public AnkhReadBook(String title, String author, int pageCount) : base(ReadBooks.ANKH, title, author, pageCount) { }

        public AnkhReadBook(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);			// Version;
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}


