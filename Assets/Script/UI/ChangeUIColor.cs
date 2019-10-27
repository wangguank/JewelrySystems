using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Jewelry
{
    public enum ButtonColorStyle
    {
        DeepBlue,       //深蓝
        LightBlue,      //浅蓝
        DarkGrey,       //深灰
        LightGray,      //浅灰

        Green_ash,      //青灰（绿灰） 不可点击按钮颜色

    }

    public class ChangeUIColor : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        public Color NormalColor;
        public Color HighlightedColor;
        public Color PressedColor;
        public Color DisabledColor;

        public ButtonColorStyle style;

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

        public bool isMangImages = false;
        public Image btnimage;
        public Image[] btnimages;

        public void SetColor(ButtonColorStyle style)
        {
            if (style == ButtonColorStyle.DeepBlue)
            {
                NormalColor = HexToColor(Color_DeepBlue_Normal);
                HighlightedColor = HexToColor(Color_DeepBlue_Enter);
                PressedColor = NormalColor;
                DisabledColor = NormalColor;
            }
            else if (style == ButtonColorStyle.LightBlue)
            {
                NormalColor = HexToColor(Color_LightBlue_Normal);
                HighlightedColor = HexToColor(Color_LightBlue_Enter);
                PressedColor = NormalColor;
                DisabledColor = NormalColor;
            }
            else if (style == ButtonColorStyle.DarkGrey)
            {
                NormalColor = HexToColor(Color_DarkGrey_Normal);
                HighlightedColor = HexToColor(Color_DarkGrey_Enter);
                PressedColor = NormalColor;
                DisabledColor = NormalColor;
            }
            else if (style == ButtonColorStyle.LightGray)
            {
                NormalColor = HexToColor(Color_LightGray_Normal);
                HighlightedColor = HexToColor(Color_LightGray_Enter);
                PressedColor = NormalColor;
                DisabledColor = NormalColor;
            }
            else if (style == ButtonColorStyle.Green_ash)
            {
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
            if (!isMangImages)
            {
                btnimage.color = NormalColor;
            }else
            {
                for (int i = 0; i < btnimages.Length; i++)
                {
                    btnimages[i].color = NormalColor;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isMangImages)
            {
                btnimage.color = HighlightedColor;
            }
            else
            {
                for (int i = 0; i < btnimages.Length; i++)
                {
                    btnimages[i].color = HighlightedColor;
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isMangImages)
            {
                btnimage.color = NormalColor;
            }
            else
            {
                for (int i = 0; i < btnimages.Length; i++)
                {
                    btnimages[i].color = NormalColor;
                }
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isMangImages)
            {
                btnimage.color = HighlightedColor;
            }
            else
            {
                for (int i = 0; i < btnimages.Length; i++)
                {
                    btnimages[i].color = HighlightedColor;
                }
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}