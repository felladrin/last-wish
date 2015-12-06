using System;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{

	[FlipableAttribute( 0xFB5, 0xFB4 )]
	public class FencingHammer : Item
	{

		[Constructable]
		public FencingHammer() : base( 0xFB5 )
		{
			Layer = Layer.OneHanded;
		}

		public FencingHammer( Serial serial ) : base( serial )
		{
		}

		public override bool OnEquip( Mobile from ) 
		{
			return true;
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

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage ("Target the fence");
			from.Target = new FencingTarget();
		}

		private class FencingTarget : Target
		{
			public FencingTarget() : base( 10, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if ( targ is NorthGate || targ is WestGate || targ is  NorthFence || targ is EastFence || targ is FenceCorner || targ is FencePost)
				{
					Item fg = (Item) targ;

					if ( fg.Movable == true )
					{
						from.SendMessage("You set the fence into the ground");
						fg.Movable = false;
					}
					else
					{
						from.SendMessage("You knock it free from the ground");
						fg.Movable = true;
					}
				}
				else
				{
					from.SendMessage ("You can't use do that");
				}
			}
		}
	}


// -------------------- Gates ----------------------

	public abstract class FenceGate : BaseDoor
	{
		private Mobile      m_Owner;  // i added for ownership

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[Constructable]
		public FenceGate(DoorFacing facing): base(0x866 + (2 * (int)facing), 0x867 + (2 * (int)facing), 0xEB, 0xF2, BaseDoor.GetOffset(facing))
		{
			Weight = 50.0;
			Movable = true;
		}

		public FenceGate(Serial serial): base(serial)
		{
		}

		public override bool Decays { get { return false; } }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) && m_Owner == null )
			{
				m_Owner = from;
				from.SendMessage( "This is yours now" );
			}
			else if ( !IsChildOf( from.Backpack) && m_Owner == null )
			{
				from.SendMessage("This must be in your backpack to claim ownership");
			}
			else if ( !IsChildOf( from.Backpack))
				Use( from );
		}

		public string GetResponseFor( String text, Mobile from )
		{
			if (text == "release" && ((from == m_Owner) || from.AccessLevel >= AccessLevel.GameMaster ))
			{
				Movable = true;
				return "Released";
			}
			if (text == "lock" && ((from == m_Owner) || from.AccessLevel >= AccessLevel.GameMaster ))
			{
				Movable = false;
				return "Locked";
			}
			return text;
		}
	}

	public class NorthGate : FenceGate
	{
		[Constructable]
		public NorthGate () : base(DoorFacing.NorthCCW) 
		{
		}

		public NorthGate ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class WestGate : FenceGate
	{
		[Constructable]
		public WestGate (  ) : base( DoorFacing.WestCW )
		{
		}

		public WestGate ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

// ---------------------- Fences -----------------------
	public class NorthFence : Item
	{
		private Mobile      m_Owner;  // i added for ownership

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[Constructable]
		public NorthFence () : base( 0x88B )
		{
			Name = "a fence";
			Weight = 50.0;
			Movable = true;
		}

		public NorthFence ( Serial serial ) : base( serial )
		{
		}

		public override bool Decays { get { return false; } }

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) && m_Owner == null )
			{
				m_Owner = from;
				from.SendMessage( "Only you may move this now" );
			}
/*
			else if ( !IsChildOf( from.Backpack) && m_Owner == from && Movable == false )
			{
				Movable = true;
				from.SendMessage("You release it from the ground");
			}
			else if ( !IsChildOf( from.Backpack) && m_Owner == from && Movable == true )
			{
				Movable = false;
				from.SendMessage("You set it into the ground");
			}
*/
			else if ( !IsChildOf( from.Backpack) && m_Owner != from && (m_Owner != null))
			{
				from.SendMessage("This doesn't belong to you");
			}

			else if ( !IsChildOf( from.Backpack) && m_Owner == null )
			{
				from.SendMessage("This must be in your backpack to claim ownership");
			}
		}

		public override bool OnDragLift( Mobile from )
		{
			if ((from == this.Owner)|| from.AccessLevel >= AccessLevel.GameMaster )
			{
				return base.OnDragLift(from);
			}
			else
			{
				from.SendMessage("This doesn't belong to you");
				return false;
			}
		}
	}

	public class EastFence : Item
	{
		private Mobile      m_Owner;  // i added for ownership

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[Constructable]
		public EastFence () : base( 0x88A )
		{
			Name = "a fence";
			Weight = 50.0;
			Movable = true;
		}

		public EastFence ( Serial serial ) : base( serial )
		{
		}

		public override bool Decays { get { return false; } }

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) && m_Owner == null )
			{
				m_Owner = from;
				from.SendMessage( "Only you may move this now" );
			}
