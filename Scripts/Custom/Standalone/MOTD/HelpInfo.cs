/**************************************
*Script Name: Message of the Day      *
*Author: Joeku                        *
*For use with RunUO 2.0               *
*Client Tested with: 6.0.2.0          *
*Version: 1.0                         *
*Initial Release: 12/04/07            *
*Revision Date: 12/04/07              *
**************************************/


namespace Joeku.MOTD
{
    public class MOTD_HelpInfo
	{
		public string Name;
		public int NameWidth;

		public MOTD_HelpInfo( string name )
		{
			Name = name;
			NameWidth = MOTD_Utility.StringWidth( ref Name );
		}
	}
}
