using System;

namespace Server.Items
{
	public class NecroReadBook : BaseReadBook 
	{
		[Constructable]
		public NecroReadBook() : base ( ReadBooks.NECROMANCER )
		{
		}

		[Constructable]
		public NecroReadBook(String title, String author, int pageCount) : base( ReadBooks.NECROMANCER, title, author, pageCount) {}

		public NecroReadBook(Serial serial) : base(serial) {}

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

