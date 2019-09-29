using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;


public class ConnectHttp {

    private const int INTERNET_CONNECTION_MODEM = 1;        //因特网连接调制解调器
    private const int INTERNET_CONNECTION_LAN = 2;          //因特网连接局域网
    public Text text;                       //用于在界面上显示网络连接状态
    [DllImport("winInet.dll")]
    private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);

    // Update is called once per frame
    void Update()
    {
       // btnJudge_Click();           //实时判断网络连接状态
    }

    /// <summary>
    /// 判断网络的连接状态
    /// </summary>
    /// 网络状态（1--》未联网；2--》采用调制解调器；3--》采用网卡上网）
    /// <returns></returns>
    public static int GetNetConnectedState(string strNetAdderss)
    {
        int iNetStatus = 0;
        System.Int32 dwFlag = new int();
        if (!InternetGetConnectedState(ref dwFlag, 0))
        {
            //没能连接上网络
            iNetStatus = 1;
        }
        else if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0)
        {
            //采用调治解调器上网,需要进一步判断能否登录具体网站
            if (PingNetAddress(strNetAdderss))
            {
                //可以ping通给定的网址,网络OK
                iNetStatus = 2;
            }
            else
            {
                //不可以ping通给定的网址,网络不OK
                iNetStatus = 4;
            }
        }
        else if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
        {
            //采用网卡上网,需要进一步判断能否登录具体网站
            if (PingNetAddress(strNetAdderss))
            {
                //可以ping通给定的网址,网络OK
                iNetStatus = 3;
            }
            else
            {
                //不可以ping通给定的网址,网络不OK
                iNetStatus = 5;
            }
        }
        return iNetStatus;
    }

    /// <summary>
    /// ping 具体的网址看能否ping通
    /// </summary>
    /// <param name="strNetAdd"></param>
    /// <returns></returns>
    private static bool PingNetAddress(string strNetAdd)
    {
        bool Flage = false;
        System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
        try
        {
            PingReply pr = ping.Send(strNetAdd, 3000);
            if (pr.Status == IPStatus.TimedOut)
            {
                Flage = false;
            }
            if (pr.Status == IPStatus.Success)
            {
                Flage = true;
            }
            else
            {
                Flage = false;
            }
        }
        catch
        {
            Flage = false;
        }
        return Flage;
    }

    //判断方法
    //private void btnJudge_Click(object sender, EventArgs e)

    private void btnJudge_Click()
    {
        int iNetStatus = GetNetConnectedState("baidu.com");
        if (iNetStatus == 1)
        {
            text.text = "网络未连接";
        }
        else if (iNetStatus == 2)
        {
            text.text = "采用调治解调器上网";
        }
        else if (iNetStatus == 3)
        {
            text.text = "采用网卡上网";
        }
        else if (iNetStatus == 4)
        {
            text.text = "采用调治解调器上网,但是联不通指定网络";
        }
        else if (iNetStatus == 5)
        {
            text.text = "采用网卡上网,但是联不通指定网络";
        }
    }

}
