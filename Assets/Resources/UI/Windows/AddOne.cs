using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;

using Client.Utils;
using UnityEngine.EventSystems;


namespace Jewelry
{
    public class AddOne : MonoBehaviour
    {

        #region   玩家信息
        Text userNumber;               //玩家编号
        InputField userName;           //
        Toggle man;                    //
        Toggle woman;                  //
        InputField height;             //
        InputField weight;             //
        InputField company;            //
        InputField phoneNumber;        //
        Toggle isGoOn;                 //
                                       // Text brithday;
                                       // InputField brithday;
        GameObject age_Year;
        GameObject age_Mouth;
        GameObject age_Day;

        Text age_Year_Text;
        Text age_Mouth_Text;
        Text age_Day_Text;

        #endregion
        RectTransform PC_MainRootUI;

        // DataManager dataManager;
        UserInformationCheckout checkout;
        string birthdayInformation;

        GameObject nameIsPass;
        GameObject ageIsPass;
        GameObject heightIsPass;
        GameObject weightIsPass;
        GameObject companyIsPass;
        GameObject phoneIsPass;

        //List<UserInfo> userList;

        // Use this for initialization
        void Start()
        {

            PC_MainRootUI = gameObject.GetComponent<RectTransform>();
            userNumber = transform.Find("Background/UserNumber").GetComponent<Text>();
            userName = transform.GetChild(1).Find("InputField_Name").GetComponent<InputField>();
            man = transform.GetChild(1).Find("Toggle_Man").GetComponent<Toggle>();
            woman = transform.GetChild(1).Find("Toggle_Woman").GetComponent<Toggle>();
            height = transform.GetChild(1).Find("InputField_Height").GetComponent<InputField>();
            weight = transform.GetChild(1).Find("InputField_Weight").GetComponent<InputField>();
            company = transform.GetChild(1).Find("InputField_Company").GetComponent<InputField>();
            phoneNumber = transform.GetChild(1).Find("InputField_Phone").GetComponent<InputField>();
            isGoOn = transform.GetChild(1).Find("Toggle_GoOn").GetComponent<Toggle>();
            //  brithday = transform.GetChild(0).Find("DatePicker 1 (1)").GetChild(0).GetComponent<Text>();  //选择出生年月日

            age_Year = transform.GetChild(1).Find("Dropdown_Age/Dropdown_Year").gameObject;
            age_Mouth = transform.GetChild(1).Find("Dropdown_Age/Dropdown_Month").gameObject;
            age_Day = transform.GetChild(1).Find("Dropdown_Age/Dropdown_Day").gameObject;

            age_Year_Text = age_Year.transform.Find("Label").GetComponent<Text>();
            age_Mouth_Text = age_Mouth.transform.Find("Label").GetComponent<Text>();
            age_Day_Text = age_Day.transform.Find("Label").GetComponent<Text>();

            // brithday = transform.GetChild(1).Find("InputField_Age").GetComponent<InputField>();


            nameIsPass = userName.transform.Find("IsPass").gameObject;
            ageIsPass = transform.GetChild(1).Find("Dropdown_Age/IsPass").gameObject;
            heightIsPass = height.transform.Find("IsPass").gameObject;
            weightIsPass = weight.transform.Find("IsPass").gameObject;
            companyIsPass = company.transform.Find("IsPass").gameObject;
            phoneIsPass = phoneNumber.transform.Find("IsPass").gameObject;

            birthdayInformation = age_Year_Text.text + age_Mouth_Text.text + age_Day_Text.text;

            checkout = new UserInformationCheckout();

        }


