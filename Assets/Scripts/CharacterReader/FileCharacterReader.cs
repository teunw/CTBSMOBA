using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.CharacterReader
{
    public class FileCharacterReader : ICharacterReader
    {
        public bool GetMembers(ref List<MemberData> members)
        {
            string path = Application.dataPath + "/Resources/Data2.txt";

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(path);

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] lines = line.Split('-');
                    int stamina = Convert.ToInt32(lines[0]);
                    int speed = Convert.ToInt32(lines[1]);
                    string name = lines[2];

                    MemberData m = new MemberData()
                    {
                        Stamina = stamina,
                        Speed = speed,
                        Name = name
                    };
                    members.Add(m);
                }

                reader.Close();
                return true;
            }
            catch
            {
                Debug.LogWarning("Could not read data file");
                return false;
            }
        }
    }
}