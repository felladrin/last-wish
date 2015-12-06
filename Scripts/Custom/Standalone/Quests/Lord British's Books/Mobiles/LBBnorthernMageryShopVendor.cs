using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBnorthernMageryShopVendor : LBBbaseCreature
    {
        [Constructable]
        public LBBnorthernMageryShopVendor()
            : base()
        {
            Name = NameList.RandomName("male");
            Title = "the Mage";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x40D;
            HairItemID = 0x203C; // Long
            HairHue = 0x3FF;
            FacialHairItemID = 0x203E; // LongBeard
            FacialHairHue = 0x3FF;
            RangeHome = 5;

            AddItem(new Shirt(0x15E));
            AddItem(new LongPants(0x159));
            AddItem(new Shoes(0x462));
            AddItem(new Robe(0x258));

            AddSpeech("Welcome, fellow brother. What do you seek for today's journey?");
            AddSpeech("If I sold reagents to... A bald guy? Black robe?");
            AddSpeech("No, I'm sorry... I don't remember anyone like this aroud here in these last days.");
            AddSpeech("We recieve lots of customers here, everyday, but I don't think I would miss a face.");
            AddSpeech("Then, he definitely didn't come here. I hope that helps.");
            AddSpeech("Have a good journey. may the peace be with you!");
        }

        public LBBnorthernMageryShopVendor(Serial serial)
            : base(serial)
        {
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
    }
}
