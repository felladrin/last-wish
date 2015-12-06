using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Commands;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("taskmaster corpse")]
    public class QuestGiver : Mobile
    {
        public virtual bool IsInvulnerable { get { return true; } }
        [Constructable]
        public QuestGiver()
        {
            InitStats(31, 41, 51);

            Hue = Utility.RandomSkinHue();
            Body = 0x190;
            Blessed = true;

            AddItem(new Robe(Utility.RandomNeutralHue()));
            AddItem(new Boots());
            Utility.AssignRandomHair(this);
            Direction = Direction.South;
            Name = NameList.RandomName("male");
            Title = "the taskmaster";
            CantWalk = true;
        }

        public QuestGiver(Serial serial)
            : base(serial)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new QuestGiverEntry(from, this));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public class QuestGiverEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public QuestGiverEntry(Mobile from, Mobile giver)
                : base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }

            public override void OnClick()
            {
                if (!(m_Mobile is PlayerMobile))
                    return;

                PlayerMobile mobile = (PlayerMobile)m_Mobile;

                {
                    if (!mobile.HasGump(typeof(QuestGiver_gump)))
                    {
                        mobile.SendGump(new QuestGiver_gump(mobile));
                    }
                }
            }
        }

        private static void GetRandomAOSStats(out int attributeCount, out int min, out int max, int level)
        {
            int rnd = Utility.Random(15);

            if (level == 6)
            {
                attributeCount = Utility.RandomMinMax(2, 6);
                min = 20; max = 70;
            }
            else if (level == 5)
            {
                attributeCount = Utility.RandomMinMax(2, 4);
                min = 20; max = 50;
            }
            else if (level == 4)
            {
                attributeCount = Utility.RandomMinMax(2, 3);
                min = 20; max = 40;
            }
            else if (level == 3)
            {
                attributeCount = Utility.RandomMinMax(1, 3);
                min = 10; max = 30;
            }
            else if (level == 2)
            {
                attributeCount = Utility.RandomMinMax(1, 2);
                min = 10; max = 30;
            }
            else
            {
                attributeCount = 1;
                min = 10; max = 20;
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;

            if (mobile != null)
            {
                if (dropped is Gold && dropped.Amount == 5)
                {
                    mobile.AddToBackpack(new QuestScroll(1));
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Return the Quest parchment to me when you are done...for your reward.", mobile.NetState);
                    return true;
                }
                else if (dropped is Gold && dropped.Amount == 10)
                {
                    mobile.AddToBackpack(new QuestScroll(2));
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Return the Quest parchment to me when you are done...for your reward.", mobile.NetState);
                    return true;
                }
                else if (dropped is Gold && dropped.Amount == 15)
                {
                    mobile.AddToBackpack(new QuestScroll(3));
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Return the Quest parchment to me when you are done...for your reward.", mobile.NetState);
                    return true;
                }
                else if (dropped is Gold && dropped.Amount == 20)
                {
                    mobile.AddToBackpack(new QuestScroll(4));
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Return the Quest parchment to me when you are done...for your reward.", mobile.NetState);
                    return true;
                }
                else if (dropped is Gold && dropped.Amount == 25)
                {
                    mobile.AddToBackpack(new QuestScroll(5));
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Return the Quest parchment to me when you are done...for your reward.", mobile.NetState);
                    return true;
                }
                else if (dropped is Gold && dropped.Amount == 30)
                {
                    mobile.AddToBackpack(new QuestScroll(6));
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Return the Quest parchment to me when you are done...for your reward.", mobile.NetState);
                    return true;
                }
                else if (dropped is Gold)
                {
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "That is not the amount I am looking for.", mobile.NetState);
                    return false;
                }
                else if (dropped is QuestScroll)
                {
                    QuestScroll m_Quest = (QuestScroll)dropped;

                    if (m_Quest.NNeed > m_Quest.NGot)
                    {
                        mobile.AddToBackpack(dropped);
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "You have not completed this quest.", mobile.NetState);
                        return false;
                    }

                    string sMessage = "";
                    if (m_Quest.NType == 1) { sMessage = "I see you were victorious. Here is your reward."; }
                    else { sMessage = "Ahh...you have found " + m_Quest.NItemName + "! Here is your reward"; }

                    if (Utility.RandomMinMax(1, 4) == 1)
                    {
                        mobile.AddToBackpack(new Gold(m_Quest.NLevel * Utility.RandomMinMax(125, 200)));
                    }
                    else
                    {

                        mobile.AddToBackpack(new Gold(m_Quest.NLevel * Utility.RandomMinMax(75, 150)));

                        Item item;

                        if (Core.AOS)
                            item = Loot.RandomArmorOrShieldOrWeaponOrJewelry();
                        else
                            item = Loot.RandomArmorOrShieldOrWeapon();

                        if (item is BaseWeapon)
                        {
                            BaseWeapon weapon = (BaseWeapon)item;

                            if (Core.AOS)
                            {
                                int attributeCount;
                                int min, max;

                                GetRandomAOSStats(out attributeCount, out min, out max, m_Quest.NLevel);

                                BaseRunicTool.ApplyAttributesTo(weapon, attributeCount, min, max);
                            }
                            else
                            {
                                weapon.DamageLevel = (WeaponDamageLevel)Utility.Random(6);
                                weapon.AccuracyLevel = (WeaponAccuracyLevel)Utility.Random(6);
                                weapon.DurabilityLevel = (WeaponDurabilityLevel)Utility.Random(6);
                            }

                            mobile.AddToBackpack(item);
                        }
                        else if (item is BaseArmor)
                        {
                            BaseArmor armor = (BaseArmor)item;

                            if (Core.AOS)
                            {
                                int attributeCount;
                                int min, max;

                                GetRandomAOSStats(out attributeCount, out min, out max, m_Quest.NLevel);

                                BaseRunicTool.ApplyAttributesTo(armor, attributeCount, min, max);
                            }
                            else
                            {
                                armor.ProtectionLevel = (ArmorProtectionLevel)Utility.Random(6);
                                armor.Durability = (ArmorDurabilityLevel)Utility.Random(6);
                            }

                            mobile.AddToBackpack(item);
                        }
                        else if (item is BaseHat)
                        {
                            BaseHat hat = (BaseHat)item;

                            if (Core.AOS)
                            {
                                int attributeCount;
                                int min, max;

                                GetRandomAOSStats(out attributeCount, out min, out  max, m_Quest.NLevel);

                                BaseRunicTool.ApplyAttributesTo(hat, attributeCount, min, max);
                            }

                            mobile.AddToBackpack(item);
                        }
                        else if (item is BaseJewel)
                        {
                            int attributeCount;
                            int min, max;

                            GetRandomAOSStats(out attributeCount, out min, out max, m_Quest.NLevel);

                            BaseRunicTool.ApplyAttributesTo((BaseJewel)item, attributeCount, min, max);

                            mobile.AddToBackpack(item);
                        }
                    }
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, mobile.NetState);

                    dropped.Delete();

                    return true;
                }
                else
                {
                    mobile.AddToBackpack(dropped);
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need for this...", mobile.NetState); return true;
                }
            }

            return false;
        }
    }
}

