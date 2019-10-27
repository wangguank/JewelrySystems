using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JObject = System.Collections.Generic.Dictionary<string, object>;

namespace Jewelry
{
    public class UIController : MonoBehaviour
    {
        UIManager _uiManager;
        UIEnum _uiEnum= UIEnum.None;

        public WarningWindows warningWindows;
        public WarningWindows warningWindows_Button;


        public static UIController Instance;
        private void Awake()
        {
            Instance = this;

        }


        //加载文件
        public static void LoadFile()
        {
            JObject json_data = DataController.ReadUsersDataJObject();
            if (json_data != null)
            {
                DataManager.Instance = new DataManager(json_data, ClientConfig.VERSION_CODE_INIT);
            }else
            {
                DataManager.Instance = new DataManager();
            }
            Debug.Log("文件加载完毕");
        }

        private void Start()
        {
            LoadFile();//加载文件           
            _uiManager = transform.GetComponent<UIManager>();
          
        }

        public void AfterInit()
        {
            Debug.Log("加载数据");
            (_uiManager.GetBehavirous(UIEnum.User) as UserManager).SetDatas();
            ChangeUserPanel();//最终要切换到会员页面
        }

        public void ShowWarningPanel(string showWord, Action ac)
        {
            warningWindows.Init(showWord, ac);
        }

        public void ShowWarningPanelButton(string showWord, Action ac)
        {
            warningWindows_Button.Init(showWord, ac);

        }

        void ChangePanel(UIEnum uIEnum)
        {
            if (_uiEnum != UIEnum .None && _uiEnum != uIEnum)
            {
                _uiManager.GetBehavirous(_uiEnum).DisablePanel();
            }

            _uiEnum = uIEnum;

            _uiManager.GetBehavirous(_uiEnum).ShowPanel();
        }

        //去会员管理
        public void ChangeUserPanel()
        {
            ChangePanel(UIEnum.User);
        }

        //去售卖
        public void ChangeSellPanel()
        {
            ChangePanel(UIEnum.Sell);
        }

        //去店员
        public void ChangeClerkPanel()
        {
            ChangePanel(UIEnum.Clerk);
        }

        //去库存
        public void ChangeStockPanel()
        {
            ChangePanel(UIEnum.Stock);
        }
    }

}
