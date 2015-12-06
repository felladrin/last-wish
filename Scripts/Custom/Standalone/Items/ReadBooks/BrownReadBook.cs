using System;

namespace Server.Items
{
	public class BrownReadBook : BaseReadBook 
	{
		[Constructable]
		public BrownReadBook() : base ( ReadBooks.BROWN )
		{
		}

		[Constructable]
		public BrownReadBook(String title, String author, int pageCount) : base( ReadBooks.BROWN, title, author, pageCount) {}

		public BrownReadBook(Serial serial) : base(serial) {}

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
