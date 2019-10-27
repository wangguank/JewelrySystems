using Client.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JObject = System.Collections.Generic.Dictionary<string, object>;

namespace Jewelry
{
    //单个会员信息
    public class UserData : Json_BasicInfo
    {
        public UserInfo userInfo;//会员基本信息
        public List<BuyCargo> userBuyInfos;//购买信息

        public UserData()//第一次开软件的时候
        {
            userInfo = new UserInfo();
            userBuyInfos = new List<BuyCargo>();
        }

        public UserData(JObject json, int vercode)
        {
            Deserialize(json, vercode);
        }

        public override void Deserialize(JObject json, int versionCode)
        {
            if (json == null) return;
            List<object> _List = JsonHelper.Get<List<object>>(json, "userBuyInfos");
            userBuyInfos = _List.Select(o => new BuyCargo(o as JObject, versionCode)).ToList();

            JObject _json= JsonHelper.Get<JObject>(json, "userInfo");
            userInfo = new UserInfo(_json, versionCode);
        }

        public override JObject Serialize(int versionCode)
        {
            JObject json = new JObject();
            List<JObject> userBuyInfolist = userBuyInfos.Select(c => c.Serialize(versionCode)).ToList();
            JsonHelper.Set(json, "userBuyInfos", userBuyInfolist);

            JsonHelper.Set(json, "userInfo", userInfo.Serialize(versionCode));

            return json;
        }
       
    }
}

