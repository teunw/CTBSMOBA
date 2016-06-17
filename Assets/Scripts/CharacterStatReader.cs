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
        /// <summary>
        /// The default location of the file of the stats to add
        /// </summary>
        public const string DefaultFile = "characterstats.json";

        /// <summary>
        /// Reads the json file to a collection of stats
        /// </summary>
        /// <returns>The list of memberstats, which were read from the file</returns>
        public static List<MemberStats> Read()
        {
            string json = File.ReadAllText(DefaultFile);
            MemberStatsCollection msc = JsonUtility.FromJson<MemberStatsCollection>(json);
            return msc.MemberStats.ToList();
        }

        /// <summary>
        /// Writes the memberstats to a json file to the default location
        /// Note that this will replace the existing file, if it exists
        /// </summary>
        /// <param name="mc">The memberstats needed to write away</param>
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

    /// <summary>
    /// A class used to store away a series of stats of a member
    /// </summary>
    public class MemberStatsCollection
    {
        public MemberStats[] MemberStats;
    }

    /// <summary>
    /// A class used to store away stats of a single member
    /// </summary>
    [Serializable]
    public class MemberStats
    {
        /// <summary>
        /// The trainable stats
        /// </summary>
        [SerializeField]
        public int Stamina, Speed;
        /// <summary>
        /// The name of the member
        /// </summary>
        [SerializeField]
        public string Name;

        /// <summary>
        /// Creates a memberstats object from a given member
        /// </summary>
        /// <param name="m">The specified member to generate memberstats from</param>
        /// <returns>The stats of a member</returns>
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

        /// <summary>
        /// Applies stats to a specified member
        /// </summary>
        /// <param name="m">The given member</param>
        public void ApplyToMember(Member m)
        {
            m.Speed = this.Speed;
            m.Stamina = this.Stamina;
            m.PlayerName = this.Name;
        }
    }

}
