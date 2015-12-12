using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBlordBritish : LBBbaseCreature
    {
        [Constructable]
        public LBBlordBritish()
            : base()
        {
            Name = "Lord British";
            Female = false;
            SpeechHue = Utility.RandomDyedHue();

            Body = 0x190;
            Hue = 0x83F3;
            RangeHome = 0;

            AddItem(new LordBritishSuit());

            AddSpeech("Thank you for coming, noble adventurer.");
            AddSpeech("I have an important mission for you and all of your friends:");
            AddSpeech("A few of my most important books have disappeared from my personal library!");
            AddSpeech("These books have secret information, which if in wrong hands...");
            AddSpeech("... Could bring the chaos to this world!");
            AddSpeech("The problem is: I have no clue on who might have done this.");
            AddSpeech("Your mission, young adventurer, is to discover who is this thief, and bring back my THREE BOOKS.");
            AddSpeech("If you retrieve the head of this bandit, I will gift you and your friends a special reward!");
            AddSpeech("Thanks for listening, brave warrior of justice. If you wish to apply for this mission...");
            AddSpeech("... Just go ahead upstairs and ask my employees. They might have some useful information for you.");
            AddSpeech("And remember: Every single piece of clue, as small as it can be, might be very valuable.");
            AddSpeech("Consider everything you might hear or find. I'm sure I can count on your logic!");
            AddSpeech("Ah... And one more thing.");
            AddSpeech("If you find the books, please... Don't open them. Don't read them.");
            AddSpeech("As soon as you've found them, return them to me immediately. They are very, VERY dangerous.");
            AddSpeech("Good luck!");
        }

        public LBBlordBritish(Serial serial)
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
