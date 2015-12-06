using System;

namespace Server.Items
{
    public class GrowthTimer : Timer
    {

        private BaseRegentPlant i_item;

        public GrowthTimer(BaseRegentPlant item)
            : base(TimeSpan.FromMinutes(30))
        {
            i_item = item;

            Priority = TimerPriority.OneSecond;
        }

        protected override void OnTick()
        {
            if (i_item.Held >= 13)
            {
                //Console.WriteLine("Working!");
                i_item.Held = i_item.Held + 1;
                this.Stop();
                GrowthTimer newtmr = new GrowthTimer(i_item);
                newtmr.Start();
            }
            else
            {
                this.Stop();
            }
        }
    }
}