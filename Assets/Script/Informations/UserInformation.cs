using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jewelry
{
    public class UserInformation : DynamicInfinityItem
    {

        public Text _userName;
        public Text _userAge;
        public Text _userPhone;
        public Text _userAddress;
        public Text _userBuyNum;
       
        public Button m_Btn;
        // Use this for initialization
        void Start()
        {
            m_Btn.onClick.AddListener(() =>
            {
                print("Click " + mData.ToString());
            });
        }

        protected override void OnRenderer()
        {
            base.OnRenderer();
            if(mData is UserData)
            {
                UserData userData = mData as UserData;
                _userName.text = userData.userInfo.userName;

                DateTime now = DateTime.Now;

                char[] a = new char[] { '.', '-', '/' };
                string [] b= userData.userInfo.birthday.Split(a);
                int _age = 0;
                if (b.Length == 3)
                {
                    int year = Convert.ToInt32(b[0]);
                    int mouth = Convert.ToInt32(b[1]);
                    int day = Convert.ToInt32(b[2]);

                    _age = now.Year - year;

                    if (now.Month < mouth || (now.Month == mouth && now.Day < day))
                    {
                        _age--;
                    }

                    if (_age < 0)
                    {
                        _age = 0;
                    }
                }
                
                _userAge.text = _age.ToString ();
                _userPhone.text = userData.userInfo.phoneNumber;

                if(userData.userInfo.address.Length > 6)
                {
                    _userAddress.text = userData.userInfo.address.Remove(6, userData.userInfo.address.Length-6) + "...";
                }
                else
                {
                    _userAddress.text = userData.userInfo.address;

                }

                _userBuyNum.text = userData.userBuyInfos.Count.ToString();



            }

            
        }
    }
}