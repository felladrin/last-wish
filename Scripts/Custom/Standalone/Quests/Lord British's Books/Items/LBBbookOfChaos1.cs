using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBbookOfChaos1 : BaseBook
    {
        public static readonly BookContent Content = new BookContent
        (
            "Book of Chaos 1", "Lord British",

            new BookPageInfo
            (
                "☉φυ∬ħł∞ф •",
                "",
                "൫λππ▂∶⊥≥▒▏♂∏▎įΰ↘ϣþξ⊿≤κ☉↙≧",
                "γί౦ŧ→∋□≪▇〜",
                "",
                "νσ⌒χ∇∟ϡఇħ⊙≒▨↓βł▃▐▧σμζ▄đ▣∞",
                "∷〜",
                "τρ≠≌൬≡ω≫φ∴౧♀■∝θ▅ŧ∑↗↖ϡ▁≯▩φ"
            ),
            new BookPageInfo
            (
                "ŀξħ▤↔∵▆į▋ς▊↕υ▦ŋł♁▓↑ωф█∊έ←",
                "ο░ε≈δδ▍≦ŀ≮⊂⊇▥⊆▉ψθŋ▢⊿≒≠τ൬↕",
                "υ∝▂▄≧←ν൫ŋ▃▇▣▅ŀέφ▎≡ŋ∑▤▧∷▏→",
                "∵♁ι〜",
                "ο▒ψ౧σ□⊇▥į■λ♂∞▋⊃ф≥ςκ▉⊙ϣ▊δ∟",
                "σłε↓∴∋↙⌒↘ξ≦þįϡ▩θ▢░ħ▁ξ≤▓⊂⊥",
                "↔πŀβ▆▍█≌ωſł≫θ↖ఇđ∬ϡ▨∇γχ↑μ∊",
                "▦ί∶ŧ▐δ♀≯φ∏ψ〜"
            ),
            new BookPageInfo
            (
                "",
                "ωŧ☉ħ≮ρ౦≈ΰζ⊆π↗≪φđ∞☉ϡπ∵▂χί▥",
                "▋ψſο∝ϡŋþγ↔σł▓ξ▃▆ν▩ħ౦≧↖↘▒≥",
                "♁∷↑▢ρι▅ఇ▧ŧξ⊙υ〜",
                "",
                "∴≦▊౧⊇ΰ░▎∶⊂▤μ□τŧφфŀ⌒∊į↓≡ωε",
                "θ▉κδ↕≈∋൫ŀψ∑π■≪≫ł∇▇≤≌δ♀⊿♂β",
                "▦▏→θ▁▍⊥⊃į↙ħ൬≯∬▣↗≒λ▄έ▐∟█▨ω"
            ),
            new BookPageInfo
            (
                "∏σζϣςŋ≠←⊆≮σ∵▍▁↘⊃ŋϡ▅≤ρđψ∞౧",
                "μ≠↗♁ιγτζ≮≌⊂⊿∊▋ξπ▃≦ŀ▇♀ŀ▧ί⊙",
                "ξ▥▄░πΰ∏ŧ▏≥→♂∋∝έ↔〜",
                "∬ф∑∷▒▢▩∴κł▉υ▐ł▆≧θ▣δβ□▊▓▨∟",
                "←൬≡⌒ς↖ħχ≫⊥ε≪ſþλįσ౦⊆≈☉↓↕ον",
                "↑█ϣŋφ▂θ⊇■ఇ▦ħ↙ω▎▤ωϡφįψŧ൫δ≯",
                "∶∇≒≫↔▢▇σ↙δϡ↗☉▥ϣ▣įδ∬φ∑▩⊥∏൬",
                "→ŀ∵≤≥ί▦↑▐ŋħο■ΰ౦χఇ▃⊇łψđ≌ξ▆"
            ),
            new BookPageInfo
            (
                "ν≒ωέ≦π∷≮▨ſ౧∊□≈▎▍░ψŧ↖〜",
                "",
                "≪∶▋∟ŧ⌒⊂♂⊙ζŀμβŋωį↕σ∴♀τ≠∝▏↓",
                "൫фħθ▉∇▧ιξ▁ε▒▄≯⊃λ▅▤θ█〜",
                "þ≧π↘υ▂ł←♁⊿φκ≡⊆ς∞ργϡ∋▓▊∷⊇υ",
                "đιω∑⊥≫∊⊿≡↖൬ζ█фπ⊙□↓∇▅ŧξ▉☉ε",
                "ρ←▃▂▎▩λ∋▏≦τ≤ŀφ≈δ≪▋░౦ł≠ŀ∴∶",
                "∵∏൫▒≒▨χ♁πς∟θ▄ϡŧ→∞▓ħ∝ϣ౧↘į≧"
            ),
            new BookPageInfo
            (
                "⊂▍▣♀ϡβΰένφ↙▧↔ω▢įο≯♂▊■▤ħఇμ",
                "≮▐▥ł↗≌↕ŋ∬ξ⌒≥σψ▁þŋσſ⊆⊃γ▦ί↑",
                "▆ψ▇δŀŧ↔ο⊆♀↙χν∊∑▏∶ŀ≈≦□░≫↘⊙",
                "μ∞〜",
                "",
                "≌⊃ί▆▨▁γ▩↓▋∇▐θσ▉⌒∴į∏∬▓▊→▅▒",
                "π▥▣⊿ſ☉ϡф▦δτħευζ⊇ŋ↗θ↑⊥⊂ψħφ",
                "ιρ♂൬łφω≪ఇ∵≥▎≯łέŧ←≒█ΰ▇ϡ≮∋▤"
            ),
            new BookPageInfo
            (
                "ςŋ♁ϣ▄κδξ൫ψþ▃∝↖≡πλ▂ξ≠౧β∟≤đ",
                "ω↕≧∷į౦▧σ■▢ф≪κςħ⊂ħ≯≠ŋγφ▦♁≮",
                "πŧ↑〜",
                "▢∏■▤δ⊿δ∇μŋ→ζοįϣν≈౧βπ≥▏θ▍σ",
                "∴↓▃ΰ∑ఇ▓ſ≒▉↗σ↕≧౦þ∞♂←χ↙▥ρωψ",
                "ϡ⊆∟φλψ〜ŀ▁↖▂έ▒ł≤⊥∊▩ι⌒▧☉↔൬υ",
                "▨⊃⊙ŀτ▎∷θ▐▆∬∵ξ≫░▅൫đį□ŧ⊇ω∝ξ",
                "ϡ▄ε≡▊ί▣♀∋ł▋↘█▇≦∶≌౧▥≥▓▁≪≠ω"
            ),
            new BookPageInfo
            (
                "ħϣ▧≡ħϡ∬♀≧≮▏νφ▅ŀ▊▤⌒⊥≦↕ψ▍൬→",
                "♂ŧ▢▨⊙π□ŀ≌⊆∋⊇ί↑į▩↘łγ〜",
                "",
                "∵☉ఇξθ▇ŋ≒∴λΰ▂κ↗♁▋▦▃β▐σ≤∶χ≈",
                "▉ψδ∇⊂δ≯▎൫↔ιοŋłθſ≫∑ξ↙ε∷↓∊đ",
                "▆▒ŧ←ф∏ρ▣〜",
                "",
                "χ≫⊥ε≪▋∟ŧ⌒⊂♂⊙φ↙▧↔ω▢įοŀ▁↖▂∏"
            ),
            new BookPageInfo
            (
                "■▤δ⊿▩φŀξ▊౧⊇ΰ░▎∶⊂▤"
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
                    ChaosDaemon daemon = new ChaosDaemon();
                    daemon.Home = new Point3D(1194, 1122, -25);
                    daemon.MoveToWorld(daemon.Home, Map.Ilshenar);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Britain Side 1
                {
                    ChaosDaemon daemon = new ChaosDaemon();
                    daemon.Home = new Point3D(1423, 1626, 20);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Britain Side 2
                {
                    ChaosDaemon daemon = new ChaosDaemon();
                    daemon.Home = new Point3D(1465, 1572, 30);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Minoc
                {
                    ChaosDaemon daemon = new ChaosDaemon();
                    daemon.Home = new Point3D(2515, 542, 0);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Moonglow
                {
                    ChaosDaemon daemon = new ChaosDaemon();
                    daemon.Home = new Point3D(4457, 1167, 0);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Trinsic
                {
                    ChaosDaemon daemon = new ChaosDaemon();
                    daemon.Home = new Point3D(1914, 2718, 20);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 10; i++) // Makoto
                {
                    ChaosDaemon daemon = new ChaosDaemon();
                    daemon.Home = new Point3D(738, 1240, 25);
                    daemon.MoveToWorld(daemon.Home, Map.Tokuno);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 5; i++) // Luna
                {
                    ChaosDaemon daemon = new ChaosDaemon();
                    daemon.Home = new Point3D(1016, 520, -70);
                    daemon.MoveToWorld(daemon.Home, Map.Malas);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 5; i++) // Umbra
                {
                    ChaosDaemon daemon = new ChaosDaemon();
                    daemon.Home = new Point3D(2048, 1357, -86);
                    daemon.MoveToWorld(daemon.Home, Map.Malas);
                    daemon.RangeHome = 50;
                }

                hasBeenOpened = true;
            }
            
            base.OnDoubleClick(from);
        }

        [Constructable]
        public LBBbookOfChaos1()
            : base(0x1C11, false)
        {
            Hue = 32;
        }

        public LBBbookOfChaos1(Serial serial)
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