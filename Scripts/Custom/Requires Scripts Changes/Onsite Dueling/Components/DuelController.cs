/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Engines/Onsite Duel System/Components/DuelController.cs#8 $

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Gumps;
using Server.Regions;
using Server.Mobiles;
using Server.Commands;

namespace Server.Engines.Dueling
{
    public class DuelController
	{
		public static readonly string Version = "2.1.3";
		private static readonly string SaveDirectory = Path.Combine(Core.BaseDirectory, "Saves/Onsite Dueling");
		private static readonly string PointsFile = Path.Combine(SaveDirectory, "Points.sav");

		private int m_MaxDistance = 30;
		private int m_DuelLengthInSeconds = 1800;

		private static bool m_Enabled = false;

		[CommandProperty(AccessLevel.Administrator)]
		public static bool Enabled { get { return m_Enabled; } set { m_Enabled = value; } }

		[CommandProperty(AccessLevel.Administrator)]
		public int DuelLengthInSeconds { get { return m_DuelLengthInSeconds; } set { m_DuelLengthInSeconds = value; } }

		[CommandProperty(AccessLevel.Administrator)]
		public int MaxDistance { get { return m_MaxDistance; } set { m_MaxDistance = value; } }

		private static List<Mobile> m_DeclineDuelList;
		public static List<Mobile> DeclineDuelList { get { return m_DeclineDuelList; } }

		private static DuelController m_Instance;
		public static DuelController Instance { get { return m_Instance; } }

		private static Dictionary<Serial, LogoutTimeoutTimer> m_TimeoutTable;
		private static Dictionary<Serial, DuelStartTimer> m_DuelStartTimeoutTable;
		private static Dictionary<Serial, Duel> m_DuelTable;
		private static Dictionary<Serial, DuelPoints> m_PointsTable;

		public static Dictionary<Serial, Duel> DuelTable { get { return m_DuelTable; } set { m_DuelTable = value; } }
		public static Dictionary<Serial, DuelStartTimer> DuelStartTimeoutTable { get { return m_DuelStartTimeoutTable; } set { m_DuelStartTimeoutTable = value; } }
		public static Dictionary<Serial, DuelPoints> PointsTable { get { return m_PointsTable; } }

		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_PlayerDeath);
			EventSink.Logout += new LogoutEventHandler(EventSink_Logout);
			EventSink.Login += new LoginEventHandler(EventSink_Login);
			EventSink.Movement += new MovementEventHandler(EventSink_Movement);
			EventSink.WorldSave += new WorldSaveEventHandler(EventSink_WorldSave);

			PlayerMobile.AllowBeneficialHandler = new AllowBeneficialHandler(PlayerMobile_AllowBenificial);
			PlayerMobile.AllowHarmfulHandler = new AllowHarmfulHandler(PlayerMobile_AllowHarmful);

			Notoriety.Handler = new NotorietyHandler(Notoriety_HandleNotoriety);

			CommandSystem.Register("OnsiteConfig", AccessLevel.Administrator, new CommandEventHandler(OnCommand_OnsiteConfig));
			CommandSystem.Register("Duel", AccessLevel.Player, new CommandEventHandler(OnCommand_Duel));

			m_Instance = new DuelController();
			m_TimeoutTable = new Dictionary<Serial, LogoutTimeoutTimer>();
			m_DuelTable = new Dictionary<Serial, Duel>();
			m_PointsTable = new Dictionary<Serial, DuelPoints>();
			m_DeclineDuelList = new List<Mobile>();
			m_DuelStartTimeoutTable = new Dictionary<Serial, DuelStartTimer>();

