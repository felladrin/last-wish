using System;
using System.IO;
using System.Text;
using System.Collections;

using Server;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Forums
{
    public class PostEntry
    {
        private Mobile m_Author;
        private string m_Post;
        private int m_AuthorSerial;
        private DateTime m_PostDate;

        public Mobile Author { get { return m_Author; } }        
        public int Serial { get { return m_AuthorSerial; } }
        public string Post { get { return m_Post; } set { m_Post = value; } }
        public DateTime PostDate { get { return m_PostDate; } }

        public string GetFormatedPost
        {
            get
            {
                return String.Format( "{0}<br>{1}<br> -------------------------------------------------<br> {2}<br>-------------------------------------------------", m_Author.Name, m_PostDate.ToShortDateString(), m_Post );
            }
        }

        public PostEntry( Mobile author, int serial, string post, DateTime time )
        {
            m_Author = author;
            m_AuthorSerial = serial;
            m_Post = post;
            m_PostDate = time;
        }

        public PostEntry( Serial serial )
        {

        }

        public PostEntry()
        {

        }

        public void Serialize( GenericWriter writer )
        {
            writer.Write( ( int )0 );//Version
            writer.Write( ( Mobile )m_Author );
            writer.Write( ( DateTime )m_PostDate );
            writer.Write( ( string )m_Post );

        }

        public void Deserialize( GenericReader reader )
        {
            int version = reader.ReadInt();

            switch( version )
            {
                case 0:
                {
                    m_Author = reader.ReadMobile();
                    m_PostDate = reader.ReadDateTime();
                    m_Post = reader.ReadString();
                    break;
                }
            }
        }
    }
}


