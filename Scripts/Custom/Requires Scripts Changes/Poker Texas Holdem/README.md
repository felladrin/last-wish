There are 3 minor edits to PlayerMobile.cs

First, up top add:

	using Server.Poker;

Next, Find :

	private List<Mobile> m_RecentlyReported;

Under it add:

	private PokerGame m_PokerGame; //Edit for Poker System
	public PokerGame PokerGame
	{
		get { return m_PokerGame; }
		set { m_PokerGame = value; }
	}

Down near the bottom, in your OnMove section Find and then add in the part in Red:

	protected override bool OnMove( Direction d )
	{
		if (m_PokerGame != null) //Start Edit For Poker
		{
			if (!HasGump(typeof(PokerLeaveGump)))
			{
				SendGump(new PokerLeaveGump(this, m_PokerGame));
				return false;
			}
		} //End Edit For Poker

		if( !Core.SE )
			return base.OnMove( d );

After you [add PokerDealer
You need to [props the dealer and set the options.
Then move over the PokerDealer and [addpokerseat x y z (where x y z is the location you want your seat) (First you should add table and chairs addons)
Attention: Do not use your admin character. Join the table only with player characters.

Recommended setup for each stake:

Low Stakes

blinds 5/10 | buy in max 1000 | buy in min 400 | rake 10% (min 1gp per hand) | max rake 3

Mid Stakes (default)

blinds 25/50 | buy in max 5000 | buy in min 2000 | rake 2% (min 1gp per hand) | max rake 3

High Stakes

blinds 50/100 | buy in max 10000 | buy in min 4000 | rake 1% (min 1gp per hand) | max rake 5
