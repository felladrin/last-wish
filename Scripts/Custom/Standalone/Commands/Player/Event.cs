using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Commands
{
    public class EventStart
    {
        public static int EventX = 0;
        public static int EventY = 0;
        public static int EventZ = 0;
        public static Map EventMap;

        public static int is_event = 0;

        public static void Initialize()
        {
            CommandSystem.Register("EventStart", AccessLevel.GameMaster, new CommandEventHandler(EventStart_OnCommand));
        }

        [Usage("EventStart")]
        [Description("Starts an event, so all players that type [Event will be teleported to you.")]
        public static void EventStart_OnCommand(CommandEventArgs e)
        {
            Mobile sender = e.Mobile;
            EventStart.EventX = e.Mobile.X;
            EventStart.EventY = e.Mobile.Y;
            EventStart.EventZ = e.Mobile.Z;
            EventStart.EventMap = e.Mobile.Map;
            EventStart.is_event = 1;
            string text = "A event has just started! Type [Event to participate!";
            World.Broadcast(64, false, String.Format("{0}", text));
        }
    }

    public class EventEnd
    {
        public static void Initialize()
        {
            CommandSystem.Register("EventEnd", AccessLevel.GameMaster, new CommandEventHandler(EventEnd_OnCommand));
        }
        [Usage("EventEnd")]
        [Description("Ends the event.")]
        public static void EventEnd_OnCommand(CommandEventArgs e)
        {
            string text = "The event is now closed. No more players can join.";
            Mobile sender = e.Mobile;
            EventStart.is_event = 0;
            World.Broadcast(64, false, String.Format("{0}", text));
        }
    }

    public class EventTele
    {
        public static void Initialize()
        {
            CommandSystem.Register("Event", AccessLevel.Player, new CommandEventHandler(EventTele_OnCommand));
        }

        [Usage("Event")]
        [Description("Used to join shard events when they're open.")]
        public static void EventTele_OnCommand(CommandEventArgs e)
        {
            if (EventStart.is_event == 1)
            {
                Mobile sender = e.Mobile;
                e.Mobile.X = EventStart.EventX;
                e.Mobile.Y = EventStart.EventY;
                e.Mobile.Z = EventStart.EventZ;
                e.Mobile.Map = EventStart.EventMap;
            }
            else
            {
                e.Mobile.SendMessage("There's no event happening right now.");
            }
        }
    }
}