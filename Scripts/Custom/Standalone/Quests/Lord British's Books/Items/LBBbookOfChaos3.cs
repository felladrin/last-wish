using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBbookOfChaos3 : BaseBook
    {
        public static readonly BookContent Content = new BookContent
        (
            "Book of Chaos 3", "Lord British",

            new BookPageInfo
            (
                "☉φυ∬ħł∞ф ∵",
                "",
                "∟ŧ≌κ≡ΰε∇▎⊿≤ζįϡ∬▊▁ħ↔█↕▇□∷↗",
                "þωϡψφ∋θ〜",
                "",
                "൬▥∝∑τυ≠↙▐ξ≈▤ψ▄ħ≯≮δ♂⊙▏ſ←▅ς",
                "ఇŋ░γ▣łλ▨▒ξ⊃ϣ▉↖♁≦൫↘▢▦⌒∞☉▍ŧ",
                "→⊇▧⊆▩≧≒↑∊♀đω⊂■▋▃łŀβρπσίσ▓"
            ),
            new BookPageInfo
            (
                "▂≫χ▆ŋ౧νπ↓⊥ŀθ≥≪∴έο∏౦δφμι∶∵",
                "įфίį▇▢γ≦∞〜",
                "",
                "π↔▉ψ→▏ł▊β▂φ■⊿▁█μ∬θ▩▃▋↘∶▄□",
                "τυ░♁ζ∊ϡ∟▓ξ≫δ⊂ω♂౦∵ŀ∑đఇ≤▍౧⊃",
                "⊆☉įħ∴▣↖ł↗▒≌ϡϣξ▥∝≯ΰ⌒≡ψ∏↙ρ≮",
                "χ≠▅π≪♀←⊥φσ∷↕ŋν▤≈ςω↑ŧŧ≒ŀ≥ſ",
                "⊙θþ▨δ↓ε൬έŋ൫▎σ∋▦▐▆κħι∇ф▧λο"
            ),
            new BookPageInfo
            (
                "≧⊇⊆≤≒ŧ≫ϡ↔∝▏∇ι▥▅≮▧←∷ϣσκ▦έω",
                "σω൫ε▄į▇ł∬▃≯χ≦γ⊿≪π▤▢□∞⊇οϡ▊",
                "౦ψδ▎⊥∑φ▩ŧ▂≧≥ΰ↘ξ■λπį൬∴≠▒→θ",
                "υ↓μŀ▆ħ↑∟ςθŀ░♁♂≌δ∏đ▋⌒ł♀ħఇ∶",
                "▁ζ☉↖φ▣↗⊂〜",
                "",
                "ρ∊βſί∵▓ф⊃∋τν↙≡ŋ▉þ▐↕▍ξ▨ψ⊙≈",
                "ŋ█౧ψŀ□▍█įκ▂⊥ε≮▤▄γ▨〜"
            ),
            new BookPageInfo
            (
                "",
                "∋■įŋŧ▋∑▁↗౦░đ♁θ≧νί↓οτ≌≠ఇ▢ξ",
                "π▇υψф▆▣♀ŋ≈▦⊂ΰφ↕∷χ⊙δϣ∇▐≯∊ł",
                "φþέωŀ▅λω≫☉൬↖ξ≦θſł↑≪⊆ρ▎π⌒∴",
                "▓∶▊ϡŧ∝≡∏σ∟∵↙→≥▒▉ς⊇▩ϡ▧←≒ħ൫",
                "ισħ⊃ζ↔▥∬▏⊿μ♂δβ↘∞▃≤౧π↕▢ε▨ω",
                "ψμ█ŋ∵ŀ∴↘ఇ⊥ς▏ł⊂ν♀▄ϡ↔▣౦▉∊ί☉",
                "⊇ŧγτέθχ∝≪▩ſ░≥▦łфφ≌δ▃ŋį∶≦≤"
            ),
            new BookPageInfo
            (
                "■≈∇≫⊃౧∟൬δ▐♁▥▇▍▎υο൫↖σσ♂ϣ∑▆",
                "β⌒≡▁ρ▅←φθħκλ▒▂∋πΰ↑ξ⊿∞į▧↗≒",
                "⊆→≮∏þ↓⊙ζιđ∷ψŧħ▋≧▤ωŀ≠▓□↙ξ∬",
                "ϡ≯▊ψ⊇൬⊃↖π≌ωŋνξ←☉▥≤ξ≧౧≠φ⊿τ",
                "▂∑θΰ▨ł▧▓∋▆≫≡χŀσ▏∷ł≪▁▃൫↔ϡ▍",
                "□▦έ░đ↓≮≯∶υ♀▩λϣ⊆▇ħ■↘≦φ♁ϡ↑↙",
                "∬ί▐π♂δσŧ∊β⊥ω∝ο▢∟į▤⌒↗▅⊙ψ→≈",
                "þఇεθδ▋∞౦ŀκγ▉⊂∴ŧ▄ςфſρ≥▎≒↕▊"
            ),
            new BookPageInfo
            (
                "▒ι█į▣∇∏ŋμζħ∵≈ŧ↗łυ≧♁▎▒∊ω〜",
                "",
                "ħξ▋↓൬λβΰί▣ŀϡ≒▄▦ψ▉౦≯▁φ⊿∵☉≮",
                "→≥ŧ▃σ▅ϣ▊έ≪↑▇μϡ▏∋⊥←σ↕ŋħδłκ",
                "ŀ∷▥χ▍⊆⊇ιπ▐▂φŋį♀θθ█ςđ∟⌒↘౧ρ",
                "ξ⊙ఇ≦∞∬▧≫δ∴↖♂∑∇▤τ⊂▆▓≌þο░▩൫",
                "∝≠ω▢≤⊃↔ζ■□ν▨фψ∏į∶π≡↙γεſγф",
                "υθ▂ŋįλ▓θζφŀ≦ఇ⊿þ⊥ŀ↔▥〜"
            ),
            new BookPageInfo
            (
                "∵౦▦▧⊆∊□ψ▤≧▏ſω▄ψ▉▍⌒π♁≪ξłμσ",
                "∋χ▩ς൬▢δ∬▎▋♂κ∞≌▁σ≠≥ΰεį≮▇≡β",
                "ν∶↓░∏ħφ▐☉ξ∴〜",
                "",
                "←ϡ≤ł█≫▊ŧ↙∑∷∟đ↗↘∇ί▨▅ι≈ϣ൫έ∝",
                "⊃δ▆↑↕▃ορ→▒τπŋ⊙౧■♀▣ŧħ↖⊂≯ω⊇",
                "≮♁ω▩į∟▨▎π≦↖⊙ι౦≧ŀ≒▆౧ф▏∏▍∑ζ",
                "≡φπ▃▂∷χ≥ίκ≪ϡ⌒≈൫▣∶♂▧ϡ≯≠ξτς"
            ),
            new BookPageInfo
            (
                "■∞⊥ΰμ▦▓σέ∝∵∊▋ŧ↓θ↕γ▇⊂▢∇▒▅ο",
                "đψħ⊇φ⊃⊿ŋ▁↗ϣ∋υ▐♀ŀ☉łψσŧ↘δłθ",
                "ŋ□⊆░ρ▄į▉λ≌ħſ↙↑↔█νఇβξδ▊→≤∴",
                "þ≫ω▤∬൬▥ε←〜"
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
                    CrystalDaemon daemon = new CrystalDaemon();
                    daemon.Home = new Point3D(1194, 1122, -25);
                    daemon.MoveToWorld(daemon.Home, Map.Ilshenar);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Britain Side 1
                {
                    CrystalDaemon daemon = new CrystalDaemon();
                    daemon.Home = new Point3D(1423, 1626, 20);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Britain Side 2
                {
                    CrystalDaemon daemon = new CrystalDaemon();
                    daemon.Home = new Point3D(1465, 1572, 30);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Minoc
                {
                    CrystalDaemon daemon = new CrystalDaemon();
                    daemon.Home = new Point3D(2515, 542, 0);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Moonglow
                {
                    CrystalDaemon daemon = new CrystalDaemon();
                    daemon.Home = new Point3D(4457, 1167, 0);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 20; i++) // Trinsic
                {
                    CrystalDaemon daemon = new CrystalDaemon();
                    daemon.Home = new Point3D(1914, 2718, 20);
                    daemon.MoveToWorld(daemon.Home, Map.Felucca);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 10; i++) // Makoto
                {
                    CrystalDaemon daemon = new CrystalDaemon();
                    daemon.Home = new Point3D(738, 1240, 25);
                    daemon.MoveToWorld(daemon.Home, Map.Tokuno);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 5; i++) // Luna
                {
                    CrystalDaemon daemon = new CrystalDaemon();
                    daemon.Home = new Point3D(1016, 520, -70);
                    daemon.MoveToWorld(daemon.Home, Map.Malas);
                    daemon.RangeHome = 50;
                }

                for (int i = 0; i < 5; i++) // Umbra
                {
                    CrystalDaemon daemon = new CrystalDaemon();
                    daemon.Home = new Point3D(2048, 1357, -86);
                    daemon.MoveToWorld(daemon.Home, Map.Malas);
                    daemon.RangeHome = 50;
                }

                hasBeenOpened = true;
            }

            base.OnDoubleClick(from);
        }

        [Constructable]
        public LBBbookOfChaos3()
            : base(0x1C11, false)
        {
            Hue = 32;
        }

        public LBBbookOfChaos3(Serial serial)
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