﻿using System.Collections;
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
        public static string FilePath = "/UserData";//保存路径
    }

    public class ClientConfig
    {

        public const string KEY_SOFTWARE_VERSION_CODE = "SoftwareVersionCode";
        public const string KEY_SOFTWARE_VERSION_NAME = "SoftwareVersionName";

        public const string KEY_DATA_VERSION_CODE = "DataVersionCode";
        public const string KEY_DATA_VERSION_NAME = "DataVersionName";

        public const string KEY_SKINCOLOR_VALUE = "SkinColorValue";


        public const string VERSION_NAME_INIT = "0.0.0000";


        public const int VERSION_CODE_INIT = 0;
        public const int VERSION_CODE_1 = 1;
        public const int VERSION_CODE_2 = 2;
        public const int VERSION_CODE_3 = 3;
        public const int VERSION_CODE_4 = 4;
        public const int VERSION_CODE_5 = 5;
        public const int VERSION_CODE_6 = 6;
        public const int VERSION_CODE_7 = 7;
        public const int VERSION_CODE_8 = 8;
        public const int VERSION_CODE_9 = 9;
        public const int VERSION_CODE_10 = 10;
        public const int VERSION_CODE_11 = 11;
        public const int VERSION_CODE_12 = 12;
        public const int VERSION_CODE_13 = 13;
        public const int VERSION_CODE_14 = 14;
        public const int VERSION_CODE_15 = 15;
        public const int VERSION_CODE_16 = 16;
        public const int VERSION_CODE_17 = 17;
        public const int VERSION_CODE_18 = 18;
        public const int VERSION_CODE_19 = 19;

        //软件版本号，“主版本.子版本.发布日期”
        public static string Version = "1.0.20191027";

        //软件版本号，初始为0，为便于管理，这里使用snv主版本的版本号
        public static int SoftwareVersionCode = VERSION_CODE_INIT;
      
        //数据版本号，“主版本.子版本.发布日期”
        public static string DataVersion = "1.0.1027";

        //数据版本号，初始为0，仅当数据结构发生变化时才升级
        //2015-10-22 升级为VERSION_CODE_1，对于hd数据内所有字段进行精简，压缩存储空间；
        //2015-12-23 升级为VERSION_CODE_2，对于hd数据进行zip之后加密存储
        public static int DataVersionCode = VERSION_CODE_INIT;

		public static int width = 1280;
		public static int height = 800;
		public static bool isFirst = false;
		public static bool isIntroduce = true;
		public static bool isLogin = false;// false login ui  ture already login
		public static bool dynamicLoad = false;

        public static int NetVesrion;

    }
}

