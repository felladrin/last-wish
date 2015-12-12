using System;
using System.IO;
using System.Collections;
using Server.Commands;

namespace Server.Forums
{
    public class ForumCore
	{
		private static ArrayList m_Threads; //Thread Collection
		//private static ArrayList m_PosterHolder;//Long Post Collection Holder
		private static Hashtable m_PlayerStatistics; //Player Statistics Collection
		private static string m_SavePath = Path.Combine( Core.BaseDirectory, "Forums" ); //Save Directory
		private static int m_ThreadNumber = 0;//Thread number for next thread. !!DO NOT CHANGE THIS!!
		private static string m_Version = "1.3.6";
		
		public static Hashtable PlayerStatistics { get { return m_PlayerStatistics; } set { m_PlayerStatistics = value; } }
		public static string SaveDirectory { get { return m_SavePath; } }
		public static string Version { get { return m_Version; } }
		//public static ArrayList PosterHolder { get { return m_PosterHolder; } }
		public static ArrayList Threads { get { return m_Threads; } }
		
		/* Values for Administration Stone */
		private static AccessLevel m_ThreadLockAccesLevel;
		private static AccessLevel m_ThreadDeleteAccessLevel;
		private static bool m_AutoCleanup;
		private static int m_AutoCleanupDays;
		private static ArrayList m_Moderators;
		private static int m_MinPostCharactersCount;
		private static int m_MaxPostCharactersCount;

		public static int MinPostCharactersCount { get { return m_MinPostCharactersCount; } set { m_MinPostCharactersCount = value; } }
		public static int MaxPostCharactersCount { get { return m_MaxPostCharactersCount; } set { m_MaxPostCharactersCount = value; } }
		public static bool AutoCleanup { get { return m_AutoCleanup; } set { m_AutoCleanup = value; } }
		public static int AutoCleanupDays { get { return m_AutoCleanupDays; } set { m_AutoCleanupDays = value; } }
		public static ArrayList Moderators { get { return m_Moderators; } set { m_Moderators = value; } }
		public static AccessLevel ThreadLockAccesLevel { get { return m_ThreadLockAccesLevel; } set { m_ThreadLockAccesLevel = value; } }
		public static AccessLevel ThreadDeleteAccessLevel { get { return m_ThreadDeleteAccessLevel; } set { m_ThreadDeleteAccessLevel = value; } }
		/* Values for Administration Stone */

		public static int ThreadCount
		{
			get
			{
				int count = 0;
				for( int i = 0; i < m_Threads.Count; i++ )
				{
					ThreadEntry te = ( ThreadEntry )m_Threads[i];
					if( !te.Deleted && !te.StaffMessage )
						count++;
				}

				return count;
			}
		}

		public static int ThreadNumber
		{
			get
			{
				m_ThreadNumber++;
				return m_ThreadNumber;
			}
		}

		public static void Initialize()
		{
			m_Threads = new ArrayList();
			m_Moderators = new ArrayList();
			m_PlayerStatistics = new Hashtable();

			Server.EventSink.WorldSave += new WorldSaveEventHandler( EventSink_WorldSave );
			CommandSystem.Register( "Forum", AccessLevel.Player, new CommandEventHandler( ViewForums_OnCommand ) );
			
			Console.Write( "Ingame Forums: " );
			
			if( File.Exists( Path.Combine( m_SavePath, "forumdata.sig" ) ) )
			{
				Load();
			}
			else
			{
				Console.WriteLine( "No save file was found. Using default settings." );
				m_ThreadLockAccesLevel = AccessLevel.GameMaster;
				m_ThreadDeleteAccessLevel = AccessLevel.Administrator;
				m_AutoCleanup = false;
				m_AutoCleanupDays = 30;
				m_MinPostCharactersCount = 5;
				m_MaxPostCharactersCount = 10000;
			}
		}

		[Usage( "Forum" )]
		[Description( "Opens the forum." )]
		public static void ViewForums_OnCommand( CommandEventArgs e )
		{
			Mobile pm = ( Mobile )e.Mobile;

			AuthorStatistics ast = ForumCore.GetAuthorStatistics( pm.Serial.Value );
			if( ast.Banned )
			{
				pm.SendMessage( "You've been banned from the forum!" );
				return;
			}

			m_Threads.Sort( new DateSort() );
			pm.CloseGump( typeof( ForumGump ) );
			pm.SendGump( new ForumGump( pm, 0 ) );
		}

		public static void UpdatePlayerStatistics( Mobile pm )
		{
			AuthorStatistics ast;
			bool newUser = false;

			if( ForumCore.PlayerStatistics.ContainsKey( pm.Serial.Value ) )
			{

				ast = ForumCore.GetAuthorStatistics( pm.Serial.Value );
			}
			else
			{
				ast = new AuthorStatistics( pm.Serial.Value, pm.Name, DateTime.Now, 0 );
				newUser = true;
			}

			ast.PostCount++;

			if( newUser )
			{
				if( !PlayerStatistics.ContainsKey( ast.Serial ) )
					PlayerStatistics.Add( ast.Serial, ast );
			}
		}

		public static bool AuthorExists(out ArrayList list, string name)
		{
			IDictionaryEnumerator myEnum = m_PlayerStatistics.GetEnumerator();

			list = new ArrayList();

			bool found = false;
			while (myEnum.MoveNext())
			{
				AuthorStatistics ast = (AuthorStatistics)myEnum.Value;

				if( ast.Name.ToLower() == name.ToLower() )
				{
					found = true;
					list.Add(ast);
				}
			}

			return found;
		}

