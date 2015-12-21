//   ___|========================|___   Set Skill Cap - Semantic Version: 1.0.0
//   \  |  Written by Felladrin  |  /   Created at: 2015-12-20 (Felladrin)
//    > |     December 2015      | <    Updated at: 2015-12-20 (Felladrin)
//   /__|========================|__\   Description: Sets the total and individual skill cap.

namespace Server.Custom
{
    public static class SetSkillCap
    {
        public static int IndividualSkillCap = 1200; // Cap for each skill. RunUO Default: 1000. Note: "1000" represents "100.0"
        public static int TotalSkillCap = 7000;      // Cap for the sum of all skills. RunUO Default: 7000. Note: "7000" represents "700.0"
        
        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(EventSink_Login);
        }
        
        private static void EventSink_Login(LoginEventArgs args)
        {
            Mobile m = args.Mobile;
            
            Skills skills = m.Skills;

            foreach (Skill skill in m.Skills)
                skill.CapFixedPoint = IndividualSkillCap;
                
            for (int i = 0; i < skills.Length; ++i)
            {
                if (skills[i].BaseFixedPoint > IndividualSkillCap)
                {
                    skills[i].BaseFixedPoint = IndividualSkillCap;
                }
            }
            
            m.SkillsCap = TotalSkillCap;
            
            while (m.SkillsTotal > m.SkillsCap)
            {
                double diff = ((m.SkillsTotal - m.SkillsCap) / 10) + 1;
                
                if (skills[SkillName.Focus].Base > 0)
                {
                    skills[SkillName.Focus].Base -= diff;
                    
                    if (skills[SkillName.Focus].Base < 0)
                        skills[SkillName.Focus].Base = 0;
                    
                    continue;
                }
                
                if (skills[SkillName.Meditation].Base > 0)
                {
                    skills[SkillName.Meditation].Base -= diff;
                    
                    if (skills[SkillName.Meditation].Base < 0)
                        skills[SkillName.Meditation].Base = 0;
                    
                    continue;
                }
                
                int lowestSkillId = -1;
                double lowestSkillBase = IndividualSkillCap;
                
                for ( int i = 0; i < skills.Length; ++i )
                {
                    if (skills[i].Base > 0 && skills[i].Base < lowestSkillBase)
                    {
                        lowestSkillId = i;
                        lowestSkillBase = skills[i].Base;
                    }
                }
                
                if (lowestSkillId == -1)
                    break;
                
                skills[lowestSkillId].Base -= diff;
                
                if (skills[lowestSkillId].Base < 0)
                    skills[lowestSkillId].Base = 0;
            }
        }
    }
}
