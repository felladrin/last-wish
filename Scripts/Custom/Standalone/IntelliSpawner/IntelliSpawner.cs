// IntelliSpawner 1.0.5
// Author: Felladrin
// Started: 2013-06-19
// Updated: 2016-02-05

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Server.Commands;
using Server.Items;
using Server.Multis;
using Server.Network;
using CPA = Server.CommandPropertyAttribute;

namespace Server.Mobiles
{
    public class IntelliSpawner: Item, ISpawner
    {
        public static void Initialize()
        {
            new ActivationTimer().Start();
        }

        class ActivationTimer: Timer
        {
            public ActivationTimer()
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(5))
            {
                Priority = TimerPriority.FiveSeconds;
            }

            protected override void OnTick()
            {
                var spawners = new List<Item>();

                foreach (NetState state in NetState.Instances)
                {
                    if (state.Mobile != null)
                    {
                        foreach (Item item in state.Mobile.GetItemsInRange(100))
                        {
                            if (item is IntelliSpawner)
                            {
                                spawners.Add(item);
                            }
                        }
                    }
                }

                foreach (Item item in spawners)
                {
                    var spawner = item as IntelliSpawner;

                    if (spawner != null && !spawner.Running)
                    {
                        spawner.Running = true;
                        spawner.Respawn();
                        spawner.DoTimer();
                    }
                }
            }
        }


        int m_Team;
        int m_HomeRange;
        int m_WalkingRange;
        int m_Count;
        TimeSpan m_MinDelay;
        TimeSpan m_MaxDelay;
        List < string > m_SpawnNames;
        List < ISpawnable > m_Spawned;
        InternalTimer m_Timer;
        bool m_Running;
        bool m_Group;

        public bool IsFull
        {
            get
            {
                return (m_Spawned.Count >= m_Count);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (m_Spawned.Count == 0);
            }
        }

        public List < string > SpawnNames
        {
            get
            {
                return m_SpawnNames;
            }
            set
            {
                m_SpawnNames = value;
                if (m_SpawnNames.Count < 1)
                    Stop();

                InvalidateProperties();
            }
        }

        public List < ISpawnable > Spawned
        {
            get
            {
                return m_Spawned;
            }
        }

        public virtual int SpawnNamesCount
        {
            get
            {
                return m_SpawnNames.Count;
            }
        }

