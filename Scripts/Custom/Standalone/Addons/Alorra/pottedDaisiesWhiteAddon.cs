////////////////////////////////////////
//                                     //
//   Generated by CEO's YAAAG - Ver 2  //
// (Yet Another Arya Addon Generator)  //
//    Modified by Hammerhand for       //
//      SA & High Seas content         //
//                                     //
////////////////////////////////////////

namespace Server.Items
{
    public class pottedDaisiesWhiteAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {6094, 0, 0, 4}// 11	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new pottedDaisiesWhiteAddonDeed();
			}
		}

		[ Constructable ]
		public pottedDaisiesWhiteAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 4551, 0, 0, 0, 1153, -1, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 3332, 0, 0, 3, 0, -1, "leaves", 1);// 2
			AddComplexComponent( (BaseAddon) this, 4180, 1, 0, 11, 1150, -1, "daisies", 1);// 3
			AddComplexComponent( (BaseAddon) this, 4179, 0, 0, 9, 1150, -1, "daisies", 1);// 4
			AddComplexComponent( (BaseAddon) this, 4179, 1, 0, 8, 1150, -1, "daisies", 1);// 5
			AddComplexComponent( (BaseAddon) this, 4180, 0, 1, 12, 1150, -1, "daisies", 1);// 6
			AddComplexComponent( (BaseAddon) this, 4179, 0, 0, 4, 1150, -1, "daisies", 1);// 7
			AddComplexComponent( (BaseAddon) this, 4180, 0, 0, 8, 1150, -1, "daisies", 1);// 8
			AddComplexComponent( (BaseAddon) this, 4179, 0, 1, 8, 1150, -1, "daisies", 1);// 9
			AddComplexComponent( (BaseAddon) this, 6093, 0, 0, 6, 0, -1, "daisies", 1);// 10

		}

		public pottedDaisiesWhiteAddon( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
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

	public class pottedDaisiesWhiteAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new pottedDaisiesWhiteAddon();
			}
		}

		[Constructable]
		public pottedDaisiesWhiteAddonDeed()
		{
			Name = "pottedDaisiesWhite";
		}

		public pottedDaisiesWhiteAddonDeed( Serial serial ) : base( serial )
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