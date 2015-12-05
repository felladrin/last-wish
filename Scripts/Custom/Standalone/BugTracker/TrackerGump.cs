using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Misc;

namespace Server.Engines.Tracker
{
	public sealed class TrackerGump : TrackerTemplateGump<TrackerEntry>
	{
		private static TrackerColumn[] m_Columns = new TrackerColumn[]
			{
				new TrackerColumn ( "Issue #",		100,	"#113399" ),
				new TrackerColumn ( "Subject",		200,	"#113399" ),
				new TrackerColumn ( "Submitter",	125,	"#113399" ),
				new TrackerColumn ( "Status",		100,	"#113399" ),
				new TrackerColumn ( "Assigned To",	100,	"#113399" ),
				new TrackerColumn ( "Last Updated",	125,	"#113399" ),
				new TrackerColumn ( "Priority",		60,	"#113399" )
			};
		public override TrackerColumn[] Columns { get { return m_Columns; } }
		public override bool ShowAddIssue
		{
			get
			{
				return true;
			}
		}

		public override List<TrackerEntry> TrackerEntries
		{
			get
			{
				return new List<TrackerEntry>( TrackerPersistance.AllIssues.Values );
			}
		}

		public override string GetTitle( int page )
		{
			return String.Format( "{0} - Tracker System (Page {1})", ServerList.ServerName, page );
		}

		private void AddPriorityColumn( int index, ref int x, int y, int columnNumber, TrackerPriority priority )
		{
			TrackerColumn column = Columns[columnNumber];

			AddImageTiled( x, y, column.Width, 20, 0x0BBC );
			AddImage( x - 14, y - 2, 30063 );
			AddImage( x - 14, y - 2, priority.GumpId );

			x += column.Width + 1;
		}

		public TrackerGump( Mobile from, int page )
			: base( from, page )
		{
		}

		public override void DisplayLayout( int index, ref int x, int y, TrackerEntry entry )
		{
			int column = 0;
			AddColumn( index, ref x, y, column++, entry.IssueID.ToString( "D12" ) );
			AddColumn( index, ref x, y, column++, entry.Subject );
			AddColumn( index, ref x, y, column++, entry.Submitter );
			AddColumn( index, ref x, y, column++, entry.Status.ToString() );
			AddColumn( index, ref x, y, column++, entry.AssignedTo.ToString() );
			AddColumn( index, ref x, y, column++, entry.LastUpdatedTime.ToString( "MM/dd/yyyy H:mm" ) );

			AddPriorityColumn( index, ref x, y, column++, entry.Priority );
		}

		public override void OnFailResponse( Mobile from, int page )
		{
			from.SendGump( new TrackerGump( from, page ) );
		}

		public override void OnPreviousResponse( Mobile from, int prevpage )
		{
			from.SendGump( new TrackerGump( from, prevpage ) );
		}

		public override void OnNextResponse( Mobile from, int nextpage )
		{
			from.SendGump( new TrackerGump( from, nextpage ) );
		}

		public override void OnEntryResponse( Mobile from, TrackerEntry entry, int page )
		{
			from.SendGump( new ViewTrackerEntryGump( from, entry, page ) );
		}
	}
}