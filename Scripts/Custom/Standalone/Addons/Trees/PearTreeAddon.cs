namespace Server.Items
{
    public class PearTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new PearTreeDeed(); } }

		[Constructable]
		public PearTreeAddon()
		{
            Name = "Pear Tree";
			AddComponent( new AddonComponent( 0xDA4 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0xDA6 ), 0, 0, 0 );
		}

		public PearTreeAddon( Serial serial ) : base( serial )
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

	public class PearTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new PearTreeAddon(); } }
		//public override int LabelNumber{ get{ return 1023492; } } // Pear Tree

		[Constructable]
		public PearTreeDeed() 
		{
			Name = "Pereira";
			Hue = 55;
		}

		public PearTreeDeed( Serial serial ) : base( serial )
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
