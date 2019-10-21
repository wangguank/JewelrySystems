using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jewelry
{
    public class UIController : MonoBehaviour
    {
        UIManager _uiManager;
        UIEnum _uiEnum= UIEnum.None;

        //加载文件
        public static void LoadFile()
        {

        }

        private void Start()
        {
            LoadFile();//加载文件           
            _uiManager = transform.GetComponent<UIManager>();

            ChangeUserPanel();//最终要切换到会员页面
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
