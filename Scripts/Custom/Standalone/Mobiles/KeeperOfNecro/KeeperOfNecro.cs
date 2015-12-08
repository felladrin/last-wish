using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class KeeperOfNecro : BaseVendor
	{
        private List<SBInfo> m_SBInfos = new List<SBInfo>();
        protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }

        [Constructable]
		public KeeperOfNecro() : base( "the Keeper of Necro" )
		{
			SetSkill( SkillName.Fencing, 75.0, 85.0 );
			SetSkill( SkillName.Macing, 75.0, 85.0 );
			SetSkill( SkillName.Swords, 75.0, 85.0 );
			SetSkill( SkillName.Necromancy, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBKeeperOfNecro() );
		}

		public override void InitOutfit()
		{
			AddItem( new DeathShroud() );
			AddItem( new ThighBoots() );
		}

		public KeeperOfNecro( Serial serial ) : base( serial )
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
}
