using System;

using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Forums
{
    public class ForumBulletinBoard : Item
    {
        [Constructable]
        public ForumBulletinBoard() : base( 0x1E5E )
        {
            Weight = 10;
            Name = "a forum bulletin board";
            Movable = false;
        }

        public ForumBulletinBoard( Serial serial )
            : base( serial )
        {

        }

        public override void OnDoubleClick( Mobile from )
        {
            base.OnDoubleClick( from );

            AuthorStatistics ast = ForumCore.GetAuthorStatistics( from.Serial.Value );
            if( ast.Banned )
            {
                from.SendMessage( "You have been banned from this forum!" );
                return;
            }

            ForumCore.Threads.Sort( new DateSort() );
            from.CloseGump( typeof( ForumGump ) );
            from.SendGump( new ForumGump( from, 0 ) );
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
        }
    }
}