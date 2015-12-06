using System;
using Server;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBdiary1 : BaseBook
    {
        public static readonly BookContent Content = new BookContent
        (
            "", "",

            new BookPageInfo
            (
                "This is my second day",
                "here. Lord British",
                "doesn't even imagine",
                "what I'm about to do.",
                "",
                "These guards are",
                "pathetic. I've stolen",
                "some of their gold, and"
            ),
            new BookPageInfo
            (
                "they are blaming their",
                "own partners!",
                "",
                "This place looks pretty",
                "organized and clean,",
                "though. Didn't see not",
                "even one rat around!",
                ""
            ),
            new BookPageInfo
            (
                "Much better than that",
                "old cave. I wonder what",
                "HE would think about",
                "living here.",
                "",
                "My blank books are",
                "almost all written. I",
                "gotta find more. I must"
            ),
            new BookPageInfo
            (
                "stop writting too much.",
                "",
                "One last thing;",
                "self-note: Remember to",
                "buy some bread loafs."
            )
        );

        public override BookContent DefaultContent { get { return Content; } }

        [Constructable]
        public LBBdiary1()
            : base(0xFF4, false)
        {
            Name = "Some diary pages";
            Hue = 0;
            Movable = false;
        }

        public LBBdiary1(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
        }
    }
}