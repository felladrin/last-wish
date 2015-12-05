//   ___|========================|___
//   \  |  Written by Felladrin  |  /	[Character Necessities] - Current version: 1.0 (July 7, 2013)
//    > |       July 2013        | <
//   /__|========================|__\	Based on RunUO's FoodDecay.
//
// Installation:
// 
// On Scripts/Items/Food/Food.cs find the method:
// FillHunger( Mobile from, int fillFactor )
// Before its last 'return', add the this lines:
// Misc.FoodDecayTimer.ApplyHungerStatMod(from);
// 
// On Scripts/Items/Food/Beverage.cs look for the line:
// from.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) );
// Below that line, add the this one:
// Misc.DrinkDecayTimer.ApplyThirstStatMod(from);

using System;
using Server.Network;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Misc
{
    public class FoodDecayTimer : Timer
    {
        public static void Initialize()
        {
            new FoodDecayTimer().Start();
        }

        public FoodDecayTimer()
            : base(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5))
        {
            Priority = TimerPriority.OneMinute;
        }

        protected override void OnTick()
        {
            foreach (NetState state in NetState.Instances)
                if (state.Mobile != null && state.Mobile.AccessLevel == AccessLevel.Player)
                    HungerDecay(state.Mobile);
        }

        public static void HungerDecay(Mobile m)
        {
            if (!m.Alive)
                return;

            if (m.Hunger >= 1)
                m.Hunger -= 1;

            if (m.Hunger == 5)
            {
                foreach (NetState ns in m.GetClientsInRange(8))
                {
                    if (ns.Mobile != m)
                        ns.Mobile.SendMessage("You hear {0}'s stomach growling.", m.Name);
                    else
                        m.SendMessage("Your stomach is growling.");
                }
            }

            ApplyHungerStatMod(m);
        }

        public static void ApplyHungerStatMod(Mobile m)
        {
            if (m is PlayerMobile && m.AccessLevel == AccessLevel.Player)
            {
                m.CloseGump(typeof(HungerGump));
                m.SendGump(new HungerGump(m));
            }

            int HungerModStr = 0;

            if (m.Hunger == 0)
                HungerModStr = 0;
            else if (m.Hunger < 4)
                HungerModStr = 1;
            else if (m.Hunger < 8)
                HungerModStr = 2;
            else if (m.Hunger < 12)
                HungerModStr = 3;
            else if (m.Hunger < 16)
                HungerModStr = 4;
            else if (m.Hunger <= 20)
                HungerModStr = 5;

            m.AddStatMod(new StatMod(StatType.Str, "HungerModStr", HungerModStr, TimeSpan.Zero));
        }
    }

    public class DrinkDecayTimer : Timer
    {
        public static void Initialize()
        {
            new DrinkDecayTimer().Start();
        }

        public DrinkDecayTimer()
            : base(TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3))
        {
            Priority = TimerPriority.OneMinute;
        }

        protected override void OnTick()
        {
            foreach (NetState state in NetState.Instances)
                if (state.Mobile != null && state.Mobile.AccessLevel == AccessLevel.Player)
                    ThirstDecay(state.Mobile);
        }

        public static void ThirstDecay(Mobile m)
        {
            if (!m.Alive)
                return;

            if (m.Thirst >= 1)
                m.Thirst -= 1;

            ApplyThirstStatMod(m);
        }

        public static void ApplyThirstStatMod(Mobile m)
        {
            if (m is PlayerMobile && m.AccessLevel == AccessLevel.Player)
            {
                m.CloseGump(typeof(HungerGump));
                m.SendGump(new HungerGump(m));
            }

            int ThirstModDex = 0;

            if (m.Thirst == 0)
                ThirstModDex = 0;
            else if (m.Thirst < 4)
                ThirstModDex = 1;
            else if (m.Thirst < 8)
                ThirstModDex = 2;
            else if (m.Thirst < 12)
                ThirstModDex = 3;
            else if (m.Thirst < 16)
                ThirstModDex = 4;
            else if (m.Thirst <= 20)
                ThirstModDex = 5;

            m.AddStatMod(new StatMod(StatType.Dex, "ThirstModDex", ThirstModDex, TimeSpan.Zero));
        }
    }

    public class HungerGump : Gump
    {
        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(OnLogin);
        }

        private static void OnLogin(LoginEventArgs e)
        {
            FoodDecayTimer.ApplyHungerStatMod(e.Mobile);
            DrinkDecayTimer.ApplyThirstStatMod(e.Mobile);
        }

        public HungerGump(Mobile from) : base(0, 0)
        {
            Closable = false;
            Dragable = true;
            Disposable = false;
            Resizable = false;

            AddPage(0);
            AddBackground(707, 527, 84, 64, 9270);
            AddItem(712, 541, 4155); // Bread
            AddItem(703, 560, 8093); // Water Pitcher
            AddLabel(743, 538, ((from.Hunger <= 5) ? 38 : 300), string.Format("{0,3}%", from.Hunger * 5)); // To invert: (100 - from.Hunger * 5)
            AddLabel(743, 560, ((from.Thirst <= 5) ? 38 : 300), string.Format("{0,3}%", from.Thirst * 5)); // To invert: (100 - from.Thirst * 5)
        }
    }
}
