using System;

namespace Server.Items
{
	public class PaladinReadBook : BaseReadBook 
	{
		[Constructable]
		public PaladinReadBook() : base ( ReadBooks.PALADIN )
		{
		}

		[Constructable]
		public PaladinReadBook(String title, String author, int pageCount) : base( ReadBooks.PALADIN, title, author, pageCount) {}

		public PaladinReadBook(Serial serial) : base(serial) {}

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
