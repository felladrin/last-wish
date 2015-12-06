using System;

namespace Server.Items
{
	public class RedReadBook : BaseReadBook 
	{
		[Constructable]
		public RedReadBook() : base ( ReadBooks.RED )
		{
		}

		[Constructable]
		public RedReadBook(String title, String author, int pageCount) : base( ReadBooks.RED, title, author, pageCount) {}

		public RedReadBook(Serial serial) : base(serial) {}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize (writer);
			writer.Write( (int) 0);			// Version;
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize (reader);
			int version = reader.ReadInt();
		}
	}
}
