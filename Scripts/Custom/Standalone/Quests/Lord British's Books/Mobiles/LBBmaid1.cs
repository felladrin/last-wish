using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBmaid1 : LBBbaseCreature
    {
        [Constructable]
        public LBBmaid1() : base()
        {
            Name = NameList.RandomName("female");
            Title = "the Maid";
            Female = true;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x191;
            Hue = 0x40E;
            HairItemID = 0x203D; // PonyTail
            HairHue = 0x471;
            RangeHome = 5;
            
            AddItem(new FancyShirt(0x1E3));
            AddItem(new Boots());
            AddItem(new Skirt(0x180));
            AddItem(new FullApron(0x1ED));

            AddSpeech("Ah, so you came for the thief... It is really a cruel person, isn't it?");
            AddSpeech("I can't tell much about it. All that I know is that the crime happened last night");
            AddSpeech("when our Lord British was having a dinner.");
            AddSpeech("Hmm...");
            AddSpeech("But I remember seeing someone with a black robe going upstairs, last night.");
            AddSpeech("I couldn't identify his face, but he surely isn't around here anymore. Maybe he's the one...");
            AddSpeech("I'm sorry I can't tell you anything else.");
        }

        public LBBmaid1(Serial serial)
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
