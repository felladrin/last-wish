using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Custom.Quests.LordBritishsBooks
{
    public class LBBbaseCreature : BaseCreature
    {
        public LBBbaseCreature()
            : base(AIType.AI_Melee, FightMode.None, 2, 1, 0.5, 2)
        {
            InitStats(30, 30, 30);
            Blessed = true;
        }

        public List<String> Speech = new List<String>();

        public Timer SpeechTimer = null;

        public int SpeechIndex = 0;

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m is PlayerMobile && m.Alive && !m.Hidden && InLOS(m) && InRange(m, 4) && !Hidden && !Squelched)
            {
                Direction = GetDirectionTo(m.Location);

                if (SpeechTimer == null)
                {
                    SpeechTimer = Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(5.0), new TimerCallback(Speech_Callback));
                    Frozen = true;
                }
            }
        }

        public void Speech_Callback()
        {
            if (SpeechIndex < 0 || SpeechIndex >= Speech.Count)
            {
                if (SpeechTimer != null)
                    SpeechTimer.Stop();

                Frozen = false;
                SpeechTimer = null;
                SpeechIndex = 0;
            }
            else
            {
                Say(Speech[SpeechIndex]);
                SpeechIndex++;
            }
        }

        public void AddSpeech(String text)
        {
            Speech.Add(text);
        }

        public LBBbaseCreature(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
