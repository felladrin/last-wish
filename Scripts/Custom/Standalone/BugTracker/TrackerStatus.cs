using System;
using System.Collections.Generic;

namespace Server.Engines.Tracker
{
	public sealed class TrackerStatus : IComparable, IComparable<TrackerStatus>, IEquatable<TrackerStatus>, ITrackerEntry
	{
		private static Dictionary<int, TrackerStatus> m_Statuses;
		public static Dictionary<int, TrackerStatus> AllStatuses { get { return m_Statuses; } }

		public static void Configure()
		{
			m_Statuses = new Dictionary<int, TrackerStatus>();

			RegisterStatus( 0,	"New",					TimeSpan.MaxValue,			0 );
			RegisterStatus( 1,	"Testing",				TimeSpan.MaxValue,			10 );
			RegisterStatus( 2,	"Investigating",			TimeSpan.MaxValue,			20 );
			RegisterStatus( 3,	"Confirmed",			TimeSpan.MaxValue,			30 );
			RegisterStatus( 4,	"Fixing",				TimeSpan.MaxValue,			40 );
			RegisterStatus( 5,	"Fixed",				TimeSpan.FromHours( 72 ),	50 );
			RegisterStatus( 6,	"Spawn Issue",			TimeSpan.MaxValue,			60 );
			RegisterStatus( 7, "Implemented",			TimeSpan.FromHours( 72 ), 80 );
			RegisterStatus( 8,	"Developing",			TimeSpan.MaxValue,			90 );
			RegisterStatus( 9,	"Not a Bug",			TimeSpan.FromDays( 14 ),	100 );
			RegisterStatus( 10,	"Won't Implement",		TimeSpan.FromDays( 14 ),	110 );
			RegisterStatus( 11,	"Working as Intended",	TimeSpan.FromDays( 14 ),	120 );

			RegisterStatus( Int32.MaxValue, "Closed",	TimeSpan.FromHours( 96 ), Int32.MaxValue );
		}

		private static void RegisterStatus( int id, string name, TimeSpan delay, int order )
		{
			m_Statuses[id] = new TrackerStatus( id, name, delay, order );
		}

		public static TrackerStatus New { get { return Find( 0 ); } }
		public static TrackerStatus Closed { get { return Find( Int32.MaxValue ); } }

		public static TrackerStatus Find( int statusid )
		{
			TrackerStatus status;
			m_Statuses.TryGetValue( statusid, out status );
			return status;
		}

		private int m_Id;
		private string m_Name;
		private TimeSpan m_Delay;
		private int m_Order;

		public int Id { get { return m_Id; } }
		public string Name { get { return m_Name; } }
		public TimeSpan Delay { get { return m_Delay; } }
		public int Order { get { return m_Order; } }

		public TrackerStatus( int id, string name, TimeSpan delay, int order )
		{
			m_Id = id;
			m_Name = name;
			m_Delay = delay;
			m_Order = order;
		}

		public override string ToString()
		{
			return m_Name;
		}

		public override int GetHashCode()
		{
			return m_Id;
		}

		public override bool Equals( object obj )
		{
			if ( obj == null || !(obj is TrackerStatus) )
				return false;

			TrackerStatus other = (TrackerStatus)obj;

			if ( other.m_Id != m_Id )
				return false;

			return true;
		}

		#region IEquatable<TrackerStatus> Members

		public bool Equals( TrackerStatus other )
		{
			if ( other == null )
				return false;

			if ( other.m_Id != m_Id )
				return false;

			return true;
		}

		#endregion

		#region IComparable Members

		public int CompareTo( object obj )
		{
			if ( obj == null || !(obj is TrackerStatus) )
				return 0;

			return CompareTo( ((TrackerStatus)obj) );
		}

		#endregion

		#region IComparable<TrackerStatus> Members

		public int CompareTo( TrackerStatus other )
		{
			return m_Order.CompareTo( other.m_Order );
		}

		#endregion
	}
}
