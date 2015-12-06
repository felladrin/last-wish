//A Script By: BlurryDude
using System;
using Server.Commands;

namespace Server.Items
{
	public class CommandBall : Item
	{
		private string com = "props";
		
		[CommandProperty( AccessLevel.Administrator )]
		public string comm 
		{
			get { return com; }
			set { com = value; this.Name = value; }
		}

		[Constructable]
		public CommandBall() : base( 6256 )
		{
			Name = "Program-A-Ball";
			Weight = 0.0;
			Stackable = false;
			Hue = 1167;
		}
		
		[Constructable]
		public CommandBall( string command, int hue  ) : base( 6256 )
		{
			Name = command;
			Weight = 0.0;
			Stackable = false;
			Hue = hue;
			com = command;
		}

		public CommandBall( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			string prefix = CommandSystem.Prefix;
			
			from.SendMessage( 2109, "{0}{1}", prefix, com );
			CommandSystem.Handle( from, String.Format( "{0}{1}", prefix, com ) );
		
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( com );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			com = reader.ReadString();
			if ( Hue == 0 )
				Hue = 1167;
		}
	}
}