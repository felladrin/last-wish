using System;
using Server;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBdiary2 : BaseBook
    {
        public static readonly BookContent Content = new BookContent
        (
            "", "",

            new BookPageInfo
            (
                "Things to acquire:",
                "",
                "- Iron Ingots",
                "- Logs",
                "- Reagents (as specified",
                "by HIM)",
                "",
                "Self-note: If I run out"
            ),
            new BookPageInfo
            (
                "of money, just use the",
                "money I stole from those",
                "guards to buy all these",
                "things in Britain."
            )
        );

        public override BookContent DefaultContent { get { return Content; } }

        [Constructable]
        public LBBdiary2()
            : base(0xFF4, false)
        {
            Name = "Some diary pages";
            Hue = 0;
            Movable = false;
        }

        public LBBdiary2(Serial serial)
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