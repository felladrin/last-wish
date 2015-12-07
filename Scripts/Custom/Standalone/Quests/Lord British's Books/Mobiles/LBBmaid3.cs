using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBmaid3 : LBBbaseCreature
    {
        [Constructable]
        public LBBmaid3() : base()
        {
            Name = NameList.RandomName("female");
            Title = "the Maid";
            Female = true;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x191;
            Hue = 0x40E;
            HairItemID = 0x203B; // Short
            HairHue = 0x47A;
            RangeHome = 5;
            
            AddItem(new FancyShirt(0x14A));
            AddItem(new Shoes(0x462));
            AddItem(new Skirt(0x2A7));
            AddItem(new HalfApron());

            AddSpeech("Ah!!!");
            AddSpeech("Sorry, I'm scared about what happened, and didn't see you coming!");
            AddSpeech("Did you know? Some of our Lord British valious items were stolen! How come?!");
            AddSpeech("That never happened before...");
            AddSpeech("I think the guards might have done this... They're everywhere, after all.");
            AddSpeech("Er... Ah! Forget what I said! I shouldn't be accusing anyone like this, how rude of me!");
            AddSpeech("Anyway... No, I don't know who could have done this. I'm sorry...");
        }

        public LBBmaid3(Serial serial)
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
