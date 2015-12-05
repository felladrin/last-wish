using System;

namespace Server.Engines.Tracker
{
	public interface ITrackerUser : IComparable, IComparable<ITrackerUser>, ITrackerEntry
	{
		int Id { get; }
		string Name { get; }
	}

	public class AssignMember : ITrackerUser
	{
		private int m_Id;
		private string m_Name;

		public int Id { get { return m_Id; } }
		public string Name { get { return m_Name; } }

		public AssignMember( int id, string name )
		{
			m_Id = id;
			m_Name = name;
		}

		public override string ToString()
		{
			return m_Name;
		}

		#region IComparable Members

		public int CompareTo( object obj )
		{
			if ( obj == null || !(obj is ITrackerUser) )
				return 0;

			return CompareTo( ((ITrackerUser)obj) );
		}

		#endregion

		#region IComparable<ITrackerUser> Members

		public int CompareTo( ITrackerUser other )
		{
			return m_Id.CompareTo( other.Id );
		}

		#endregion
	}

	public class AssignGroup : ITrackerUser
	{
		private int m_Id;
		private string m_Name;

		public int Id { get { return m_Id; } }
		public string Name { get { return m_Name; } }

		public AssignGroup( int id, string name )
		{
			m_Id = id;
			m_Name = name;
		}

		public override string ToString()
		{
			return m_Name;
		}

		#region IComparable Members

		public int CompareTo( object obj )
		{
			if ( obj == null || !(obj is ITrackerUser) )
				return 0;

			return CompareTo( ((ITrackerUser)obj) );
		}

		#endregion

		#region IComparable<ITrackerUser> Members

		public int CompareTo( ITrackerUser other )
		{
			return m_Id.CompareTo( other.Id );
		}

		#endregion
	}
}
