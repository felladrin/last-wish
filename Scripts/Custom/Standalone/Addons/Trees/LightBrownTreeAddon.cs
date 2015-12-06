using System;
using Server;

namespace Server.Items
{
	public class LightBrownTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new LightBrownTreeDeed(); } }

		[Constructable]
		public LightBrownTreeAddon()
		{
            Name = "Light Brown Tree";
			AddComponent( new AddonComponent( 0xCCC ), 0, 0, 0 );
			
		}

		public LightBrownTreeAddon( Serial serial ) : base( serial )
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

	public class LightBrownTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new LightBrownTreeAddon(); } }
		//public override int LabelNumber{ get{ return 1023492; } } // LightBrown Tree

		[Constructable]
		public LightBrownTreeDeed() 
		{
			Name = "Árvore Marrom-clara";
			Hue = 1037;
		}

		public LightBrownTreeDeed( Serial serial ) : base( serial )
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
