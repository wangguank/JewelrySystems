/************************************************************************************
 * @author   wangjian
 *  单例
************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Reflection;
using System;


namespace Client.Utils
{
    public class Singleton<T> where T : new()
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                    //_instance = System.Activator.CreateInstance<T>();
                    //Debug.Log("new Instance initialize"+_instance.ToString());
                    Type type = _instance.GetType();
                    MethodInfo method = type.GetMethod("initialize");
                    if (method != null)
                        method.Invoke(_instance as object, null);
                }
                return _instance;
            }
        }
        public virtual void initialize()
        {
            //Debug.Log("new Instance initialize" + _instance.ToString());
        }
    }

    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;
                    if (_instance == null)
                    {
                        string name = typeof(T).ToString();
                        GameObject obj = new GameObject(name);
                        _instance = Common.AddComponent<T>(obj);
                        if (_instance is ResourceManager)
                        {
                            Common.AddComponent<DontDestroyer>(obj);
                        }
                        //Debug.Log("new Instance initialize" + _instance.ToString());
                        Type type = _instance.GetType();
                        MethodInfo method = type.GetMethod("initialize");
                        if (method != null)
                            method.Invoke(_instance as object, null);
                    }
                }
                return _instance;
            }
        }
        public virtual void initialize()
        {
            //Debug.Log("new Instance initialize" + _instance.ToString());
        }
    }



}