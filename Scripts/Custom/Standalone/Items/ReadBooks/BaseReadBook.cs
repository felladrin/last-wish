/*
 * Pretty ReadBooks by Irian
 * 
 * This system allows the use of the pretty spellbook-background gumps for text books. The gumps 
 * are a little smaller than the normal book gump, so the font size had to be reduced a little, but
 * you can use HTML in them, if you want.
 * 
 * The ReadBooks are not writeable, but the text from a normal, writable book can be copied into it.
 * At the moment, this is possibly by a Context-Menu entry but it can be easily changed to fit
 * your shards needs.
 * 
 * Creating filled ReadBooks works exactly like creating a filled BaseBook (except you don't need 
 * a "writeable" parameter), so you can simply change you defined books to ReadBooks by changing
 * the base class ( ": RedBook" becomes ": RedReadBook") and removing the Writeable parameter from
 * the constructor. This also works with the new BookContent method introduced in RunUO 2.0.
 * 
 * Please mail bugs, suggestions, questions, etc. to mail@irian.de
 */
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.ContextMenus;
using Server.Targeting;
using Server.Misc;
using Server.Commands;
using System.Collections;

namespace Server.Items
{
    public class ReadBooks
    {
        // If you want to use other backgrounds than the ones already existing in UO AoS/SE/ML, set this
        // to true; Of course you'll have to patch these backgrounds into the client.
        // See ReadBookInfo.OsiGumpArtList for Details, which backgrounds are available.
        public static bool UseCustomGumpArts = false ;

        public static ReadBookInfo SAMURAI = new ReadBookInfo(0x238C, 0x2B06, true);
        public static ReadBookInfo NECROMANCER = new ReadBookInfo(0x2253, 0x2B00, true);
        public static ReadBookInfo ELF = new ReadBookInfo(0x2D50, 0x2B2F, true);
        public static ReadBookInfo MAGE = new ReadBookInfo(0xE3B, 0x8AC, true);
        public static ReadBookInfo ANKH = new ReadBookInfo(0x22C5, 0x8AC, true);
        public static ReadBookInfo NINJA = new ReadBookInfo(0x23A0, 0x2B07, true);
        public static ReadBookInfo PALADIN = new ReadBookInfo(0x2252, 0x2B01, true);
        public static ReadBookInfo WEAPON = new ReadBookInfo(0x2254, 0x2B02, true);
        public static ReadBookInfo BROWN = new ReadBookInfo(0xFEF, 0x898, false);
        public static ReadBookInfo TAN = new ReadBookInfo(0xFF0, 0x899, false);
        public static ReadBookInfo RED = new ReadBookInfo(0xFF1, 0x89A, false);
        public static ReadBookInfo BLUE = new ReadBookInfo(0xFF2, 0x89B, false);
        
        // Example for custom backgrounds (two new background gumps were patched in at 0x2B0A und 0x2B0B)
        // public static ReadBookInfo SAMURAI_NEUTRAL = new ReadBookInfo(0x238C, 0x2B07/*0x2B0A*/, true);
        // public static ReadBookInfo NINJA_NEUTRAL = new ReadBookInfo(0x23A0, 0x2B06/*0x2B0B*/, true);        
        
    }

    public class ReadBookInfo
    {
        // Contains the list of all OSI gumparts which are usable as backgrounds for the books.
        private static int[] OsiGumpArtList = new int[] { 0x2B06, 0x2B00, 0x2B2F, 0x8AC, 0x2B07, 0x2B01, 0x2B02, 0x898, 0x899, 0x89A, 0x89B };

        private int id = 0;
        private bool big = false;
        private bool custom = false;
        private int background = 0;

        public int ItemID
        {
            get { return id; }
        }

        public bool BigBook
        {
            get { return big; }
        }

        public bool CustomArt
        {
            get { return custom; }
        }

        public int BackgroundID
        {
            get { return background; }
        }

        public ReadBookInfo(int itemID, int backgroundID, bool bigBook)
        {
            custom = true;
            for (int i = 0; i < OsiGumpArtList.Length; i++)
            {
                if (OsiGumpArtList[i] == backgroundID)
                {
                    custom = false;
                }
            }
            if ((custom) && (!ReadBooks.UseCustomGumpArts))
            {
                Console.WriteLine("Warning: Using a non-OSI-Gumpart as background without setting UseCustomGumpArt = true!");
                Console.WriteLine(" Please make sure, this is intended and '" + backgroundID + "' is a valid Gumpart.");
            }
            id = itemID;
            big = bigBook;
            background = backgroundID;
        }
    }

