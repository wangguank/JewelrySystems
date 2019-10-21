using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jewelry
{
    public class UserManager : UIPanel
    {
        public GameObject searchPanel;
        public InputField inputField_name;
        public InputField inputField_phone;

        LoopList loopList;

        // Use this for initialization
        void Start()
        {
            Regist();
            loopList = transform.GetComponent<LoopList>();

        }

        public void OnClickShowSearchButton()
        {
            Debug.Log("这里要设置查询面板:"+ searchPanel.activeSelf);
            searchPanel.SetActive(!searchPanel.activeSelf);
        }

        public void OnClickSearchButton()
        {
            Debug.Log("点击了查询按钮,name:"+ inputField_name.text +"    phone:"+ inputField_phone.text);

            if(inputField_phone.text == "" && inputField_name.text == "")
            {
                Debug.Log("啥也没输入");
            }

            if (inputField_phone.text==""&& inputField_name.text!="")
            {
                Debug.Log("只输入了手机");

                UserData userData = DataManager.Instance.GetUserInfoByID(inputField_phone.text);

                if (userData != null)
                {
                    loopList.SearchData(userData);
                    OnClickShowSearchButton();
                }
                else
                {
                    Debug.Log("没有找到");
                }


            }

            if (inputField_phone.text == "" && inputField_name.text != "")
            {
                Debug.Log("只输入了姓名，查询很多个，按钮变成下一个");

                List<UserData> userDatas = DataManager.Instance.GetUserInfoByName(inputField_phone.text);


            }

          

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}