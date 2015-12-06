using System;

namespace Server.Items
{
	public class TanReadBook : BaseReadBook 
	{
		[Constructable]
		public TanReadBook() : base ( ReadBooks.TAN )
		{
		}

		[Constructable]
		public TanReadBook(String title, String author, int pageCount) : base( ReadBooks.TAN, title, author, pageCount) {}

		public TanReadBook(Serial serial) : base(serial) {}

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
