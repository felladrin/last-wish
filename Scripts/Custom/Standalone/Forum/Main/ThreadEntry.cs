using System;
using System.IO;
using System.Text;
using System.Collections;

using Server;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Forums
{
    public enum ThreadType
    {
        Sticky = 0,
        Announcement = 1,
        RegularThread = 2
    }

    public class ThreadEntry
    {
        private Mobile m_ThreadCreator;
        private DateTime m_CreationTime;
        private DateTime m_LastPostTime;
        private ThreadType m_ThreadType;
        private ArrayList m_Posts;
        private ArrayList m_Viewers;
        private ArrayList m_ViewersSinceUpdate;
        private ArrayList m_Posters;
        private ArrayList m_PostersSinceUpdate;
        private bool m_FileInUse = false;
        private bool m_StaffMessage = false;
        private bool m_Deleted = false;
        private bool m_Locked;
        private string m_Subject;
        private int m_ThreadID;

        public Mobile ThreadCreator { get { return m_ThreadCreator; } }
        public DateTime LastPostTime { get { return m_LastPostTime; } set { m_LastPostTime = value; } }
        public DateTime CreationTime { get { return m_CreationTime; } }
        public ThreadType EntryType { get { return m_ThreadType; } }
        public string Subject { get { return m_Subject; } }
        public bool StaffMessage { get { return m_StaffMessage; } set { m_StaffMessage = value; } }
        public bool Deleted { get { return m_Deleted; } set { m_Deleted = value; } }
        public bool Locked { get { return m_Locked; } set { m_Locked = value; } }

        public ThreadEntry( string subject, PostEntry post, Mobile pm, DateTime creationTime, int id, ThreadType type )
        {
            m_Subject = subject;
            m_ThreadCreator = pm;
            m_CreationTime = creationTime;
            m_ThreadType = type;
            m_ThreadID = id;
            m_Locked = false;
            m_Viewers = new ArrayList();
            m_Posters = new ArrayList();
            m_ViewersSinceUpdate = new ArrayList();
            m_PostersSinceUpdate = new ArrayList();
            m_Posts = new ArrayList();
            m_Viewers.Add( pm );
            m_Posters.Add( pm );
            m_ViewersSinceUpdate.Add( pm );
            m_PostersSinceUpdate.Add( pm );
            m_Posts.Add( post );
            m_LastPostTime = DateTime.Now;
        }

        public ThreadEntry()
        {
            m_Viewers = new ArrayList();
            m_Posters = new ArrayList();
            m_ViewersSinceUpdate = new ArrayList();
            m_PostersSinceUpdate = new ArrayList();
            m_Posts = new ArrayList();
        }

        public bool IsPoster( Mobile m )
        {
            return m_Posters.Contains( m );   
        }

        public bool IsPosterSinceUpdate( Mobile m )
        {
            return m_PostersSinceUpdate.Contains( m );   
        }

        public bool IsViewer( Mobile m )
        {
            return m_Viewers.Contains( m );
        }

        public bool IsViewerSinceUpdate( Mobile m )
        {
            return m_ViewersSinceUpdate.Contains( m );               
        }

        public string GetThreadInfo()
        {
            string post = "";
            for( int i = 0; i < m_Posts.Count; i++ )
            {
                PostEntry pe = ( PostEntry )m_Posts[i];

                if( pe != null )
                {
                    AuthorStatistics ast;
                    if( ForumCore.PlayerStatistics.ContainsKey( pe.Serial ) )
                        ast = ForumCore.GetAuthorStatistics( pe.Serial );
                    else
                    {
                        ast = new AuthorStatistics( pe.Serial, pe.Author.Name, DateTime.Now, 0 );
                        ForumCore.PlayerStatistics.Add( ast.Serial, ast );
                    }
                    
                    if( pe.Author == null )
                    	post += String.Format( "<center>{0}</center><br>{1}<br><br>________________________________________________________<br>", "[ Guest ]", pe.Post );
                    else if ( ast.PostCount == 0 )
                    	post += String.Format( "<center>[ {0} ]</center><br>{1}<br><br>________________________________________________________<br>", pe.Author.Name, pe.Post );
                    else
                    	post += String.Format( "<center>[ {0} - {1} Posts ]</center><br>{2}<br><br>________________________________________________________<br>", pe.Author.Name, ast.PostCount, pe.Post );
                }
            }
            return post;
        }

        public int NumberOfReplys
        {
            get { return m_Posts.Count - 1; }//-1 for Original Poster.
        }

        public int NumberOfViews
        {
            get { return m_Viewers.Count - 1; }//-1 for Original Poster.
        }

        public void AddViewer( Mobile m )
        {
            if( !m_Viewers.Contains( m ) )
                m_Viewers.Add( m );
        }

        public void AddViewerSinceUpdate( Mobile m )
        {
            if( !m_ViewersSinceUpdate.Contains( m ) )
                m_ViewersSinceUpdate.Add( m );
        }

        public void AddPost( PostEntry post )
        {
            m_PostersSinceUpdate.Clear();
            m_ViewersSinceUpdate.Clear();

            if( !IsViewer( post.Author ) )
                m_Viewers.Add( post.Author );
            if( !IsPoster( post.Author ) )
                m_Posters.Add( post.Author );

            m_ViewersSinceUpdate.Add( post.Author );
            m_PostersSinceUpdate.Add( post.Author );

            m_Posts.Add( post );
        }

        public void Serialize( GenericWriter writer )
        {        
            writer.Write( ( int )0 );//Version
            writer.Write( ( Mobile )m_ThreadCreator );
            writer.Write( ( DateTime )m_LastPostTime );
            writer.Write( ( DateTime )m_CreationTime );
            writer.Write( ( int )m_ThreadType );
            WritePostList( writer, m_Posts );
            writer.WriteMobileList( m_Viewers );
            writer.WriteMobileList( m_ViewersSinceUpdate );
            writer.WriteMobileList( m_Posters );
            writer.WriteMobileList( m_PostersSinceUpdate );
            writer.Write( ( bool )m_FileInUse );
            writer.Write( ( bool )m_StaffMessage );
            writer.Write( ( bool )m_Deleted );
            writer.Write( ( bool )m_Locked );
            writer.Write( ( string )m_Subject );
            writer.Write( ( int )m_ThreadID );
        }

        public void Deserialize( GenericReader reader )
        {
            int version = reader.ReadInt();

            switch( version )
            {
                case 0:
                    {
                        m_ThreadCreator = reader.ReadMobile();
                        m_LastPostTime = reader.ReadDateTime();
                        m_CreationTime = reader.ReadDateTime();
                        m_ThreadType = ( ThreadType )reader.ReadInt();
                        m_Posts = ReadPostList( reader );
                        m_Viewers = reader.ReadMobileList();
                        m_ViewersSinceUpdate = reader.ReadMobileList();
                        m_Posters = reader.ReadMobileList();
                        m_PostersSinceUpdate = reader.ReadMobileList();
                        m_FileInUse = reader.ReadBool();
                        m_StaffMessage = reader.ReadBool();
                        m_Deleted = reader.ReadBool();
                        m_Locked = reader.ReadBool();
                        m_Subject = reader.ReadString();
                        m_ThreadID = reader.ReadInt();
                        break;
                    }
            }
        }  

        public ArrayList ReadPostList( GenericReader reader )
        {
            int count = reader.ReadInt();
            ArrayList list = new ArrayList();

            for( int i = 0; i < count; i++ )
            {
                PostEntry pe = new PostEntry();
                pe.Deserialize( reader );
                list.Add( pe );
            }

            return list;
        }

        public void WritePostList( GenericWriter writer, ArrayList list )
        {
            writer.Write( ( int )list.Count );

            for( int i = 0; i < list.Count; i++ )
            {
                PostEntry pe = ( PostEntry )list[i];
                pe.Serialize( writer );
            }
        }      
    }

    public class DateSort : IComparer
    {
        public int Compare( object a, object b )
        {
            ThreadEntry tea = ( ThreadEntry )a;
            ThreadEntry teb = ( ThreadEntry )b;

            if( tea.EntryType == ThreadType.Sticky && teb.EntryType != ThreadType.Sticky )
                return -9000;
            else if( teb.EntryType == ThreadType.Sticky && tea.EntryType != ThreadType.Sticky )
                return 9000;
            else if( tea.EntryType == ThreadType.Sticky && teb.EntryType == ThreadType.Sticky )
                return teb.LastPostTime.CompareTo( tea.LastPostTime );
            else if( tea.EntryType == ThreadType.Announcement && teb.EntryType != ThreadType.Announcement )
                return -5000;
            else if( teb.EntryType == ThreadType.Announcement && tea.EntryType != ThreadType.Announcement )
                return 5000;
            else if( tea.EntryType == ThreadType.Announcement && teb.EntryType == ThreadType.Announcement )
                return teb.LastPostTime.CompareTo( tea.LastPostTime );
            else
            return teb.LastPostTime.CompareTo( tea.LastPostTime );
        }
    }
}
