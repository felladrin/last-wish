using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBmaid4 : LBBbaseCreature
    {
        [Constructable]
        public LBBmaid4() : base()
        {
            Name = NameList.RandomName("female");
            Title = "the Maid";
            Female = true;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x191;
            Hue = 0x416;
            HairItemID = 0x2049; // TwoPigTails
            HairHue = 0x45C;
            RangeHome = 0;
            
            AddItem(new Shirt(0x0FB));
            AddItem(new Shoes(0x462));
            AddItem(new Skirt(0x1AD));
            AddItem(new HalfApron());

            AddSpeech("Er... Hi there...");
            AddSpeech("You know... This is very weird, isn't it?");
            AddSpeech("Yesterday, it was all fine, and now... Everyone's worried...");
            AddSpeech("I'm worried too... Argh...");
            AddSpeech("*looks to both sides*");
            AddSpeech("Sorry, where was I?");
            AddSpeech("Ah, the theft. I wish I knew who made this. I'm worried they'll blame me! I could get killed!");
            AddSpeech("Sorry that I can't help you...");
        }

        public LBBmaid4(Serial serial)
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
