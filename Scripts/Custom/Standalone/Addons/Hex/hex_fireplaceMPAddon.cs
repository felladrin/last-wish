////////////////////////////////////////
//                                    //
//   Generated by CEO's YAAAG - V1.2  //
// (Yet Another Arya Addon Generator) //
//                                    //
////////////////////////////////////////

namespace Server.Items
{
    public class hex_fireplaceMPAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {33, 2, 0, 1}, {27, 0, 1, 1}, {29, 0, 0, 1}// 2	3	4	
			, {45, 2, 2, 0}, {28, 1, 0, 1}, {32, 0, 2, 1}// 5	6	7	
			, {13410, 1, 2, 0}, {13410, 2, 2, 0}, {7134, 2, 2, 4}// 8	9	10	
			, {13410, 2, 1, 0}, {37, 1, 2, 0}, {38, 2, 1, 0}// 11	12	13	
			, {45, 2, 2, 9}, {47, 1, 2, 9}, {46, 2, 1, 9}// 14	15	16	
			, {36, 1, 1, 11}, {46, 2, 1, 8}, {1311, 1, 2, 12}// 17	18	19	
			, {1311, 2, 2, 12}, {1311, 2, 1, 12}// 20	21	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new hex_fireplaceMPAddonDeed();
			}
		}

		[ Constructable ]
		public hex_fireplaceMPAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 1179, -1, -1, 0, 1891, -1, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 6571, 2, 2, 8, 0, 0, "", 1);// 22
			AddComplexComponent( (BaseAddon) this, 2253, 2, 2, 12, 642, -1, "", 1);// 23
			AddComplexComponent( (BaseAddon) this, 2252, 2, 1, 12, 642, -1, "", 1);// 24
			AddComplexComponent( (BaseAddon) this, 2254, 1, 2, 12, 642, -1, "", 1);// 25

		}

		public hex_fireplaceMPAddon( Serial serial ) : base( serial )
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

	public class hex_fireplaceMPAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new hex_fireplaceMPAddon();
			}
		}

		[Constructable]
		public hex_fireplaceMPAddonDeed()
		{
			Name = "Fireplace MP Corner";
		}

		public hex_fireplaceMPAddonDeed( Serial serial ) : base( serial )
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