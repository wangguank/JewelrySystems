using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Jewelry
{
    public class UIPanel : MonoBehaviour
    {
        CanvasGroup _canvasGroup;

        [SerializeField]
        UIEnum uiEnum;

        protected void Init()
        {
            _canvasGroup = transform.GetComponent<CanvasGroup>();
            if (_canvasGroup == null) _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }


        public void SetCanvasGroup(bool isOpen)
        {
            _canvasGroup.alpha = isOpen?1:0;
            _canvasGroup.interactable = isOpen;//子物体的交互
            _canvasGroup.blocksRaycasts = isOpen;//
        }

        public bool GetPanelState()
        {
            return _canvasGroup.alpha == 1;
        }


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
            SetCanvasGroup(false);
        }

        public virtual void ShowPanel()
        {
            SetCanvasGroup(true);
        }
    }
}
   

