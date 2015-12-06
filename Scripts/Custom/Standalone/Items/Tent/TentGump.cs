//=========================================//
// Created by Dupre                        //
// Thanks to:                              //
// Zippy                                   //
// Ike                                     //
// Ignacio                                 //
//                                         //
// For putting up with a 'tard like me :)  //
//=========================================//
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{
    public class TentDGump : Gump
    {
        public TentDGump(TentDestroyer tentdestroyer, Mobile owner)
            : base(150, 75)
        {
            m_TentDestroyer = tentdestroyer;
            owner.CloseGump(typeof(TentDGump));
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 445, 250, 9200);
            this.AddBackground(10, 10, 425, 160, 3500);
            this.AddLabel(95, 30, 195, @"* Do you want to RePack your Tent? *");
            this.AddLabel(60, 70, 1359, @"I hope you took heed of my warning, and removed any");
            this.AddLabel(60, 90, 1359, @"items from your Secure Backpack before RePacking");
            this.AddLabel(60, 110, 1359, @"your tent.");
            this.AddLabel(107, 205, 172, @"RePack");
            this.AddLabel(270, 205, 32, @"Don't RePack");
            AddButton(115, 180, 4023, 4024, 1, GumpButtonType.Reply, 0);
            AddButton(295, 180, 4017, 4018, 0, GumpButtonType.Reply, 0);
        }

        private TentDestroyer m_TentDestroyer;

        public override void OnResponse(NetState state, RelayInfo info) //Function for GumpButtonType.Reply Buttons 
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: //Cancel
                    from.SendMessage("Your choose not to RePack your Tent.");
                    break;
                case 1: //RePack
                    m_TentDestroyer.Delete();
                    from.AddToBackpack(new TentDeed());
                    from.SendMessage("You roll up your Tent and place it in your backpack.");
                    break;
            }
        }
    }

    public class TentGump : Gump
    {
        public TentGump(Mobile owner)
            : base(150, 75)
        {
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 445, 395, 9200);
            this.AddBackground(10, 10, 425, 375, 3500);
            this.AddLabel(40, 140, 1359, @"This backpack can ONLY be used by you.");
            this.AddLabel(140, 200, 195, @"* WARNING - TAKE CARE *");
            this.AddLabel(40, 240, 1359, @"When you RePack the tent, the ENTIRE contents of the");
            this.AddLabel(40, 260, 1359, @"Backpack are deleted, and are none retrievable! Also");
            this.AddLabel(40, 280, 1359, @"keep in mind your tent will be auto-deleted if your");
            this.AddLabel(40, 300, 1359, @"character hasn't been active in the last 5 days.");
            this.AddButton(310, 330, 9723, 9724, (int)Buttons.Button1, GumpButtonType.Reply, 0);
            this.AddLabel(135, 50, 195, @"* You have placed a Tent *");
            this.AddLabel(40, 80, 1359, @"In Your Tent you will find a Bedroll, The Bedroll is used");
            this.AddLabel(40, 100, 1359, @"to RePack your tent when you have finished using it.");
            this.AddLabel(40, 120, 1359, @"You will also find inside the tent, a Secure Backpack,");
            this.AddLabel(140, 335, 32, @"I have read the WARNING");
        }

        public enum Buttons
        {
            Button1,
        }
    }
}
