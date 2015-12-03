using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Gumps;
using Server.Items;
using Server.Spells.Ninjitsu;
using Server.Multis;

namespace Server.Commands
{
    public class DressingCommand
    {
        private static Layer[] layer = new Layer[]
			{Layer.Bracelet,Layer.Neck,Layer.Earrings,Layer.Ring,
			Layer.TwoHanded,Layer.Gloves,Layer.Helm,Layer.Arms,
			Layer.InnerLegs,Layer.OuterLegs,Layer.Pants,Layer.InnerTorso,
			Layer.OuterTorso,Layer.Shirt,Layer.Shoes,Layer.Waist,Layer.Cloak};

        private enum DressingMethods
        {
            QuitAllAndDressAll,//unEquip all layers, get in pack in order to Equip back layers
            DressAllPossibleFromPack,//unEquip only some layers, in order to Equip all items equipable in pack
            DressIfLayerEmpty,//get in pack only items can be equiped without need to unequip something else
            NoMethod
        }

        public static void Initialize()
        {
            CommandSystem.Register("Equip", AccessLevel.Player, new CommandEventHandler(Dress_OnCommand));
        }

        [Usage("Equip")]
        [Description("Equip a set of items from a container.")]
        private static void Dress_OnCommand(CommandEventArgs e)
        {
            Dressing instance;
            if (!Dressing.Running) instance = new Dressing();

            Mobile from = e.Mobile;

            if (CanBegin(from))
            {
                from.Target = new DressingCommand.InternalTarget();
                from.SendMessage("Target a container to equip up.");
                from.SendMessage("Or target yourself to access menu.");
            }
        }

        private static bool CanBegin(Mobile from)
        {
            if (!from.Alive)
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019048); // I am dead and cannot do that.
                return false;
            }

            if (!ClearHands(from)) return false;

            if (from.Mounted)
            {
                from.SendLocalizedMessage(1005583);
                return false;
            }

            if (AnimalForm.UnderTransformation(from))
            {
                return false;
            }

            if (from.Frozen)
            {
                from.SendLocalizedMessage(500111); // You are frozen and cannot move.
                return false;
            }

            if (!from.Body.IsHuman)
            {
                return false;
            }

            from.DisruptiveAction();

