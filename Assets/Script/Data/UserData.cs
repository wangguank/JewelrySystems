using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JObject = System.Collections.Generic.Dictionary<string, object>;

namespace Jewelry
{
    public class UserData : Json_BasicInfo
    {


        public override void Deserialize(JObject json, int versionCode)
        {
            if (json == null) return;

        }

        public override JObject Serialize(int versionCode)
        {
            JObject json = new JObject();


            return json;
        }
       
    }
}

