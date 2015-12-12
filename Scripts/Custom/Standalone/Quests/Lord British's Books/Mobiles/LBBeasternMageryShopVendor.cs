using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBeasternMageryShopVendor : LBBbaseCreature
    {
        [Constructable]
        public LBBeasternMageryShopVendor()
            : base()
        {
            Name = NameList.RandomName("male");
            Title = "the Mage";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x415;
            HairItemID = 0x203C; // Long
            HairHue = 0x473;
            RangeHome = 5;

            AddItem(new Shirt(0x09F));
            AddItem(new ShortPants(0x356));
            AddItem(new Shoes(0x462));
            AddItem(new Robe(0x1EE));

            AddSpeech("*gasp* Howdy, traveler. What brings you to my humble magery shop?");
            AddSpeech("Ahhh... A bald man wearing black. Let's see... My last customer... Yes, it was him!");
            AddSpeech("How could I forget, anyway? Business are pretty terrible around this part of Britain.");
            AddSpeech("My 'good' Lord British only worries himself with that fancy part of Britain. And us? ...");
            AddSpeech("... We are the forgotten.");
            AddSpeech("Ah, er... Anyway, I'm sorry. Yes. Yes. I saw him. He was quite nervous and in a hurry.");
            AddSpeech("I also forgot to ask his name... *sigh*");
            AddSpeech("If I'm correct, he asked me a few reagents... They were:");
            AddSpeech("Blood Moss, Mandrake Root, Spiders' Silk and Sulfurous Ash.");
            AddSpeech("By the way... In his flutter, he left some papers drop to the floor.");
            AddSpeech("He quickly grabbed them all... I tried to help, but he yelled at me!");
            AddSpeech("Oh, I remember reading one of the papers with some red ink and a hand-drawn map...");
            AddSpeech("It was written... 'De..' Er... I don't know.");
            AddSpeech("Deceit? Oh, maybe it was Despise! ... Maybe Destard...");
            AddSpeech("... I really don't know, I'm sorry. But I hope I've somehow helped you.");
            AddSpeech("Anyway...");
            AddSpeech("So... That means you're not interested in buying anything? *sigh*");
        }

        public LBBeasternMageryShopVendor(Serial serial)
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
