using System;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
    public class QuestScroll : Item
	{
		///// CONFIGURE THE LOCATIONS HERE ///////////////////////////////////////////////////////////////
		// MAKE SURE YOU ADD THE X & Y & MAP TO THE "SECTION - LCXY1" LOWER IN THE SCRIPT ////////////////
		public static string[] Places1 { get { return m_Places1; } }
		public static string[] Places2 { get { return m_Places2; } }
		public static string[] Places3 { get { return m_Places3; } }
		public static string[] Places4 { get { return m_Places4; } }
		public static string[] Places5 { get { return m_Places5; } }
		public static string[] Places6 { get { return m_Places6; } }

		private static string[] m_Places1 = new string[]
		{
			"Dungeon Despise (Level 1)",
			"Dungeon Despise (Level 2)",
			"the Sewers"
		};

		private static string[] m_Places2 = new string[]
		{
			"Dungeon Covetous (Level 1)",
			"Dungeon Despise (Level 3)",
			"Dungeon Wrong (Level 1)",
			"the Spider Cave",
			"the Britain Cemetery",
			"the Spectre Dungeon"
		};

		private static string[] m_Places3 = new string[]
		{
			"Dungeon Covetous (Level 2)",
			"Dungeon Deceit (Level 1)",
			"Dungeon Deceit (Level 2)",
			"Dungeon Hythloth (Level 1)",
			"Dungeon Shame (Level 1)",
			"Dungeon Wrong (Level 2)",
			"Dungeon Wrong (Level 3)",
			"the Trinsic Passage",
			"the Ratman Mines (Level 1)",
			"the Wisp Dungeon (Level 3)"
		};

		private static string[] m_Places4 = new string[]
		{
			"Dungeon Ankh",
			"Dungeon Covetous (Level 3)",
			"Dungeon Deceit (Level 3)",
			"Dungeon Hythloth (Level 2)",
			"Dungeon Shame (Level 2)",
			"the Fire Dungeon (Level 1)",
			"the Ice Dungeon",
			"the Ratman Fort",
			"the Rock Dungeon",
			"the Solen Hive",
			"the Sorcerer`s Dungeon (Level 1)",
			"the Wisp Dungeon (Level 5)",
			"the Hedge Maze"
		};

		private static string[] m_Places5 = new string[]
		{
			"Dungeon Covetous (Jail Cells)",
			"Dungeon Deceit (Level 4)",
			"Dungeon Hythloth (Level 3)",
			"Dungeon Khaldun",
			"Dungeon Shame (Level 3)",
			"Dungeon Shame Mage Towers",
			"Terathan Keep",
			"the Fire Dungeon (Level 2)",
			"the Kirin Passage",
			"the Serpentine Passage",
			"the Ratman Mines (Level 2)",
			"the Sorcerer`s Dungeon (Jail Cells)",
			"the Sorcerer`s Dungeon (Level 2)",
			"the Wisp Dungeon (Level 7)"
		};

		private static string[] m_Places6 = new string[]
		{
			"Bedlam",
			"Dungeon Blood",
			"Dungeon Covetous (Lake Cave)",
			"Dungeon Destard",
			"Dungeon Doom",
			"Dungeon Hythloth (Level 4)",
			"Dungeon Shame (Level 5)",
			"the Ancient Cave",
			"the Ice Demon Lair",
			"the Sorcerer`s Dungeon (Level 3)",
			"Dungeon Exodus",
			"the Wisp Dungeon (Level 8)"
		};

		///// CONFIGURE THE CREATURES HERE ///////////////////////////////////////////////////////////////
		// MAKE SURE YOU ADD THE TYPES TO THE "SECTION - MNTP1" LOWER IN THE SCRIPT //////////////////////
		public static string[] Monster1 { get { return m_Monster1; } }
		public static string[] Monster2 { get { return m_Monster2; } }
		public static string[] Monster3 { get { return m_Monster3; } }
		public static string[] Monster4 { get { return m_Monster4; } }
		public static string[] Monster5 { get { return m_Monster5; } }
		public static string[] Monster6 { get { return m_Monster6; } }

		private static string[] m_Monster1 = new string[]
		{
			"Bull Frog",
			"Giant Rat",
			"Lizardman",
			"Mongbat",
			"Orc",
			"Ratman",
			"Sewer Rat",
			"Skeleton",
			"Slime",
			"Zombie"
		};

		private static string[] m_Monster2 = new string[]
		{
			"Brigand",
			"Earth Elemental",
			"Ettin",
			"Gazer Larva",
			"Ghoul",
			"Giant Spider",
			"Giant Toad",
			"Harpy",
			"Headless One",
			"Orc Bomber",
			"Scorpion",
			"Shade",
			"Spectre",
			"Wraith"
		};

		private static string[] m_Monster3 = new string[]
		{
			"Agapite Elemental",
			"Air Elemental",
			"Ant Lion",
			"Beetle",
			"Bogle",
			"Bogling",
			"Bone Knight",
			"Bone Magi",
			"Bronze Elemental",
			"Copper Elemental",
			"Corpser",
			"Crystal Elemental",
			"Cyclops",
			"Deathwatch Beetle",
			"Deathwatch Beetle Hatchling",
			"Dull Copper Elemental",
			"Fire Beetle",
			"Fire Elemental",
			"Flesh Golem",
			"Frost Ooze",
			"Frost Spider",
			"Frost Troll",
			"Gargoyle",
			"Gazer",
			"Giant Serpent",
			"Golden Elemental",
			"Gore Fiend",
			"Hell Hound",
			"Ice Elemental",
			"Ice Snake",
			"Imp",
			"Lava Snake",
			"Minotaur",
			"Ogre",
			"Ophidian Knight",
			"Ophidian Warrior",
			"Orc Brute",
			"Orc Captain",
			"Orcish Lord",
			"Orcish Mage",
			"Patchwork Skeleton",
			"Ratman Archer",
			"Ratman Mage",
			"Ridgeback",
			"Sand Vortex",
			"Savage",
			"Savage Rider",
			"Savage Shaman",
			"Shadow Iron Elemental",
			"Skeletal Knight",
			"Skeletal Mage",
			"Snow Elemental",
			"Stone Gargoyle",
			"Stone Harpy",
			"Terathan Drone",
			"Terathan Warrior",
			"Troll",
			"Valorite Elemental",
			"Vampire Bat",
			"Verite Elemental",
			"Wailing Banshee",
			"Water Elemental"
		};

		private static string[] m_Monster4 = new string[]
		{
			"Bog Thing",
			"Centaur",
			"Drake",
			"Dread Spider",
			"Elite Ninja",
			"Evil Mage",
			"Exodus Minion",
			"Exodus Overseer",
			"Fan Dancer",
			"Fire Gargoyle",
			"Gargoyle Destroyer",
			"Gargoyle Enforcer",
			"Giant Black Widow",
			"Golem",
			"Golem Controller",
			"Ice Serpent",
			"Lava Lizard",
			"Lava Serpent",
			"Lich",
			"Minotaur Scout",
			"Mummy",
			"Ogre Lord",
			"Ophidian Archmage",
			"Ophidian Mage",
			"Ophidian Matriarch",
			"Phoenix",
			"Plague Spawn",
			"Quagmire",
			"Restless Soul",
			"Ronin",
			"Rune Beetle",
			"Sea Serpent",
			"Shadow Fiend",
			"Swamp Dragon",
			"Swamp Tentacle",
			"Terathan Avenger",
			"Terathan Matriarch",
			"Titan",
			"Wyvern"
		};

		private static string[] m_Monster5 = new string[]
		{
			"Arctic Ogre Lord",
			"Blood Elemental",
			"Daemon",
			"Efreet",
			"Elder Gazer",
			"Evil Mage Lord",
			"Fire Steed",
			"Ice Fiend",
			"Juggernaut",
			"Lich Lord",
			"Minotaur Captain",
			"Nightmare",
			"Plague Beast",
			"Poison Elemental",
			"Rotting Corpse",
			"Serpentine Dragon",
			"Silver Serpent"
		};

		private static string[] m_Monster6 = new string[]
		{
			"Ancient Lich",
			"Ancient Wyrm",
			"Balron",
			"Deep Sea Serpent",
			"Dragon",
			"Kraken",
			"Shadow Wyrm",
			"Skeletal Dragon",
			"Succubus",
			"White Wyrm"
		};


		///// CONFIGURE THE ITEM STORY HERE ///////////////////////////////////////////////////////////////
		public static string[] Story1 { get { return m_Story1; } }

		private static string[] m_Story1 = new string[]
		{
			"Rumors say that it is in", "The prophecy claims it is in", "The ancient text talks of it in",
			"Some say they have seen it in", "It is said you can find it in", "It was lost in",
			"It was left behind in", "It was hidden in", "An old map says it is in", "A discovered scroll says it can be found in",
			"This recovered tablet tells of it in", "A fairy tale tells of it in", "a rambling wizard says it can be found in",
			"Many have claimed to see it in", "They claim it was stolen while they were in", "Someone dropped it while exploring",
			"A bard sung of it being in", "Some gypsy said it can be found in"
		};


		///// CONFIGURE THE MONSTER STORY HERE ///////////////////////////////////////////////////////////
		public static string[] Story2 { get { return m_Story2; } }
		public static string[] Story3 { get { return m_Story3; } }

		private static string[] m_Story2 = new string[]
		{
			"wants them dead", "wants revenge for their daughter`s death", "wants revenge for their son`s death",
			"wants to rid the land of them", "is giving rewards for their deaths", "is afraid to face them on their own",
			"is hiring many people to slay them", "has been wounded by them and is quite angry", "is pushing for their extinction",
			"is trying to clear them out", "will not rest until they are all wiped out", "wants them in our land no longer"
		};

		private static string[] m_Story3 = new string[]
		{
			"For the death of", "By decree of", "For the disappearance of", "Bounty put out by", "Rewards offered by",
			"For the attack on", "For terrorizing", "For the death of the father of", "For the death of the mother of",
			"For the assault on", "Ordered by"
		};


		///// CONFIGURE THE MAIN ITEMs HERE //////////////////////////////////////////////////////////////
		public static string[] Items1 { get { return m_Items1; } }

		private static string[] m_Items1 = new string[]
		{
			"Orb", "Crystal", "Eye", "Jewel", "Gem", "Axe", "Pike", "Shield", "Rapier", "Sword", "Staff", "Cube", "Globe", "Ring", 
			"Necklace", "Amulet", "Book", "Tome", "Potion", "Elixir", "Scroll", "Wand", "Helm", "Gloves", "Bow", "Gauntlets", "Bracers",
			"Robe", "Cloak", "Horn", "Knife", "Dagger", "Mace", "Spear", "Boots", "Stone", "Rod", "Harp", "Crossbow", "Crown", "Talisman",
			"Totem", "Idol", "Chalice", "Armor", "Belt", "Bracelet"
		};


		///// CONFIGURE THE ENDING ADVJECTIVES HERE //////////////////////////////////////////////////////
		public static string[] Items2 { get { return m_Items2; } }

		private static string[] m_Items2 = new string[]
		{
			"Frost", "Ice", "Fire", "Death", "Doom", "Power", "Enchantment", "Invulnerability", "Might", "Protection", "Shadows", "Light",
			"Evil", "Truth", "Cold", "Poison", "Energy", "Force", "Justice", "Life", "Warning", "Destruction", "Healing", "the Gods",
			"Fortune", "Teleportation", "Astral Travel", "Illusion", "Darkness",
			"Frost", "Ice", "Fire", "Death", "Doom", "Power", "Enchantment", "Invulnerability", "Might", "Protection", "Shadows", "Light",
			"Evil", "Truth", "Cold", "Poison", "Energy", "Force", "Justice", "Life", "Warning", "Destruction", "Healing", "the Gods",
			"Fortune", "Teleportation", "Astral Travel", "Illusion", "Darkness",
			"the Templar", "the Thief", "the Illusionist", "the Princess", "the Invoker", "the Priest",
			"the Conjurer", "the Bandit", "the Priestess", "the Baron", "the Wizard", "the Cleric",
			"the Monk", "the Minstrel", "the Defender", "the Cavalier", "the Magician", "the Witch",
			"the Fighter", "the Seeker", "the Slayer", "the Ranger", "the Barbarian", "the Explorer",
			"the Heretic", "the Gladiator", "the Sage", "the Rogue", "the Paladin", "the Bard",
			"the Diviner", "the Lord", "the Outlaw", "the Prophet", "the Mercenary", "the Adventurer",
			"the Enchanter", "the King", "the Scout", "the Mystic", "the Mage", "the Traveler",
			"the Summoner", "the Queen", "the Warrior", "the Sorcerer", "the Seer", "the Hunter",
			"the Knight", "the Prince", "the Necromancer", "the Sorceress", "the Shaman"
		};


		///// CONFIGURE THE STARTING ADJECTIVES HERE /////////////////////////////////////////////////////
		public static string[] Items3 { get { return m_Items3; } }

		private static string[] m_Items3 = new string[]
		{
			"Exotic", "Mysterious", "Enchanted", "Marvelous", "Amazing", "Astonishing", "Mystical",
			"Astounding", "Magical", "Divine", "Excellent", "Magnificent", "Phenomenal", "Fantastic",
			"Incredible", "Miraculous", "Extraordinary", "Fabulous", "Wondrous", "Glorious", "Dreadful",
			"Horrific", "Terrible", "Disturbing", "Frightful", "Awful", "Dire", "Grim", "Vile", "Lost", "Fabled",
			"Legendary", "Mythical", "Missing", "Doomed", "Endless", "Eternal", "Exalted", "Glimmering",
			"Sadistic", "Disrupting", "Spiritual", "Demonic", "Holy", "Heavenly", "Ancestral",
			"Ornate", "Ultimate", "Abyssmal", "Crazed", "Elven", "Orcish", "Dwarvish", "Gnomish", "Cursed",
			"Sylvan", "Wizardly", "Sturdy", "Disturbing", "Odd", "Rare", "Treasured", "Damned", "Evil",
			"Lawful", "Foul", "Infernal", "Royal", "Worldy", "Blasphemous", "Planar", "Wonderful",
			"Perfected", "Vicious", "Chaotic", "Haunted", "Travelling", "Unholy", "Infernal",
			"Villainous", "Accursed", "Fiendish", "Adored", "Hallowed", "Glorified", "Sacred",
			"Blissful", "Almighty", "Dominant", "Supreme", "Fallen", "Dark", "Earthly", "Mighty",
			"Unspeakable", "Unknown", "Forgotten", "Deathly", "Undead", "Infinite", "Abyssmal"
		};

		///// CONFIGURE THE ITEM OWNERS HERE /////////////////////////////////////////////////////////////
		public static string[] Items4 { get { return m_Items4; } }

		private static string[] m_Items4 = new string[]
		{
			"Adaradle", "Cimaclidor", "Gertur", "Maldorink", "Robois", "Angunald", "Aade", "Mabias",
			"Adkyna", "Cinghin", "Gladh", "Marmosi", "Rondona", "Aurging", "Ddald", "Liding",
			"Aeferryfela", "Cinthereban", "Glasar", "Marrese", "Rothiw", "Cala", "Dhonl", "Madon",
			"Aehes", "Cirth", "Glassa", "Marri", "Ruadhad", "Cangla", "Dorome", "Maelowr",
			"Aemathan", "Gobar", "Marus", "Ruamnan", "Chan", "Eesa", "Maeluinus",
			"Aenchoba", "Cognvareding", "Gobharlas", "Marvaldiri", "Ruditherming", "Chuthmmi", "Eiba", "Lifferg",
			"Acwulf", "Colairion", "Gimran", "Marry", "Selin", "Dorthu", "Farine", "Litiny",
			"Adakonico", "Colan", "Ginou", "Marus", "Selrontus", "Falla", "Fkisl", "Llice",
			"Aeter", "Collsbe", "Godrica", "Marzhaetod", "Ryannen", "Fauauthencaro", "Gerrut", "Lonan",
			"Adegen", "Collullen", "Glederile", "Marvegis", "Senstarlough", "Gorgortuna", "Higafi", "Magda",
			"Agraien", "Colmgristrow", "Goibhinnach", "Matha", "Sabhrewi", "Hallalur", "Kalir", "Loron",
			"Agrimona", "Colphe", "Gollewyn", "Mattancheude", "Sadyn", "Haulo", "Kesig", "Maglor",
			"Adulf", "Comgalant", "Goibhin", "Mathere", "Seporvi", "Hmoturol", "Moc", "Malog",
			"Agulac", "Conandinas", "Gouthos", "Maxina", "Sadyn", "Kalli", "Oige", "Malpaigley",
			"Aieth", "Concouise", "Grevon", "Melain", "Saegalach", "Lalarornargw", "Rer", "Luches",
			"Aimboruin", "Concs", "Gunory", "Melan", "Saemglas", "Langororobat", "Ritdar", "Maagdvar",
			"Aesci", "Concuxoman", "Goibhir", "Mefferog", "Sequa", "Lkatugin", "Rud", "Macka",
			"Aesiry", "Conner", "Gomeri", "Meladrywood", "Serne", "Lungoth", "Rufar", "Malrickannie",
			"Aitharrely", "Connlugon", "Gwain", "Melathe", "Saiuche", "Miantu", "Sbit", "Malronna",
			"Aethennye", "Conovan", "Gondorcan", "Metta", "Seven", "Moguglk", "Sirtuf", "Maddi",
			"Alennifiel", "Coolis", "Gwalloyd", "Meldurie", "Sammarth", "Sair", "Suro", "Maolinn",
			"Aethosgorla", "Corbjalas", "Gorbis", "Milimbron", "Shaug", "Ugurirt", "Teddot", "Maeret",
			"Agius", "Cordberthyn", "Gorsaihirri", "Moire", "Sheelana", "Vangulurc", "Tedula", "Maglach",
			"Agnat", "Corflaneu", "Grachansien", "Molum", "Shenargharth", "Weglauli", "Tirik", "Maiet",
			"Aiblaithet", "Corina", "Granwy", "Monesthet", "Shenn", "Welrot", "Vada", "Malcolum",
			"Aille", "Coryssa", "Guolde", "Morga", "Shermund", "Alagula", "Aere", "Marcanovane",
			"Alfil", "Covedu", "Gwazeldan", "Meline", "Sandoralith", "Baimmagild", "Dalbal", "Salther",
			"Alherson", "Cregodsunnye", "Gwensin", "Melinneth", "Santhinter", "Caualorirchal", "Delga", "Praergaed",
			"Ailpin", "Creicath", "Gwene", "Morgan", "Shouenollus", "Chaldu", "Dir", "Samaneelene",
			"Alescforther", "Cucus", "Gwith", "Mortiphri", "Sibyll", "Gitulinaethegu", "Dired", "Samith",
			"Alewenna", "Cuintraya", "Haaric", "Moynteinvain", "Sibyrnach", "Hengona", "Fohor", "Sammen",
			"Alingvar", "Culbury", "Gwirkitta", "Meluf", "Saoignolf", "Kaert", "Garit", "Priel",
			"Alien", "Culleofer", "Hadrifiel", "Muirchar", "Signachne", "Kamothuro", "Grocr", "Priveliann",
			"Aliquesnan", "Cunedo", "Hariomhnaid", "Mylee", "Sigurbertur", "Kauiaz", "Hidte", "Prossoke",
			"Allan", "Cunomen", "Hayla", "Naiglossi", "Sipithne", "Korndur", "Hokede", "Pwyllgever",
			"Altheired", "Cunos", "Heiliach", "Nathianus", "Sirardolen", "Lkortul", "Kgedn", "Samporotic",
			"Alungoth", "Cyhilgaleth", "Helmhearlo", "Nealemberhae", "Smiss", "Lungodu", "Lgob", "Quienere",
			"Alvarth", "Cymox", "Helmott", "Nelan", "Solia", "Mamelaz", "Meti", "Sanben",
			"Amaerell", "Cynhel", "Helwyn", "Netheilos", "Sorod", "Nathimingorm", "Nbadr", "Sanberin",
			"Alpho", "Cynlo", "Gwynthian", "Mengar", "Saxus", "Nchammaglkaug", "Nir", "Santheiua",
			"Amalphyth", "Cynraenta", "Halann", "Merdan", "Scait", "Odulurchirordr", "Nondeb", "Quier",
			"Amervis", "Cyrus", "Hazra", "Mersides", "Schaed", "Rilalavangon", "Radavi", "Ragorten",
			"Ancelyndy", "Daalneylyn", "Hearbathekin", "Meryl", "Schondorn", "Rothi", "Rakrit", "Readwennor",
			"Andalf", "Daghallfreda", "Heatosus", "Mewanezel", "Scipirene", "Rthaetulauglu", "Rocnot", "Saury",
			"Anskarim", "Daille", "Hedden", "Millon", "Serezik", "Sakaugitur", "Rohag", "Seenad",
			"Aodre", "Dalbeth", "Helhervi", "Mindon", "Seuma", "Shmmelarirot", "Rreht", "Reosa",
			"Ammeidoc", "Dalie", "Hered", "Nicathrennur", "Spier", "Vandrthuindu", "Ruhdo", "Segast",
			"Amsyndser", "Dalwing", "Hervyn", "Niccus", "Sripthien", "Vaturirthm", "Rumibe", "Reose",
			"Apias", "Damalongan", "Hered", "Minmo", "Shaus", "Vaugberugia", "Simuyo", "Reosef",
			"Andabanberth", "Davetus", "Hildiwa", "Nijenn", "Steald", "Wergor", "Tarkor", "Rhybranen",
			"Anfast", "Deaga", "Hilgriwa", "Nimide", "Steine", "Baingurchul", "Agluah", "Ribus",
			"Aracbergtun", "Decyvedu", "Heredergana", "Moglos", "Sigdis", "Cathiaullrthmm", "Alruur", "Seibhing",
			"Anfela", "Dedegant", "Hires", "Norix", "Steinzena", "Chimak", "Arucraen", "Cathes",
			"Anfridge", "Degil", "Huntresa", "Nuarnoth", "Stwine", "Drndethmatugl", "Athotd", "Carnach",
			"Angborn", "Deiristo", "Hyldristick", "Ogmael", "Suald", "Dronaurndurdug", "Bariag", "Catinethiana",
			"Araddox", "Delany", "Hirmelis", "Moinsanth", "Siger", "Durorind", "Cahubraig", "Celotherich",
			"Aragorn", "Dener", "Hithur", "Molfrikinoth", "Sillovigfus", "Eglrorgundrugo", "Cihededt", "Certan",
			"Anjarmoth", "Denkth", "Iardall", "Oilbhe", "Sulish", "Encatu", "Dogihsosh", "Cassavusime",
			"Annal", "Derriht", "Iatachtach", "Oilfhil", "Svafnkell", "Fando", "Edatibulh", "Cassimurcia",
			"Arberyd", "Derufineryn", "Holas", "Molueli", "Slefsunsha", "Gurinca", "Habsaul", "Catisoth",
			"Ansellryeter", "Desth", "Ideardin", "Oranithos", "Swald", "Gwegorodulam", "Haglitr", "Cativ",
			"Ap-Owerkhes", "Devan", "Idwene", "Orcailifeth", "Sybet", "Ilkarirthar", "Hircalh", "Changaniam",
			"Amlait", "D'Evre", "Heofsa", "Nevettele", "Spant", "Ituth", "Huhahn", "Ceighterri",
			"Archite", "Diand'", "Hosbeth", "Morgar", "Sodenevrina", "Korchaug", "Ibnotn", "Chani",
			"Aptidh", "Dician", "Ilford", "Ordys", "Taigardus", "Korilo", "Ilheab", "Chastinia",
			"Argond", "Dilarion", "Hracye", "Morgonan", "Solas", "Kormiaturgo", "Inorelinl", "Chlinethian",
			"Ariwen", "Dimus", "Hrotheker", "Morix", "Sontz", "Ngogiak", "Itolraab", "Celeribe",
			"Aradys", "Dockermas", "Imchad", "Orefrfast", "Talig", "Ntulrolo", "Lalihg", "Celeved",
			"Arald", "Doireth", "Imlad", "Orody", "Tegar", "Ogoturdugldr", "Loceraoh", "Celte",
			"Arlethawluig", "Doirss", "Hroxie", "Morna", "Sorry", "Olar", "Nennuan", "Ciach",
			"Arhtshallmo", "Domnallys", "Ininna", "Oroke", "Tegarmail", "Orgo", "Olalehaad", "Cibus",
			"Arley", "Donath", "Inionna", "Orrianaid", "Telizez", "Rururch", "Sicatatr", "Celyan",
			"Arlygan", "Donchorundon", "Huailindrog", "Morwe", "Spaisia", "Sharua", "Tinariin", "Cinunnse",
			"Arlos", "Donnie", "Intha", "Osbeorsatus", "Telrichardo", "Vakazgl", "Ubidnert", "Cercnain",
			"Arnagh", "Driathink", "Hunter", "Morwenn", "Stebbi", "Vamimm", "Ulerteeh", "Chrono",
			"Arnethire", "Drovath", "Hwambrevonn", "Mothe", "Straid", "Akoduti", "Ablagr", "Ciacion",
			"Asberg", "Dryrymon", "Idelos", "Mouthiana", "Strogiel", "Asudad", "Adattang", "Ciarion",
			"Artuir", "Dubhe", "Ioardoc", "Otbond", "Tendiegisan", "Atadedak", "Adenalinl", "Clemma",
			"Athallyr", "Dubiggur", "Iehmassi", "Muadamulakk", "Surre", "Axeer", "Aralcail", "Fugeiria",
			"Avien", "Duilen", "Iseas", "Ottiriel", "Terfula", "Binye", "Aribihd", "Gablez",
			"Aylina", "Dumnagh", "Iethilo", "Muiriehmarus", "Sveinestel", "Edeline", "Atedetiet", "Gaess",
			"Avituc", "Dumnochobb", "Isotharic", "Overninus", "Terot", "Etifaca", "Bogatbius", "Gaillughaill",
			"Aylinsonse", "Eadan", "Ilchoard", "Mulnus", "Svert", "Lelhi", "Casbuit", "Galadrime",
			"Baishian", "Eafrikinn", "Imloth", "Murry", "Sween", "Mutydare", "Dunaet", "Galak",
			"Barladucus", "Eaneidh", "Imrilla", "Myles", "Swine", "Niyni", "Edelocoel", "Galduit",
			"Beardolde", "Earcorodrich", "Indingaer", "Naogurm", "Tadhagol", "Odadinir", "Ehalonoad", "Garrinaic",
			"Ayleribesta", "Ebemma", "Issch", "Palman", "Teseibhne", "Ohibarat", "Erdaud", "Garry",
			"Baith", "Eburn", "Jartman", "Paraps", "Teway", "Raruced", "Godreub", "Gatiarth",
			"Becca", "Edrich", "Inmouthor", "Naueritta", "Tallaith", "Rexaxrem", "Hatalitt", "Gauros",
			"Balyesmourn", "Edynoreth", "Jarvelaed", "Pawlynn", "Thats", "Roletizi", "Isniah", "Gavin",
			"Barrim", "Effroyd", "Jazma", "Peole", "Therly", "Royazar", "Lahhoas", "Gavinas",
			"Bedwick", "Eitin", "Intheodar", "Naugh", "Tanton", "Serez", "Lediag", "Gaylesh",
			"Bauisligal", "Eksiprepri", "Jokulan", "Perond", "Therto", "Tarar", "Naderbaah", "Geathet",
			"Bearus", "Eksisbury", "Kabristofa", "Pesifalas", "Thian", "Toxva", "Ogelath", "Geirdarnan",
			"Beleg", "Ekwesth", "Ioete", "Nealys", "Tanyalek", "Utaen", "Onarareab", "Gelege",
			"Belvana", "Ekwiremia", "Ismeneouuin", "Nechuff", "Tasses", "Vosa", "Ralcaal", "Gematurg",
			"Beley", "Elagus", "Kaithe", "Peutoust", "Thidric", "Xetaroka", "Rehatb", "Genildis",
			"Benciann", "Elbius", "Kaluth", "Phana", "Thiet", "Xira", "Renenirl", "Geofgivye",
			"Benou", "Elborn", "Istacheidir", "Neile", "Tefalaf", "Zetomyhe", "Rughend", "Geofraelisa",
			"Beornorth", "Elduin", "Ivalee", "Neldur", "TerSeekonny", "Zilohona", "Teluhoer", "Georht",
			"Benji", "Elian", "Keelyne", "Pothswineron", "Thikaden", "Adinikox", "Batemz", "Gerechula",
			"Bennonne", "Elich", "Kenear", "Prak-Zig", "Thryth", "Adivema", "Bed",
			"Bethimen", "Elimagus", "Jacloves", "Nemilinny", "Teriana", "Anami", "Besotg", "Gerrim",
			"Beribenzel", "Elissire", "Kenni", "Presaric", "Thuiley", "Arira", "Bmon", "Gilos",
			"Berthause", "Elricherick", "Kennie", "Priel", "Tianarus", "Azekis", "Bokesm", "Xabilie",
			"Bethe", "Elrong", "Kennock", "Purtlan", "Tigeri", "Cihal", "Bzagn", "Woodwyn",
			"Bjarma", "Elsecgren", "Jakebing", "Netta", "Thargoll", "Cizavix", "Dob", "Xandur",
			"Blathea", "Elstanly", "Jayden", "Nevalinnyn", "Thella", "Dasibmir", "Duterm", "Xaphorster",
			"Bitane", "Emmena", "Kevyn", "Qilla", "Tillentius", "Dirob", "Gabm", "Xelan",
			"Blatell", "Emyndenelda", "Kibes", "Quaethenzio", "Tilloc", "Elofivi", "Gmerx", "Wrough",
			"Blayne", "Emynyr", "Jayne", "Nicolm", "Theod", "Ferirded", "Gobadk", "Wulla",
			"Blina", "Endrai", "Jazliko", "Nides", "Think", "Hafde", "Gox", "Wynwoioi",
			"Bolbjora", "Eneelesonket", "Knutiltito", "Quebrand", "Tirina", "Ireet", "Gud", "Xabus",
			"Bowdyn", "Enoulheirc", "Kordurh", "Ragluman", "Tiriok", "Mirital", "Kaz", "Xiommish",
			"Boanna", "Entink", "Jenncho", "Nomond", "Thoces", "Nexerek", "Kusz", "Xavio",
			"Boriya", "Eochearnir", "Jesmonan", "Norody", "Thoren", "Nyse", "Mdos", "Xippeleg",
			"Bozef", "Eochuter", "Joakhanezah", "Norysset", "Thosgarenn", "Ratsa", "Mubazs", "Yarmotherl",
			"Brach", "Eogentyne", "Joevicca", "O'Neid", "Thowella", "Renyvar", "Nazogr", "Yourn",
			"Breas", "Eosarath", "Kriseaghy", "Raskars", "Toenbert", "Ridan", "Nes", "Xydrie",
			"Bradowfax", "Eosina", "Jonya", "Odhred", "Thrabent", "Rokan", "Rabx", "Xylan",
			"Brenzander", "Epilla", "Kyantesso", "Ravaciacus", "Torchon", "Sazikak", "Rgemad", "Yeers",
			"Brespal", "Erachlo", "Laistjane", "Refil", "Trach", "Suros", "Temg", "Zahna",
			"Breth", "Eride", "Lardullian", "Reidhg", "Trecumharust", "Tehidora", "Xanozt", "Zaximo",
			"Branabell", "Ermournen", "Joscale", "Olvagosa", "Tikingal", "Todxo", "Zagumx", "Yenoli",
			"Branden", "Erricia", "Josse", "Omnaltgaliam", "Tipher", "Uvalalil", "Zdusb", "Zepheron",
			"Brethur", "Esairfydd", "Laugurespath", "Rekwe", "Tresink", "Afazer", "Brog", "Ysfall",
			"Bridra", "Estendir", "Josuuarn", "Orgeralassa", "Tissia", "Ahoni", "Bxorem", "Zabel",
			"Brilionn", "Esubsiorsa", "Jowan", "Orielsecha", "Toduned", "Ateloba", "Gebakx", "Zabeni",
			"Bring", "Etarva", "Judigbryht", "Orret", "Tothryth", "Avkov", "Gox", "Zebin ",
			"Brochebig", "Etgera", "Kaellecton", "Osbeothe", "Traem", "Dixutir", "Gxomub", "Zozzo ",
			"Brockmarille", "Ethelmotor", "Karkus", "Osbertur", "Treen", "Donorauk", "Kmot", "Boh",
			"Brythaid", "Etrick", "Katanyalin", "Osripi", "Trilynton", "Hara", "Kunedb", "Dehoro",
			"Bretta", "Eultutney", "Lawaithburga", "Relar", "Treva", "Ihizan", "Med", "Del",
			"Burthgham", "Eurieth", "Katyrus", "Ossedelle", "Tuorne", "Iroharim", "Munads", "Detro",
			"Byrtelena", "Evernen", "Kavanora", "Osvinaugh", "Tutel", "Isubasak", "Nmabg", "Fadher",
			"Cadfang", "Evrenth", "Keena", "Othryth", "Twichos", "Itoso", "Ntuser", "Fircis",
			"Briatha", "Excolma", "Leach", "Remay", "Tronellen", "Ivirikor", "Nuxr", "Fsah",
			"Brichadha", "Exinan", "Leanna", "Rendore", "Truin", "Kohurbar", "Nzork", "Garde",
			"Brith", "Faireth", "Leide", "Reote", "Tuarad", "Lidebam", "Rmukg", "Gelnar",
			"Brocht", "Falmtyrnetta", "Leigh", "Reprick", "Tudfran", "Mitik", "Rtegob", "Geri",
			"Brook", "Falyndzai", "Lenoel", "Reprost", "Tyrnaster", "Ranereku", "Suk", "Hvad",
			"Brossies", "Fanghus", "Lerice", "Rettan", "Uilief", "Ronelioc", "Tax", "Iama",
			"Caela", "Fanus", "Kende", "Ottinus", "Uaignobe", "Roxalah", "Tmazx", "Ieva",
			"Bryales", "Fareth", "Lestril", "Rhyas", "Uiran", "Ryxox", "Toxb", "Iiro",
			"Caili", "Faria", "Kennos", "Owaynegalim", "Unpith", "Tayator", "Tzasb", "Kehmul",
			"Bryanna", "Farma", "Leuff", "Rikur", "Uriarmosink", "Teri", "Tzax", "Liler",
			"Caina", "Fenectus", "Kevin", "Padsto", "Unter", "Ulari", "Xbased", "Mnavb",
			"Cairex", "Feras", "Kinburh", "Palurial", "Vadrickael", "Uridol", "Xdukez", "Nrir",
			"Caith", "Ferrick", "Kiorte", "Pants", "Valinick", "Xarirab", "Xem", "Resrol",
			"Caithren", "Ferth", "Knutun", "Parkathant", "Varingwayn", "Ymasul", "Zrebom", "Rregr",
			"Calde", "Fevard", "Kolbye", "Pearus", "Vellw", "Aafi", "Baz", "Rudik",
			"Bryth", "Filbh", "Lilion", "Rivan", "Valan", "Aeyi", "Bmetog", "Rvas",
			"Budouth", "Filies", "Linnda", "Robert", "Vauberth", "Dlarr", "Bsadon", "Tat",
			"Bygyn", "Seekirianor", "Lithlea", "Roche", "Vaunt", "Fufiri", "Bsutax", "Uavi",
			"Byrhthe", "Finshep", "Livarzegin", "Rolyn", "Veneve", "Hide", "Daxonr", "Vidra",
			"Bytzer", "Firil", "Lizola", "Romar", "Waldor", "Hihe", "Der", "Zxotug",
			"Calduil", "Firth", "Kongalangwin", "Pelegoronth", "Vidan", "Hmuht", "Derg", "Videh",
			"Calhoun", "Fithur", "Koule", "Perann", "Vorwin", "Horden", "Dezt", "Zmedr",
			"Calion", "Flaenguallyn", "Krise", "Perman", "Vuall", "Iada", "Dox", "Zrak",
			"Calura", "Flaine", "Laimithifiet", "Persidius", "Waithe", "Kcah", "Gtekus", "Ttem",
			"Camremar", "Fobble", "Latiabair", "Pirogaidach", "Waken", "Kfabt", "Magebs", "Uote",
			"Carapharyn", "Forop", "Lebahar", "Pitinis", "Weidlyn", "Ldor", "Mkanog", "Wihen",
			"Cadha", "Fough", "Lliez", "Ronni", "Wartmanna", "Ledsat", "Mtog", "Wynfreen",
			"Calla", "Fowelinder", "Lobrandiern", "Ronya", "Wathfais", "Lodar", "Mxerb", "Possimpre",
			"Camrod", "Franie", "Lomerveinse", "Rorianton", "Weies", "Mis", "Ndet", "Saebertui",
			"Camrotmather", "Frarellastie", "Losinedyth", "Rorystach", "Wightiua", "Nnukn", "Nuz", "Leoses",
			"Caperes", "Fravela", "Lothiuture", "Roscollus", "Wmffricus", "Rarif", "Ran", "Lynne",
			"Caravis", "Freth", "Lebee", "Pleri", "Werni", "Reb", "Sbaxun", "Froighe",
			"Cardon", "Friennain", "Lotrissimac", "Roway", "Worth", "Rtak", "Sgor", "Froth",
			"Carrannin", "Frienny", "Lucham", "Runstina", "Wrainne", "Safo", "Snar", "Carin",
			"Casin", "Frietga", "Lugalle", "Rutha", "Wulffre", "Sarda", "Xgemz", "Casye",
			"Carilann", "Frith", "Leffer", "Ploughne", "Wichalish", "Tadri", "Xzeb",
			"Lord Blackthorn", "Mondain", "Conan", "Elric", "Merlin", "King Arthur", "Gandalf",
			"Bilbo", "Frodo", "Elrond", "Aragorn", "Lord British", "Thulsa Doom", "King Osric",
			"Valeria", "Zeddicus", "Rahl", "Venger", "Tiamat", "Aphrodite", "Ares", "Hades",
			"Hermes", "Zeus", "Poseidon", "Elminster", "Drizzt", "Yyrkoon", "Moonglum", "Djeryv",
			"Lord Blackthorn", "Mondain", "Conan", "Elric", "Merlin", "King Arthur", "Gandalf",
			"Bilbo", "Frodo", "Elrond", "Aragorn", "Lord British", "Thulsa Doom", "King Osric",
			"Valeria", "Zeddicus", "Rahl", "Venger", "Tiamat", "Aphrodite", "Ares", "Hades",
			"Hermes", "Zeus", "Poseidon", "Elminster", "Drizzt", "Yyrkoon", "Moonglum", "Djeryv",
			"Lord Blackthorn", "Mondain", "Conan", "Elric", "Merlin", "King Arthur", "Gandalf",
			"Bilbo", "Frodo", "Elrond", "Aragorn", "Lord British", "Thulsa Doom", "King Osric",
			"Valeria", "Zeddicus", "Rahl", "Venger", "Tiamat", "Aphrodite", "Ares", "Hades",
			"Hermes", "Zeus", "Poseidon", "Elminster", "Drizzt", "Yyrkoon", "Moonglum",
			"Agtatt", "Erlitl", "Irennaet", "Tararculc", "Asilnaor", "Denead", "Nonanlohl",
			"Ahadnebh", "Gorriin", "Lohiteir", "Toriah", "Astiib", "Dohalosb", "Nublaar",
			"Ahuhcods", "Helirars", "Ogriut", "Udgial", "Badgeal", "Enehtiln", "Onadaog",
			"Anelraul", "Horihitr", "Radatluor", "Uhatens", "Banideor", "Enolsast", "Ruganish",
			"Anrorc", "Ihatigeag", "Raliradr", "Adutenael", "Decollenb", "Etohaat", "Sacunsabn",
			"Atilcesn", "Ihsuhg", "Recergatl", "Alerrohr", "Direrl", "Galnaag", "Todtuhb",
			"Cunaht", "Ledlaub", "Tahedanb", "Anniut", "Edduhr", "Hanenteon", "Udahoic",
			"Deltagt", "Necnilr", "Tararculc", "Asilnaor", "Ernaan", "Hotoog", "Adaraniin",
			"Deneeg", "Nugancues", "Toriah", "Astiib", "Gatair", "Ihediit", "Adrosc",
			"Endelh", "Nuthoed", "Udgial", "Badgeal", "Henisabt", "Ilecurugg", "Agalihenr",
			"Erasaal", "Odirnuag", "Uhatens", "Banideor", "Ilrelt", "Larahn", "Ageneol",
			"Ergiet", "Raganebs", "Ahudieh", "Decollenb", "Irennaet", "Leluul", "Agluun",
			"Etnond", "Raraan", "Anohirirn", "Direrl", "Lohiteir", "Lerhind", "Agutasihh",
			"Gadalaug", "Relicenh", "Cehlaog", "Edduhr", "Ogriut", "Ohadaer", "Aletaab",
			"Habiot", "Silutiin", "Ecehnien", "Ernaan", "Radatluor", "Reroteuh", "Alsoab",
			"Hirern", "Sohbaln", "Eceluel", "Gatair", "Raliradr", "Adutenael", "Ataneon",
			"Isuteeb", "Tarahisd", "Ecurinaln", "Henisabt", "Recergatl", "Alerrohr", "Caralbenl",
			"Nitebecg", "Tinhaed", "Elaceah", "Ilrelt", "Tahedanb", "Anniut",
			"Ath", "Balmonth", "Baranth", "Bralmuth", "Briarananth", "Bucarth", "Bullinth", "Camalanth",
			"Carmath", "Caroth", "Cath", "Chaneth", "Charanuth", "Chatianth", "Colareth", "Coliath",
			"Craillanth", "Craimath", "Crairenarth", "Duvenath", "Emaleth", "Esianth", "Fith", "Galzieth",
			"Gamiath", "Gatianth", "Gebeth", "Gith", "Giyeth", "Glanarth", "Glath", "Gorianth", "Hallath",
			"Halzanth", "Iacanth", "Iasiluth", "Jemiath", "Jenith", "Jeranth", "Jeranuth", "Jeth", "Laneth",
			"Malanenth", "Mallieth", "Mareninth", "Menianth", "Meranuth", "Meth", "Mikenth", "Mikieth",
			"Mileth", "Mneniath", "Mneth", "Mogonth", "Morenelth", "Moth", "Naranoth", "Nenith", "Palararth",
			"Paloth", "Perinth", "Plath", "Polaneth", "Polarinth", "Polzeth", "Poraneth", "Porenorth",
			"Povarith", "Quessith", "Quevenanth", "Rolieth", "Rossoth", "Roth", "Sagrianth", "Samaloth",
			"Shayenth", "Sholzianth", "Shonniath", "Shoth", "Sidieth", "Spamath", "Spamuth", "Spath", "Speth",
			"Tagmuth", "Tarmuth", "Tiagorth", "Tiakoth", "Tiarath", "Trabieth", "Trananth", "Traranath",
			"Tueth", "Weth", "Wielgianth", "Wieneth", "Wienieth", "Wirmarth", "Lunamon", "Ioneron", "Reanthasala",
			"Tiamar", "Olrion", "Lunorx", "Jonin", "Olthonis", "Taleron", "Abraanthasala", "Lunadine", "Olthas",
			"Tiansa", "Valorx", "Talrion", "Caradine", "Shinikon", "Ti", "Takhansa", "Ollev", "Sirorx", "Abrae",
			"Riisis", "Lauralare", "Darrion", "Juderon", "Tieth", "Ri", "Takhorx", "Raist", "Riorx", "Shinadine",
			"Rias", "Nophean", "Amonter", "Antarahi", "Danyan", "Darangan", "Erantog", "Gandong", "Gioga", "Iguga",
			"Jimondan", "Jioga", "Jirondra", "Loga", "Ngran", "Ptangra", "Ptorag", "Rahiosar", "Ranttira", "Rosegh",
			"Sanyarar", "Sptor", "Varon", "Ytaro", "Ytingahi", "Zigodare", "Zilahed", "Bahiga", "Cora", "Egog",
			"Eragontt", "Gospt", "Hirah", "Hratapru", "Hrontr", "Jiran", "Jisa", "Larange", "Llan", "Ndrant", "Ngat",
			"Ntar", "Odzonte", "Onya", "Pigaheey", "Pimahrao", "Ragugieg", "Thraheg", "Uira", "Ulloda", "Vanyanga",
			"Yttongal", "Abotoram", "Bimo", "Cogo", "Contod", "Conya", "Daralora", "Dzig", "Erot", "Ghilong", "Guig",
			"Imegig", "Iong", "Londod", "Mispra", "Pisa", "Pronga", "Ptorahee", "Randrag", "Tahra", "Tong",
			"Ttilo", "Ugago", "Ugaraga", "Zontt", "Arangama", "Bigama", "Dongio", "Espispto", "Gimiosp", "Ilabimo",
			"Irall", "Irgui", "Jilarara", "Lant", "Lararo", "Largimol", "Mand", "Marab", "Ndron", "Ntorahra", "Onthi",
			"Ragrara", "Santega", "Segiong", "Ugra", "Ulor", "Vama", "Yarameg", "Yttigal", "Rieron", "Olrion",
			"Shinadine", "Chislev", "Caras",
			"Mulpelpe", "Ermohup", "Hehsiel", "Nahehoehsas", "Oratinael", "Eriel", "Pocaspomon", "Sorubiseriel",
			"Aropet", "Zases", "Tpahihuz", "Pael", "Erorasl", "Aknrar", "Ahelas", "Pontael", "Asiel", "Nopoz",
			"Rneenuziel", "Namolon", "Tuep", "Henbolaron", "Zedeson", "Assoaz", "Sotas", "Tadal", "Huslcir",
			"Rutselomiel", "Solael", "Saez", "Bettael", "Ampahoel", "Zatar", "Osaselael", "Irpsan", "Alael",
			"Pdutozab", "Luziel", "Tadon", "Asramel", "Aknoan", "Ahnet", "Unonom", "Xuksetpo", "Pemcapso", "Osapon",
			"Pimunael", "Esulamon", "Tuhkaraip", "Menepruron", "Ranaron", "Allaten", "Asoreb", "Razeniel",
			"Sapanolr", "Ticos", "Ussaxot", "Hahlraz", "Tedrahamael", "Apcbun", "Bahmensu", "Oron", "Xatnpih",
			"Opael", "Ipamorz", "Sanopars", "Exroh", "Lurtapios", "Tocpertaniel", "Islopaar", "Nizlpad", "Umlaboor",
			"Dalisatosiel", "Erammozal", "Amsaset", "Anhozal", "Irlap", "Nour", "Etnoxaad", "Imubenn", "Ezipexon",
			"Sihunosl", "Ehohit", "Zatbuhsatiel", "Sazsutpe", "Hipdiel", "Honed", "Unikesm", "Udazbanoe",
			"Zotipeposael", "Halmaneop", "Semnsat", "Itakup", "Ehhbes", "Araraz", "Halbasoon", "Nahahetael",
			"Ubeezh", "Ilpbeh", "Hilopael", "Esboot", "Usoparb", "Hotesiatrem", "Epnanaet", "Lehael", "Lapael",
			"Urapes", "Obalasc", "Pandael", "Damaz", "Ehnnat", "Pnecamob", "Ethahoat", "Kuhretieh", "Ekanzapis",
			"Pundohien", "Honolens", "Ahtuxies", "Asidodiel", "Laripael", "Bortarsariel", "Hirular", "Dunaneboriel",
			"Ensadaap", "Usraat", "Hahuhnerael", "Bolenoz", "Larhepeis", "Irasteheh", "Rarahaimzah", "Alahotael",
			"Litedabh", "Lasnuhtasael", "Unamah", "Topriraiz", "Rsaset", "Ansaap", "Opoasl", "Hetet", "Sotsopsehiel",
			"Kopozel", "Xartatesael", "Amosun", "Perensahael", "Chatomep", "Adiarh", "Loos", "Hador", "Sbeihapiel",
			"Odipin", "Tramater", "Mephotasael", "Mozed", "Depar", "Taron", "Anaobm", "Larpusmason", "Essaheah",
			"Suel", "Sahaminapiel", "Dinatoh", "Anrac", "Rasuniolpas", "Isoteciel", "Atcis", "Hatazeh", "Anhoor",
			"Samnerra", "Snilot", "Esasitm", "Arhel", "Rkesitas", "Hexpemsazon", "Izatap", "Ezon", "Koit", "Obasahiel",
			"Cerneplihael", "Husmled", "Hisipon", "Anolahon", "Ppaironael", "Lanetsosiel", "Olon", "Arpzih",
			"Ekarnahox", "Ibmnat", "Ipon", "Sutlanasael", "Srosirad", "Apomael", "Asemet", "Uknatias", "Nohon",
			"Maholab", "Sacuhatakael", "Orpir", "Bessipcopiel", "Isnal", "Elxar",
			"Aetrtevikr", "Airnucaha", "Alesei", "Amaniuevia", "Anendese", "Anentend", "Aneseicon", "Anonish",
			"Ardelica", "Argaluse", "Astharn", "Astiule", "Avacen", "Baliere", "Balucic", "Bardo", "Beserdei", "Bravamos",
			"Brliamide", "Cacosa", "Caleoleb", "Candrnial", "Cebalel", "Celele", "Chareviu", "Chenus", "Civon", "Danani",
			"Dandelene", "Davismarir", "Delel", "Deliusa", "Desalie", "Dethebe", "Doreti", "Drcatrda", "Dricor", "Ebali",
			"Ebelelu", "Einuce", "Elenenga", "Elestrn", "Elianam", "Elustule", "Ercikrl", "Erdor", "Erelucende", "Esanae",
			"Eseishan", "Esetucae", "Etralan", "Ezanelar", "Febrth", "Felele", "Felerelia", "Felidrane", "Femiard",
			"Fentrdes", "Feost", "Festhar", "Fezan", "Galelile", "Ganacas", "Gardet", "Gargda", "Garoneva", "Gdanue",
			"Gdestia", "Gdonisat", "Gdridordro", "Gebriustr", "Genebe", "Genievel", "Getich", "Gileli", "Hacelu", "Hachie",
			"Haleshi", "Hamazalesa", "Hangde", "Haret", "Havarl", "Helica", "Helichas", "Hetal", "Hianar", "Hicielen",
			"Hinasa", "Holusel", "Hononus", "Ialenanete", "Ianenel", "Ianthian", "Icamalius", "Ienag", "Ienive", "Ikriena",
			"Ilialerl", "Iuceosani", "Iulen", "Jercos", "Jucanin", "Juleva", "Juretri", "Jusara", "Jusaranus", "Justhieole",
			"Kraneb", "Kranie", "Krdrethag", "Kreoles", "Krevia", "Krgdei", "Krgdr", "Kriaster", "Krirni", "Krleleni",
			"Krlenagia", "Krneleba", "Krneleneb", "Lananar", "Lartius", "Laselilaza", "Lebrop", "Lelusaror", "Leorn",
			"Leramaman", "Letrtesend", "Libelem", "Liesm", "Lurneseler", "Luson", "Mabanteno", "Macaralen", "Maneleli",
			"Mantha", "Micon", "Miesth", "Miuli", "Moshardosh", "Nanar", "Neleiuc", "Ntholich", "Oleluc", "Olerusa", "Olusar",
			"Onanist", "Onaret", "Onetial", "Ongdastho", "Onicerus", "Orisel", "Orler", "Orosal", "Osaev", "Osazarna", "Phaletesma",
			"Phard", "Pheni", "Phesel", "Phesor", "Phienuebeb", "Phonu", "Ralus", "Rcanoluele", "Rcivamah", "Rdenaro", "Rdeth",
			"Relelete", "Rnara", "Rores", "Rorngane", "Saiabelel", "Sameti", "Santise", "Sareva", "Sasai", "Serantivet",
			"Sezarnti", "Smanda", "Smelieli", "Smetenelul", "Stiulenet", "Taleluso", "Tenda", "Tharanasan", "Thararin",
			"Thialardrd", "Tiusa", "Ucalelen", "Uchenter", "Uetrdrd", "Ulelen", "Ulerosh", "Uleseles", "Urndern", "Usant",
			"Usavamam", "Usonicar", "Usoris", "Vamorchine", "Vanebr", "Vargatu", "Vatielucac", "Velurelu", "Venez", "Vicav",
			"Viust", "Vondr", "Vorntes", "Vosthel", "Xacagalela", "Xaiel", "Xanismis", "Xarlic", "Xarnieta", "Zalebesan",
			"Zalemeta", "Zamint", "Zanabal", "Zanamai", "Zandoni", "Zaraluc", "Zaror", "Zatesor"
		};

		///// CONFIGURE THE FIRST PART OF A SPELL NAME HERE /////////////////////////////////////////////////////
		public static string[] Items5 { get { return m_Items5; } }

		private static string[] m_Items5 = new string[]
		{
			"Clyz", "Achug", "Theram", "Quale", "Lutin", "Gad",
			"Croeq", "Achund", "Therrisi", "Qualorm", "Lyeit", "Garaso",
			"Crul", "Ackhine", "Thritai", "Quaso", "Lyetonu", "Garck",
			"Cuina", "Ackult", "Tig", "Quealt", "Moin", "Garund",
			"Daror", "Aeny", "Tinalt", "Rador", "Moragh", "Ghagha",
			"Deet", "Aeru", "Tinkima", "Rakeld", "Morir", "Ghatas",
			"Deldrad", "Ageick", "Tinut", "Rancwor", "Morosy", "Gosul",
			"Deldrae", "Agemor", "Tonk", "Ranildu", "Mosat", "Hatalt",
			"Delz", "Aghai", "Tonolde", "Ranot", "Mosd", "Hatash",
			"Denad", "Ahiny", "Tonper", "Ranper", "Mosrt", "Hatque",
			"Denold", "Aldkely", "Torint", "Ransayi", "Mosyl", "Hatskel",
			"Denyl", "Aleler", "Trooph", "Ranzmor", "Moszight", "Hattia",
			"Drahono", "Anagh", "Turbelm", "Raydan", "Naldely", "Hiert",
			"Draold", "Anclor", "Uighta", "Rayxwor", "Nalusk", "Hinalde",
			"Dynal", "Anl", "Uinga", "Rhit", "Nalwar", "Hinall",
			"Dyndray", "Antack", "Umnt", "Risormy", "Nas", "Hindend",
			"Eacki", "Ardburo", "Undaughe", "Risshy", "Nat", "Iade",
			"Earda", "Ardmose", "Untdran", "Rodiz", "Nator", "Iaper",
			"Echal", "Ardurne", "Untld", "Rodkali", "Nayth", "Iass",
			"Echind", "Ardyn", "Uoso", "Rodrado", "Neil", "Iawy",
			"Echwaro", "Ashaugha", "Urnroth", "Roort", "Nenal", "Iechi",
			"Eeni", "Ashdend", "Urode", "Ruina", "New", "Ightult",
			"Einea", "Ashye", "Uskdar", "Rynm", "Nia", "Ildaw",
			"Eldsera", "Asim", "Uskmdan", "Rynryna", "Nikim", "Ildoq",
			"Eldwen", "Athdra", "Usksough", "Ryns", "Nof", "Inabel",
			"Eldyril", "Athskel", "Usktoro", "Rynut", "Nook", "Inaony",
			"Elmkach", "Atkin", "Ustagee", "Samgha", "Nybage", "Inease",
			"Elmll", "Aughint", "Ustld", "Samnche", "Nyiy", "Ineegh",
			"Emath", "Aughthere", "Ustton", "Samssam", "Nyseld", "Ineiti",
			"Emengi", "Avery", "Verporm", "Sawor", "Nysklye", "Ineun",
			"Emild", "Awch", "Vesrade", "Sayimo", "Nyw", "Ingr",
			"Emmend", "Banend", "Voraughe", "Sayn", "Oasho", "Isbaugh",
			"Emnden", "Beac", "Vorril", "Sayskelu", "Oendy", "Islyei",
			"Endvelm", "Belan", "Vorunt", "Scheach", "Oenthi", "Issy",
			"Endych", "Beloz", "Whedan", "Scheyer", "Ohato", "Istin",
			"Engeh", "Beltiai", "Whisam", "Serat", "Oldack", "Iumo",
			"Engen", "Bliorm", "Whok", "Sernd", "Oldar", "Jyhin",
			"Engh", "Burold", "Worath", "Skell", "Oldr", "Jyon",
			"Engraki", "Buror", "Worav", "Skelser", "Oldtar", "Kalov",
			"Engroth", "Byt", "Worina", "Slim", "Omdser", "Kelol",
			"Engum", "Cakal", "Worryno", "Snaest", "Ond", "Kinser",
			"Enhech", "Carr", "Worunty", "Sniund", "Oron", "Koor",
			"Enina", "Cayld", "Worwaw", "Sosam", "Orrbel", "Lear",
			"Enk", "Cerar", "Yary", "Stayl", "Osnt", "Leert",
			"Enlald", "Cerl", "Yawi", "Stol", "Peright", "Legar",
			"Enskele", "Cerv", "Yena", "Strever", "Perpban", "Lerev",
			"Eoru", "Chaur", "Yero", "Swaih", "Phiunt", "Lerzshy",
			"Ernysi", "Chayn", "Yerrves", "Tagar", "Poll", "Llash",
			"Erque", "Cheimo", "Yhone", "Taienn", "Polrad", "Llotor",
			"Errusk", "Chekim", "Yradi", "Taiyild", "Polsera", "Loem",
			"Ervory", "Chreusk", "Zhugar", "Tanen", "Puon", "Loing",
			"Essisi", "Chrir", "Zirt", "Tasaf", "Quaev", "Lorelmo",
			"Essnd", "Chroelt", "Zoine", "Tasrr", "Quahang", "Lorud",
			"Estech", "Cloran", "Zotin", "Thaeng", "Qual", "Lour",
			"Estkunt", "Etoth", "Esule", "Estnight"
		};

		///// CONFIGURE THE SECOND PART OF A SPELL NAME HERE /////////////////////////////////////////////////////
		public static string[] Items6 { get { return m_Items6; } }

		private static string[] m_Items6 = new string[]
		{
			"Acidic", "Summoning", "Scrying", "Obscure", "Iron", "Ghoulish", "Enfeebling",
			"Altered", "Secret", "Obscuring", "Irresistible", "Gibbering", "Enlarged", "Confusing",
			"Analyzing", "Sympathetic", "Secure", "Permanent", "Keen", "Glittering", "Ethereal", "Contacting",
			"Animal", "Telekinetic", "Seeming", "Persistent", "Lawful", "Evil", "Continual",
			"Animated", "Telepathic", "Shadow", "Phantasmal", "Legendary", "Good", "Expeditious", "Control",
			"Antimagic", "Teleporting", "Shattering", "Phantom", "Lesser", "Grasping", "Explosive", "Crushing",
			"Arcane", "Temporal", "Shocking", "Phasing", "Levitating", "Greater", "Fabricated", "Cursed",
			"Articulated", "Tiny", "Shouting", "Planar", "Limited", "Guarding", "Faithful", "Dancing",
			"Binding", "Transmuting", "Shrinking", "Poisonous", "Lucubrating", "Fearful", "Dazzling",
			"Black", "Undead", "Silent", "Polymorphing", "Magical", "Hallucinatory", "Delayed",
			"Blinding", "Undetectable", "Slow", "Prismatic", "Magnificent", "Hideous", "Fire", "Demanding",
			"Blinking", "Unseen", "Solid", "Programmed", "Major", "Holding", "Flaming", "Dimensional",
			"Vampiric", "Soul", "Projected", "Mass", "Horrid", "Discern",
			"Burning", "Vanishing", "Spectral", "Mending", "Hypnotic", "Floating", "Disintegrating",
			"Cat", "Protective", "Mind", "Ice", "Flying", "Disruptive",
			"Chain", "Spidery", "Prying", "Minor", "Illusionary", "Force", "Dominating",
			"Changing", "Warding", "Stinking", "Pyrotechnic", "Mirrored", "Improved", "Forceful", "Dreaming",
			"Chaotic", "Water", "Stone", "Rainbow", "Misdirected", "Incendiary", "Freezing", "Elemental",
			"Charming", "Watery", "Misleading", "Instant", "Gaseous", "Emotional",
			"Chilling", "Weird", "Storming", "Resilient", "Mnemonic", "Interposing", "Gentle", "Enduring",
			"Whispering", "Suggestive", "Reverse", "Moving", "Invisible", "Ghostly", "Energy",
			"Clenched", "Climbing", "Comprehending", "Colorful", "True", "False"
		};

		///// CONFIGURE THE THIRD PART OF A SPELL NAME HERE /////////////////////////////////////////////////////
		public static string[] Items7 { get { return m_Items7; } }

		private static string[] m_Items7 = new string[]
		{
			"Acid", "Tentacles", "Sigil", "Plane", "Legend", "Gravity", "Emotion", "Chest",
			"Alarm", "Terrain", "Simulacrum", "Poison", "Lightning", "Grease", "Endurance", "Circle",
			"Anchor", "Thoughts", "Skin", "Polymorph", "Lights", "Growth", "Enervation", "Clairvoyance",
			"Animal", "Time", "Sleep", "Prestidigitation", "Location", "Guards", "Enfeeblement", "Clone",
			"Antipathy", "Tongues", "Soul", "Projection", "Lock", "Hand", "Enhancer", "Cloud",
			"Arcana", "Touch", "Sound", "Pyrotechnics", "Lore", "Haste", "Etherealness", "Cold",
			"Armor", "Transformation", "Spells", "Refuge", "Lucubration", "Hat", "Evil", "Color",
			"Arrows", "Trap", "Sphere", "Repulsion", "Magic", "Hound", "Evocation", "Confusion",
			"Aura", "Trick", "Spider", "Resistance", "Mansion", "Hypnotism", "Eye", "Conjuration",
			"Banishment", "Turning", "Spray", "Retreat", "Mask", "Ice", "Fall", "Contagion",
			"Banshee", "Undead", "Stasis", "Rope", "Maze", "Image", "Fear", "Creation",
			"Bear", "Vanish", "Statue", "Runes", "Message", "Imprisonment", "Feather", "Curse",
			"Binding", "Veil", "Steed", "Scare", "Meteor", "Insanity", "Field", "Dance",
			"Vision", "Stone", "Screen", "Mind", "Invisibility", "Fireball", "Darkness",
			"Blindness", "Vocation", "Storm", "Script", "Mirage", "Invulnerability", "Flame", "Daylight",
			"Blink", "Wail", "Strength", "Scrying", "Misdirection", "Iron", "Flesh", "Dead",
			"Blur", "Walk", "Strike", "Seeing", "Missile", "Item", "Fog", "Deafness",
			"Body", "Wall", "Stun", "Self", "Mist", "Force", "Death",
			"Bolt", "Wards", "Suggestion", "Sending", "Monster", "Jaunt", "Foresight", "Demand",
			"Bond", "Water", "Summons", "Servant", "Mouth", "Jump", "Form", "Disjunction",
			"Breathing", "Weapon", "Sunburst", "Shadow", "Mud", "Slay", "Freedom", "Disk",
			"Burning", "Weather", "Swarm", "Shape", "Nightmare", "Slayer", "Frost", "Dismissal",
			"Cage", "Web", "Symbol", "Shelter", "Object", "Knock", "Gate", "Displacement",
			"Chain", "Wilting", "Sympathy", "Shield", "Page", "Languages", "Good", "Door",
			"Chaos", "Wind", "Telekinesis", "Shift", "Pattern", "Laughter", "Grace", "Drain",
			"Charm", "Wish", "Teleport", "Shout", "Person", "Law", "Grasp", "Dream",
			"Elements", "Edge", "Earth", "Dust"
		};

		// m_Places1 = LEVEL 1 AREA
		// m_Places2 = LEVEL 2 AREA
		// m_Places3 = LEVEL 3 AREA
		// m_Places4 = LEVEL 4 AREA
		// m_Places5 = LEVEL 5 AREA
		// m_Places6 = LEVEL 6 AREA
		// m_Monster1 = LEVEL 1 MONSTER
		// m_Monster2 = LEVEL 2 MONSTER
		// m_Monster3 = LEVEL 3 MONSTER
		// m_Monster4 = LEVEL 4 MONSTER
		// m_Monster5 = LEVEL 5 MONSTER
		// m_Monster6 = LEVEL 6 MONSTER
		// m_Items1 = ITEM NAME
		// m_Items2 = ENDING ADJECTIVES
		// m_Items3 = STARTING ADJECTIVES
		// m_Items4 = ITEM OWNERS
		// m_Items5 = SPELL NAME - 1ST
		// m_Items6 = SPELL NAME - 2ND
		// m_Items7 = SPELL NAME - 3RD

		public int mNNeed = 0;			// HOW MANY ARE NEEDED
		public int mNGot = 0;			// HOW MANY THE PLAYER GOT
		public int mNChance = 0;		// CHANCE TO GET WHAT IS NEEDED
		public int mNType = 0;			// TYPE OF QUEST 1=SLAY 2=SEEK
		public int mNLevel = 0;			// LEVEL
		public string mNItemName = "";		// NAME OF ITEM NEEDED
		public string mNMonsterType = "";	// TYPE OF MONSTER THAT NEEDS TO BE KILLED
		public string mNLocation = "";		// NAME OF DUNGEON THAT HAS THE ITEM
		public string mNStory = "";		// A SHORT STORY OF THE ITEM

		[CommandProperty(AccessLevel.GameMaster)]
		public int NNeed { get { return mNNeed; } set { mNNeed = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public int NGot { get { return mNGot; } set { mNGot = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public int NChance { get { return mNChance; } set { mNChance = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public int NType { get { return mNType; } set { mNType = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public int NLevel { get { return mNLevel; } set { mNLevel = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public string NItemName { get { return mNItemName; } set { mNItemName = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public string NMonsterType { get { return mNMonsterType; } set { mNMonsterType = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public string NLocation { get { return mNLocation; } set { mNLocation = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public string NStory { get { return mNStory; } set { mNStory = value; } }

		[Constructable]
		public QuestScroll( int level ) : base( 5360 )
		{
			//LootType = LootType.Blessed;
			Hue = 511;

			NType = Utility.RandomMinMax( 1, 2 ); // TYPE OF QUEST

			if ( level > 0 )
			{
				NLevel = level; // QUEST LEVEL

				int tName = Utility.RandomMinMax( 1, 100 );

				if ( tName > 90 )
				{
					NItemName = m_Items5[Utility.Random(m_Items5.Length)] + "'s Scroll of " + m_Items6[Utility.Random(m_Items6.Length)] + " " + m_Items7[Utility.Random(m_Items7.Length)];
				}
				else if ( tName > 45 )
				{
					NItemName = "the " + m_Items3[Utility.Random(m_Items3.Length)] + " " + m_Items1[Utility.Random(m_Items1.Length)] + " of " + m_Items4[Utility.Random(m_Items4.Length)];
				}
				else
				{
					NItemName = "the " + m_Items1[Utility.Random(m_Items1.Length)] + " of " + m_Items2[Utility.Random(m_Items2.Length)];
				}
			}

			if ( level == 1 )
			{
				NLocation = m_Places1[Utility.Random(m_Places1.Length)];
				NMonsterType = m_Monster1[Utility.Random(m_Monster1.Length)];
				NChance = Utility.RandomMinMax( 5, 30 );
				NNeed = Utility.RandomMinMax( 1, 25 );
			}

			else if ( level == 2 )
			{
				NLocation = m_Places2[Utility.Random(m_Places2.Length)];
				NMonsterType = m_Monster2[Utility.Random(m_Monster2.Length)];
				NChance = Utility.RandomMinMax( 10, 35 );
				NNeed = Utility.RandomMinMax( 1, 20 );
			}

			else if ( level == 3 )
			{
				NLocation = m_Places3[Utility.Random(m_Places3.Length)];
				NMonsterType = m_Monster3[Utility.Random(m_Monster3.Length)];
				NChance = Utility.RandomMinMax( 15, 40 );
				NNeed = Utility.RandomMinMax( 1, 15 );
			}

			else if ( level == 4 )
			{
				NLocation = m_Places4[Utility.Random(m_Places4.Length)];
				NMonsterType = m_Monster4[Utility.Random(m_Monster4.Length)];
				NChance = Utility.RandomMinMax( 20, 45 );
				NNeed = Utility.RandomMinMax( 1, 10 );
			}

			else if ( level == 5 )
			{
				NLocation = m_Places5[Utility.Random(m_Places5.Length)];
				NMonsterType = m_Monster5[Utility.Random(m_Monster5.Length)];
				NChance = Utility.RandomMinMax( 25, 55 );
				NNeed = Utility.RandomMinMax( 1, 5 );
			}

			else if ( level == 6 )
			{
				NLocation = m_Places6[Utility.Random(m_Places6.Length)];
				NMonsterType = m_Monster6[Utility.Random(m_Monster6.Length)];
				NChance = Utility.RandomMinMax( 30, 60 );
				NNeed = 1;
			}

			if ( NType == 1 )
			{
				Name = "Slay a " + NMonsterType + " (" + NGot.ToString() + " of " + NNeed.ToString() + ")";

				string sPerson = "";

				if ( Utility.RandomMinMax( 1, 2 ) == 1 )
				{
					sPerson = NameList.RandomName( "female" );
				}
				else
				{
					sPerson = NameList.RandomName( "male" );
				}

				if ( Utility.RandomMinMax( 1, 3 ) > 1 )
				{
					NStory = sPerson + " " + m_Story2[Utility.Random(m_Story2.Length)];
				}
				else
				{
					NStory = m_Story3[Utility.Random(m_Story3.Length)] + " " + sPerson;
				}
			}
			else
			{
				NNeed = 1;
				Name = "Seek " + NItemName;
				NStory = m_Story1[Utility.Random(m_Story1.Length)] + " " + NLocation;
			}
		}

		public QuestScroll( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( NGot >= NNeed )
			{
				from.SendMessage( "This quest is already complete!" );
			}
			else if ( NType == 1 )
			{
				from.SendMessage( "Target the corpse you wish to claim credit for." );
				from.SendMessage( "The corpse will vanish once credit is claimed." );
				from.Target = new CorpseTarget( this );
			}
			else
			{
				from.SendMessage( "Target the corpse you wish to search." );
				from.SendMessage( "The corpse will vanish once it is searched." );
				from.Target = new CorpseTarget( this );
			}
		}

		private class CorpseTarget : Target
		{
			private QuestScroll m_Quest;

			public CorpseTarget( QuestScroll quest ) : base( 3, false, TargetFlags.None )
			{
				m_Quest = quest;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Quest.Deleted )
					return;

				if (!(targeted is Corpse))
				{
					from.SendLocalizedMessage( 1042600 ); // That is not a corpse!
				}
				else if ( !m_Quest.IsChildOf( from.Backpack ) )
				{
					from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				}
				else
				{
					object obj = targeted;

					Corpse c = (Corpse)targeted;

					Type M_Type = typeof(Orc);

					int nSpot = 0;



					// SECTION - MNTP1 ////////////////////////////////////////////////////////////////////////

					// THIS SECTION FINDS THE TYPES OF MONSTERS...SINCE I CANNOT SEEM TO CONVERT THE NAMES

					if ( m_Quest.NMonsterType == "Bull Frog" ) { M_Type = typeof(BullFrog); }
					else if ( m_Quest.NMonsterType == "Giant Rat" ) { M_Type = typeof(GiantRat); }
					else if ( m_Quest.NMonsterType == "Lizardman" ) { M_Type = typeof(Lizardman); }
					else if ( m_Quest.NMonsterType == "Mongbat" ) { M_Type = typeof(Mongbat); }
					else if ( m_Quest.NMonsterType == "Orc" ) { M_Type = typeof(Orc); }
					else if ( m_Quest.NMonsterType == "Ratman" ) { M_Type = typeof(Ratman); }
					else if ( m_Quest.NMonsterType == "Sewer Rat" ) { M_Type = typeof(Sewerrat); }
					else if ( m_Quest.NMonsterType == "Skeleton" ) { M_Type = typeof(Skeleton); }
					else if ( m_Quest.NMonsterType == "Slime" ) { M_Type = typeof(Slime); }
					else if ( m_Quest.NMonsterType == "Zombie" ) { M_Type = typeof(Zombie); }
					else if ( m_Quest.NMonsterType == "Brigand" ) { M_Type = typeof(Brigand); }
					else if ( m_Quest.NMonsterType == "Earth Elemental" ) { M_Type = typeof(EarthElemental); }
					else if ( m_Quest.NMonsterType == "Ettin" ) { M_Type = typeof(Ettin); }
					else if ( m_Quest.NMonsterType == "Gazer Larva" ) { M_Type = typeof(GazerLarva); }
					else if ( m_Quest.NMonsterType == "Ghoul" ) { M_Type = typeof(Ghoul); }
					else if ( m_Quest.NMonsterType == "Giant Spider" ) { M_Type = typeof(GiantSpider); }
					else if ( m_Quest.NMonsterType == "Giant Toad" ) { M_Type = typeof(GiantToad); }
					else if ( m_Quest.NMonsterType == "Harpy" ) { M_Type = typeof(Harpy); }
					else if ( m_Quest.NMonsterType == "Headless One" ) { M_Type = typeof(HeadlessOne); }
					else if ( m_Quest.NMonsterType == "Orc Bomber" ) { M_Type = typeof(OrcBomber); }
					else if ( m_Quest.NMonsterType == "Scorpion" ) { M_Type = typeof(Scorpion); }
					else if ( m_Quest.NMonsterType == "Shade" ) { M_Type = typeof(Shade); }
					else if ( m_Quest.NMonsterType == "Spectre" ) { M_Type = typeof(Spectre); }
					else if ( m_Quest.NMonsterType == "Wraith" ) { M_Type = typeof(Wraith); }
					else if ( m_Quest.NMonsterType == "Agapite Elemental" ) { M_Type = typeof(AgapiteElemental); }
					else if ( m_Quest.NMonsterType == "Air Elemental" ) { M_Type = typeof(AirElemental); }
					else if ( m_Quest.NMonsterType == "Ant Lion" ) { M_Type = typeof(AntLion); }
					else if ( m_Quest.NMonsterType == "Beetle" ) { M_Type = typeof(Beetle); }
					else if ( m_Quest.NMonsterType == "Bogle" ) { M_Type = typeof(Bogle); }
					else if ( m_Quest.NMonsterType == "Bogling" ) { M_Type = typeof(Bogling); }
					else if ( m_Quest.NMonsterType == "Bone Knight" ) { M_Type = typeof(BoneKnight); }
					else if ( m_Quest.NMonsterType == "Bone Magi" ) { M_Type = typeof(BoneMagi); }
					else if ( m_Quest.NMonsterType == "Bronze Elemental" ) { M_Type = typeof(BronzeElemental); }
					else if ( m_Quest.NMonsterType == "Copper Elemental" ) { M_Type = typeof(CopperElemental); }
					else if ( m_Quest.NMonsterType == "Corpser" ) { M_Type = typeof(Corpser); }
					else if ( m_Quest.NMonsterType == "Crystal Elemental" ) { M_Type = typeof(CrystalElemental); }
					else if ( m_Quest.NMonsterType == "Cyclops" ) { M_Type = typeof(Cyclops); }
					else if ( m_Quest.NMonsterType == "Deathwatch Beetle" ) { M_Type = typeof(DeathwatchBeetle); }
					else if ( m_Quest.NMonsterType == "Deathwatch Beetle Hatchling" ) { M_Type = typeof(DeathwatchBeetleHatchling); }
					else if ( m_Quest.NMonsterType == "Dull Copper Elemental" ) { M_Type = typeof(DullCopperElemental); }
					else if ( m_Quest.NMonsterType == "Fire Beetle" ) { M_Type = typeof(FireBeetle); }
					else if ( m_Quest.NMonsterType == "Fire Elemental" ) { M_Type = typeof(FireElemental); }
					else if ( m_Quest.NMonsterType == "Flesh Golem" ) { M_Type = typeof(FleshGolem); }
					else if ( m_Quest.NMonsterType == "Frost Ooze" ) { M_Type = typeof(FrostOoze); }
					else if ( m_Quest.NMonsterType == "Frost Spider" ) { M_Type = typeof(FrostSpider); }
					else if ( m_Quest.NMonsterType == "Frost Troll" ) { M_Type = typeof(FrostTroll); }
					else if ( m_Quest.NMonsterType == "Gargoyle" ) { M_Type = typeof(Gargoyle); }
					else if ( m_Quest.NMonsterType == "Gazer" ) { M_Type = typeof(Gazer); }
					else if ( m_Quest.NMonsterType == "Giant Serpent" ) { M_Type = typeof(GiantSerpent); }
					else if ( m_Quest.NMonsterType == "Golden Elemental" ) { M_Type = typeof(GoldenElemental); }
					else if ( m_Quest.NMonsterType == "Gore Fiend" ) { M_Type = typeof(GoreFiend); }
					else if ( m_Quest.NMonsterType == "Hell Hound" ) { M_Type = typeof(HellHound); }
					else if ( m_Quest.NMonsterType == "Ice Elemental" ) { M_Type = typeof(IceElemental); }
					else if ( m_Quest.NMonsterType == "Ice Snake" ) { M_Type = typeof(IceSnake); }
					else if ( m_Quest.NMonsterType == "Imp" ) { M_Type = typeof(Imp); }
					else if ( m_Quest.NMonsterType == "Lava Snake" ) { M_Type = typeof(LavaSnake); }
					else if ( m_Quest.NMonsterType == "Minotaur" ) { M_Type = typeof(Minotaur); }
					else if ( m_Quest.NMonsterType == "Ogre" ) { M_Type = typeof(Ogre); }
					else if ( m_Quest.NMonsterType == "Ophidian Knight" ) { M_Type = typeof(OphidianKnight); }
					else if ( m_Quest.NMonsterType == "Ophidian Warrior" ) { M_Type = typeof(OphidianWarrior); }
					else if ( m_Quest.NMonsterType == "Orc Brute" ) { M_Type = typeof(OrcBrute); }
					else if ( m_Quest.NMonsterType == "Orc Captain" ) { M_Type = typeof(OrcCaptain); }
					else if ( m_Quest.NMonsterType == "Orcish Lord" ) { M_Type = typeof(OrcishLord); }
					else if ( m_Quest.NMonsterType == "Orcish Mage" ) { M_Type = typeof(OrcishMage); }
					else if ( m_Quest.NMonsterType == "Patchwork Skeleton" ) { M_Type = typeof(PatchworkSkeleton); }
					else if ( m_Quest.NMonsterType == "Ratman Archer" ) { M_Type = typeof(RatmanArcher); }
					else if ( m_Quest.NMonsterType == "Ratman Mage" ) { M_Type = typeof(RatmanMage); }
					else if ( m_Quest.NMonsterType == "Ridgeback" ) { M_Type = typeof(Ridgeback); }
					else if ( m_Quest.NMonsterType == "Sand Vortex" ) { M_Type = typeof(SandVortex); }
					else if ( m_Quest.NMonsterType == "Savage" ) { M_Type = typeof(Savage); }
					else if ( m_Quest.NMonsterType == "Savage Rider" ) { M_Type = typeof(SavageRider); }
					else if ( m_Quest.NMonsterType == "Savage Shaman" ) { M_Type = typeof(SavageShaman); }
					else if ( m_Quest.NMonsterType == "Shadow Iron Elemental" ) { M_Type = typeof(ShadowIronElemental); }
					else if ( m_Quest.NMonsterType == "Skeletal Knight" ) { M_Type = typeof(SkeletalKnight); }
					else if ( m_Quest.NMonsterType == "Skeletal Mage" ) { M_Type = typeof(SkeletalMage); }
					else if ( m_Quest.NMonsterType == "Snow Elemental" ) { M_Type = typeof(SnowElemental); }
					else if ( m_Quest.NMonsterType == "Stone Gargoyle" ) { M_Type = typeof(StoneGargoyle); }
					else if ( m_Quest.NMonsterType == "Stone Harpy" ) { M_Type = typeof(StoneHarpy); }
					else if ( m_Quest.NMonsterType == "Terathan Drone" ) { M_Type = typeof(TerathanDrone); }
					else if ( m_Quest.NMonsterType == "Terathan Warrior" ) { M_Type = typeof(TerathanWarrior); }
					else if ( m_Quest.NMonsterType == "Troll" ) { M_Type = typeof(Troll); }
					else if ( m_Quest.NMonsterType == "Valorite Elemental" ) { M_Type = typeof(ValoriteElemental); }
					else if ( m_Quest.NMonsterType == "Vampire Bat" ) { M_Type = typeof(VampireBat); }
					else if ( m_Quest.NMonsterType == "Verite Elemental" ) { M_Type = typeof(VeriteElemental); }
					else if ( m_Quest.NMonsterType == "Wailing Banshee" ) { M_Type = typeof(WailingBanshee); }
					else if ( m_Quest.NMonsterType == "Water Elemental" ) { M_Type = typeof(WaterElemental); }
					else if ( m_Quest.NMonsterType == "Bog Thing" ) { M_Type = typeof(BogThing); }
					else if ( m_Quest.NMonsterType == "Centaur" ) { M_Type = typeof(Centaur); }
					else if ( m_Quest.NMonsterType == "Drake" ) { M_Type = typeof(Drake); }
					else if ( m_Quest.NMonsterType == "Dread Spider" ) { M_Type = typeof(DreadSpider); }
					else if ( m_Quest.NMonsterType == "Elite Ninja" ) { M_Type = typeof(EliteNinja); }
					else if ( m_Quest.NMonsterType == "Evil Mage" ) { M_Type = typeof(EvilMage); }
					else if ( m_Quest.NMonsterType == "Exodus Minion" ) { M_Type = typeof(ExodusMinion); }
					else if ( m_Quest.NMonsterType == "Exodus Overseer" ) { M_Type = typeof(ExodusOverseer); }
					else if ( m_Quest.NMonsterType == "Fan Dancer" ) { M_Type = typeof(FanDancer); }
					else if ( m_Quest.NMonsterType == "Fire Gargoyle" ) { M_Type = typeof(FireGargoyle); }
					else if ( m_Quest.NMonsterType == "Gargoyle Destroyer" ) { M_Type = typeof(GargoyleDestroyer); }
					else if ( m_Quest.NMonsterType == "Gargoyle Enforcer" ) { M_Type = typeof(GargoyleEnforcer); }
					else if ( m_Quest.NMonsterType == "Giant Black Widow" ) { M_Type = typeof(GiantBlackWidow); }
					else if ( m_Quest.NMonsterType == "Golem" ) { M_Type = typeof(Golem); }
					else if ( m_Quest.NMonsterType == "Golem Controller" ) { M_Type = typeof(GolemController); }
					else if ( m_Quest.NMonsterType == "Ice Serpent" ) { M_Type = typeof(IceSerpent); }
					else if ( m_Quest.NMonsterType == "Lava Lizard" ) { M_Type = typeof(LavaLizard); }
					else if ( m_Quest.NMonsterType == "Lava Serpent" ) { M_Type = typeof(LavaSerpent); }
					else if ( m_Quest.NMonsterType == "Lich" ) { M_Type = typeof(Lich); }
					else if ( m_Quest.NMonsterType == "Minotaur Scout" ) { M_Type = typeof(MinotaurScout); }
					else if ( m_Quest.NMonsterType == "Mummy" ) { M_Type = typeof(Mummy); }
					else if ( m_Quest.NMonsterType == "Ogre Lord" ) { M_Type = typeof(OgreLord); }
					else if ( m_Quest.NMonsterType == "Ophidian Archmage" ) { M_Type = typeof(OphidianArchmage); }
					else if ( m_Quest.NMonsterType == "Ophidian Mage" ) { M_Type = typeof(OphidianMage); }
					else if ( m_Quest.NMonsterType == "Ophidian Matriarch" ) { M_Type = typeof(OphidianMatriarch); }
					else if ( m_Quest.NMonsterType == "Phoenix" ) { M_Type = typeof(Phoenix); }
					else if ( m_Quest.NMonsterType == "Plague Spawn" ) { M_Type = typeof(PlagueSpawn); }
					else if ( m_Quest.NMonsterType == "Quagmire" ) { M_Type = typeof(Quagmire); }
					else if ( m_Quest.NMonsterType == "Restless Soul" ) { M_Type = typeof(RestlessSoul); }
					else if ( m_Quest.NMonsterType == "Ronin" ) { M_Type = typeof(Ronin); }
					else if ( m_Quest.NMonsterType == "Rune Beetle" ) { M_Type = typeof(RuneBeetle); }
					else if ( m_Quest.NMonsterType == "Sea Serpent" ) { M_Type = typeof(SeaSerpent); }
					else if ( m_Quest.NMonsterType == "Shadow Fiend" ) { M_Type = typeof(ShadowFiend); }
					else if ( m_Quest.NMonsterType == "Swamp Dragon" ) { M_Type = typeof(SwampDragon); }
					else if ( m_Quest.NMonsterType == "Swamp Tentacle" ) { M_Type = typeof(SwampTentacle); }
					else if ( m_Quest.NMonsterType == "Terathan Avenger" ) { M_Type = typeof(TerathanAvenger); }
					else if ( m_Quest.NMonsterType == "Terathan Matriarch" ) { M_Type = typeof(TerathanMatriarch); }
					else if ( m_Quest.NMonsterType == "Titan" ) { M_Type = typeof(Titan); }
					else if ( m_Quest.NMonsterType == "Wyvern" ) { M_Type = typeof(Wyvern); }
					else if ( m_Quest.NMonsterType == "Arctic Ogre Lord" ) { M_Type = typeof(ArcticOgreLord); }
					else if ( m_Quest.NMonsterType == "Blood Elemental" ) { M_Type = typeof(BloodElemental); }
					else if ( m_Quest.NMonsterType == "Daemon" ) { M_Type = typeof(Daemon); }
					else if ( m_Quest.NMonsterType == "Efreet" ) { M_Type = typeof(Efreet); }
					else if ( m_Quest.NMonsterType == "Elder Gazer" ) { M_Type = typeof(ElderGazer); }
					else if ( m_Quest.NMonsterType == "Evil Mage Lord" ) { M_Type = typeof(EvilMageLord); }
					else if ( m_Quest.NMonsterType == "Fire Steed" ) { M_Type = typeof(FireSteed); }
					else if ( m_Quest.NMonsterType == "Ice Fiend" ) { M_Type = typeof(IceFiend); }
					else if ( m_Quest.NMonsterType == "Juggernaut" ) { M_Type = typeof(Juggernaut); }
					else if ( m_Quest.NMonsterType == "Lich Lord" ) { M_Type = typeof(LichLord); }
					else if ( m_Quest.NMonsterType == "Minotaur Captain" ) { M_Type = typeof(MinotaurCaptain); }
					else if ( m_Quest.NMonsterType == "Nightmare" ) { M_Type = typeof(Nightmare); }
					else if ( m_Quest.NMonsterType == "Plague Beast" ) { M_Type = typeof(PlagueBeast); }
					else if ( m_Quest.NMonsterType == "Poison Elemental" ) { M_Type = typeof(PoisonElemental); }
					else if ( m_Quest.NMonsterType == "Rotting Corpse" ) { M_Type = typeof(RottingCorpse); }
					else if ( m_Quest.NMonsterType == "Serpentine Dragon" ) { M_Type = typeof(SerpentineDragon); }
					else if ( m_Quest.NMonsterType == "Silver Serpent" ) { M_Type = typeof(SilverSerpent); }
					else if ( m_Quest.NMonsterType == "Ancient Lich" ) { M_Type = typeof(AncientLich); }
					else if ( m_Quest.NMonsterType == "Ancient Wyrm" ) { M_Type = typeof(AncientWyrm); }
					else if ( m_Quest.NMonsterType == "Balron" ) { M_Type = typeof(Balron); }
					else if ( m_Quest.NMonsterType == "Deep Sea Serpent" ) { M_Type = typeof(DeepSeaSerpent); }
					else if ( m_Quest.NMonsterType == "Dragon" ) { M_Type = typeof(Dragon); }
					else if ( m_Quest.NMonsterType == "Kraken" ) { M_Type = typeof(Kraken); }
					else if ( m_Quest.NMonsterType == "Shadow Wyrm" ) { M_Type = typeof(ShadowWyrm); }
					else if ( m_Quest.NMonsterType == "Skeletal Dragon" ) { M_Type = typeof(SkeletalDragon); }
					else if ( m_Quest.NMonsterType == "Succubus" ) { M_Type = typeof(Succubus); }
					else if ( m_Quest.NMonsterType == "White Wyrm" ) { M_Type = typeof(WhiteWyrm); }

					// END OF SECTION - MNTP1 /////////////////////////////////////////////////////////////////



					// SECTION - LCXY1 ////////////////////////////////////////////////////////////////////////

					// THIS SECTION DEFINES MY LOCATIONS INTO MAP AND XY COORDINATES

					if (
					( m_Quest.NLocation == "the Hedge Maze" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 1032 && from.Y >= 2159 && from.X <= 1256 && from.Y <= 2304 ) ||
					( m_Quest.NLocation == "Dungeon Destard" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5121 && from.Y >= 769 && from.X <= 5372 && from.Y <= 1020 ) ||
					( m_Quest.NLocation == "the Britain Cemetery" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 1336 && from.Y >= 1443 && from.X <= 1390 && from.Y <= 1493 ) ||
					( m_Quest.NLocation == "the Britain Cemetery" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 1336 && from.Y >= 1487 && from.X <= 1375 && from.Y <= 1510 ) ||
					( m_Quest.NLocation == "Dungeon Shame (Level 1)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5376 && from.Y >= 0 && from.X <= 5503 && from.Y <= 127 ) ||
					( m_Quest.NLocation == "Dungeon Shame (Level 2)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5505 && from.Y >= 0 && from.X <= 5363 && from.Y <= 127 ) ||
					( m_Quest.NLocation == "Dungeon Shame (Level 3)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5376 && from.Y >= 131 && from.X <= 5630 && from.Y <= 256 ) ||
					( m_Quest.NLocation == "Dungeon Shame (Level 5)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5636 && from.Y >= 0 && from.X <= 5893 && from.Y <= 126 ) ||
					( m_Quest.NLocation == "Dungeon Shame Mage Towers" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5431 && from.Y >= 175 && from.X <= 5455 && from.Y <= 199 ) ||
					( m_Quest.NLocation == "Dungeon Shame Mage Towers" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5567 && from.Y >= 183 && from.X <= 5591 && from.Y <= 207 ) ||
					( m_Quest.NLocation == "the Ice Dungeon" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5663 && from.Y >= 128 && from.X <= 5892 && from.Y <= 263 ) ||
					( m_Quest.NLocation == "the Ice Demon Lair" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5650 && from.Y >= 319 && from.X <= 5774 && from.Y <= 370 ) ||
					( m_Quest.NLocation == "the Ratman Fort" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5795 && from.Y >= 313 && from.X <= 5866 && from.Y <= 384 ) ||
					( m_Quest.NLocation == "Dungeon Hythloth (Level 1)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5889 && from.Y >= 0 && from.X <= 6005 && from.Y <= 117 ) ||
					( m_Quest.NLocation == "Dungeon Hythloth (Level 2)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5905 && from.Y >= 136 && from.X <= 6002 && from.Y <= 244 ) ||
					( m_Quest.NLocation == "Dungeon Hythloth (Level 3)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 6025 && from.Y >= 134 && from.X <= 6133 && from.Y <= 237 ) ||
					( m_Quest.NLocation == "Dungeon Hythloth (Level 4)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 6037 && from.Y >= 22 && from.X <= 6125 && from.Y <= 110 ) ||
					( m_Quest.NLocation == "Dungeon Deceit (Level 1)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5128 && from.Y >= 521 && from.X <= 5239 && from.Y <= 642 ) ||
					( m_Quest.NLocation == "Dungeon Deceit (Level 2)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5270 && from.Y >= 523 && from.X <= 5356 && from.Y <= 636 ) ||
					( m_Quest.NLocation == "Dungeon Deceit (Level 3)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5124 && from.Y >= 644 && from.X <= 5233 && from.Y <= 768 ) ||
					( m_Quest.NLocation == "Dungeon Deceit (Level 4)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5249 && from.Y >= 640 && from.X <= 5339 && from.Y <= 764 ) ||
					( m_Quest.NLocation == "Dungeon Despise (Level 1)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5377 && from.Y >= 511 && from.X <= 5517 && from.Y <= 639 ) ||
					( m_Quest.NLocation == "Dungeon Despise (Level 2)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5377 && from.Y >= 640 && from.X <= 5529 && from.Y <= 767 ) ||
					( m_Quest.NLocation == "Dungeon Despise (Level 3)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5375 && from.Y >= 768 && from.X <= 5633 && from.Y <= 1022 ) ||
					( m_Quest.NLocation == "Dungeon Wrong (Level 1)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5775 && from.Y >= 512 && from.X <= 5892 && from.Y <= 634 ) ||
					( m_Quest.NLocation == "Dungeon Wrong (Level 2)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5635 && from.Y >= 506 && from.X <= 5745 && from.Y <= 588 ) ||
					( m_Quest.NLocation == "Dungeon Wrong (Level 3)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5682 && from.Y >= 612 && from.X <= 5724 && from.Y <= 673 ) ||
					( m_Quest.NLocation == "Dungeon Khaldun" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5377 && from.Y >= 1281 && from.X <= 5627 && from.Y <= 1512 ) ||
					( m_Quest.NLocation == "the Trinsic Passage" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5886 && from.Y >= 1274 && from.X <= 6047 && from.Y <= 1414 ) ||
					( m_Quest.NLocation == "the Fire Dungeon (Level 1)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5765 && from.Y >= 1281 && from.X <= 5884 && from.Y <= 1417 ) ||
					( m_Quest.NLocation == "the Fire Dungeon (Level 1)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5666 && from.Y >= 1402 && from.X <= 5887 && from.Y <= 1520 ) ||
					( m_Quest.NLocation == "the Fire Dungeon (Level 2)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5633 && from.Y >= 1274 && from.X <= 5763 && from.Y <= 1398 ) ||
					( m_Quest.NLocation == "the Fire Dungeon (Level 2)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5630 && from.Y >= 1383 && from.X <= 5664 && from.Y <= 1456 ) ||
					( m_Quest.NLocation == "the Sewers" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 6019 && from.Y >= 1418 && from.X <= 6132 && from.Y <= 1520 ) ||
					( m_Quest.NLocation == "Terathan Keep" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5124 && from.Y >= 1532 && from.X <= 5376 && from.Y <= 1784 ) ||
					( m_Quest.NLocation == "the Solen Hive" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5639 && from.Y >= 1780 && from.X <= 5934 && from.Y <= 2037 ) ||
					( m_Quest.NLocation == "Dungeon Covetous (Level 1)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5373 && from.Y >= 1843 && from.X <= 5508 && from.Y <= 1942 ) ||
					( m_Quest.NLocation == "Dungeon Covetous (Level 2)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5374 && from.Y >= 1951 && from.X <= 5622 && from.Y <= 2045 ) ||
					( m_Quest.NLocation == "Dungeon Covetous (Level 3)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5533 && from.Y >= 1821 && from.X <= 5630 && from.Y <= 1937 ) ||
					( m_Quest.NLocation == "Dungeon Covetous (Jail Cells)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5491 && from.Y >= 1791 && from.X <= 5556 && from.Y <= 1821 ) ||
					( m_Quest.NLocation == "Dungeon Covetous (Lake Cave)" && (from.Map == Map.Trammel || from.Map == Map.Felucca) && from.X >= 5390 && from.Y >= 1780 && from.X <= 5486 && from.Y <= 1838 ) ||
					( m_Quest.NLocation == "Dungeon Doom" && from.Map == Map.Malas && from.X >= 249 && from.Y >= 0 && from.X <= 515 && from.Y <= 257 ) ||
					( m_Quest.NLocation == "Bedlam" && from.Map == Map.Malas && from.X >= 71 && from.Y >= 1564 && from.X <= 211 && from.Y <= 1690 ) ||
					( m_Quest.NLocation == "the Sorcerer`s Dungeon (Level 1)" && from.Map == Map.Ilshenar && from.X >= 365 && from.Y >= 0 && from.X <= 483 && from.Y <= 116 ) ||
					( m_Quest.NLocation == "the Sorcerer`s Dungeon (Level 2)" && from.Map == Map.Ilshenar && from.X >= 196 && from.Y >= 0 && from.X <= 363 && from.Y <= 101 ) ||
					( m_Quest.NLocation == "the Sorcerer`s Dungeon (Level 3)" && from.Map == Map.Ilshenar && from.X >= 52 && from.Y >= 0 && from.X <= 185 && from.Y <= 134 ) ||
					( m_Quest.NLocation == "the Sorcerer`s Dungeon (Jail Cells)" && from.Map == Map.Ilshenar && from.X >= 218 && from.Y >= 104 && from.X <= 251 && from.Y <= 147 ) ||
					( m_Quest.NLocation == "the Ancient Cave" && from.Map == Map.Ilshenar && from.X >= 13 && from.Y >= 658 && from.X <= 134 && from.Y <= 760 ) ||
					( m_Quest.NLocation == "the Kirin Passage" && from.Map == Map.Ilshenar && from.X >= 0 && from.Y >= 805 && from.X <= 187 && from.Y <= 1198 ) ||
					( m_Quest.NLocation == "Dungeon Ankh" && from.Map == Map.Ilshenar && from.X >= 0 && from.Y >= 1247 && from.X <= 183 && from.Y <= 1584 ) ||
					( m_Quest.NLocation == "the Serpentine Passage" && from.Map == Map.Ilshenar && from.X >= 382 && from.Y >= 1497 && from.X <= 542 && from.Y <= 1596 ) ||
					( m_Quest.NLocation == "the Wisp Dungeon (Level 3)" && from.Map == Map.Ilshenar && from.X >= 815 && from.Y >= 1446 && from.X <= 913 && from.Y <= 1584 ) ||
					( m_Quest.NLocation == "the Wisp Dungeon (Level 5)" && from.Map == Map.Ilshenar && from.X >= 917 && from.Y >= 1456 && from.X <= 1017 && from.Y <= 1578 ) ||
					( m_Quest.NLocation == "the Wisp Dungeon (Level 7)" && from.Map == Map.Ilshenar && from.X >= 740 && from.Y >= 1509 && from.X <= 815 && from.Y <= 1585 ) ||
					( m_Quest.NLocation == "the Wisp Dungeon (Level 8)" && from.Map == Map.Ilshenar && from.X >= 746 && from.Y >= 1460 && from.X <= 792 && from.Y <= 1495 ) ||
					( m_Quest.NLocation == "the Ratman Mines (Level 1)" && from.Map == Map.Ilshenar && from.X >= 1263 && from.Y >= 1460 && from.X <= 1355 && from.Y <= 1574 ) ||
					( m_Quest.NLocation == "the Ratman Mines (Level 2)" && from.Map == Map.Ilshenar && from.X >= 1151 && from.Y >= 1460 && from.X <= 1259 && from.Y <= 1558 ) ||
					( m_Quest.NLocation == "the Spider Cave" && from.Map == Map.Ilshenar && from.X >= 1749 && from.Y >= 941 && from.X <= 1870 && from.Y <= 1003 ) ||
					( m_Quest.NLocation == "the Spectre Dungeon" && from.Map == Map.Ilshenar && from.X >= 1940 && from.Y >= 1006 && from.X <= 2022 && from.Y <= 1113 ) ||
					( m_Quest.NLocation == "Dungeon Blood" && from.Map == Map.Ilshenar && from.X >= 2048 && from.Y >= 825 && from.X <= 2195 && from.Y <= 1060 ) ||
					( m_Quest.NLocation == "the Rock Dungeon" && from.Map == Map.Ilshenar && from.X >= 2084 && from.Y >= 0 && from.X <= 2244 && from.Y <= 183 ) ||
					( m_Quest.NLocation == "Dungeon Exodus" && from.Map == Map.Ilshenar && from.X >= 1836 && from.Y >= 9 && from.X <= 2082 && from.Y <= 210 )
					)
					{
						nSpot = 1;
					}

					// END OF SECTION - LCXY1 /////////////////////////////////////////////////////////////////



					if ( c.Owner == null )
					{
						if ( m_Quest.NType == 1 )
						{
							from.SendMessage( "It is too late to claim credit for this!" );
						}
						else
						{
							from.SendMessage( "Your search turns up nothing!" );
						}

						return;
					}

					if ( obj is Corpse )
					{
						obj = ((Corpse)obj).Owner;

						if ( ( M_Type == obj.GetType() ) && ( m_Quest.NType == 1 ) )
						{
							from.SendMessage( "You claim this toward your quest." );

							c.Delete();

							m_Quest.NGot = m_Quest.NGot + 1;

							if ( m_Quest.NGot >= m_Quest.NNeed )
							{
								from.PrivateOverheadMessage(MessageType.Regular, 0x44, false, "Quest Complete!", from.NetState);
								m_Quest.Name = "COMPLETE - Slay a " + m_Quest.NMonsterType + " (" + m_Quest.NGot.ToString() + " of " + m_Quest.NNeed.ToString() + ")";
								m_Quest.Hue = 1258;
							}
							else
							{
								m_Quest.Name = "Slay a " + m_Quest.NMonsterType + " (" + m_Quest.NGot.ToString() + " of " + m_Quest.NNeed.ToString() + ")";
							}
						}

						else if ( ( nSpot == 1 ) && ( m_Quest.NType == 2 ) )
						{
							if ( m_Quest.NChance > Utility.Random( 100 ) )
							{
								from.PrivateOverheadMessage(MessageType.Regular, 0x44, false, "I found " + m_Quest.NItemName + "!", from.NetState);
								from.SendMessage( "You have found " + m_Quest.NItemName + "!" );
								c.Delete();
								m_Quest.NGot = m_Quest.NGot + 1;
								m_Quest.Name = "COMPLETE - Seek " + m_Quest.NItemName;
								m_Quest.Hue = 1258;
							}
							else
							{
								from.SendMessage( "You did not find the item you seek!" );
								c.Delete();
							}
						}
						else
						{
							from.SendMessage( "That has nothing to do with your quest!" );
						}

						return;
					}

					from.SendMessage( "That has nothing to do with your quest!" );
				}
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			string sStatus = "Level " + NLevel.ToString() + " Quest";

			base.AddNameProperties( list );
			list.Add( 1070722, NStory );
			list.Add( 1049644, sStatus );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write(mNNeed);
			writer.Write(mNGot);
			writer.Write(mNChance);
			writer.Write(mNLevel);
			writer.Write(mNType);
			writer.Write(mNItemName);
			writer.Write(mNMonsterType);
			writer.Write(mNLocation);
			writer.Write(mNStory);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			mNNeed = reader.ReadInt();
			mNGot = reader.ReadInt();
			mNChance = reader.ReadInt();
			mNLevel = reader.ReadInt();
			mNType = reader.ReadInt();
			mNItemName = reader.ReadString();
			mNMonsterType = reader.ReadString();
			mNLocation = reader.ReadString();
			mNStory = reader.ReadString();

			string sCOMPLETE = "";

			if ( NGot >= NNeed )
			{
				sCOMPLETE = "COMPLETE - ";
				Hue = 1258;
			}

			if ( NType == 1 )
			{
				Name = sCOMPLETE + "Slay a " + NMonsterType + " (" + NGot.ToString() + " of " + NNeed.ToString() + ")";
			}
			else
			{
				Name = sCOMPLETE + "Seek " + NItemName;
			}
		}
	}
}