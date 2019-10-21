/************************************************************************************
 * @author   wangjian
 * 按钮颜色
************************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

using Client.Utils;

namespace SHMH.UI{
    public enum ButtonColorStyle{
        DeepBlue,       //深蓝
        LightBlue,      //浅蓝
        DarkGrey,       //深灰
        LightGray,      //浅灰

        Green_ash,      //青灰（绿灰） 不可点击按钮颜色

    }
    public class ButtonColorTransition : MonoBehaviour,IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,IPointerUpHandler 
    {

        public Color NormalColor;
        public Color HighlightedColor;
        public Color PressedColor;
        public Color DisabledColor;

        public ButtonColorStyle style;
        public bool isUGUI = true;
        public bool isInvalid = false;  //按钮是否可见

        public const string Color_DeepBlue_Normal = "367cd6ff";//深蓝
        public const string Color_DeepBlue_Enter = "4294feff";

        public const string Color_LightBlue_Normal = "469cddff";//浅蓝
        public const string Color_LightBlue_Enter = "57b1f5ff";

        public const string Color_DarkGrey_Normal = "4e546bff";//深灰
        public const string Color_DarkGrey_Enter = "6b728fff";

        public const string Color_LightGray_Normal = "8d93a4ff";//浅灰
        public const string Color_LightGray_Enter = "a3abc1ff";

        public const string Color_Green_ash_Normal = "C0CCD7FF";////青灰（绿灰） 不可点击按钮颜色
        public const string Color_Green_ash_Text = "8D93A4FF";




        public Image btnimage;
        void Start () {
            isUGUI = true;
            Button button = GetComponent<Button>();
            Transform imageTrans =  transform.Find("background");
            if(imageTrans!=null){
                btnimage = Common.GetComponet<Image>(imageTrans.gameObject);
            }

            SetColor(style);
            if(isUGUI){
                if(btnimage!=null)
                    button.targetGraphic = btnimage;
                else
                    LogManager.Instance.error("ButtonColorTransition image is null ");

                btnimage.color = Color.white;
                button.transition = UnityEngine.UI.Selectable.Transition.ColorTint;

                ColorBlock cb = new ColorBlock();
                cb.normalColor = NormalColor;
                cb.highlightedColor = HighlightedColor;
                cb.pressedColor = PressedColor;
                cb.disabledColor = DisabledColor;
                cb.colorMultiplier = 1;
                cb.fadeDuration = 0.1f;
                
                button.colors = cb;

                
            }else{//isUGUI==true Button的targetGraphic属性在检测面板一定要时空的
                button.targetGraphic = null;
                button.transition = UnityEngine.UI.Selectable.Transition.None;
                btnimage.color = NormalColor;
                //StartCoroutine("delayedSetButton");
            }

            if(isInvalid){
                button.interactable = false;
                GetComponentInChildren<Text>().color = HexToColor(Color_Green_ash_Text);
            }

        }
        IEnumerator delayedSetButton(){
            btnimage.gameObject.SetActive(false);
            yield return 1;
            btnimage.gameObject.SetActive(true);
            btnimage.color = NormalColor;
            yield break;
        }


        public void SetColor(ButtonColorStyle style){
            if(style == ButtonColorStyle.DeepBlue){
                NormalColor = HexToColor(Color_DeepBlue_Normal);
                HighlightedColor = HexToColor(Color_DeepBlue_Enter);
                PressedColor = NormalColor;
                DisabledColor = NormalColor;
            }else if(style == ButtonColorStyle.LightBlue){
                NormalColor = HexToColor(Color_LightBlue_Normal);
                HighlightedColor = HexToColor(Color_LightBlue_Enter);
                PressedColor = NormalColor;
                DisabledColor = NormalColor;
            }else if(style == ButtonColorStyle.DarkGrey){
                NormalColor = HexToColor(Color_DarkGrey_Normal);
                HighlightedColor = HexToColor(Color_DarkGrey_Enter);
                PressedColor = NormalColor;
                DisabledColor = NormalColor;
            }else if(style == ButtonColorStyle.LightGray){
                NormalColor = HexToColor(Color_LightGray_Normal);
                HighlightedColor = HexToColor(Color_LightGray_Enter);
                PressedColor = NormalColor;
                DisabledColor = NormalColor;
            }else if(style == ButtonColorStyle.Green_ash){
                NormalColor = HexToColor(Color_Green_ash_Normal);
                HighlightedColor = NormalColor;
                PressedColor = NormalColor;
                DisabledColor = NormalColor;
            }


        }
    



        public Color HexToColor(string hex)
        {
            byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            float r = br / 255f;
            float g = bg / 255f;
            float b = bb / 255f;
            float a = cc / 255f;
            return new Color(r, g, b, a);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(!isUGUI){
                btnimage.color = NormalColor;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!isUGUI){
                btnimage.color = HighlightedColor;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!isUGUI){
                btnimage.color = NormalColor;
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if(!isUGUI){
                btnimage.color = HighlightedColor;
            }
        }

    
    }
}
