using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
//using System.Web.WebPages.Html;
using System.Web.Mvc;

namespace CustomerService.Models
{
    public class CustomerServiceModel 
    {
        public string custID { get; set; }
        public string userName { get; set; }
        public string walletNo { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string emailAdd { get; set; }
        public string mobileNo { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string fullName { get; set; }



        public Int64 kpsocount { get; set; }
        public Int64 kppocount { get; set; }
        public Int64 kprfccount { get; set; }
        public Int64 kprtscount { get; set; }
        public Int64 kpcsocount { get; set; }
        public Int64 kpcpocount { get; set; }
        public Int64 walletsocount { get; set; }
        public Int64 walletpocount { get; set; }
        public Int64 walletrfccount { get; set; }
        public Int64 walletrtscount { get; set; }
        public Int64 walletcsocount { get; set; }
        public Int64 walletcpocount { get; set; }
        public Int64 walletbpcount { get; set; }
        public Int64 walleteloadcount { get; set; }
        public Int64 walletcorppocount { get; set; }
        public Int64 expresssocount { get; set; }
        public Int64 expresspocount { get; set; }
        public Int64 expressrfccount { get; set; }
        public Int64 expressrtscount { get; set; }
        public Int64 expresscsocount { get; set; }
        public Int64 expresscpocount { get; set; }
        public Int64 expressbpcount { get; set; }
        public Int64 expresseloadcount { get; set; }
        public Int64 expresscorppocount { get; set; }
        public Int64 globalsocount { get; set; }
        public Int64 globalpocount { get; set; }
        public Int64 globalrfccount { get; set; }
        public Int64 globalrtscount { get; set; }
        public Int64 globalcsocount { get; set; }
        public Int64 globalcpocount { get; set; }
        public Int64 apisocount { get; set; }
        public Int64 apipocount { get; set; }
        public Int64 apirfccount { get; set; }
        public Int64 apirtscount { get; set; }
        public Int64 apicsocount { get; set; }
        public Int64 apicpocount { get; set; }
        public Int64 fusocount { get; set; }
        public Int64 fupocount { get; set; }
        public Int64 furfccount { get; set; }
        public Int64 furtscount { get; set; }
        public Int64 fucsocount { get; set; }
        public Int64 fucpocount { get; set; }
        public Int64 wscsocount { get; set; }
        public Int64 wscpocount { get; set; }
        public Int64 wscrfccount { get; set; }
        public Int64 wscrtscount { get; set; }
        public Int64 wsccsocount { get; set; }
        public Int64 wsccpocount { get; set; }
        public Int64 bpsocount { get; set; }
        public Int64 bprfccount { get; set; }
        public Int64 bprtscount { get; set; }
        public Int64 bpcsocount { get; set; }
        public Int64 prendacount { get; set; }
        public Int64 lukatcount { get; set; }
        public Int64 renewcount { get; set; }
        public Int64 reappraisecount { get; set; }
        public Int64 layawaycount { get; set; }
        public Int64 salescount { get; set; }
        public Int64 tradeincount { get; set; }
        public Int64 sblcount { get; set; }
        public Int64 eloadcount { get; set; }
        public Int64 insurancecount { get; set; }
        public Int64 goodscount { get; set; }


        public string street { get; set; }
        public string provincecity { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string branchid { get; set; }
        public string idtype { get; set; }
        public string idno { get; set; }
        public string expirydate { get; set; }
        public string dtcreated { get; set; }
        public string dtmodified { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public string phoneno { get; set; }
        public string cardno { get; set; }
        public string placeofbirth { get; set; }
        public string natureofwork { get; set; }
        public string permanentaddress { get; set; }
        public string nationality { get; set; }
        public string companyoremployer { get; set; }
        public string businessorprofession { get; set; }
        public string govtidtype { get; set; }
        public string govtidno { get; set; }
        public string branchcreated { get; set; }
        public string branchmodified { get; set; }
        public string mlcardno { get; set; }


        public string path { get; set; }
        public string path1 { get; set; }

        public string kycfront { get; set; }
        public string kycback { get; set; }
        public string id1 { get; set; }
        public string id2 { get; set; }
        public string id3 { get; set; }
        public string fingerprint { get; set; }
        public string imagefree1 { get; set; }
        public string imagefree2 { get; set; }
        public string imagefree3 { get; set; }
        public string customersimage { get; set; }

        public byte[] imgkycfront { get; set; }
        public byte[] imgkycback { get; set; }
        public byte[] imgid1 { get; set; }
        public byte[] imgid2 { get; set; }
        public byte[] imgid3 { get; set; }
        public byte[] imgfingerprint { get; set; }
        public byte[] imgimagefree1 { get; set; }
        public byte[] imgimagefree2 { get; set; }
        public byte[] imgimagefree3 { get; set; }
        public byte[] imgcustomersimage { get; set; }


        public IEnumerable<SelectListItem> Category { get; set; }
        public IEnumerable<SelectListItem> Month { get; set; }
        public int Year { get; set; }

        public DataTable dt { get; set; }
        public string user { get; set; }

        public DataTable dtable()
        {
            DataTable dt = new DataTable();
            DataColumn column = new DataColumn();
            column.ColumnName = "id";
            column.DataType = System.Type.GetType("System.Int32");
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            DataColumn id = column;
            dt.Columns.Add(id);
           dt.Columns.Add(new DataColumn("custid", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("lname", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("fname", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("mname", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("gender", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("address", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("contactno", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("emailadd", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("date", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("amount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("charge", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("txntype", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("category", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("txnno", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("operator", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("description", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("senderreceiver", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("currency", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("partner", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("branch", System.Type.GetType("System.String")));
            dt.PrimaryKey = new DataColumn[] { id };
            return dt;
        }
        public DataTable dtablereport()
        {
            DataTable dt = new DataTable();
            DataColumn column = new DataColumn();
            column.ColumnName = "id";
            column.DataType = System.Type.GetType("System.Int32");
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            DataColumn id = column;
            dt.Columns.Add(id);
            dt.Columns.Add(new DataColumn("custid", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("lname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("fname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("mname", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("gender", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("address", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("contactno", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("emailadd", System.Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("date", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("txnno", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("senderreceiver", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("category", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("txntype", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("currency", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("amount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("charge", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("description", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("partner", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("operator", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("branch", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("kpsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("kppocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("kprfccount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("kprtscount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("kpcsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("kpcpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("walletsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("walletpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("walletrfccount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("walletrtscount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("walletcsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("walletcpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("walletbpcount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("walleteloadcount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("walletcorppocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("expresssocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("expresspocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("expressrfccount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("expressrtscount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("expresscsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("expresscpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("expressbpcount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("expresseloadcount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("expresscorppocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("globalsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("globalpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("globalrfccount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("globalrtscount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("globalcsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("globalcpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("apisocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("apipocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("apirfccount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("apirtscount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("apicsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("apicpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("fusocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("fupocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("furfccount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("furtscount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("fucsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("fucpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("wscsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("wscpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("wscrfccount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("wscrtscount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("wsccsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("wsccpocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("bpsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("bprfccount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("bprtscount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("bpcsocount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("prendacount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("lukatcount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("renewcount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("reappraisecount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("layawaycount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("salescount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("tradeincount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("sblcount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("eloadcount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("insurancecount", System.Type.GetType("System.String")));
           dt.Columns.Add(new DataColumn("goodscount", System.Type.GetType("System.String")));
            dt.PrimaryKey = new DataColumn[] { id };
            return dt;
        }
        public void WriteToFile(string text)
        {
            string folderName = @"c:\ReportsLogs\CustomerServiceLogs";
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

    }


    public class CustomerInfo {
        public string custid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public string street { get; set; }
        public string provincecity { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public string branchid { get; set; }
        public string idtype { get; set; }
        public string idno { get; set; }
        public string expirydate { get; set; }
        public string dtcreated { get; set; }
        public string dtmodified { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public string phoneno { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string cardno { get; set; }
        public string placeofbirth { get; set; }
        public string natureofwork { get; set; }
        public string permanentaddress { get; set; }
        public string nationality { get; set; }
        public string companyoremployer { get; set; }
        public string businessorproffession { get; set; }
        
    }

    public class CustomerSummary
    {
        public IEnumerable<CustomerServiceModel> CustomerTxnSummary { get; set; }

        public string GetTxnType(int num)
        {
            string txntype = string.Empty;
            switch (num)
            {
                case 1: txntype = "kpso"; break;
                case 2: txntype = "kppo"; break;
                case 3: txntype = "kprfc"; break;
                case 4: txntype = "kprts"; break;
                case 5: txntype = "kpcso"; break;
                case 6: txntype = "kpcpo"; break;
                case 7: txntype = "walletso"; break;
                case 8: txntype = "walletpo"; break;
                case 9: txntype = "walletrfc"; break;
                case 10: txntype = "walletrts"; break;
                case 11: txntype = "walletcso"; break;
                case 12: txntype = "walletcpo"; break;
                case 13: txntype = "walletbp"; break;
                case 14: txntype = "walleteload"; break;
                case 15: txntype = "walletcorppo"; break;
                case 16: txntype = "expressso"; break;
                case 17: txntype = "expresspo"; break;
                case 18: txntype = "expressrfc"; break;
                case 19: txntype = "expressrts"; break;
                case 20: txntype = "expresscso"; break;
                case 21: txntype = "expresscpo"; break;
                case 22: txntype = "expressbp"; break;
                case 23: txntype = "expresseload"; break;
                case 24: txntype = "expresscorppo"; break;
                case 25: txntype = "apiso"; break;
                case 26: txntype = "apipo"; break;
                case 27: txntype = "apirfc"; break;
                case 28: txntype = "apirts"; break;
                case 29: txntype = "apicso"; break;
                case 30: txntype = "apicpo"; break;
                case 31: txntype = "fuso"; break;
                case 32: txntype = "fupo"; break;
                case 33: txntype = "furfc"; break;
                case 34: txntype = "furts"; break;
                case 35: txntype = "fucso"; break;
                case 36: txntype = "fucpo"; break;
                case 37: txntype = "wscso"; break;
                case 38: txntype = "wscpo"; break;
                case 39: txntype = "wscrfc"; break;
                case 40: txntype = "wscrts"; break;
                case 41: txntype = "wsccso"; break;
                case 42: txntype = "wsccpo"; break;
                case 43: txntype = "bpso"; break;
                case 44: txntype = "bprfc"; break;
                case 45: txntype = "bprts"; break;
                case 46: txntype = "bpcso"; break;
                case 47: txntype = "prenda"; break;
                case 48: txntype = "lukat"; break;
                case 49: txntype = "renew"; break;
                case 50: txntype = "reappraise"; break;
                case 51: txntype = "globalso"; break;
                case 52: txntype = "globalpo"; break;
                case 53: txntype = "globalrfc"; break;
                case 54: txntype = "globalrts"; break;
                case 55: txntype = "globalcso"; break;
                case 56: txntype = "globalcpo"; break;
                case 57: txntype = "layaway"; break;
                case 58: txntype = "sales"; break;
                case 59: txntype = "tradein"; break;
                case 60: txntype = "sbl"; break;
                case 61: txntype = "eload"; break;
                case 62: txntype = "insurance"; break;
                case 63: txntype = "goods"; break;

            }
            return txntype;
        }
    }
}