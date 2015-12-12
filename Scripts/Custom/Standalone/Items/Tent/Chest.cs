//=========================================//
// Created by Dupre                        //
// Thanks to:                              //
// Zippy                                   //
// Ike                                     //
// Ignacio                                 //
//                                         //
// For putting up with a 'tard like me :)  //
//=========================================//
using System;
using Server.Mobiles;

namespace Server.Items
{
    public class SecureTent : BaseContainer
	{ 
		private Mobile m_Player; 

		public override int DefaultGumpID{ get{ return 0x3C; } } 
		public override int DefaultDropSound{ get{ return 0x42; } } 

		public override Rectangle2D Bounds 
		{ 
			get{ return new Rectangle2D( 16, 51, 168, 73 ); } 
		} 

		public SecureTent( Mobile player) : base( 0xE80 ) 
		{ 
			this.LiftOverride = true;
			m_Player = player; 
			this.ItemID = 2482; 
			this.Visible = true; 
			this.Movable = false; 
			MaxItems = 100; 
		} 

		[CommandProperty( AccessLevel.GameMaster )] 
		public Mobile Player 
		{ 
			get 
			{ 
				return m_Player; 
			} 
			set 
			{ 
				m_Player = value; 
				InvalidateProperties(); 
			} 
		} 

		public override int MaxWeight 
		{ 
			get 
			{ 
				return 0; 
			} 
		} 

		public SecureTent( Serial serial ) : base(serial) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 

			writer.Write( m_Player ); 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 

			m_Player = (PlayerMobile)reader.ReadMobile(); 
		} 

		public override TimeSpan DecayTime 
		{ 
			get 
			{ 
				return TimeSpan.FromMinutes( 30.0 ); 
			} 
		}

		public override void AddNameProperty( ObjectPropertyList list ) 
		{ 
			if ( m_Player != null ) 
			list.Add( "A Secure Travel Bag" );
			else 
			base.AddNameProperty( list ); 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			if ( m_Player != null ) 
			{ 
				LabelTo( from, "A Secure Travel Bag");

				if ( CheckContentDisplay( from ) ) 
				LabelTo( from, "({0} items, {1} stones)", TotalItems, TotalWeight ); 
			} 
			else 
			{ 
				base.OnSingleClick( from ); 
			} 
		} 

		public override bool IsAccessibleTo( Mobile m ) 
		{ 
			if (( m==m_Player || m.AccessLevel >= AccessLevel.GameMaster))
				return true;
			else
				return false;
		} 
	} 
}