            return true;
        }

        private static bool ClearHands(Mobile from)
        {
            Item oneHanded = from.FindItemOnLayer(Layer.OneHanded);
            Item twoHanded = from.FindItemOnLayer(Layer.TwoHanded);

            if (Core.AOS)
            {
                if (oneHanded != null)
                    from.AddToBackpack(oneHanded);

                if (twoHanded != null)
                    from.AddToBackpack(twoHanded);

                return true;
            }
            else if (oneHanded != null || twoHanded != null)
            {
                from.SendMessage("You must have free hands!");
                return false;
            }
            return true;
        }

        private static bool AllowThis(Mobile from, Item item)
        {
            if (item is Container)
            {
                Container pack = from.Backpack;
                Container bank = from.BankBox;

                if (item == pack || item.IsChildOf(pack) || item == bank || item.IsChildOf(bank)) return true;

                if (BaseHouse.FindHouseAt(item).Owner == from) return true;
            }
            return false;
        }

        private static void DressProcess(Mobile from, Container pack)
        {
            switch (Dressing.Method[from])
            {
                case DressingMethods.QuitAllAndDressAll:
                    {
                        QuitAllAndDressAll(from, pack);
                        break;
                    }
                case DressingMethods.DressAllPossibleFromPack:
                    {
                        DressAllPossibleFromPack(from, pack);
                        break;
                    }
                case DressingMethods.DressIfLayerEmpty:
                    {
                        DressIfLayerEmpty(from, pack);
                        break;
                    }
            }
        }

        private static void QuitAllAndDressAll(Mobile from, Container pack)
        {
            Item[] inPack = pack.FindItemsByType(typeof(Item));

            Item itemUp;
            Item itemDown;

            for (int l = 0; l < 17; l++)
            {
                itemDown = from.FindItemOnLayer(layer[l]);
                if (itemDown != null) pack.AddItem(itemDown);

                for (int i = 0; i < inPack.Length; i++)
                {
                    itemUp = inPack[i];

                    if (itemUp is BaseClothing || itemUp is BaseArmor || itemUp is BaseJewel)
                    {
                        if (itemUp.Layer == layer[l])
                        {
                            from.EquipItem(itemUp);
                            break;
                        }
                    }
                }
            }
        }

        private static void DressAllPossibleFromPack(Mobile from, Container pack)
        {
            Item[] inPack = pack.FindItemsByType(typeof(Item));

            Item itemUp;
            Item itemDown;

            for (int l = 0; l < 17; l++)
            {
                for (int i = 0; i < inPack.Length; i++)
                {
                    itemUp = inPack[i];

                    if (itemUp is BaseClothing || itemUp is BaseArmor || itemUp is BaseJewel)
                    {
                        if (itemUp.Layer == layer[l])
                        {
                            itemDown = from.FindItemOnLayer(layer[l]);
                            if (itemDown != null) pack.AddItem(itemDown);
                            from.EquipItem(itemUp);
                            break;
                        }
                    }
                }
            }
        }

        private static void DressIfLayerEmpty(Mobile from, Container pack)
        {
            Item[] inPack = pack.FindItemsByType(typeof(Item));

            Item itemUp;
            Item itemDown;

            for (int l = 0; l < 17; l++)
            {
                itemDown = from.FindItemOnLayer(layer[l]);

                if (itemDown == null)
                {
                    for (int i = 0; i < inPack.Length; i++)
                    {
                        itemUp = inPack[i];

                        if (itemUp is BaseClothing || itemUp is BaseArmor || itemUp is BaseJewel)
                        {
                            if (itemUp.Layer == layer[l])
                            {
                                from.EquipItem(itemUp);
                                break;
                            }
                        }
                    }
                }
            }

        }

        private class InternalTarget : Target
        {
            public InternalTarget()
                : base(8, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile)
                {
                    from.CloseGump(typeof(DressingGump));
                    from.SendGump(new DressingCommand.DressingGump(from));
                    return;
                }

                if (targeted is Item)
                {
                    Item item = (Item)targeted as Item;

                    if (item != null && AllowThis(from, item) && Dressing.Method.ContainsKey(from))
                    {
                        Timer timer = new DressTimer(from, (Container)item);
                        timer.Start();
                        from.Emote("*equip up*");
                    }
                    else
                    {
                        from.CloseGump(typeof(DressingGump));
                        from.SendGump(new DressingCommand.DressingGump(from));
                    }
                }

            }
        }

        private class DressTimer : Timer
        {
            public DressTimer(Mobile f, Container p)
                : base(TimeSpan.FromSeconds(3.0), TimeSpan.FromSeconds(1.0))
            {
                from = f;
                pack = p;
            }

            private Mobile from;
            private Container pack;

            protected override void OnTick()
            {
                DressProcess(from, pack);
                Stop();
            }
        }

        private class DressingGump : Gump
        {
            public DressingGump(Mobile from)
                : base(0, 0)
            {
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(50, 50, 680, 180, 9200);
                this.AddLabel(110, 70, 0, @"Unequip all your equipement and equip all possible from the container.");
                this.AddLabel(110, 110, 0, @"Equip all possible from the container, by unequiping all conflicting layers.");
                this.AddLabel(110, 150, 0, @"Equip all possible from the container only if corresponding layer is empty.");
                this.AddButton(70, 70, Active(from, DressingMethods.QuitAllAndDressAll), 211, (int)DressingMethods.QuitAllAndDressAll, GumpButtonType.Reply, 0);
                this.AddButton(70, 110, Active(from, DressingMethods.DressAllPossibleFromPack), 211, (int)DressingMethods.DressAllPossibleFromPack, GumpButtonType.Reply, 0);
                this.AddButton(70, 150, Active(from, DressingMethods.DressIfLayerEmpty), 211, (int)DressingMethods.DressIfLayerEmpty, GumpButtonType.Reply, 0);
                this.AddButton(300, 180, 247, 248, (int)DressingMethods.NoMethod, GumpButtonType.Reply, 0);
            }

            private int Active(Mobile from, DressingMethods current)
            {
                if (Dressing.Method.ContainsKey(from))
                {
                    if (Dressing.Method[from] == current) return 211;
                }
                return 210;
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                Mobile from = sender.Mobile;

                DressingMethods dressAction = DressingMethods.NoMethod;

                switch (info.ButtonID)
                {
                    case (int)DressingMethods.QuitAllAndDressAll:
                        {
                            dressAction = DressingMethods.QuitAllAndDressAll;
                            break;
                        }
                    case (int)DressingMethods.DressAllPossibleFromPack:
                        {
                            dressAction = DressingMethods.DressAllPossibleFromPack;
                            break;
                        }
                    case (int)DressingMethods.DressIfLayerEmpty:
                        {
                            dressAction = DressingMethods.DressIfLayerEmpty;
                            break;
                        }
                    case (int)DressingMethods.NoMethod:
                        {
                            from.CloseGump(typeof(DressingGump));
                            if (CanBegin(from)) from.Target = new DressingCommand.InternalTarget();
                            return;
                        }
                }

                if (dressAction != DressingMethods.NoMethod)
                {
                    if (Dressing.Method.ContainsKey(from)) Dressing.Method[from] = dressAction;
                    else Dressing.Method.Add(from, dressAction);
                }

                from.SendGump(new DressingCommand.DressingGump(from));
            }
        }

        private class Dressing : Item
        {
            public static Dictionary<Mobile, DressingMethods> Method = new Dictionary<Mobile, DressingMethods>();

            public static bool Running;

            public Dressing()
            {
                Running = true;
            }

            public Dressing(Serial s)
                : base(s)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                writer.Write((int)Method.Count);

                foreach (KeyValuePair<Mobile, DressingMethods> kvp in Method)
                {
                    writer.Write((Mobile)kvp.Key);
                    writer.Write((int)kvp.Value);
                }
            }

            public override void Deserialize(GenericReader reader)
            {
                Running = true;

                int count;

                count = reader.ReadInt();
                for (int i = 0; i < count; i++)
                {
                    Mobile mob = reader.ReadMobile();
                    Method[mob] = (DressingMethods)reader.ReadInt();
                }
            }
        }
    }
}

