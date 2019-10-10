using JObject = System.Collections.Generic.Dictionary<string, object>;
using System.Collections.Generic;
using UnityEngine;
using Client.Utils;

namespace Jewelry
{
    //单个货品信息
	public class Cargo : Json_BasicInfo
    {
        public string kind;//品类，因为不知道有多少种，所以这里无法写成枚举
        public float weight;

        public float mainStoneWeight;
        public int mainStoneNumber;

        public float deputyStoneWeight1;
        public int deputyStoneNumber1;

        public float deputyStoneWeight2;
        public int deputyStoneNumber2;

        public float deputyStoneWeight3;
        public int deputyStoneNumber3;

        public float price;//标签价格
        public string _time;//录入时间

        public bool isSell = false;
        float _sellPrice;//售卖价格
        public float sellPrice
        {
            get
            {
                return _sellPrice;
            }
        }
        float _sellDayGoldPrice;//售卖当天金价
        public float sellDayGoldPrice
        {
            get
            {
                return _sellDayGoldPrice;
            }
        }
        public string sellTime;
        public string _mainSeller;
        public string _otherSeller1;
        public string _otherSeller2;
        public string _otherSeller3;


        public Cargo()
        {
            _time = System.DateTime.Now.ToString("yyyy-MM-dd");
        }

        //卖了
        public void Sell(float price,float goldPrice,string mainSeller,string otherSeller1, string otherSeller2, string otherSeller3)
        {
            isSell = true;
            _sellPrice = price;
            _sellDayGoldPrice = goldPrice;
            sellTime = System.DateTime.Now.ToString("yyyy-MM-dd");
            _mainSeller = mainSeller;
            _otherSeller1 = otherSeller1;
            _otherSeller2 = otherSeller2;
            _otherSeller3 = otherSeller3;
        }

        public Cargo(JObject json, int vercode)
        {
            Deserialize(json, vercode);
        }

        public override void Deserialize(JObject json, int versionCode)
        {
            if (json == null) return;
            kind = JsonHelper.GetString(json, "kind");
            weight = JsonHelper.GetFloat(json, "weight");
            mainStoneWeight = JsonHelper.GetFloat(json, "mainStoneWeight");
            mainStoneNumber = JsonHelper.GetInteger(json, "mainStoneNumber");

            deputyStoneWeight1 = JsonHelper.GetFloat(json, "deputyStoneWeight1");
            deputyStoneNumber1 = JsonHelper.GetInteger(json, "deputyStoneNumber1");
            deputyStoneWeight2 = JsonHelper.GetFloat(json, "deputyStoneWeight2");
            deputyStoneNumber2 = JsonHelper.GetInteger(json, "deputyStoneNumber2");
            deputyStoneWeight3 = JsonHelper.GetFloat(json, "deputyStoneWeight3");
            deputyStoneNumber3 = JsonHelper.GetInteger(json, "deputyStoneNumber3");
            price = JsonHelper.GetFloat(json, "price");
            _time= JsonHelper.GetString(json, "_time");

            isSell= JsonHelper.GetBoolean(json, "isSell");

        }

        public override JObject Serialize(int versionCode)
        {
            JObject json = new JObject();
          

            return json;
        }
    }
}
