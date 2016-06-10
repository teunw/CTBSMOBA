using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterStatReader
    {
        public const string DefaultFile = "characterstats.json";

        public static List<MemberStats> Read()
        {
            string json = File.ReadAllText(DefaultFile);
            MemberStatsCollection msc = JsonUtility.FromJson<MemberStatsCollection>(json);
            return msc.MemberStats.ToList();
        }

        public static void Write(List<MemberStats> mc)
        {
            MemberStatsCollection msc = new MemberStatsCollection();
            msc.MemberStats = mc.ToArray();
            string json = JsonUtility.ToJson(msc);

            if (File.Exists(DefaultFile))
                File.Delete(DefaultFile);

            Debug.Log("Written file");
            File.WriteAllText(DefaultFile, json, Encoding.UTF8);
        }

    }

    public class MemberStatsCollection
    {
        public MemberStats[] MemberStats;
    }

    [Serializable]
    public class MemberStats
    {
        [SerializeField]
        public int Stamina, Speed;
        [SerializeField]
        public string Name;

        public static MemberStats CreateFromMember(Member m)
        {
            MemberStats ms = new MemberStats
            {
                Speed = m.Speed,
                Stamina = m.Stamina,
                Name = m.PlayerName
            };
            return ms;
        }

        public void ApplyToMember(Member m)
        {
            m.Speed = this.Speed;
            m.Stamina = this.Stamina;
            m.PlayerName = this.Name;
        }
    }

}
