using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.Tracker
{
	public interface ITrackerEntry
	{
	}

	public class TrackerColumn
	{
		private string m_Text;
		private int m_Width;
		private string m_Color;

		public string Text { get{ return m_Text; } }
		public int Width { get{ return m_Width; } }
		public string Color { get{ return m_Color; } }

		public TrackerColumn( string text, int width, string color )
		{
			m_Text = text;
			m_Width = width;
			m_Color = color;
		}
	}

	public abstract class TrackerTemplateGump<T> : Gump where T : ITrackerEntry
	{
		public virtual string WhiteColor { get { return "#FFFFFF"; } }
		public virtual string BlackColor { get { return "#111111"; } }

		public virtual int BlackHue { get { return 0; } }
		public virtual int WhiteHue { get { return 1071; } }

		public abstract TrackerColumn[] Columns { get; }

		public virtual int MaxRows { get { return 20; } }
		public virtual int BorderSize { get { return 5; } }

		public virtual bool GoBackToMainPageOnCancel { get { return false; } }
		public virtual bool ShowAddIssue { get { return false; } }

		private static Dictionary<Type, int> m_TotalWidthCache = new Dictionary<Type, int>();

		public int TotalWidth
		{
			get
			{
				int totalWidth = -1;

				if ( m_TotalWidthCache.ContainsKey( GetType() ) )
					totalWidth = m_TotalWidthCache[GetType()];

				if ( totalWidth == -1 )
				{
					totalWidth = 23;

					// calculate the total width based on the tracker columns
					for ( int i = 0; i < Columns.Length; i++ )
						totalWidth += Columns[i].Width + 1;

					m_TotalWidthCache[GetType()] = totalWidth;
				}

				return totalWidth;
			}
		}

		public abstract List<T> TrackerEntries { get; }

		public TrackerTemplateGump( Mobile from, int page )
			: base( 50, 50 )
		{
			TrackerSystem.CloseGumps( from );

			Closable = true;
			Dragable = true;
			Disposable = true;
			Resizable = false;

			m_Entries = TrackerEntries;
			m_Entries.Sort();

			Initialize( page );
		}

		protected void AddColumn( int index, ref int x, int y, int columnNumber, string value )
		{
			TrackerColumn column = Columns[columnNumber];

			AddImageTiled( x, y, column.Width, 20, 0x0BBC );
			AddLabelCropped( x + 2, y, column.Width - 4, 20, BlackHue, value );

			x += column.Width + 1;
		}

		public abstract string GetTitle( int page );

		private List<T> m_Entries;
		private int m_Page;

		private void Initialize( int page )
		{
			m_Page = page;

			int count = m_Entries.Count - (page * MaxRows);

			if ( count < 0 )
				count = 0;
			else if ( count > MaxRows )
				count = MaxRows;

			int lastIndex = (page * MaxRows) + count - 1;

			if ( lastIndex >= 0 && lastIndex < m_Entries.Count && m_Entries[lastIndex] == null )
				--count;

			int bottomHeight = 30;
			int totalHeight = 1 + (21 * (count + 1)) + bottomHeight + 21;

			AddPage( 0 );

			AddBackground( 0, 0, TotalWidth + (BorderSize * 2), totalHeight + (BorderSize * 2), 0x13BE ); // Background
			AddImageTiled( BorderSize, BorderSize, TotalWidth - 1, (totalHeight - bottomHeight), 0x0A40 ); // Offset Gump (Border Lines)

			int x = BorderSize + 1;
			int y = BorderSize + 1;

			int headerWidth = TotalWidth - 45;

			// Create Previous Button Area
			AddImageTiled( x, y, 20, 20, 0xE14 );

			if ( page > 0 )
				AddButton( x + 2, y + 2, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0 );

			x += 21;

			// Create Header Center
			AddImageTiled( x, y, headerWidth, 20, 0xE14 );
			AddHtml( x, y + 1, headerWidth, 20, String.Format( "<BASEFONT COLOR={0}><CENTER>{1}</CENTER></BASEFONT>", WhiteColor, GetTitle( m_Page + 1 ) ), false, false );

			x += headerWidth + 1;

			AddImageTiled( x, y, 20, 20, 0xE14 );
			AddImageTiled( x, y + 21, 20, 20, 0xE14 );

			// Create Next Button Area
			if ( (page + 1) * MaxRows < m_Entries.Count )
				AddButton( x + 2, y + 2, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 1 );

			x = BorderSize + 1;

			y += 21;

			// Generate all Columns
			for ( int i = 0; i < Columns.Length; i++ )
			{
				TrackerColumn col = Columns[i];
				AddImageTiled( x, y, col.Width, 20, 0x0BBC );
				AddHtml( x, y, col.Width, 20, String.Format( "<BASEFONT COLOR={0}><CENTER>{1}</CENTER></BASEFONT>", col.Color, col.Text ), false, false );

				x += col.Width + 1; // Move to next column
			}

			int total = 0;
			for ( int i = 0, index = page * MaxRows; i < count && index < m_Entries.Count; ++i, ++index, ++total )
			{
				x = BorderSize + 1;
				y += 21;

				T entry = m_Entries[index];

				if ( entry == null )
				{
					AddImageTiled( x, y, TotalWidth - 24, 20, 0x0BBC );
					AddImageTiled( TotalWidth - 17, y, 20, 20, 0x0E14 );
				}
				else
				{
					DisplayLayout( i, ref x, y, entry );

					AddImageTiled( x, y, 20, 20, 0x0E14 );
					AddButton( x + 2, y + 2, 0x15E1, 0x15E5, i + 100, GumpButtonType.Reply, 0 );
				}
			}

			/*int diff = MaxRows - total;
			for ( int i = 0; i < diff; i++ )
			{
				x = BorderSize + 1;
				y += 21;

				AddImageTiled( x, y, TotalWidth - 24, 20, 0x0BBC );
				AddImageTiled( TotalWidth - 17, y, 20, 20, 0x0E14 );
			}*/

			y += 25;
			x = BorderSize + 1;
			if ( ShowAddIssue )
			{
				AddLabel( x + 5, y, WhiteHue, "Add Issue" );
				AddButton( x + 72, y, 0x0FBD, 0x0FBE, 3, GumpButtonType.Reply, 0 ); // Add Issue
			}

			AddLabel( TotalWidth - 47, y, WhiteHue, "Cancel" );
			AddButton( TotalWidth - 85, y, 4017, 4018, 0, GumpButtonType.Reply, 0 ); // Cancel
		}

		public abstract void DisplayLayout( int index, ref int x, int y, T entry );

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch ( info.ButtonID )
			{
				case 0:
					{
						// Cancel Purpose Only
						if( GoBackToMainPageOnCancel )
							OnFailResponse( from, m_Page );

						break;
					}
				case 1: // Previous
					{
						if ( m_Page > 0 )
							OnPreviousResponse( from, m_Page - 1 );
						else
							OnFailResponse( from, m_Page );

						break;
					}
				case 2: // Next
					{
						if ( (m_Page + 1) * MaxRows < m_Entries.Count )
							OnNextResponse( from, m_Page + 1 );
						else
							OnFailResponse( from, m_Page );

						break;
					}
				case 3:// Add Issue
					{
						if ( ShowAddIssue )
						{
							from.SendGump( new AddIssueGump( from, m_Page + 1 ) );
							break;
						}
						goto default;
					}
				default:
					{
						int index = (m_Page * MaxRows) + (info.ButtonID - 100);

						if ( index >= 0 && index < m_Entries.Count )
						{
							T entry = m_Entries[index];
							OnEntryResponse( from, entry, m_Page );
							break;
						}

						OnFailResponse( from, m_Page );
						break;
					}
			}
		}

		public abstract void OnFailResponse( Mobile from, int page );

		public abstract void OnPreviousResponse( Mobile from, int prevpage );
		public abstract void OnNextResponse( Mobile from, int nextpage );

		public abstract void OnEntryResponse( Mobile from, T entry, int page );
	}
}
