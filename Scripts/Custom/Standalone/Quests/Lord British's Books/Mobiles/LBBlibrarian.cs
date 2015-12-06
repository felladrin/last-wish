using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBlibrarian : LBBbaseCreature
    {
        [Constructable]
        public LBBlibrarian() : base()
        {
            Name = NameList.RandomName("female");
            Title = "the Librarian";
            Female = true;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x191;
            Hue = 0x415;
            HairItemID = 0x2045; // PageboyHair
            HairHue = 0x46A;
            CantWalk = true;
            
            AddItem(new PlainDress(0x0E0));
            AddItem(new FeatheredHat(0x21B));

            AddSpeech("You're looking for...?");
            AddSpeech("... A bald man? Hmm...");
            AddSpeech("Well, you know, we recieve lots of citizen around everyday. but if I can remember...");
            AddSpeech("There is Jeffrey, a bald drunk beggar who always come here and ask me for some gold!");
            AddSpeech("Argh, I hate him!");
            AddSpeech("... I've sold a good ammount of blank books to a pale man. Always wearing that black robe.");
            AddSpeech("Ack, that fish smell he always had... It was terrible.");
            AddSpeech("There is, also, a great customer of ours, Leonard. He is quite a gentleman.");
            AddSpeech("He lives on the eastern part of Britain, near the East Bank.");
            AddSpeech("Yeah, I can only remember these bald people. Hmm...");
            AddSpeech("... Anyway! You're wasting my time here! I got lots of work to do! Be gone!");
        }

        public LBBlibrarian(Serial serial)
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
