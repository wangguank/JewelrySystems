using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Jewelry
{
    public class DataManager : MonoBehaviour
    {
        List<UserData> allUserData;
        public static DataManager Instance;
        private void Awake()
        {
            Instance = this;          
        }

        #region add ,del, edit ... method

        public bool isExitUser(string key)
        {
            for (int i = 0; i < allUserData.Count; i++)
            {
                if (allUserData[i].userInfo.phoneNumber == key)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddUser(UserData info)
        {
            if (!isExitUser(info.userInfo.phoneNumber))
            {
                allUserData.Add(info);
            }
            else
            {
                EidtUser(info);
            }

        }

        public void DelUser(UserData info)
        {
            for (int i = 0; i < allUserData.Count; i++)
            {
                if (allUserData[i].userInfo.phoneNumber == info.userInfo.phoneNumber)
                {
                    allUserData[i] = info;
                    break;
                }
            }          
        }

        public void EidtUser(UserData info)
        {

            for (int i = 0; i < allUserData.Count; i++)
            {
                if (allUserData[i].userInfo.phoneNumber == info.userInfo.phoneNumber)
                {
                    allUserData[i] = info;
                    break;
                }
            }
        }

        public UserData GetUserInfoByID(string phone)
        {
            for (int i = 0; i < allUserData.Count; i++)
            {
                if (allUserData[i].userInfo.phoneNumber.Equals(phone))
                {
                    return allUserData[i];
                }
            }
            return null;
        }

        public List<UserData> GetUserInfoByName(string name)
        {
            List<UserData> userDatas = new List<UserData>();
            for (int i = 0; i < allUserData.Count; i++)
            {
                if (allUserData[i].userInfo.userName.Equals(name))
                {
                    userDatas.Add(allUserData[i]);
                }
            }

            if (userDatas.Count > 0) return userDatas;

            return null;
        }


        #endregion
    }
}

