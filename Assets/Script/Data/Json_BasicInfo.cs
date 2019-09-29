using UnityEngine;
using JObject = System.Collections.Generic.Dictionary<string, object>;
using System;

namespace Jewelry
{
    public abstract class Json_BasicInfo : MonoBehaviour
    {

        public abstract JObject Serialize(int versionCode);

        public abstract void Deserialize(JObject json, int versionCode);
    }

}
