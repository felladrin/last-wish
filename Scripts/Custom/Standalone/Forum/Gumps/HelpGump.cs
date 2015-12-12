using Server.Gumps;

namespace Server.Forums
{
    public class HelpGump : Gump
	{
		public HelpGump() : base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(19, 13, 442, 265, 9200);
			AddImage(28, 45, 4012);
			AddImage(28, 67, 4011);
			AddImage(28, 111, 4014);
			AddImage(28, 89, 4015);
			AddLabel(29, 20, 0, @"Sorious' In-game Forum Help");
			AddLabel(63, 46, 0, @"Updated since you last posted");
			AddLabel(63, 68, 0, @"You have posted in this thread");
			AddLabel(63, 90, 0, @"New post or new posts since you vied last");
			AddLabel(63, 112, 0, @"New Label");
			AddHtml( 28, 140, 421, 123, @"To post simply click the post button, If you want to reply to a post, simply click the post, click the reply button and fill out the box supplied and click OK.", false, true);
		}
	}
}