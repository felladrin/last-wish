using System.Collections;
using Server.Mobiles;
using Server.Multis;
using Server.Items;
using Server.Network;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBcontroller : Item
    {
        private ArrayList eventItems = new ArrayList();
        private ArrayList eventMobiles = new ArrayList();

        [Constructable]
        public LBBcontroller()
            : base(0x14F0)
        {
            Name = "Lord British's Books Event Controller";
            Hue = 100;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel < AccessLevel.GameMaster)
            {
                from.SendMessage("You have initiated server shutdown.");
                from.SendMessage("The server is going down shortly.");
                this.Delete();
                return;
            }
            
            if (eventMobiles.Count != 0)
            {
                foreach (Item i in eventItems)
                {
                    i.Delete();
                }

                eventItems.Clear();

                foreach (Mobile m in eventMobiles)
                {
                    m.Delete();
                }

                eventMobiles.Clear();

                foreach (NetState state in NetState.Instances)
                    if (state.Mobile != null && state.Mobile.AccessLevel > AccessLevel.Player)
                        state.Mobile.SendMessage(66, "All NPCs and Items from Lord British's Books event have been removed.");
            }
            else
            {
                Map map = Map.Felucca;

                /////////////
                // MOBILES //
                /////////////

                LBBbaker baker = new LBBbaker();
                baker.Home = new Point3D(1451, 1614, 20);
                baker.MoveToWorld(baker.Home, map);
                eventMobiles.Add(baker);

                LBBbaldur baldur = new LBBbaldur();
                baldur.Home = new Point3D(5220, 730, -20);
                baldur.MoveToWorld(baldur.Home, map);
                eventMobiles.Add(baldur);

                LBBblacksmith blacksmith = new LBBblacksmith();
                blacksmith.Home = new Point3D(1420, 1547, 30);
                blacksmith.MoveToWorld(blacksmith.Home, map);
                eventMobiles.Add(blacksmith);

                LBBcarpenter carpenter = new LBBcarpenter();
                carpenter.Home = new Point3D(1432, 1595, 20);
                carpenter.MoveToWorld(carpenter.Home, map);
                eventMobiles.Add(carpenter);

                LBBcook cook = new LBBcook();
                cook.Home = new Point3D(1318, 1604, 50);
                cook.MoveToWorld(cook.Home, map);
                eventMobiles.Add(cook);

                LBBdrunkJeffrey drunkJeffrey = new LBBdrunkJeffrey();
                drunkJeffrey.Home = new Point3D(1412, 1655, 10);
                drunkJeffrey.MoveToWorld(drunkJeffrey.Home, map);
                eventMobiles.Add(drunkJeffrey);

                LBBeasternMageryShopVendor easternMageryShopVendor = new LBBeasternMageryShopVendor();
                easternMageryShopVendor.Home = new Point3D(1595, 1654, 10);
                easternMageryShopVendor.MoveToWorld(easternMageryShopVendor.Home, map);
                eventMobiles.Add(easternMageryShopVendor);

                LBBfisherman fisherman = new LBBfisherman();
                fisherman.Home = new Point3D(1489, 1749, -2);
                fisherman.MoveToWorld(fisherman.Home, map);
                eventMobiles.Add(fisherman);

                LBBleonard leonard = new LBBleonard();
                leonard.Home = new Point3D(1675, 1593, 7);
                leonard.MoveToWorld(leonard.Home, map);
                eventMobiles.Add(leonard);

                LBBlibrarian librarian = new LBBlibrarian();
                librarian.Home = new Point3D(1410, 1604, 30);
                librarian.MoveToWorld(librarian.Home, map);
                eventMobiles.Add(librarian);

                LBBlordBritish lordBritish = new LBBlordBritish();
                lordBritish.Home = new Point3D(1323, 1624, 55);
                lordBritish.MoveToWorld(lordBritish.Home, map);
                eventMobiles.Add(lordBritish);

                LBBmaid1 maid1 = new LBBmaid1();
                maid1.Home = new Point3D(1352, 1660, 72);
                maid1.MoveToWorld(maid1.Home, map);
                eventMobiles.Add(maid1);

                LBBmaid2 maid2 = new LBBmaid2();
                maid2.Home = new Point3D(1351, 1604, 72);
                maid2.MoveToWorld(maid2.Home, map);
                eventMobiles.Add(maid2);

                LBBmaid3 maid3 = new LBBmaid3();
                maid3.Home = new Point3D(1329, 1660, 72);
                maid3.MoveToWorld(maid3.Home, map);
                eventMobiles.Add(maid3);

                LBBmaid4 maid4 = new LBBmaid4();
                maid4.Home = new Point3D(1345, 1643, 50);
                maid4.MoveToWorld(maid4.Home, map);
                eventMobiles.Add(maid4);

                LBBmaid5 maid5 = new LBBmaid5();
                maid5.Home = new Point3D(1353, 1588, 50);
                maid5.MoveToWorld(maid5.Home, map);
                eventMobiles.Add(maid5);

                LBBnorthernMageryShopVendor northernMageryShopVendor = new LBBnorthernMageryShopVendor();
                northernMageryShopVendor.Home = new Point3D(1492, 1547, 35);
                northernMageryShopVendor.MoveToWorld(northernMageryShopVendor.Home, map);
                eventMobiles.Add(northernMageryShopVendor);

                LBBoriandur oriandur = new LBBoriandur();
                oriandur.Home = new Point3D(5323, 749, -20);
                oriandur.MoveToWorld(oriandur.Home, map);
                eventMobiles.Add(oriandur);

                LBBpriest priest = new LBBpriest();
                priest.Home = new Point3D(1452, 1589, 20);
                priest.MoveToWorld(priest.Home, map);
                eventMobiles.Add(priest);

                LBBguard guard1 = new LBBguard();
                guard1.Home = new Point3D(1323, 1626, 55);
                guard1.MoveToWorld(guard1.Home, map);
                guard1.Direction = guard1.GetDirectionTo(new Point3D(1328, 1624, 50));
                eventMobiles.Add(guard1);

                LBBguard guard2 = new LBBguard();
                guard2.Home = new Point3D(1323, 1622, 55);
                guard2.MoveToWorld(guard2.Home, map);
                guard2.Direction = guard2.GetDirectionTo(new Point3D(1328, 1624, 50));
                eventMobiles.Add(guard2);

                LBBguard guard3 = new LBBguard();
                guard3.Home = new Point3D(1328, 1627, 50);
                guard3.MoveToWorld(guard3.Home, map);
                guard3.Direction = guard3.GetDirectionTo(new Point3D(1328, 1624, 50));
                eventMobiles.Add(guard3);

                LBBguard guard4 = new LBBguard();
                guard4.Home = new Point3D(1328, 1621, 50);
                guard4.MoveToWorld(guard4.Home, map);
                guard4.Direction = guard4.GetDirectionTo(new Point3D(1328, 1624, 50));
                eventMobiles.Add(guard4);

                LBBguard guard5 = new LBBguard();
                guard5.Home = new Point3D(1334, 1627, 50);
                guard5.MoveToWorld(guard5.Home, map);
                guard5.Direction = guard5.GetDirectionTo(new Point3D(1328, 1624, 50));
                eventMobiles.Add(guard5);

                LBBguard guard6 = new LBBguard();
                guard6.Home = new Point3D(1334, 1621, 50);
                guard6.MoveToWorld(guard6.Home, map);
                guard6.Direction = guard6.GetDirectionTo(new Point3D(1328, 1624, 50));
                eventMobiles.Add(guard6);

                ///////////
                // ITEMS //
                ///////////

                LBBdiary1 diary1 = new LBBdiary1();
                diary1.MoveToWorld(new Point3D(1454, 1582, 30), map);
                eventItems.Add(diary1);

                LargeBoat boat = new LargeBoat();
                boat.TillerMan = null;
                boat.Facing = Direction.West;
                boat.MoveToWorld(new Point3D(1493, 1854, -5), map);
                eventItems.Add(boat);

                LBBdiary2 diary2 = new LBBdiary2();
                diary2.MoveToWorld(new Point3D(1496, 1854, -2), map);
                eventItems.Add(diary2);

                BattleAxe axe = new BattleAxe();
                axe.MoveToWorld(new Point3D(1490, 1854, -2), map);
                axe.Movable = false;
                eventItems.Add(axe);

                GlassBottle bottle = new GlassBottle();
                bottle.MoveToWorld(new Point3D(1497, 1854, -2), map);
                bottle.Movable = false;
                eventItems.Add(bottle);

                Backpack pack = new Backpack();
                pack.MoveToWorld(new Point3D(1494, 1854, -2), map);
                pack.Movable = false;
                eventItems.Add(pack);

                pack.DropItem(new GlassBottle());
                pack.DropItem(new GlassBottle());
                pack.DropItem(new FishingPole());
                pack.DropItem(new BigFish());

                SpidersSilk packItem2 = new SpidersSilk();
                packItem2.Amount = 3;
                pack.DropItem(packItem2);

                MandrakeRoot packItem3 = new MandrakeRoot();
                packItem3.Amount = 5;
                pack.DropItem(packItem3);

                Bloodmoss packItem4 = new Bloodmoss();
                packItem4.Amount = 3;
                pack.DropItem(packItem4);

                SulfurousAsh packItem5 = new SulfurousAsh();
                packItem5.Amount = 4;
                pack.DropItem(packItem5);

                foreach (NetState state in NetState.Instances)
                    if (state.Mobile != null && state.Mobile.AccessLevel > AccessLevel.Player)
                        state.Mobile.SendMessage(66, "All NPCs and Items have been generated. Ready to start Lord British's Books event.");
            }
        }

        public override void OnDelete()
        {
            foreach (Item i in eventItems)
            {
                i.Delete();
            }

            eventItems.Clear();

            foreach (Mobile m in eventMobiles)
            {
                m.Delete();
            }

            eventMobiles.Clear();

            foreach (NetState state in NetState.Instances)
                if (state.Mobile != null && state.Mobile.AccessLevel > AccessLevel.Player)
                    state.Mobile.SendMessage(66, "All NPCs and Items from Lord British's Books event have been removed.");
            
            base.OnDelete();
        }

        public LBBcontroller(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.WriteItemList(eventItems);
            writer.WriteMobileList(eventMobiles);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            eventItems = reader.ReadItemList();
            eventMobiles = reader.ReadMobileList();
        }
    }
}

