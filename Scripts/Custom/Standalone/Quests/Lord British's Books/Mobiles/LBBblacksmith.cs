using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBblacksmith : LBBbaseCreature
    {
        [Constructable]
        public LBBblacksmith()
            : base()
        {
            Name = NameList.RandomName("male");
            Title = "the Blacksmith";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x41F;
            HairItemID = 0x2044; // Mohawk
            HairHue = 0x46A;
            FacialHairItemID = 0x2041; // Mustache
            FacialHairHue = 0x472;
            RangeHome = 5;

            AddItem(new Shirt(0x23F));
            AddItem(new Shoes(0x462));
            AddItem(new ShortPants(0x0EF));
            AddItem(new FullApron());
            AddItem(new Bascinet());
            AddItem(new SmithHammer());

            AddSpeech("Ah? Who?");
            AddSpeech("No, I'm sorry. I never saw anyone like the one you're describing.");
        }

        public LBBblacksmith(Serial serial)
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
