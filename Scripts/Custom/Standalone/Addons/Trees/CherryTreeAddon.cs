using System;
using Server;

namespace Server.Items
{
	public class CherryTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new CherryTreeDeed(); } }

		[Constructable]
		public CherryTreeAddon()
		{
            Name = "Cherry Tree";
			AddComponent( new AddonComponent( 0x2477 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x2472 ), 0, 0, 0 );
		}

		public CherryTreeAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class CherryTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new CherryTreeAddon(); } }
		//public override int LabelNumber{ get{ return 1076268; } } // Cherry Tree

		[Constructable]
		public CherryTreeDeed() 
		{
			Name = "Cerejeira Florida";
			Hue = 25;
		}

		public CherryTreeDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
