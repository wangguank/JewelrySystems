using Client.Utils;
using JObject = System.Collections.Generic.Dictionary<string, object>;
using System;
namespace Jewelry
{
    //单个会员信息
	public class UserInfo : Json_BasicInfo
    {
        public string userName;
        public GenderEnum gender;
        public string birthday;
        public string phoneNumber;
        public string address;
        public string joinTime;

        public UserInfo() 
        {
            joinTime = DateTime.Now.ToString ("yyyy - MM - dd");
        }

        public UserInfo(JObject json, int vercode)
        {
            Deserialize(json, vercode);
        }

        public override void Deserialize(JObject json, int versionCode)
        {
            if (json == null) return;
            userName = JsonHelper.GetString(json, "userName");
            birthday = JsonHelper.GetString(json, "birthday");
            phoneNumber = JsonHelper.GetString(json, "phoneNumber");
            address = JsonHelper.GetString(json, "address");
            gender = (GenderEnum)JsonHelper.GetInteger(json, "gender");
            joinTime= JsonHelper.GetString(json, "joinTime");
        }

        public override JObject Serialize(int versionCode)
        {
            JObject json = new JObject();
            JsonHelper.SetString(json, "userName", userName);
            JsonHelper.SetString(json, "birthday", birthday);
            JsonHelper.SetString(json, "phoneNumber", phoneNumber);
            JsonHelper.SetString(json, "address", address);
            JsonHelper.SetInteger(json, "gender", (int)gender);
            JsonHelper.SetString(json, "joinTime", joinTime);
            return json;
        }
    }
}
