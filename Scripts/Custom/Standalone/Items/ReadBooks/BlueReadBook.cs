using System;

namespace Server.Items
{
	public class BlueReadBook : BaseReadBook 
	{
		[Constructable]
		public BlueReadBook() : base ( ReadBooks.BLUE )
		{
		}

		[Constructable]
		public BlueReadBook(String title, String author, int pageCount) : base( ReadBooks.BLUE, title, author, pageCount) {}

		public BlueReadBook(Serial serial) : base(serial) {}

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
