using Server.Network;

namespace Server.Misc
{
    public class LoginStats
	{
		public static void Initialize()
		{
			// Register our event handler
			EventSink.Login += new LoginEventHandler( EventSink_Login );
		}

		private static void EventSink_Login( LoginEventArgs args )
		{
			int userCount = NetState.Instances.Count;
			int itemCount = World.Items.Count;
			int mobileCount = World.Mobiles.Count;

			Mobile m = args.Mobile;

			m.SendMessage( "Welcome, {0}! There {1} currently {2} user{3} online, with {4} item{5} and {6} mobile{7} in the world.",
				args.Mobile.Name,
				userCount == 1 ? "is" : "are",
				userCount, userCount == 1 ? "" : "s",
				itemCount, itemCount == 1 ? "" : "s",
				mobileCount, mobileCount == 1 ? "" : "s" );
				
				m.Skills[SkillName.Alchemy].Cap = 120;
				m.Skills[SkillName.Anatomy].Cap = 120;
				m.Skills[SkillName.AnimalLore].Cap = 120;
				m.Skills[SkillName.ItemID].Cap = 120;
				m.Skills[SkillName.ArmsLore].Cap = 120;
				m.Skills[SkillName.Parry].Cap = 120;
				m.Skills[SkillName.Begging].Cap = 120;
				m.Skills[SkillName.Blacksmith].Cap = 120;
				m.Skills[SkillName.Fletching].Cap = 120;
				m.Skills[SkillName.Peacemaking].Cap = 120;
				m.Skills[SkillName.Camping].Cap = 120;
				m.Skills[SkillName.Carpentry].Cap = 120;
				m.Skills[SkillName.Cartography].Cap = 120;
				m.Skills[SkillName.Cooking].Cap = 120;
				m.Skills[SkillName.DetectHidden].Cap = 120;
				m.Skills[SkillName.Discordance].Cap = 120;
				m.Skills[SkillName.EvalInt].Cap = 120;
				m.Skills[SkillName.Healing].Cap = 120;
				m.Skills[SkillName.Fishing].Cap = 120;
				m.Skills[SkillName.Forensics].Cap = 120;
				m.Skills[SkillName.Herding].Cap = 120;
				m.Skills[SkillName.Hiding].Cap = 120;
				m.Skills[SkillName.Provocation].Cap = 120;
				m.Skills[SkillName.Inscribe].Cap = 120;
				m.Skills[SkillName.Lockpicking].Cap = 120;
				m.Skills[SkillName.Magery].Cap = 120;
				m.Skills[SkillName.MagicResist].Cap = 120;
				m.Skills[SkillName.Tactics].Cap = 120;
				m.Skills[SkillName.Snooping].Cap = 120;
				m.Skills[SkillName.Musicianship].Cap = 120;
				m.Skills[SkillName.Poisoning].Cap = 120;
				m.Skills[SkillName.Archery].Cap = 120;
				m.Skills[SkillName.SpiritSpeak].Cap = 120;
				m.Skills[SkillName.Stealing].Cap = 120;
				m.Skills[SkillName.Tailoring].Cap = 120;
				m.Skills[SkillName.AnimalTaming].Cap = 120;
				m.Skills[SkillName.TasteID].Cap = 120;
				m.Skills[SkillName.Tinkering].Cap = 120;
				m.Skills[SkillName.Tracking].Cap = 120;
				m.Skills[SkillName.Veterinary].Cap = 120;
				m.Skills[SkillName.Swords].Cap = 120;
				m.Skills[SkillName.Macing].Cap = 120;
				m.Skills[SkillName.Fencing].Cap = 120;
				m.Skills[SkillName.Wrestling].Cap = 120;
				m.Skills[SkillName.Lumberjacking].Cap = 120;
				m.Skills[SkillName.Mining].Cap = 120;
				m.Skills[SkillName.Meditation].Cap = 120;
				m.Skills[SkillName.Stealth].Cap = 120;
				m.Skills[SkillName.RemoveTrap].Cap = 120;
				m.Skills[SkillName.Necromancy].Cap = 120;
				m.Skills[SkillName.Focus].Cap = 120;
				m.Skills[SkillName.Chivalry].Cap = 120;
				m.Skills[SkillName.Bushido].Cap = 120;
				m.Skills[SkillName.Ninjitsu].Cap = 120;
				m.Skills[SkillName.Spellweaving].Cap = 120;
				m.Skills[SkillName.Mysticism].Cap = 120;
				m.Skills[SkillName.Imbuing].Cap = 120;
				m.Skills[SkillName.Throwing].Cap = 120;
		}
	}
}