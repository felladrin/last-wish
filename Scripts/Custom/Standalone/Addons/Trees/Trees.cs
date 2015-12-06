
using System;
using Server;
using Server.Items;


namespace Server.Items
{
	public class CedarTree : BaseAddon
	{
		[Constructable]
		public CedarTree()
		{
			if ( Utility.RandomBool() )
			{
				// trunk
				AddComponent( new AddonComponent ( 3286 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3287 ), 0, 0, 0 );
			}
			else
			{
				// trunk
				AddComponent( new AddonComponent ( 3288 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3289 ), 0, 0, 0 );
			}
		}

		public CedarTree( Serial serial ) : base( serial )
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


// misspelling, oops.
	public class CederTree : BaseAddon
	{
		[Constructable]
		public CederTree()
		{
			if ( Utility.RandomBool() )
			{
				// trunk
				AddComponent( new AddonComponent ( 3286 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3287 ), 0, 0, 0 );
			}
			else
			{
				// trunk
				AddComponent( new AddonComponent ( 3288 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3289 ), 0, 0, 0 );
			}
		}

		public CederTree( Serial serial ) : base( serial )
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




	public class CypressTree : BaseAddon
	{
		[Constructable]
		public CypressTree()
		{
			switch ( Utility.Random( 4 ) )
			{
				case 0:
				// trunk
				AddComponent( new AddonComponent ( 3320 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3321 ), 0, 0, 0 );
				break;

				case 1:
				// trunk
				AddComponent( new AddonComponent ( 3323 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3324 ), 0, 0, 0 );
				break;

				case 2:
				// trunk
				AddComponent( new AddonComponent ( 3326 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3327 ), 0, 0, 0 );
				break;

				default:
				// trunk
				AddComponent( new AddonComponent ( 3329 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3330 ), 0, 0, 0 );
				break;

			}

		}

		public CypressTree( Serial serial ) : base( serial )
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






	public class OakTree : BaseAddon
	{
		[Constructable]
		public OakTree()
		{
			if ( Utility.RandomBool() )
			{
				// trunk
				AddComponent( new AddonComponent ( 3290 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3291 ), 0, 0, 0 );
			}
			else
			{
				// trunk
				AddComponent( new AddonComponent ( 3293 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3294 ), 0, 0, 0 );
			}
		}

		public OakTree( Serial serial ) : base( serial )
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



	public class OhiiTree : BaseAddon
	{
		[Constructable]
		public OhiiTree()
		{
			// tree
			AddComponent( new AddonComponent( 3230 ), 0, 0, 0 );
		}

		public OhiiTree( Serial serial ) : base( serial )
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



	public class SaplingTree : BaseAddon
	{
		[Constructable]
		public SaplingTree()
		{
			// tree
			AddComponent( new AddonComponent ( Utility.RandomList( 3305, 3306 ) ), 0, 0, 0 );
		}

		public SaplingTree( Serial serial ) : base( serial )
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

	public class SmallPalmTree : BaseAddon
	{
		[Constructable]
		public SmallPalmTree()
		{
			// tree
			AddComponent( new AddonComponent ( Utility.RandomMinMax( 3225, 3229 ) ), 0, 0, 0 );
		}

		public SmallPalmTree( Serial serial ) : base( serial )
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


	public class SpiderTree : BaseAddon
	{
		[Constructable]
		public SpiderTree()
		{
			// tree
			AddComponent( new AddonComponent ( 3273 ), 0, 0, 0 );
		}

		public SpiderTree( Serial serial ) : base( serial )
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



