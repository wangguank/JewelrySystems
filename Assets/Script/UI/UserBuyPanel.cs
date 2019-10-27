using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Jewelry
{
    public class UserBuyPanel : MonoBehaviour 
    {
        public Text _nameText;//王冠（男）
        public Text _birthdayText;
        public Text _phoneText;
        public Text _addressText;//地址：***
        public Text _buyInformationText;//购买详情：***
        UserData _userData;


        // Use this for initialization
        void Start()
        {

        }

        public void SetData(UserData userData)
        {
            if (userData != null)
            {
                _nameText.text = userData.userInfo.userName;
                _birthdayText.text = userData.userInfo.birthday;
                _phoneText.text = userData.userInfo.phoneNumber;
                _addressText.text = "地址：" + userData.userInfo.address;
                _userData = userData;

                if (userData.userBuyInfos != null&& userData.userBuyInfos.Count>0)
                {
                    var captureLis = from d in userData.userBuyInfos
                                     orderby d._buyTime descending        //按照时间降序排列
                                     select d;


                    _userData.userBuyInfos = captureLis as List<BuyCargo>;

                    _buyInformationText.text = "购买详情：" + GetBuyInfo(userData.userBuyInfos);
                }else
                {
                    _buyInformationText.text = "购买详情：无";

                }

            }
        }

        string GetBuyInfo(List<BuyCargo> userBuyInfos)
        {
            if (userBuyInfos == null) return "无";

            string info = "";

            string kind = "";
            var captureLis = from d in userBuyInfos
                             orderby d.cargo.kind descending        //按照时间降序排列
                             select d;

            int count = 0;
            List<BuyCargo> _userBuyInfos = captureLis as List<BuyCargo>;
            for (int i = 0; i < _userBuyInfos.Count; i++)
            {
                if(kind!= _userBuyInfos[i].cargo.kind)
                {
                    if(i!=0)
                        kind += count.ToString()+"  ";

                    count = 1;
                    kind = _userBuyInfos[i].cargo.kind;
                    info += _userBuyInfos[i].cargo.kind;
                }
                else
                {
                    count++;
                }
            }
            return info;

        }

        public void DeleteUser()
        {
            if (_userData != null)
                //确认弹框
                UIController.Instance.ShowWarningPanelButton("确认删除？", Delete);
        }

        void Delete()
        {
            if (_userData != null)
                DataManager.Instance.DelUser(_userData);
            Debug.Log("删除了用户");
            Reset();
        }

        void Reset()
        {
            _userData = null;
            _nameText.text = "";
            _birthdayText.text = "";
            _phoneText.text = "";
            _addressText.text = "";
            _buyInformationText.text = "";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}