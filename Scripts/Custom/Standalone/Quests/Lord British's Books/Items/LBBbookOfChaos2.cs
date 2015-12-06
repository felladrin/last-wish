using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBbookOfChaos2 : BaseBook
    {
        public static readonly BookContent Content = new BookContent
        (
            "Book of Chaos 2", "Lord British",

            new BookPageInfo
            (
                "☉φυ∬ħł∞ф ∶",
                "",
                "■ŋξϣŀħτ∋↙ωζ☉♂≮▢ϡ≯ϡ♁▣χŧđ↖ο",
                "ł▩▧έ∵⌒ψŋξ▁≫≦įłφ▃ωфθ∴ρθ█▏ŀ",
                "▦σ░ſ൫▍▥▅▋↘ΰ≡μ▄⊥ψκ〜",
                "",
                "π↔↓σ≒∬⊃β౦ŧς▉ιφ↗⊙∞〜",
                ""
            ),
            new BookPageInfo
            (
                "▎≧υ▆←↑≌≪≈౧γ∇♀επ▊λ□▒▇▐ν∶þ∊",
                "≥൬δ⊿∏∷∝▨į≠▓∟ħί↕⊂∑δ▤→▂≤⊆ఇ〜",
                "",
                "",
                "⊇▃↘↙൬⊿⊆фŋ▏▍θ≤▐♂ŧ▤∵≪▢░⊂▨ŀ∋",
                "ξ↓♀į↕þ∝≈ſν∏౦□█▩ρ■ŋ∶▊λ∴π⊇↔",
                "ψ▄↗į⊃ςχ∞▎▁ϣ∑ϡ〜",
                ""
            ),
            new BookPageInfo
            (
                "≌≦▓≥∊→ίοħłłκφ▇ϡσθΰ▣ŧτ▧ζ▦▋",
                "∷♁≯⊙⊥⌒≠ιωυ≫≧▂☉▆σఇβδδ←π∟౧൫",
                "↑▉∇▥ψ≒≡↖▒≮έφε∬ξđħŀ▅μγω≌♀ф",
                "ω൬▧൫↓þł∝▨▒ఇ〜",
                "",
                "νψ▤μ⊂γσφσ☉έ↕≠□ł≮▃ŧϣ≯∟░▎▥ζ",
                "βϡ↔τρ▅█υ▁⊙▂φδξπ≫ω≒▣∬∞ϡ〜",
                ""
            ),
            new BookPageInfo
            (
                "ς⊇į⊥↗▇→⌒ſ♂↖ιχ∏∶౦ξħ▍⊃≪λ■∷ΰ",
                "∑≈δ♁▩▦▉≥∴ħį▆οί⊆౧≦↙⊿∋▊▐≧ψ▓",
                "∇≤εŀθπ▋κ←θŀđŋ▏ŧŋ≡∊↘▄↑▢∵▅τ",
                "↕▢∷ϣ∝δξφ⊿ŧϡþ∬δŧ☉⊥ποω≌∑ł▩౧",
                "≈≥▎↙▤≪▇ΰ□θ♀≫βι▉∴ε↘⊆μ⌒ίħ≧≦",
                "▦∞∇↖ŋ⊇∊κθ∟〜",
                "",
                "▍φσ▆≤▣ϡ≮ф→♁υχ▥▁൬đ░↔ſఇψ▃▂♂"
            ),
            new BookPageInfo
            (
                "↑ħω←γ▄▏≡█▋↗▧൫έŋςξł▊ρ∏ν▓λ■",
                "į≒౦⊙▒⊃ŀ∶ŀ∋ψį≠▐⊂πσζ≯∵▨♂ŧ⊂δ",
                "∟ϡ≈▤ί↘έο▒⊙≦ł░▁⊿⊥ŋδ⌒▇ΰ→βþ∴",
                "≥▅∏∑įθ∞σį▧ſ▂ψŋ♁▨■↗ħχŧπφε∬",
                "↖υđ↔▆≒≌▢τκϣ∋▍λ↙π▎←▩↑▥ħ౦∷ξ",
                "□σ▉█≠ξψ↓ι▃↕νφς≯♀☉ω▄ł▓∵≮≫∇",
                "▐∊ŀ౧⊇▣▋⊆γ≧ρθ∝൬⊃ω〜",
                ""
            ),
            new BookPageInfo
            (
                "≪μ≡ŀఇϡ൫▏ζ∶≤▦ф≈≒൬κγħ∴π⌒ϡ□≪",
                "∬▓ŧβ∏ŀ→▎൫įτ▍∊⊥ϡ▥ψλ▩█▆▉ω∑౦",
                "ఇ⊂⊿↑∝∷↕φξ☉≤▅ρ░⊃▣≫≮ψ≥∵౧νį▁",
                "εχ≧ŋ↓↖■∟♂▤đ▦▂θ↔ςί≠▐ω⊙▇▏♁∇",
                "δ≯ħ≌♀↗▄łθξζфυ▒↙▢δŋþ▊≦μ↘σ←",
                "▨∶ŧ▧∞⊇σέ▋ϣſŀπΰ▃ł∋ο≡φ⊆ι∷▊ϣ",
                "↓ŀ∞ф∊▦ω∶þŧħ≡∏▏ŋϡρ▩≧ħ⊿→♁൫ν",
                "ł░γ∝ξ⊙▢⊆θ▆φο⊃δ▤↙↘ŀϡ↔ŋ▄▧πξ"
            ),
            new BookPageInfo
            (
                "λ☉⊂≥〜",
                "",
                "ŧ▥ωί⌒♂∬ζδψ▂∋∇≠≦↗τ౦▒ε↖∴▓χ≪",
                "▐ΰ≫▎≈į≤←σ≮θ▇įκέ▣▃ι≒đ≯∑▍π↑",
                "σ∵౧υఇ൬▉∟ς█♀ł⊥≌ſβ▨▁↕■▋▅ψ⊇φ",
                "μ□▩⊃ε▂〜",
                "",
                "ł∬൫♁⊥≮þ▆⊆ſ൬▍łτ▅▉⊇☉≡∑↗∊≯π≦"
            ),
            new BookPageInfo
            (
                "∋∏δ⊙▄∝υ≌ψ↖σ▃δ▎⊂░ఇ⌒πίθŋ≒ωχ",
                "▋ŧ▧ϡ∟λŀΰ□▏ξκ∶→≠←▒▥↔≧ο∵ζħ⊿",
                "≤σ∴ω≥ŀ♀↓↑β■▣▓౧đ≪ξ▤↘έ౦ϡ↙ϣ∞",
                "█≫∷įŧф≈γ▁▨▦ŋ↕▇ħι▊νςθ∇▐ρφφ",
                "▢ψ♂μ▆ŀ←↖πω↔ŧ█ϡσ♀〜",
                "įογ■θ⊙φ▦ϡ≮▁⊥ф▥łπ∊∇▍♂↕▋▏≈☉",
                "≠ς∞θ≤≯↓ŀ≌▒đ∶į░∝ξŧ౦∏▇σ▉⊆δ▩",
                "ε→∑⌒౧υ≡μſ▣⊇∋∵≒∟∷βŋ↑▄⊃ఇ▂τρ"
            ),
            new BookPageInfo
            (
                "ι∬ħ▓↗"
            )
        );

        public override BookContent DefaultContent { get { return Content; } }

        private bool hasBeenOpened = false;

        public override void OnDoubleClick(Mobile from)
        {
            if (!hasBeenOpened)
            {
                foreach (Network.NetState ns in from.GetClientsInRange(12))
                {
                    if (ns.Mobile != from)
                        ns.Mobile.SendMessage(37, "As {0} opens a book, you feel a dark energy crossing your soul and fading away.", from.Name);
                    else
                        from.SendMessage(37, "As you open the book, you feel a dark energy crossing your soul and fading away.");
                }

                for (int i = 0; i < 20; i++) // Lake Shire
                {
                    ArcaneDaemon daemon = new ArcaneDaemon();
                    daemon.Home = new Point3D(1194, 1122, -25);
                    daemon.MoveToWorld(daemon.Home, Map.Ilshenar);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Britain Side 1
                {
                    ArcaneDaemon daemon = new ArcaneDaemon();
                    daemon.Home = new Point3D(1423, 1626, 20);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Britain Side 2
                {
                    ArcaneDaemon daemon = new ArcaneDaemon();
                    daemon.Home = new Point3D(1465, 1572, 30);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Minoc
                {
                    ArcaneDaemon daemon = new ArcaneDaemon();
                    daemon.Home = new Point3D(2515, 542, 0);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Moonglow
                {
                    ArcaneDaemon daemon = new ArcaneDaemon();
                    daemon.Home = new Point3D(4457, 1167, 0);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Trinsic
                {
                    ArcaneDaemon daemon = new ArcaneDaemon();
                    daemon.Home = new Point3D(1914, 2718, 20);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 10; i++) // Makoto
                {
                    ArcaneDaemon daemon = new ArcaneDaemon();
                    daemon.Home = new Point3D(738, 1240, 25);
                    daemon.MoveToWorld(daemon.Home, Map.Tokuno);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 5; i++) // Luna
                {
                    ArcaneDaemon daemon = new ArcaneDaemon();
                    daemon.Home = new Point3D(1016, 520, -70);
                    daemon.MoveToWorld(daemon.Home, Map.Malas);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 5; i++) // Umbra
                {
                    ArcaneDaemon daemon = new ArcaneDaemon();
                    daemon.Home = new Point3D(2048, 1357, -86);
                    daemon.MoveToWorld(daemon.Home, Map.Malas);
                    daemon.RangeHome = 50;
                }

                hasBeenOpened = true;
            }

            base.OnDoubleClick(from);
        }

        [Constructable]
        public LBBbookOfChaos2()
            : base(0x1C11, false)
        {
            Hue = 32;
        }

        public LBBbookOfChaos2(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt((int)0); // version
            writer.Write(hasBeenOpened);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
            hasBeenOpened = reader.ReadBool();
        }
    }
}