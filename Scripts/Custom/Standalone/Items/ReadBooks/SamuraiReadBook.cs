using System;

namespace Server.Items
{
	public class SamuraiReadBook : BaseReadBook 
	{
		[Constructable]
		public SamuraiReadBook() : base ( ReadBooks.SAMURAI )
		{
		}

		[Constructable]
		public SamuraiReadBook(String title, String author, int pageCount) : base( ReadBooks.SAMURAI, title, author, pageCount) {}

		public SamuraiReadBook(Serial serial) : base(serial) {}

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


