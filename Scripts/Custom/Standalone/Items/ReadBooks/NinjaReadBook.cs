using System;

namespace Server.Items
{
	public class NinjaReadBook : BaseReadBook 
	{
		[Constructable]
		public NinjaReadBook() : base ( ReadBooks.NINJA )
		{
		}

		[Constructable]
		public NinjaReadBook(String title, String author, int pageCount) : base( ReadBooks.NINJA, title, author, pageCount) {}

		public NinjaReadBook(Serial serial) : base(serial) {}

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


