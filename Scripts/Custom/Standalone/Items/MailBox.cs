/*	Original Release: Koluch -  September 24,2011 - http://www.runuo.com/community/threads/beta-mailbox-with-flag-up-down-feature.491292/
 *  Last date Moded / Added 03/03/2013 - By: AlphaDragon
 * Still working on:
 * on chop - if has items, gump to ask what to do with the items.
 */


using Server.Accounting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using Server;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Server.Items
{
	[Furniture]
	[FlipableAttribute( 0x4142, 0x4144 )]
    public class Mailbox : Container, ISecurable, IChopable //Note - Needed cause when locked down could not access the context menu, to set who you want to drop into mail box. And chop for deed.
    {
#region Declorations And Construction
//    	public override int DefaultMaxWeight{get{if ( IsSecure ) return 0;return base.DefaultMaxWeight;}}//To adjust defaultmax weight
    	public override int DefaultMaxWeight{get{if ( IsLockedDown ) return 0;return base.DefaultMaxWeight;}}//To adjust defaultmax weight

        public override int LabelNumber { get { return 1113927; } } // Mailbox
        public override int DefaultGumpID { get { return 0x11A; } }
        public override int DefaultDropSound { get { return 0x42; } }
        
        private bool m_PublicCanOpen;
        private bool m_PublicCanDrop;
        
        private Mobile m_Owner;
        private SecureLevel m_Level;

//		private ArrayList m_Secures;
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool PublicCanOpen{get { return m_PublicCanOpen;}set{m_PublicCanOpen = value;}}
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool PublicCanDrop{get { return m_PublicCanDrop;}set{m_PublicCanDrop = value;}}
        
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner { get { return m_Owner; } set { m_Owner = value;}}//InvalidateProperties();

        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level{get { return m_Level; }set { m_Level = value;}} //InvalidateProperties();

//        public ArrayList Secures{ get{ return m_Secures; } }
        
        [Constructable]
        public Mailbox() : base( 0x4142 )
        {
        	Name = "Mailbox";
//        	m_Level = SecureLevel.Anyone;
        }
        
        public Mailbox(Serial serial) : base(serial)
        {
        }
#endregion Declorations And Construction


#region on player movement
		public override bool HandlesOnMovement { get { return true; } }
        public override void OnMovement(Mobile from, Point3D oldLocation)
        {
        	from.CloseGump(typeof(MailBoxOptionsGump));
        }
#endregion on player movement

#region context menu entries
        public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
        	BaseHouse house = BaseHouse.FindHouseAt(this);
        	
        	if ( from.AccessLevel >= AccessLevel.GameMaster )
        		list.Add(new mailboxoptionsgumpCME(from, this));
        	
        	if (house != null && house.IsCoOwner(from))
        	{
        		if (IsSecure)
        		{
        			list.Add(new isSECUREDsecuritysettingsCME(from, this, house));
        			base.GetContextMenuEntries( from, list );
        		}
        		else if (IsLockedDown)//acts like deco if just locked down.
        		{
        			//        		If locked down acts like deco- so dont show anything.
        			//        		SetSecureLevelEntry.AddTo(from, this, list);
        			//        		base.GetContextMenuEntries( from, list );
        		}
        	}
        	else
        	{
        		base.GetContextMenuEntries( from, list );
        	}
//			SetSecureLevelEntry.AddTo(from, this, list);
//  		list.Add(new isSECUREDsecuritysettings(from, this, house));
//			base.GetContextMenuEntries( from, list );
		}

        private class mailboxoptionsgumpCME : ContextMenuEntry
		{
			private Mobile contextmenu_from;
			private Mailbox contextmenu_mailbox;
			
			public mailboxoptionsgumpCME(Mobile from, Mailbox mailbox)
				: base(0127)//5070=Done>>//0127 options
			{
				contextmenu_from = from;
				contextmenu_mailbox = mailbox;
			}
			
			public override void OnClick()
			{
				contextmenu_from.CloseGump(typeof(MailBoxOptionsGump));
				contextmenu_from.SendGump(new MailBoxOptionsGump(contextmenu_from,contextmenu_mailbox));
			}
		}

		private class isSECUREDsecuritysettingsCME : ContextMenuEntry
		{
			private Mobile contextmenu_from;
			private Mailbox contextmenu_mailbox;
			private BaseHouse contextmenu_house;
			
			public isSECUREDsecuritysettingsCME(Mobile from, Mailbox mailbox, BaseHouse house)
				: base(6203)//5070=Done>>//6203 Security settings
			{
				contextmenu_from = from;
				contextmenu_mailbox = mailbox;
				contextmenu_house = house;					
			}
			
			public override void OnClick()
			{
//				contextmenu_from.SendMessage("screwed");//testing
				contextmenu_house.AddSecure(contextmenu_from,contextmenu_mailbox);
			}
		}//Still need work additional gumps ect
#endregion
		
		public bool CheckAccess(Mobile m)
        {
        	BaseHouse house = BaseHouse.FindHouseAt(this);
        	
        	if (house == null)
        		return false;
        	
        	if (!house.IsAosRules)
        		return true;
        	
        	if (house.Public ? house.IsBanned(m) : !house.HasAccess(m))
        		return false;
        	
        	return house.HasSecureAccess(m, m_Level);
        }
        
		public void DropItemsToGround()
		{
			for ( int i = Items.Count - 1; i >= 0; i-- )
				Items[ i ].MoveToWorld( Location );
		}
		
#region Code To Prevent it from being moved

		public void Say(string args)
		{
			PublicOverheadMessage(MessageType.Regular, 0x3B2, false, args);
		}

		public override bool OnDragLift(Mobile from)
		{
			BaseHouse house = BaseHouse.FindHouseAt(this);//changed from (from to this)

			if (!BaseHouse.CheckLockedDownOrSecured(this) && house != null)
			{
				house.AddSecure(from, (Item)this);
				this.Owner = house.Owner;
			}
			this.Say("Needs to be choped down to be moved.");
			this.Movable = false;
			return false;
		}

//		public override bool Decays { get { return !IsLockedDown || !IsSecure; } }//will decay if not locked down or secured

#endregion Code To Prevent it from being moved
		
		public virtual void OnChop( Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsOwner( from ) )
			{
				if (this.TotalItems != 0)
					DropItemsToGround();
					Effects.PlaySound( GetWorldLocation(), Map, 0x3B3 );
					from.SendLocalizedMessage( 500461 ); // You destroy the item.
					Delete();
//					house.Addons.Remove( this );
					house.ReleaseSecure(from, (Item)this);
					from.AddToBackpack( new MailboxDeed() );
			}
			else
				from.SendMessage( "You can not do that." ); // This item must be unlocked/unsecured before re-deeding it.
		}
		
        public override void OnDoubleClick(Mobile from)
        	{
        	SecureLevel securelevel = this.m_Level;
        	BaseHouse housefoundation = BaseHouse.FindHouseAt(this);

        	if (from.InRange( this, 2 ))
        		{
        		if ( housefoundation == null )
        			{
        			if (from.AccessLevel >= AccessLevel.Counselor || this.PublicCanOpen == true )
        				{
        				if (from.AccessLevel >= AccessLevel.Counselor)
        				{//if a GMs opens
        					AdjustMailboxFlagDown(this);
        					from.SendSound (45);
        					base.OnDoubleClick(from);
        					return;
        				}
        				else
        				{//if a regular player opens a public mailbox
        					from.SendMessage("You can look inside the mailbox, but you can not reach anything.");
        					from.SendSound (45);
        					base.OnDoubleClick(from);
        					return;
        				}
        			}
        			else
        			{
        				from.SendMessage("You can not open this mailbox.");
        			}
        		}
        		else if (CheckAccess( from ) && housefoundation.IsFriend( from ) )
        		{
        			base.OnDoubleClick(from);
        			AdjustMailboxFlagDown(this);
					from.SendSound (45);
					}
        		else
        			{
        			from.SendMessage("You are not allowed to open that.");
        			}
        		}
        	else
        		{
        		from.SendLocalizedMessage( 500446 ); // That is too far away.
        		}
        	}
        
        public override bool OnDragDrop(Mobile from, Item dropped)
        {
        	BaseHouse housefoundation = BaseHouse.FindHouseAt(this);
        	Item item = dropped as Item;

        	if (dropped.Weight + dropped.TotalWeight + this.TotalWeight >= this.MaxWeight)
        	{
        		if (IsLockedDown)
	        		from.SendMessage(38,"Seems to be just a Decoration Mailbox.");

        		if (IsSecure)
        			from.SendMessage(38,"This will be to much weight for the Mailbox to hold.");
        		return false;
        	}

        	if ((housefoundation == null) & (this.PublicCanDrop == true))
        	{
        		from.SendMessage(68,"You put that into the Mailbox.");
        		DropItem( dropped );
        		from.SendSound( GetDroppedSound( item ), GetWorldLocation() );
        		AdjustMailboxFlagUp(this);
        		return true;//false for the moment cause of losing bag
        	}
        	else if((housefoundation != null) & (this.IsSecure))
        	{
        		from.SendMessage(68,"You put that into the Mailbox.");
        		DropItem( dropped );
        		from.SendSound( GetDroppedSound( item ), GetWorldLocation() );
        		AdjustMailboxFlagUp(this);        		
        		return true;//false for the moment cause of losing bag
        	}
        	else
        	{
        		from.SendMessage(38,"You can not do that.");
        		return false;
        	}
        }
        
        private void AdjustMailboxFlagUp(Mailbox mailbox)
		{
			if (this.ItemID == 0x4142 )
				this.ItemID = 0x4141;
			if (this.ItemID == 0x4144 )
				this.ItemID = 0x4143;
		}
		
		private void AdjustMailboxFlagDown(Mailbox mailbox)
		{
			if ( ItemID == 0x4141 )
				this.ItemID = 0x4142;
			if ( ItemID == 0x4143 )
				this.ItemID = 0x4144;
		}

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D point3d )
		{
			
			BaseHouse housefoundation = BaseHouse.FindHouseAt(this);
        	Item item = dropped as Item;

        	if (dropped.Weight + dropped.TotalWeight + this.TotalWeight >= this.MaxWeight)
        	{
        		if (IsLockedDown)
	        		from.SendMessage(38,"Seems to be just a Decoration Mailbox.");

        		if (IsSecure)
        			from.SendMessage(38,"This will be to much weight for the Mailbox to hold.");
        		return false;
        	}

        	if ((housefoundation == null) & (this.PublicCanDrop == true))
        	{
        		from.SendMessage(68,"You put that into the Mailbox.");
        		DropItem( dropped );
        		from.SendSound( GetDroppedSound( item ), GetWorldLocation() );
        		AdjustMailboxFlagUp(this);        		
        		return true;//false for the moment cause of losing bag
        	}
        	else if((housefoundation != null) & (this.IsSecure))
        	{
        		from.SendMessage(68,"You put that into the Mailbox.");
        		DropItem( dropped );
        		from.SendSound( GetDroppedSound( item ), GetWorldLocation() );
        		AdjustMailboxFlagUp(this);
        		return true;//false for the moment cause of losing bag
        	}
        	else
        	{
        		from.SendMessage(38,"You can not do that.");
        		return false;
        	}
		}        

		public override void AddNameProperty( ObjectPropertyList list )
		{
			BaseHouse housefoundation = BaseHouse.FindHouseAt(this);
			if ( this.PublicCanDrop == true)
			{
				list.Add( "Public Drop Mailbox" );
			}
			else if ( housefoundation != null)
			{
				if (this.PublicCanDrop == false)
				{
					if(this.PublicCanOpen == false)
					{
						 if (IsSecure & !IsLockedDown)
						 {
						 	list.Add( "A Private Mailbox" );
						 }
						 else
						 {
						 	if (IsLockedDown & !IsSecure)
						 	{
						 		list.Add( "A Decoration Mailbox" );
						 	}
						 	else
						 	{
						 		base.AddNameProperty( list );
						 	}
						 }
					}
					else
					{
						list.Add( "Mailbox - Error TN1 (Please Notify GMs)" );
					}
				}
				else
				{
					list.Add( "Mailbox - Error TN2 (Please Notify GMs)" );
				}
			}
			else
			{
				base.AddNameProperty( list );
				//list.Add( "3Mailbox" );/*causes dup name*/
			}
		}
		
        public override void GetProperties(ObjectPropertyList list)//errored
        {
        	SecureLevel securelevel = this.m_Level;
        	BaseHouse housefoundation = BaseHouse.FindHouseAt(this);
        	if (housefoundation !=null)
        	{
        		if (this.IsLockedDown == true)
        		{
        			base.GetProperties(list);
        			if (this.Owner != null)
        			list.Add("Owner: {0} ",this.Owner.RawName);
        		}
        		else if (this.IsSecure == true)
        		{
        			base.GetProperties(list);
        			if (this.Owner !=null)
        			list.Add("Owner: {0} ",this.Owner.RawName);
        		}
        		else
        		{
        			base.GetProperties(list);
        		}
        	}
        	else
        	{
        		base.GetProperties(list);
        	}
        }
        
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version

            writer.Write((int)m_Level);
			writer.Write( m_Owner );
            writer.Write((bool)m_PublicCanOpen);
            writer.Write((bool)m_PublicCanDrop);
            
        }

        public override void Deserialize(GenericReader reader)
        {
        	base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            m_Level = (SecureLevel)reader.ReadInt();
			m_Owner = reader.ReadMobile();
            m_PublicCanOpen = reader.ReadBool();
            m_PublicCanDrop = reader.ReadBool();
        }
    }
    
	public class MailboxDeed : Item
	{

		[Constructable]
		public MailboxDeed() : base( 0x14F0 )
		{
			Name = "Mailbox Deed";
			Weight = 1.0;
		}

		public MailboxDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				{
				BaseHouse house = BaseHouse.FindHouseAt( from );
				if ( house != null && house.IsOwner( from ) )
					{
					from.Say("Where do I want to put this in my house?");
					from.SendMessage( "Where do you want to place this Mailbox?" );
					from.BeginTarget( -1, true, TargetFlags.None, new TargetStateCallback( Placement_OnTarget ), null );
					}
				else
					{
					from.SendLocalizedMessage( 502092 ); // You must be in your house to do this.
					}
				}
			else
				{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				}
		}
		public void Placement_OnTarget( Mobile from, object targeted, object state )
		{
			IPoint3D p = targeted as IPoint3D;
			Map map = from.Map;

			if ( p == null )
				return;

			Point3D loc = new Point3D( p );

			BaseHouse house = BaseHouse.FindHouseAt( loc, from.Map, 16 );

			if ( house != null && house.IsOwner( from ) )
			{
				Mailbox mailbox = new Mailbox( );
				mailbox.MoveToWorld( new Point3D( p ), map );
				Delete();
				mailbox.Owner = from;
//				house.Addons.Add(mailbox);
				house.AddSecure(from,mailbox);
			}
			else
			{
				from.SendMessage(38,"You don't Own that property. You can only place this Mailbox on house property you own.");
			}
		}
	}	
}



