// Created by Tru.
using System;
using System.Collections;
using System.Reflection;
using Server.Mobiles;

namespace Server.Commands
{
    public class CloneMe
	{
		public static void Initialize()
		{
			CommandSystem.Register( "CloneMe", AccessLevel.GameMaster, new CommandEventHandler( CloneMe_OnCommand ) );
		}

		[Usage( "CloneMe" )]
		[Description( "Makes an exact duplicate of you at your present location and hides you" )]
		public static void CloneMe_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile.Mounted )
			{
				e.Mobile.SendMessage( "This command doesn't work while mounted" );
				return;
			}

			BaseCreature m = new Cloner();
			e.Mobile.Hidden = true;
			m.Dex = e.Mobile.Dex;
			m.Int = e.Mobile.Int;
			m.Str = e.Mobile.Str;
			m.Fame = e.Mobile.Fame;
			m.Karma = e.Mobile.Karma;
			m.NameHue = e.Mobile.NameHue;
			m.SpeechHue = e.Mobile.SpeechHue;
			m.Criminal = e.Mobile.Criminal;
			m.Name = e.Mobile.Name;
			m.Title = e.Mobile.Title;
			m.Female = e.Mobile.Female;
			m.Body = e.Mobile.Body;
			m.Hue = e.Mobile.Hue;
			m.Hits = e.Mobile.HitsMax;
			m.Mana = e.Mobile.ManaMax;
			m.Stam = e.Mobile.StamMax;
			m.BodyMod = e.Mobile.Body;
			m.HairItemID = e.Mobile.HairItemID;
			m.HairHue = e.Mobile.HairHue;
			m.FacialHairItemID = e.Mobile.FacialHairItemID;
			m.FacialHairHue = e.Mobile.FacialHairHue;
			//m.Controled = true;  <-----this can be uncommented (and next line) and it will become tame to  you (although it says tame above it)
			//m.ControlMaster = e.Mobile;  <-----this can be uncommented and it will become tame to  you
			m.Map = e.Mobile.Map;
			m.Location = e.Mobile.Location;
			m.Direction = e.Mobile.Direction;

			for (int i=0; i<e.Mobile.Skills.Length; i++)
				m.Skills[i].Base = e.Mobile.Skills[i].Base;

			// This code block duplicates all equiped items
			ArrayList items = new ArrayList( e.Mobile.Items );
			for (int i=0; i<items.Count; i++)
			{
				Item item = (Item)items[i];
				if(( item != null ) && ( item.Parent == e.Mobile ) && ( item != e.Mobile.Backpack ) && (item != e.Mobile.BankBox))
				{
					Type type = item.GetType();
					Item newitem = Loot.Construct( type );
					CopyProperties( newitem, item );
					newitem.Parent = null;
					m.AddItem( newitem );
				}
			}
		}

		private static void CopyProperties ( Item dest, Item src )
		{
			PropertyInfo[] props = src.GetType().GetProperties();

			for ( int i = 0; i < props.Length; i++ )
			{
				try
				{
					if ( props[i].CanRead && props[i].CanWrite )
						props[i].SetValue( dest, props[i].GetValue( src, null ), null );
				}
				catch
				{
				}
			}
		}
	}
}

namespace Server.Mobiles
{
    [CorpseName( "Corpse" )]
	public class Cloner : BaseCreature
	{
		private DateTime m_ExpireTime;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime ExpireTime
		{
			get{ return m_ExpireTime; }
			set{ m_ExpireTime = value; }
		}

		public Cloner() : base( AIType.AI_Thief, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			m_ExpireTime = DateTime.Now + TimeSpan.FromMinutes( 10.0 );
		}

		public override void GetProperties( ObjectPropertyList list ) { list.Add( Name ); }

		public override void OnThink()
		{
			bool expired;

			expired = ( DateTime.Now >= m_ExpireTime );

			if ( expired )
				Delete();
			else
				base.OnThink();
		}

		public Cloner(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