namespace Server.Gumps
{
    public class QuestGiver_gump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("QuestGiver_gump", AccessLevel.GameMaster, new CommandEventHandler(QuestGiver_gump_OnCommand));
        }

        private static void QuestGiver_gump_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new QuestGiver_gump(e.Mobile));
        }

        public QuestGiver_gump(Mobile owner)
            : base(50, 50)
        {
            AddPage(0);
            AddImageTiled(54, 33, 369, 400, 2624);
            AddAlphaRegion(54, 33, 369, 400);
            AddImageTiled(416, 39, 44, 389, 203);

            AddImage(97, 49, 9005);
            AddImageTiled(58, 39, 29, 390, 10460);
            AddImageTiled(412, 37, 31, 389, 10460);
            AddLabel(140, 60, 0x34, "The Taskmaster");

            AddHtml(107, 140, 300, 230, " < BODY > " +
                //////////////////////  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx  THIS IS THE LENGTH OF THE TEXT ALLOWED //
            "<BASEFONT COLOR=YELLOW>Hail brave adventurer. I am the local<BR>" +
            "<BASEFONT COLOR=YELLOW>taskmaster. If anything needs to be<BR>" +
            "<BASEFONT COLOR=YELLOW>done around here...I am the one to see.<BR>" +
            "<BASEFONT COLOR=YELLOW>Although I am not supposed to hire any<BR>" +
            "<BASEFONT COLOR=YELLOW>citizens, you look like you can handle<BR>" +
            "<BASEFONT COLOR=YELLOW>yourself. Of course, I could get in<BR>" +
            "<BASEFONT COLOR=YELLOW>much trouble if they find out that I<BR>" +
            "<BASEFONT COLOR=YELLOW>let slip what needs to be done, as gold<BR>" +
            "<BASEFONT COLOR=YELLOW>is rare and usually the nobles want to<BR>" +
            "<BASEFONT COLOR=YELLOW>get the riches for themselves.<BR>" +
            "<BASEFONT COLOR=YELLOW><BR>" +
            "<BASEFONT COLOR=YELLOW>I'll tell you what, you slip a few gold<BR>" +
            "<BASEFONT COLOR=YELLOW>coins my way, and I will be a little<BR>" +
            "<BASEFONT COLOR=YELLOW>careless about what I say. The more<BR>" +
            "<BASEFONT COLOR=YELLOW>gold I get...the more careless I will<BR>" +
            "<BASEFONT COLOR=YELLOW>speak.<BR>" +
            "<BASEFONT COLOR=YELLOW><BR>" +
            "<BASEFONT COLOR=YELLOW>5 Gold - Level 1 Quest<BR>" +
            "<BASEFONT COLOR=YELLOW>10 Gold - Level 2 Quest<BR>" +
            "<BASEFONT COLOR=YELLOW>15 Gold - Level 3 Quest<BR>" +
            "<BASEFONT COLOR=YELLOW>20 Gold - Level 4 Quest<BR>" +
            "<BASEFONT COLOR=YELLOW>25 Gold - Level 5 Quest<BR>" +
            "<BASEFONT COLOR=YELLOW>30 Gold - Level 6 Quest<BR>" +
            "<BASEFONT COLOR=YELLOW><BR>" +
            "<BASEFONT COLOR=YELLOW>Simply follow the quest by targeting a<BR>" +
            "<BASEFONT COLOR=YELLOW>corpse of a slain creature. The corpse<BR>" +
            "<BASEFONT COLOR=YELLOW>must be either a creature you are in a<BR>" +
            "<BASEFONT COLOR=YELLOW>quest to slay...or a creature in the<BR>" +
            "<BASEFONT COLOR=YELLOW>area you are seeking an item.<BR>" +
            "<BASEFONT COLOR=YELLOW><BR>" +
            "<BASEFONT COLOR=YELLOW>Rewards very from much gold or some<BR>" +
            "<BASEFONT COLOR=YELLOW>gold and a magical item.<BR>" +
            "</BODY>", false, true);

            AddImage(430, 9, 10441);
            AddImageTiled(40, 38, 17, 391, 9263);
            AddImage(6, 25, 10421);
            AddImage(34, 12, 10420);
            AddImageTiled(94, 25, 342, 15, 10304);
            AddImageTiled(40, 427, 415, 16, 10304);
            AddImage(-10, 314, 10402);
            AddImage(56, 150, 10411);
            AddImage(155, 120, 2103);
            AddImage(136, 84, 96);
            AddButton(225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            switch (info.ButtonID)
            {
                case 0: { break; }
            }
        }
    }
}