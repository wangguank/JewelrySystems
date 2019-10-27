using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Jewelry
{
    public class SendEmailSrc : MonoBehaviour
    {

        const int limitDay = 7;//超过几天发送邮件
                               //发送的路径
        const string filePath = @"C:\Users\wangg\Desktop\NGA\NGATest\Noitom\NGA\Capture\2018-11-29 15-56-47-PM_1_88da0c7a-e7ed-4231-89e9-f2293e90f960_1\mapinfo.ts";
        const string filePath1 = @"C:\Users\wangg\Desktop\NGA\NGATest\Noitom\NGA\Capture\2018-11-29 15-56-47-PM_1_88da0c7a-e7ed-4231-89e9-f2293e90f960_1\Data\SitDown_Stand_Measure.txt";
        //string[] allFilePath = new string[2] { filePath, filePath1 };
        string[] allFilePath ;


        private void SendEmail(string[] path, string currentDateTime)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("unityOpenGL@163.com");//发送人
            mail.To.Add("383866279@qq.com");//收件人
            mail.Subject = "Test Mail";//主题
            mail.Body = "This is for testing SMTP mail from GMAIL";//内容
                                                                   //mail.Attachments.Add(new Attachment("screen.png"));//@"C:test.txt"
            for (int i = 0; i < path.Length; i++)
            {
                if (File.Exists(path[i]))
                    mail.Attachments.Add(new Attachment(path[i]));//@"C:test.txt"
            }
            //mail.Attachments.Add(new Attachment(path));//@"C:test.txt"
            //mail.Attachments.Add(new Attachment(filePath1));//@"C:test.txt"

            SmtpClient smtpServer = new SmtpClient("smtp.163.com");
            //smtpServer.Port = ;
            smtpServer.Credentials = new System.Net.NetworkCredential("unityOpenGL@163.com", "wg1989222") as ICredentialsByHost;//U3D123456  
                                                                                                                                //注意这里填写的账号密码 账号是发送账号，密码是邮箱设置的授权码 ，不是邮箱登录密码
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

            smtpServer.Send(mail);

            if (currentDateTime != "")
                PlayerPrefs.SetString(GETNOWDateTime, currentDateTime.ToString());
            Debug.Log("success");

            Close();
        }

        const string GETNOWDateTime = "GETNOWDateTime";
        void Start()
        {
            string encryptFile = string.Format("{0}/{1}.ts", Application.dataPath + Common.FilePath, "UserData");

            allFilePath = new string[] { encryptFile };
            GetNetTime();//启动软件查询时间并且判断是否发送邮件
        }

        //获取系统时间（不准确）
        private void GetLocalTime()
        {
            CheckSendMail(DateTime.Now);
        }

        //查看是否需要发送邮件
        void CheckSendMail(DateTime currentDateTime)
        {
            string dateTimeStr = "";

            dateTimeStr = PlayerPrefs.GetString(GETNOWDateTime);
            DateTime lastDateTime;
            if (dateTimeStr != "")//不是第一次启动
            {
                lastDateTime = Convert.ToDateTime(dateTimeStr);
                TimeSpan timeSpan = currentDateTime.Subtract(lastDateTime); //当前DateTime - 上一个DateTime

                double totalDays = timeSpan.TotalDays; //总间隔多少天（总数，即全部换算成天）

                double totalHours = timeSpan.TotalHours; //总间隔多少小时

                double totalMinutes = timeSpan.TotalMinutes;//总间隔多少分钟

                double totalSeconds = timeSpan.TotalSeconds; //总间隔多少秒

                double seconds = timeSpan.Seconds; //间隔总时间中的秒数，例如间隔3天10小时10分20秒，返回的是20秒

                if (totalDays >= limitDay)
                {
                    Debug.Log("查询到过了7天，curDateTime：" + currentDateTime.ToString() + "     lastDateTime:" + lastDateTime.ToString());


                    StartCoroutine(StartSendEmail(allFilePath, currentDateTime.ToString()));
                }
                else
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        void Close()
        {
            StartCoroutine(ClosePanel());
        }

        IEnumerator ClosePanel()
        {
            Debug.Log("结束查询，关闭发送邮件面板");
            UIController.Instance.AfterInit();
            yield return 20;

            gameObject.SetActive(false);
        }

        IEnumerator StartSendEmail(string[] allFilePath, string currentDateTime)
        {
            yield return 1;

            SendEmail(allFilePath, currentDateTime);
        }

        //获取网络时间
        private void GetNetTime()
        {
            if (btnJudge_Click())//能联通网络
            {
                //StartCoroutine(GetWWWTime());//查询网络时间
                StartCoroutine(GetWWWTime());
            }
            else
            {
                GetLocalTime();//查询系统时间
            }
        }

        private bool btnJudge_Click()
        {
            int iNetStatus = ConnectHttp.GetNetConnectedState("baidu.com");
            if (iNetStatus == 2 || iNetStatus == 3)
            {
                return true;
            }
            return false;

            if (iNetStatus == 1)
            {
                //"网络未连接";
            }
            else if (iNetStatus == 2)
            {
                //"采用调治解调器上网";
            }
            else if (iNetStatus == 3)
            {
                //"采用网卡上网";
            }
            else if (iNetStatus == 4)
            {
                //"采用调治解调器上网,但是联不通指定网络";
            }
            else if (iNetStatus == 5)
            {
                //"采用网卡上网,但是联不通指定网络";
            }
        }

        IEnumerator GetWWWTime()
        {
            WWW www = new WWW("http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=1");
            yield return www;
            if (www.text == "" || www.text.Trim() == "")//如果获取网络时间失败,改为获取系统时间
            {
                GetLocalTime();//使用本地时间
            }
            else//成功获取网络时间
            {
                string timeStr = www.text.Substring(2);
                System.DateTime time = System.DateTime.MinValue;
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                time = startTime.AddMilliseconds(Convert.ToDouble(timeStr));
                //timeStr = time.ToString();

                //monthAndDay = time.Month + "_" + time.Day;
                Debug.Log(time);

                CheckSendMail(time);
            }

        }


        //下面的代码获取不到时间，可能是网络解析有问题
        public int year, mouth, day, hour, min, sec;

        public string timeURL = "http://cgi.im.qq.com/cgi-bin/cgi_svrtime";
        IEnumerator GetTime111()
        {
            WWW www = new WWW(timeURL);
            while (!www.isDone)
            {
                yield return www;
            }
            SplitTime(www.text);
        }

        void SplitTime(string dateTime)
        {
            dateTime = dateTime.Replace("-", "|");
            dateTime = dateTime.Replace(" ", "|");
            dateTime = dateTime.Replace(":", "|");
            string[] Times = dateTime.Split('|');
            year = int.Parse(Times[0]);
            mouth = int.Parse(Times[1]);
            day = int.Parse(Times[2]);
            hour = int.Parse(Times[3]);
            min = int.Parse(Times[4]);
            sec = int.Parse(Times[5]);

            Debug.LogFormat("year:{0},mouth:{1},day:{2},hour:{3},min:{4},sec:{5},", year, mouth, day, hour, min, sec);
        }
    }

}