        //初始化
        public void Init(int number)
        {
            this.Start();

            Reset();
        }
        string giveUserNum;
        private void Reset()
        {

            giveUserNum = Guid.NewGuid().ToString();

            userNumber.text = giveUserNum.Remove(0, giveUserNum.Length - 5);
            userName.text = "";
            man.isOn = true;
            woman.isOn = false;
            height.text = "";
            weight.text = "";
            company.text = "";
            phoneNumber.text = "";
            isGoOn.isOn = false;
            // brithday.text = "";
            age_Year.GetComponent<Dropdown>().captionText.text = "";
            age_Mouth.GetComponent<Dropdown>().captionText.text = "";
            age_Day.GetComponent<Dropdown>().captionText.text = "";

            StartCoroutine(setBir());

            //age_Year_Text.text = "";
            //age_Mouth_Text.text = "";
            //age_Day_Text.text = "";


            nameIsPass.SetActive(false);
            ageIsPass.SetActive(false);
            heightIsPass.SetActive(false);
            weightIsPass.SetActive(false);
            companyIsPass.SetActive(false);
            phoneIsPass.SetActive(false);


        }


        IEnumerator setBir()
        {
            yield return 5;
            age_Year.GetComponent<Dropdown>().value = 100;
            age_Mouth.GetComponent<Dropdown>().value = 12;
            age_Day.GetComponent<Dropdown>().value = 31;
        }

        public void NamePass()
        {
            if (UserInformationCheckout.nameJudge(userName.text) && nameIsPass.activeSelf == false)
            {
                nameIsPass.SetActive(true);
            }
            else if (!UserInformationCheckout.nameJudge(userName.text) && nameIsPass.activeSelf == true)
            {
                nameIsPass.SetActive(false);
            }
        }
        public void HeightPass()
        {
            if (UserInformationCheckout.userHeight(height.text) && heightIsPass.activeSelf == false)
            {
                heightIsPass.SetActive(true);
            }
            else if (!UserInformationCheckout.userHeight(height.text) && heightIsPass.activeSelf == true)
            {
                heightIsPass.SetActive(false);
            }
        }
        public void WeightPass()
        {
            if (UserInformationCheckout.userWeight(weight.text) && weightIsPass.activeSelf == false)
            {
                weightIsPass.SetActive(true);
            }
            else if (!UserInformationCheckout.userWeight(weight.text) && weightIsPass.activeSelf == true)
            {
                weightIsPass.SetActive(false);
            }
        }
        public void CompanyPass()
        {
            if (UserInformationCheckout.userCompany(company.text) && companyIsPass.activeSelf == false)
            {
                companyIsPass.SetActive(true);

            }
            else if (!UserInformationCheckout.userCompany(company.text) && companyIsPass.activeSelf == true)
            {
                companyIsPass.SetActive(false);
            }
        }
        public void PhonePass()
        {
            if (/*!DataManager.Instance.usersData.isExitUser(phoneNumber.text) && */UserInformationCheckout.userPhone(phoneNumber.text) && phoneIsPass.activeSelf == false)
            {
                phoneIsPass.SetActive(true);
            }
            else if (!UserInformationCheckout.userPhone(phoneNumber.text) && phoneIsPass.activeSelf == true)
            {
                phoneIsPass.SetActive(false);
            }
        }
        public void BirthdayPass()
        {
            year = Convert.ToInt32(age_Year.GetComponent<Dropdown>().captionText.text.Replace("年", ""));
            mouth = Convert.ToInt32(age_Mouth.GetComponent<Dropdown>().captionText.text.Replace("月", ""));
            day = Convert.ToInt32(age_Day.GetComponent<Dropdown>().captionText.text.Replace("日", ""));

            if (mouth == 2 | mouth == 4 | mouth == 6 | mouth == 9 | mouth == 11)
            {
                if (day < 0)
                {
                    ageIsPass.SetActive(false);
                    birthdayPass = false;
                    return;
                }
                if (day > 30)
                {
                    ageIsPass.SetActive(false);
                    birthdayPass = false;
                }
                else
                {
                    ageIsPass.SetActive(true);
                    birthdayPass = true;
                }
                if (mouth == 2 && year % 4 == 0)
                {
                    if (day > 29)
                    {
                        ageIsPass.SetActive(false);
                        birthdayPass = false;
                    }
                    else
                    {
                        ageIsPass.SetActive(true);
                        birthdayPass = true;
                    }
                }
                if (mouth == 2 && year % 4 != 0)
                {
                    if (day > 28)
                    {
                        ageIsPass.SetActive(false);
                        birthdayPass = false;
                    }
                    else
                    {
                        ageIsPass.SetActive(true);
                        birthdayPass = true;
                    }
                }
            }
            else if (year != -1 && mouth != -1 && day != -1)
            {
                ageIsPass.SetActive(true);
                birthdayPass = true;
            }
        }


