using System;

namespace Server.Items
{
	public class ElfReadBook : BaseReadBook 
	{
		[Constructable]
		public ElfReadBook() : base ( ReadBooks.ELF )
		{
		}

		[Constructable]
		public ElfReadBook(String title, String author, int pageCount) : base( ReadBooks.ELF, title, author, pageCount) {}
		
		public ElfReadBook(Serial serial) : base(serial) {}

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



