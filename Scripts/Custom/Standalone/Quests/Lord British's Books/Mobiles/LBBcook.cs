using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBcook : LBBbaseCreature
    {
        [Constructable]
        public LBBcook()
            : base()
        {
            Name = NameList.RandomName("male");
            Title = "the Cook";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x41E;
            HairItemID = 0x203B; // Short
            HairHue = 0x47B;
            FacialHairItemID = 0x203f; // Short
            FacialHairHue = 0x47B;
            RangeHome = 5;

            AddItem(new Shirt(0x2DD));
            AddItem(new Boots(0x33E));
            AddItem(new LongPants(0x2EC));
            AddItem(new FullApron());
            AddItem(new SkinningKnife());

            AddSpeech("Hmm.");
            AddSpeech("What? That worthless bastard? Yes, good thing he is gone! He gave me some big headaches!");
            AddSpeech("Why?! Because he wouldn't work fast enough, that's why! He always had 'something else' to do!");
            AddSpeech("Last time I saw him? Of course it was in the kitchen! Do you think I'll ever stop working?!");
            AddSpeech("You want my opinion? I don't think it was him at all. He's just some pile of lazy garbage!");
            AddSpeech("I know nothing about him! And hey, the King needs to eat, you know?! Let me do my job!");
        }

        public LBBcook(Serial serial)
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
