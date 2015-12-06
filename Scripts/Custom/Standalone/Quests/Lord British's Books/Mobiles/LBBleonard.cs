using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBleonard : LBBbaseCreature
    {
        [Constructable]
        public LBBleonard()
            : base()
        {
            Name = "Leonard";
            Title = "the Gentleman";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x41D;
            FacialHairItemID = 0x2041; // Mustache
            FacialHairHue = 0x47C;
            CantWalk = true;

            AddItem(new FancyShirt(0x14F));
            AddItem(new Boots(0x967));
            AddItem(new LongPants(0x260));

            AddSpeech("Good day! Whom I owe the pleasure of this visit?");
            AddSpeech("Oh, welcome, Mr... or Mrs...");
            AddSpeech("I'm sorry, I can't find my contact lenses, so, I can't see well. My apologies.");
            AddSpeech("How can I help you, by the way?");
            AddSpeech("");
            AddSpeech("... Bald? Black robe?... Hmm...");
            AddSpeech("...");
            AddSpeech("...");
            AddSpeech("... No, I don't remember seeing anyone like the one you're describing. I'm sorry...");
            AddSpeech("But hey, have a good day! *waves*");
        }

        public LBBleonard(Serial serial)
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
