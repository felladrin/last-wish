namespace Server.Items
{
    // version 1.1.1 Bed coordinates of 0,0,0 will cause npc to sleep and wake at it's current location.
    // version 1.0 initial release.
    public class SleeperEWAddon: SleeperBaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SleeperEWAddonDeed();
			}
		}
	
		public SleeperEWAddon( Serial serial ) : base( serial )
		{
		}
	
		[Constructable]
		public SleeperEWAddon( ) 
		{
			Visible = true;
			Name = "Sleeper";
			AddComponent( new SleeperEWPiece(this, 0xA7D), 0, 0, 0 );
			AddComponent( new SleeperEWPiece(this, 0xA7C), 0, 1, 0 );
			AddComponent( new SleeperEWPiece(this, 0xA79), 1, 0, 0 );
			AddComponent( new SleeperEWPiece(this, 0xA78), 1, 1, 0 );
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			UseBed(from, new Point3D(this.Location.X+1, this.Location.Y, this.Location.Z+6), Direction.East);
		}
	
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );			
			// don't read any serialization for old scripts, it's read in the base class
			if (OldStyleSleepers)
				return;
			
			int version = reader.ReadInt();
		}
	}

	public class SleeperEWAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SleeperEWAddon ();
			}
		}
	
		[Constructable]
		public SleeperEWAddonDeed ()
		{
			Name = "a large sleepable bed facing east deed";
		}
	
		public SleeperEWAddonDeed ( Serial serial )
		: base ( serial )
		{
		}
	
		public override void Serialize ( GenericWriter writer )
		{
			base.Serialize ( writer );
			writer.Write ( (int) 0 ); // Version
		}
	
		public override void Deserialize ( GenericReader reader )
		{
			base.Deserialize ( reader );
			int version = reader.ReadInt ();
		}
	}
	
	// Eni - the below is necesary to be compatible with older versions of the script
	// also because of furniture dyability.
	[Furniture]
	public class SleeperEWPiece : SleeperPiece
	{
		public SleeperEWPiece( SleeperBaseAddon sleeper, int itemid ) : base( sleeper, itemid )
		{
		}
	
		public SleeperEWPiece( Serial serial ): base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); }
	}
}