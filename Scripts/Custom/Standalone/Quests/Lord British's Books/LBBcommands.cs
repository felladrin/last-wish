using System.Collections.Generic;
using Server.Mobiles;
using Server.Multis;
using Server.Commands;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBcommands
    {
        public static void Initialize()
        {
            CommandSystem.Register("LBBstart", AccessLevel.GameMaster, new CommandEventHandler(LBBstart_OnCommand));
            CommandSystem.Register("LBBend", AccessLevel.GameMaster, new CommandEventHandler(LBBend_OnCommand));
        }

        [Usage("LBBstart")]
        [Description("Starts Lord British's Books event. Places all npcs on the world.")]
        public static void LBBstart_OnCommand(CommandEventArgs e)
        {
            Map map = Map.Felucca;

            /////////////
            // MOBILES //
            /////////////
            
            LBBbaker baker = new LBBbaker();
            baker.Home = new Point3D(1451, 1614, 20);
            baker.MoveToWorld(baker.Home, map);

            LBBbaldur baldur = new LBBbaldur();
            baldur.Home = new Point3D(5220, 730, -20);
            baldur.MoveToWorld(baldur.Home, map);

            LBBblacksmith blacksmith = new LBBblacksmith();
            blacksmith.Home = new Point3D(1420, 1547, 30);
            blacksmith.MoveToWorld(blacksmith.Home, map);

            LBBcarpenter carpenter = new LBBcarpenter();
            carpenter.Home = new Point3D(1432, 1595, 20);
            carpenter.MoveToWorld(carpenter.Home, map);

            LBBcook cook = new LBBcook();
            cook.Home = new Point3D(1318, 1604, 50);
            cook.MoveToWorld(cook.Home, map);

            LBBdrunkJeffrey drunkJeffrey = new LBBdrunkJeffrey();
            drunkJeffrey.Home = new Point3D(1412, 1655, 10);
            drunkJeffrey.MoveToWorld(drunkJeffrey.Home, map);

            LBBeasternMageryShopVendor easternMageryShopVendor = new LBBeasternMageryShopVendor();
            easternMageryShopVendor.Home = new Point3D(1595, 1654, 10);
            easternMageryShopVendor.MoveToWorld(easternMageryShopVendor.Home, map);

            LBBfisherman fisherman = new LBBfisherman();
            fisherman.Home = new Point3D(1489, 1749, -2);
            fisherman.MoveToWorld(fisherman.Home, map);

            LBBleonard leonard = new LBBleonard();
            leonard.Home = new Point3D(1675, 1593, 7);
            leonard.MoveToWorld(leonard.Home, map);

            LBBlibrarian librarian = new LBBlibrarian();
            librarian.Home = new Point3D(1410, 1604, 30);
            librarian.MoveToWorld(librarian.Home, map);

            LBBlordBritish lordBritish = new LBBlordBritish();
            lordBritish.Home = new Point3D(1323, 1624, 55);
            lordBritish.MoveToWorld(lordBritish.Home, map);

            LBBmaid1 maid1 = new LBBmaid1();
            maid1.Home = new Point3D(1352, 1660, 72);
            maid1.MoveToWorld(maid1.Home, map);

            LBBmaid2 maid2 = new LBBmaid2();
            maid2.Home = new Point3D(1351, 1604, 72);
            maid2.MoveToWorld(maid2.Home, map);

            LBBmaid3 maid3 = new LBBmaid3();
            maid3.Home = new Point3D(1329, 1660, 72);
            maid3.MoveToWorld(maid3.Home, map);

            LBBmaid4 maid4 = new LBBmaid4();
            maid4.Home = new Point3D(1345, 1643, 50);
            maid4.MoveToWorld(maid4.Home, map);

            LBBmaid5 maid5 = new LBBmaid5();
            maid5.Home = new Point3D(1353, 1588, 50);
            maid5.MoveToWorld(maid5.Home, map);

            LBBnorthernMageryShopVendor northernMageryShopVendor = new LBBnorthernMageryShopVendor();
            northernMageryShopVendor.Home = new Point3D(1492, 1547, 35);
            northernMageryShopVendor.MoveToWorld(northernMageryShopVendor.Home, map);

            LBBoriandur oriandur = new LBBoriandur();
            oriandur.Home = new Point3D(5323, 749, -20);
            oriandur.MoveToWorld(oriandur.Home, map);

            LBBpriest priest = new LBBpriest();
            priest.Home = new Point3D(1452, 1589, 20);
            priest.MoveToWorld(priest.Home, map);

            LBBguard guard1 = new LBBguard();
            guard1.Home = new Point3D(1323, 1626, 55);
            guard1.MoveToWorld(guard1.Home, map);
            guard1.Direction = guard1.GetDirectionTo(new Point3D(1330, 1624, 50));

            LBBguard guard2 = new LBBguard();
            guard2.Home = new Point3D(1323, 1622, 55);
            guard2.MoveToWorld(guard2.Home, map);
            guard2.Direction = guard2.GetDirectionTo(new Point3D(1330, 1624, 50));

            LBBguard guard3 = new LBBguard();
            guard3.Home = new Point3D(1328, 1627, 50);
            guard3.MoveToWorld(guard3.Home, map);
            guard3.Direction = guard3.GetDirectionTo(new Point3D(1330, 1624, 50));

            LBBguard guard4 = new LBBguard();
            guard4.Home = new Point3D(1328, 1621, 50);
            guard4.MoveToWorld(guard4.Home, map);
            guard4.Direction = guard4.GetDirectionTo(new Point3D(1330, 1624, 50));

            LBBguard guard5 = new LBBguard();
            guard5.Home = new Point3D(1334, 1627, 50);
            guard5.MoveToWorld(guard5.Home, map);
            guard5.Direction = guard5.GetDirectionTo(new Point3D(1330, 1624, 50));

            LBBguard guard6 = new LBBguard();
            guard6.Home = new Point3D(1334, 1621, 50);
            guard6.MoveToWorld(guard6.Home, map);
            guard6.Direction = guard6.GetDirectionTo(new Point3D(1330, 1624, 50));

            ///////////
            // ITEMS //
            ///////////

            LBBdiary1 diary1 = new LBBdiary1();
            diary1.MoveToWorld(new Point3D(1454, 1582, 30), map);

            LargeBoat boat = new LargeBoat();
            boat.TillerMan = null;
            boat.Facing = Direction.West;
            boat.MoveToWorld(new Point3D(1493, 1854, -5), map);

            LBBdiary2 diary2 = new LBBdiary2();
            diary2.MoveToWorld(new Point3D(1496, 1854, -2), map);

            BattleAxe axe = new BattleAxe();
            axe.MoveToWorld(new Point3D(1490, 1854, -2), map);
            axe.Movable = false;

            GlassBottle bottle = new GlassBottle();
            bottle.MoveToWorld(new Point3D(1497, 1854, -2), map);
            bottle.Movable = false;

            Backpack pack = new Backpack();
            pack.MoveToWorld(new Point3D(1494, 1854, -2), map);
            pack.Movable = false;
            pack.DropItem(new GlassBottle());
            pack.DropItem(new GlassBottle());
            pack.DropItem(new FishingPole());

            BigFish packItem1 = new BigFish();
            packItem1.Amount = 8;
            pack.DropItem(packItem1);

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

            e.Mobile.SendMessage("All NPCs and Items have been generated. Ready to start Lord British's Books event.");
        }

        [Usage("LBBend")]
        [Description("Ends Lord British's Books event. Removes all npcs from the world.")]
        public static void LBBend_OnCommand(CommandEventArgs e)
        {
            List<IEntity> list = new List<IEntity>();

            foreach (Mobile m in World.Mobiles.Values)
            {
                if (m is LBBbaseCreature || m is LBBbaldur || m is LBBoriandur || m is LBBguard)
                {
                    list.Add(m);
                }
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is LBBdiary1 || i is LBBdiary2)
                {
                    list.Add(i);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Delete();
            }

            e.Mobile.SendMessage("All NPCs and Items from Lord British's Books event have been removed.");
        }
    }
}