			LoadSaves();
		}

		[Usage("Duel")]
		[Description("Initiates a duel between 2 or more players.")]
		private static void OnCommand_Duel(CommandEventArgs e)
		{
			Mobile m = e.Mobile;

			if (m == null)
				return;


			if (!m_Enabled)
			{
				m.SendMessage("Sorry the duel system is currently offline. Please try again later.");
				return;
			}

			if (!CanDuel(m))
				return;

			Duel duel = new Duel(m);

			if (!m_DuelTable.ContainsKey(m.Serial))
				m_DuelTable.Add(m.Serial, duel);

			DuelStartTimer timer = new DuelStartTimer(duel);
			DuelStartTimeoutTable[m.Serial] = timer;

			m.CloseGump(typeof(DuelConfigGump));
			m.SendGump(new DuelConfigGump(duel));
		}

		public static bool CanDuel(Mobile m)
		{
			Duel duel;

			if (m.Criminal)
			{
				m.SendMessage("You may not start a duel while flagged criminal.");
				return false;
			}
			/*else if( _DeclineDuelList.Contains( m ) )
			{
				m.SendMessage( "You have elected to not duel, use \"[AllowDuel true\" to be able to duel." );
				return false;
			}*/
			else if (Spells.SpellHelper.CheckCombat(m))
			{
				m.SendMessage("You cannot start a duel while in combat.");
				return false;
			}
			else if (IsInDuel(m, out duel))
			{
				m.SendMessage("You are currently in a duel.");
				return false;
			}
			else if (m.Hits != m.HitsMax)
			{
				m.SendMessage("Try again when you have full health.");
				return false;
			}
			else if (Factions.Sigil.ExistsOn(m))
			{
				m.SendMessage("You may not challenge someone while you have a faction sigil.");
				return false;
			}

			return true;
		}

		[Usage("OnsiteConfig")]
		[Description("Configures the Onsite Dueling System.")]
		private static void OnCommand_OnsiteConfig(CommandEventArgs e)
		{
			Mobile m = e.Mobile;

			if (m == null)
				return;

			m.CloseGump(typeof(PropertiesGump));
			m.SendGump(new PropertiesGump(m, m_Instance));
		}

		private static void LoadSaves()
		{
			try
			{
				if (!File.Exists(PointsFile))
					return;

				BinaryFileReader read = new BinaryFileReader(new BinaryReader(new FileStream(PointsFile, FileMode.Open)));
				GenericReader reader = read;
				InternalLoad(reader);
				read.Close();
				Console.WriteLine("Onsite Dueling System: DuelPoints loaded.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Onsite Dueling System: Load failed!");
				Console.WriteLine("Caught an exception: {0}", e.ToString());
				m_PointsTable.Clear();
				m_DeclineDuelList.Clear();
				Console.WriteLine("Onsite Dueling System: Cleared potentially corrupted data.");
			}
		}

		private static void InternalLoad(GenericReader reader)
		{
			int version = reader.ReadEncodedInt();

			switch (version)
			{
				case 1:
					{
						m_Enabled = reader.ReadBool();
						m_Instance.m_DuelLengthInSeconds = reader.ReadEncodedInt();
						m_Instance.m_MaxDistance = reader.ReadEncodedInt();
						goto case 0;
					}
				case 0:
					{
						int count = reader.ReadEncodedInt();

						for (int i = 0; i < count; i++)
						{
							Serial serial = (Serial)reader.ReadInt();
							DuelPoints points = new DuelPoints(reader);
							m_PointsTable.Add(serial, points);
						}

						m_DeclineDuelList = reader.ReadStrongMobileList();

						break;
					}
			}
		}

		public static bool IsInDuel(Mobile m)
		{
			Duel d;
			return IsInDuel(m, out d);
		}

		public static bool IsInDuel(Mobile m, out Duel duel)
		{
			duel = null;

			if (m_DuelTable != null && m != null && m_DuelTable.TryGetValue(m.Serial, out duel))
				return true;
			else
				return false;
		}

		private static void EventSink_WorldSave(WorldSaveEventArgs e)
		{
			try
			{
				if (!Directory.Exists(SaveDirectory))
					Directory.CreateDirectory(SaveDirectory);
				GenericWriter writer = new BinaryFileWriter(new FileStream(PointsFile, FileMode.OpenOrCreate), true);
				InternalSave(writer);
				writer.Close();
				Console.WriteLine("Onsite Dueling System: DuelPoints saved!");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Onsite Dueling System: Save failed!");
				Console.WriteLine("Caught an exception: {0}", ex.ToString());
			}
		}

		private static void InternalSave(GenericWriter writer)
		{
			writer.WriteEncodedInt(1); // version

			writer.Write((bool)m_Enabled);
			writer.WriteEncodedInt(m_Instance.m_DuelLengthInSeconds);
			writer.WriteEncodedInt(m_Instance.m_MaxDistance);

			int count = m_PointsTable.Count;

			writer.WriteEncodedInt(count);
			IDictionaryEnumerator myEnum = m_PointsTable.GetEnumerator();

			int i = 0;
			while (myEnum.MoveNext())
			{
				Serial serial = (Serial)myEnum.Key;
				DuelPoints duelPoints = (DuelPoints)myEnum.Value;

				writer.Write((int)serial);
				duelPoints.Serialize(writer);

				i++;
			}

			writer.WriteMobileList<Mobile>(m_DeclineDuelList);
		}

		private static int Notoriety_HandleNotoriety(Mobile from, Mobile target)
		{
			if (from == null || target == null)
				return NotorietyHandlers.MobileNotoriety(from, target);

			Duel fromDuel, targetDuel;
			bool fromInDuel = IsInDuel(from, out fromDuel);
			bool targetInDuel = IsInDuel(target, out targetDuel);

			if (fromInDuel && targetInDuel)
			{
				if (fromDuel == null || targetDuel == null)
					return NotorietyHandlers.MobileNotoriety(from, target);

				if (fromDuel == targetDuel)
				{
					if (fromDuel.Started)
					{
						if ((fromDuel.Attackers.Contains(from) && fromDuel.Attackers.Contains(target)) || (fromDuel.Defenders.Contains(from) && fromDuel.Defenders.Contains(target)))
							return Notoriety.Ally;
						else
							return Notoriety.Enemy;
					}
					else
						return NotorietyHandlers.MobileNotoriety(from, target);
				}
				else
					return Notoriety.Invulnerable;
			}
			else if ((fromInDuel && !targetInDuel) || (!fromInDuel && targetInDuel))
			{
				if (!target.Player || !from.Player)
					return NotorietyHandlers.MobileNotoriety(from, target);
				else if (!(target.Region is GuardedRegion))
					return NotorietyHandlers.MobileNotoriety(from, target);
				else
					if ((fromInDuel && fromDuel.Started) || (targetInDuel && targetDuel.Started))
						return Notoriety.Invulnerable;
					else
						return NotorietyHandlers.MobileNotoriety(from, target);
			}
			else
				return NotorietyHandlers.MobileNotoriety(from, target);
		}

		private static bool PlayerMobile_AllowHarmful(Mobile from, Mobile target)
		{
			if (from == null || target == null)
				return NotorietyHandlers.Mobile_AllowHarmful(from, target); ;

			Duel fromDuel, targetDuel;
			bool fromInDuel = IsInDuel(from, out fromDuel);
			bool targetInDuel = IsInDuel(target, out targetDuel);

			if (fromInDuel && targetInDuel)
			{
				if (fromDuel == null || targetDuel == null)
					return NotorietyHandlers.Mobile_AllowHarmful(from, target);

				return (fromDuel == targetDuel);
			}
			else if ((fromInDuel && !targetInDuel) || (targetInDuel && !fromInDuel))
				if (from.Player && target.Player)
					return false;

			return NotorietyHandlers.Mobile_AllowHarmful(from, target);
		}

		private static bool PlayerMobile_AllowBenificial(Mobile from, Mobile target)
		{
			if (from == null || target == null)
				return NotorietyHandlers.Mobile_AllowBeneficial(from, target); ;

			Duel fromDuel, targetDuel;
			bool fromInDuel = IsInDuel(from, out fromDuel);
			bool targetInDuel = IsInDuel(target, out targetDuel);

			if (fromInDuel && targetInDuel)
			{
				if (fromDuel == null || targetDuel == null)
					return NotorietyHandlers.Mobile_AllowBeneficial(from, target);

				return (fromDuel == targetDuel);
			}
			else if ((fromInDuel && !targetInDuel) || (targetInDuel && !fromInDuel))
				if (from.Player && target.Player)
					return false;

			return NotorietyHandlers.Mobile_AllowBeneficial(from, target);
		}

		private static void EventSink_Movement(MovementEventArgs e)
		{
			Mobile m = e.Mobile;

			if (m == null)
				return;

			Duel duel;
			if (m_DuelTable.TryGetValue(m.Serial, out duel))
			{
				if (duel.Started && duel.CheckDistance())
					duel.EndDuel();
			}
		}

		private static void EventSink_Login(LoginEventArgs e)
		{
			Mobile m = e.Mobile;

			if (m == null)
				return;

			LogoutTimeoutTimer timer;
			if (m_TimeoutTable.TryGetValue(m.Serial, out timer))
			{
				timer.Stop();
				m_TimeoutTable.Remove(m.Serial);
			}
		}

		private static void EventSink_Logout(LogoutEventArgs e)
		{
			Mobile m = e.Mobile;

			if (m == null)
				return;

			Duel duel;

			if (IsInDuel(m, out duel) && !m_TimeoutTable.ContainsKey(m.Serial))
			{
				if (duel != null && duel.Started)
				{
					LogoutTimeoutTimer timer = new LogoutTimeoutTimer(m, duel);
					timer.Start();

					m_TimeoutTable.Add(m.Serial, timer);
				}
			}
		}

		private static void EventSink_PlayerDeath(PlayerDeathEventArgs e)
		{
			Mobile m = e.Mobile;

			if (m == null)
				return;

			Duel duel;
			if (m_DuelTable.TryGetValue(m.Serial, out duel))
			{
				if (duel.Started)
					duel.HandleDeath(m);
			}
		}

		public static void WritePointsDictionary(Dictionary<int, DuelInfo> dictionary, GenericWriter writer)
		{
			writer.Write((int)0);

			int count = dictionary.Count;
			writer.Write((int)count);

			IDictionaryEnumerator myEnum = dictionary.GetEnumerator();

			while (myEnum.MoveNext())
			{
				int key = (int)myEnum.Key;
				DuelInfo info = (DuelInfo)myEnum.Value;

				writer.Write((int)key);
				info.Serialize(writer);
			}
		}

		public static void WriteIntDictionary(Dictionary<int, int> dictionary, GenericWriter writer)
		{
			writer.Write((int)0);

			int count = dictionary.Count;
			writer.Write((int)count);

			IDictionaryEnumerator myEnum = dictionary.GetEnumerator();

			while (myEnum.MoveNext())
			{
				int key = (int)myEnum.Key;
				int info = (int)myEnum.Value;

				writer.Write((int)key);
				writer.Write((int)info);
			}
		}

		public static Dictionary<int, DuelInfo> ReadPointsDictionary(GenericReader reader)
		{
			Dictionary<int, DuelInfo> dictionary = new Dictionary<int, DuelInfo>();

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
					{
						int count = reader.ReadInt();

						for (int i = 0; i < count; i++)
						{
							int key = reader.ReadInt();
							DuelInfo dInfo = new DuelInfo(reader);
							dictionary.Add(key, dInfo);
						}

						break;
					}
			}

			return dictionary;
		}

		public static Dictionary<int, int> ReadIntDictionary(GenericReader reader)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
					{
						int count = reader.ReadInt();

						for (int i = 0; i < count; i++)
						{
							int key = reader.ReadInt();
							int value = reader.ReadInt();
							dictionary.Add(key, value);
						}

						break;
					}
			}

			return dictionary;
		}

		public static void DestroyDuel(Duel duel)
		{
			List<Mobile> mobs = new List<Mobile>();

			if (m_DuelTable.ContainsKey(duel.Creator.Serial))
				m_DuelTable.Remove(duel.Creator.Serial);

			if (duel.Attackers != null)
				mobs.AddRange(duel.Attackers);
			if (duel.Defenders != null)
				mobs.AddRange(duel.Defenders);
			if (duel.Contestants != null)
				mobs.AddRange(duel.Contestants);

			for (int i = 0; i < mobs.Count; i++)
			{
				if (m_DuelTable.ContainsKey(mobs[i].Serial))
					m_DuelTable.Remove(mobs[i].Serial);
			}

			duel = null;
		}

		public static void Broadcast(string msg, List<Mobile> broadcastTo)
		{
			if (broadcastTo != null && broadcastTo.Count > 0)
			{
				foreach (Mobile m in broadcastTo)
					m.SendMessage(msg);
			}
		}

		public static void Broadcast(int hue, string msg, List<Mobile> broadcastTo)
		{
			if (broadcastTo != null && broadcastTo.Count > 0)
			{
				foreach (Mobile m in broadcastTo)
					m.SendMessage(hue, msg);
			}
		}

		public static void AddWin(Duel duel, Mobile m)
		{
			DuelPoints dPoints;
			if (m_PointsTable.TryGetValue(m.Serial, out dPoints))
			{
				DuelInfo dInfo = new DuelInfo(duel.Attackers.Contains(m) ? duel.Attackers : duel.Defenders,
					TimeSpan.FromSeconds(DuelController.Instance.DuelLengthInSeconds - duel.DuelTimer.SecondsRemaining));
				dPoints.AddWin(duel.Attackers.Count, dInfo);
			}
			else
			{
				dPoints = new DuelPoints(m);
				DuelInfo dInfo = new DuelInfo(duel.Attackers.Contains(m) ? duel.Attackers : duel.Defenders,
					TimeSpan.FromSeconds(DuelController.Instance.DuelLengthInSeconds - duel.DuelTimer.SecondsRemaining));
				dPoints.AddWin(duel.Attackers.Count, dInfo);
				m_PointsTable.Add(m.Serial, dPoints);
			}
		}

		public static void AddLoss(Duel duel, Mobile m)
		{
			DuelPoints dPoints;
			if (m_PointsTable.TryGetValue(m.Serial, out dPoints))
			{
				DuelInfo dInfo = new DuelInfo(duel.Attackers.Contains(m) ? duel.Attackers : duel.Defenders,
					TimeSpan.FromSeconds(DuelController.Instance.DuelLengthInSeconds - duel.DuelTimer.SecondsRemaining));
				dPoints.AddLoss(duel.Attackers.Count, dInfo);
			}
			else
			{
				dPoints = new DuelPoints(m);
				DuelInfo dInfo = new DuelInfo(duel.Attackers.Contains(m) ? duel.Attackers : duel.Defenders,
					TimeSpan.FromSeconds(DuelController.Instance.DuelLengthInSeconds - duel.DuelTimer.SecondsRemaining));
				dPoints.AddLoss(duel.Attackers.Count, dInfo);
				m_PointsTable[m.Serial] = dPoints;
			}
		}
	}
}
