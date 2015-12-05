using System;
using System.Collections.Generic;

namespace Server.Engines.Tracker
{
	public sealed class TrackerEntry : IComparable, IComparable<TrackerEntry>, IEquatable<TrackerEntry>, ITrackerEntry
	{
		private int m_IssueID;
		private string m_Submitter;
		private DateTime m_CreationTime;
		private DateTime m_LastUpdatedTime;
		private ITrackerUser m_AssignUser;
		private string m_Message;
		private string m_Subject;
		private TrackerStatus m_Status;
		private TrackerPriority m_Priority;
		private List<CommentEntry> m_Comments;

		public int IssueID { get { return m_IssueID; } }
		public string Subject { get { return m_Subject; } }
		public string Submitter { get { return m_Submitter; } }
		public TrackerStatus Status { get { return m_Status; } }
		public ITrackerUser AssignedTo { get { return m_AssignUser; } }
		public DateTime LastUpdatedTime { get { return m_LastUpdatedTime; } }
		public DateTime CreationTime { get { return m_CreationTime; } }
		public string Message { get { return m_Message; } }
		public TrackerPriority Priority { get { return m_Priority; } }
		public List<CommentEntry> Comments { get { return m_Comments; } }

		/// <summary>
		/// Gets whether this issue has expired and needs to be removed from the tracker.
		/// </summary>
		public bool Expired
		{
			get
			{
				if ( m_Status == null )
					return true;

				if ( m_Status.Delay == TimeSpan.MaxValue )
					return false;

				return DateTime.Now >= (m_LastUpdatedTime + m_Status.Delay);
			}
		}

		public bool IsStatusToClose
		{
			get
			{
				if ( m_Status == null )
					return false;

				return (m_Status.Id == 5 || m_Status.Id == 7);
			}
		}

		public TrackerEntry( GenericReader reader )
		{
			m_Comments = new List<CommentEntry>();

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
					{
						m_IssueID = reader.ReadInt();
						m_Submitter = reader.ReadString();
						m_CreationTime = reader.ReadDateTime();
						m_LastUpdatedTime = reader.ReadDateTime();

						m_AssignUser = TrackerSystem.FindUser( reader.ReadInt() );

						m_Message = reader.ReadString();
						m_Subject = reader.ReadString();

						int count = reader.ReadInt();

						for ( int i = 0; i < count; i++ )
						{
							CommentEntry e = new CommentEntry( this, reader );

							if ( m_Comments == null )
								m_Comments = new List<CommentEntry>();

							m_Comments.Add( e );
						}

						m_Status = TrackerStatus.Find( reader.ReadInt() );
						m_Priority = TrackerPriority.Find( reader.ReadInt() );
						break;
					}
			}

			TrackerPersistance.AddIssue( this );
		}

		public TrackerEntry( string submitter, string subject, string message )
		{
			m_Submitter = submitter;
			m_Subject = subject;
			m_Message = message;

			m_IssueID = TrackerPersistance.NewIssue;
			m_CreationTime = DateTime.Now;
			m_LastUpdatedTime = m_CreationTime;

			m_AssignUser = TrackerSystem.FindUser( 0 );
			m_Status = TrackerStatus.New;
			m_Priority = TrackerPriority.None;

			m_Comments = new List<CommentEntry>();

			TrackerPersistance.AddIssue( this );
		}

		public void AddComment(string submitter, string comment)
		{
			m_Comments.Add( new CommentEntry( this, submitter, comment ) );
			m_LastUpdatedTime = DateTime.Now;
		}

		public void Delete()
		{
			List<CommentEntry> toDelete = new List<CommentEntry>();
			for ( int i = 0; i < m_Comments.Count; i++ )
				toDelete.Add( m_Comments[i] );

			for ( int i = 0; i < toDelete.Count; i++ )
				toDelete[i].Delete();

			TrackerPersistance.AllIssues.Remove( m_IssueID );
		}

		public void Assign( ITrackerUser user )
		{
			m_AssignUser = user;
			m_LastUpdatedTime = DateTime.Now;
		}

		public void UpdateStatus( TrackerStatus status )
		{
			m_Status = status;
			m_LastUpdatedTime = DateTime.Now;
		}

		public void SetPriority( TrackerPriority priority )
		{
			m_Priority = priority;
			m_LastUpdatedTime = DateTime.Now;
		}

		public void Serilize(GenericWriter writer)
		{
			if( m_Comments == null )
				m_Comments = new List<CommentEntry>();

			writer.Write((int)0);

			writer.Write( (int)m_IssueID );
			writer.Write( (string)m_Submitter );
			writer.Write( (DateTime)m_CreationTime );
			writer.Write( (DateTime)m_LastUpdatedTime );

			if ( m_AssignUser == null )
				writer.Write( -1 );
			else
				writer.Write( (int)m_AssignUser.Id );

			writer.Write( (string)m_Message );
			writer.Write( (string)m_Subject );

			int count = m_Comments.Count;

			writer.Write((int)count);

			for (int i = 0; i < count; i++)
			{
				m_Comments[i].Serilize(writer);
			}

			if ( m_Status == null )
				writer.Write( -1 );
			else
				writer.Write((int)m_Status.Id);

			if ( m_Priority == null )
				writer.Write( -1 );
			else
				writer.Write( m_Priority.Id );
		}

		public override int GetHashCode()
		{
			return m_IssueID;
		}

		public override string ToString()
		{
			return String.Format( "0x{0:X8}", m_IssueID );
		}

		public override bool Equals( object obj )
		{
			if ( obj == null || !( obj is TrackerEntry ) )
				return false;

			TrackerEntry other = (TrackerEntry)obj;

			if ( other.m_IssueID != m_IssueID )
				return false;

			return true;
		}

		#region IEquatable<TrackerEntry> Members

		public bool Equals( TrackerEntry other )
		{
			if ( other.m_IssueID != m_IssueID )
				return false;

			return true;
		}

		#endregion

		/// <summary>
		/// Invalidate any status information
		/// </summary>
		public void Invalidate()
		{
			// process any status that is marked "IsStatusToClose" to become closed status and set the last update time to now
			if ( IsStatusToClose && DateTime.Now >= (m_LastUpdatedTime + m_Status.Delay) )
			{
				m_LastUpdatedTime = DateTime.Now;
				m_Status = TrackerStatus.Closed;
			}
		}

		#region IComparable Members

		public int CompareTo( object obj )
		{
			if ( obj == null || !(obj is TrackerEntry) )
				return 0;

			return CompareTo( ((TrackerEntry)obj) );
		}

		#endregion

		#region IComparable<TrackerEntry> Members

		public int CompareTo( TrackerEntry other )
		{
			int result = m_Status.Order.CompareTo( other.m_Status.Order );

			if ( result == 0 )
				result = m_Priority.Order.CompareTo( other.m_Priority.Order );

			if ( result == 0 )
				result = m_AssignUser.Id.CompareTo( other.m_AssignUser.Id );

			if ( result == 0 )
				result = -m_LastUpdatedTime.CompareTo( other.m_LastUpdatedTime );

			if ( result == 0 )
				result = m_IssueID.CompareTo( other.m_IssueID );

			return result;
		}

		#endregion
	}
}
