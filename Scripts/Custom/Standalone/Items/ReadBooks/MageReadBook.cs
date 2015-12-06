using System;

namespace Server.Items
{
	[Flipable( 0xE3B, 0xEFA )]
	public class MageReadBook : BaseReadBook 
	{
		[Constructable]
		public MageReadBook() : base ( ReadBooks.MAGE )
		{
		}

		[Constructable]
		public MageReadBook(String title, String author, int pageCount) : base( ReadBooks.MAGE, title, author, pageCount) {}

		public MageReadBook(Serial serial) : base(serial) {}

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
