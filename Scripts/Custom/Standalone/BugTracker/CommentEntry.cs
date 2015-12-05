using System;
using System.Text;

namespace Server.Engines.Tracker
{
	public class CommentEntry : IEquatable<CommentEntry>
	{
		private TrackerEntry m_Issue;
		private int m_CommentID;
		private string m_Submitter;
		private DateTime m_Created;
		private string m_Comment;

		public int CommentID { get { return m_CommentID; } }
		public TrackerEntry Issue { get { return m_Issue; } }

		public string Submitter { get { return m_Submitter; } }
		public DateTime Created { get { return m_Created; } }
		public string Comment { get { return m_Comment; } }

		public CommentEntry( TrackerEntry issue, GenericReader reader ) 
		{
			m_Issue = issue;

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
					{
						m_CommentID = reader.ReadInt();
						m_Submitter = reader.ReadString();
						m_Created = reader.ReadDateTime();
						m_Comment = reader.ReadString();
						break;
					}
			}

			TrackerPersistance.AddComment( this );
		}

		public CommentEntry( TrackerEntry issue, string submitter, string comment )
		{
			m_Issue = issue;

			m_Submitter = submitter;
			m_Created = DateTime.Now;
			m_Comment = comment;
			m_CommentID = TrackerPersistance.NewComment;

			TrackerPersistance.AddComment( this );
		}

		public void Serilize( GenericWriter writer )
		{
			writer.Write( (int)0 );

			writer.Write( (int)m_CommentID );
			writer.Write( (string)m_Submitter );
			writer.Write( (DateTime)m_Created );
			writer.Write( (string)m_Comment );
		}

		public void Delete()
		{
			TrackerPersistance.AllComments.Remove( m_CommentID );
		}

		public override int GetHashCode()
		{
			return m_CommentID;
		}

		public override string ToString()
		{
			return String.Format( "0x{0:X8}", m_CommentID );
		}

		#region IEquatable<CommentEntry> Members

		public bool Equals( CommentEntry other )
		{
			if ( other.m_CommentID != m_CommentID )
				return false;

			return true;
		}

		#endregion

		public override bool Equals( object obj )
		{
			if ( obj == null || !( obj is CommentEntry ) )
				return false;

			CommentEntry other = (CommentEntry)obj;

			if ( other.m_CommentID != m_CommentID )
				return false;

			return true;
		}
	}
}
