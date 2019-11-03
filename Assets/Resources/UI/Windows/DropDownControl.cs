using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropDownControl : MonoBehaviour
{

    Dropdown dropDownItem;

    List<string> temoNames;

    public int count;
    public int startShow;
    public string word;

    void Start()
    {
        dropDownItem = this.GetComponent<Dropdown>();

        temoNames = new List<string>();


        for (int i = 0; i < count; i++)
        {
            temoNames.Add(startShow + i + word);
        }
        temoNames.Add("");
        UpdateDropDownItem(temoNames);
   

    }


    void UpdateDropDownItem(List<string> showNames)
    {
        dropDownItem.options.Clear();
        Dropdown.OptionData temoData;
        for (int i = 0; i < showNames.Count; i++)
        {
            //给每一个option选项赋值
            temoData = new Dropdown.OptionData();
            if(showNames[i].Length == 2)
            {
                showNames[i] = "0" + showNames[i];
            }
            temoData.text = showNames[i];
            //  temoData.image = sprite_list[i];
            dropDownItem.options.Add(temoData);
        }
        ////初始选项的显示
        //dropDownItem.captionText.text = showNames[0];

    }

    void OnDropdownValueChange(int num)
    {
        Debug.Log(dropDownItem.captionText.text);

    }

}