        public override void OnAfterDuped(Item newItem)
        {
            var s = newItem as IntelliSpawner;

            if (s == null)
                return;

            s.m_SpawnNames = new List < string >(m_SpawnNames);
            s.m_Spawned = new List < ISpawnable >();
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IgnoreHousing
        {
            get;
            set;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool MobilesSeekHome
        {
            get;
            set;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Rectangle2D SpawnArea
        {
            get;
            set;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Count
        {
            get
            {
                return m_Count;
            }
            set
            {
                m_Count = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public WayPoint WayPoint
        {
            get;
            set;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Running
        {
            get
            {
                return m_Running;
            }
            set
            {
                if (value)
                    Start();
                else
                    Stop();

                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool UsesSpawnerHome
        {
            get;
            set;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HomeRange
        {
            get
            {
                return m_HomeRange;
            }
            set
            {
                m_HomeRange = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WalkingRange
        {
            get
            {
                return m_WalkingRange;
            }
            set
            {
                m_WalkingRange = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Team
        {
            get
            {
                return m_Team;
            }
            set
            {
                m_Team = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan MinDelay
        {
            get
            {
                return m_MinDelay;
            }
            set
            {
                m_MinDelay = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan MaxDelay
        {
            get
            {
                return m_MaxDelay;
            }
            set
            {
                m_MaxDelay = value;
                InvalidateProperties();
            }
        }

        public DateTime End
        {
            get;
            set;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextSpawn
        {
            get
            {
                if (m_Running)
                    return End - DateTime.UtcNow;

                return TimeSpan.FromSeconds(0);
            }
            set
            {
                Start();
                DoTimer(value);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Group
        {
            get
            {
                return m_Group;
            }
            set
            {
                m_Group = value;
                InvalidateProperties();
            }
        }

        [Constructable]
        public IntelliSpawner()
            : this(null)
        {
        }

        [Constructable]
        public IntelliSpawner(string spawnName)
            : this(1, 5, 10, 0, 4, spawnName)
        {
        }

        [Constructable]
        public IntelliSpawner(int amount, int minDelay, int maxDelay, int team, int homeRange, string spawnName)
            : this(amount, TimeSpan.FromMinutes(minDelay), TimeSpan.FromMinutes(maxDelay), team, homeRange, spawnName)
        {
        }

        [Constructable]
        public IntelliSpawner(int amount, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, string spawnName)
            : base(0x1f13)
        {
            var spawnNames = new List < string >();

            if (!String.IsNullOrEmpty(spawnName))
                spawnNames.Add(spawnName);

            InitSpawner(amount, minDelay, maxDelay, team, homeRange, spawnNames);
        }

        public IntelliSpawner(int amount, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, List < string > spawnNames)
            : base(0x1f13)
        {
            InitSpawner(amount, minDelay, maxDelay, team, homeRange, spawnNames);
        }

        public override string DefaultName
        {
            get
            {
                return "IntelliSpawner";
            }
        }

        void InitSpawner(int amount, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, List < string > spawnNames)
        {
            Visible = false;
            Movable = false;
            m_Running = false;
            m_Group = false;
            m_MinDelay = minDelay;
            m_MaxDelay = maxDelay;
            m_Count = amount;
            m_Team = team;
            m_HomeRange = homeRange;
            m_WalkingRange = -1;
            m_SpawnNames = spawnNames;
            m_Spawned = new List < ISpawnable >();
        }

        public IntelliSpawner(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel < AccessLevel.GameMaster)
                return;

            from.SendGump(new IntelliSpawnerGump(this));
            from.SendGump(new Gumps.PropertiesGump(from, this));
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_Running)
            {
                list.Add(1060742); // active

                list.Add(1060656, m_Count.ToString()); // amount to make: ~1_val~
                list.Add(1061169, m_HomeRange.ToString()); // range ~1_val~
                list.Add(1060658, "walking range\t{0}", m_WalkingRange); // ~1_val~: ~2_val~

                list.Add(1060659, "group\t{0}", m_Group); // ~1_val~: ~2_val~
                list.Add(1060660, "team\t{0}", m_Team); // ~1_val~: ~2_val~
                list.Add(1060661, "speed\t{0} to {1}", m_MinDelay, m_MaxDelay); // ~1_val~: ~2_val~

                if (m_SpawnNames.Count != 0)
                    list.Add(SpawnedStats());
            }
            else
            {
                list.Add(1060743); // inactive
            }
        }

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);

            if (m_Running)
                LabelTo(from, "[Running]");
            else
                LabelTo(from, "[Off]");
        }

        public void Start()
        {
            if (!m_Running)
            {
                if (SpawnNamesCount > 0)
                {
                    m_Running = true;
                    DoTimer();
                }
            }
        }

        public void Stop()
        {
            if (m_Running)
            {
                if (m_Timer != null)
                    m_Timer.Stop();

                m_Running = false;
            }
        }

        public static string ParseType(string s)
        {
            return s.Split(null, 2)[0];
        }

        public void Defrag()
        {
            for (int i = 0; i < m_Spawned.Count; ++i)
            {
                ISpawnable e = m_Spawned[i];

                bool toRemove = false;

                var item = e as Item;
                if (item != null && (item.Deleted || item.Parent != null))
                {
                    toRemove = true;
                }
                else
                {
                    var mobile = e as Mobile;
                    if (mobile != null)
                    {
                        if (mobile.Deleted)
                        {
                            toRemove = true;
                        }
                        else
                        {
                            var baseCreature = mobile as BaseCreature;
                            if (baseCreature != null && (baseCreature.Controlled || baseCreature.IsStabled))
                            {
                                toRemove = true;
                            }
                        }
                    }
                }

                if (toRemove)
                {
                    InvalidateProperties();
                    m_Spawned.RemoveAt(i);
                    --i;
                }
            }
        }

        bool ISpawner.UnlinkOnTaming
        {
            get
            {
                return true;
            }
        }

        void ISpawner.Remove(ISpawnable spawn)
        {
            m_Spawned.Remove(spawn);

            InvalidateProperties();
        }

        public void OnTick()
        {
            DoTimer();

            bool stayActive = false;

            foreach (var spawned in m_Spawned)
            {
                var mobile = spawned as Mobile;
                if (mobile != null)
                {
                    foreach (Mobile m in mobile.GetMobilesInRange(Core.GlobalMaxUpdateRange))
                    {
                        if (ValidTrigger(m))
                        {
                            stayActive = true;
                            break;
                        }
                    }
                }
                else
                {
                    var item = spawned as Item;
                    if (item != null)
                    {
                        foreach (Mobile m in item.GetMobilesInRange(Core.GlobalMaxUpdateRange))
                        {
                            if (ValidTrigger(m))
                            {
                                stayActive = true;
                                break;
                            }
                        }
                    }
                }
                if (stayActive)
                {
                    break;
                }
            }

            if (stayActive)
            {
                Defrag();

                if (IsEmpty)
                {
                    Respawn();
                }
                else
                {
                    Spawn();
                }
            }
            else
            {
                RemoveSpawned();
                m_Running = false;
            }
        }

        public override bool HandlesOnMovement
        {
            get
            {
                return true;
            }
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (!m_Running && ValidTrigger(m))
            {
                m_Running = true;
                Respawn();
                DoTimer();
            }
        }

        public virtual bool ValidTrigger(Mobile m)
        {
            var baseCreature = m as BaseCreature;
            if (baseCreature != null)
            {
                BaseCreature bc = baseCreature;

                if (!bc.Controlled && !bc.Summoned)
                    return false;
            }
            else if (!m.Player)
            {
                return false;
            }

            return true;
        }

        public virtual void Respawn()
        {
            RemoveSpawned();

            for (int i = 0; i < m_Count; i++)
                Spawn();
        }

        public virtual void Spawn()
        {
            if (SpawnNamesCount > 0)
                Spawn(Utility.Random(SpawnNamesCount));
        }

        public void Spawn(string creatureName)
        {
            for (int i = 0; i < m_SpawnNames.Count; i++)
            {
                if (m_SpawnNames[i] == creatureName)
                {
                    Spawn(i);
                    break;
                }
            }
        }

        protected virtual ISpawnable CreateSpawnedObject(int index)
        {
            if (index >= m_SpawnNames.Count)
                return null;

            Type type = ScriptCompiler.FindTypeByName(ParseType(m_SpawnNames[index]));

            if (type != null)
            {
                try
                {
                    return Build(type, CommandSystem.Split(m_SpawnNames[index]));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return null;
        }

        public static ISpawnable Build(Type type, string[] args)
        {
            bool isISpawnable = typeof(ISpawnable).IsAssignableFrom(type);

            if (!isISpawnable)
            {
                return null;
            }

            Add.FixArgs(ref args);

            string[, ] props = null;

            for (int i = 0; i < args.Length; ++i)
            {
                if (Insensitive.Equals(args[i], "set"))
                {
                    int remains = args.Length - i - 1;

                    if (remains >= 2)
                    {
                        props = new string[remains / 2, 2];

                        remains /= 2;

                        for (int j = 0; j < remains; ++j)
                        {
                            props[j, 0] = args[i + (j * 2) + 1];
                            props[j, 1] = args[i + (j * 2) + 2];
                        }

                        Add.FixSetString(ref args, i);
                    }

                    break;
                }
            }

            PropertyInfo[] realProps = null;

            if (props != null)
            {
                realProps = new PropertyInfo[props.GetLength(0)];

                PropertyInfo[] allProps = type.GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);

                for (int i = 0; i < realProps.Length; ++i)
                {
                    PropertyInfo thisProp = null;

                    string propName = props[i, 0];

                    for (int j = 0; thisProp == null && j < allProps.Length; ++j)
                    {
                        if (Insensitive.Equals(propName, allProps[j].Name))
                            thisProp = allProps[j];
                    }

                    if (thisProp != null)
                    {
                        CPA attr = Properties.GetCPA(thisProp);

                        if (attr != null && AccessLevel.GameMaster >= attr.WriteLevel && thisProp.CanWrite && !attr.ReadOnly)
                            realProps[i] = thisProp;
                    }
                }
            }

            ConstructorInfo[] ctors = type.GetConstructors();

            foreach (var ctor in ctors)
            {
                if (!Add.IsConstructable(ctor, AccessLevel.GameMaster))
                    continue;
                ParameterInfo[] paramList = ctor.GetParameters();
                if (args.Length == paramList.Length)
                {
                    object[] paramValues = Add.ParseValues(paramList, args);
                    if (paramValues == null)
                        continue;
                    object built = ctor.Invoke(paramValues);
                    if (built != null && realProps != null)
                    {
                        for (int j = 0; j < realProps.Length; ++j)
                        {
                            if (realProps[j] == null)
                                continue;
                            Properties.InternalSetValue(built, realProps[j], props[j, 1]);
                        }
                    }
                    return (ISpawnable)built;
                }
            }

            return null;
        }

        public Point3D HomeLocation
        {
            get
            {
                return Location;
            }
        }

        public virtual bool CheckSpawnerFull()
        {
            return (m_Spawned.Count >= m_Count);
        }

        public void Spawn(int index)
        {
            Map map = Map;

            if (map == null || map == Map.Internal || SpawnNamesCount == 0 || index >= SpawnNamesCount || Parent != null)
                return;

            Defrag();

            if (CheckSpawnerFull())
                return;

            ISpawnable spawned = CreateSpawnedObject(index);

            if (spawned == null)
                return;

            spawned.Spawner = this;
            m_Spawned.Add(spawned);

            Point3D loc = (spawned is BaseVendor ? Location : GetSpawnPosition(spawned));

            spawned.OnBeforeSpawn(loc, map);
            spawned.MoveToWorld(loc, map);
            spawned.OnAfterSpawn();

            var baseCreature = spawned as BaseCreature;
            if (baseCreature != null)
            {
                baseCreature.RangeHome = m_WalkingRange >= 0 ? m_WalkingRange : m_HomeRange;

                baseCreature.CurrentWayPoint = WayPoint;

                baseCreature.SeeksHome = MobilesSeekHome;

                if (m_Team > 0)
                    baseCreature.Team = m_Team;

                baseCreature.Home = (UsesSpawnerHome) ? HomeLocation : baseCreature.Location;
            }

            InvalidateProperties();
        }

        public Point3D GetSpawnPosition()
        {
            return GetSpawnPosition(null);
        }

        static int GetAdjustedLocation(int range, int side, int coord, int coordThis)
        {
            return (((coord > 0) ? coord : (coordThis - range)) + (Utility.Random(Math.Max((((range * 2) + 1) + side), 1))));
        }

        public Point3D GetSpawnPosition(ISpawnable spawned)
        {
            Map map = Map;

            if (map == null)
                return Location;

            bool waterMob, waterOnlyMob;

            var mobile = spawned as Mobile;
            if (mobile != null)
            {
                Mobile mob = mobile;

                waterMob = mob.CanSwim;
                waterOnlyMob = (mob.CanSwim && mob.CantWalk);
            }
            else
            {
                waterMob = false;
                waterOnlyMob = false;
            }

            for (int i = 0; i < 10; ++i)
            {
                int x = GetAdjustedLocation(m_HomeRange, SpawnArea.Width, SpawnArea.X, X);
                int y = GetAdjustedLocation(m_HomeRange, SpawnArea.Height, SpawnArea.Y, Y);

                int mapZ = map.GetAverageZ(x, y);

                if (IgnoreHousing || ((BaseHouse.FindHouseAt(new Point3D(x, y, mapZ), Map, 16) == null &&
                    BaseHouse.FindHouseAt(new Point3D(x, y, Z), Map, 16) == null)))
                {
                    if (waterMob)
                    {
                        if (IsValidWater(map, x, y, Z))
                            return new Point3D(x, y, Z);

                        if (IsValidWater(map, x, y, mapZ))
                            return new Point3D(x, y, mapZ);
                    }

                    if (!waterOnlyMob)
                    {
                        if (map.CanSpawnMobile(x, y, Z))
                            return new Point3D(x, y, Z);

                        if (map.CanSpawnMobile(x, y, mapZ))
                            return new Point3D(x, y, mapZ);
                    }
                }
            }

            return Location;
        }

        public static bool IsValidWater(Map map, int x, int y, int z)
        {
            if (!Region.Find(new Point3D(x, y, z), map).AllowSpawn() || !map.CanFit(x, y, z, 16, false, true, false))
                return false;

            LandTile landTile = map.Tiles.GetLandTile(x, y);

            if (landTile.Z == z && (TileData.LandTable[landTile.ID & TileData.MaxLandValue].Flags & TileFlag.Wet) != 0)
                return true;

            StaticTile[] staticTiles = map.Tiles.GetStaticTiles(x, y, true);

            foreach (var staticTile in staticTiles)
            {
                if (staticTile.Z == z && (TileData.ItemTable[staticTile.ID & TileData.MaxItemValue].Flags & TileFlag.Wet) != 0)
                    return true;
            }

            return false;
        }

        public void DoTimer()
        {
            if (!m_Running)
                return;

            int minSeconds = (int)m_MinDelay.TotalSeconds;
            int maxSeconds = (int)m_MaxDelay.TotalSeconds;

            TimeSpan delay = TimeSpan.FromSeconds(Utility.RandomMinMax(minSeconds, maxSeconds));
            DoTimer(delay);
        }

        public virtual void DoTimer(TimeSpan delay)
        {
            if (!m_Running)
                return;

            End = DateTime.UtcNow + delay;

            if (m_Timer != null)
                m_Timer.Stop();

            m_Timer = new InternalTimer(this, delay);
            m_Timer.Start();
        }

        class InternalTimer: Timer
        {
            readonly IntelliSpawner m_Spawner;

            public InternalTimer(IntelliSpawner spawner, TimeSpan delay)
                : base(delay)
            {
                Priority = spawner.IsFull ? TimerPriority.FiveSeconds : TimerPriority.OneSecond;

                m_Spawner = spawner;
            }

            protected override void OnTick()
            {
                if (m_Spawner != null)
                if (!m_Spawner.Deleted)
                    m_Spawner.OnTick();
            }
        }

        public string SpawnedStats()
        {
            Defrag();

            var counts = new Dictionary < string, int >(StringComparer.OrdinalIgnoreCase);

            foreach (string entry in m_SpawnNames)
            {
                string name = ParseType(entry);
                Type type = ScriptCompiler.FindTypeByName(name);

                if (type == null)
                    counts[name] = 0;
                else
                    counts[type.Name] = 0;
            }

            foreach (ISpawnable spawned in m_Spawned)
            {
                string name = spawned.GetType().Name;

                if (counts.ContainsKey(name))
                    ++counts[name];
                else
                    counts[name] = 1;
            }

            var names = new List < string >(counts.Keys);
            names.Sort();

            var result = new StringBuilder();

            for (int i = 0; i < names.Count; ++i)
                result.AppendFormat("{0}{1}: {2}", (i == 0) ? "" : "<BR>", names[i], counts[names[i]]);

            return result.ToString();
        }

        public int CountCreatures(string creatureName)
        {
            Defrag();

            int count = 0;

            for (int i = 0; i < m_Spawned.Count; ++i)
                if (Insensitive.Equals(creatureName, m_Spawned[i].GetType().Name))
                    ++count;

            return count;
        }

        public void RemoveSpawned(string creatureName)
        {
            Defrag();

            for (int i = m_Spawned.Count - 1; i >= 0; --i)
            {
                IEntity e = m_Spawned[i];

                if (Insensitive.Equals(creatureName, e.GetType().Name))
                    e.Delete();
            }

            InvalidateProperties();
        }

        public void RemoveSpawned()
        {
            Defrag();

            for (int i = m_Spawned.Count - 1; i >= 0; --i)
                m_Spawned[i].Delete();

            InvalidateProperties();
        }

        public void BringToHome()
        {
            Defrag();

            foreach (var e in m_Spawned)
            {
                e.MoveToWorld(Location, Map);
            }
        }

        public override void OnDelete()
        {
            base.OnDelete();

            RemoveSpawned();

            if (m_Timer != null)
                m_Timer.Stop();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(6); // version

            writer.Write(MobilesSeekHome);

            writer.Write(IgnoreHousing);

            writer.Write(SpawnArea);

            writer.Write(UsesSpawnerHome);

            writer.Write(m_WalkingRange);

            writer.Write(WayPoint);

            writer.Write(m_Group);

            writer.Write(m_MinDelay);
            writer.Write(m_MaxDelay);
            writer.Write(m_Count);
            writer.Write(m_Team);
            writer.Write(m_HomeRange);
            writer.Write(m_Running);

            if (m_Running)
                writer.WriteDeltaTime(End);

            writer.Write(m_SpawnNames.Count);

            for (int i = 0; i < m_SpawnNames.Count; ++i)
                writer.Write(m_SpawnNames[i]);

            writer.Write(m_Spawned.Count);

            foreach (var e in m_Spawned)
            {
                var item = e as Item;
                var mobile = e as Mobile;

                if (item != null)
                    writer.Write(item);
                else if (mobile != null)
                    writer.Write((Mobile)e);
                else
                    writer.Write(Serial.MinusOne);
            }
        }

        static WarnTimer m_WarnTimer;

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 6:
                    {
                        MobilesSeekHome = reader.ReadBool();
                        UsesSpawnerHome = reader.ReadBool();
                        goto
                        case 5;
                    }
                case 5:
                    {
                        SpawnArea = reader.ReadRect2D();
                        UsesSpawnerHome = reader.ReadBool();

                        goto
                        case 4;
                    }
                case 4:
                    {
                        m_WalkingRange = reader.ReadInt();

                        goto
                        case 3;
                    }
                case 3:
                case 2:
                    {
                        WayPoint = reader.ReadItem() as WayPoint;

                        goto
                        case 1;
                    }

                case 1:
                    {
                        m_Group = reader.ReadBool();

                        goto
                        case 0;
                    }

                case 0:
                    {
                        m_MinDelay = reader.ReadTimeSpan();
                        m_MaxDelay = reader.ReadTimeSpan();
                        m_Count = reader.ReadInt();
                        m_Team = reader.ReadInt();
                        m_HomeRange = reader.ReadInt();
                        m_Running = reader.ReadBool();

                        if (m_Running)
                            reader.ReadDeltaTime();

                        int size = reader.ReadInt();

                        m_SpawnNames = new List < string >(size);

                        for (int i = 0; i < size; ++i)
                        {
                            string creatureString = reader.ReadString();

                            m_SpawnNames.Add(creatureString);
                            string typeName = ParseType(creatureString);

                            if (ScriptCompiler.FindTypeByName(typeName) == null)
                            {
                                if (m_WarnTimer == null)
                                    m_WarnTimer = new WarnTimer();

                                m_WarnTimer.Add(Location, Map, typeName);
                            }
                        }

                        int count = reader.ReadInt();

                        m_Spawned = new List < ISpawnable >(count);

                        for (int i = 0; i < count; ++i)
                        {
                            var e = World.FindEntity(reader.ReadInt()) as ISpawnable;

                            if (e != null)
                            {
                                e.Spawner = this;
                                m_Spawned.Add(e);
                            }
                        }

                        if (m_Running)
                        {
                            RemoveSpawned();
                            m_Running = false;
                        }

                        break;
                    }
            }

            if (version < 3 && Math.Abs(Weight) < 1)
                Weight = -1;
        }

        class WarnTimer: Timer
        {
            readonly List < WarnEntry > m_List;

            class WarnEntry
            {
                public Point3D Point;
                public Map Map;
                public string Name;

                public WarnEntry(Point3D p, Map map, string name)
                {
                    Point = p;
                    Map = map;
                    Name = name;
                }
            }

            public WarnTimer()
                : base(TimeSpan.FromSeconds(1.0))
            {
                m_List = new List < WarnEntry >();
                Start();
            }

            public void Add(Point3D p, Map map, string name)
            {
                m_List.Add(new WarnEntry(p, map, name));
            }

            protected override void OnTick()
            {
                try
                {
                    Console.WriteLine("Warning: {0} bad spawns detected, logged: 'badspawn.log'", m_List.Count);

                    using (var op = new StreamWriter("badspawn.log", true))
                    {
                        op.WriteLine("# Bad spawns : {0}", DateTime.UtcNow);
                        op.WriteLine("# Format: X Y Z F Name");
                        op.WriteLine();

                        foreach (WarnEntry e in m_List)
                            op.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", e.Point.X, e.Point.Y, e.Point.Z, e.Map, e.Name);

                        op.WriteLine();
                        op.WriteLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}