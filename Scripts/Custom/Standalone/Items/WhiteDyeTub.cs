namespace Server.Items
{
	public class WhiteDyeTub : DyeTub
	{
		[Constructable]
		public WhiteDyeTub()
		{
			Hue = DyedHue = 1150;
			Redyable = false;
			Name = "White Dye Tub";
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Skills.Tailoring.Value < 70.0 )
			{
				from.SendLocalizedMessage( 1063013, "70\tTailoring" ); // You need at least ~1_SKILL_REQUIREMENT~ ~2_SKILL_NAME~ skill to use that ability.
            }

			else
			{
				base.OnDoubleClick( from );
			}
		}

		public WhiteDyeTub( Serial serial ) : base( serial )
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