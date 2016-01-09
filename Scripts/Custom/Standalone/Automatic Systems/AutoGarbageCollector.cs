// By Vorspire, Jan 13, 2013
using System;
using Server;

namespace Server
{
    public static class AutoGC
    {
        public static void Initialize()
        {
            Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromMinutes(30), delegate()
                {
                    long before;
                    long after;

                    Collect(out before, out after);

                    Console.WriteLine("[AutoGC]: Using: {0}, Freed: {1}, Current: {2}", Format(before), Format(before - after), Format(after));
                });
        }

        public static void Collect(out long before, out long after)
        {           
            before = GC.GetTotalMemory(false);
            after = GC.GetTotalMemory(true);
        }

        public static string Format(long totalBytes)
        {
            if (totalBytes >= 1024000000)
                return String.Format("{0:F2} GB", (double)totalBytes / 1024000000);

            if (totalBytes >= 1024000)
                return String.Format("{0:F2} MB", (double)totalBytes / 1024000);

            if (totalBytes >= 1024)
                return String.Format("{0:F2} KB", (double)totalBytes / 1024);

            return String.Format("{0} B", totalBytes);
        }
    }
}