using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//bomberman upgrades are upgrades that are randomly found while demolishing the game field
	public abstract class BombermanUpgrade : BombermanObstacle
	{
		public override bool Destructable{ get{ return true; } }
		
		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement


		protected DecayTimer _DecayTimer;

		
		public BombermanUpgrade( int itemid, string name ) : base( itemid, name )
		{
			StartDecayTimer();
		}
		
		
		//deserialize constructor
		public BombermanUpgrade( Serial serial ) : base( serial )
		{
		}
		
		protected void StartDecayTimer()
		{
			_DecayTimer = new DecayTimer( this );
			_DecayTimer.Start();
			
		}
		
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			base.OnMovement( m, oldLocation );
			
			if( BoardGameControlItem == null )
			{
				return;
			}
			
			//ignore anyone who is not a player of this game
			if( BoardGameControlItem.Players.IndexOf( m ) == -1 )
			{
				return;
			}

			if( m.Location == oldLocation )
				return;

			if( m.InRange( this, 0 ) )
			{
				Upgrade( m );
				Destroy();
			}
		}
		
		protected virtual void Upgrade( Mobile m )
		{
		string[] upgrade = this.ToString().Split(new char[]{' '}); 
			m.SendMessage( "You picked up an upgrade: " );
			m.SendMessage(upgrade[1]);
			m.PlaySound( m.Female ? 0x337 : 0x44A );
		}
		
		public override void OnAfterDelete()
		{
			if( _DecayTimer != null )
			{
				_DecayTimer.Stop();
				_DecayTimer = null;
			}
			
			base.OnAfterDelete();
			
		}

		
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			StartDecayTimer();
		}
		
		protected class DecayTimer : Timer
		{
			private BombermanUpgrade _BombermanUpgrade;

			public DecayTimer( BombermanUpgrade upgrade ) : base( TimeSpan.FromSeconds( BombermanSettings.UPGRADE_DECAY_DELAY ), TimeSpan.FromSeconds( 1.0 ) )
			{
				_BombermanUpgrade = upgrade;
			}

			protected override void OnTick()
			{
				_BombermanUpgrade.Destroy();
			}
		}

		public static BombermanUpgrade GetRandomUpgrade(bool speed, bool detonator, bool badaBoom, bool blast)
		{
			
			
			double prizeroll = Utility.RandomDouble();
			
			if( (prizeroll < .1 ) && (speed==true))
			{
				return new SpeedUpgrade();
				
			}
			else if( (prizeroll < .15 ) &&(detonator == true))
			{
				return new DetonatorUpgrade();
			}
			else if( (prizeroll < .2 ) && (badaBoom == true))
			{
				return new BaddaBoomUpgrade();
			}
			else if(( prizeroll < .6 ) && (blast == true))
			{
				return new BlastStrengthUpgrade();
			}
			else
			{
				return new BombCountUpgrade();
			}

			
		}
	}
}