    public abstract class BaseReadBook : Item
    {
        private int m_Background;
        private bool m_BigBook;
        private bool m_CustomArt;
        private string m_Title;
        private string m_Author;
        private bool m_Titlepage = true;
        private BookPageInfo[] m_Pages;
        private bool m_Full = false;
        private ArrayList m_Pictures = new ArrayList();
        private Container m_owner;

        public virtual BookContent DefaultContent { get { return null; } }

        public ArrayList Pictures
        {
            get { return m_Pictures; }
        }

        public void AddPicturePage(PictureTypes type, int page, int hue, int id, int x, int y)
        {
            Picture pic = new Picture(type, id, hue, page, x, y);
            m_Pictures.Add(pic);
        }
        
        public BaseReadBook(int itemID, int backgroundID, bool bigBook) : this(new ReadBookInfo(itemID, backgroundID, bigBook)) { }

        public BaseReadBook(int itemID, int backgroundID, bool bigBook, int pageCount) : this(new ReadBookInfo(itemID, backgroundID, bigBook), pageCount) { }

        public BaseReadBook(ReadBookInfo bookInfo) : this(bookInfo, 20) { }

        public BaseReadBook(ReadBookInfo bookInfo, int pageCount) : this(bookInfo, pageCount, null, null, null) { }

        public BaseReadBook(ReadBookInfo bookInfo, String title, String author, int pageCount) : this(bookInfo, pageCount, title, author, null) { }

        public BaseReadBook(ReadBookInfo bookInfo, int pageCount, String title, String author, BaseBook sourceBook)
            : base(bookInfo.ItemID)
        {
            m_Background = bookInfo.BackgroundID;
            m_CustomArt = bookInfo.CustomArt;
            m_BigBook = bookInfo.BigBook;
            BookContent content = this.DefaultContent;

            if (sourceBook != null)
            {
                m_Author = sourceBook.Author;
                m_Title = sourceBook.Title;
                m_Pages = (BookPageInfo[])sourceBook.Pages.Clone();
                m_Full = true;
            }
            else
            {
                if (content == null)
                {
                    m_Pages = new BookPageInfo[pageCount];
                    for (int i = 0; i < m_Pages.Length; ++i)
                        m_Pages[i] = new BookPageInfo();
                }
                else
                {
                    m_Title = content.Title;
                    m_Author = content.Author;
                    m_Pages = content.Copy();
                }                
                
            }
            if (author != null)
            {
                m_Author = author;
            }
            if (title != null)
            {
                m_Title = title;
            }
            if ((author != null) && (title != null))
            {
                m_Full = true;
            }
            Weight = BookWeight;
        }

        public BaseReadBook(Serial serial)
            : base(serial)
        { }


