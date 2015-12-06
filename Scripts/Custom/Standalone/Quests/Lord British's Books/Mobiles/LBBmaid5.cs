using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBmaid5 : LBBbaseCreature
    {
        [Constructable]
        public LBBmaid5() : base()
        {
            Name = NameList.RandomName("female");
            Title = "the Maid";
            Female = true;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x191;
            Hue = 0x416;
            HairItemID = 0x203C; // Long
            HairHue = 0x472;
            RangeHome = 5;
            
            AddItem(new Shirt(0x126));
            AddItem(new Shoes(0x462));
            AddItem(new Skirt(0x2C4));
            AddItem(new FullApron(0x3B6));

            AddSpeech("HA-HA!");
            AddSpeech("I knew it was going to happen. I told them all!");
            AddSpeech("And you know what? That happened! Finally!");
            AddSpeech("I mean... Not that I wanted that to happen. No. Not at all.");
            AddSpeech("But I adviced them! I told them to be more cautious with our king's possessions!");
            AddSpeech("And did they hear me?! NO! Of course not! No one would pay attention to that!");
            AddSpeech("'Aaah, but it's already very secure inside the castle'... Argh!");
            AddSpeech("Maybe they'll learn it now!");
            AddSpeech("By the way, I have no clue on who might have done this.");
        }

        public LBBmaid5(Serial serial)
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
