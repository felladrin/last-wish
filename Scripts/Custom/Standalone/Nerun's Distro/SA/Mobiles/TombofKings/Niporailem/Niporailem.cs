using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName( "the corpse of niporailem" )]
	public class Niporailem : BaseCreature
	{
		[Constructable]
		public Niporailem() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "niporailem";
			Title = "the thief";

			Body = 722; 

			SetStr( 1000, 1200 );
			SetDex( 1200 );
			SetInt( 1200 );

			SetHits( 2654, 3988 );

			SetDamage( 15, 27 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );

			SetResistance( ResistanceType.Physical, 42 );
			SetResistance( ResistanceType.Fire, 29 );
			SetResistance( ResistanceType.Cold, 53, 56 );
			SetResistance( ResistanceType.Poison, 23, 24 );
			SetResistance( ResistanceType.Energy, 34, 39 );

			SetSkill( SkillName.MagicResist, 87.7, 93.5 );
			SetSkill( SkillName.Tactics, 56.1, 65.0 );
			SetSkill( SkillName.Wrestling, 68.8, 76.2 );

			PackNecroReg( 12, 24 ); /// Stratics didn't specify

            		Fame = 15000;
            		Karma = -15000;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 6 );
            		AddLoot(LootPack.Gems, 6);
		}

		public override int Meat{ get{ return 1; } }
          	public override bool AlwaysMurderer{ get{ return true; } }

		public override int GetIdleSound() { return 1609; } 
		public override int GetAngerSound() { return 1606; } 
		public override int GetHurtSound() { return 1608; } 
		public override int GetDeathSound()	{ return 1607; }




		public Niporailem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public void SpawnSpectralArmour( Mobile m )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			SpectralArmour spawned = new SpectralArmour();

			spawned.Team = this.Team;

			bool validLocation = false;
			Point3D loc = this.Location;

			for ( int j = 0; !validLocation && j < 10; ++j )
			{
				int x = X + Utility.Random( 3 ) - 1;
				int y = Y + Utility.Random( 3 ) - 1;
				int z = map.GetAverageZ( x, y );

				if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
					loc = new Point3D( x, y, Z );
				else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
					loc = new Point3D( x, y, z );
			}

			spawned.MoveToWorld( loc, map );
			spawned.Combatant = m;
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( this.Hits > (this.HitsMax / 4) )
			{
				if ( 0.25 >= Utility.RandomDouble() )
					SpawnSpectralArmour( attacker );
			}
			else if ( 0.10 >= Utility.RandomDouble() )
			{
				SpawnSpectralArmour( attacker );
			}
		}

		private DateTime m_NextTreasure;
		private int m_Thrown;

		public override void OnActionCombat()
		{
			Mobile combatant = Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != Map || !InRange( combatant, 12 ) || !CanBeHarmful( combatant ) || !InLOS( combatant ) )
				return;

			if ( DateTime.Now >= m_NextTreasure )
			{
				ThrowTreasure( combatant );

				m_Thrown++;

				if ( 0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1 ) // 75% chance to toss a second one
					m_NextTreasure = DateTime.Now + TimeSpan.FromSeconds( 3.0 );
				else
					m_NextTreasure = DateTime.Now + TimeSpan.FromSeconds( 5.0 + (10.0 * Utility.RandomDouble()) ); // 5-15 seconds
			}
		}

		public void ThrowTreasure( Mobile m )
		{
			DoHarmful( m );

			this.MovingParticles( m, 0xEEF, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );

			new InternalTimer( m, this ).Start();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile, m_From;

			public InternalTimer( Mobile m, Mobile from ) : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Mobile = m;
				m_From = from;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				m_Mobile.PlaySound( 0x033 );
				m_Mobile.AddToBackpack( new NiporailemsTreasure() );
				m_Mobile.SendLocalizedMessage( 1112111 ); // To steal my gold? To give it freely!
			}
		}

	}
}