// Possess Stone 1.0
//
// allows staff to take on body shape, clothes, hair, hue, fame, karma, etc, of a mobile
// skills, stats and such are not copied. This is purely cosmetic.
// Double click stone to acquire target or to revert back to self
//
// 5/2005 : Admin Oak : SylvanDreams.Com
//
using System;
using Server;
using System.Collections;
using Server.Accounting;
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;

namespace Server.Items
{
    public class Pstone : Item
    {
        [Constructable]
        public Pstone()
            : base(0x1869)
        {
            Visible = true;
            Name = "Possess Stone";
            LootType = LootType.Blessed;
            Weight = 1.0;
        }

        public Pstone(Serial serial)
            : base(serial)
        {
        }

        private Mobile m_Possessor;
        private Mobile m_Possessed;
        private bool m_Possessing;
        private string m_Name;
        private string m_Title;
        private int m_Hue;
        private int m_SpeechHue;
        private int m_BodyValue;
        private int m_Karma;
        private int m_Fame;
        private int m_Kills;
        private bool m_Female;
        private Container m_Stuff;

        public override void OnDoubleClick(Mobile from)
        {
            // if not a player
            Mobile m_Mob = (Mobile)from;
            AccessLevel al_MobLevel = m_Mob.AccessLevel;
            Account a_Account = (Account)m_Mob.Account;
            AccessLevel al_AccLevel = a_Account.AccessLevel;
            if (al_AccLevel > AccessLevel.Player)
            {
                // if not already possessing a mobile
                if (!m_Possessing)
                {
                    from.SendMessage("Who or What do you want to possess?");
                    from.Target = new InternalTarget(this);
                }
                // restore your self and unhide the previously possessed mobile
                else
                {
                    // restore basics
                    m_Possessing = false;
                    from.Karma = m_Karma;
                    from.Fame = m_Fame;
                    from.Name = m_Name;
                    from.Title = m_Title;
                    from.Hue = m_Hue;
                    from.BodyValue = m_BodyValue;
                    from.Female = m_Female;
                    from.Hidden = true;
                    // Delete eveything you have equipped which is just a copy of what the NPC had   
                    ArrayList equipitems = new ArrayList(from.Items);
                    foreach (Item item in equipitems)
                    {
                        if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack))
                            item.Delete();
                    }
                    // get dressed and delete that white backpack
                    for (int i = m_Stuff.Items.Count - 1; i >= 0; --i)
                    {
                        if (i >= m_Stuff.Items.Count)
                            continue;
                        from.EquipItem((Item)m_Stuff.Items[i]);
                    }
                    // do we really want to delete the backpack?
                    m_Stuff.Delete();

                    // get your location, orient, enabale and show the mobile
                    Point3D from_Location = new Point3D(from.Location.X, from.Location.Y, from.Location.Z);
                    if (m_Possessed != null)
                    {
                        m_Possessed.Direction = from.Direction;
                        m_Possessed.MoveToWorld(from_Location, from.Map);
                        m_Possessed.Hidden = false;
                        m_Possessed.CantWalk = false;
                    }

                }
            }
            else // somehow a player got one of these which should never happen, just delete it and put a scare into them
            {
                from.SendMessage("You are trying to access a restricted item. The item has been removed and your account has been scheduled for deletion.");
                this.Delete();
            }

        }

        private class InternalTarget : Target
        {
            private Pstone ps;

            public InternalTarget(Pstone item)
                : base(10, false, TargetFlags.None)
            {
                ps = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (from == targeted)
                {
                    from.SendMessage("Possess yourself? Hmm, interesting concept.");
                }
                else if (targeted is Mobile) // we only possess mobiles. If you want to possess a house, find another script
                {
                    if (targeted is PlayerMobile)
                    {
                        from.SendMessage("You can't possess a player. They are already possessed by Satan.");
                    }
                    else
                    {

                        ps.m_Possessed = targeted as Mobile;

                        // take everything off and put it in a white backpack
                        ps.m_Stuff = new Backpack();
                        ps.m_Stuff.Hue = 1153;
                        Container packy = from.Backpack;
                        packy.DropItem(ps.m_Stuff);

                        RemoveLayer(from, Layer.FirstValid, ps.m_Stuff);
                        RemoveLayer(from, Layer.TwoHanded, ps.m_Stuff);
                        RemoveLayer(from, Layer.OneHanded, ps.m_Stuff);
                        // wing layer on my shard
                        RemoveLayer(from, Layer.Unused_xF, ps.m_Stuff);
                        RemoveLayer(from, Layer.Shoes, ps.m_Stuff);
                        RemoveLayer(from, Layer.Pants, ps.m_Stuff);
                        RemoveLayer(from, Layer.Shirt, ps.m_Stuff);
                        RemoveLayer(from, Layer.Helm, ps.m_Stuff);
                        RemoveLayer(from, Layer.Gloves, ps.m_Stuff);
                        RemoveLayer(from, Layer.Ring, ps.m_Stuff);
                        RemoveLayer(from, Layer.Neck, ps.m_Stuff);
                        RemoveLayer(from, Layer.Hair, ps.m_Stuff);
                        RemoveLayer(from, Layer.Waist, ps.m_Stuff);
                        RemoveLayer(from, Layer.InnerTorso, ps.m_Stuff);
                        RemoveLayer(from, Layer.Bracelet, ps.m_Stuff);
                        RemoveLayer(from, Layer.FacialHair, ps.m_Stuff);
                        RemoveLayer(from, Layer.MiddleTorso, ps.m_Stuff);
                        RemoveLayer(from, Layer.Earrings, ps.m_Stuff);
                        RemoveLayer(from, Layer.Arms, ps.m_Stuff);
                        RemoveLayer(from, Layer.Cloak, ps.m_Stuff);
                        RemoveLayer(from, Layer.OuterTorso, ps.m_Stuff);
                        RemoveLayer(from, Layer.OuterLegs, ps.m_Stuff);
                        RemoveLayer(from, Layer.LastUserValid, ps.m_Stuff);

                        // now copy everything from the mobile you are possessing and equip it
                        CopyLayer(ps.m_Possessed, from, Layer.FirstValid);
                        CopyLayer(ps.m_Possessed, from, Layer.TwoHanded);
                        CopyLayer(ps.m_Possessed, from, Layer.OneHanded);
                        // wing layer on my shard
                        CopyLayer(ps.m_Possessed, from, Layer.Unused_xF);
                        CopyLayer(ps.m_Possessed, from, Layer.Shoes);
                        CopyLayer(ps.m_Possessed, from, Layer.Pants);
                        CopyLayer(ps.m_Possessed, from, Layer.Shirt);
                        CopyLayer(ps.m_Possessed, from, Layer.Helm);
                        CopyLayer(ps.m_Possessed, from, Layer.Gloves);
                        CopyLayer(ps.m_Possessed, from, Layer.Ring);
                        CopyLayer(ps.m_Possessed, from, Layer.Neck);
                        CopyLayer(ps.m_Possessed, from, Layer.Hair);
                        CopyLayer(ps.m_Possessed, from, Layer.Waist);
                        CopyLayer(ps.m_Possessed, from, Layer.InnerTorso);
                        CopyLayer(ps.m_Possessed, from, Layer.Bracelet);
                        CopyLayer(ps.m_Possessed, from, Layer.FacialHair);
                        CopyLayer(ps.m_Possessed, from, Layer.MiddleTorso);
                        CopyLayer(ps.m_Possessed, from, Layer.Earrings);
                        CopyLayer(ps.m_Possessed, from, Layer.Arms);
                        CopyLayer(ps.m_Possessed, from, Layer.Cloak);
                        CopyLayer(ps.m_Possessed, from, Layer.OuterTorso);
                        CopyLayer(ps.m_Possessed, from, Layer.OuterLegs);
                        CopyLayer(ps.m_Possessed, from, Layer.LastUserValid);

                        // save and set basics
                        ps.m_Possessor = from;

                        ps.m_Name = from.Name;
                        from.Name = ps.m_Possessed.Name;

                        ps.m_Title = from.Title;
                        from.Title = ps.m_Possessed.Title;

                        ps.m_Hue = from.Hue;
                        from.Hue = ps.m_Possessed.Hue;

                        ps.m_SpeechHue = from.SpeechHue;
                        from.SpeechHue = ps.m_Possessed.SpeechHue;

                        ps.m_BodyValue = from.BodyValue;
                        from.BodyValue = ps.m_Possessed.BodyValue;

                        ps.m_Female = from.Female;
                        from.Female = ps.m_Possessed.Female;

                        ps.m_Karma = from.Karma;
                        from.Karma = ps.m_Possessed.Karma;

                        ps.m_Fame = from.Fame;
                        from.Fame = ps.m_Possessed.Fame;

                        ps.m_Kills = from.Kills;
                        from.Kills = ps.m_Possessed.Kills;

                        // if mounted, dismount. 
                        // Just be sure you are riding an ethy or you will abandon your mount
                        IMount ride = from.Mount;
                        if (ride != null)
                            ride.Rider = null;

                        // orient and display
                        Point3D m_Location = new Point3D(ps.m_Possessed.Location.X, ps.m_Possessed.Location.Y, ps.m_Possessed.Location.Z);
                        from.Direction = ps.m_Possessed.Direction;
                        from.MoveToWorld(m_Location, ps.m_Possessed.Map);
                        from.Hidden = false;
                        // hide the mobile 
                        ps.m_Possessed.Hidden = true;
                        ps.m_Possessed.CantWalk = true;
                        ps.m_Possessing = true;
                    }
                }
                else
                {
                    from.SendMessage("You can't possess that!");
                }
            }
        }

        // copy what the possessee has equipped and wear it
        static void CopyLayer(Mobile possessed, Mobile from, Layer layer)
        {
            Item anItem = possessed.FindItemOnLayer(layer);
            if (anItem != null)
            {
                Item myitem = new Item();
                myitem.ItemID = anItem.ItemID;
                myitem.Hue = anItem.Hue;
                myitem.Layer = layer;
                from.AddItem(myitem);
            }
        }

        // get rid of everything equipped and store it
        static void RemoveLayer(Mobile from, Layer layer, Container pack)
        {
            Item anItem = from.FindItemOnLayer(layer);
            if (anItem != null)
                pack.DropItem(anItem);
        }



        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write((Mobile)m_Possessor);
            writer.Write((Mobile)m_Possessed);
            writer.Write(m_Possessing);
            writer.Write((string)m_Name);
            writer.Write((string)m_Title);
            writer.Write((int)m_Hue);
            writer.Write((int)m_Karma);
            writer.Write((int)m_Fame);
            writer.Write((int)m_Kills);
            writer.Write((int)m_SpeechHue);
            writer.Write((int)m_BodyValue);
            writer.Write(m_Female);
            writer.Write((Item)m_Stuff);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Possessor = reader.ReadMobile();
            m_Possessed = reader.ReadMobile();
            m_Possessing = reader.ReadBool();
            m_Name = reader.ReadString();
            m_Title = reader.ReadString();
            m_Hue = reader.ReadInt();
            m_Karma = reader.ReadInt();
            m_Fame = reader.ReadInt();
            m_Kills = reader.ReadInt();
            m_SpeechHue = reader.ReadInt();
            m_BodyValue = reader.ReadInt();
            m_Female = reader.ReadBool();
            m_Stuff = reader.ReadItem() as Container;
        }
    }
}
