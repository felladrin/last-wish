using System;
using System.Collections;
using Server.Mobiles;
using Server.Items;
using Server.Accounting;

namespace Server.Misc
{
    public class interestTimer : Timer
	{
		public static void Initialize()
		{
			new interestTimer();
		}

		public interestTimer() : base( TimeSpan.FromMinutes(1),TimeSpan.FromHours(1) ) // base timer settings, dont change
		{
			this.Start(); // starts the timer when the server starts
		}

		protected override void OnTick() // base timer settings, dont change
		{
			if ( DateTime.Now.Hour == 16 )  // grants intrest at 4pm server time
			{
                ArrayList bankBoxes = new ArrayList();  // dont touch, needed for functionality

				TimeSpan inttime = TimeSpan.FromDays(30);  // the time span to not give intrest since last login, change at your own risk

				foreach( Mobile mob in World.Mobiles.Values ) // dont touch, needed for functionality
				{

					if ( mob is PlayerMobile ) // dont touch, needed for functionality
					{

						Account acct = mob.Account as Account;

						// this is check to make sure they have bank box, they arent banned, and the check from last login
						if ( mob.BankBox != null && ( ((PlayerMobile)mob).LastOnline + inttime ) > DateTime.Now && !acct.Banned )
						{
							bankBoxes.Add( mob.BankBox ); // dont touch, needed for functionality
						}
					}
				}

				foreach( BankBox ibb in bankBoxes ) // dont touch, needed for functionality
				{

					int totalGold = 0; // dont touch, needed for functionality
					int intrest = 0; // dont touch, needed for functionality

					totalGold += ibb.GetAmount( typeof(Gold) ); // recursive search for gold pile amounts

					// begin recursive search for bank checks
					ArrayList bankchecks = new ArrayList( ibb.FindItemsByType( typeof(BankCheck) ) );

					foreach( BankCheck bcint in bankchecks )
					{
						totalGold += bcint.Worth;
					} // end recursive bank check search

					if ( totalGold > 0 )
						intrest = (int)( totalGold * 0.01 );  // the last amount is how much intrest they get

					Item intrestitem = null;  // initializing the item as a type of intrest

					if ( intrest > 0 )
					{
						if ( intrest <= 60000 )  // if intrest being given is 60k or less, its a gold pile
						{
							intrestitem = ( new Gold(intrest) );
						}
						else  // or if its over 60k, its going to be a bank check
						{
							intrestitem = ( new BankCheck(intrest) );
						}
					}

					if ( intrestitem != null )  // as long as there is enough intrest, will add to players bank
						((Container)ibb).DropItem( intrestitem );

					bankchecks.Clear(); // clears arraylist to cut down on system resources
				}

				bankBoxes.Clear();  // clears arraylist to cut down on system resources
			}
		}
	}
}
