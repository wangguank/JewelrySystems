using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jewelry
{
    public enum UIEnum
    {
        None,
        User,//用户
        Sell,//商品
        Clerk,//店员
        Stock,//库存
        Warning,//警告
        Warning_Button,//带按钮的警告
        Success
    }

    public enum GenderEnum
    {
        Male,
        Female,
    }
    public enum UserType
    {
        None,
        Boss,
        Employee
    }
    public class Common
    {
        public static string FilePath = "UserData";//保存路径
    }
}

