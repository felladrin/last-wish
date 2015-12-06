using System;

namespace Server.Items
{
	[Flipable( 0x2B02, 0x2B03 )]
	public class Quiver : BaseMiddleTorso
	{
		[Constructable]
		public Quiver() : base( 0x2B03 )
		{
			Name = "Quiver";
		}

		public Quiver( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}