        public bool BirthdayPass(int year, int mouth, int day)
        {
            if (mouth < 1 | mouth > 12)
            {
                return false;
            }
            if (day < 1 | day > 31)
            {
                return false;
            }
            if (mouth == 2 && year % 4 == 0)
            {
                if (day > 29)
                {
                    return false;
                }
            }
            if (mouth == 2 && year % 4 != 0)
            {
                if (day > 28)
                {
                    return false;
                }

            }
            if (mouth == 4 | mouth == 6 | mouth == 9 | mouth == 11)
            {
                if (day > 30)
                {
                    return false;
                }

            }
            return true;

        }
        int year;
        int mouth;
        int day;
        bool birthdayPass;



        private bool IsFocusOnInputText()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                return false;
            if (EventSystem.current.currentSelectedGameObject.GetComponent<InputField>() != null)
                return true;
            return false;
        }

        //关闭
        public void Close()
        {
            Destroy(gameObject);
        }
        //导入EXCEL按钮
        public void AddAll()
        {
            ChooseExcel();
        }
        //添加单个
        public void Done()
        {

            if (!isGoOn.isOn)
            {
                //不继续添加  
                AddOneInfo(isGoOn.isOn);

            }
            else
            {
                //继续添加
                AddOneInfo(isGoOn.isOn);

                Reset();

            }
        }
        //添加一个
        void AddOneInfo(bool goOn)
        {

            if (userName.text == "" | company.text == "" | height.text == "" | phoneNumber.text == "" | weight.text == "")
            {
                Debug.Log("请完善信息");
                return;
            }
            if (birthdayInformation == age_Year_Text.text + age_Mouth_Text.text + age_Day_Text.text | age_Year_Text.text == "" | age_Mouth_Text.text == "" | age_Day_Text.text == "")
            {
                Debug.Log("请更改生日");
                return;
            }
            if ((!man.isOn && !woman.isOn))
            {
                Debug.Log("请选择性别");
                return;
            }

            if (!UserInformationCheckout.nameJudge(userName.text))
            {
                Debug.Log("用户名输入错误，2-10个汉字以内或15个英文字母");
                return;
            }
            if (!UserInformationCheckout.userCompany(company.text))
            {
                Debug.Log("公司名输入错误，15个汉字以内或25个英文字母");
                return;
            }
            if (!UserInformationCheckout.userHeight(height.text))
            {
                Debug.Log("请输入一个有效的身高（0-230），单位cm");
                return;
            }

            if (!phoneIsPass.activeSelf)
            {

                //if (DataManager.Instance.usersData.isExitUser(phoneNumber.text))
                {
                    Debug.Log("手机号已经注册");

                }
            }
            if (!UserInformationCheckout.userPhone(phoneNumber.text))
            {
                Debug.Log("手机号输入错误");
                return;
            }
            if (!UserInformationCheckout.userWeight(weight.text))
            {
                Debug.Log("请输入一个有效的重量");
                return;
            }
            if (!ageIsPass.activeSelf)
            {
                Debug.Log("请更改生日");
                //请更改生日
                return;
            }

            //for (int i = 0; i < DataManager.Instance.usersData.userList.Count; i++)
            //{
            //    if (DataManager.Instance.usersData.isExitUser (phoneNumber.text))
            //    {
            //        PromptManager.Instance.ShowPrompt(5020, null, null);
            //        Debug.Log("手机号已经注册");

            //        //手机号已经注册了
            //        return;
            //    }
            //}

            if (man.isOn)
            {

                SaveBrn(userName.text, GenderEnum.Male,Convert.ToInt32( height.text), age_Year_Text.text + age_Mouth_Text.text + age_Day_Text.text, Convert.ToInt32(weight.text), company.text,

          phoneNumber.text, null);

                Debug.Log("添加成功");
            }
            if (woman.isOn)
            {

                SaveBrn(userName.text, GenderEnum.Female, Convert.ToInt32(height.text), age_Year_Text.text + age_Mouth_Text.text + age_Day_Text.text, Convert.ToInt32(weight.text), company.text,

          phoneNumber.text, null);

                Debug.Log("添加成功");
            }

            //DataManager.Instance.SaveUserData();//存储
            if (!goOn)
            {
                Close();
            }
        }

