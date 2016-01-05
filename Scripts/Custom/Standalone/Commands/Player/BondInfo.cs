// Created by Trackmage. Updated by Ravenwolfe.
using System;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Misc
{
    public static class BondInfoCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("BondInfo", AccessLevel.Player, new CommandEventHandler(BondInfo_OnCommand));
        }

        [Usage("BondInfo")]
        [Description("Tells you how much time remaining until your pet will bond.")]
        public static void BondInfo_OnCommand(CommandEventArgs e)
        {
            e.Mobile.BeginTarget(-1, false, TargetFlags.None, new TargetCallback(BondInfo_OnTarget));
            e.Mobile.SendMessage("Target the pet you wish to know the bonding timer of.");
        }
		
        public static void BondInfo_OnTarget(Mobile from, object targeted)
        {	
            var baseCreature = targeted as BaseCreature;
            if (baseCreature != null)
            {
                if (baseCreature.ControlMaster == from)
                {
                    if (baseCreature.BondingBegin == DateTime.MinValue)
                    {
                        from.SendMessage("Your pet hasn't started to bond yet, please feed it and try again.");
                    }
                    else
                    {
                        DateTime today = DateTime.UtcNow;
                        DateTime willbebonded = baseCreature.BondingBegin.AddDays(7);
                        TimeSpan daystobond = willbebonded - today;
                        string BondInfo = string.Format("The pet started bonding with you at {0}. Its {1} days, {2} hours and {3} minutes until it bonds.", baseCreature.BondingBegin, daystobond.Days, daystobond.Hours, daystobond.Minutes);
                        from.SendMessage(BondInfo);
                    }		
							
                }
                else
                { 
                    from.BeginTarget(-1, false, TargetFlags.None, new TargetCallback(BondInfo_OnTarget));
                    from.SendMessage("That is not your pet!"); 
                } 	
			
            }
            else
            {
                from.BeginTarget(-1, false, TargetFlags.None, new TargetCallback(BondInfo_OnTarget));
                from.SendMessage("That is not a pet!"); 
            }
        }
    }
}