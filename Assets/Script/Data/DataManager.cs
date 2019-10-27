using Client.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JObject = System.Collections.Generic.Dictionary<string, object>;

namespace Jewelry
{
    public class DataManager : MonoBehaviour
    {
        List<UserData> _allUserData;

        public List<UserData> allUserData
        {
            get
            {
                return _allUserData;
            }
        }

        public static DataManager Instance;
        private void Awake()
        {
            Instance = this;          
        }

        public DataManager()
        {
            _allUserData = new List<UserData>();
        }


        public DataManager(JObject json, int vercode)
        {
            Deserialize(json, vercode);
        }

        public JObject Serialize(int vercode)
        {
            JObject json = new JObject();
            List<JObject> userlist = _allUserData.Select(c => c.Serialize(vercode)).ToList();
            JsonHelper.Set(json, "allUserData", userlist);
            return json;
        }

        public void Deserialize(JObject json, int vercode)
        {
            if (json == null) return;
            List<object> _List = JsonHelper.Get<List<object>>(json, "allUserData");
            _allUserData = _List.Select(o => new UserData(o as JObject, vercode)).ToList();
        }

        #region add ,del, edit ... method

        public bool isExitUser(string key)
        {
            for (int i = 0; i < _allUserData.Count; i++)
            {
                if (_allUserData[i].userInfo.phoneNumber == key)
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
                _allUserData.Add(info);
            }
            else
            {
                EidtUser(info);
            }

        }

        public void DelUser(UserData info)
        {
            for (int i = 0; i < _allUserData.Count; i++)
            {
                if (_allUserData[i].userInfo.phoneNumber == info.userInfo.phoneNumber)
                {
                    _allUserData[i] = info;
                    break;
                }
            }          
        }

        public void EidtUser(UserData info)
        {

            for (int i = 0; i < _allUserData.Count; i++)
            {
                if (_allUserData[i].userInfo.phoneNumber == info.userInfo.phoneNumber)
                {
                    _allUserData[i] = info;
                    break;
                }
            }
        }

        public UserData GetUserInfoByID(string phone)
        {
            for (int i = 0; i < _allUserData.Count; i++)
            {
                if (_allUserData[i].userInfo.phoneNumber.Equals(phone))
                {
                    return _allUserData[i];
                }
            }
            return null;
        }

        public List<UserData> GetUserInfoByName(string name)
        {
            List<UserData> userDatas = new List<UserData>();
            for (int i = 0; i < _allUserData.Count; i++)
            {
                if (_allUserData[i].userInfo.userName.Equals(name))
                {
                    userDatas.Add(_allUserData[i]);
                }
            }

            if (userDatas.Count > 0) return userDatas;

            return null;
        }


        #endregion
    }
}