        //打开window窗口
        void ChooseExcel()
        {
            OpenFileName openFileName = new OpenFileName();
            openFileName.structSize = Marshal.SizeOf(openFileName);
            openFileName.filter = "Excel文件(*.xlsx)\0*.xlsx";
            openFileName.file = new string(new char[256]);
            openFileName.maxFile = openFileName.file.Length;
            openFileName.fileTitle = new string(new char[64]);
            openFileName.maxFileTitle = openFileName.fileTitle.Length;
            openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
            openFileName.title = "窗口标题";
            openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
            if (LocalDialog.GetOpenFileName(openFileName))
            {
                if (File.Exists(openFileName.file))
                {
                    bool isOpen = false;        //是否被其他程序占用
                    int status = FileIsOpen(openFileName.file);
                    isOpen = status == 1 ? true : false;

                    if (isOpen)
                    {
                        Debug.Log("EXCEL文件已经打开，请关闭");

                        return;
                    }

                }
                //Debug.Log(openFileName.file);//选择的文件输出为C:\Users\王冠\Desktop\SHMH项目表格\体适能测试项目立项书.xlsx
                //Debug.Log(openFileName.fileTitle);//体适能测试项目立项书.xlsx

                Debug.Log("正在导入文件");

                //打开导入 
                StartCoroutine(XLSX(openFileName.file));



                Debug.Log("导入成功");

                Close();

            }
        }

        static int FileIsOpen(string fileFullName)
        {
            if (!File.Exists(fileFullName))
            {
                return -1;
            }
            try
            {
                var fs = File.Open(fileFullName, FileMode.Open, FileAccess.Write);
                fs.Close();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e.Message);
                return 1;
            }

