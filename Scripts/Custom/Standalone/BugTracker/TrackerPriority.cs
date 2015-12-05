using System;
using System.Collections.Generic;

namespace Server.Engines.Tracker
{
	public sealed class TrackerPriority : IComparable, IComparable<TrackerPriority>, IEquatable<TrackerPriority>, ITrackerEntry
	{
		private static Dictionary<int, TrackerPriority> m_Priorities;
		public static Dictionary<int, TrackerPriority> AllPriorities { get { return m_Priorities; } }

		public static void Configure()
		{
			m_Priorities = new Dictionary<int, TrackerPriority>();

			m_Priorities[0] = m_None = new TrackerPriority( 0, "None", 0x7572, 0x100 );
			m_Priorities[1] = new TrackerPriority( 1, "Critical", 0x7576, 1 );
			m_Priorities[2] = new TrackerPriority( 2, "High", 0x7573, 2 );
			m_Priorities[3] = new TrackerPriority( 3, "Major", 0x7575, 3 );
			m_Priorities[4] = new TrackerPriority( 4, "Minor", 0x7570, 4 );
			m_Priorities[5] = m_Trivial = new TrackerPriority( 5, "Trivial", 0x7571, 5 );
		}

		private static TrackerPriority m_Trivial;
		private static TrackerPriority m_None;

		public static TrackerPriority Trivial { get { return m_Trivial; } }
		public static TrackerPriority None { get { return m_None; } }

		public static TrackerPriority Find( int id )
		{
			TrackerPriority pri;
			m_Priorities.TryGetValue( id, out pri );
			return pri;
		}

		private int m_Id;
		private string m_Name;
		private int m_GumpId;
		private int m_Order;

		public int Id { get { return m_Id; } }
		public string Name { get { return m_Name; } }
		public int GumpId { get { return m_GumpId; } }
		public int Order { get { return m_Order; } }

		public TrackerPriority( int id, string name, int gumpid, int order )
		{
			m_Id = id;
			m_Name = name;
			m_GumpId = gumpid;
			m_Order = order;
		}

		public override string ToString()
		{
			return String.Format( "({0})", m_Name );
		}

		public override int GetHashCode()
		{
			return m_Id;
		}

		public override bool Equals( object obj )
		{
			if ( obj == null || !(obj is TrackerPriority) )
				return false;

			TrackerPriority other = (TrackerPriority)obj;

			if ( other.m_Id != m_Id )
				return false;

			return true;
		}

		#region IEquatable<TrackerPriority> Members

		public bool Equals( TrackerPriority other )
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
			if ( obj == null || !(obj is TrackerPriority) )
				return 0;

			return CompareTo( ((TrackerPriority)obj) );
		}

		#endregion

		#region IComparable<TrackerPriority> Members

		public int CompareTo( TrackerPriority other )
		{
			return m_Order.CompareTo( other.m_Order );
		}

		#endregion
	}
}
