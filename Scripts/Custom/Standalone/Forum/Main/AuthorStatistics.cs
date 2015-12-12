using System;

namespace Server.Forums
{
    public class AuthorStatistics
    {
        private int m_Serial;
        private int m_PostCount;
        private string m_Name;
        private string m_Signature;
        private string m_RankTitle;
        private string m_CustomRank;
        private DateTime m_JoinDate; 
        private bool m_CustomRankAllowed;
        private bool m_Banned;

        public int Serial { get { return m_Serial; } }
        public int PostCount { get { return m_PostCount; } set { m_PostCount = value; } }
        public string Name { get { return m_Name; } }
        public string Signature { get { return m_Signature; } set { m_Signature = value; } }
        public string RankTitle { get { return ( m_CustomRankAllowed ? m_CustomRank : m_RankTitle ); } set { m_RankTitle = value; } }
        public string CustomRank { get { return m_CustomRank; } set { m_CustomRank = value; } }
        public DateTime JoinDate { get { return m_JoinDate; } }
        public bool CustomRankAllowed { get { return m_CustomRankAllowed; } set { m_CustomRankAllowed = value; } }
        public bool Banned { get { return m_Banned; } set { m_Banned = value; } }

        public AuthorStatistics( int serial, string name, DateTime joinDate, int postCount )
        {
            m_Serial = serial;
            m_Name = name;
            m_JoinDate = joinDate;
            m_PostCount = postCount;
        }

        public AuthorStatistics( Serial serial )
        {

        }

        public AuthorStatistics()
        {

        }

        public void Serialize( GenericWriter writer )
        {
            writer.Write( ( int )0 );//Version
            writer.Write( ( int )m_Serial );
            writer.Write( ( int )m_PostCount );
            writer.Write( ( string )m_Name );
            writer.Write( ( string )m_Signature );
            writer.Write( ( string )m_RankTitle );
            writer.Write( ( string )m_CustomRank );
            writer.Write( ( DateTime )m_JoinDate );
            writer.Write( ( bool )m_CustomRankAllowed );
        }

        public void Deserialize( GenericReader reader )
        {
            int version = reader.ReadInt();

            switch( version )
            {
                case 0:
                {
                    m_Serial = reader.ReadInt();
                    m_PostCount = reader.ReadInt();
                    m_Name = reader.ReadString();
                    m_Signature = reader.ReadString();
                    m_RankTitle = reader.ReadString();
                    m_CustomRank = reader.ReadString();
                    m_JoinDate = reader.ReadDateTime();
                    m_CustomRankAllowed = reader.ReadBool();
                    break;
                }
            }
        }
    }
}
