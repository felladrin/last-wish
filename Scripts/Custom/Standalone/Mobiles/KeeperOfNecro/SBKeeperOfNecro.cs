using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	public class SBKeeperOfNecro : SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBKeeperOfNecro()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, 20, 0xFFFF, 0 ) );
				Add( new GenericBuyInfo( typeof( AnimateDeadScroll ), 100, 20, 0x2260, 0 ) );
				Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 100, 20, 0x2263, 0 ) );
				Add( new GenericBuyInfo( typeof( HorrificBeastScroll ), 100, 20, 0x2265, 0 ) );
				Add( new GenericBuyInfo( typeof( PainSpikeScroll ), 100, 20, 0x2268, 0 ) );
				Add( new GenericBuyInfo( typeof( SummonFamiliarScroll ), 100, 20, 0x226B, 0 ) );
				Add( new GenericBuyInfo( typeof( WitherScroll ), 100, 20, 0x226E, 0 ) );
				Add( new GenericBuyInfo( typeof( BloodOathScroll ), 100, 20, 0x2261, 0 ) );
				Add( new GenericBuyInfo( typeof( EvilOmenScroll ), 100, 20, 0x2264, 0 ) );
				Add( new GenericBuyInfo( typeof( LichFormScroll ), 100, 20, 0x2266, 0 ) );
				Add( new GenericBuyInfo( typeof( PoisonStrikeScroll ), 100, 20, 0x2269, 0 ) );
				Add( new GenericBuyInfo( typeof( VampiricEmbraceScroll ), 100, 20, 0x226C, 0 ) );
				Add( new GenericBuyInfo( typeof( WraithFormScroll ), 100, 20, 0x226F, 0 ) );
				Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 100, 20, 0x2262, 0 ) );
				Add( new GenericBuyInfo( typeof( ExorcismScroll ), 100, 20, 0x2270, 0 ) );
				Add( new GenericBuyInfo( typeof( MindRotScroll ), 100, 20, 0x2267, 0 ) );
				Add( new GenericBuyInfo( typeof( StrangleScroll ), 100, 20, 0x226A, 0 ) );
				Add( new GenericBuyInfo( typeof( VengefulSpiritScroll ), 100, 20, 0x226D, 0 ) );
				Add( new GenericBuyInfo( typeof( BatWing ), 3, 999, 0xF78, 0 ) );
				Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, 999, 0xF7D, 0 ) );
				Add( new GenericBuyInfo( typeof( PigIron ), 5, 999, 0xF8A, 0 ) );
				Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, 999, 0xF8E, 0 ) );
				Add( new GenericBuyInfo( typeof( GraveDust ), 3, 999, 0xF8F, 0 ) );
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
