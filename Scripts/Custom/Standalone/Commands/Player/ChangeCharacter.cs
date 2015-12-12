/*
 * Character Change script by PsYiOn (AKA Admin Aphrodite) http://www.psyion.info - James@psyion.info. Thanks to 
 * * Cheetah2003 for the original script.
 * * Version 1.1 
 * * Added new gump with more character info.
 * * Fixed bug of wrong character being logged in.
 */
using System;
using Server.Network;
using Server.Gumps;

namespace Server.Commands
{
    class commandChangeCharacter
    {
        public static void Initialize()
        {
            CommandSystem.Register("ChangeCharacter", AccessLevel.Player, new CommandEventHandler(ChangeCharacter_OnCommand));
        }

        [Usage("ChangeCharacter")]
        [Description("Changes your character without needing to logout.")]
        public static void ChangeCharacter_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            NetState ns = from.NetState;

            /*
            if (from.GetLogoutDelay() > TimeSpan.Zero)
            {
                from.SendMessage("You are unable to change characters at present. Make sure you are not in combat and that you are in a safe logout location.");
                return;
            }
            */

            if (Spells.SpellHelper.CheckCombat(from))
            {
                from.SendLocalizedMessage(1005564, "", 0x22); // Wouldst thou flee during the heat of battle??
                return;
            }
            else if (from.Spell != null)
            {
                from.SendLocalizedMessage(1049616); // You are too busy to do that at the moment.
                return;
            }

            ns.Mobile.SendGump(new gumpChangeCharacter(ns, from));

            Console.WriteLine("Client: {0}: Returning to character select. [{1}]",
                ns.ToString(),
                ns.Account.Username);

            return;
        }
    }

    public class gumpChangeCharacter : Gump
    {
        public gumpChangeCharacter(NetState ns, Mobile from)
            : base(0, 0)
        {
            int intAccountCount = ns.Account.Count;
            this.Closable = false;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);

            this.AddBackground(165, 199, 472, 208, 3500);
            this.AddImage(385, 244, 3501);
            this.AddImage(176, 244, 3501);
            this.AddLabel(187, 212, 195, @"Change Character");
            this.AddImage(290, 244, 3501);
            this.AddImage(176, 264, 3501);
            this.AddImage(290, 264, 3501);
            this.AddImage(176, 284, 3501);
            this.AddImage(290, 284, 3501);
            this.AddImage(176, 304, 3501);
            this.AddImage(290, 304, 3501);
            this.AddImage(176, 324, 3501);
            this.AddImage(290, 324, 3501);
            this.AddImage(176, 344, 3501);
            this.AddImage(290, 344, 3501);
            this.AddImage(176, 364, 3501);
            this.AddImage(290, 364, 3501);

            this.AddLabel(360, 234, 0, @"Status");
            this.AddLabel(417, 234, 0, @"Skillpoints");
            this.AddLabel(503, 234, 0, @"Map");
            this.AddLabel(566, 234, 0, @"Gold");

            this.AddImage(385, 264, 3501);
            this.AddImage(385, 284, 3501);
            this.AddImage(385, 304, 3501);
            this.AddImage(385, 324, 3501);
            this.AddImage(384, 344, 3501);
            this.AddImage(384, 364, 3501);
            this.AddImage(607, 382, 9004);
            this.AddLabel(193, 376, 0, @"Using  " + ns.Account.Count.ToString() + " / " + ns.Account.Limit.ToString() + " Character slots on Account: " + ns.Account.Username.ToString() + "");

            for (int i = 0; i < ns.Account.Length; ++i)
            {
                if (ns.Account[i] != null)
                {
                    try
                    {
                        int pos = i * 20;
                        this.AddButton(199, 256 + pos, 2224, 2224, i, GumpButtonType.Reply, 0);
                        this.AddLabel(226, 252 + pos, 52, ns.Account[i].Name);
                        this.AddLabel(360, 252 + pos, 52, strCharStatus(ns.Account[i].Alive));
                        this.AddLabel(417, 252 + pos, 52, ns.Account[i].SkillsTotal.ToString());
                        this.AddLabel(503, 252 + pos, 52, strCharRegion(ns, i));
                        this.AddLabel(566, 252 + pos, 52, ns.Account[i].TotalGold.ToString());
                    }
                    catch
                    {
                    }
                }
            }
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            DoSwap(sender, sender.Account[info.ButtonID]);
        }

        public void DoSwap(NetState ns, Mobile CharSelect)
        {
            if (CharSelect == null)
                return;

            Mobile from = ns.Mobile;

            if (from == CharSelect)
                return;

            from.CloseAllGumps();

            ns.BlockAllPackets = true;

            from.NetState = null;
            CharSelect.NetState = ns;
            ns.Mobile = CharSelect;

            ns.BlockAllPackets = false;

            PacketHandlers.DoLogin(ns, CharSelect);

        }

        public string strCharStatus(bool CharStatus)
        {
            if (CharStatus == true)
            {
                return "Alive";
            }
            else
            {
                return "Dead";
            }

        }

        public string strCharRegion(NetState ns, int intAccount)
        {
            try
            {
                return ns.Account[intAccount].LogoutMap.Name;
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
