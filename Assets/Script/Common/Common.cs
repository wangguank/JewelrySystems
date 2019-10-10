﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jewelry
{
    public enum UIEnum
    {
        None,
        User,//用户
        Goods,//商品

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

