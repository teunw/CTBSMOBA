using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using SimpleJSON;
using UnityEngine;

namespace Assets.Scripts.CharacterReader
{
    public class HttpCharacterReader : ICharacterReader
    {
        public bool GetMembers(ref List<MemberData> members)
        {
            HttpWebResponse response = null;
            WebRequest request = null;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
                request = WebRequest.Create("https://dashcap.teunwillems.nl/data");
                request.Timeout = 1000;
                response = (HttpWebResponse)request.GetResponse();


                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();


                JSONNode node = JSON.Parse(responseFromServer);
                JSONArray array = node["characters"].AsArray;
                IEnumerator enumerator = array.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    object current = enumerator.Current;
                    JSONClass currentNode = JSON.Parse(current.ToString()).AsObject;

                    MemberData md = new MemberData()
                    {
                        Name = currentNode["name"].Value,
                        Speed = (int)Convert.ToDouble(currentNode["speed"].Value),
                        Stamina = (int)Convert.ToDouble(currentNode["stamina"].Value)
                    };

                    members.Add(md);
                    Debug.Log(md);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Could not retrieve characters: " + e.Message);
                if (response != null) response.Close();
                if (request != null) request.Abort();
                return false;
            }
        }
    }
}
