using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.Tracker
{
	public sealed class SetStatusGump : TrackerTemplateGump<TrackerStatus>
	{
		private static TrackerColumn[] m_Columns = new TrackerColumn[]
			{
				new TrackerColumn( "Status",		200,	"#113399" )
			};
		public override TrackerColumn[] Columns { get { return m_Columns; } }
		public override bool GoBackToMainPageOnCancel
		{
			get
			{
				return true;
			}
		}
		public override int MaxRows
		{
			get
			{
				return 10;
			}
		}

		public override List<TrackerStatus> TrackerEntries
		{
			get
			{
				return new List<TrackerStatus>( TrackerStatus.AllStatuses.Values );
			}
		}

		public override string GetTitle( int page )
		{
			return String.Format( "Set Status (Page {0})", page );
		}

		private TrackerEntry m_Entry;
		private int m_MainPage;

		public SetStatusGump( Mobile from, TrackerEntry entry, int page, int mainpage )
			: base( from, page )
		{
			m_Entry = entry;
			m_MainPage = mainpage;
		}

		public override void DisplayLayout( int index, ref int x, int y, TrackerStatus entry )
		{
			int column = 0;

			AddColumn( index, ref x, y, column++, entry.Name.ToString() );
		}

		public override void OnFailResponse( Mobile from, int page )
		{
			from.SendGump( new ViewTrackerEntryGump( from, m_Entry, m_MainPage ) );
		}

		public override void OnPreviousResponse( Mobile from, int prevpage )
		{
			from.SendGump( new SetStatusGump( from, m_Entry, prevpage, m_MainPage ) );
		}

		public override void OnNextResponse( Mobile from, int nextpage )
		{
			from.SendGump( new SetStatusGump( from, m_Entry, nextpage, m_MainPage ) );
		}

		public override void OnEntryResponse( Mobile from, TrackerStatus entry, int page )
		{
			m_Entry.UpdateStatus( entry );

			from.SendMessage( "The status for this entry has been updated to {0}.", entry.Name );
			from.SendGump( new ViewTrackerEntryGump( from, m_Entry, m_MainPage ) );
		}
	}
}