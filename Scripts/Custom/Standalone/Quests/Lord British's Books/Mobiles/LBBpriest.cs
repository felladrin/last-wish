using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBpriest : LBBbaseCreature
    {
        [Constructable]
        public LBBpriest() : base()
        {
            Name = NameList.RandomName("male");
            Title = "the Priest";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x41D;
            HairItemID = 0x2048; // ReceedingHair
            HairHue = 0x47A;
            CantWalk = true;
            
            AddItem(new Shirt(0x29C));
            AddItem(new Shoes(0x462));
            AddItem(new LongPants(0x1FB));
            AddItem(new Robe(0x2E4));

            AddSpeech("Welcome, young one.");
            AddSpeech("... A bald man? Yes, yes. I think I've seen him a couple of times.");
            AddSpeech("He would come here everyday, always at the sun's peak. No clue what was he doing, though.");
            AddSpeech("What was his name? ... Er...");
            AddSpeech("... Something with B... I really can't remember, I'm sorry.");
            AddSpeech("Uh... I've noticed he would write a few papers here at the church.");
            AddSpeech("I think he forgot some of these papers around here.");
            AddSpeech("Feel free to look around. May peace be with you.");
        }

        public LBBpriest(Serial serial)
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
