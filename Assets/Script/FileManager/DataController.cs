using Client.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using JObject = System.Collections.Generic.Dictionary<string, object>;

namespace Jewelry
{
    public class DataController  
    {

        public static UserData ReadUsersData()
        {
            JObject json_data = null;

            //检查基本数据文件是否存在，并尝试解析
            //string normalFile = string.Format("{0}/{1}.txt", Application.dataPath + filePath, "userinfo");
            string encryptFile = string.Format("{0}/{1}.ts", Application.dataPath + Common.FilePath, "UserData");
#if !CLIENT_WEB
            //if (File.Exists(normalFile))
            //{
            //    json_data = FileManager.Instance.ReadJsonObject(normalFile);
            //}
            //else
            //{
            //不存在基本数据文件，则尝试以加密方式解析
            if (File.Exists(encryptFile))
                json_data = FileManager.Instance.ReadEncryptZipJsonObject(encryptFile);
            //}
#endif
            if (json_data != null)
            {
                //检查数据文件版本号，如果低于当前系统版本，则升级重写
                int vercode = JsonHelper.GetInteger(json_data, ClientConfig.KEY_DATA_VERSION_CODE);

//                if (vercode != ClientConfig.DataVersionCode)
//                {
//                    if (json_data.ContainsKey(ClientConfig.KEY_DATA_VERSION_CODE))
//                    {
//                        json_data.Remove(ClientConfig.KEY_DATA_VERSION_CODE);
//                    }
//                    json_data.Add(ClientConfig.KEY_DATA_VERSION_CODE, ClientConfig.DataVersionCode);
//#if !CLIENT_WEB
//                    FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);

//                    if (File.Exists(normalFile))
//                    {
//                        File.Delete(normalFile);
//                    }
//#endif
//                }
                return new UserData(json_data, vercode);
            }

            Debug.LogError("没有查询到文件");
            return null;
        }

        public static JObject ReadUsersDataJObject()
        {
            JObject json_data = null;

            //检查基本数据文件是否存在，并尝试解析
            //string normalFile = string.Format("{0}/{1}.txt", Application.dataPath + filePath, "userinfo");
            string encryptFile = string.Format("{0}/{1}.ts", Application.dataPath + Common.FilePath, "UserData");
#if !CLIENT_WEB

            //不存在基本数据文件，则尝试以加密方式解析
            if (File.Exists(encryptFile))
                json_data = FileManager.Instance.ReadEncryptZipJsonObject(encryptFile);

#endif
            return json_data;
        }

        public static void SaveUserData(UserData data)
        {
            WriteUsersData(data, "UserData");
        }

        static bool WriteUsersData(UserData data, string fileName)
        {
            string encryptFile = string.Format("{0}/{1}.ts", Application.dataPath + Common.FilePath, fileName);
            JObject json_data = data.Serialize(ClientConfig.DataVersionCode);
            FileManager.Instance.WriteEncryptZipJsonObject(encryptFile, json_data);
            return true;
        }
    }
}