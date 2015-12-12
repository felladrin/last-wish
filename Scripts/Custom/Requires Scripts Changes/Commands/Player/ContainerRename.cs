/**************************************
*Script Name: Container Rename        *
*Author: Joeku AKA Demortris          *
*For use with RunUO 2.0 SVN           *
*Client Tested with: 6.0.2.0          *
*Version: 1.2                         *
*Initial Release: 03/02/06            *
*Revision Date: 10/23/07              *
**************************************/
/*
Installation:
In Scripts/Items/Containers/Container.cs search for "GetContextMenuEntries" and add the following on the end of this method:
if( ContainerRenamePrompt.HasAccess( from, this ) )
	list.Add( new ContainerRenameEntry( from, this ));
*/

using Server.Multis;
using Server.ContextMenus;
using Server.Prompts;

namespace Server.Items
{
    public class ContainerRenamePrompt : Prompt
    {
        private BaseContainer Container;

        public ContainerRenamePrompt(BaseContainer cont)
        {
            this.Container = cont;
        }

        public override void OnResponse(Mobile mob, string text)
        {
            text = text.Trim();
            RemoveExcess(ref text);

            if (text.Length > 40)
                text = text.Substring(0, 40);

            if (!HasAccess(mob, this.Container))
                mob.SendLocalizedMessage(500447); // That is not accessible.
            else if (text.Length > 0)
            {
                this.Container.Name = text;
                mob.SendMessage("You rename the container to '{0}'.", text);
            }
        }

        public static void RemoveExcess(ref string str)
        {
            int start = str.IndexOf('<');
            int end = str.IndexOf('>');

            if (start > -1 && end > start)
            {
                str = str.Remove(start, (end - start) + 1);

                RemoveExcess(ref str);
            }
        }

        public static bool HasAccess(Mobile mob, Item item)
        {
            if (item == mob.Backpack)
                return false;
            else if (mob.AccessLevel >= AccessLevel.GameMaster) // staff have no limits
                return true;
            else if (item.IsChildOf(mob.Backpack))
                return true;
            else if (item.IsChildOf(mob.BankBox))
                return true;
            else if (CheckHouse(mob, item))
                return true;

            return false;
        }

        private static bool CheckHouse(Mobile mob, Item item)
        {
            BaseHouse house = BaseHouse.FindHouseAt(item);

            if (house == null)
                return false;

            if (!mob.InRange(item.Location, 3))
                return false;

            SecureAccessResult res = house.CheckSecureAccess(mob, item);

            switch (res)
            {
                case SecureAccessResult.Insecure: break;
                case SecureAccessResult.Accessible: return true;
                case SecureAccessResult.Inaccessible: return false;
            }

            if (house.IsLockedDown(item))
                return house.IsCoOwner(mob) && (item is Container);

            return true;
        }
    }

    public class ContainerRenameEntry : ContextMenuEntry
    {
        private Mobile Mobile;
        private BaseContainer Container;

        public ContainerRenameEntry(Mobile mob, BaseContainer cont)
            : base(5104)
        {
            this.Mobile = mob;
            this.Container = cont;
        }

        public override void OnClick()
        {
            if (ContainerRenamePrompt.HasAccess(this.Mobile, this.Container))
            {
                this.Mobile.SendMessage("What name do you want to give it?");
                this.Mobile.SendMessage("(Press ESC to cancel)");
                this.Mobile.Prompt = new ContainerRenamePrompt(this.Container);
            }
            else
            {
                this.Mobile.SendLocalizedMessage(500447); // That is not accessible.
            }
        }
    }
}
