using CustomerService.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerService.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        CustomerServiceModel cust = new CustomerServiceModel();
        AdminModel admin = new AdminModel();
        Connection conn = new Connection();

        public ActionResult Index()
        {
            cust.user = "admin";
            cust.Year = DateTime.Now.Year;
            cust.Category = reportname();
            return View(cust);
        }

        public SelectList reportname()
        {
            var item = new List<SelectListItem>();
            List<SelectListItem> reportlist = new List<SelectListItem>();
            //domestic
            reportlist.Add(new SelectListItem { Text = "KP Sendout", Value = "kpso" });
            reportlist.Add(new SelectListItem { Text = "KP Payout", Value = "kppo" });
            reportlist.Add(new SelectListItem { Text = "KP RFC", Value = "kprfc" });
            reportlist.Add(new SelectListItem { Text = "KP RTS", Value = "kprts" });
            reportlist.Add(new SelectListItem { Text = "KP CSO", Value = "kpcso" });
            reportlist.Add(new SelectListItem { Text = "KP CPO", Value = "kpcpo" });
            //wallet
            reportlist.Add(new SelectListItem { Text = "Wallet Sendout", Value = "walletso" });
            reportlist.Add(new SelectListItem { Text = "Wallet Payout", Value = "walletpo" });
            reportlist.Add(new SelectListItem { Text = "Wallet RFC", Value = "walletrfc" });
            reportlist.Add(new SelectListItem { Text = "Wallet RTS", Value = "walletrts" });
            reportlist.Add(new SelectListItem { Text = "Wallet CSO", Value = "walletcso" });
            reportlist.Add(new SelectListItem { Text = "Wallet CPO", Value = "walletcpo" });
            reportlist.Add(new SelectListItem { Text = "Wallet Billspay", Value = "walletbp" });
            reportlist.Add(new SelectListItem { Text = "Wallet Eload", Value = "walleteload" });
            reportlist.Add(new SelectListItem { Text = "Wallet Corp PO", Value = "walletcorppo" });
            //express
            reportlist.Add(new SelectListItem { Text = "Express Sendout", Value = "expressso" });
            reportlist.Add(new SelectListItem { Text = "Express Payout", Value = "expresspo" });
            reportlist.Add(new SelectListItem { Text = "Express RFC", Value = "expressrfc" });
            reportlist.Add(new SelectListItem { Text = "Express RTS", Value = "expressrts" });
            reportlist.Add(new SelectListItem { Text = "Express CSO", Value = "expresscso" });
            reportlist.Add(new SelectListItem { Text = "Express CPO", Value = "expresscpo" });
            reportlist.Add(new SelectListItem { Text = "Express Billspay", Value = "expressbp" });
            reportlist.Add(new SelectListItem { Text = "Express Eload", Value = "expresseload" });
            reportlist.Add(new SelectListItem { Text = "Express Corp PO", Value = "expresscorppo" });
            //corporate api
            reportlist.Add(new SelectListItem { Text = "API Sendout", Value = "apiso" });
            reportlist.Add(new SelectListItem { Text = "API Payout", Value = "apipo" });
            reportlist.Add(new SelectListItem { Text = "API RFC", Value = "apirfc" });
            reportlist.Add(new SelectListItem { Text = "API RTS", Value = "apirts" });
            reportlist.Add(new SelectListItem { Text = "API CSO", Value = "apicso" });
            reportlist.Add(new SelectListItem { Text = "API CPO", Value = "apicpo" });
            //corporate fu
            reportlist.Add(new SelectListItem { Text = "FU Sendout", Value = "fuso" });
            reportlist.Add(new SelectListItem { Text = "FU Payout", Value = "fupo" });
            reportlist.Add(new SelectListItem { Text = "FU RFC", Value = "furfc" });
            reportlist.Add(new SelectListItem { Text = "FU RTS", Value = "furts" });
            reportlist.Add(new SelectListItem { Text = "FU CSO", Value = "fucso" });
            reportlist.Add(new SelectListItem { Text = "FU CPO", Value = "fucpo" });
            //corporate fu
            reportlist.Add(new SelectListItem { Text = "WSC Sendout", Value = "wscso" });
            reportlist.Add(new SelectListItem { Text = "WSC Payout", Value = "wscpo" });
            reportlist.Add(new SelectListItem { Text = "WSC RFC", Value = "wscrfc" });
            reportlist.Add(new SelectListItem { Text = "WSC RTS", Value = "wscrts" });
            reportlist.Add(new SelectListItem { Text = "WSC CSO", Value = "wsccso" });
            reportlist.Add(new SelectListItem { Text = "WSC CPO", Value = "wsccpo" });
            //billspay
            reportlist.Add(new SelectListItem { Text = "BP Sendout", Value = "bpso" });
            reportlist.Add(new SelectListItem { Text = "BP RFC", Value = "bprfc" });
            reportlist.Add(new SelectListItem { Text = "BP RTS", Value = "bprts" });
            reportlist.Add(new SelectListItem { Text = "BP CSO", Value = "bpcso" });
            //qcl
            reportlist.Add(new SelectListItem { Text = "Prenda", Value = "prenda" });
            reportlist.Add(new SelectListItem { Text = "Lukat", Value = "lukat" });
            reportlist.Add(new SelectListItem { Text = "Renew", Value = "renew" });
            reportlist.Add(new SelectListItem { Text = "Reappraise", Value = "reappraise" });
            //global
            reportlist.Add(new SelectListItem { Text = "Global Sendout", Value = "globalso" });
            reportlist.Add(new SelectListItem { Text = "Global Payout", Value = "globalpo" });
            reportlist.Add(new SelectListItem { Text = "Global RFC", Value = "globalrfc" });
            reportlist.Add(new SelectListItem { Text = "Global RTS", Value = "globalrts" });
            reportlist.Add(new SelectListItem { Text = "Global CSO", Value = "globalcso" });
            reportlist.Add(new SelectListItem { Text = "Global CPO", Value = "globalcpo" });
            //jewellers
            reportlist.Add(new SelectListItem { Text = "Lay Away", Value = "layaway" });
            reportlist.Add(new SelectListItem { Text = "Sales", Value = "sales" });
            reportlist.Add(new SelectListItem { Text = "Trade-In", Value = "tradein" });
            //SBL
            reportlist.Add(new SelectListItem { Text = "SBL", Value = "sbl" });
            reportlist.Add(new SelectListItem { Text = "Eload", Value = "eload" });
            reportlist.Add(new SelectListItem { Text = "Insurance", Value = "insurance" });
            reportlist.Add(new SelectListItem { Text = "Goods", Value = "goods" });

            item = reportlist;
            return new SelectList(item, "Value", "Text");
        }

        public DataTable GetGlobalTXN(string category, int year, int month)
        {
            AdminModel am = new AdminModel();
            DataTable dtmerge = new DataTable();
            dtmerge = am.dtable();
            var str = string.Empty;
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        var fromdate = new DateTime(year, month, 1);
                        var todate = fromdate.AddMonths(1).AddDays(-1);
                        while (fromdate <= todate)
                        {
                            if (category == "globalso")
                            {
                                str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                    " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,`transdate` ,'0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` ,charge, " +
                                    " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                    " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,sendercustid AS custid, " +
                                    " senderlname as lname,senderfname as fname, sendermname as mname,'' as username,'' as walletno, 'SENDOUT' as   `txntype`, " +
                                    " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`, `cancelreason`,   `cancelleddate`, " +
                                    " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                    " cancelledbyzonecode,canceldetails,cancelcharge,chargeto,remotezonecode,currency,stationid, " +
                                    " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                    " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                    " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'GLOBAL' as servicetype " +
                                    " from `global_kpglobal`.`sendout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                    " and year(transdate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "globalpo")
                            {
                                str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                    " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                    " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  sobranch as  `senderbranch` , '' as  `receiverbranch` , " +
                                    " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                    " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'PAYOUT' as   `txntype`, " +
                                    " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                    " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                    " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid, " +
                                    " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                    " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                    " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'GLOBAL' as servicetype " +
                                    " from `global_kpglobal`.`payout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                    " and year(claimeddate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "globalrfc" || category == "globalrts" || category == "globalcso")
                            {
                                string cancelreason = (category == "globalrfc") ? "Request for Change" : (category == "globalrts") ? "Return to Sender" : "Cancel Sendout";
                                str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,`transdate` ,'0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` ,charge, " +
                                " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,sendercustid AS custid, " +
                                " senderlname as lname,senderfname as fname, sendermname as mname,'' as username,'' as walletno, '{2}' as   `txntype`, " +
                                " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`, `cancelreason`,   `cancelleddate`, " +
                                " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                " cancelledbyzonecode,canceldetails,cancelcharge,chargeto,remotezonecode,currency,stationid, " +
                                " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'GLOBAL' as servicetype " +
                                " from global_kpglobal.`sendout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                " and year(transdate)='{1}' and cancelreason='{2}' ", fromdate.ToString("MMdd"), fromdate.Year,   cancelreason);
                            }
                            else if (category == "globalcpo")
                            {
                                str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                     " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                     " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  sobranch as  `senderbranch` , '' as  `receiverbranch` , " +
                                     " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                     " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'CANCEL PAYOUT' as   `txntype`, " +
                                     " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                     " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                     " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid, " +
                                     " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                     " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                     " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'GLOBAL' as servicetype " +
                                     " from global_kpglobal.`payout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                     " and year(claimeddate)='{1}' and cancelledreason='Wrong Payout' ", fromdate.ToString("MMdd"), fromdate.Year );
                            } 
                            cmd.CommandText = str;
                            cmd.CommandTimeout = 0;
                            using (OdbcDataReader Reader = cmd.ExecuteReader())
                            {
                                if (Reader.HasRows)
                                {
                                    DataTable dt = new DataTable();
                                    dt = am.dtable();
                                    dt.Load(Reader);
                                    dtmerge.Merge(dt);
                                    Reader.Close();
                                }
                            }
                            fromdate = fromdate.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error getting Domestic TXN using hadoop : " + ex.ToString());
            }

            if (dtmerge.Rows.Count==0) {
                try
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        if (i == 1) { conn.connectdb("GlobalB"); }
                        else if (i == 2) { conn.connectdb("GlobalCloudB"); }

                        using (MySqlConnection mycon = conn.getConnection())
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                var fromdate = new DateTime(year, month, 1);
                                var todate = fromdate.AddMonths(1).AddDays(-1);
                                while (fromdate <= todate)
                                {
                                    if (category == "globalso")
                                    {
                                        string yr = (i == 3) ? fromdate.Year.ToString() : "";
                                        str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                            " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,`transdate` ,'0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` ,charge, " +
                                            " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                            " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,sendercustid AS custid, " +
                                            " senderlname as lname,senderfname as fname, sendermname as mname,'' as username,'' as walletno, 'SENDOUT' as   `txntype`, " +
                                            " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`, `cancelreason`,   `cancelleddate`, " +
                                            " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                            " cancelledbyzonecode,canceldetails,cancelcharge,chargeto,remotezonecode,currency,stationid, " +
                                            " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                            " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                            " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'GLOBAL' as servicetype " +
                                            " from kpglobal{2}.`sendout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                            " and year(transdate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year, yr);
                                    }
                                    else if (category == "globalpo")
                                    {
                                        string yr = (i == 3) ? fromdate.Year.ToString() : "";
                                        str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                            " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                            " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  sobranch as  `senderbranch` , '' as  `receiverbranch` , " +
                                            " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                            " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'PAYOUT' as   `txntype`, " +
                                            " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                            " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                            " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid, " +
                                            " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                            " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                            " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'GLOBAL' as servicetype " +
                                            " from kpglobal{2}.`payout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                            " and year(claimeddate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year, yr);
                                    }
                                    else if (category == "globalrfc" || category == "globalrts" || category == "globalcso")
                                    {
                                        string yr = (i == 3) ? fromdate.Year.ToString() : "";
                                        string cancelreason = (category == "globalrfc") ? "Request for Change" : (category == "globalrts") ? "Return to Sender" : "Cancel Sendout";
                                        str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                        " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,`transdate` ,'0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` ,charge, " +
                                        " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                        " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,sendercustid AS custid, " +
                                        " senderlname as lname,senderfname as fname, sendermname as mname,'' as username,'' as walletno, '{2}' as   `txntype`, " +
                                        " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`, `cancelreason`,   `cancelleddate`, " +
                                        " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                        " cancelledbyzonecode,canceldetails,cancelcharge,chargeto,remotezonecode,currency,stationid, " +
                                        " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                        " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                        " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'GLOBAL' as servicetype " +
                                        " from kpglobal{2}.`sendout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                        " and year(transdate)='{1}' and cancelreason='{2}' ", fromdate.ToString("MMdd"), fromdate.Year, yr, cancelreason);
                                    }
                                    else if (category == "globalcpo")
                                    {
                                        string yr = (i == 3) ? fromdate.Year.ToString() : "";
                                        str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                             " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                             " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  sobranch as  `senderbranch` , '' as  `receiverbranch` , " +
                                             " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                             " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'CANCEL PAYOUT' as   `txntype`, " +
                                             " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                             " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                             " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid, " +
                                             " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                             " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                             " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'GLOBAL' as servicetype " +
                                             " from kpglobal{2}.`payout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                             " and year(claimeddate)='{1}' and cancelledreason='Wrong Payout' ", fromdate.ToString("MMdd"), fromdate.Year, yr);
                                    }
                                    cmd.CommandText = str;
                                    cmd.CommandTimeout = 0;
                                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = am.dtable();
                                            dt.Load(Reader);
                                            dtmerge.Merge(dt);
                                            Reader.Close();
                                        }
                                    }
                                    fromdate = fromdate.AddDays(1);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cust.WriteToFile("Error getting Global TXN using mysql : " + ex.ToString());
                }
            }
            
            return dtmerge;
        }
        public DataTable GetDomesticTXN(string category, int year, int month)
        {
            AdminModel am = new AdminModel();
            DataTable dtmerge = new DataTable();
            dtmerge = am.dtable();
            var str = string.Empty;
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        var fromdate = new DateTime(year, month, 1);
                        var todate = fromdate.AddMonths(1).AddDays(-1);
                        while (fromdate <= todate)
                        {
                            if (category == "kpso")
                            {
                                str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                    " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,`transdate` ,'0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` ,charge, " +
                                    " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                    " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,custid, " +
                                    " senderlname as lname,senderfname as fname, sendermname as mname,'' as username,'' as walletno, 'SENDOUT' as   `txntype`, " +
                                    " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`, `cancelreason`,   `cancelleddate`, " +
                                    " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                    " cancelledbyzonecode,canceldetails,cancelcharge,chargeto,remotezonecode,currency,stationid, " +
                                    " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                    " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                    " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'DOMESTIC' as servicetype " +
                                    " from `domestic_kpdomestic`.`sendout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                    " and year(transdate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "kppo")
                            {
                                str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                    " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                    " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  sobranch as  `senderbranch` , '' as  `receiverbranch` , " +
                                    " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                    " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'PAYOUT' as   `txntype`, " +
                                    " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                    " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                    " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid, " +
                                    " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                    " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                    " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'DOMESTIC' as servicetype " +
                                    " from `domestic_kpdomestic`.`payout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                    " and year(claimeddate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "kprfc" || category == "kprts" || category == "kpcso")
                            {
                                string cancelreason = (category == "kprfc") ? "Request for Change" : (category == "kprts") ? "Return to Sender" : "Cancel Sendout";
                                str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,`transdate` ,'0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` ,charge, " +
                                " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,custid, " +
                                " senderlname as lname,senderfname as fname, sendermname as mname,'' as username,'' as walletno, '{2}' as   `txntype`, " +
                                " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`, `cancelreason`,   `cancelleddate`, " +
                                " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                " cancelledbyzonecode,canceldetails,cancelcharge,chargeto,remotezonecode,currency,stationid, " +
                                " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'DOMESTIC' as servicetype " +
                                " from domestic_kpdomestic.`sendout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                " and year(transdate)='{1}' and cancelreason='{2}' ", fromdate.ToString("MMdd"), fromdate.Year,   cancelreason);
                            }
                            else if (category == "kpcpo")
                            {
                                str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                     " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                     " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  sobranch as  `senderbranch` , '' as  `receiverbranch` , " +
                                     " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                     " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'CANCEL PAYOUT' as   `txntype`, " +
                                     " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                     " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                     " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid, " +
                                     " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                     " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                     " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'DOMESTIC' as servicetype " +
                                     " from domestic_kpdomestic.`payout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                     " and year(claimeddate)='{1}' and cancelledreason='Wrong Payout' ", fromdate.ToString("MMdd"), fromdate.Year );
                            } 
                            cmd.CommandText = str;
                            cmd.CommandTimeout = 0;
                            using (OdbcDataReader Reader = cmd.ExecuteReader())
                            {
                                if (Reader.HasRows)
                                {
                                    DataTable dt = new DataTable();
                                    dt = am.dtable();
                                    dt.Load(Reader);
                                    dtmerge.Merge(dt);
                                    Reader.Close();
                                }
                            }
                            fromdate = fromdate.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error getting Domestic TXN using hadoop : " + ex.ToString());
            }

            if (dtmerge.Rows.Count==0) {
                try
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        if (i == 1) { conn.connectdb("DomesticB"); }
                        else if (i == 2) { conn.connectdb("CloudDomesticB"); }
                        else if (i == 3) { conn.connectdb("KP8DomesticB"); }

                        using (MySqlConnection mycon = conn.getConnection())
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                var fromdate = new DateTime(year, month, 1);
                                var todate = fromdate.AddMonths(1).AddDays(-1);
                                while (fromdate <= todate)
                                {
                                    if (category == "kpso")
                                    {
                                        string yr = (i == 3) ? fromdate.Year.ToString() : "";
                                        str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                            " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,`transdate` ,'0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` ,charge, " +
                                            " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                            " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,custid, " +
                                            " senderlname as lname,senderfname as fname, sendermname as mname,'' as username,'' as walletno, 'SENDOUT' as   `txntype`, " +
                                            " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`, `cancelreason`,   `cancelleddate`, " +
                                            " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                            " cancelledbyzonecode,canceldetails,cancelcharge,chargeto,remotezonecode,currency,stationid, " +
                                            " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                            " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                            " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'MONEY TRANSFER' as servicetype " +
                                            " from kpdomestic{2}.`sendout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                            " and year(transdate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year, yr);
                                    }
                                    else if (category == "kppo")
                                    {
                                        string yr = (i == 3) ? fromdate.Year.ToString() : "";
                                        str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                            " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                            " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  sobranch as  `senderbranch` , '' as  `receiverbranch` , " +
                                            " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                            " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'PAYOUT' as   `txntype`, " +
                                            " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                            " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                            " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid, " +
                                            " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                            " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                            " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'MONEY TRANSFER' as servicetype " +
                                            " from kpdomestic{2}.`payout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                            " and year(claimeddate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year, yr);
                                    }
                                    else if (category == "kprfc" || category == "kprts" || category == "kpcso")
                                    {
                                        string yr = (i == 3) ? fromdate.Year.ToString() : "";
                                        string cancelreason = (category == "kprfc") ? "Request for Change" : (category == "kprts") ? "Return to Sender" : "Cancel Sendout";
                                        str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                        " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,`transdate` ,'0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` ,charge, " +
                                        " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                        " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,custid, " +
                                        " senderlname as lname,senderfname as fname, sendermname as mname,'' as username,'' as walletno, '{2}' as   `txntype`, " +
                                        " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`, `cancelreason`,   `cancelleddate`, " +
                                        " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                        " cancelledbyzonecode,canceldetails,cancelcharge,chargeto,remotezonecode,currency,stationid, " +
                                        " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                        " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                        " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'MONEY TRANSFER' as servicetype " +
                                        " from kpdomestic{2}.`sendout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                        " and year(transdate)='{1}' and cancelreason='{2}' ", fromdate.ToString("MMdd"), fromdate.Year, yr, cancelreason);
                                    }
                                    else if (category == "kpcpo")
                                    {
                                        string yr = (i == 3) ? fromdate.Year.ToString() : "";
                                        str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                             " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                             " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  sobranch as  `senderbranch` , '' as  `receiverbranch` , " +
                                             " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                             " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'CANCEL PAYOUT' as   `txntype`, " +
                                             " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                             " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                             " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid, " +
                                             " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                             " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                             " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'MONEY TRANSFER' as servicetype " +
                                             " from kpdomestic{2}.`payout{0}`  WHERE lower(kptnno) not rlike 'mlw' and lower(kptnno) not rlike 'mlx' " +
                                             " and year(claimeddate)='{1}' and cancelledreason='Wrong Payout' ", fromdate.ToString("MMdd"), fromdate.Year, yr);
                                    }
                                    cmd.CommandText = str;
                                    cmd.CommandTimeout = 0;
                                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = am.dtable();
                                            dt.Load(Reader);
                                            dtmerge.Merge(dt);
                                            Reader.Close();
                                        }
                                    }
                                    fromdate = fromdate.AddDays(1);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cust.WriteToFile("Error getting Domestic TXN using mysql : " + ex.ToString());
                }
            }
            
            return dtmerge;
        }
        public DataTable GetWalletTXN(string category, int year, int month)
        {
            AdminModel am = new AdminModel();
            DataTable dtmerge = new DataTable();
            dtmerge = am.dtable();
            var str = string.Empty;

            #region hadoop
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        var fromdate = new DateTime(year, month, 1);
                        var todate = fromdate.AddMonths(1).AddDays(-1);
                        while (fromdate <= todate)
                        {
                            if (category == "walletso")
                            {
                                str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                    " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,  `transdate` , '0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` , charge, " +
                                    " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                    " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername, custid, " +
                                    " senderlname as lname,senderfname as fname, sendermname as mname,operatorid as username,'' as walletno, 'SENDOUT' as   `txntype`, " +
                                    " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,   `cancelreason`, `cancelleddate`, " +
                                    " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,0 as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                    " cancelledbyzonecode, canceldetails, cancelcharge, chargeto,remotezonecode,currency,stationid, " +
                                    " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                    " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                    " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'WALLET' as servicetype " +
                                    " from `mobile_kpmobiletransactions`.`sendout{0}`  WHERE lower(kptnno) rlike 'mlw' " +
                                    "  and year(transdate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "walletpo")
                            {
                                str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                    " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                    " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                    " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                    " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'PAYOUT' as   `txntype`, " +
                                    " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                    " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                    " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                    " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                    " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                    " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'WALLET' as servicetype " +
                                    " from `mobile_kpmobiletransactions`.`payout{0}`  WHERE lower(kptnno) rlike 'mlw' " +
                                    " and year(claimeddate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year);
                            }

                            if (category == "walletso" || category == "walletpo")
                            {
                                cmd.CommandText = str;
                                cmd.CommandTimeout = 0;
                                using (OdbcDataReader Reader = cmd.ExecuteReader())
                                {
                                    if (Reader.HasRows)
                                    {
                                        DataTable dt = new DataTable();
                                        dt = am.dtable();
                                        dt.Load(Reader);
                                        dtmerge.Merge(dt);
                                        Reader.Close();
                                    }
                                }
                            }
                            fromdate = fromdate.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error getting WALLET TXN using hadoop : " + ex.ToString());
            }
            #endregion wallet txn
            if (dtmerge.Rows.Count == 0)
            {

                #region mysql

                #region wallet txn
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    conn.connectdb("MobileB");
                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            var fromdate = new DateTime(year, month, 1);
                            var todate = fromdate.AddMonths(1).AddDays(-1);
                            while (fromdate <= todate)
                            {
                                if (category == "walletso")
                                {
                                    str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                         " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,  `transdate` , '0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` , charge, " +
                                         " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                         " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername, custid, " +
                                         " senderlname as lname,senderfname as fname, sendermname as mname,operatorid as username,'' as walletno, 'SENDOUT' as   `txntype`, " +
                                         " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,   `cancelreason`, `cancelleddate`, " +
                                         " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,0 as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                         " cancelledbyzonecode, canceldetails, cancelcharge, chargeto,remotezonecode,currency,stationid, " +
                                         " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                         " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                         " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'WALLET' as servicetype " +
                                         " from `kpmobiletransactions`.`sendout{0}`  WHERE lower(kptnno) rlike 'mlw' " +
                                         "  and year(transdate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year);
                                }
                                else if (category == "walletpo")
                                {
                                    str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                       " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                       " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                       " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                       " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'PAYOUT' as   `txntype`, " +
                                       " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                       " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                       " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                       " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                       " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                       " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'WALLET' as servicetype " +
                                       " from `kpmobiletransactions`.`payout{0}`  WHERE lower(kptnno) rlike 'mlw' " +
                                       " and year(claimeddate)='{1}' ", fromdate.ToString("MMdd"), fromdate.Year);
                                }
                                else if (category == "walletrfc" || category == "walletrts" || category == "walletcso")
                                {
                                    string cancelreason = (category == "walletrfc") ? "Request for Change" : (category == "walletrts") ? "Return to Sender" : "Cancel Sendout";
                                    str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                        " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,  `transdate` , '0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` , charge, " +
                                        " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                        " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername, custid, " +
                                        " senderlname as lname,senderfname as fname, sendermname as mname,operatorid as username,'' as walletno, '{2}' as   `txntype`, " +
                                        " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,   `cancelreason`, `cancelleddate`, " +
                                        " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,0 as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                        " cancelledbyzonecode, canceldetails, cancelcharge, chargeto,remotezonecode,currency,stationid, " +
                                        " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                        " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                        " '' as accountname,'' as payor,'' as otherdetails,purpose,source as sourceoffund,'WALLET' as servicetype " +
                                        " from `kpmobiletransactions`.`sendout{0}`  WHERE lower(kptnno) rlike 'mlw' " +
                                        "  and year(transdate)='{1}' and upper(cancelreason)='{2}' ", fromdate.ToString("MMdd"), fromdate.Year, cancelreason.ToUpper());
                                }
                                else if (category == "walletcpo")
                                {
                                    str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                      " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                      " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                      " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                      " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'CANCEL PAYOUT' as   `txntype`, " +
                                      " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                      " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                      " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                      " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                      " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                      " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'WALLET' as servicetype " +
                                      " from `kpmobiletransactions`.`payout{0}`  WHERE lower(kptnno) rlike 'mlw' " +
                                      " and year(claimeddate)='{1}' and cancelledreason='Wrong Payout' ", fromdate.ToString("MMdd"), fromdate.Year);
                                }
                                if (category == "walletso" || category == "walletpo" || category == "walletrfc" || category == "walletrts" || category == "walletcso" || category == "walletcpo")
                                {
                                    cmd.CommandText = str;
                                    cmd.CommandTimeout = 0;
                                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = am.dtable();
                                            dt.Load(Reader);
                                            dtmerge.Merge(dt);
                                            Reader.Close();
                                        }
                                    }
                                }
                                fromdate = fromdate.AddDays(1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cust.WriteToFile("Error getting Wallet TXN using mysql : " + ex.ToString());
                }
                #endregion  wallet txn
                #region billspay
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    conn.connectdb("BillspayB");
                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            var fromdate = new DateTime(year, month, 1);
                            var todate = fromdate.AddMonths(1).AddDays(-1);
                            while (fromdate <= todate)
                            {
                                if (category == "walletbp")
                                {
                                    str = string.Format(" SELECT '' AS emailadd,'' AS birthdate,payorcontactno AS mobileno, " +
                                    " payoraddress AS address,  '' AS gender,'' AS `refno` , kptnno AS  `kptn` ,  " +
                                    " `transdate` , '0000-00-00 00:00:00' AS `claimeddate` ,amountpaid AS  `amount` , customercharge,  0 AS  `commission` , " +
                                    " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` , '' AS   receivername, " +
                                    " b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, CONCAT(accountfname,' ',accountmname,' ',accountlname) AS sendername, " +
                                    " '' AS custid,  payorlname AS lname, payorfname AS fname, payormname AS mname,operatorid AS username,'' AS walletno, 'WALLET BILLSPAY' AS   `txntype`, " +
                                    "  oldkptnno AS  `oldkptn` ,'' AS ptn,  '' AS `oldptn`,companyid AS  `accountcode`,   `cancelreason`, `cancelleddate`,  " +
                                    "  controlno,irno,'' AS orno,operatorid,'' AS isremote,'' AS remotebranch,remoteoperatorid,0 AS othercharge,branchcode," +
                                    "  cancelledbyoperatorid,cancelledbybranchcode,  cancelledbyzonecode, canceldetails, cancelcharge, '' AS chargeto," +
                                    "  remotezonecode,currency,stationid,'' AS maturitydate,'' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  " +
                                    "  '' AS weight,'' AS karat, '' AS carat,'' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname," +
                                    "   CONCAT(payorfname,' ',payormname,' ',payorlname) AS payor,accountno AS otherdetails,'' AS purpose,'' AS sourceoffund,'WALLET' AS servicetype  " +
                                    "  FROM `kpbillspayment`.`sendout{0}`  a " +
                                    "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.companyid " +
                                    "  WHERE LOWER(kptnno) RLIKE 'bpw'   AND YEAR(transdate)='{1}'  ", fromdate.ToString("MMdd"), fromdate.Year);

                                    cmd.CommandText = str;
                                    cmd.CommandTimeout = 0;
                                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = am.dtable();
                                            dt.Load(Reader);
                                            dtmerge.Merge(dt);
                                            Reader.Close();
                                        }
                                    }
                                }
                                fromdate = fromdate.AddDays(1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cust.WriteToFile("Error getting  Wallet Billspay TXN using mysql : " + ex.ToString());
                }
                #endregion billspay
                #region eload
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    conn.connectdb("EloadB");
                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            var fromdate = new DateTime(year, month, 1);
                            var todate = fromdate.AddMonths(1).AddDays(-1);
                            while (fromdate <= todate)
                            {
                                if (category == "walleteload")
                                {
                                    str = string.Format(" SELECT emailadd,'' AS birthdate,mobileno, address,  '' AS gender,'' AS `refno` , " +
                                        " transno AS  `kptn` ,  `transdate` , '0000-00-00 00:00:00' AS `claimeddate` ,  `amount` , " +
                                        " 0 AS customercharge,  0 AS  `commission` ,  zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , " +
                                        " '' AS  `receiverbranch` , '' AS   receivername, '' AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, " +
                                        " '' AS sendername,  '' AS custid,  '' AS lname, '' AS fname, ''  AS mname,operator AS username,'' AS walletno, " +
                                        " 'WALLET ELOAD' AS   `txntype`, '' AS  `oldkptn` ,'' AS ptn,  '' AS `oldptn`,a.networkid AS  `accountcode`, " +
                                        " '' AS `cancelreason`,'' AS  `cancelleddate`, '' AS controlno,'' AS irno,'' AS orno,operator AS operatorid, " +
                                        " '' AS isremote,'' AS remotebranch,'' AS remoteoperatorid,0 AS othercharge,branchcode, " +
                                        " '' AS cancelledbyoperatorid,'' AS cancelledbybranchcode, '' AS cancelledbyzonecode, '' AS canceldetails, " +
                                        " '' AS  cancelcharge, '' AS chargeto, '' AS remotezonecode,'' AS currency,'' AS stationid,'' AS maturitydate, " +
                                        " '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity, '' AS weight,'' AS karat, '' AS carat, " +
                                        " '' AS appraiser,'' AS operator,0 AS discount,productcode AS itemcode, network AS accountname, contactperson AS payor, " +
                                        " loadtype AS otherdetails,'' AS purpose,'' AS sourceoffund,'WALLET' AS servicetype " +
                                        " FROM `ELoadTransactions`.`TransLog{0}`  a " +
                                        " INNER JOIN `ELoadAdmin`.`NetworkInfo` b ON b.networkid=a.networkid " +
                                        " WHERE LOWER(transno) RLIKE 'mwe'   AND YEAR(transdate)='{1}'  ", fromdate.ToString("MM"), fromdate.Year);

                                    cmd.CommandText = str;
                                    cmd.CommandTimeout = 0;
                                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = am.dtable();
                                            dt.Load(Reader);
                                            dtmerge.Merge(dt);
                                            Reader.Close();
                                        }
                                    }
                                }

                                fromdate = fromdate.AddDays(1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cust.WriteToFile("Error getting  Wallet Eload TXN using mysql : " + ex.ToString());
                }
                #endregion eload
                #region corp po
                //corp po
                try
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        //OdbcCommand cmd = new OdbcCommand();
                        MySqlCommand cmd = new MySqlCommand();
                        //using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                        if (i == 0) { conn.connectdb("APIB"); }
                        else if (i == 1) { conn.connectdb("APIB"); }
                        else if (i == 2) { conn.connectdb("RuralNetB"); }
                        else if (i == 3) { conn.connectdb("APINewB"); }
                        else if (i == 4) { conn.connectdb("FileUploadB"); }
                        else if (i == 5) { conn.connectdb("WSCB"); }

                        using (MySqlConnection mycon = conn.getConnection())
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                var fromdate = new DateTime(year, month, 1);
                                var todate = fromdate.AddMonths(1).AddDays(-1);
                                while (fromdate <= todate)
                                {
                                    if (category == "walletcorpo")
                                    {
                                        if (i == 0 || i == 4 || i == 5)
                                        { //old api
                                            str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                                " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                                " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                                " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                                " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                                "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                                "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                                "  '' AS walletno, 'CORPORATE PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                                "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                                "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                                "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                                "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                                "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                                "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                                "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                                "  FROM `kppartners`.`payout{0}`  a" +
                                                "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                                "  WHERE  YEAR(claimeddate)='{1}' and controlno  rlike 'mlw'  ", fromdate.ToString("MMdd"), fromdate.Year);
                                        }
                                        else if (i == 1)
                                        { //old api aub
                                            str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                                 " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                                 " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                                 " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                                 " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                                 "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                                 "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                                 "  '' AS walletno, 'CORPORATE PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                                 "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                                 "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                                 "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                                 "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                                 "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                                 "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                                 "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                                 "  FROM `kppartnersAUB`.`payout{0}`  a" +
                                                 "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                                 "  WHERE  YEAR(claimeddate)='{1}' and controlno  rlike 'mlw' ", fromdate.ToString("MMdd"), fromdate.Year);
                                        }
                                        else if (i == 2)
                                        { //rural net
                                            if (fromdate.Year >= 2018)
                                            {
                                                str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                                   " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,if(servicecharge is null,0,servicecharge) as charge, " +
                                                   " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                                   " receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                                   " receiverlname as lname,receiverfname as fname, receivermname as mname,operatorid as username,'' as walletno, 'CORPORATE PAYOUT' as   `txntype`, " +
                                                   " if(oldkptnno is null,'',oldkptnno) as  `oldkptn` ,'' as ptn,  '' as `oldptn`,a.accountid as  `accountcode`,if(cancelledreason is null,'',cancelledreason) as  `cancelreason`, " +
                                                   " if(cancelleddate is null,'',cancelleddate) as `cancelleddate`, " +
                                                   " controlno,if(irno is null,'',irno) as irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid, " +
                                                   " if(cancelledempcharge  is null,0,cancelledempcharge ) as othercharge,branchcode,if(cancelledbyoperatorid is null,'',cancelledbyoperatorid) as cancelledbyoperatorid, " +
                                                   "  if(cancelledbybranchcode  is null,'',cancelledbybranchcode )  as cancelledbybranchcode, " +
                                                   " '' as cancelledbyzonecode,reason as canceldetails,if(cancelledcustcharge  is null,0,cancelledcustcharge ) as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                                   " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                                   " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                                   " b.accountname as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'CORPORATE' as servicetype " +
                                                   " from `payout{0}`.`payout{0}` a INNER JOIN kpadminpartners.accountlist b ON b.accountid=b.accountid where  controlno  rlike 'mlw' ", fromdate.ToString("MMddyyyy"));
                                            }
                                        }
                                        else if (i == 3)
                                        { //new api
                                            str = string.Format("  SELECT receiverbirthdate AS birthdate,'' AS emailadd,receivercontactno AS mobileno, " +
                                              " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                              " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                              " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                              " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                              "  receivername,b.accountname  AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                              "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                              "  '' AS walletno, 'CORPORATE PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                              "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                              "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                              "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                              "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                              "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                              "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                              "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                              "  FROM `kppartners`.`payout{0}`  a" +
                                              "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                              "  WHERE  YEAR(claimeddate)='{1}' and controlno  rlike 'mlw' ", fromdate.ToString("MMdd"), fromdate.Year);
                                        }
                                        cmd.CommandText = str;
                                        cmd.CommandTimeout = 0;
                                        //using (OdbcDataReader Reader = cmd.ExecuteReader())
                                        using (MySqlDataReader Reader = cmd.ExecuteReader())
                                        {
                                            if (Reader.HasRows)
                                            {
                                                DataTable dt = new DataTable();
                                                dt = am.dtable();
                                                dt = am.dtable();
                                                dtmerge.Merge(dt, true, MissingSchemaAction.Ignore);
                                                Reader.Close();
                                            }
                                        }
                                    }

                                    fromdate = fromdate.AddDays(1);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cust.WriteToFile("Error getting WALLET Corp PO TXN : " + ex.ToString());
                }
                #endregion corp po

                #endregion mysql
            }
            return dtmerge;
        }
        public DataTable GetExpressTXN(string category, int year, int month)
        {
            AdminModel am = new AdminModel();
            DataTable dtmerge = new DataTable();
            dtmerge = am.dtable();
            var str = string.Empty;

            #region hadoop
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        var fromdate = new DateTime(year, month, 1);
                        var todate = fromdate.AddMonths(1).AddDays(-1);
                        while (fromdate <= todate)
                        {
                            if (category == "expressso")
                            {
                                if (fromdate.Year >= 2015)
                                {
                                    str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                   " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,  `transdate` , '0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` , charge, " +
                                   " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                   " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername, custid, " +
                                   " senderlname as lname,senderfname as fname, sendermname as mname,operatorid as username,'' as walletno, 'SENDOUT' as   `txntype`, " +
                                   " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,   `cancelreason`, `cancelleddate`, " +
                                   " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,0 as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                   " cancelledbyzonecode, canceldetails, cancelcharge, chargeto,remotezonecode,currency,stationid,  " +
                                   " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                   " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                   " '' as accountname,'' as payor,'' as otherdetails,  purpose,source as sourceoffund,'EXPRESS' as servicetype " +
                                   " from `express_reports{2}{1}`.`sendout`  WHERE lower(kptnno) rlike 'mlx' " +
                                   " AND  day(transdate)='{0}' ", fromdate.Day.ToString().PadLeft(2, '0'), fromdate.Year, fromdate.Month.ToString().PadLeft(2, '0'));
                                }
                            }
                            else if (category == "expresspo")
                            {
                                if (fromdate.Year >= 2015)
                                {
                                    str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                    " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                    " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                    " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                    " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'PAYOUT' as   `txntype`, " +
                                    " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                    " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                    " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                    " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                    " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                    " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'EXPRESS' as servicetype " +
                                    " from `express_reports{2}{1}`.`payout`  WHERE lower(kptnno) rlike 'mlx' " +
                                    " and day(claimeddate)='{0}' ", fromdate.Day.ToString().PadLeft(2, '0'), fromdate.Year, fromdate.Month.ToString().PadLeft(2, '0'));
                                }
                            }
                            if (category == "expressso" || category == "expresspo" )
                            {
                                cmd.CommandText = str;
                                cmd.CommandTimeout = 0;
                                using (OdbcDataReader Reader = cmd.ExecuteReader())
                                {
                                    if (Reader.HasRows)
                                    {
                                        DataTable dt = new DataTable();
                                        dt = am.dtable();
                                        dt.Load(Reader);
                                        dtmerge.Merge(dt, true, MissingSchemaAction.Ignore);
                                        Reader.Close();
                                    }
                                }
                            }
                            fromdate = fromdate.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error getting EXPRESS TXN : " + ex.ToString());
            }
            #endregion hadoop
            if (dtmerge.Rows.Count == 0)
            {

                #region mysql

                #region express txn
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    conn.connectdb("ExpressB");
                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            var fromdate = new DateTime(year, month, 1);
                            var todate = fromdate.AddMonths(1).AddDays(-1);
                            while (fromdate <= todate)
                            {
                                if (category == "expressso")
                                {
                                    if (fromdate.Year >= 2015)
                                    {
                                        str = string.Format(" select '' as emailadd,senderbirthdate as birthdate,sendercontactno as mobileno,concat(senderstreet,' ',senderprovincecity) as address, " +
                                       " sendergender as gender,'' as `refno` , kptnno as  `kptn` ,  `transdate` , '0000-00-00 00:00:00' as `claimeddate` ,principal as  `amount` , charge, " +
                                       " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                       " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername, custid, " +
                                       " senderlname as lname,senderfname as fname, sendermname as mname,operatorid as username,'' as walletno, 'SENDOUT' as   `txntype`, " +
                                       " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,   `cancelreason`, `cancelleddate`, " +
                                       " controlno,irno,orno,operatorid,isremote,remotebranch,remoteoperatorid,0 as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                       " cancelledbyzonecode, canceldetails, cancelcharge, chargeto,remotezonecode,currency,stationid,  " +
                                       " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                       " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                       " '' as accountname,'' as payor,'' as otherdetails,  purpose,source as sourceoffund,'EXPRESS' as servicetype " +
                                       " from `Reports{2}{1}`.`sendout`  WHERE lower(kptnno) rlike 'mlx' " +
                                       " AND  day(transdate)='{0}' ", fromdate.Day.ToString().PadLeft(2, '0'), fromdate.Year, fromdate.Month.ToString().PadLeft(2, '0'));
                                    }
                                }
                                else if (category == "expresspo")
                                {
                                    if (fromdate.Year >= 2015)
                                    {
                                        str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                        " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                        " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                        " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                        " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'PAYOUT' as   `txntype`, " +
                                        " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                        " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                        " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                        " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                        " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                        " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'EXPRESS' as servicetype " +
                                        " from Reports{2}{1}.`payout`  WHERE lower(kptnno) rlike 'mlx' " +
                                        " and day(claimeddate)='{0}' ", fromdate.Day.ToString().PadLeft(2, '0'), fromdate.Year, fromdate.Month.ToString().PadLeft(2, '0'));
                                    }
                                }
                                else if (category == "expressrfc" || category == "expressrts" || category == "expresscso")
                                {
                                    string cancelreason = (category == "expressrfc") ? "Request for Change" : (category == "expressrts") ? "Return to Sender" : "Cancel Sendout";
                                    if (fromdate.Year >= 2015)
                                    {
                                        str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                        " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                        " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                        " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                        " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, '{3}' as   `txntype`, " +
                                        " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,canceldetails as  `cancelreason`, `cancelleddate`, " +
                                        " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                        " '' as cancelledbyzonecode,cancelreason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                        " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                        " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                        " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'EXPRESS' as servicetype " +
                                        " from `Reports{2}{1}`.`socancel`  WHERE lower(kptnno) rlike 'mlx' " +
                                        " and day(claimeddate)='{0}' and upper(canceldetails)='{3}' ", fromdate.Day.ToString().PadLeft(2, '0'), fromdate.Year, fromdate.Month.ToString().PadLeft(2, '0'), cancelreason.ToUpper());
                                    }
                                }
                                else if (category == "expresscpo")
                                {
                                    if (fromdate.Year >= 2015)
                                    {
                                        str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                        " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                        " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                        " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                        " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, 'CANCEL PAYOUT' as   `txntype`, " +
                                        " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,cancelledreason as  `cancelreason`, `cancelleddate`, " +
                                        " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                        " '' as cancelledbyzonecode,reason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                        " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                        " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                        " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'EXPRESS' as servicetype " +
                                        " from Reports{2}{1}.`pocancel`  WHERE lower(kptnno) rlike 'mlx' " +
                                        " and day(claimeddate)='{0}' and cancelledreason is not null ", fromdate.Day.ToString().PadLeft(2, '0'), fromdate.Year, fromdate.Month.ToString().PadLeft(2, '0'));
                                    }
                                }
                                else if (category == "expresscorppo")
                                {
                                    if (fromdate.Year >= 2015)
                                    {
                                        str = string.Format("  SELECT '' AS emailadd,'' AS birthdate,receiverphone AS mobileno, " +
                                            "   '' AS address,  '' AS gender,reference AS `refno` ,`kptn` ,   " +
                                            "   txndate AS `transdate` , '0000-00-00 00:00:00' AS `claimeddate` , `amount` , charge,   `commission` ,  " +
                                            "   1 AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` , '' AS   receivername,  " +
                                            "   sendername AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`,  sendername,  " +
                                            "   '' AS custid,  '' AS lname, '' AS fname, '' AS mname,operator AS username, walletno, 'CORPORATE PAYOUT' AS   `txntype`,  " +
                                            "    '' AS  `oldkptn` ,'' AS ptn,  '' AS `oldptn`,partnersname AS  `accountcode`, ''  `cancelreason`, ''AS `cancelleddate`,   " +
                                            "    controlno,'' AS irno,'' AS orno,operator AS operatorid,'' AS isremote,'' AS remotebranch,'' AS remoteoperatorid,0 AS othercharge,001 AS branchcode, " +
                                            "    '' AS cancelledbyoperatorid,'' AS cancelledbybranchcode, '' AS  cancelledbyzonecode, '' AS canceldetails,'' AS  cancelcharge, '' AS chargeto, " +
                                            "    '' AS remotezonecode,currency,'' AS stationid,'' AS maturitydate,'' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,   " +
                                            "    '' AS weight,'' AS karat, '' AS carat,'' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  sendername AS accountname, " +
                                            "    '' AS payor,'' AS otherdetails,'' AS purpose,'' AS sourceoffund,'EXPRESS' AS servicetype   " +
                                            "   FROM `KPPartnersExpress`.`apihistory`   " +
                                            "   WHERE  YEAR(txndate)='{0}' AND MONTH(txndate)='{1}' ", fromdate.Year, fromdate.Month.ToString().PadLeft(2, '0'));
                                    }
                                }
                                if (category == "expressso" || category == "expresspo" || category == "expressrfc" || category == "expressrts" || category == "expresscso" || category == "expresscpo" || category == "expresscorppo")
                                {
                                    cmd.CommandText = str;
                                    cmd.CommandTimeout = 0;
                                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = am.dtable();
                                            dt.Load(Reader);
                                            dtmerge.Merge(dt, true, MissingSchemaAction.Ignore);
                                            Reader.Close();
                                        }
                                    }
                                }
                                fromdate = fromdate.AddDays(1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cust.WriteToFile("Error getting Express TXN using mysql : " + ex.ToString());
                }
                #endregion  express txn
                #region billspay
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    conn.connectdb("BillspayB");
                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            var fromdate = new DateTime(year, month, 1);
                            var todate = fromdate.AddMonths(1).AddDays(-1);
                            while (fromdate <= todate)
                            {
                                if (category == "expressbp")
                                {
                                    str = string.Format(" SELECT '' AS emailadd,'' AS birthdate,payorcontactno AS mobileno, " +
                                    " payoraddress AS address,  '' AS gender,'' AS `refno` , kptnno AS  `kptn` ,  " +
                                    " `transdate` , '0000-00-00 00:00:00' AS `claimeddate` ,amountpaid AS  `amount` , customercharge,  0 AS  `commission` , " +
                                    " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` , '' AS   receivername, " +
                                    " b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, CONCAT(accountfname,' ',accountmname,' ',accountlname) AS sendername, " +
                                    " '' AS custid,  payorlname AS lname, payorfname AS fname, payormname AS mname,operatorid AS username,'' AS walletno, 'EXPRESS BILLSPAY' AS   `txntype`, " +
                                    "  oldkptnno AS  `oldkptn` ,'' AS ptn,  '' AS `oldptn`,companyid AS  `accountcode`,   `cancelreason`, `cancelleddate`,  " +
                                    "  controlno,irno,'' AS orno,operatorid,'' AS isremote,'' AS remotebranch,remoteoperatorid,0 AS othercharge,branchcode," +
                                    "  cancelledbyoperatorid,cancelledbybranchcode,  cancelledbyzonecode, canceldetails, cancelcharge, '' AS chargeto," +
                                    "  remotezonecode,currency,stationid,'' AS maturitydate,'' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  " +
                                    "  '' AS weight,'' AS karat, '' AS carat,'' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname," +
                                    "   CONCAT(payorfname,' ',payormname,' ',payorlname) AS payor,accountno AS otherdetails,'' AS purpose,'' AS sourceoffund,'WALLET' AS servicetype  " +
                                    "  FROM `kpbillspayment`.`sendout{0}`  a " +
                                    "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.companyid " +
                                    "  WHERE LOWER(kptnno) RLIKE 'bpx'   AND YEAR(transdate)='{1}'  ", fromdate.ToString("MMdd"), fromdate.Year);

                                    cmd.CommandText = str;
                                    cmd.CommandTimeout = 0;
                                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = am.dtable();
                                            dt.Load(Reader);
                                            dtmerge.Merge(dt, true, MissingSchemaAction.Ignore);
                                            Reader.Close();
                                        }
                                    }
                                }
                                fromdate = fromdate.AddDays(1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cust.WriteToFile("Error getting  Express Billspay TXN using mysql : " + ex.ToString());
                }
                #endregion billspay
                #region eload
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    conn.connectdb("EloadB");
                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            var fromdate = new DateTime(year, month, 1);
                            var todate = fromdate.AddMonths(1).AddDays(-1);
                            while (fromdate <= todate)
                            {
                                if (category == "expresseload")
                                {
                                    str = string.Format(" SELECT emailadd,'' AS birthdate,mobileno, address,  '' AS gender,'' AS `refno` , " +
                                        " transno AS  `kptn` ,  `transdate` , '0000-00-00 00:00:00' AS `claimeddate` ,  `amount` , " +
                                        " 0 AS customercharge,  0 AS  `commission` ,  zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , " +
                                        " '' AS  `receiverbranch` , '' AS   receivername, '' AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, " +
                                        " '' AS sendername,  '' AS custid,  '' AS lname, '' AS fname, ''  AS mname,operator AS username,'' AS walletno, " +
                                        " 'EXPRESS ELOAD' AS   `txntype`, '' AS  `oldkptn` ,'' AS ptn,  '' AS `oldptn`,a.networkid AS  `accountcode`, " +
                                        " '' AS `cancelreason`,'' AS  `cancelleddate`, '' AS controlno,'' AS irno,'' AS orno,operator AS operatorid, " +
                                        " '' AS isremote,'' AS remotebranch,'' AS remoteoperatorid,0 AS othercharge,branchcode, " +
                                        " '' AS cancelledbyoperatorid,'' AS cancelledbybranchcode, '' AS cancelledbyzonecode, '' AS canceldetails, " +
                                        " '' AS  cancelcharge, '' AS chargeto, '' AS remotezonecode,'' AS currency,'' AS stationid,'' AS maturitydate, " +
                                        " '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity, '' AS weight,'' AS karat, '' AS carat, " +
                                        " '' AS appraiser,'' AS operator,0 AS discount,productcode AS itemcode, network AS accountname, contactperson AS payor, " +
                                        " loadtype AS otherdetails,'' AS purpose,'' AS sourceoffund,'WALLET' AS servicetype " +
                                        " FROM `ELoadTransactions`.`TransLog{0}`  a " +
                                        " INNER JOIN `ELoadAdmin`.`NetworkInfo` b ON b.networkid=a.networkid " +
                                        " WHERE LOWER(transno) RLIKE 'elx'   AND YEAR(transdate)='{1}'  ", fromdate.ToString("MM"), fromdate.Year);

                                    cmd.CommandText = str;
                                    cmd.CommandTimeout = 0;
                                    using (MySqlDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = am.dtable();
                                            dt.Load(Reader);
                                            dtmerge.Merge(dt, true, MissingSchemaAction.Ignore);
                                            Reader.Close();
                                        }
                                    }
                                }
                                fromdate = fromdate.AddDays(1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    cust.WriteToFile("Error getting  Express Eload TXN using mysql : " + ex.ToString());
                }
                #endregion eload

                #endregion mysql
            }

            return dtmerge;
        }
        public DataTable GetAPITXN(string category, int year, int month)
        {
            AdminModel am = new AdminModel();
            DataTable dtmerge = new DataTable();
            dtmerge = am.dtable();
            var str = string.Empty;
            try
            {
                for (int i = 0; i <= 3; i++)
                {
                    //OdbcCommand cmd = new OdbcCommand();
                    MySqlCommand cmd = new MySqlCommand();
                    //using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                    if (i == 0) { conn.connectdb("APIB"); }
                    else if (i == 1) { conn.connectdb("APIB"); }
                    else if (i == 2) { conn.connectdb("RuralNetB"); }
                    else { conn.connectdb("APINewB"); }

                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            var fromdate = new DateTime(year, month, 1);
                            var todate = fromdate.AddMonths(1).AddDays(-1);
                            while (fromdate <= todate)
                            {

                                if (i == 0 || i == 3)
                                { //old api
                                    if (category == "apipo")
                                    {
                                        str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                            " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                            " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                            " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                            " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                            "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                            "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                            "  '' AS walletno, 'PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                            "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                            "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                            "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                            "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                            "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                            "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                            "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                            "  FROM `kppartners`.`payout{0}`  a" +
                                            "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                            "  WHERE  YEAR(claimeddate)='{1}' and  lower(controlno) not rlike 'mlw'  ", fromdate.ToString("MMdd"), fromdate.Year);
                                    }
                                    else if (category == "apirfc" || category == "apirts" || category == "apicso")
                                    {
                                        string cancelreason = (category == "apirfc") ? "Request for Change" : (category == "apirts") ? "Return to Sender" : "Cancel Sendout";
                                        str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                            " receiveraddress AS address,  receivergender AS gender, " +
                                            " referenceno AS `refno` , `kptn` , `transdate` , '' AS `claimeddate` , " +
                                            " principal AS  `amount` , charge,  0 AS  `commission` ,  " +
                                            " '' AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                            "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                            "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,'' AS username," +
                                            "  '' AS walletno, '{2}' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                            "  `accountcode`,  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                            "  operatorid,'' AS isremote,'' AS remotebranch,'' AS remoteoperatorid,0 AS othercharge,branchcode," +
                                            "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,canceldetails," +
                                            "    cancelcharge,'' AS chargeto,'' AS remotezonecode,currency,stationid,  '' AS maturitydate," +
                                            "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                            "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor, otherdetails," +
                                            "  '' AS purpose,IF(sourceoffund IS NULL,'',sourceoffund) AS sourceoffund,'CORPORATE' AS servicetype  " +
                                            "  FROM `kppartners`.`sendout{0}`  a" +
                                            "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                            "  WHERE  YEAR(transdate)='{1}' AND lower(controlno) NOT RLIKE 'mlw' AND upper(cancelreason)='{2}'  ", fromdate.ToString("MMdd"), fromdate.Year,cancelreason.ToUpper() );
                                    }
                                    else if (category == "apicpo" )
                                    {
                                        str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                              " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                              " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                              " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                              " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                              "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                              "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                              "  '' AS walletno, 'CANCEL PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                              "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                              "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                              "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                              "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                              "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                              "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                              "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                              "  FROM `kppartners`.`payout{0}`  a" +
                                              "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                              "  WHERE  YEAR(claimeddate)='{1}' and  lower(controlno) not rlike 'mlw' and cancelleddate is not null " +
                                              "  and cancelleddate<>'0000-00-00 00:00:00' AND  cancelledtype='Payout' ", fromdate.ToString("MMdd"), fromdate.Year);
                                    }
                                }
                                else if (i == 1)
                                { //old api aub
                                    if (category == "apipo")
                                    {
                                        str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                             " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                             " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                             " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                             " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                             "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                             "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                             "  '' AS walletno, 'PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                             "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                             "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                             "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                             "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                             "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                             "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                             "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                             "  FROM `kppartnersAUB`.`payout{0}`  a" +
                                             "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                             "  WHERE  YEAR(claimeddate)='{1}' AND lower(controlno) not rlike 'mlw' ", fromdate.ToString("MMdd"), fromdate.Year);
                                    }
                                }
                                else if (i == 2)
                                { //rural net
                                    if (fromdate.Year >= 2018)
                                    {
                                        if (category == "apipo")
                                        {
                                            str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                           " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,if(servicecharge is null,0,servicecharge) as charge, " +
                                           " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                           " receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                           " receiverlname as lname,receiverfname as fname, receivermname as mname,operatorid as username,'' as walletno, 'PAYOUT' as   `txntype`, " +
                                           " if(oldkptnno is null,'',oldkptnno) as  `oldkptn` ,'' as ptn,  '' as `oldptn`,a.accountid as  `accountcode`,if(cancelledreason is null,'',cancelledreason) as  `cancelreason`, " +
                                           " if(cancelleddate is null,'',cancelleddate) as `cancelleddate`, " +
                                           " controlno,if(irno is null,'',irno) as irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid, " +
                                           " if(cancelledempcharge  is null,0,cancelledempcharge ) as othercharge,branchcode,if(cancelledbyoperatorid is null,'',cancelledbyoperatorid) as cancelledbyoperatorid, " +
                                           "  if(cancelledbybranchcode  is null,'',cancelledbybranchcode )  as cancelledbybranchcode, " +
                                           " '' as cancelledbyzonecode,reason as canceldetails,if(cancelledcustcharge  is null,0,cancelledcustcharge ) as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                           " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                           " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                           " b.accountname as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'CORPORATE' as servicetype " +
                                           " from `payout{0}`.`payout{0}` a INNER JOIN kpadminpartners.accountlist b ON b.accountid=b.accountid where   lower(controlno) not rlike 'mlw' ", fromdate.ToString("MMddyyyy"));
                                        }
                                        else if (category == "apirfc" || category == "apirts" || category == "apicso")
                                        {
                                            string cancelreason = (category == "apirfc") ? "Request for Change" : (category == "apirts") ? "Return to Sender" : "Cancel Sendout";
                                            str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                             " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,servicecharge as charge, " +
                                             " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                             " receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                             " receiverlname as lname,receiverfname as fname, receivermname as mname,'' as username,'' as walletno, '{1}' as   `txntype`, " +
                                             " oldkptnno as  `oldkptn` ,'' as ptn,  '' as `oldptn`,'' as  `accountcode`,canceldetails as  `cancelreason`, `cancelleddate`, " +
                                             " controlno,irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge as othercharge,branchcode,cancelledbyoperatorid,cancelledbybranchcode, " +
                                             " '' as cancelledbyzonecode,cancelreason as canceldetails,cancelledcustcharge as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                             " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                             " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                             " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'CORPORATE' as servicetype " +
                                             " from `sendout{0}`.`sendout{0}` a INNER JOIN kpadminpartners.accountlist b ON b.accountid=b.accountid where   upper(canceldetails)='{1}' and  lower(controlno) not rlike 'mlw' ", fromdate.ToString("MMddyyyy"), cancelreason.ToUpper());
                                        }
                                        else if (category == "apicpo")
                                        {
                                            str = string.Format(" select '' as emailadd,receiverbirthdate as birthdate,receivercontactno as mobileno,concat(receiverstreet,' ',receiverprovincecity) as address, " +
                                         " receivergender as gender,'' as `refno` , kptnno as  `kptn` ,sodate as `transdate` , `claimeddate` ,principal as  `amount` ,if(servicecharge is null,0,servicecharge) as charge, " +
                                         " 0 as  `commission` , zonecode as  `zcode`,'' as `zone` ,  senderbranchid as  `senderbranch` , '' as  `receiverbranch` , " +
                                         " receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, sendername,'' as custid, " +
                                         " receiverlname as lname,receiverfname as fname, receivermname as mname,operatorid as username,'' as walletno, 'CANCEL PAYOUT' as   `txntype`, " +
                                         " if(oldkptnno is null,'',oldkptnno) as  `oldkptn` ,'' as ptn,  '' as `oldptn`,a.accountid as  `accountcode`,if(cancelledreason is null,'',cancelledreason) as  `cancelreason`, " +
                                         " if(cancelleddate is null,'',cancelleddate) as `cancelleddate`, " +
                                         " controlno,if(irno is null,'',irno) as irno,'' as orno,operatorid,isremote,remotebranch,remoteoperatorid, " +
                                         " if(cancelledempcharge  is null,0,cancelledempcharge ) as othercharge,branchcode,if(cancelledbyoperatorid is null,'',cancelledbyoperatorid) as cancelledbyoperatorid, " +
                                         "  if(cancelledbybranchcode  is null,'',cancelledbybranchcode )  as cancelledbybranchcode, " +
                                         " '' as cancelledbyzonecode,reason as canceldetails,if(cancelledcustcharge  is null,0,cancelledcustcharge ) as cancelcharge,'' as chargeto,remotezonecode,currency,stationid,  " +
                                         " '' as maturitydate,'' as expirydate,0 as advanceinterest,'' as itemdesc,0 as quantity, " +
                                         " '' as weight,'' as karat,'' as carat,'' as appraiser,'' as operator,0 as discount,'' as itemcode, " +
                                         " b.accountname as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'CORPORATE' as servicetype " +
                                         " from `payout{0}`.`payout{0}` a INNER JOIN kpadminpartners.accountlist b ON b.accountid=b.accountid where  cancelledreason='WRONG PAYOUT' and  lower(controlno) not rlike 'mlw' ", fromdate.ToString("MMddyyyy"));
                                        }
                                    }
                                }
                                cmd.CommandText = str;
                                cmd.CommandTimeout = 0;
                                //using (OdbcDataReader Reader = cmd.ExecuteReader())
                                using (MySqlDataReader Reader = cmd.ExecuteReader())
                                {
                                    if (Reader.HasRows)
                                    {
                                        DataTable dt = new DataTable();
                                        dt = am.dtable();
                                        dt.Load(Reader);
                                        dtmerge.Merge(dt, true, MissingSchemaAction.Ignore);
                                        Reader.Close();
                                    }
                                }
                                fromdate = fromdate.AddDays(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error getting API TXN : " + ex.ToString());
            }
            return dtmerge;
        }
        public DataTable GetFUTXN(string category, int year, int month)
        {
            AdminModel am = new AdminModel();
            DataTable dtmerge = new DataTable();
            dtmerge = am.dtable();
            var str = string.Empty;
            try
            {
                //OdbcCommand cmd = new OdbcCommand();
                MySqlCommand cmd = new MySqlCommand();
                //using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                conn.connectdb("FileUploadB");
                using (MySqlConnection mycon = conn.getConnection())
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        var fromdate = new DateTime(year, month, 1);
                        var todate = fromdate.AddMonths(1).AddDays(-1);
                        while (fromdate <= todate)
                        {

                            if (category == "fuso")
                            {
                                str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                    " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                    " referenceno AS `refno` , `kptn` ,  `transdate` ,'' as  `claimeddate` , " +
                                    " principal AS  `amount` ,charge,  0 AS  `commission` ,  " +
                                    " 1 AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                    "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                    "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                    "  '' AS walletno, 'SENDOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                    "  `accountcode`, `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                    "  operatorid,'' as isremote,'' as remotebranch,'' as remoteoperatorid, othercharge,branchcode," +
                                    "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode, canceldetails," +
                                    "  cancelcharge,'' AS chargeto,'' as remotezonecode,currency,stationid,  '' AS maturitydate," +
                                    "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                    "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                    "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                    "  FROM `kppartners`.`sendout{0}`  a" +
                                    "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                    "  WHERE  YEAR(transdate)='{1}' and  lower(controlno) not rlike 'mlw'  ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "fupo")
                            {
                                str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                    " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                    " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                    " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                    " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                    "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                    "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                    "  '' AS walletno, 'PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                    "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                    "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                    "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                    "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                    "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                    "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                    "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                    "  FROM `kppartners`.`payout{0}`  a" +
                                    "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                    "  WHERE  YEAR(claimeddate)='{1}' and  lower(controlno) not rlike 'mlw'  ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "furfc" || category == "furts" || category == "fucso")
                            {
                                string cancelreason = (category == "furfc") ? "Request for Change" : (category == "furts") ? "Return to Sender" : "Cancel Sendout";
                                str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                    " receiveraddress AS address,  receivergender AS gender, " +
                                    " referenceno AS `refno` , `kptn` , `transdate` , '' AS `claimeddate` , " +
                                    " principal AS  `amount` , charge,  0 AS  `commission` ,  " +
                                    " '' AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                    "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                    "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,'' AS username," +
                                    "  '' AS walletno, '{2}' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                    "  `accountcode`,  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                    "  operatorid,'' AS isremote,'' AS remotebranch,'' AS remoteoperatorid,0 AS othercharge,branchcode," +
                                    "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,canceldetails," +
                                    "    cancelcharge,'' AS chargeto,'' AS remotezonecode,currency,stationid,  '' AS maturitydate," +
                                    "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                    "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor, otherdetails," +
                                    "  '' AS purpose,IF(sourceoffund IS NULL,'',sourceoffund) AS sourceoffund,'CORPORATE' AS servicetype  " +
                                    "  FROM `kppartners`.`sendout{0}`  a" +
                                    "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                    "  WHERE  YEAR(transdate)='{1}' AND lower(controlno) NOT RLIKE 'mlw' AND upper(cancelreason)='{2}'  ", fromdate.ToString("MMdd"), fromdate.Year, cancelreason.ToUpper());
                            }
                            else if (category == "fucpo")
                            {
                                str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                      " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                      " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                      " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                      " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                      "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                      "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                      "  '' AS walletno, 'CANCEL PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                      "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                      "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                      "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                      "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                      "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                      "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                      "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                      "  FROM `kppartners`.`payout{0}`  a" +
                                      "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                      "  WHERE  YEAR(claimeddate)='{1}' and  lower(controlno) not rlike 'mlw' and cancelleddate is not null " +
                                      "  and cancelleddate<>'0000-00-00 00:00:00' AND  cancelledtype='Payout' ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            cmd.CommandText = str;
                            cmd.CommandTimeout = 0;
                            //using (OdbcDataReader Reader = cmd.ExecuteReader())
                            using (MySqlDataReader Reader = cmd.ExecuteReader())
                            {
                                if (Reader.HasRows)
                                {
                                    DataTable dt = new DataTable();
                                    dt = am.dtable();
                                    dt.Load(Reader);
                                    dtmerge.Merge(dt, true, MissingSchemaAction.Ignore);
                                    Reader.Close();
                                }
                            }
                            fromdate = fromdate.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error getting FileUpload TXN : " + ex.ToString());
            }
            return dtmerge;
        }
        public DataTable GetWSCTXN(string category, int year, int month)
        {
            AdminModel am = new AdminModel();
            DataTable dtmerge = new DataTable();
            dtmerge = am.dtable();
            var str = string.Empty;
            try
            {
                //OdbcCommand cmd = new OdbcCommand();
                MySqlCommand cmd = new MySqlCommand();
                //using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                conn.connectdb("WSCB");
                using (MySqlConnection mycon = conn.getConnection())
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        var fromdate = new DateTime(year, month, 1);
                        var todate = fromdate.AddMonths(1).AddDays(-1);
                        while (fromdate <= todate)
                        {

                            if (category == "wscso")
                            {
                                str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                    " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                    " referenceno AS `refno` , `kptn` ,  `transdate` ,'' as  `claimeddate` , " +
                                    " principal AS  `amount` ,charge,  0 AS  `commission` ,  " +
                                    " 1 AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                    "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                    "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                    "  '' AS walletno, 'SENDOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                    "  `accountcode`, `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                    "  operatorid,'' as isremote,'' as remotebranch,'' as remoteoperatorid, othercharge,branchcode," +
                                    "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode, canceldetails," +
                                    "  cancelcharge,'' AS chargeto,'' as remotezonecode,currency,stationid,  '' AS maturitydate," +
                                    "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                    "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                    "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                    "  FROM `kppartners`.`sendout{0}`  a" +
                                    "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                    "  WHERE  YEAR(transdate)='{1}' and  lower(controlno) not rlike 'mlw'  ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "wscpo")
                            {
                                str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                    " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                    " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                    " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                    " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                    "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                    "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                    "  '' AS walletno, 'PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                    "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                    "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                    "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                    "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                    "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                    "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                    "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                    "  FROM `kppartners`.`payout{0}`  a" +
                                    "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                    "  WHERE  YEAR(claimeddate)='{1}' and  lower(controlno) not rlike 'mlw'  ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "wscrfc" || category == "wscrts" || category == "wsccso")
                            {
                                string cancelreason = (category == "wscrfc") ? "Request for Change" : (category == "wscrts") ? "Return to Sender" : "Cancel Sendout";
                                str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                    " receiveraddress AS address,  receivergender AS gender, " +
                                    " referenceno AS `refno` , `kptn` , `transdate` , '' AS `claimeddate` , " +
                                    " principal AS  `amount` , charge,  0 AS  `commission` ,  " +
                                    " '' AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                    "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                    "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,'' AS username," +
                                    "  '' AS walletno, '{2}' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                    "  `accountcode`,  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                    "  operatorid,'' AS isremote,'' AS remotebranch,'' AS remoteoperatorid,0 AS othercharge,branchcode," +
                                    "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,canceldetails," +
                                    "    cancelcharge,'' AS chargeto,'' AS remotezonecode,currency,stationid,  '' AS maturitydate," +
                                    "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                    "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor, otherdetails," +
                                    "  '' AS purpose,IF(sourceoffund IS NULL,'',sourceoffund) AS sourceoffund,'CORPORATE' AS servicetype  " +
                                    "  FROM `kppartners`.`sendout{0}`  a" +
                                    "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                    "  WHERE  YEAR(transdate)='{1}' AND lower(controlno) NOT RLIKE 'mlw' AND upper(cancelreason)='{2}'  ", fromdate.ToString("MMdd"), fromdate.Year, cancelreason.ToUpper());
                            }
                            else if (category == "wsccpo")
                            {
                                str = string.Format("  SELECT '' AS emailadd,receiverbirthdate AS birthdate,receivercontactno AS mobileno, " +
                                      " CONCAT(receiverstreet,' ',receiverprovince) AS address,  receivergender AS gender, " +
                                      " referenceno AS `refno` , `kptn` ,'' AS `transdate` , `claimeddate` , " +
                                      " principal AS  `amount` ,servicecharge AS charge,  0 AS  `commission` ,  " +
                                      " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` ,  " +
                                      "  receivername,b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, sendername, " +
                                      "  '' AS custid,  receiverlname AS lname,receiverfname AS fname, receivermname AS mname,operatorid AS username," +
                                      "  '' AS walletno, 'CANCEL PAYOUT' AS   `txntype`, `oldkptn` ,'' AS ptn,  '' AS `oldptn`," +
                                      "  `accountcode`,cancelledreason AS  `cancelreason`, `cancelleddate`,  controlno,irno,'' AS orno," +
                                      "  operatorid,isremote,remotebranch,remoteoperatorid,cancelledempcharge AS othercharge,branchcode," +
                                      "  cancelledbyoperatorid,cancelledbybranchcode, cancelledbyzonecode,reason AS canceldetails," +
                                      "  cancelledcustcharge AS cancelcharge,'' AS chargeto,remotezonecode,currency,stationid,  '' AS maturitydate," +
                                      "  '' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  '' AS weight,'' AS karat,'' AS carat," +
                                      "  '' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname,'' AS payor,'' AS otherdetails," +
                                      "  '' AS purpose,if(sourceoffund is null,'',sourceoffund) as sourceoffund,'CORPORATE' AS servicetype  " +
                                      "  FROM `kppartners`.`payout{0}`  a" +
                                      "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.accountcode" +
                                      "  WHERE  YEAR(claimeddate)='{1}' and  lower(controlno) not rlike 'mlw' and cancelleddate is not null " +
                                      "  and cancelleddate<>'0000-00-00 00:00:00' AND  cancelledtype='Payout' ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            cmd.CommandText = str;
                            cmd.CommandTimeout = 0;
                            //using (OdbcDataReader Reader = cmd.ExecuteReader())
                            using (MySqlDataReader Reader = cmd.ExecuteReader())
                            {
                                if (Reader.HasRows)
                                {
                                    DataTable dt = new DataTable();
                                    dt = am.dtable();
                                    dt.Load(Reader);
                                    dtmerge.Merge(dt, true, MissingSchemaAction.Ignore);
                                    Reader.Close();
                                }
                            }
                            fromdate = fromdate.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error getting WSC TXN : " + ex.ToString());
            }
            return dtmerge;
        }
        public DataTable GetBillspayTXN(string category, int year, int month)
        {
            AdminModel am = new AdminModel();
            DataTable dtmerge = new DataTable();
            dtmerge = am.dtable();
            var str = string.Empty;
            try
            {
                //OdbcCommand cmd = new OdbcCommand();
                MySqlCommand cmd = new MySqlCommand();
                //using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                conn.connectdb("BillspayB");
                using (MySqlConnection mycon = conn.getConnection())
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        var fromdate = new DateTime(year, month, 1);
                        var todate = fromdate.AddMonths(1).AddDays(-1);
                        while (fromdate <= todate)
                        {

                            if (category == "bpso")
                            {
                                str = string.Format(" SELECT '' AS emailadd,'' AS birthdate,payorcontactno AS mobileno, " +
                                 " payoraddress AS address,  '' AS gender,'' AS `refno` , kptnno AS  `kptn` ,  " +
                                 " `transdate` , '0000-00-00 00:00:00' AS `claimeddate` ,amountpaid AS  `amount` , customercharge,  0 AS  `commission` , " +
                                 " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` , '' AS   receivername, " +
                                 " b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, CONCAT(accountfname,' ',accountmname,' ',accountlname) AS sendername, " +
                                 " '' AS custid,  payorlname AS lname, payorfname AS fname, payormname AS mname,operatorid AS username,'' AS walletno, 'SENDOUT' AS   `txntype`, " +
                                 "  oldkptnno AS  `oldkptn` ,'' AS ptn,  '' AS `oldptn`,companyid AS  `accountcode`,   `cancelreason`, `cancelleddate`,  " +
                                 "  controlno,irno,'' AS orno,operatorid,'' AS isremote,'' AS remotebranch,remoteoperatorid,0 AS othercharge,branchcode," +
                                 "  cancelledbyoperatorid,cancelledbybranchcode,  cancelledbyzonecode, canceldetails, cancelcharge, '' AS chargeto," +
                                 "  remotezonecode,currency,stationid,'' AS maturitydate,'' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  " +
                                 "  '' AS weight,'' AS karat, '' AS carat,'' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname," +
                                 "   CONCAT(payorfname,' ',payormname,' ',payorlname) AS payor,accountno AS otherdetails,'' AS purpose,'' AS sourceoffund,'BILLSPAY' AS servicetype  " +
                                 "  FROM `kpbillspayment`.`sendout{0}`  a " +
                                 "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.companyid " +
                                 "  WHERE LOWER(kptnno) RLIKE 'bpp'   AND YEAR(transdate)='{1}'  ", fromdate.ToString("MMdd"), fromdate.Year);
                            }
                            else if (category == "bprfc" || category == "bpcso")
                            {
                                string cancelreason = (category == "bprfc") ? "Change Details" : "Cancel";
                                str = string.Format(" SELECT '' AS emailadd,'' AS birthdate,payorcontactno AS mobileno, " +
                              " payoraddress AS address,  '' AS gender,'' AS `refno` , kptnno AS  `kptn` ,  " +
                              " `transdate` , '0000-00-00 00:00:00' AS `claimeddate` ,amountpaid AS  `amount` , customercharge,  0 AS  `commission` , " +
                              " zonecode AS  `zcode`,'' AS `zone` ,  '' AS  `senderbranch` , '' AS  `receiverbranch` , '' AS   receivername, " +
                              " b.accountname AS `partnername` ,'0000-00-00 00:00:00' AS  `datemodified`, CONCAT(accountfname,' ',accountmname,' ',accountlname) AS sendername, " +
                              " '' AS custid,  payorlname AS lname, payorfname AS fname, payormname AS mname,operatorid AS username,'' AS walletno, '{2}' AS   `txntype`, " +
                              "  oldkptnno AS  `oldkptn` ,'' AS ptn,  '' AS `oldptn`,companyid AS  `accountcode`,   `cancelreason`, `cancelleddate`,  " +
                              "  controlno,irno,'' AS orno,operatorid,'' AS isremote,'' AS remotebranch,remoteoperatorid,0 AS othercharge,branchcode," +
                              "  cancelledbyoperatorid,cancelledbybranchcode,  cancelledbyzonecode, canceldetails, cancelcharge, '' AS chargeto," +
                              "  remotezonecode,currency,stationid,'' AS maturitydate,'' AS expirydate,0 AS advanceinterest,'' AS itemdesc,0 AS quantity,  " +
                              "  '' AS weight,'' AS karat, '' AS carat,'' AS appraiser,'' AS operator,0 AS discount,'' AS itemcode,  b.accountname AS accountname," +
                              "   CONCAT(payorfname,' ',payormname,' ',payorlname) AS payor,'' AS otherdetails,'' AS purpose,'' AS sourceoffund,'BILLSPAY' AS servicetype  " +
                              "  FROM `kpbillspayment`.`sendout{0}`  a " +
                              "  INNER JOIN kpadminpartners.accountlist b ON b.accountid=a.companyid " +
                              "  WHERE LOWER(kptnno) RLIKE 'bpp' and upper(cancelreason)='{2}'  AND YEAR(transdate)='{1}'  ", fromdate.ToString("MMdd"), fromdate.Year, cancelreason.ToUpper());
                            }
                            cmd.CommandText = str;
                            cmd.CommandTimeout = 0;
                            //using (OdbcDataReader Reader = cmd.ExecuteReader())
                            using (MySqlDataReader Reader = cmd.ExecuteReader())
                            {
                                if (Reader.HasRows)
                                {
                                    DataTable dt = new DataTable();
                                    dt = am.dtable();
                                    dt.Load(Reader);
                                    dtmerge.Merge(dt, true, MissingSchemaAction.Ignore);
                                    Reader.Close();
                                }
                            }
                            fromdate = fromdate.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error getting Billspay TXN : " + ex.ToString());
            }
            return dtmerge;
        }
        public DataTable GetQCLTXN(string category, int year, int month)
        {
            AdminModel am = new AdminModel();
            DataTable dtmerge = new DataTable();
            dtmerge = am.dtable();
            var str = string.Empty;
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        var fromdate = new DateTime(year, month, 1);
                        var todate = fromdate.AddMonths(1).AddDays(-1);
                        while (fromdate <= todate)
                        {
                            var branchlist = admin.branch();
                            foreach (var list in branchlist)
                            {
                                var bcode = list.Value;
                                var zcode = list.Text;
                                //var bcode = "004";
                                //var zcode = 1;
                                if (category == "lukat")
                                {
                                    str = string.Format(" select '' as emailadd,'' as birthdate,custtelno as mobileno,concat(custadd,' ',custcity) as address, " +
                                  " custgender as gender,'' as `refno` , '' as  `kptn` ,  `transdate` , '0000-00-00 00:00:00' as `claimeddate` ,ptnprincipal as  `amount` ,sc_value as  charge, " +
                                  " 0 as  `commission` , '{3}' as  `zcode`,'' as `zone` ,  reqbranchname as  `senderbranch` , '' as  `receiverbranch` , " +
                                  " '' as receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, '' as sendername, custid, " +
                                  " custlastname as lname,custfirstname as fname, custmiddleinitial as mname,'' as username,'' as walletno, transtype as   `txntype`, " +
                                  " '' as  `oldkptn` , ptn,  prev_ptn as `oldptn`,'' as  `accountcode`,  `cancelreason`, canceldate as `cancelleddate`, " +
                                  " '' as controlno,'' as irno,ornumber as orno,res_id as operatorid,'' as isremote,'' as remotebranch,'' as remoteoperatorid,0 as othercharge,bccode as branchcode, " +
                                  " cancelres_id as cancelledbyoperatorid,'' as cancelledbybranchcode,  '' as cancelledbyzonecode, '' as canceldetails, " +
                                  " 0 as cancelcharge, '' as chargeto,'' as remotezonecode,'' as currency,'' as stationid " +
                                  " maturitydate,expirydate,ai_value as advanceinterest,'' as itemdesc,0 as quantity, " +
                                  " '' as weight,'' as karat,'' as carat,appraiser,employee as operatorname,0 as discount,'' as itemcode, " +
                                  " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'QCL' as servicetype " +
                                  " from rems_{0}.`tbl_pt_tran`  WHERE (lower(transtype)!='prenda' and lower(transtype)!='renew' " +
                                  " and lower(transtype)!='reappraise')  and year(transdate)='{2}' and month(transdate)='{4}' ", bcode, category, year, zcode, month.ToString().PadLeft(2, '0'));
                                }
                                else
                                {
                                    str = string.Format(" select '' as emailadd,'' as birthdate,custtelno as mobileno,concat(custadd,' ',custcity) as address, " +
                                  " custgender as gender,'' as `refno` , '' as  `kptn` ,  `transdate` , '0000-00-00 00:00:00' as `claimeddate` ,ptnprincipal as  `amount` ,0 as  charge, " +
                                  " 0 as  `commission` , '{3}' as  `zcode`,'' as `zone` ,  reqbranchname as  `senderbranch` , '' as  `receiverbranch` , " +
                                  " '' as receivername,'' as `partnername` ,'0000-00-00 00:00:00' as  `datemodified`, '' as sendername, custid, " +
                                  " custlastname as lname,custfirstname as fname, custmiddleinitial as mname,'' as username,'' as walletno, transtype as   `txntype`, " +
                                  " '' as  `oldkptn` , ptn,  prev_ptn as `oldptn`,'' as  `accountcode`,  `cancelreason`, canceldate as `cancelleddate`, " +
                                  " '' as controlno,'' as irno,ornumber as orno,res_id as operatorid,'' as isremote,'' as remotebranch,'' as remoteoperatorid,0 as othercharge,bccode as branchcode, " +
                                  " cancelres_id as cancelledbyoperatorid,'' as cancelledbybranchcode,  '' as cancelledbyzonecode, '' as canceldetails, " +
                                  " 0 as cancelcharge, '' as chargeto,'' as remotezonecode,'' as currency,'' as stationid, " +
                                  " maturitydate,expirydate,ai_value as advanceinterest,'' as itemdesc,0 as quantity, " +
                                  " '' as weight,'' as karat,'' as carat,appraiser,employee as operatorname,0 as discount,'' as itemcode, " +
                                  " '' as accountname,'' as payor,'' as otherdetails,'' as purpose,'' as sourceoffund,'QCL' as servicetype " +
                                  " from rems_{0}.`tbl_pt_tran`  WHERE lower(transtype)='{1}'   and year(transdate)='{2}'  and month(transdate)='{4}' ", bcode, category, year, zcode, month.ToString().PadLeft(2, '0'));
                                }
                                cmd.CommandText = str;
                                cmd.CommandTimeout = 0;
                                try
                                {
                                    using (OdbcDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = am.dtable();
                                            dt.Load(Reader);
                                            dtmerge.Merge(dt);
                                            Reader.Close();
                                        }
                                    }
                                }
                                catch { }
                            }
                            fromdate = fromdate.AddDays(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error getting QCL TXN : " + ex.ToString());
            }
            return dtmerge;
        }
       
        public ActionResult insertcusttxnhistory(int month, int year, string category)
        {
            try
            {
                var iscorp = false;
                var isapi = false;
                OdbcCommand cmd = new OdbcCommand();
                DataTable dt = new DataTable();

                using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        #region create table
                        for (int i = 2013; i <= DateTime.Now.Year; i++)
                        {
                            //cmd.CommandText = "drop table if exists  customerservicedb." + category.Trim().ToUpper() + i;
                            //cmd.ExecuteNonQuery();

                            cmd.CommandText = "create TABLE IF NOT EXISTS  customerservicedb." + category.Trim().ToUpper() + i + "(id int, emailadd string, birthdate string, mobileno string, " +
                                     " address string, gender string, refno string, kptn string, transdate string, claimeddate string, amount decimal(14,2), " +
                                     " charge decimal(14,2), commission decimal(14,2), zone string, senderbranch string, receiverbranch string, receivername string,  " +
                                     " partnername string, datemodified timestamp, sendername string, custid string, lname string, fname string, " +
                                     " mname string, username string, walletno string, txntype string,oldkptn string,ptn string,oldptn string,accountcode string, " +
                                     " cancelreason string,cancelleddate string,controlno string,irno string,orno string,operatorid string,  " +
                                     " isremote string,remotebranch string,remoteoperatorid string,othercharge decimal(14,2),branchcode string,cancelledbyoperatorid string, " +
                                     " cancelledbybranchcode string,cancelledbyzonecode string,canceldetails string, cancelcharge string,chargeto string,remotezonecode string," +
                                     " currency string,stationid string,maturitydate string,expirydate string,advanceinterest decimal(14,2),itemdesc string,quantity int, " +
                                     " weight string,karat string,carat string,appraiser string,operatorname string,discount decimal(14,2),itemcode string,  " +
                                     " accountname string,payor string,otherdetails string,mcno string,custname string,foreigncurrency string,exchangerate string, " +
                                     " foreignamount decimal(14,2),purpose string,sourceoffund string,servicetype string,cancelledoperator string,remoteoperator string,cancelledbranch string,branch string)" +
                                     " PARTITIONED BY (dateinserted timestamp) CLUSTERED BY(id) INTO 3 BUCKETS STORED AS ORC TBLPROPERTIES ('transactional'='true')";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion create table
                    }
                }

                if (category == "kpso" || category == "kppo" || category == "kprfc" || category == "kprts" || category == "kpcso" || category == "kpcpo") { dt = GetDomesticTXN(category, year, month); }
                else if (category == "globalso" || category == "globalpo" || category == "globalrfc" || category == "globalrts" || category == "globalcso" || category == "globalcpo") { dt = GetGlobalTXN(category, year, month); }
                else if (category == "walletso" || category == "walletpo" || category == "walletrfc" || category == "walletrts" || category == "walletcso" || category == "walletcpo" || category == "walletbp" || category == "walleteload" || category == "walletcorppo") { dt = GetWalletTXN(category, year, month); }
                else if (category == "expressso" || category == "expresspo" || category == "expressrfc" || category == "expressrts" || category == "expresscso" || category == "expresscpo" || category == "expressbp" || category == "expresseload" || category == "expresscorppo") { dt = GetExpressTXN(category, year, month); }
                else if (category == "apiso" || category == "apipo" || category == "apirfc" || category == "apirts" || category == "apicso" || category == "apicpo") { dt = GetAPITXN(category, year, month); iscorp = true; isapi = true; }
                else if (category == "fuso" || category == "fupo" || category == "furfc" || category == "furts" || category == "fucso" || category == "fucpo") { dt = GetFUTXN(category, year, month); iscorp = true; }
                else if (category == "wscso" || category == "wscpo" || category == "wscrfc" || category == "wscrts" || category == "wsccso" || category == "wsccpo") { dt = GetWSCTXN(category, year, month); iscorp = true; }
                else if (category == "bpso" || category == "bprfc" || category == "bprts" || category == "bpcso") { dt = GetBillspayTXN(category, year, month); }
                else if (category == "prenda" || category == "lukat" || category == "renew" || category == "reappraise") { dt = GetQCLTXN(category, year, month); }

                var listcusttxn = new List<CustTxn>();
                GetNameModel name = new GetNameModel();
                foreach (DataRow Reader in dt.Rows)
                {
                    var kptn = Reader["kptn"].ToString().Trim().ToUpper();
                    var ptn = Reader["ptn"].ToString().Trim().ToUpper();
                    var filtered = from a in listcusttxn where a.kptn.Trim().ToUpper() == kptn && a.ptn.Trim().ToUpper()==ptn select a;
                    var count = filtered.Count();
                    if (count == 0)
                    {
                        listcusttxn.Add(new CustTxn
                        {
                            emailadd = (Reader["emailadd"] == System.DBNull.Value) ? "" : Reader["emailadd"].ToString().Trim().ToUpper(),
                            birthdate = (Reader["birthdate"] == System.DBNull.Value) ? "" : Reader["birthdate"].ToString().Trim().ToUpper(),
                            mobileno = (Reader["mobileno"] == System.DBNull.Value) ? "" : Reader["mobileno"].ToString().Trim().ToUpper(),
                            address = (Reader["address"] == System.DBNull.Value) ? "" : Reader["address"].ToString().Trim().ToUpper(),
                            gender = (Reader["gender"] == System.DBNull.Value) ? "" : Reader["gender"].ToString().Trim().ToUpper(),
                            refno = (Reader["refno"] == System.DBNull.Value) ? "" : Reader["refno"].ToString().Trim().ToUpper(),
                            kptn = (Reader["kptn"] == System.DBNull.Value) ? "" : Reader["kptn"].ToString().Trim().ToUpper(),
                            transdate = (Reader["transdate"] == System.DBNull.Value) ? "" : Reader["transdate"].ToString().Trim(),
                            claimeddate = (Reader["claimeddate"] == System.DBNull.Value) ? "" : Reader["claimeddate"].ToString().Trim(),
                            amount = (Reader["amount"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["amount"]),
                            charge = (Reader["charge"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["charge"]),
                            commission = (Reader["commission"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["commission"]),
                            zone = (Reader["zcode"] == System.DBNull.Value) ? "" : Reader["zcode"].ToString(),
                            senderbranch = (Reader["senderbranch"] == System.DBNull.Value) ? "" : Reader["senderbranch"].ToString().Trim().ToUpper(),
                            receiverbranch = (Reader["receiverbranch"] == System.DBNull.Value) ? "" : Reader["receiverbranch"].ToString().Trim().ToUpper(),
                            receivername = (Reader["receivername"] == System.DBNull.Value) ? "" : Reader["receivername"].ToString().Trim().ToUpper(),
                            partnername = (Reader["partnername"] == System.DBNull.Value) ? "" : Reader["partnername"].ToString().Trim().ToUpper(),
                            datemodified = Reader["datemodified"].ToString().Trim().ToUpper(),
                            sendername = (Reader["sendername"] == System.DBNull.Value) ? "" : Reader["sendername"].ToString().Trim().ToUpper(),
                            custid = (Reader["custid"] == System.DBNull.Value) ? "" : Reader["custid"].ToString().Trim().ToUpper(),
                            lname = (Reader["lname"] == System.DBNull.Value) ? "" : Reader["lname"].ToString().Trim().ToUpper().Replace("Ñ", "N"),
                            fname = (Reader["fname"] == System.DBNull.Value) ? "" : Reader["fname"].ToString().Trim().ToUpper().Replace("Ñ", "N"),
                            mname = (Reader["mname"] == System.DBNull.Value) ? "" : Reader["mname"].ToString().Trim().ToUpper().Replace("Ñ", "N"),
                            username = (Reader["username"] == System.DBNull.Value) ? "" : Reader["username"].ToString().Trim().ToUpper(),
                            walletno = (Reader["walletno"] == System.DBNull.Value) ? "" : Reader["walletno"].ToString().Trim().ToUpper(),
                            txntype = Reader["txntype"].ToString().Trim().ToUpper(),
                            oldkptn = (Reader["oldkptn"] == System.DBNull.Value) ? "" : Reader["oldkptn"].ToString().Trim().ToUpper(),
                            ptn = (Reader["ptn"] == System.DBNull.Value) ? "" : Reader["ptn"].ToString().Trim().ToUpper(),
                            oldptn = (Reader["oldptn"] == System.DBNull.Value) ? "" : Reader["oldptn"].ToString().Trim().ToUpper(),
                            accountcode = (Reader["accountcode"] == System.DBNull.Value) ? "" : Reader["accountcode"].ToString().Trim().ToUpper(),
                            cancelreason = (Reader["cancelreason"] == System.DBNull.Value) ? "" : Reader["cancelreason"].ToString().Trim().ToUpper(),
                            cancelleddate = (Reader["cancelleddate"] == System.DBNull.Value) ? "" : Reader["cancelleddate"].ToString().Trim().ToUpper(),
                            controlno = (Reader["controlno"] == System.DBNull.Value) ? "" : Reader["controlno"].ToString().Trim().ToUpper(),
                            irno = (Reader["irno"] == System.DBNull.Value) ? "" : Reader["irno"].ToString().Trim().ToUpper(),
                            orno = (Reader["orno"] == System.DBNull.Value) ? "" : Reader["orno"].ToString().Trim().ToUpper(),
                            operatorid = (Reader["operatorid"] == System.DBNull.Value) ? "" : Reader["operatorid"].ToString().Trim().ToUpper(),
                            isremote = (Reader["isremote"] == System.DBNull.Value) ? "" : Reader["isremote"].ToString().Trim().ToUpper(),
                            remotebranch = (Reader["remotebranch"] == System.DBNull.Value) ? "" : Reader["remotebranch"].ToString().Trim().ToUpper(),
                            remoteoperatorid = (Reader["remoteoperatorid"] == System.DBNull.Value) ? "" : Reader["remoteoperatorid"].ToString().Trim().ToUpper(),
                            othercharge = (Reader["othercharge"] == System.DBNull.Value) ? "" : Reader["othercharge"].ToString().Trim().ToUpper(),
                            branchcode = (Reader["branchcode"] == System.DBNull.Value) ? "" : Reader["branchcode"].ToString().Trim().ToUpper(),
                            cancelledbyoperatorid = (Reader["cancelledbyoperatorid"] == System.DBNull.Value) ? "" : Reader["cancelledbyoperatorid"].ToString().Trim().ToUpper(),
                            cancelledbybranchcode = (Reader["cancelledbybranchcode"] == System.DBNull.Value) ? "" : Reader["cancelledbybranchcode"].ToString().Trim().ToUpper(),
                            cancelledbyzonecode = (Reader["cancelledbyzonecode"] == System.DBNull.Value) ? "" : Reader["cancelledbyzonecode"].ToString().Trim().ToUpper(),
                            canceldetails = (Reader["canceldetails"] == System.DBNull.Value) ? "" : Reader["canceldetails"].ToString().Trim().ToUpper(),
                            cancelcharge = (Reader["cancelcharge"] == System.DBNull.Value) ? "" : Reader["cancelcharge"].ToString().Trim().ToUpper(),
                            chargeto = (Reader["chargeto"] == System.DBNull.Value) ? "" : Reader["chargeto"].ToString().Trim().ToUpper(),
                            remotezonecode = (Reader["remotezonecode"] == System.DBNull.Value) ? "" : Reader["remotezonecode"].ToString().Trim().ToUpper(),
                            currency = (Reader["currency"] == System.DBNull.Value) ? "" : Reader["currency"].ToString().Trim().ToUpper(),
                            stationid = (Reader["stationid"] == System.DBNull.Value) ? "" : Reader["stationid"].ToString().Trim().ToUpper(),
                            dateinserted = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            zcode = (Reader["zcode"] == System.DBNull.Value) ? "" : Reader["zcode"].ToString().Trim().ToUpper(),
                            maturitydate = (Reader["maturitydate"] == System.DBNull.Value) ? "" : Reader["maturitydate"].ToString().Trim().ToUpper(),
                            expirydate = (Reader["expirydate"] == System.DBNull.Value) ? "" : Reader["expirydate"].ToString().Trim().ToUpper(),
                            advanceinterest = (Reader["advanceinterest"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["advanceinterest"]),
                            itemdesc = (Reader["itemdesc"] == System.DBNull.Value) ? "" : Reader["itemdesc"].ToString().Trim().ToUpper(),
                            quantity = (Reader["quantity"] == System.DBNull.Value) ? 0 : Convert.ToInt16(Reader["quantity"]),
                            weight = (Reader["weight"] == System.DBNull.Value) ? "" : Reader["weight"].ToString().Trim().ToUpper(),
                            karat = (Reader["karat"] == System.DBNull.Value) ? "" : Reader["karat"].ToString().Trim().ToUpper(),
                            carat = (Reader["carat"] == System.DBNull.Value) ? "" : Reader["carat"].ToString().Trim().ToUpper(),
                            appraiser = (Reader["appraiser"] == System.DBNull.Value) ? "" : Reader["appraiser"].ToString().Trim().ToUpper(),
                            discount = (Reader["discount"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["discount"]),
                            itemcode = (Reader["itemcode"] == System.DBNull.Value) ? "" : Reader["itemcode"].ToString().Trim().ToUpper(),
                            accountname = (Reader["accountname"] == System.DBNull.Value) ? "" : Reader["accountname"].ToString().Trim().ToUpper(),
                            payor = (Reader["payor"] == System.DBNull.Value) ? "" : Reader["payor"].ToString().Trim().ToUpper(),
                            otherdetails = (Reader["otherdetails"] == System.DBNull.Value) ? "" : Reader["otherdetails"].ToString().Trim().ToUpper(),
                            purpose = (Reader["purpose"] == System.DBNull.Value) ? "" : Reader["purpose"].ToString().Trim().ToUpper(),
                            sourceoffund = (Reader["sourceoffund"] == System.DBNull.Value) ? "" : Reader["sourceoffund"].ToString().Trim().ToUpper(),
                            servicetype = (Reader["servicetype"] == System.DBNull.Value) ? "" : Reader["servicetype"].ToString().Trim().ToUpper(),
                            mcno="",
                            custname = "",
                            foreigncurrency = "",
                            exchangerate = "",
                            foreignamount = "",
                        });
                    }
                }

                var success = false;
                var loop = 1;
                foreach (var list in listcusttxn)
                {
                    var operatorname = name.corpoperator(list.operatorid, 1, list.accountcode);
                    operatorname = name.operatorname(list.operatorid, 1);
                    var cancelledoperator = name.operatorname(list.cancelledbyoperatorid, 1);
                    if (isapi == true) { name.kycinfo(list.custid, list.receivername ,"",""); }
                    else { name.kycinfo(list.custid, list.fname, list.mname, list.lname); }
                    var remoteoperator = name.operatorname(list.remoteoperatorid , 1);
                    var custid = (name.custid == "" || name.custid == null || name.custid.ToLower() == "null") ? list.custid : name.custid.ToUpper();
                    var gender = (name.gender == "" || name.gender == null || name.gender.ToLower() == "null") ? list.gender : name.gender.ToUpper();
                    var birthdate = (name.birthdate == "" || name.birthdate == null || name.birthdate.ToLower() == "null") ? list.birthdate : name.birthdate.ToUpper();
                    var mobileno = (name.mobileno == "" || name.mobileno == null || name.mobileno.ToLower() == "null") ? list.mobileno : name.mobileno.ToUpper();
                    var emailadd = (name.emailadd == "" || name.emailadd == null || name.emailadd.ToLower() == "null") ? list.emailadd : name.emailadd.ToUpper();
                    var fname = (name.fname == "" || name.fname == null || name.fname.ToLower() == "null") ? list.fname : name.fname.ToUpper();
                    var mname = (name.mname == "" || name.mname == null || name.mname.ToLower() == "null") ? list.mname : name.mname.ToUpper();
                    var lname = (name.lname == "" || name.lname == null || name.lname.ToLower() == "null") ? list.lname : name.lname.ToUpper();


                    var username = name.operatorname(list.username,0);
                    username = (username=="" || username==null)?list.username:username;
                    name.walletinfo(username, list.walletno, list.custid, fname, mname, lname,iscorp);

                    fname = (name.fname == "" || name.fname == null || name.fname.ToLower() == "null") ? fname : name.fname.ToUpper().Replace("Ñ", "N");
                    mname = (name.mname == "" || name.mname == null || name.mname.ToLower() == "null") ? mname : name.mname.ToUpper().Replace("Ñ", "N");
                    lname = (name.lname == "" || name.lname == null || name.lname.ToLower() == "null") ? lname : name.lname.ToUpper().Replace("Ñ", "N");

                    custid = (name.custid == "" || name.custid == null || name.custid.ToLower() == "null") ? custid : name.custid;
                    var walletno = (name.walletno == "" || name.walletno == null || name.walletno.ToLower() == "null") ? list.walletno : "";
                    mobileno = (name.mobileno == "" || name.mobileno == null || name.mobileno.ToLower() == "null") ? mobileno : name.mobileno;
                    emailadd = (name.emailadd == "" || name.emailadd == null || name.emailadd.ToLower() == "null") ? emailadd : name.emailadd;
                    birthdate = (name.birthdate == "" || name.birthdate == null || name.birthdate.ToLower() == "null") ? birthdate : name.birthdate;
                    gender = (name.gender == "" || name.gender == null || name.gender.ToLower() == "null") ? gender : name.gender;
                    username = (name.username == "" || name.username == null || name.username.ToLower() == "null") ? username : name.username;
                    
                    if(name.iscorporatepo==false){
                        var exists = true;
                        using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                        {
                            mycon.Open();

                            using (cmd = mycon.CreateCommand())
                            {
                                cmd.CommandText = string.Format("select * from customerservicedb.{0}{1} where lower(kptn)='{2}' and lower(ptn)='{4}' and lower(txntype)='{3}' ", category.Trim().ToUpper(), year, list.kptn.ToString().ToLower(), list.txntype.ToString().ToLower(), list.ptn.ToLower());
                                using (OdbcDataReader Reader = cmd.ExecuteReader())
                                {
                                    if (!Reader.HasRows) { exists = false; Reader.Close(); }
                                }
                            }
                        }

                        if (exists == false)
                        {
                            var tdate = (list.transdate.ToString() == "0000-00-00 00:00:00" || list.transdate.ToString() == null || list.transdate.ToString() == "" || list.transdate.ToString() == "0/0/0000 0:00:00 AM" || list.transdate.ToString() == "0/0/0000 0:00:00 PM") ? list.transdate.ToString() : Convert.ToDateTime(list.transdate).ToString("yyyy-MM-dd HH:mm:ss").ToUpper();
                            var cdate = (list.claimeddate.ToString() == "0000-00-00 00:00:00" || list.claimeddate.ToString() == null || list.claimeddate.ToString() == "" || list.claimeddate.ToString() == "0/0/0000 0:00:00 AM" || list.claimeddate.ToString() == "0/0/0000 0:00:00 PM") ? list.claimeddate.ToString() : Convert.ToDateTime(list.claimeddate).ToString("yyyy-MM-dd HH:mm:ss").ToUpper();
                            var bdate = (birthdate == "NULL" || birthdate == "" || birthdate == "0/0/0000" || birthdate == "0000-00-00" || birthdate == "0/0/0000 0:00:00 AM" || birthdate == "00/00/0000") ? birthdate : Convert.ToDateTime(birthdate).ToString("yyyy-MM-dd").ToUpper();
                            var branch = name.bname(Convert.ToInt16(list.zcode), list.branchcode.ToString().Trim().ToUpper());
                            var cancelbranch = (list.cancelledbyzonecode == "") ? "" : name.bname(Convert.ToInt16(list.cancelledbyzonecode), list.cancelledbybranchcode);
                            var zone = name.zone(Convert.ToInt16(list.zcode));

                            using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                            {
                                mycon.Open();

                                using (cmd = mycon.CreateCommand())
                                {
                                    cmd.CommandText = string.Format("insert into table customerservicedb.{0}{29} partition (dateinserted= '{1}') " +
                                    " values('{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},{14}, " +
                                    " '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}', " +
                                    " '{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}', " +
                                    " '{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}', " +
                                    " '{58}','{59}','{60}','{61}','{62}','{63}','{64}','{65}','{66}','{67}','{68}','{69}','{70}','{71}', " +
                                    " '{72}','{73}','{74}','{75}','{76}','{77}','{78}','{79}','{80}' ) ",
                                    category.Trim().ToUpper(), Convert.ToDateTime(list.dateinserted).ToString("yyyy-MM-dd HH:mm:ss"), loop,
                                    ((emailadd == null || emailadd == "" || emailadd.ToLower() == "null") ? list.emailadd : emailadd), bdate,
                                    ((mobileno == null || mobileno == "" || mobileno.ToLower() == "null") ? list.mobileno : mobileno),
                                    list.address.ToString().ToUpper(),
                                    ((gender == null || gender == "" || gender.ToLower() == "null") ? list.gender : gender),
                                    list.refno.ToString().ToUpper(), list.kptn.ToString().ToUpper(),
                                    tdate, cdate, Convert.ToDecimal(list.amount), Convert.ToDecimal(list.charge), Convert.ToDecimal(list.commission),
                                    zone.ToString().ToUpper(), list.senderbranch.ToString().ToUpper(), list.receiverbranch.ToString().ToUpper(), list.receivername.ToString().ToUpper(),
                                    list.partnername.ToString().ToUpper(), list.datemodified.ToString().ToUpper(), list.sendername.ToString().ToUpper(),
                                    ((custid == null || custid == "" || custid.ToLower() == "null") ? list.custid : custid),
                                    ((lname == null || lname == "" || lname.ToLower() == "null") ? list.lname : lname),
                                    ((fname == null || fname == "" || fname.ToLower() == "null") ? list.fname : fname),
                                    ((mname == null || mname == "" || mname.ToLower() == "null") ? list.mname : mname),
                                    ((username == null || username == "" || username.ToLower() == "null") ? list.username : username),
                                    ((walletno == null || walletno == "" || walletno.ToLower() == "null") ? list.walletno : walletno),
                                    list.txntype.ToString().ToUpper(), year,
                                    list.oldkptn.ToString().ToUpper(), list.ptn.ToString().ToUpper(), list.oldptn.ToString().ToUpper(), list.accountcode.ToString().ToUpper(),
                                    list.cancelreason.ToString().ToUpper(), list.cancelleddate.ToString().ToUpper(), list.controlno.ToString().ToUpper(), list.irno.ToString().ToUpper(),
                                    list.orno.ToString().ToUpper(), list.operatorid.ToString().ToUpper(), list.isremote.ToString().ToUpper(), list.remotebranch.ToString().ToUpper(),
                                    list.remoteoperatorid.ToString().ToUpper(), list.othercharge.ToString(), list.branchcode.ToString().ToUpper(), list.cancelledbyoperatorid.ToString().ToUpper(),
                                    list.cancelledbybranchcode.ToString().ToUpper(), list.cancelledbyzonecode.ToString().ToUpper(), list.canceldetails.ToString().ToUpper(), list.cancelcharge.ToString().ToUpper(),
                                    list.chargeto.ToString().ToUpper(), list.remotezonecode.ToString().ToUpper(), list.currency.ToString().ToUpper(), list.stationid.ToString().ToUpper(),
                                    list.maturitydate.ToString().ToUpper(), list.expirydate.ToString().ToUpper(), list.advanceinterest.ToString().ToUpper(), list.itemdesc.ToString().ToUpper(),
                                    list.quantity.ToString().ToUpper(), list.weight.ToString().ToUpper(), list.karat.ToString().ToUpper(), list.carat.ToString().ToUpper(),
                                    list.appraiser.ToString().ToUpper(), operatorname.ToUpper(), list.discount.ToString().ToUpper(), list.itemcode.ToString().ToUpper(),
                                    list.accountname.ToString().ToUpper(), list.payor.ToString().ToUpper(), list.otherdetails.ToString().ToUpper(),
                                    list.mcno.ToString().ToUpper(),
                                    list.custname.ToString().ToUpper(), list.foreigncurrency.ToString().ToUpper(),
                                    list.exchangerate.ToString().ToUpper(),
                                    list.foreignamount.ToString().ToUpper(), list.purpose.ToString().ToUpper(),
                                    list.sourceoffund.ToString().ToUpper(), list.servicetype.ToString().ToUpper(),
                                    cancelledoperator.ToUpper(), remoteoperator.ToString(), cancelbranch.ToUpper(), branch.ToUpper());
                                    cmd.CommandTimeout = 0;
                                    cmd.ExecuteNonQuery();
                                    success = true;
                                    loop++;

                                }
                            }
                        }
                    }
                }
                if (success == true) { return Json("Success", JsonRequestBehavior.AllowGet); }
                else { return Json("Empty", JsonRequestBehavior.AllowGet); }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Admin Insert Customer TXN History: " + ex.ToString());
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult insertcusttxnsummary(int month, int year, string category)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand();
                var listtxncount = new List<TxnCount>();
                GetNameModel name = new GetNameModel();
                var str = string.Empty;
                using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                {
                    mycon.Open();

                    using (cmd = mycon.CreateCommand())
                    {
                        #region create table
                        for (int i = 2013; i <= DateTime.Now.Year ; i++)
                        {
                            cmd.CommandText = "drop table if exists customerservicedb." + category.Trim().ToUpper() + i + "COUNT";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "create TABLE IF NOT EXISTS  customerservicedb." + category.Trim().ToUpper() + i + "COUNT" +
                                     "( id int, emailadd string, birthdate string, mobileno string, " +
                                     " address string, gender string, totalamount decimal(14,2),  " +
                                     " totalcharge decimal(14,2), totalcommission decimal(14,2), " +
                                     " datemodified timestamp,  custid string, lname string, fname string, " +
                                     " mname string, username string, walletno string, txntype string,totalcount int, " +
                                     " street string,provincecity string,country string,  zipcode string,branchid string,idtype string," +
                                     " idno string,expirydate string,dtcreated string,dtmodified string,createdby string,modifiedby string,  phoneno string," +
                                     " cardno string,placeofbirth string,natureofwork string,permanentaddress string,nationality string," +
                                     " companyoremployer string,businessorprofession string,govtidtype string,govtidno string, " +
                                     " branchcreated string,branchmodified string,mlcardno string,servicetype string ) " +
                                     " PARTITIONED BY (dateinserted timestamp) CLUSTERED BY(id) INTO 3 BUCKETS STORED AS ORC TBLPROPERTIES ('transactional'='true')";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion create table

                        str = string.Format(" select custid,fname,lname,mname,address, " +
                                            " emailadd,birthdate,gender,walletno,username,mobileno,`amount` , " +
                                            " `charge` ,`commission` , `txntype`,servicetype  " +
                                            " from `customerservicedb`.{0}{1}   ", category.Trim().ToUpper(), year);
                        cmd.CommandText = str;
                        cmd.CommandTimeout = 0;
                        using (OdbcDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                while (Reader.Read())
                                {
                                    var custid = Reader["custid"].ToString().Trim().ToUpper();
                                    var walletno = Reader["walletno"].ToString().Trim().ToUpper();
                                    var username = Reader["username"].ToString().Trim().ToUpper();
                                    var fname = Reader["fname"].ToString().Trim().ToUpper();
                                    var lname = Reader["lname"].ToString().Trim().ToUpper();
                                    var mname = Reader["mname"].ToString().Trim().ToUpper();

                                    var filtered = from a in listtxncount
                                                   where  a.username.Trim().ToUpper() == username.ToString().Trim().ToUpper()
                                                   && a.walletno.Trim().ToUpper() == walletno.ToString().Trim().ToUpper()
                                                   && a.custid.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                                   && a.fname.Trim().ToUpper() == fname.ToString().Trim().ToUpper() 
                                                   && a.lname.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                                   && a.mname.Trim().ToUpper() == mname.ToString().Trim().ToUpper() 
                                                   select a;

                                    var count = filtered.Count();
                                    if (count == 0)
                                    {
                                        listtxncount.Add(new TxnCount
                                        {
                                            custid = (Reader["custid"] == System.DBNull.Value) ? "" : Reader["custid"].ToString().Trim().ToUpper(),
                                            fname = (Reader["fname"] == System.DBNull.Value) ? "" : Reader["fname"].ToString().Trim().ToUpper(),
                                            lname = (Reader["lname"] == System.DBNull.Value) ? "" : Reader["lname"].ToString().Trim().ToUpper(),
                                            mname = (Reader["mname"] == System.DBNull.Value) ? "" : Reader["mname"].ToString().Trim().ToUpper(),
                                            username = (Reader["username"] == System.DBNull.Value) ? "" : Reader["username"].ToString().Trim().ToUpper(),
                                            emailadd = (Reader["emailadd"] == System.DBNull.Value) ? "" : Reader["emailadd"].ToString().Trim().ToUpper(),
                                            birthdate = (Reader["birthdate"] == System.DBNull.Value) ? "" : Reader["birthdate"].ToString().Trim().ToUpper(),
                                            mobileno = (Reader["mobileno"] == System.DBNull.Value) ? "" : Reader["mobileno"].ToString().Trim().ToUpper(),
                                            address = (Reader["address"] == System.DBNull.Value) ? "" : Reader["address"].ToString().Trim().ToUpper(),
                                            gender = (Reader["gender"] == System.DBNull.Value) ? "" : Reader["gender"].ToString().Trim().ToUpper(),
                                            walletno = (Reader["walletno"] == System.DBNull.Value) ? "" : Reader["walletno"].ToString().Trim().ToUpper(),
                                            totalamount = (Reader["amount"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["amount"]),
                                            totalcount = 1,
                                            totalcharge = (Reader["charge"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["charge"]),
                                            totalcommission = (Reader["commission"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["commission"]),
                                            txntype = Reader["txntype"].ToString().Trim().ToUpper(),
                                            categorytype = Reader["servicetype"].ToString().Trim().ToUpper(),
                                        });
                                    }
                                    else {
                                        var obj = listtxncount.FirstOrDefault(a => a.custid.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                            && a.lname.Trim().ToUpper() == lname.Trim().ToUpper()
                                            && a.fname.Trim().ToUpper() == fname.Trim().ToUpper()
                                            && a.mname.Trim().ToUpper() == mname.Trim().ToUpper()
                                            && a.walletno.Trim().ToUpper() == walletno.Trim().ToUpper()
                                            && a.username.Trim().ToUpper() == username.Trim().ToUpper());
                                        obj.totalamount = Convert.ToDecimal(obj.totalamount) + ((Reader["amount"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["amount"]));
                                        obj.totalcharge = Convert.ToDecimal(obj.totalcharge) + ((Reader["charge"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["charge"]));
                                        obj.totalcommission = Convert.ToDecimal(obj.totalcommission) + ((Reader["commission"] == System.DBNull.Value) ? 0 : Convert.ToDecimal(Reader["commission"]));
                                        obj.totalcount = Convert.ToInt64(obj.totalcount) + 1;
                                    }
                                }
                                Reader.Close();
                            }
                        }
                    }
                }


                var success = false;
                var loop = 1;
                foreach (var list in listtxncount)
                {
                    var exists = true;

                    name.kycinfo(list.custid, list.fname, list.mname, list.lname);
                    var custid = (name.custid == "" || name.custid == null || name.custid.ToLower() == "null") ? list.custid : name.custid.ToUpper();
                    var gender = (name.gender == "" || name.gender == null || name.gender.ToLower() == "null") ? list.gender : name.gender.ToUpper();
                    var birthdate = (name.birthdate == "" || name.birthdate == null || name.birthdate.ToLower() == "null") ? list.birthdate : name.birthdate.ToUpper();
                    var mobileno = (name.mobileno == "" || name.mobileno == null || name.mobileno.ToLower() == "null") ? list.mobileno : name.mobileno.ToUpper();
                    var emailadd = (name.emailadd == "" || name.emailadd == null || name.emailadd.ToLower() == "null") ? list.emailadd : name.emailadd.ToUpper();
                    var fname = (name.fname == "" || name.fname == null || name.fname.ToLower() == "null") ? list.fname : name.fname.ToUpper();
                    var mname = (name.mname == "" || name.mname == null || name.mname.ToLower() == "null") ? list.mname : name.mname.ToUpper();
                    var lname = (name.lname == "" || name.lname == null || name.lname.ToLower() == "null") ? list.lname : name.lname.ToUpper();

                    var street = (name.street == "" || name.street == null || name.street.ToLower() == "null") ? "" : name.street.ToUpper();
                    var provincecity = (name.provincecity == "" || name.provincecity == null || name.provincecity.ToLower() == "null") ? "" : name.provincecity.ToUpper();
                    var country = (name.country == "" || name.country == null || name.country.ToLower() == "null") ? "" : name.country.ToUpper();
                    var zipcode = (name.zipcode == "" || name.zipcode == null || name.zipcode.ToLower() == "null") ? "" : name.zipcode.ToUpper();
                    var branchid = (name.branchid == "" || name.branchid == null || name.branchid.ToLower() == "null") ? "" : name.branchid.ToUpper();
                    var idtype = (name.idtype == "" || name.idtype == null || name.idtype.ToLower() == "null") ? "" : name.idtype.ToUpper();
                    var idno = (name.idno == "" || name.idno == null || name.idno.ToLower() == "null") ? "" : name.idno.ToUpper();
                    var expirydate = (name.expirydate == "" || name.expirydate == null || name.expirydate.ToLower() == "null") ? "" : name.expirydate.ToUpper();
                    var dtcreated = (name.dtcreated == "" || name.dtcreated == null || name.dtcreated.ToLower() == "null") ? "" : name.dtcreated.ToUpper();
                    var dtmodified = (name.dtmodified == "" || name.dtmodified == null || name.dtmodified.ToLower() == "null") ? "" : name.dtmodified.ToUpper();
                    var createdby = (name.createdby == "" || name.createdby == null || name.createdby.ToLower() == "null") ? "" : name.createdby.ToUpper();
                    createdby = name.operatorname(createdby, 1);
                    var modifiedby = (name.modifiedby == "" || name.modifiedby == null || name.modifiedby.ToLower() == "null") ? "" : name.modifiedby.ToUpper();
                    modifiedby = name.operatorname(modifiedby, 1);
                    var phoneno = (name.phoneno == "" || name.phoneno == null || name.phoneno.ToLower() == "null") ? "" : name.phoneno.ToUpper();
                    var cardno = (name.cardno == "" || name.cardno == null || name.cardno.ToLower() == "null") ? "" : name.cardno.ToUpper();
                    var placeofbirth = (name.placeofbirth == "" || name.placeofbirth == null || name.placeofbirth.ToLower() == "null") ? "" : name.placeofbirth.ToUpper();
                    var natureofwork = (name.natureofwork == "" || name.natureofwork == null || name.natureofwork.ToLower() == "null") ? "" : name.natureofwork.ToUpper();
                    var permanentaddress = (name.permanentaddress == "" || name.permanentaddress == null || name.permanentaddress.ToLower() == "null") ? "" : name.permanentaddress.ToUpper();
                    var nationality = (name.nationality == "" || name.nationality == null || name.nationality.ToLower() == "null") ? "" : name.nationality.ToUpper();
                    var companyoremployer = (name.companyoremployer == "" || name.companyoremployer == null || name.companyoremployer.ToLower() == "null") ? "" : name.companyoremployer.ToUpper();
                    var businessorprofession = (name.businessorprofession == "" || name.businessorprofession == null || name.businessorprofession.ToLower() == "null") ? "" : name.businessorprofession.ToUpper();
                    var govtidtype = (name.govtidtype == "" || name.govtidtype == null || name.govtidtype.ToLower() == "null") ? "" : name.govtidtype.ToUpper();
                    var govtidno = (name.govtidno == "" || name.govtidno == null || name.govtidno.ToLower() == "null") ? "" : name.govtidno.ToUpper();
                    var branchcreated = (name.branchcreated == "" || name.branchcreated == null || name.branchcreated.ToLower() == "null") ? "" : name.branchcreated.ToUpper();
                    var branchmodified = (name.branchmodified == "" || name.branchmodified == null || name.branchmodified.ToLower() == "null") ? "" : name.branchmodified.ToUpper();
                    var mlcardno = (name.mlcardno == "" || name.mlcardno == null || name.mlcardno.ToLower() == "null") ? "" : name.mlcardno.ToUpper();

                    var username = name.operatorname(list.username,0);
                    name.walletinfo(username, list.walletno, custid, fname, mname, lname, false);
                    custid = (name.custid == "" || name.custid == null || name.custid.ToLower() == "null") ? custid : name.custid;
                    var walletno = (name.walletno == "" || name.walletno == null || name.walletno.ToLower() == "null") ? list.walletno : name.walletno;
                    mobileno = (name.mobileno == "" || name.mobileno == null || name.mobileno.ToLower() == "null") ? mobileno : name.mobileno;
                    emailadd = (name.emailadd == "" || name.emailadd == null || name.emailadd.ToLower() == "null") ? emailadd : name.emailadd;
                    birthdate = (name.birthdate == "" || name.birthdate == null || name.birthdate.ToLower() == "null") ? birthdate : name.birthdate;
                    gender = (name.gender == "" || name.gender == null || name.gender.ToLower() == "null") ? gender : name.gender;
                    username = (name.username == "" || name.username == null || name.username.ToLower() == "null") ? username : name.username;

                    street = (name.street == "" || name.street == null || name.street.ToLower() == "null") ? street : name.street.ToUpper();
                    provincecity = (name.provincecity == "" || name.provincecity == null || name.provincecity.ToLower() == "null") ? provincecity : name.provincecity.ToUpper();
                    country = (name.country == "" || name.country == null || name.country.ToLower() == "null") ? country : name.country.ToUpper();
                    zipcode = (name.zipcode == "" || name.zipcode == null || name.zipcode.ToLower() == "null") ? zipcode : name.zipcode.ToUpper();
                    branchid = (name.branchid == "" || name.branchid == null || name.branchid.ToLower() == "null") ? branchid : name.branchid.ToUpper();
                    dtcreated = (name.dtcreated == "" || name.dtcreated == null || name.dtcreated.ToLower() == "null") ? dtcreated : name.dtcreated.ToUpper();
                    dtmodified = (name.dtmodified == "" || name.dtmodified == null || name.dtmodified.ToLower() == "null") ? dtmodified : name.dtmodified.ToUpper();
                    natureofwork = (name.natureofwork == "" || name.natureofwork == null || name.natureofwork.ToLower() == "null") ? natureofwork : name.natureofwork.ToUpper();
                    permanentaddress = (name.permanentaddress == "" || name.permanentaddress == null || name.permanentaddress.ToLower() == "null") ? permanentaddress : name.permanentaddress.ToUpper();
                    nationality = (name.nationality == "" || name.nationality == null || name.nationality.ToLower() == "null") ? nationality : name.nationality.ToUpper();

                    fname = (name.fname == "" || name.fname == null || name.fname.ToLower() == "null") ? fname : name.fname.ToUpper().Replace("Ñ", "N");
                    mname = (name.mname == "" || name.mname == null || name.mname.ToLower() == "null") ? mname : name.mname.ToUpper().Replace("Ñ", "N");
                    lname = (name.lname == "" || name.lname == null || name.lname.ToLower() == "null") ? lname : name.lname.ToUpper().Replace("Ñ", "N");

                    using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            var usernamecondition = string.Format(((username == "" || username == null || username.Trim().ToLower() == "null") ? "" : "lower(username)='{0}' and"), username.Trim().ToLower());
                            var custidcondition = string.Format(((custid == "" || custid == null || custid.Trim().ToLower() == "null") ? "" : "lower(custid)='{0}' and"), custid.Trim().ToLower());
                            //var walletnocondition = string.Format(((walletno == "" || walletno == null || walletno.Trim().ToLower() == "null") ? "" : "lower(walletno)='{0}' and"), walletno.Trim().ToLower());
                            var lnamecondition = string.Format(((lname == "" || lname == null || lname.Trim().ToLower() == "null") ? "" : "lower(lname)='{0}' and"), lname.Trim().ToLower());
                            var fnamecondition = string.Format(((fname == "" || fname == null || fname.Trim().ToLower() == "null") ? "" : "lower(fname)='{0}' and"), fname.Trim().ToLower());
                            fnamecondition = string.Format(((mname == "" || mname == null || mname.Trim().ToLower() == "null") ? "lower(fname)='{0}' " : fnamecondition), fname.Trim().ToLower());
                            var mnamecondition = string.Format(((mname == "" || mname == null || mname.Trim().ToLower() == "null") ? "" : "lower(mname)='{0}' "), mname.Trim().ToLower());
                            cmd.CommandText = string.Format("select * from customerservicedb.{0}{1}COUNT " +
                                " where {2} {3} {4} {5} {6} ",
                                category.Trim().ToUpper(), year, usernamecondition, custidcondition,
                                lnamecondition, fnamecondition, mnamecondition);
                            using (OdbcDataReader Reader = cmd.ExecuteReader())
                            {
                                if (!Reader.HasRows) { exists = false; Reader.Close(); }
                            }
                        }
                    }
                    if (exists == false)
                    {
                        using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                var q = string.Format("insert into table customerservicedb.{0}{1}COUNT partition (dateinserted= '{2}') " +
                              " values({3},'{4}','{5}','{6}','{7}','{8}',{9},{10},{11},'0000-00-00 00:00:00','{12}','{13}','{14}', " +
                              " '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}', " +
                              " '{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}') ",
                              category.Trim().ToUpper(), year, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), loop,
                              ((emailadd == null || emailadd == "" || emailadd.ToLower() == "null") ? list.emailadd : emailadd),
                              ((birthdate == null || birthdate == "" || birthdate.ToLower() == "null") ? ((list.birthdate == null || list.birthdate == "" || list.birthdate.ToLower() == "null") ? "" : list.birthdate) : birthdate),
                              ((mobileno == null || mobileno == "" || mobileno.ToLower() == "null") ? list.mobileno : mobileno),
                              ((permanentaddress == null || permanentaddress == "" || permanentaddress.ToLower() == "null") ? list.address : permanentaddress),
                              ((gender == null || gender == "" || gender.ToLower() == "null") ? list.gender : gender),
                              list.totalamount, list.totalcharge, list.totalcommission,
                              ((custid == null || custid == "" || custid.ToLower() == "null") ? list.custid : custid),
                              ((lname == null || lname == "" || lname.ToLower() == "null") ? list.lname : lname),
                              ((fname == null || fname == "" || fname.ToLower() == "null") ? list.fname : fname),
                              ((mname == null || mname == "" || mname.ToLower() == "null") ? list.mname : mname), username,
                              ((walletno == null || walletno == "" || walletno.ToLower() == "null") ? list.walletno : walletno),
                              list.txntype, list.totalcount,
                              street, provincecity, country, zipcode, branchid, idtype,
                              idno, expirydate, dtcreated, dtmodified, createdby, modifiedby,
                              phoneno, cardno, placeofbirth, natureofwork, permanentaddress,
                              nationality, companyoremployer, businessorprofession, govtidtype, govtidno,
                              branchcreated, branchmodified, mlcardno,list.categorytype
                              );
                                cmd.CommandText = q;
                                cmd.CommandTimeout = 0;
                                cmd.ExecuteNonQuery();
                                success = true;
                                loop++;
                            }
                        }
                    }
                    else
                    {
                        using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                var q = string.Format(" update customerservicedb.{0}{1}COUNT " +
                            " set totalcount={2},totalamount={3},totalcharge={4},totalcommission={5},datemodified='{6}', " +
                            " govtidtype='{13}',govtidno='{14}',branchcreated='{15}',branchmodified='{16}',mlcardno='{17}', " +
                            " createdby='{18}',modifiedby='{19}',walletno='{9}',username='{7}'  " +
                            " where  lower(custid)='{8}'  and lower(lname)='{10}' and lower(fname)='{11}' and lower(mname)='{12}' ",
                           category.Trim().ToUpper(), year, list.totalcount, list.totalamount, list.totalcharge, list.totalcommission,
                           DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), username.ToUpper(),
                           ((custid == null || custid == "" || custid.ToLower() == "null") ? list.custid.ToLower() : custid.ToLower()),
                           ((walletno == null || walletno == "" || walletno.ToLower() == "null") ? list.walletno : walletno),
                           ((lname == null || lname == "" || lname.ToLower() == "null") ? list.lname.ToLower() : lname.ToLower()),
                           ((fname == null || fname == "" || fname.ToLower() == "null") ? list.fname.ToLower() : fname.ToLower()),
                           ((mname == null || mname == "" || mname.ToLower() == "null") ? list.mname.ToLower() : mname.ToLower()),
                           govtidtype, govtidno,  branchcreated, branchmodified, mlcardno,createdby ,modifiedby  );
                                cmd.CommandText = q;
                                cmd.CommandTimeout = 0;
                                cmd.ExecuteNonQuery();
                                success = true;
                            }
                        }
                    }
                }
                if (success == true) { return Json("Success", JsonRequestBehavior.AllowGet); }
                else { return Json("Empty", JsonRequestBehavior.AllowGet); }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Admin Insert Customer TXN Summary: " + ex.ToString());
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
