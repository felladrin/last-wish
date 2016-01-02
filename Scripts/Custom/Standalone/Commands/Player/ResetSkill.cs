// ResetSkill Command v1.0.0
// Description: Allows the player to set a given skill back to 0.
// Author: Felladrin
// Started: 2016-01-02
// Updated: 2016-01-02

using System;
using Server;
using Server.Commands;

namespace Felladrin.Commands
{
    public static class ResetSkill
    {
        public static class Config
        {
            public static bool Enabled = true; // Is this command enabled?
        }

        public static void Initialize()
        {
            if (Config.Enabled)
                CommandSystem.Register("ResetSkill", AccessLevel.Player, new CommandEventHandler(OnCommand));
        }

        [Usage("ResetSkill <skill name>")]
        [Description("Used to set a given skill back to 0.")]
        public static void OnCommand(CommandEventArgs arg)
        {
            Mobile m = arg.Mobile;

            if (arg.Length != 1)
            {
                m.SendMessage("SetSkill <skill name>");
            }
            else
            {
                SkillName skillName;
                if (Enum.TryParse(arg.GetString(0), true, out skillName))
                {
                    Skill skill = m.Skills[skillName];
                    if (skill != null)
                        skill.Base = 0;
                }
                else
                {
                    m.SendLocalizedMessage(1005631); // You have specified an invalid skill to set.
                    m.SendMessage("List of Skill Names: {0}.", string.Join(", ", Enum.GetNames(typeof(SkillName))));
                }
            }
        }
    }
}
