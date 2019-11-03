using System;
using System.Data;
using UnityEngine;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using UnityEngine.UI;



using OfficeOpenXml;

public class TestExcel : MonoBehaviour
{

    string file_xlsx = @"\批量导入格式.xlsx";
    string file_xls = @"\批量导入格式123.xls";
    string m_tsr = "";

    public Text m_text;

    public void Xls()
    {
        fileName = Application.streamingAssetsPath+file_xls;

        ExcelToDataTable("Sheet1", true);

    }

    public void Xlsx()
    {
        fileName = Application.streamingAssetsPath + file_xlsx;

        ExcelToDataTable("Sheet1", true);

    }

    private string fileName = null; //文件名
    private IWorkbook workbook = null;
    private FileStream fs = null;

    void ReadExcel(FileStream fs)
    {

        ExcelPackage package = new ExcelPackage(fs);


        ExcelWorksheet sheet = package.Workbook.Worksheets[1];//注意下标 表1


        List<string> user = new List<string>();

        for (int i = 1; i < sheet.Dimension.End.Row + 1; i++)//注意行列不同  初始是[1,1]
        {

            for (int j = 1; j < sheet.Dimension.End.Column + 1; j++)
            {
                string nvalue = "";
                if (sheet.Cells[i, j].Value != null)
                {
                    nvalue = sheet.Cells[i, j].Value.ToString();

                }
                user.Add(nvalue);
            }

        }


        string[] output;
        List<string[]> userinfo = new List<string[]>();
        for (int i = 1; i < sheet.Dimension.End.Row; i++)//行数-1
        {
            output = new string[] { };

            output = user.GetRange(i * 8, 8).ToArray();

            if (output[0] != "")
                userinfo.Add(output);

        }

        aaa(userinfo);
        //m_tsr = "";
        //if (userinfo.Count > 999)
        //{

        //}
        //else
        //{
        //    string excelName;
        //    string excelGroupID;
        //    string excelGender;
        //    string ecxelHeight;
        //    string ecxelWeight;
        //    string ecxelBrithday;
        //    string identifyID;//id规则：4位组织号+年月日时分秒+mac地址后四位+3位随机码
        //    string exlastTestTime;

        //    //按照行数存储信息
        //    for (int i = 0; i < userinfo.Count; i++)
        //    {
        //        string[] a = userinfo[i];
        //        excelGroupID = a[0];
        //        identifyID = a[1];
        //        excelName = a[2];
        //        excelGender = a[3];
        //        ecxelBrithday = a[4];
        //        ecxelHeight = a[5];
        //        ecxelWeight = a[6];
        //        exlastTestTime = a[7];

        //        m_tsr += string.Format("excelGroupID:{0} ,identifyID:{1} , excelName:{2} , excelGender:{3} , ecxelBrithday:{4} , ecxelHeight:{5} , ecxelWeight:{6} , exlastTestTime:{7} ,\n\r", excelGroupID, identifyID, excelName, excelGender, ecxelBrithday, ecxelHeight, ecxelWeight, exlastTestTime);


        //    }
        //    m_text.text = m_tsr;
        //}
    }


    public void ExcelToDataTable(string sheetName, bool isFirstRowColumn)
    {
        ISheet sheet = null;
        DataTable data = new DataTable();
        int startRow = 0;

        try
        {
            fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            if (fileName.IndexOf(".xlsx") > 0)
            {
                ReadExcel(fs);//这里返回一个list
                return ;
            }// 2007版本
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook(fs);

            if (sheetName != null)
            {
                sheet = workbook.GetSheet(sheetName);
                if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                {
                    sheet = workbook.GetSheetAt(0);
                }
            }
            else
            {
                sheet = workbook.GetSheetAt(0);
            }
            if (sheet != null)
            {
                IRow firstRow = sheet.GetRow(0);
                int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                if (isFirstRowColumn)
                {
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (cellValue != null)
                            {
                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                    startRow = sheet.FirstRowNum + 1;
                }
                else
                {
                    startRow = sheet.FirstRowNum;
                }

                //最后一列的标号
                int rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; //没有数据的行默认是null　　　　　　　

                    DataRow dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            dataRow[j] = row.GetCell(j).ToString();
                    }
                    data.Rows.Add(dataRow);
                }
            }
            int columns = data.Columns.Count;//这里Tables[0]表示第一个sheet, 如果你有多个sheet的话，可以写sheet的名子
            int rows = data.Rows.Count;

            List<string> user = new List<string>();

            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < columns; j++)
                {

                    string nvalue = data.Rows[i][j].ToString();

                    user.Add(nvalue);
                }

            }

            string[] output;
            List<string[]> userinfo = new List<string[]>();
            for (int i = 0; i < rows - 1; i++)//行数-1
            {
                output = new string[] { };

                output = user.GetRange(i * 8, 8).ToArray();

                if (output[0] != "")
                    userinfo.Add(output);

            }
            aaa(userinfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            return ;
        }
    }


    void aaa(List<string[]> userinfo)
    {   
        m_tsr = "";
        if (userinfo.Count > 999)
        {

        }
        else
        {
            string excelName;
            string excelGroupID;
            string excelGender;
            string ecxelHeight;
            string ecxelWeight;
            string ecxelBrithday;
            string identifyID;//id规则：4位组织号+年月日时分秒+mac地址后四位+3位随机码
            string exlastTestTime;

            //按照行数存储信息
            for (int i = 0; i < userinfo.Count; i++)
            {
                string[] a = userinfo[i];
                excelGroupID = a[0];
                identifyID = a[1];
                excelName = a[2];
                excelGender = a[3];
                ecxelBrithday = a[4];
                ecxelHeight = a[5];
                ecxelWeight = a[6];
                exlastTestTime = a[7];
                m_tsr += string.Format("excelGroupID:{0} ,identifyID:{1} , excelName:{2} , excelGender:{3} , ecxelBrithday:{4} , ecxelHeight:{5} , ecxelWeight:{6} , exlastTestTime:{7} ,\n\r", excelGroupID, identifyID, excelName, excelGender, ecxelBrithday, ecxelHeight, ecxelWeight, exlastTestTime);
            }
            m_text.text = m_tsr;
        }

    }
}