        [CommandProperty(AccessLevel.GameMaster)]
        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Background
        {
            get { return m_Background; }
            set { m_Background = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Container Owner
        {
            get { return m_owner; }
            set { m_owner = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ShowTitlePage
        {
            get { return m_Titlepage; }
            set { m_Titlepage = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Author
        {
            get { return m_Author; }
            set { m_Author = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int ItemID
        {
            get { return base.ItemID; }
            set { base.ItemID = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int PagesCount
        {
            get { return m_Pages.Length; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool UsesCustomGumpArt
        {
            get { return m_CustomArt; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsFull
        {
            get { return m_Full; }
            set { m_Full = value; }
        }

        public bool isBigBook
        {
            get { return m_BigBook; }
        }

        public BookPageInfo[] Pages
        {
            get { return m_Pages; }
        }

        public virtual double BookWeight { get { return 1.0; } }

        public bool CopyFromBook(BaseBook book)
        {
            if (IsFull)
            {
                return false;
            }
            m_Author = book.Author;
            m_Title = book.Title;
            m_Pages = (BookPageInfo[])book.Pages.Clone();
            InvalidateProperties();
            return true;
        }

        public int indexOf(string text)
        {
            if (m_Author.IndexOf(text) != -1)
            {
                return 0;
            }
            if (m_Title.IndexOf(text) != -1)
            {
                return 0;
            }
            for (int i = 0; i < m_Pages.Length; i++)
            {
                if ((m_Pages[i] != null) && (m_Pages[i].Lines != null))
                {
                    for (int j = 0; j < m_Pages[i].Lines.Length; j++)
                    {
                        if (m_Pages[i].Lines[j] != null)
                        {
                            if (m_Pages[i].Lines[j].IndexOf(text) != -1)
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if ((m_Title != null) && (m_Title.Length > 0))
                list.Add(m_Title);
            else
                base.AddNameProperty(list);
        }

        public override void GetContextMenuEntries(Mobile from, System.Collections.Generic.List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            if (!IsFull)
            {
                list.Add(new CopyFromBookEntry(this));
            }
        }            

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_Pictures.Count);

            for (int i = 0; i < m_Pictures.Count; i++)
            {
                Picture pic = (Picture)m_Pictures[i];
                pic.Serialize(writer);
            }         

            writer.Write(m_Title);
            writer.Write(m_Author);
            writer.Write(m_Background);
            writer.Write(m_Titlepage);
            writer.Write(m_Full);
            writer.Write(m_BigBook);
            writer.Write(m_CustomArt);

            writer.Write(m_Pages.Length);

            for (int i = 0; i < m_Pages.Length; ++i)
                m_Pages[i].Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        int pic_number = reader.ReadInt();

                        for (int i = 0; i < pic_number; i++)
                        {
                            Picture pic = new Picture(PictureTypes.gump, -1, -1, -1, -1, -1);
                            pic.Deserialize(reader);
                            m_Pictures.Add(pic);
                        }

                        m_Title = reader.ReadString();
                        m_Author = reader.ReadString();
                        m_Background = reader.ReadInt();
                        m_Titlepage = reader.ReadBool();
                        m_Full = reader.ReadBool();
                        m_BigBook = reader.ReadBool();
                        m_CustomArt = reader.ReadBool();

                        m_Pages = new BookPageInfo[reader.ReadInt()];

                        for (int i = 0; i < m_Pages.Length; ++i)
                            m_Pages[i] = new BookPageInfo(reader);
                        break;
                    }
            }
            Weight = BookWeight;
        }

        public override void OnSingleClick(Mobile from)
        {
            LabelTo(from, "{0} by {1}", m_Title, m_Author);
            LabelTo(from, "[{0} pages]", m_Pages.Length);
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendGump(new ReadBookGump(this, 0, from));
        }

        public virtual bool CanRead(Mobile reader)
        {

            return true;
        }

        public virtual void onPageRead(int page)
        {
        }
    }


    public class CopyFromBookEntry : ContextMenuEntry
    {
        public CopyFromBookEntry(BaseReadBook book)
            : base(2955261, 1)
        {
        }

        public override void OnClick()
        {
            if (Owner.From.CheckAlive() && Owner.Target is BaseReadBook)
                Owner.From.Target = new InternalTarget((BaseReadBook)Owner.Target);
        }

        private class InternalTarget : Target
        {
            private BaseReadBook sourceBook;

            public InternalTarget(BaseReadBook book)
                : base(3, false, TargetFlags.None)
            {
                sourceBook = book;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseBook)
                {
                    if (from.CheckAlive() && !sourceBook.Deleted && sourceBook.Movable && sourceBook.Amount >= 1)
                    {
                        BaseBook originBook = (BaseBook)targeted;
                        sourceBook.CopyFromBook(originBook);
                    }
                }
            }
        }
    }

    public class ReadBookGump : Gump
    {
        private BaseReadBook m_book;
        private int m_page;
        private Mobile m_reader;

        public ReadBookGump(BaseReadBook source, int actualpage, Mobile reader)
            : base(0, 0)
        {
            m_reader = reader;
            m_book = source;
            m_page = actualpage;
            int indentHor = horizontalIndent();
            int indentVer = verticalIndent();
            AddImage(0, 0, source.Background);
            if (actualpage > 0)
            {
                AddButton(23 + indentHor, 5 + indentVer, 0x89D, 0x89D, 1, GumpButtonType.Reply, indentHor);
            }
            if (actualpage < (source.Pages.Length - 1))
            {
                AddButton(293 + indentHor, 5 + indentVer, 0x89E, 0x89E, 2, GumpButtonType.Reply, indentHor);
            }
            if (actualpage == 0)
            {
                addTitle(26 + indentHor, 9 + indentVer, source.Title, source.Author, source.ShowTitlePage);
            }
            else
            {
                AddTextToPage(source.Pages[actualpage - 1].Lines, indentHor, indentVer, actualpage);
            }
            if (actualpage < source.Pages.Length)
            {
                AddTextToPage(source.Pages[actualpage].Lines, 155 + indentHor, indentVer, actualpage + 1);
            }
            else
            {
                AddTextToPage(null, 155 + indentHor, indentVer, actualpage + 1);
            }
            source.onPageRead(actualpage);
        }

        private void addTitle(int x, int y, String title, String author, bool image)
        {
            // Titlepage GumpID is 0x89C
            if (image)
            {
                AddImage(x-171, y-137, 0x89C);
            }

            if (title != null)
            {
                AddHtml(x + 22, y + 40, 100, 50, "<basefont size=1><center>" + title + "</center></basefont>", false, false);
            }
            if (author != null)
            {
                AddHtml(x + 22, y + 130, 100, 20, "<basefont size=1><center>" + author + "</center></basefont>", false, false);
            }
        }

        private void AddPictureToPage(int page)
        {
            for (int i = 0; i < m_book.Pictures.Count; i++)
            {
                Picture pic = (Picture)m_book.Pictures[i];

                if (pic.Page == page)
                {
                    if (pic.Type == PictureTypes.gump)
                    {
                        this.AddImage(pic.X, pic.Y, pic.Id, pic.Hue);
                    }
                    else
                    {
                        this.AddItem(pic.X, pic.Y, pic.Id, pic.Hue);
                    }
                }
            }
        }

        private void AddTextToPage(String[] lines, int indentHor, int indentVer, int actualpage)
        {
            AddHtml(100 + indentHor, 185 + indentVer, 20, 20, "<basefont size=1>" + actualpage + "</basefont>", false, false);

            if (lines != null)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != null)
                    {

                        AddHtml(indentHor + 30, (i * 17) + 25 + indentVer, 200, 17, "<basefont size=1>" + lines[i] + "</font>", false, false);
                    }
                }
            }

            this.AddPictureToPage(actualpage - 1);

        }

        private string convertToForeign(string text)
        {
            string toReturn = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    toReturn += " ";
                }
                else
                {
                    toReturn += "?";
                }
            }
            return toReturn;
        }

        private int horizontalIndent()
        {
            if (m_book.isBigBook)
            {
                return 28;
            }
            return 0;
        }

        private int verticalIndent()
        {
            if (m_book.isBigBook)
            {
                return 4;
            }
            return 0;
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID <= 0)
                return;

            int buttonId = info.ButtonID;
            switch (buttonId)
            {
                case 1:
                    {
                        // sender.Mobile.PlaySound( ??? ); // Page turning sound is
                        sender.Mobile.SendGump(new ReadBookGump(m_book, m_page - 2, m_reader));
                        break;
                    }
                case 2:
                    {
                        // sender.Mobile.PlaySound( ??? ); // Page turning sound is
                        sender.Mobile.SendGump(new ReadBookGump(m_book, m_page + 2, m_reader));
                        break;
                    }
            }
        }


    }

    public enum PictureTypes
    {
        gump = 0,
        item = 1
    }

    public class Picture
    {
        // private int[,] locations = { {0,0}, {100,100}, {200,200} };

        private PictureTypes m_type;
        private int m_id;
        private int m_page;
        private int m_x;
        private int m_y;
        private int m_hue;

        public PictureTypes Type
        {
            get { return m_type; }
        }

        public int Hue
        {
            get { return m_hue; }
        }

        public int Id
        {
            get { return m_id; }
        }

        public int Page
        {
            get { return m_page; }
        }

        public int X
        {
            get { return m_x; }
        }

        public int Y
        {
            get { return m_y; }
        }

        public Picture(PictureTypes type, int id, int hue, int page, int x, int y)
        {
            m_id = id;
            m_type = type;
            m_page = page;
            m_x = x;
            m_y = y;
            m_hue = hue;
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write((int)0); // Version
            writer.Write((int)m_type);
            writer.Write(m_id);
            writer.Write(m_page);
            writer.Write(m_x);
            writer.Write(m_y);
            writer.Write(m_hue);
        }

        public void Deserialize(GenericReader reader)
        {
            int version = reader.ReadInt();

            m_type = (PictureTypes)reader.ReadInt();
            m_id = reader.ReadInt();
            m_page = reader.ReadInt();
            m_x = reader.ReadInt();
            m_y = reader.ReadInt();
            m_hue = reader.ReadInt();

        }
    }
}