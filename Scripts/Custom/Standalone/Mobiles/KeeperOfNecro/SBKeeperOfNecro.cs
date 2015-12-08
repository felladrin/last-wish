using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBKeeperOfNecro : SBInfo
	{
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBKeeperOfNecro()
		{
		}

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, 20, 0x2253, 0 ) );
				Add( new GenericBuyInfo( typeof( AnimateDeadScroll ), 300, 20, 0x2260, 0 ) );
				Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 80, 20, 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( HorrificBeastScroll ), 400, 20, 0x2265, 0 ) );
				Add( new GenericBuyInfo( typeof( PainSpikeScroll ), 150, 20, 0x2268, 0 ) );
				Add( new GenericBuyInfo( typeof( SummonFamiliarScroll ), 200, 20, 0x226B, 0 ) );
				Add( new GenericBuyInfo( typeof( WitherScroll ), 450, 20, 0x226E, 0 ) );
				Add( new GenericBuyInfo( typeof( BloodOathScroll ), 100, 20, 0x2261, 0 ) );
				Add( new GenericBuyInfo( typeof( EvilOmenScroll ), 100, 20, 0x2264, 0 ) );
				Add( new GenericBuyInfo( typeof( LichFormScroll ), 550, 20, 0x2266, 0 ) );
				Add( new GenericBuyInfo( typeof( PoisonStrikeScroll ), 450, 20, 0x2269, 0 ) );
				Add( new GenericBuyInfo( typeof( VampiricEmbraceScroll ), 800, 20, 0x226C, 0 ) );
				Add( new GenericBuyInfo( typeof( WraithFormScroll ), 200, 20, 0x226F, 0 ) );
				Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 100, 20, 0x2262, 0 ) );
				Add( new GenericBuyInfo( typeof( ExorcismScroll ), 600, 20, 0x2270, 0 ) );
				Add( new GenericBuyInfo( typeof( MindRotScroll ), 200, 20, 0x2267, 0 ) );
				Add( new GenericBuyInfo( typeof( StrangleScroll ), 500, 20, 0x226A, 0 ) );
				Add( new GenericBuyInfo( typeof( VengefulSpiritScroll ), 700, 20, 0x226D, 0 ) );
				Add( new GenericBuyInfo( typeof( BatWing ), 3, 500, 0xF78, 0 ) );
				Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, 500, 0xF7D, 0 ) );
				Add( new GenericBuyInfo( typeof( PigIron ), 5, 500, 0xF8A, 0 ) );
				Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, 500, 0xF8E, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveDust ), 3, 500, 0xF8F, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
}
