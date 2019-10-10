using JObject = System.Collections.Generic.Dictionary<string, object>;
using System.Collections.Generic;
using System.Linq;
using Client.Utils;

namespace Jewelry
{
    //购买货品信息
	public class BuyCargo : Json_BasicInfo
    {
        List<Cargo> cargos;

        string _buyTime;
        string _buyMoney;
        public List<string> _seller;
        public BuyCargo(string buyMoney, List<string> seller)
        {
            _buyTime = System.DateTime.Now.ToString("yyyy-MM-dd");
            _buyMoney = buyMoney;
            _seller = seller;
        }

        public BuyCargo(JObject json, int vercode)
        {
            Deserialize(json, vercode);
        }

        public override void Deserialize(JObject json, int versionCode)
        {
            if (json == null) return;
            List<object> _List = JsonHelper.Get<List<object>>(json, "cargos");
            cargos = _List.Select(o => new Cargo(o as JObject, versionCode)).ToList();

            _buyTime= JsonHelper.GetString(json, "_buyTime");
            _buyMoney = JsonHelper.GetString(json, "_buyMoney");
            _seller = JsonHelper.GetStringList(json, "_seller");
        }

        public override JObject Serialize(int versionCode)
        {
            JObject json = new JObject();
            List<JObject> cargoslist = cargos.Select(c => c.Serialize(versionCode)).ToList();
            JsonHelper.Set(json, "cargos", cargoslist);
            JsonHelper.SetString(json, "_buyTime", _buyTime);
            JsonHelper.SetString(json, "_buyMoney", _buyMoney);
            JsonHelper.SetStringList(json, "_seller", _seller);
            return json;
        }

    }
}
