using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBdrunkJeffrey : LBBbaseCreature
    {
        [Constructable]
        public LBBdrunkJeffrey()
            : base()
        {
            Name = "Jeffrey";
            Title = "the Drunk";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x415;
            RangeHome = 5;

            AddItem(new Shirt(0x32C));
            AddItem(new Shoes(0x462));
            AddItem(new ShortPants(0x287));
            AddItem(new TricorneHat());

            AddSpeech("Hey, hoy! Ha hi hoo! Teeheehee....");
            AddSpeech("Find... Who? HEHEHEHE");
            AddSpeech("No, I didn't find him... But you know what? *hic*");
            AddSpeech("You just found a... Superstar! That's it! I'm a SUPER... Star.");
            AddSpeech("Heeheehee...");
            AddSpeech("I am the one you're looking for! See? Everyone... ... Loves me!");
            AddSpeech("*hic*");
            AddSpeech("DO you want a bottle of Ale?");
            AddSpeech("Maybe some Wine? Are you the sophisticated type? Hehe...");
            AddSpeech("I... Love you.");
            AddSpeech("I love you... With ALL... my forces.");
            AddSpeech("Yes, that's it. I will MARRY YOU! HAHAHAHA");
            AddSpeech("We'll reach the stars, and we'll be superstars together!");
            AddSpeech("I'll go to the space...");
            AddSpeech("And meet *hic* ... E.T.s...");
            AddSpeech("Yes...");
            AddSpeech("");
            AddSpeech("Will it rain todaaaayeee?");
            AddSpeech("Hehehe...");
            AddSpeech("If it rains, I will fill my bottle up! Horreeeey!");
            AddSpeech("I will be so happeeh... And WHEN I'M HAPPY...");
            AddSpeech("... I sing to God Felladrin.");
            AddSpeech("GOD FELLADRIN, WHO GAVE ME THE LIFE! THANK YOU FOR THIS *hic* ... BOTTLE OF ALE!");
            AddSpeech("YOU'RE THE LIGHT TO FOLLOW IN THIS HEGEMONY!");
            AddSpeech("I'M JUST A BIT OF BINARY CODE, BUT I OWE YOU MY THANKS EVEN THOUGH!");
            AddSpeech("*hic*");
            AddSpeech("... Whaaah~t?");
            AddSpeech("I think I'll take a nap... Teeheehee.... Goodbye, honoured one... Heehee...");
            AddSpeech("*hic*");
        }

        public LBBdrunkJeffrey(Serial serial)
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