namespace Server.Gumps
{
    public class MailBoxOptionsGump : Gump
    {
    	private Mailbox m_mailbox;
    	
    	public MailBoxOptionsGump(Mobile from, Mailbox mailbox) : base( 100, 100 )
        {
    		m_mailbox = mailbox;
    		
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(0, 0, 357, 259, 2620);//background immage
			AddImage(13, 190, 2152);//help info blank button
			AddImage(13, 220, 2154);//help info checked button
			AddImage(309, 180, 9004);//signature stamp graphic
			AddItem(13, 15, 16706);//mailbox graphic
			AddImage(137, 21, 5001);//option graphic
			AddLabel(151, 5, 38, @"Mailbox");//title mailbox
			AddLabel(70, 116, 1149, @"Public can drop into mailbox?");
			AddLabel(70, 86, 1149, "Public can open to look inside mailbox?");
			AddLabel(70, 56, 1149, @"Public can see this mailbox?");
			AddLabel(15, 166, 1149, @"Information help:");
			AddLabel(48, 194, 1149, @"This meens no they can not.");
			AddLabel(48, 225, 1149, @"This meens yes they can.");
			AddButton(334, 9, 3, 4, 0, GumpButtonType.Reply, 0);//X  close button
			//		x,y,color, @=center?
			
			//for button of can see the mailbox
//			AddButton(264, 110, 2151, 2154, 0, GumpButtonType.Reply, 0);//button can see this is checked
//			AddButton(320, 80, 2151, 2154, 0, GumpButtonType.Reply, 0);//button can open this is not checked
			//2152 pressed, 2154 checked, 2151 notchecked
			// x y normal pressed calltonumber
			
			if (mailbox.Visible == true)
				AddButton(248, 50, 2154, 2152, 1, GumpButtonType.Reply, 0);
			if (mailbox.Visible == false)
				AddButton(248, 50, 2151, 2152, 2, GumpButtonType.Reply, 0);
			
			
			if (mailbox.PublicCanOpen == true)
				AddButton(311, 80, 2154, 2152, 3, GumpButtonType.Reply, 0);
			if (mailbox.PublicCanOpen == false)
				AddButton(311, 80, 2151, 2152, 4, GumpButtonType.Reply, 0);
			
			
			if (mailbox.PublicCanDrop == true)
				AddButton(258, 110, 2154, 2152, 5, GumpButtonType.Reply, 0);
			if (mailbox.PublicCanDrop == false)
				AddButton(258, 110, 2151, 2152, 6, GumpButtonType.Reply, 0);
    	}
    	
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            if (info.ButtonID == 1 )
            {
            	m_mailbox.Visible = false;
            	from.CloseGump(typeof(MailBoxOptionsGump));
				from.SendGump(new MailBoxOptionsGump(from,m_mailbox));
            }
            if (info.ButtonID == 2 )
            {
            	m_mailbox.Visible = true;
            	from.CloseGump(typeof(MailBoxOptionsGump));
				from.SendGump(new MailBoxOptionsGump(from,m_mailbox));
            }
            if (info.ButtonID == 3 )
            {
            	m_mailbox.PublicCanOpen = false;
            	from.CloseGump(typeof(MailBoxOptionsGump));
				from.SendGump(new MailBoxOptionsGump(from,m_mailbox));

            }
            if (info.ButtonID == 4 )
            {
            	m_mailbox.PublicCanOpen = true;
            	from.CloseGump(typeof(MailBoxOptionsGump));
				from.SendGump(new MailBoxOptionsGump(from,m_mailbox));
            }
            if (info.ButtonID == 5 )
            {
            	m_mailbox.PublicCanDrop = false;
            	from.CloseGump(typeof(MailBoxOptionsGump));
				from.SendGump(new MailBoxOptionsGump(from,m_mailbox));
            }
            if (info.ButtonID == 6 )
            {
            	m_mailbox.PublicCanDrop = true;
            	from.CloseGump(typeof(MailBoxOptionsGump));
				from.SendGump(new MailBoxOptionsGump(from,m_mailbox));
            }
        }
    }
}