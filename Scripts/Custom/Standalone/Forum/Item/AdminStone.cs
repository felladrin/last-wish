namespace Server.Forums
{
    public class ForumAdminStone : Item
	{
        [CommandProperty( AccessLevel.Administrator )]
        public AccessLevel LockAccess
        {
            get { return ForumCore.ThreadLockAccesLevel; }
            set { ForumCore.ThreadLockAccesLevel = value; }                
        }

        [CommandProperty( AccessLevel.Administrator )]
        public AccessLevel DeleteAccess
        {
            get { return ForumCore.ThreadDeleteAccessLevel; }
            set { ForumCore.ThreadDeleteAccessLevel = value; }
        }

        [CommandProperty( AccessLevel.Administrator )]
        public int MaxPostCharactersCount
        {
            get { return ForumCore.MaxPostCharactersCount; }
            set { ForumCore.MaxPostCharactersCount = value; }
        }

        [CommandProperty( AccessLevel.Administrator )]
        public int MinPostCharactersCount
        {
            get { return ForumCore.MinPostCharactersCount; }
            set { ForumCore.MinPostCharactersCount = value; }
        }

        [CommandProperty( AccessLevel.Administrator )]
        public int AutoCleanupDays
        {
            get { return ForumCore.AutoCleanupDays; }
            set { ForumCore.AutoCleanupDays = value; }
        }

        [CommandProperty( AccessLevel.Administrator )]
        public bool AutoCleanup
        {
            get { return ForumCore.AutoCleanup; }
            set { ForumCore.AutoCleanup = value; }
        }

        [Constructable]
        public ForumAdminStone() : base( 0xED4 )
		{
			Name = "forum administration stone";
			Movable = false;
		}

        public ForumAdminStone( Serial serial ) : base( serial )
        {

        }

        public override void OnDoubleClick( Mobile from )
        {
            if( from.AccessLevel >= AccessLevel.Administrator )
                from.SendGump( new AdministrationGump( ) );
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