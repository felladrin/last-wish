using System;
using Server;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public abstract class DecoPet
	{
		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( OnLogin );
			EventSink.Disconnected += new DisconnectedEventHandler( EventSink_Disconnected );
		}
		
		private static void EventSink_Disconnected( DisconnectedEventArgs e )
		{
			Mobile from = e.Mobile;
			
			if(from is PlayerMobile)
			{
				Map map = from.Map;

				if ( map == null )
					return;

				IPooledEnumerable eable = map.GetMobilesInRange( from.Location, 10 );
				List<Mobile> pets = new List<Mobile>();
				
				foreach ( Mobile m in eable )
				{
					if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						
						if(bc != null && bc.Controlled && bc.ControlMaster == from )
						{
							pets.Add(m);
						}
					}
				}
				eable.Free();
				
				for(int i=0;i<pets.Count;i++)
				{
					Mobile pet = pets[i];
					pet.Internalize();
					pet.CantWalk = false;
				}
			}
		}
		
		private static void OnLogin( LoginEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( from != null )
			{
				Map map = from.Map;

				if ( map == null )
					return;

				Dictionary<Serial, Mobile>.Enumerator mobs = World.Mobiles.GetEnumerator();
				List<Mobile> pets = new List<Mobile>();
				while (mobs.MoveNext()) pets.Add(mobs.Current.Value);
				mobs.Dispose();
				
				for(int i=0;i<pets.Count;i++)
				{
					Mobile pet = pets[i];
					
					if ( pet is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)pet;
						
						if(bc != null && bc.Map == Map.Internal && bc.Controlled && bc.ControlMaster == from && !IsMounted(bc))
						{
							pet.Map = map;
							pet.CantWalk = false;
						}
					}
				}
			}
		}
		
		private static bool IsMounted(BaseCreature pet)
		{
			if( pet is BaseMount )
			{
				BaseMount mount = (BaseMount)pet;
				
				if( mount != null )
				{
					if( mount.Rider != null )
						return true;
				}
			}
			
			return false;
		}
	}
}
