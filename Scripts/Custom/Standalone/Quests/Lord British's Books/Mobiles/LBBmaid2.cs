using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBmaid2 : LBBbaseCreature
    {
        [Constructable]
        public LBBmaid2() : base()
        {
            Name = NameList.RandomName("female");
            Title = "the Maid";
            Female = true;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x191;
            Hue = 0x40D;
            HairItemID = 0x2047; // Afro
            HairHue = 0x469;
            RangeHome = 5;
            
            AddItem(new FancyShirt(0x116));
            AddItem(new Boots());
            AddItem(new Skirt(0x283));
            AddItem(new HalfApron());

            AddSpeech("Hello there. I've heard you're seeking for that thief.");
            AddSpeech("Well, I wasn't around here last night, so, I really can't help you much...");
            AddSpeech("");
            AddSpeech("... Oh, wait a second! I've remembered something at least intriguing!");
            AddSpeech("This bald man... He started working here some days ago. He was surely a bit strange...");
            AddSpeech("He was the cook's helper. I bet he had lots of work to do at the kitchen.");
            AddSpeech("Everyday, at 12 o'clock, he would leave the castle and walk into Britain's church.");
            AddSpeech("Since it was lunch time, no one was there at the time, except for the priest.");
            AddSpeech("Though, since yesterday's event, no one can find him anywhere! How strange is this?!");
            AddSpeech("Oh... Well. I hope that was useful.");
        }

        public LBBmaid2(Serial serial)
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
