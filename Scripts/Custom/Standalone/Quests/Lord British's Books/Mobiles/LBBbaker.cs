using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBbaker : LBBbaseCreature
    {
        [Constructable]
        public LBBbaker()
            : base()
        {
            Name = NameList.RandomName("male");
            Title = "the Baker";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x41D;
            HairItemID = 0x203B; // Short
            HairHue = 0x472;
            FacialHairItemID = 0x2041; // Mustache
            FacialHairHue = 0x472;
            CantWalk = true;

            AddItem(new Shirt(0x29C));
            AddItem(new Shoes(0x462));
            AddItem(new LongPants(0x1FB));
            AddItem(new HalfApron());
            AddItem(new Dagger());

            AddSpeech("Hiho! Welcome, young one! How may I be at your service?");
            AddSpeech("Bald man? Black Robe? Hmm...");
            AddSpeech("Ah, yes! I never forget my customers! They're who I work for, after all! *smiles*");
            AddSpeech("Well, I don't know who he is. But if I'm correct, I think I saw him carrying a pickaxe.");
            AddSpeech("Maybe he was a worker at the northern mines? I don't know.");
            AddSpeech("And we shouldn't speak about others like this, don't you know? *smiles*");
            AddSpeech("I hope you can find your path, young one! Good day!");
        }

        public LBBbaker(Serial serial)
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
