//=========================================//
// Created by Dupre                        //
// Thanks to:                              //
// Zippy                                   //
// Ike                                     //
// Ignacio                                 //
//                                         //
// For putting up with a 'tard like me :)  //
//=========================================//
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
    public class TentDeed : Item
    {
        [Constructable]
        public TentDeed()
            : base(2648)
        {
            Name = "A Rolled Up Tent";
            Hue = 277;
            Weight = 25.0;
            LootType = LootType.Blessed;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (TentCheck(from) == false)
            {
                from.SendMessage("You already have the max number of tents placed.");
            }
            else
            {

                if (IsChildOf(from.Backpack))
                {
                    if (Validate(from) == true)
                    {
                        TentWalls v = new TentWalls();
                        v.Location = from.Location;
                        v.Map = from.Map;

                        TentRoof w = new TentRoof();
                        w.Location = from.Location;
                        w.Map = from.Map;

                        TentFloor y = new TentFloor();
                        y.Location = from.Location;
                        y.Map = from.Map;

                        TentTrim z = new TentTrim();
                        z.Location = from.Location;
                        z.Map = from.Map;

                        //		TentVerifier tentverifier = new TentVerifier();
                        //		from.AddToBackpack (tentverifier);

                        SecureTent chest = new SecureTent((PlayerMobile)from);
                        chest.Location = new Point3D(from.X - 1, from.Y - 1, from.Z);
                        chest.Map = from.Map;

                        TentDestroyer x = new TentDestroyer(v, w, y, z, (PlayerMobile)from, (SecureTent)chest); // ,(TentVerifier) tentverifier); 
                        x.Location = new Point3D(from.X - 1, from.Y, from.Z);   // was x, y+1, z
                        x.Map = from.Map;

                        from.SendGump(new TentGump(from));
                        this.Delete();
                    }
                    else
                    {
                        from.SendMessage("You cannot place your tent in this area.");
                    }
                }
                else
                {
                    from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                }
            }
        }

        public bool Validate(Mobile from)
        {
            // original region "name" checks.
            if (from.Region.Name == "Cove" || from.Region.Name == "Britain" ||//towns
                    from.Region.Name == "Jhelom" || from.Region.Name == "Minoc" ||//towns
                    from.Region.Name == "Haven" || from.Region.Name == "Trinsic" ||//towns
                    from.Region.Name == "Vesper" || from.Region.Name == "Yew" ||//towns
                    from.Region.Name == "Wind" || from.Region.Name == "Serpent's Hold" ||//towns
                    from.Region.Name == "Skara Brae" || from.Region.Name == "Nujel'm" ||//towns
                    from.Region.Name == "Moonglow" || from.Region.Name == "Magincia" ||//towns
                    from.Region.Name == "Delucia" || from.Region.Name == "Papua" ||//towns
                    from.Region.Name == "Buccaneer's Den" || from.Region.Name == "Ocllo" ||//towns
                    from.Region.Name == "Gargoyle City" || from.Region.Name == "Mistas" ||//towns
                    from.Region.Name == "Montor" || from.Region.Name == "Alexandretta's Bowl" ||//towns
                    from.Region.Name == "Lenmir Anfinmotas" || from.Region.Name == "Reg Volon" ||//towns
                    from.Region.Name == "Bet-Lem Reg" || from.Region.Name == "Lake Shire" ||//towns
                    from.Region.Name == "Ancient Citadel" || from.Region.Name == "Luna" ||//towns
                    from.Region.Name == "Umbra" || from.Region.Name == "Zento" ||//towns

                    from.Region.Name == "Moongates" ||

                    from.Region.Name == "Covetous" || from.Region.Name == "Deceit" ||//dungeons
                    from.Region.Name == "Despise" || from.Region.Name == "Destard" ||//dungeons
                    from.Region.Name == "Hythloth" || from.Region.Name == "Shame" ||//dungeons
                    from.Region.Name == "Wrong" || from.Region.Name == "Terathan Keep" ||//dungeons
                    from.Region.Name == "Fire" || from.Region.Name == "Ice" ||//dungeons
                    from.Region.Name == "Rock Dungeon" || from.Region.Name == "Spider Cave" ||//dungeons
                    from.Region.Name == "Spectre Dungeon" || from.Region.Name == "Blood Dungeon" ||//dungeons
                    from.Region.Name == "Wisp Dungeon" || from.Region.Name == "Ankh Dungeon" ||//dungeons
                    from.Region.Name == "Exodus Dungeon" || from.Region.Name == "Sorcerer's Dungeon" ||//dungeons
                    from.Region.Name == "Ancient Lair" || from.Region.Name == "Doom" ||//dungeons

                    from.Region.Name == "Britain Graveyard" || from.Region.Name == "Wrong Entrance" ||
                    from.Region.Name == "Covetous Entrance" || from.Region.Name == "Despise Entrance" ||
                    from.Region.Name == "Despise Passage" || from.Region.Name == "Jhelom Islands" ||
                    from.Region.Name == "Haven Island" || from.Region.Name == "Crystal Cave Entrance" ||
                    from.Region.Name == "Protected Island" || from.Region.Name == "Jail")
            {
                return false;
            }

            // house multis: 0x1404 is a narrow but wide lot
            // house multis: 0x1420  bigger overall, square shaped.
            /*
            ArrayList toMove;
            HousePlacementResult res = HousePlacement.Check(from, 0x1404, from.Location, out toMove);

            if (res != HousePlacementResult.Valid)
            {
                return false;
            }
            */
            return true;
        }

        public bool TentCheck(Mobile from)
        {
            PlayerMobile pm = (PlayerMobile)from;
            if (pm == null)
            {
                return false;
            }

            int count = 0;

            foreach (Item verifier in World.Items.Values) // from.Backpack.Items)
            {
                if (verifier != null && verifier is TentDestroyer)  // TentVerifier)
                {
                    TentDestroyer td = (TentDestroyer)verifier;

                    if (td == null)
                        continue;

                    if (td.Player == pm)
                    {
                        count = count + 1;
                    }
                }
            }

            if (count < 1) //change this if you want players to own more than 1,2,3 etc.
            {
                return true;
            }

            return false;
        }

        public TentDeed(Serial serial)
            : base(serial)
        {
        }

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
    }
}
