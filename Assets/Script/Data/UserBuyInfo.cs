using JObject = System.Collections.Generic.Dictionary<string, object>;
using System.Collections.Generic;
using Client.Utils;
using System.Linq;

namespace Jewelry
{
    //会员单条购买信息
	public class UserBuyInfo : Json_BasicInfo
    {

        List<BuyCargo> buyCargos;
        public UserBuyInfo()
        {
            buyCargos = new List<BuyCargo>();
        }

        public UserBuyInfo(JObject json, int vercode)
        {
            Deserialize(json, vercode);
        }

        public override void Deserialize(JObject json, int versionCode)
        {
            if (json == null) return;
            List<object> _List = JsonHelper.Get<List<object>>(json, "buyCargos");
            buyCargos = _List.Select(o => new BuyCargo(o as JObject, versionCode)).ToList();
        }

        public override JObject Serialize(int versionCode)
        {
            JObject json = new JObject();
            List<JObject> buyCargoslist = buyCargos.Select(c => c.Serialize(versionCode)).ToList();
            JsonHelper.Set(json, "buyCargos", buyCargoslist);

            return json;
        }
    }
}
