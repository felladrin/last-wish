////////////////////////////
//     Watercontainers    //
//          by            //
//         Liacs          //
////////////////////////////

namespace Server.Items
{
    public class WaterBarrelAddon : BaseAddon, IWaterSource
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new WaterBarrelAddonDeed();
			}
		}

		[ Constructable ]
		public WaterBarrelAddon()
		{
			AddComponent( new AddonComponent( 5453 ), 0, 0, 0 );
            Name = "Water Barrel";

		}

        public int Quantity
        {
            get { return 200; }
            set { }
        }

		public WaterBarrelAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class WaterBarrelAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new WaterBarrelAddon();
			}
		}

		[Constructable]
		public WaterBarrelAddonDeed()
		{
            Name = "Water Barrel";
		}

		public WaterBarrelAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}