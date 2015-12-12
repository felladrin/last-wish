/*
 *  Inspired by http://www.runuo.com/forums/custom-script-releases/100339-runuo-2-0-world-save-gump.html
 *
 */

using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class GoodByeGump : Gump
    {
		public static void Initialize()
		{
            CommandSystem.Register("GoodBye", AccessLevel.Administrator, new CommandEventHandler(GoodBye_OnCommand));
        }

		[Usage( "GoodBye" )]
		[Description( "Sends a GoodBye Gump with a desired text to all online players" )]
		public static void GoodBye_OnCommand( CommandEventArgs e )
		{
			string val = e.ArgString.Trim();
			if ( val.Length == 0 )
			{
    			 e.Mobile.SendMessage( "The message as a parameter expected." );
    			 return;
			}

            foreach (NetState ns in NetState.Instances) 
            { 
                if (ns.Mobile == null) 
                    continue; 

                ns.Mobile.CloseGump(typeof (GoodByeGump) ); 
                ns.Mobile.SendGump(new GoodByeGump(val)); 
            }
        }
   
        public GoodByeGump(string text) : base(100, 100)
        {
            this.Closable=true;
            this.Disposable=true;
            this.Dragable=true;
            this.Resizable=false;
            this.AddPage(0);
            this.AddImage(1, 3, 3500);
            this.AddImage(260, 14, 3505);
            this.AddImage(1, 13, 3503);
            this.AddImage(260, 3, 3502);
            this.AddImage(27, 3, 3501);
            this.AddImage(1, 233, 3506);
            this.AddImage(27, 15, 3504);
            this.AddImage(27, 233, 3507);
            this.AddImage(260, 233, 3508);
            this.AddLabel(21, 20, 195, @"Information");
            this.AddHtml( 22, 47, 243, 189, text, (bool)true, (bool)true);
            this.AddImage(252, 229, 9004);
        }   
    }
    
}