using System;
using System.Text.RegularExpressions;


///信息校验
public class UserInformationCheckout  {
  
    //名称 
    public static bool nameJudge(string userName)
    {
        string chinese = "[\u4E00-\u9FA5]$";//中文
        string english = "[A-Za-z]$";//英文
        if(Regex.IsMatch(userName, chinese)&&userName .Length <11)
        {
           // Debug.LogError("userName:" + userName + "通过");
            return true;
        }
        if (Regex.IsMatch(userName, english) && userName.Length < 16)
        {
          //  Debug.LogError("userName:" + userName + "通过");
            return true;
        }
        return false;

    }
    //身高
    public static bool userHeight(string  height)
    {        
        if (Convert.ToInt32(height) < 231 && Convert.ToInt32(height) > -1)
        {
            return true ;
        }
        else
        {
            return false ;
        }
      
    }
    //体重
    public static bool userWeight(string weight)
    {
        int count = 0;
        if (weight.Contains("."))
        {
            for (int i = 0; i < weight.Length ; i++)
            {
                if(weight[i].Equals('.'))
                {
                    count = i;
                }
            }
            string standard = @"^\d{2}(.\d){0,2}$|^\d{3}(.\d){0,2}$|^\d{1}(.\d){0,2}$";
            if (Regex.IsMatch(weight, standard))
            {
                    return true;
            }else
            {
                return false;
            }
        }else
        {
            count = weight.Length;
        }

        if (weight != ""&& Convert.ToInt32(weight)>0&& count<4)
        {
            return true;
        }
        return false;     
    }

    public static bool userAge(string age)
    {      
        return Regex.IsMatch(age, @"[1-9]{1}[0-9]{1}$"); ;
    }

    //单位
    public static bool userCompany (string company)
    {
        string chinese = "[\u4E00-\u9FA50-9]$";//中文+数字
        string english = "[A-Za-z0-9]$";//英文+数字
        if (Regex.IsMatch(company, chinese) && company.Length < 16)
        {
           // Debug.LogError("userName:" + company + "通过");
            return true;
        }
        if (Regex.IsMatch(company, english) && company.Length < 26)
        {
           // Debug.LogError("company:" + company + "通过");
            return true;
        }
        return false;
    }
    //电话
    public static bool userPhone(string number)
    {
        if (number != "")
        {
            if(number.Length >=8&& number.Length <= 11)
            return true;
        }
        return false;
       // return Regex.IsMatch(number, @"^(0|86|17951)?(13[0-9]|15[012356789]|17[013678]|18[0-9]|14[57])[0-9]{8}$");
    }

    //隐藏中间4位
    public static string  getPhone(string number)
    {

        Regex re = new Regex(@"(\d{3})(\d{4})(\d{4})", RegexOptions.None);
        number = re.Replace(number, "$1****$3");
        return number;
    }
}
