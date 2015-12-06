using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBcarpenter : LBBbaseCreature
    {
        [Constructable]
        public LBBcarpenter()
            : base()
        {
            Name = NameList.RandomName("male");
            Title = "the Carpenter";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x41F;
            HairItemID = 0x2046; // Buns
            HairHue = 0x47A;
            CantWalk = true;

            AddItem(new Shirt(0x15E));
            AddItem(new Shoes(0x462));
            AddItem(new LongPants(0x159));

            AddSpeech("Hi! How can I help you?");
            AddSpeech("");
            AddSpeech("Eh? No, I've seen no one like the one you're describing. *smiles*");
        }

        public LBBcarpenter(Serial serial)
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
