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
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Items
{
    public class TentDestroyer : BaseAddon
    {
        [Constructable]
        public TentDestroyer(TentWalls tentwalls, TentRoof tentroof, TentFloor tentfloor, TentTrim tenttrim, PlayerMobile player, SecureTent chest)
        {
            Name = "A tent carrying bag";
            m_Player = player;
            m_TentRoof = tentroof;
            m_TentWalls = tentwalls;
            m_TentFloor = tentfloor;
            m_TentTrim = tenttrim;
            m_Chest = chest;
            this.ItemID = 2648; // 2645;
            this.Visible = true;
            Hue = 277; // 1072;

            m_Timer = new AutoDeleteTimer(this);
            m_Timer.Start();
        }
        private TentRoof m_TentRoof;
        private TentWalls m_TentWalls;
        private TentTrim m_TentTrim;
        private TentFloor m_TentFloor;
        private PlayerMobile m_Player;
        private SecureTent m_Chest;

        private static Timer m_Timer;

        public PlayerMobile Player
        {
            get { return m_Player; }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (m_Player == from)
            {
                if (m_Chest != null && m_Chest.Items.Count > 0)
                {
                    from.SendMessage("You must remove the items from the travel bag before packing up your tent.");
                }
                else
                {
                    from.SendGump(new TentDGump(this, from));
                }
            }
            else
            {
                from.SendMessage("You don't appear to own this Tent.");
            }
        }

        public override void OnDelete()
        {
            if (m_TentFloor != null) // m_TentFloor
            {
                m_TentFloor.Delete();
            }
            else
            {
                Console.WriteLine("m_TentFloor was null");
            }

            if (m_TentTrim != null) // m_TentTrim
            {
                m_TentTrim.Delete();
            }
            else
            {
                Console.WriteLine("m_TentTrim was null");
            }

            if (m_TentWalls != null) // m_TentWalls
            {
                m_TentWalls.Delete();
            }
            else
            {
                Console.WriteLine("m_TentWalls was null");
            }

            if (m_TentRoof != null)  // m_TentRoof
            {
                m_TentRoof.Delete();
            }
            else
            {
                Console.WriteLine("m_TentRoof was null");
            }

            if (m_Chest != null) // m_Chest
            {
                m_Chest.Delete();
            }
            else
            {
                Console.WriteLine("m_Chest was null");
            }
        }

        private class AutoDeleteTimer : Timer
        {
            private TentDestroyer tent;

            public AutoDeleteTimer(TentDestroyer t)
                : base(TimeSpan.FromDays(1))
            {
                tent = t;
            }

            protected override void OnTick()
            {
                tent.CheckAbandoned();
            }
        }

        private void CheckAbandoned()
        {
            if (m_Player == null)
            {
                Delete();
                if (m_Timer != null) m_Timer.Stop();
            }
            else if (m_Player.LastOnline < DateTime.Now.Subtract(TimeSpan.FromDays(5))) // If his last online time was before X days ago, we delete his tent.
            {
                Delete();
                m_Player.AddToBackpack(new TentDeed());
                if (m_Timer != null) m_Timer.Stop();
            }
            else
            {
                m_Timer = new AutoDeleteTimer(this);
                m_Timer.Start();
            }
        }

        public TentDestroyer(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1); // version 
            writer.Write(m_TentTrim);
            writer.Write(m_TentFloor);
            writer.Write(m_TentWalls);
            writer.Write(m_TentRoof);
            writer.Write(m_Player);
            writer.Write(m_Chest);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_TentTrim = (TentTrim)reader.ReadItem();
            m_TentFloor = (TentFloor)reader.ReadItem();
            m_TentWalls = (TentWalls)reader.ReadItem();
            m_TentRoof = (TentRoof)reader.ReadItem();
            m_Player = (PlayerMobile)reader.ReadMobile();
            m_Chest = (SecureTent)reader.ReadItem();

            CheckAbandoned();
        }
    }
}
