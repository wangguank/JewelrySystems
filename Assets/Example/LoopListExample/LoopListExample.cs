using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
public class LoopListExample : MonoBehaviour
{
    public DynamicInfinityListRenderer m_Dl;

    public Button m_BtnSetDatas;

    public Button m_BtnMove2Data;

    public Button m_BtnRemoveData;

    public Button m_BtnAddData;
    // Use this for initialization
    void Start () {
	    m_Dl.InitRendererList(OnSelectHandler,null);//初始化的时候计算了需要显示的格子数，并且保存
        m_BtnSetDatas.onClick.AddListener(() =>
        {
            List<int> datas = new List<int>();
            for (int i = 0; i < 500; ++i)
            {
                datas.Add(i);
            }
            m_Dl.SetDataProvider(datas);//设置数据  
                                        //1、配置每个item并保存为字典 
                                        //2、设置Scroll View的Rect 
                                        //3、查询已经生成的单元格，如果不需要显示就setactive(false)
                                        //m_Dl脚本里面有UpdateRender，实时计算显示

        });

	    m_BtnMove2Data.onClick.AddListener(() =>
	    {
	        if (m_Dl.GetDataProvider() != null)
	        {
	            m_Dl.LocateRenderItemAtTarget(24, 1);
	        }
	        else
	        {
	            print("先设置数据吧");
            }

	    });

	    m_BtnRemoveData.onClick.AddListener(() =>
	    {
	        if (m_Dl.GetDataProvider() != null)
	        {
	            if (m_Dl.GetDataProvider().Contains(6))
	            {
	                m_Dl.GetDataProvider().Remove(6);
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
	    });

        m_BtnAddData.onClick.AddListener(() =>
        {
            if (m_Dl.GetDataProvider() != null)
            {
                m_Dl.GetDataProvider().Add(999);
                m_Dl.RefreshDataProvider();
            }
            else
            {
                print("先设置数据吧");
            }
        });
    }

    void OnSelectHandler(DynamicInfinityItem item)
    {
        print("on select "+item.ToString());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
