namespace Server.Items
{
    public class DarkBrownTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new DarkBrownTreeDeed(); } }

		[Constructable]
		public DarkBrownTreeAddon()
		{
            Name = "Dark Brown Tree";
			AddComponent( new AddonComponent( 0xCCB ), 0, 0, 0 );
			
		}

		public DarkBrownTreeAddon( Serial serial ) : base( serial )
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

	public class DarkBrownTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new DarkBrownTreeAddon(); } }
		//public override int LabelNumber{ get{ return 1023492; } } // DarkBrown Tree

		[Constructable]
		public DarkBrownTreeDeed() 
		{
			Name = "�rvore Marrom-escura";
			Hue = 1117;
		}

		public DarkBrownTreeDeed( Serial serial ) : base( serial )
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
