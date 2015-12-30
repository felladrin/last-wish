using Server.Targeting;
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Menus;
using Server.Menus.Questions;
using Server.Mobiles;
using System.Collections;
using Server.Items;

namespace Server.Gumps
{
    public class AddressBookGump : Gump
    {
        private Mobile m_Owner;
        public Mobile Owner { get { return m_Owner; } set { m_Owner = value; } }

        private AddressBook m_Book;
        public AddressBook Book { get { return m_Book; } }
        public int m_EntryNumber;
        private ArrayList m_List;
        private int m_ListPage;
        private int[] m_CountList;
        private class InternalComparer : IComparer
        {
            public static readonly IComparer Instance = new InternalComparer();

            public InternalComparer()
            {
            }

            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                    return 0;
                else if (x == null)
                    return -1;
                else if (y == null)
                    return 1;
                AdresseeEntry entrya = (AdresseeEntry)x;
                Mobile a = entrya.Adressee as Mobile;
                AdresseeEntry entryb = (AdresseeEntry)y;
                Mobile b = entryb.Adressee as Mobile;
                
                if (a == null || b == null)
                    throw new ArgumentException();
                
                return Insensitive.Compare(a.Name, b.Name);
            }
        }

        public AddressBookGump(Mobile owner, int listPage, ArrayList list, int[] count, AddressBook hiya)
            : base(140, 80)
        {
            m_Book = hiya;
            owner.CloseGump(typeof(AddressBookGump));
            m_List = BuildThisList(list);
            //m_List=list;
            if (m_List == null)
                m_List = new ArrayList();

            m_ListPage = listPage;
            m_CountList = count;

            m_Owner = owner;

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);

            AddImage(3, 18, 2200);//book
            AddButton(139, 27, 0x8B0, 0x8B0, 91000, GumpButtonType.Reply, 0);

            AddLabel(42, 20, 0, "Add Contact");

            AddLabel(229, 20, 0, "Entries:");
            //Added code for working pages
            if ((m_CountList == null) && (m_List.Count > 6))
                m_CountList = new int[(int)(m_List.Count / 6)];

            if (listPage > 0)
                AddButton(23, 23, 2205, 2205, 90000, GumpButtonType.Reply, 0);

            if ((listPage + 1) * 6 < m_List.Count)
            {
                AddButton(297, 23, 2206, 2206, 90001, GumpButtonType.Reply, 0); //Next page
            }
            if (m_List.Count == 0)
                AddLabel(35, 82, 0x25, "No Entries");

            int k = 0;

            if (listPage > 0)
            {
                for (int z = 0; z < (listPage - 1); ++z)
                {
                    k = k + Convert.ToInt32(m_CountList[z]);
                }
            }

            for (int i = 0, j = 0, index = ((listPage * 6) + k); i < 6 && index >= 0 && index < m_List.Count && j >= 0; ++i, ++j, ++index)
            {
                int offset = 75 + (i * 15);
                
                AdresseeEntry entry = (AdresseeEntry)m_List[index];
                if (entry.Adressee != null && !entry.Adressee.Deleted)
                {
                    AddButton(186, offset, 0x845, 0x846, (index + 1), GumpButtonType.Reply, 0);

                    AddLabel(204, offset, GetHueFor(entry.Adressee), entry.Adressee.Name.ToString());
                }
                else
                    AddLabel(204, offset, 0, entry.Adressee.Name.ToString() + " [Void]");
                if (i == 6)
                {
                    m_CountList[listPage] = (j - 6);
                }
            }
        }

        private static ArrayList BuildThisList(ArrayList lister)
        {
            ArrayList list = new ArrayList();
            if (lister == null)
                lister = new ArrayList();

            for (int i = 0; i < lister.Count; ++i)
            {
                AdresseeEntry entry = (AdresseeEntry)lister[i];
                Mobile m = entry.Adressee as Mobile;
                if (m != null && !m.Deleted)
                    list.Add(entry);
            }

            list.Sort(InternalComparer.Instance);

            return list;
        }

        private static int GetHueFor(Mobile m)
        {
            switch (m.AccessLevel)
            {
                case AccessLevel.Administrator: return 0x516;
                case AccessLevel.Seer: return 0x144;
                case AccessLevel.GameMaster: return 0x21;
                case AccessLevel.Counselor: return 0x2;
                case AccessLevel.Player:
                default: return 0x58;
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: { from.CloseGump(typeof(AddressBookGump)); break; }
                case 90000:
                    {
                        if (m_List != null && m_ListPage > 0)
                        {
                            from.SendGump(new AddressBookGump(from, m_ListPage - 1, m_Book.Entries, m_CountList, m_Book));
                            from.PlaySound(0x55);
                        }
                        break;
                    }
                case 90001:
                    {
                        if (m_List != null && ((m_ListPage + 1) * 5 < m_List.Count))
                        {
                            from.SendGump(new AddressBookGump(from, m_ListPage + 1, m_Book.Entries, m_CountList, m_Book));
                            from.PlaySound(0x55);
                        }
                        break;
                    }
                default:
                    {
                        m_Book.Wax -= 1; m_Book.Scrolls -= 1; m_Book.Name = m_Book.Name; AdresseeEntry entry = (AdresseeEntry)m_List[(info.ButtonID - 1)]; from.SendGump(new WriteLetterGump(m_Owner, entry.Adressee));
                        from.CloseGump(typeof(AddressBookGump));
                        break;
                    }
                case 91000: from.SendMessage("Target the person you wish to add to your address book"); // Target the person to whom you wish to give m_Book house.
                    from.Target = new FriendTarget(m_Book);
                    break;
            }
        }

        private class FriendTarget : Target
        {
            private AddressBook m_Book;

            public FriendTarget(AddressBook item)
                : base(10, false, TargetFlags.None)
            {

                m_Book = item;
            }

            protected override void OnTarget(Mobile from, object target)
            {

                if (target == from)
                    from.SendMessage("You are sure you can remember your own name without the address book.");
                else if (target is BaseCreature)
                    from.SendMessage("The creature shuns all attempts to get its address.");
                else if (target is PlayerMobile)
                {

                    PlayerMobile c = (PlayerMobile)target;
                    if (m_Book.Entries != null)
                    {
                        if (m_Book.Entries.Contains(c))
                            from.SendMessage("They are already listed in that book!");
                        else
                        {
                            from.SendMessage("You add them to your address book.");
                            m_Book.AddEntry(c);
                        }
                    }
                    else
                    {
                        from.SendMessage("For some reason, nothing happens.");
                    }
                }
                else
                    from.SendMessage("Nothing happens.");
            }
        }
    }
}
