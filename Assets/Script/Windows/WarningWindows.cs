using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jewelry
{
    public class WarningWindows : MonoBehaviour 
    {
        public Button _okBtn;
        public Button _cancelBtn;
        public Text _showWord;
        Action _ac;


        public void Init(string showWord,Action ac)
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
            _showWord.text = showWord;
            _ac = ac;
        }

       public void OnClickButton()
        {
            if (_ac != null) _ac();

            gameObject.SetActive(false);

        }

        public void OnClickCancelButton()
        {
            gameObject.SetActive(false);
        }

      
    }
}