using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jewelry
{
    public class LoopList : MonoBehaviour
    {

        public DynamicInfinityListRenderer m_Dl;
        UserManager userManager;

        // Use this for initialization
        void Start()
        {
            if(m_Dl)
             m_Dl.InitRendererList(OnSelectHandler, null);//初始化的时候计算了需要显示的格子数，并且保存  

            userManager = transform.GetComponent<UserManager>();
            
        }



        public void SetDatas(List<UserData> userDatas)
        {
            m_Dl.SetDataProvider(userDatas);
        }

        public void AddData(UserData userDatas)
        {
            if (m_Dl.GetDataProvider() != null&& !m_Dl.GetDataProvider().Contains (userDatas))
            {
                m_Dl.GetDataProvider().Add(userDatas);
                m_Dl.RefreshDataProvider();
            }
            else
            {
                print("先设置数据吧");
            }
        }

        public void RemoveData(UserData userDatas)
        {
            if (m_Dl.GetDataProvider() != null)
            {
                if (m_Dl.GetDataProvider().Contains(userDatas))
                {
                    m_Dl.GetDataProvider().Remove(userDatas);
                    m_Dl.RefreshDataProvider();
                }
                else
                {
                    print("找不到数据");
                }
            }
            else
            {
                print("先设置数据吧");
            }
        }

        public void SearchData(UserData userDatas)
        {
            if (m_Dl.GetDataProvider() != null)
            {
                m_Dl.LocateRenderItemAtTarget(userDatas, 1);
            }
            else
            {
                print("先设置数据吧");
            }
        }

        void OnSelectHandler(DynamicInfinityItem item)
        {
            print("on select " + item.ToString());
        }
    }
}