using JObject = System.Collections.Generic.Dictionary<string, object>;
using System.Collections.Generic;
using UnityEngine;
namespace Jewelry
{
    //单个货品信息
	public class Cargo : Json_BasicInfo
    {

        public Cargo()
        {

        }

        public Cargo(JObject json, int vercode)
        {
            Deserialize(json, vercode);
        }

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
