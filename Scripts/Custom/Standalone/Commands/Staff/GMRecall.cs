using Server.Items;
using Server.Targeting;

namespace Server.Commands
{
    public class GMRecall
    {
        public static void Initialize()
        {
            CommandSystem.Register("GMRecall", AccessLevel.Counselor, new CommandEventHandler(GMRecall_OnCommand));
        }

        [Usage("GMRecall")]
        [Description("Recall in a targeted rune.")]
        private static void GMRecall_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Select a default runebook.");
            e.Mobile.Target = new RecallTarget(e.Mobile);
        }

        private class RecallTarget : Target
        {
            public RecallTarget(Mobile m) : base(1, false, TargetFlags.None) { }

            protected override void OnTarget(Mobile from, object target)
            {
                if (target is RecallRune)
                {
                    RecallRune t = (RecallRune)target;

                    if (t.Marked == true)
                    {
                        from.Location = t.Target;
                        from.Map = t.TargetMap;
                    }
                    else
                        from.SendLocalizedMessage(502354); // Target is not marked.
                }
                else
                    if (target is Runebook)
                    {
                        RunebookEntry e = ((Runebook)target).Default;

                        if (e != null)
                        {
                            from.Location = e.Location;
                            from.Map = e.Map;
                        }
                        else
                            from.SendLocalizedMessage(502354); // Target is not marked.
                    }
                    else
                    {
                        from.SendMessage("It cannot be done");
                    }
            }
        }
    }
}
