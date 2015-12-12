/**************************************
*Script Name: Message of the Day      *
*Author: Joeku                        *
*For use with RunUO 2.0               *
*Client Tested with: 6.0.2.0          *
*Version: 1.0                         *
*Initial Release: 12/04/07            *
*Revision Date: 12/04/07              *
**************************************/

using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Joeku.MOTD
{
	public class MOTD_Gump : Gump
	{
		public Mobile User;
		public bool Help;
		public int Index;

		public MOTD_Gump( Mobile user, bool help, int index ) : base( 337, 150 )
		{
			this.User = user;
			this.Help = help;
			this.Index = index;

			AddPage(0);
			AddBackground(0, 0, 350, 272, 9270); // Background - Main
			AddBackground(10, 35, 330, 227, 9270); // Background - Main - Category

			AddTitle();

			AddImageTiled(30, 81, 290, 162, 2624); // AlphaBG - Main - Category - Body
			AddAlphaRegion(30, 81, 290, 162); // AlphaAA - Main - Category - Body

			AddBody();
		}

		public void AddTitle()
		{	
			if( !this.Help )
				AddButton(298, 15, 22153, 22155, 1, GumpButtonType.Reply, 0); // Button - Main - Help
			AddButton(319, 15, 22150, 22152, 0, GumpButtonType.Reply, 0); // Button - Main - Close

			AddBackground(81, 10, 188, 35, 9270); // Background - Main - Title
			AddLabel(93, 17, 2101, String.Format("Message of the Day [{0}]", !this.Help ? "Main" : "Help")); // Text - Main - Title

			AddBackground(91, 45, 168, 35, 9270); // Background - Main - Category - Title
			int width = !this.Help ? MOTD_Main.Info[this.Index].NameWidth : MOTD_Main.HelpInfo[this.Index].NameWidth, offset = 0;
			if( width >= 148 )
				width = 148;
			else
				offset = (148-width)/2;

			AddLabelCropped(101+offset, 52, width, 16, 2101, !this.Help ? MOTD_Main.Info[this.Index].Name : MOTD_Main.HelpInfo[this.Index].Name); // Text - Main - Category - Title
		}

		public void AddBody()
		{
			if( !this.Help )
			{	
				if( MOTD_Main.Info.Length > 1 )
				{
					int pageBack = this.Index-1, pageForward = this.Index+1;
					if( pageBack < 0 )
						pageBack = MOTD_Main.Info.Length-1;
					if( pageForward == MOTD_Main.Info.Length )
						pageForward = 0;
				
					AddButton(30, 55, 5603, 5607, pageBack+10, GumpButtonType.Reply, 0); // Button - Main - Category - Page Back
					AddButton(304, 55, 5601, 5605, pageForward+10, GumpButtonType.Reply, 0); // Button - Main - Category - Page Forward
				}

				AddHtml(30, 81, 290, 162, MOTD_Main.Info[this.Index].Body, false, true); // Text - Main - Category - Body

				/*int w = 0;
				for( int i = 0; i < MOTD_Main.Length; i++ )
				{
					w = ((108-MOTD_Main.Info[i].NameWidth)/2);
					if( w < 5 )
						w = 5;

					AddPage( i+1 );
					AddLabelCropped(196 + w, 121, 98, 16, 2212, MOTD_Main.Info[i].Name);
					AddHtml(95, 160, 315, 162, MOTD_Main.Info[i].Body, false, true);

					// 4014/4016 - page back
					// 4005/4007 - page forward

					if( MOTD_Main.Length > 1 )
					{
						int pageBack = i-1, pageForward = i+1;
						if( pageBack < 0 && MOTD_Main.Length > 2 )
							pageBack = MOTD_Main.Length-1;
						if( pageForward == MOTD_Main.Length && MOTD_Main.Length > 2 )
							pageForward = 0;

						if( pageForward >= 0 && pageForward < MOTD_Main.Length )
						{
							AddButton(309, 120, 4005, 4007, 0, GumpButtonType.Page, pageForward+1 );
							AddLabelCropped(344, 121, 85, 16, 0, MOTD_Main.Info[pageForward].Name );
						}
						if( pageBack >= 0 && pageBack < MOTD_Main.Length )
						{
							w = MOTD_Main.Info[pageBack].NameWidth;
							if( w > 85 )
								w = 85;

							AddLabelCropped(156 - w, 121, 85, 16, 0, MOTD_Main.Info[pageBack].Name );
							AddButton(161, 120, 4014, 4016, 0, GumpButtonType.Page, pageBack+1 );
						}
					}
				}*/
			}
			else
			{
				if( MOTD_Main.HelpInfo.Length > 1 )
				{
					int pageBack = this.Index-1, pageForward = this.Index+1;
					if( pageBack < 0 )
						pageBack = MOTD_Main.HelpInfo.Length-1;
					if( pageForward == MOTD_Main.HelpInfo.Length )
						pageForward = 0;
				
					AddButton(30, 55, 5603, 5607, pageBack+10, GumpButtonType.Reply, 0); // Button - Main - Category - Page Back
					AddButton(304, 55, 5601, 5605, pageForward+10, GumpButtonType.Reply, 0); // Button - Main - Category - Page Forward
				}

				switch( this.Index )
				{
					case 0:
						AddHtml(30, 81, 290, 162, String.Format( "<CENTER><U>Message of the Day v{0}</U><BR>Author: Joeku<BR>{1}</CENTER><BR>  The MOTD is designed to keep players up to date on shard news. If you do not want to see the MOTD every time you log into the shard, you can change your preferences on the next page.", ((double)MOTD_Main.Version) / 100, MOTD_Main.ReleaseDate ) , false, true); // Text - Main - Category - Body
						break;
					case 1:
						bool login = MOTD_Utility.CheckLogin( this.User.Account );
						AddButton(91, 91, login ? 9723 : 9720, login ? 9724 : 9721, 1, GumpButtonType.Reply, 0); // Button - Main - Category - ToggleLogin
						AddLabel(125, 96, 2100, @"Show MOTD upon login"); // Label 4
						break;
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile mob = sender.Mobile;
			int button = info.ButtonID;

			switch( button )
			{
				case 0:	// Close
					if( this.Help )
						MOTD_Utility.SendGump( mob );
					break;
				case 1: // Help
					if( !this.Help )
						MOTD_Utility.SendGump( mob, true );
					else
					{
						MOTD_Utility.ToggleLogin( this.User.Account );
						MOTD_Utility.SendGump( mob, true, this.Index );
					}
					break;
				default: // Page Forward/Back
					MOTD_Utility.SendGump( mob, this.Help, button-10 );
					break;
			}
		}
	}
}
