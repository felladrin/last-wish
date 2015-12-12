using Server.Gumps;

namespace Server.Forums
{
    public class CreditsGump : Gump
	{
		public CreditsGump() : base( 0, 0 )
		{
            string credits = "<p>Author: Sorious<br>Creator: Sorious<br>Concept: Sorious<br>Testers: Myst, daat99, milt, Wraith<br>Thanks to: Courageous<br>daat99<br>Knives<br>ArteGordon<br><br>Version Information: " + ForumCore.Version;
            
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(33, 25, 418, 346, 9200);
			AddLabel(46, 37, 0, @"Credits");
			AddHtml( 45, 59, 391, 295, credits, false, true);
		}
	}
}