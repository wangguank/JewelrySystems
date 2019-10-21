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

        public float mainStoneWeight;//主石
        public int mainStoneNumber;

        public float deputyStoneWeight1;//副石
        public int deputyStoneNumber1;

        public float deputyStoneWeight2;//副石
        public int deputyStoneNumber2;

        public float deputyStoneWeight3;//副石
        public int deputyStoneNumber3;

        public float price;//标签价格
        public string _time;//录入时间

        public bool isSell = false;
        float _sellPrice=0;//售卖价格
        public float sellPrice
        {
            get
            {
                return _sellPrice;
            }
        }
        float _sellDayGoldPrice=0;//售卖当天金价
        public float sellDayGoldPrice
        {
            get
            {
                return _sellDayGoldPrice;
            }
        }
        public string sellTime = "";
        public string _mainSeller = "";
        public string _otherSeller1 = "";
        public string _otherSeller2 = "";
        public string _otherSeller3 = "";


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

        //开收据
        public void Receipt()
        {
            if (!isSell) return;

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
            _sellPrice = JsonHelper.GetFloat(json, "_sellPrice");
            _sellDayGoldPrice = JsonHelper.GetFloat(json, "_sellDayGoldPrice");
            sellTime= JsonHelper.GetString(json, "sellTime");
            _mainSeller = JsonHelper.GetString(json, "_mainSeller");
            _otherSeller1 = JsonHelper.GetString(json, "_otherSeller1");
            _otherSeller2 = JsonHelper.GetString(json, "_otherSeller2");
            _otherSeller3 = JsonHelper.GetString(json, "_otherSeller3");

        }
        public override JObject Serialize(int versionCode)
        {
            JObject json = new JObject();
            JsonHelper.SetString(json, "kind", kind);
            JsonHelper.SetFloat(json, "weight", weight);
            JsonHelper.SetFloat(json, "mainStoneWeight", mainStoneWeight);
            JsonHelper.SetInteger(json, "mainStoneNumber", mainStoneNumber);

            JsonHelper.SetFloat(json, "deputyStoneWeight1", deputyStoneWeight1);
            JsonHelper.SetInteger(json, "deputyStoneNumber1", deputyStoneNumber1);
            JsonHelper.SetFloat(json, "deputyStoneWeight2", deputyStoneWeight2);
            JsonHelper.SetInteger(json, "deputyStoneNumber2", deputyStoneNumber2);
            JsonHelper.SetFloat(json, "deputyStoneWeight3", deputyStoneWeight3);
            JsonHelper.SetInteger(json, "deputyStoneNumber3", deputyStoneNumber3);

            JsonHelper.SetFloat(json, "price", price);
            JsonHelper.SetString(json, "_time", _time);

            JsonHelper.SetBoolean(json, "isSell", isSell);
            JsonHelper.SetFloat(json, "_sellPrice", _sellPrice);
            JsonHelper.SetFloat(json, "_sellDayGoldPrice", _sellDayGoldPrice);

            JsonHelper.SetString(json, "sellTime", sellTime);
            JsonHelper.SetString(json, "_mainSeller", _mainSeller);
            JsonHelper.SetString(json, "_otherSeller1", _otherSeller1);
            JsonHelper.SetString(json, "_otherSeller2", _otherSeller2);
            JsonHelper.SetString(json, "_otherSeller3", _otherSeller3);

            return json;
        }
    }
}