	public class TreeLeaves : BaseAddon
	{
		[Constructable]
		public TreeLeaves()
		{
			// leaves
			switch ( Utility.Random( 5 ) )
			{
				case 0: 
				AddComponent( new AddonComponent ( 6943 ), 0, 0, 0 );
				break;

				case 1: 
				AddComponent( new AddonComponent ( 6944 ), 0, 0, 0 );
				break;

				case 2: 
				AddComponent( new AddonComponent ( 6945 ), 0, 0, 0 );
				break;

				case 3: 
				AddComponent( new AddonComponent ( 6946 ), 0, 0, 0 );
				break;

				// leaf pile
				default:
				AddComponent( new AddonComponent ( 6947 ), 1, 1, 0 );
				AddComponent( new AddonComponent ( 6948 ), 1, 0, 0 );
				AddComponent( new AddonComponent ( 6949 ), 0, 0, 0 );
				AddComponent( new AddonComponent ( 6950 ), 0, 1, 0 );
				break;				
			}
		}

		public TreeLeaves( Serial serial ) : base( serial )
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

	public class TuscanyPineTree : BaseAddon
	{
		[Constructable]
		public TuscanyPineTree()
		{
			// tree
			AddComponent( new AddonComponent ( 7038 ), 0, 0, 0 );
		}

		public TuscanyPineTree( Serial serial ) : base( serial )
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


	public class WalnutTree : BaseAddon
	{
		[Constructable]
		public WalnutTree()
		{
			if ( Utility.RandomBool() )
			{
				// trunk
				AddComponent( new AddonComponent ( 3296 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3297 ), 0, 0, 0 );
			}
			else
			{
				// trunk
				AddComponent( new AddonComponent ( 3299 ), 0, 0, 0 );
				// leaves
				AddComponent( new AddonComponent ( 3300 ), 0, 0, 0 );
			}
		}

		public WalnutTree( Serial serial ) : base( serial )
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



	public class WillowTree : BaseAddon
	{
		[Constructable]
		public WillowTree()
		{
			// trunk
			AddComponent( new AddonComponent ( 3302 ), 0, 0, 0 );

			// leaves
			AddComponent( new AddonComponent ( 3303 ), 0, 0, 0 );

		}

		public WillowTree( Serial serial ) : base( serial )
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


	public class YuccaTree : BaseAddon
	{
		[Constructable]
		public YuccaTree()
		{
			// tree
			AddComponent( new AddonComponent ( Utility.RandomList( 3383, 3384 ) ), 0, 0, 0 );
		}

		public YuccaTree( Serial serial ) : base( serial )
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



/////////////////////////////////////////////////
//
// Automatically generated by the
// AddonGenerator script by Arya
//
/////////////////////////////////////////////////


	public class YewTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new YewTreeAddonDeed();
			}
		}

		[ Constructable ]
		public YewTreeAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 4807 );
			AddComponent( ac, 5, -4, 7 );
			ac = new AddonComponent( 4797 );
			AddComponent( ac, 4, -4, 0 );
			ac = new AddonComponent( 4806 );
			AddComponent( ac, 4, -3, 7 );
			ac = new AddonComponent( 4805 );
			AddComponent( ac, 3, -2, 7 );
			ac = new AddonComponent( 4804 );
			AddComponent( ac, 2, -1, 7 );
			ac = new AddonComponent( 4803 );
			AddComponent( ac, 1, -1, 7 );
			ac = new AddonComponent( 4802 );
			AddComponent( ac, 0, 0, 7 );
			ac = new AddonComponent( 4801 );
			AddComponent( ac, -1, 1, 7 );
			ac = new AddonComponent( 4800 );
			AddComponent( ac, -2, 2, 7 );
			ac = new AddonComponent( 4799 );
			AddComponent( ac, -3, 3, 7 );
			ac = new AddonComponent( 4798 );
			AddComponent( ac, -4, 4, 7 );
			ac = new AddonComponent( 4798 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 4796 );
			AddComponent( ac, 3, -3, 0 );
			ac = new AddonComponent( 4795 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 4794 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 4793 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 4792 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 4791 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 4789 );
			AddComponent( ac, -3, 3, 0 );

		}

		public YewTreeAddon( Serial serial ) : base( serial )
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

	public class YewTreeAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new YewTreeAddon();
			}
		}

		[Constructable]
		public YewTreeAddonDeed()
		{
			Name = "YewTree";
		}

		public YewTreeAddonDeed( Serial serial ) : base( serial )
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
