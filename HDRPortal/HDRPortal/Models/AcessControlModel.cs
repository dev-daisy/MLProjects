using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace HDRPortal.Models
{
    public class AcessControlModel
    {
        public IEnumerable<SelectListItem> Category { get; set; }
        public IEnumerable<SelectListItem> Region { get; set; }
        public IEnumerable<SelectListItem> Area { get; set; }
        public IEnumerable<SelectListItem> Branch { get; set; }
        public string user { get; set; }
        public string role { get; set; }
        public string mappath { get; set; }
        public string trendpath { get; set; }
        public string customerstatpath { get; set; }
        public string offlinemonitoringpath { get; set; }
        public string customerservicepath { get; set; }

        public string date = DateTime.Now.ToString("MMMM-dd-yyyy,HH:mm");
        public List<cstatus> lstatus { get; set; }

        public class cstatus
        {
            public string status { get; set; }
            public string syscreated { get; set; }


        }
        public DataTable getusersdt()
        {
            DataTable dt = new DataTable();
            DataColumn column = new DataColumn();
            column.ColumnName = "id";
            column.DataType = System.Type.GetType("System.Int32");
            DataColumn id = column;
            dt.Columns.Add(id);
            dt.Columns.Add(new DataColumn("resourceid", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("roleid", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("userlogin", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("lastname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("firstname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("middlename", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("reliever", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("origbranchcode", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("origzonecode", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("tempbranchcode", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("tempzonecode", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("zonecode", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("regioncode", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("areacode", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("branchcode", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("regionname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("areaname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("branchname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("datecreated", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("usercreator", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("datemodified", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("usermodifier", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("contactno", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("emailadd", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("userpassword", System.Type.GetType("System.String")));
            dt.PrimaryKey = new DataColumn[] { id };
            return dt;
        }
        public DataTable getbranchprofiledt()
        {
            DataTable dt = new DataTable();
            DataColumn column = new DataColumn();
            column.ColumnName = "id";
            column.DataType = System.Type.GetType("System.Int32");
            DataColumn id = column;
            dt.Columns.Add(id);

            //branchinfo
            dt.Columns.Add(new DataColumn("branchname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("address", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("telno", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("celno", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("areaname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("regionname", System.Type.GetType("System.String")));
            //people
            dt.Columns.Add(new DataColumn("rmname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("rmcontactno", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("amname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("amcontactno", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("abmname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("bmname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("bmcontactno", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller1", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller2", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller3", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller4", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller5", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller6", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller7", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller8", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller9", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller10", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller11", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller12", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller13", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller14", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller15", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller16", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller17", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller18", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller19", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("teller20", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("rctname", System.Type.GetType("System.String")));
            //Computer Asset
            dt.Columns.Add(new DataColumn("StationCode", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Processor", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("HardDrives", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("ComputerName", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("OS", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("Memory", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("OSProdKey", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("OSSerialKey", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app1", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app2", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app3", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app4", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app5", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app6", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app7", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app8", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app9", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("app10", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("compasset", System.Type.GetType("System.String")));
            //branch asset
            dt.Columns.Add(new DataColumn("assetdesc", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("deliverydate", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("serialno", System.Type.GetType("System.String")));
            //branch bandwidth
            dt.Columns.Add(new DataColumn("bwispname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("bwbandwidth", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("bwdate", System.Type.GetType("System.String")));
            //history
            //ir
            dt.Columns.Add(new DataColumn("irno", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("irdate", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("irincident", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("irauthor", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("irclose", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("iropen", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("irreceived", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("irstatus", System.Type.GetType("System.String")));
            //wo
            dt.Columns.Add(new DataColumn("workno", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("workdate", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("workdesc", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("workauthor", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("workstatus", System.Type.GetType("System.String")));

            dt.PrimaryKey = new DataColumn[] { id };
            return dt;
        }

        public void WriteToFile(string text)
        {
            string folderName = @"c:\ReportsLogs\HDRPortalLogs";
            try
            {
                if (!System.IO.File.Exists(folderName)) { System.IO.Directory.CreateDirectory(folderName); }
                //string[] filesInDir = Directory.GetFiles(folderName, DateTime.Now.ToString("yyyy-MM-dd") + "*");
                //foreach (string foundFile in filesInDir)
                //{
                //    System.IO.File.Delete(foundFile);
                //}

                //string path = "C:\\ReportsLogs\\MLMapService\\MapServiceLog.txt";
                //string path = AppDomain.CurrentDomain.BaseDirectory + "logs\\MapServiceLog.txt";
                string filename = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                folderName = System.IO.Path.Combine(folderName, filename);
                using (StreamWriter writer = new StreamWriter(folderName, true))
                {
                    writer.WriteLine(text);
                    writer.Close();
                }
            }
            catch (Exception ex) { WriteToFile("Writing Logs: " + ex.ToString()); }
        }

        public DataTable ListToDataTable<T>(List<T> items)
        {

            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo propInfo in Properties)
            {
                dataTable.Columns.Add(propInfo.Name);
            }

            foreach (T item in items)
            {
                dynamic values = new object[Properties.Length];

                for (int i = 0; i <= Properties.Length - 1; i++)
                {
                    values[i] = Properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public List<string> mergebranchlist { get; set; }
        public List<string> mergereportlist { get; set; }

        public void MergePDF(string newfile,string flag)
        {
            try
            {
                iTextSharp.text.Document oPdfDoc = new iTextSharp.text.Document();
                PdfWriter oPdfWriter = PdfWriter.GetInstance(oPdfDoc, new FileStream(newfile, FileMode.Create));

                oPdfDoc.Open();

                if (flag == "mergereport") {
                    foreach (string filename in mergereportlist)
                    {
                        AddPdf(filename, ref oPdfDoc, ref oPdfWriter);
                    }
                }
                else if (flag == "mergebranch") {
                    foreach (string filename in mergebranchlist)
                    {
                        AddPdf(filename, ref oPdfDoc, ref oPdfWriter);                        
                    }
                }
                oPdfDoc.Close();
                oPdfWriter.Close();
            }
            catch{ }
            //return newfile;
        }
        public void AddPdf(string sInFilePath, ref iTextSharp.text.Document oPdfDoc, ref PdfWriter oPdfWriter)
        {

            iTextSharp.text.pdf.PdfContentByte oDirectContent = oPdfWriter.DirectContent;
            iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(sInFilePath);
            int iNumberOfPages = oPdfReader.NumberOfPages;
            int iPage = 0;

            while ((iPage < iNumberOfPages))
            {
                iPage += 1;

                int iRotation = oPdfReader.GetPageRotation(iPage);
                iTextSharp.text.pdf.PdfImportedPage oPdfImportedPage = oPdfWriter.GetImportedPage(oPdfReader, iPage);
                oPdfDoc.SetPageSize(oPdfReader.GetPageSizeWithRotation(iPage));
                oPdfDoc.NewPage();

                if ((iRotation == 90) | (iRotation == 270))
                {
                    oDirectContent.AddTemplate(oPdfImportedPage, 0, -1f, 1f, 0, 0, oPdfReader.GetPageSizeWithRotation(iPage).Height);
                }
                else
                {
                    oDirectContent.AddTemplate(oPdfImportedPage, 1f, 0, 0, 1f, 0, 0);
                }
            }

        }
    }
} 