/*
			else if ( !IsChildOf( from.Backpack) && m_Owner == from && Movable == false )
			{
				Movable = true;
				from.SendMessage("You release it from the ground");
			}
			else if ( !IsChildOf( from.Backpack) && m_Owner == from && Movable == true )
			{
				Movable = false;
				from.SendMessage("You set it into the ground");
			}
*/
			else if ( !IsChildOf( from.Backpack) && m_Owner != from && (m_Owner != null) )
			{
				from.SendMessage("This doesn't belong to you");
			}

			else if ( !IsChildOf( from.Backpack) && m_Owner == null )
			{
				from.SendMessage("This must be in your backpack to claim ownership");
			}
		}

		public override bool OnDragLift( Mobile from )
		{
			if ((from == this.Owner)|| from.AccessLevel >= AccessLevel.GameMaster )
			{
				return base.OnDragLift(from);
			}
			else
			{
				from.SendMessage("This doesn't belong to you");
				return false;
			}
		}
	}

	public class FenceCorner : Item
	{
		private Mobile      m_Owner;  // i added for ownership

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[Constructable]
		public FenceCorner () : base( 0x862 )
		{
			Name = "a corner fence";
			Weight = 50.0;
			Movable = true;
		}

		public FenceCorner ( Serial serial ) : base( serial )
		{
		}

		public override bool Decays { get { return false; } }

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) && m_Owner == null )
			{
				m_Owner = from;
				from.SendMessage( "Only you may move this now" );
			}
/*
			else if ( !IsChildOf( from.Backpack) && m_Owner == from && Movable == false )
			{
				Movable = true;
				from.SendMessage("You release it from the ground");
			}
			else if ( !IsChildOf( from.Backpack) && m_Owner == from && Movable == true )
			{
				Movable = false;
				from.SendMessage("You set it into the ground");
			}
*/
			else if ( !IsChildOf( from.Backpack) && m_Owner != from && (m_Owner != null) )
			{
				from.SendMessage("This doesn't belong to you");
			}

			else if ( !IsChildOf( from.Backpack) && m_Owner == null )
			{
				from.SendMessage("This must be in your backpack to claim ownership");
			}
		}

		public override bool OnDragLift( Mobile from )
		{
			if ((from == this.Owner)|| from.AccessLevel >= AccessLevel.GameMaster )
			{
				return base.OnDragLift(from);
			}
			else
			{
				from.SendMessage("This doesn't belong to you");
				return false;
			}
		}
	}

	public class FencePost : Item
	{
		private Mobile      m_Owner;  // i added for ownership

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[Constructable]
		public FencePost() : base( 0x85F )
		{
			Name = "a fence post";
			Weight = 50.0;
			Movable = true;
		}

		public FencePost ( Serial serial ) : base( serial )
		{
		}

		public override bool Decays { get { return false; } }

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) && m_Owner == null )
			{
				m_Owner = from;
				from.SendMessage( "Only you may move this now" );
			}
/*
			else if ( !IsChildOf( from.Backpack) && m_Owner == from && Movable == false )
			{
				Movable = true;
				from.SendMessage("You release it from the ground");
			}
			else if ( !IsChildOf( from.Backpack) && m_Owner == from && Movable == true )
			{
				Movable = false;
				from.SendMessage("You set it into the ground");
			}
*/
			else if ( !IsChildOf( from.Backpack) && m_Owner != from && (m_Owner != null) )
			{
				from.SendMessage("This doesn't belong to you");
			}

			else if ( !IsChildOf( from.Backpack) && m_Owner == null )
			{
				from.SendMessage("This must be in your backpack to claim ownership");
			}
		}

		public override bool OnDragLift( Mobile from )
		{
			if ((from == this.Owner)|| from.AccessLevel >= AccessLevel.GameMaster )
			{
				return base.OnDragLift(from);
			}
			else
			{
				from.SendMessage("This doesn't belong to you");
				return false;
			}
		}
	}
}

