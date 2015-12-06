using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBfisherman : LBBbaseCreature
    {
        [Constructable]
        public LBBfisherman()
            : base()
        {
            Name = NameList.RandomName("male");
            Title = "the Fisherman";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x41E;
            HairItemID = 0x203D; // PonyTail
            HairHue = 0x47C;
            FacialHairItemID = 0x2041; // Mustache
            FacialHairHue = 0x47C;
            RangeHome = 5;

            AddItem(new Shirt(0x09F));
            AddItem(new Shoes(0x462));
            AddItem(new ShortPants(0x356));
            AddItem(new FishingPole());

            AddSpeech("Aye aye! Who's coming?");
            AddSpeech("Oh, we usually don't recieve kinds like you around here. How can I help you?");
            AddSpeech("");
            AddSpeech("Ah, yes! The fella you're describing have been around here sometimes.");
            AddSpeech("Pretty rich guy, yes? He bought a boat AND a fishing pole!");
            AddSpeech("HAHAHA! FANTASTIC!");
            AddSpeech("Oh, er... *caham* Last time I saw him, he drove his boat to the south, from the docks.");
            AddSpeech("... Or was it to the east? ...");
            AddSpeech("HAHAHA! I DON'T KNOW!");
            AddSpeech("Oh, er... *caham* Will you buy something? No? Have a nice day, then.");
            AddSpeech("");
            AddSpeech("");
            AddSpeech("HAHAHA!");
        }

        public LBBfisherman(Serial serial)
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