            return 0;
        }

        //按照路径导入
        IEnumerator XLSX(string openFileName)
        {
            Debug.Log("使用TestExcel中的方法加载");

          //  FileStream stream = File.Open(openFileName, FileMode.Open, FileAccess.Read);
          //  IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

          //  DataSet result = excelReader.AsDataSet();

          //  int columns = result.Tables[0].Columns.Count;//这里Tables[0]表示第一个sheet, 如果你有多个sheet的话，可以写sheet的名子
          //  int rows = result.Tables[0].Rows.Count;


          //  List<string> user = new List<string>();
          //  UsersData usersData = DataManager.Instance.ReadBtn();

          //  for (int i = 0; i < rows; i++)
          //  {
          //      if (i > 0)//不读取第一行
          //      {
          //          for (int j = 0; j < columns; j++)
          //          {

          //              string nvalue = result.Tables[0].Rows[i][j].ToString();

          //              user.Add(nvalue);

          //          }
          //      }

          //  }
          //  string[] output;
          //  List<string[]> userinfo = new List<string[]>();
          //  for (int i = 0; i < rows - 1; i++)//行数-1
          //  {
          //      output = new string[] { };

          //      output = user.GetRange(i * 8, 8).ToArray();

          //      userinfo.Add(output);


          //  }
          //  string excelName;
          //  string excelGender;
          //  string ecxelHeight;
          //  string ecxelWeight;
          //  string ecxelBrithday;
          //  string ecxelPhone;
          //  string ecxelCompany;
          //  string excelTime;

          //  GenderEnum gender = new GenderEnum();

          //  //按照行数存储信息
          //  for (int i = 0; i < userinfo.Count; i++)
          //  {
          //      string[] a = userinfo[i];
          //      excelName = a[0];
          //      excelGender = a[1];
          //      ecxelHeight = a[2];
          //      ecxelWeight = a[3];
          //      ecxelBrithday = a[4];
          //      ecxelPhone = a[5];
          //      ecxelCompany = a[6];
          //      excelTime = a[7];

          //      if (excelGender == "男")
          //      {
          //          gender = GenderEnum.Male;
          //      }
          //      else if (excelGender == "女")
          //      {
          //          gender = GenderEnum.Female;

          //      }
          //      else if (excelGender != "男" && excelGender != "女")
          //      {
          //          gender = GenderEnum.Male;
          //      }

          //      SaveBrn(excelName, gender, ecxelHeight.ToInt(), ecxelBrithday, ecxelWeight.ToFloat(), ecxelCompany,
          //ecxelPhone, excelTime);

          //  }
          //  DataManager.Instance.SaveUserData();//存储



          //  ApplicationUI.Instance.closeWindowObject("PC_WarningWindow", SHOW_EFFECT.NONE, true);
            yield return null;

        }
        string[] sArray;
        public void SaveBrn(string userName, GenderEnum gender, int height, string birthday, float weight, string company, string phone, string saveExcelTime)
        {

            //if (!UserInformationCheckout.userPhone(phone) | !UserInformationCheckout.userCompany(company) | !UserInformationCheckout.nameJudge(userName))
            //{
            //    return;
            //}
            //UserInfo userInfo = new UserInfo();
            //userInfo = DataManager.Instance.usersData.GetUserInfoByID(phone);

            //if (userInfo != null)
            //{
            //    //以前就有，不覆盖
            //    return;

            //}
            //else
            //{

            //    if (birthday == "")
            //    {
            //        birthday = "1990年1月1日";//默认
            //    }
            //    else
            //    {
            //        sArray = birthday.Split('年', '月', '日');

            //        if (sArray.Length == 4)
            //        {

            //            if (!BirthdayPass(sArray[0].ToInt(), sArray[1].ToInt(), sArray[2].ToInt()))
            //            {
            //                birthday = "1990年1月1日";//默认
            //            }

            //        }
            //        else
            //        {
            //            birthday = "1990年1月1日";//默认
            //        }

            //    }




            //    if (!UserInformationCheckout.userHeight(height.ToString()))
            //    {

            //        height = 0;
            //    }
            //    if (!UserInformationCheckout.userWeight(weight.ToString()))
            //    {
            //        weight = 0;
            //    }


            //    if (gender != GenderEnum.Female && gender != GenderEnum.Male)
            //    {
            //        gender = GenderEnum.Male;
            //    }



            //    //以前没有
            //    userInfo = new UserInfo();
            //    userInfo.userBaseInfo.name = userName;
            //    userInfo.userBaseInfo.gender = gender;
            //    userInfo.userBaseInfo.height = height;
            //    userInfo.userBaseInfo.birthday = birthday;
            //    userInfo.userBaseInfo.weight = weight;
            //    userInfo.userBaseInfo.company = company;
            //    userInfo.userBaseInfo.phone = phone;
            //    userInfo.userBaseInfo.deviceUniqueIdentifier = Guid.NewGuid().ToString();
            //    userInfo.userBaseInfo.testTime = "";
            //}


            //if (saveExcelTime == null)
            //{
            //    userInfo.userBaseInfo.saveTime = System.DateTime.Now.ToString();
            //}
            //else
            //{
            //    userInfo.userBaseInfo.saveTime = saveExcelTime;
            //}


            //// DataManager.Instance.usersData.CurrentApplyUserID = userInfo.baseInfo.phone;      //默认ID

            //DataManager.Instance.usersData.AddUser(userInfo);//只做增加操作，在软件关闭前才会写入文件




        }


    }

}