		public static AuthorStatistics GetAuthorStatistics( int index )
		{
			AuthorStatistics ast = new AuthorStatistics();
			
			if( m_PlayerStatistics.ContainsKey( ( object)index ) )
				return ( AuthorStatistics )m_PlayerStatistics[index];
			else
				return ast;
		}

		private static bool CheckAutoCleanupDate( ThreadEntry te )
		{
			DateTime today = DateTime.Now;

			if ( today.Date.Day - te.LastPostTime.Date.Day  > AutoCleanupDays )
				return true;

			return false;
		}

		private static int RemovedDeletedQueue( out int queue )
		{
			ArrayList toBeDeleted = new ArrayList();

			for( int i = 0; i < m_Threads.Count; i++ )
			{
				ThreadEntry te = ( ThreadEntry )m_Threads[i];

				if( te.Deleted )
					toBeDeleted.Add( te );
				else if( AutoCleanup )
				{
					if( CheckAutoCleanupDate( te ) )
						toBeDeleted.Add( te );
				}
			}

			queue = toBeDeleted.Count;

			if( toBeDeleted.Count > 0 )
			{
				for( int i = 0; i < toBeDeleted.Count; i++ )
				{
					ThreadEntry te = ( ThreadEntry )toBeDeleted[i];
					m_Threads.Remove( te );
				}

				return toBeDeleted.Count;
			}
			else
				return 0;
		}

		private static void WritePlayerStatistics( GenericWriter writer )
		{
			IDictionaryEnumerator myEnum = m_PlayerStatistics.GetEnumerator();

			ArrayList keyArray = new ArrayList();
			ArrayList astArray = new ArrayList();

			while( myEnum.MoveNext() )
			{
				keyArray.Add( myEnum.Key );
				astArray.Add( myEnum.Value );
			}

			int count = keyArray.Count;
			writer.Write( ( int )count );

			for( int i = 0; i < count; i++ )
			{
				int key = ( int )keyArray[i];
				AuthorStatistics ast = ( AuthorStatistics )astArray[i];

				writer.Write( ( int )key );
				ast.Serialize( writer );
			}
		}

		private static Hashtable ReadPlayerStatistics( GenericReader reader )
		{
			int count = reader.ReadInt();

			Hashtable ht = new Hashtable();

			for( int i = 0; i < count; i++ )
			{
				int key = reader.ReadInt();
				AuthorStatistics ast = new AuthorStatistics();
				ast.Deserialize( reader );

				ht.Add( key, ast );
			}

			return ht;
		}

		private static void EventSink_WorldSave( WorldSaveEventArgs e )
		{
			int queue = 0;
			int deleted = RemovedDeletedQueue(out queue);

			if ( queue != 0 )
				Console.Write( "{0} Forum Posts Deleted...", deleted );
			
			string SavePath = Path.Combine( m_SavePath, "forumdata.sig" );

			if( !Directory.Exists( m_SavePath ) )
				Directory.CreateDirectory( m_SavePath );

			GenericWriter bin = new BinaryFileWriter( SavePath, true );
			
			try
			{
				bin.Write( ( int )0 );//Versioning

				WritePlayerStatistics( bin );
				
				bin.Write( ( int )( m_Threads.Count ) );
				foreach( ThreadEntry te in m_Threads )
				{
					te.Serialize( bin );
				}

				bin.WriteMobileList( m_Moderators );
				bin.Write( ( int )m_ThreadDeleteAccessLevel );
				bin.Write( ( int )m_ThreadLockAccesLevel );
				bin.Write( ( bool )m_AutoCleanup );
				bin.Write( ( int )m_AutoCleanupDays );
				bin.Write( ( int )m_MinPostCharactersCount );
				bin.Write( ( int )m_MaxPostCharactersCount );
				bin.Close();
			}
			catch( Exception err )
			{
				bin.Close();
				Console.Write( "An error occurred while trying to save the forums. {0}", err.ToString());
			}
		}

		private static void Load()
		{
			try
			{
				string SavePath = Path.Combine( m_SavePath, "forumdata.sig" );

				using( FileStream fs = new FileStream( SavePath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
				{
					BinaryReader br = new BinaryReader( fs );
					BinaryFileReader reader = new BinaryFileReader( br );

					int version = reader.ReadInt();
					switch( version )
					{
						case 0:
							{
								m_PlayerStatistics = ReadPlayerStatistics( reader );
								int count = reader.ReadInt();
								for( int i = 0; i < count; i++ )
								{
									ThreadEntry te = new ThreadEntry();
									te.Deserialize( reader );
									m_Threads.Add( te );
								}
								m_Moderators = reader.ReadMobileList();
								m_ThreadDeleteAccessLevel = (AccessLevel)reader.ReadInt();
								m_ThreadLockAccesLevel = ( AccessLevel )reader.ReadInt();
								m_AutoCleanup = reader.ReadBool();
								m_AutoCleanupDays = reader.ReadInt();
								m_MinPostCharactersCount = reader.ReadInt();
								m_MaxPostCharactersCount = reader.ReadInt();
								break;
							}
					}
				}

				m_Threads.Sort( new DateSort() );
				Console.WriteLine( "Loading...done" );
			}
			catch(Exception err)
			{
				Console.WriteLine( "An error occured while loading the forums...{0}", err.ToString() );
			}
		}
	}
}
