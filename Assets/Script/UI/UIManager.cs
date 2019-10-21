using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jewelry
{
    public class UIManager : MonoBehaviour
    {
        Dictionary<UIEnum, UIPanel> allSideObj;
        public static UIManager Instance;
        private void Awake()
        {
            Instance = this;
            allSideObj = new Dictionary<UIEnum, UIPanel>();

        }

        public void RegistBehavirous(UIEnum side, UIPanel obj)
        {
            if (side != UIEnum.None && !allSideObj.ContainsKey(side))
            {
                allSideObj.Add(side, obj);
            }
        }

        public UIPanel GetBehavirous(UIEnum side)
        {
            if (side != UIEnum.None && allSideObj.ContainsKey(side))
            {
                return allSideObj[side];
            }
            return null;
        }

      

    }
}

