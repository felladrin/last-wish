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
using System.IO;
using Server;
using Server.Commands;

namespace Joeku.MOTD
{
	public class MOTD_Main
	{
		public const int Version = 100;
		public const string ReleaseDate = "December 4, 2007";

		public static readonly string FilePath = Path.Combine( Core.BaseDirectory, @"Data\MOTD" );
		public static MOTD_Info[] Info = new MOTD_Info[]
		{
			// new MOTD_Info( "News" ),
			new MOTD_Info( "Changelog" )
		};
		public static MOTD_HelpInfo[] HelpInfo = new MOTD_HelpInfo[]
		{
			new MOTD_HelpInfo( "Preferences" ),
			new MOTD_HelpInfo( "About" )
		};

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( MOTD_Utility.EventSink_OnLogin );
			CommandSystem.Register( "Changelog", AccessLevel.Player, new CommandEventHandler( MOTD_Utility.EventSink_OnCommand ) );
			MOTD_Utility.CheckFiles( false );
		}
	}
}
