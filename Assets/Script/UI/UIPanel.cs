using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Jewelry
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField]
        UIEnum uiEnum;
        // Use this for initialization
        protected void Regist()
        {
            UIManager.Instance.RegistBehavirous(uiEnum, this);
        }

        public virtual void Reset()
        {

        }

        public virtual void DisablePanel()
        {
            Debug.Log("DisablePanel：" + uiEnum);
            if(gameObject.activeSelf)
            gameObject.SetActive(false);
        }

        public virtual void ShowPanel()
        {
            Debug.Log("ShowPanel：" + uiEnum);

            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

        }
    }
}
   

