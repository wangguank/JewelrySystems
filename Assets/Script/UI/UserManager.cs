using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jewelry
{
    public class UserManager : UIPanel
    {
        public CanvasGroup searchPanel;
        public InputField inputField_name;
        public InputField inputField_phone;

        public UserBuyPanel userBuyPanel;

        LoopList loopList;

        // Use this for initialization
        void Start()
        {
            Regist();
            Init();
            loopList = transform.GetComponent<LoopList>();
            if (searchPanel && searchPanel.interactable) OnClickShowSearchButton();
        }

        public void SetDatas()
        {
            if (loopList != null) loopList.SetDatas(DataManager.Instance.allUserData);
        }

        void OnSelectHandler(DynamicInfinityItem item)
        {
            print("on select " + item.ToString());
        }

        public override void ShowPanel()
        {
            base.ShowPanel();
            Debug.Log("进入了会员管理页面");

        }

        public void OnClickShowSearchButton()
        {
            Debug.Log("这里要设置查询面板:"+ searchPanel.interactable);
            bool isOpen = searchPanel.interactable;

            searchPanel.alpha = isOpen ? 1 : 0;
            searchPanel.interactable = isOpen;//子物体的交互
            searchPanel.blocksRaycasts = isOpen;//


        }

        public void OnClickSearchButton()
        {
            Debug.Log("点击了查询按钮,name:"+ inputField_name.text +"    phone:"+ inputField_phone.text);

            if(inputField_phone.text == "" && inputField_name.text == "")
            {
                Debug.Log("啥也没输入");
                return;
            }

            if (inputField_phone.text!="")
            {
                Debug.Log("只输入了手机");

                UserData userData = DataManager.Instance.GetUserInfoByID(inputField_phone.text);

                if (userData != null)
                {
                    //loopList.SearchData(userData);

                    if(inputField_name.text != ""&& userData.userInfo.userName!= inputField_name.text)
                    {
                        Debug.Log("找到了,但是名字跟输入的不同");

                        return;
                    }

                    Debug.Log("找到了");
                    userBuyPanel.SetData(userData);


                    OnClickShowSearchButton();
                }
                else
                {
                    Debug.Log("没有找到");
                }
            }

            if (inputField_name.text != "")
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