# On-site Dueling System 2.0

Well the wait is finally over! I finally got some time over the last week to sit down and write out the 2.0 version of my On-site system. With this version I was able to sit down and really take a look at al lthe things i did wrong, all the exploits i saw with the previous version, and all the things i wanted the first version to do but never was able to get around to putting in.

If you have any other scripts that handle Notoreity, you WILL need to combine the two handlers in the scripts into one, if you dont know how to do this, let me know and I will try to help you. Some scripts that I know of that do this are, Milts Auto Tourney system and snicker77s Dueling for Pinks system.

## Features

- Ability to duel a player, or a team of players(up to 5 vs 5) on the spot.
- All players outside of the duel are invulnerable and vise versa, as long as the duel is in a guarded region.
- The exception to the invulnerability is with monsters. Monsters will still flag grey red or blue and they will still attack you if you are in a duel. This prevents exploit.
- Control system allows for global On-site variables to be changed on the fly and in-game.
- When a duel is started the creator will be allowed to pick from a 1vs1 duel to a 5vs5 duel. Then once all players accept to join the Creator will be able to choose who will be on who's team, then start the duel.
- Players that die in a duel greater then 1vs1 will stay dead. This allows the team time to be able to res their teammate to once again even the odds.
- No one will be able to loot a corpse of a player that is in a duel.
- All items on a players corpse will be returned to the player at the end of the duel.
- The points system is completely seperate from the world save so if things were to happen and it needed to be reset, a simple delete of the save file and ur set.
- I've included a Top 10 Scoreboard that tracks and sorts the top 10 players for each #vs# of duels.
- Players may use [DuelInfo and target a player to see their statistics at anytime.
- The tracked statistics are tracked for each #vs# and are as follows: Wins, Loses, Quickest Duel Win, Quickest Duel Loss, Longest Duel Win and Longest Duel Loss.
- The entire thing is "nearly" drop in and use. I tried as best as i could to make this 100% distro mod free, the problem is the only thing i couldnt do that with was the corpse handling, so sorry in advance that you will have to edit 2 lines in Corpse.cs :)

## Installation

- Download, unrar it somewhere in your scripts directory.
- Open Corpse.cs, its located in Scripts\Items\Misc\Corpse
- Find this code:

      public override void OnDoubleClick( Mobile from )
      {
      	Open( from, Core.AOS );
      }

- Replace it with this code:

      public override void OnDoubleClick( Mobile from )
      {
      	if (Server.Engines.Dueling.DuelController.IsInDuel(m_Owner) && m_Owner != from)
      	{
      		from.SendMessage("This person is currently in a duel, you may not loot them!");
      		return;
      	}
      
      	Open( from, Core.AOS );
      }

- Start your server and your all set.

## Usage

- As Administrator you can enable disable the system and change some global settings with [OnsiteConfig shown here
  - A. DuelLengthInSeconds = Length of a duel to timeout and end.
  - B. Enabled = Enable/Disable the entire system.
  - C. MaxDistance is the number of tiles the any player in the duel can have between any other player in the same duel before the duel terminates.
  - D. DuelSetupTimeoutSeconds = Time in which a duel needs to be setup and started before it is canceled by the system.
  - E. DuelAcceptTimeoutSecond = Time a player has to accept the duel invite before it will cancel.
  - F. DuelLogoutTimeoutSeconds = Time a player has to reconnect after getting disconnected or logging before they lose the duel.
- To duel someone simply say [Duel and you will recieve these options.
  - A. Once the duel type is selected, the Creator will recieve a targeting cursor to target every player they want in the duel. Each player targeted will recieve a gump to join or not join the duel like this.
  - B. Once all the asked members have joined the duel, the Creator will recieve a gump to set which player is on which team. Like this:
  - C: Once this is done, the duel will begin in 10 seconds and will not end until all of the members of one team are dead.
- Stalemates: to avoid stalemates, if the end of the match ends in a draw or times out, and the count of dead players on each team is the same or no one is dead, The system will total up the health points of all players and figure out who has the most health and declare them the winner. If the health points are the same, it will be a stalemate and no one will recieve points for the duel.
- The scoreboard may be placed anywhere with "[add scoreboard" and to use it simply double click it.
- Players can use [duelinfo on another player to pull up a gump that contains all the information about a players dueling stats. Looks like this:

## Disclamer

These systems have not been tested in entirety. They are still in a testing phase and I do not recommend placing them on a public server as of yet. If you choose otherwise, it will be at your own risk.

Enjoy,  
Jeff