using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CustomerService.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CustomerService.Controllers
{
    public class CustomerServiceController : Controller
    {
        //
        // GET: /CustomerService/
        CustomerServiceModel cust = new CustomerServiceModel();
        Connection conn = new Connection();

        public string userfullname = (string)System.Web.HttpContext.Current.Session["userfullname"];
        public string user = (string)System.Web.HttpContext.Current.Session["user"];
        public ActionResult Index()
        {
            return View(cust);
        }

       
        [HttpPost]
        public ActionResult SearchCustomers(string custname)
        {

            OdbcCommand cmd = new OdbcCommand();
            var list = new List<CustomerServiceModel>();
            try
            {
                using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        for (int yrloop = 2013; yrloop <= DateTime.Now.Year; yrloop++)
                        {
                            for (int i = 1; i <= 63; i++)
                            {
                                CustomerSummary custsum = new CustomerSummary();
                                var txntype = custsum.GetTxnType(i);
                                
                                //cmd.CommandText = string.Format(" SELECT *  " +
                                //" FROM  customerservicedb.{0}{1}COUNT ", txntype.ToUpper(), yrloop);

                                if (custname == "") {
                                    cmd.CommandText = string.Format(" SELECT custid,walletno,username,fname,mname,lname,totalcount,emailadd,birthdate, " +
                                   " mobileno, address, gender,street,provincecity,country,  " +
                                   " zipcode,branchid,idtype,idno,expirydate,dtcreated,dtmodified,createdby,modifiedby,  " +
                                   " phoneno,cardno,placeofbirth,natureofwork,permanentaddress,nationality,  companyoremployer,businessorprofession  " +
                                    " ,govtidtype,govtidno " +
                                    " FROM  customerservicedb.{0}{1}COUNT " , txntype.ToUpper(), yrloop);
                                }
                                else {
                                    cmd.CommandText = string.Format(" SELECT custid,walletno,username,fname,mname,lname,totalcount,emailadd,birthdate, " +
                                   " mobileno, address, gender,street,provincecity,country,  " +
                                   " zipcode,branchid,idtype,idno,expirydate,dtcreated,dtmodified,createdby,modifiedby,  " +
                                   " phoneno,cardno,placeofbirth,natureofwork,permanentaddress,nationality,  companyoremployer,businessorprofession " +
                                    " ,govtidtype,govtidno " +
                                   " FROM  customerservicedb.{0}{1}COUNT " +
                                   " where lower(concat(fname,' ',mname,' ', lname)) = '{2}' " +
                                   //" or lower(fname) rlike '{2}' " +
                                   //" or lower(mname) rlike '{2}' " +
                                   //" or lower(lname) rlike '{2}' " +
                                   //" or lower(concat(lname,' ',fname,' ',mname)) rlike '{2}' " +
                                   //" or lower(concat(lname,', ',fname,' ',mname)) rlike '{2}' " +
                                   //" or lower(username) rlike '{2}' " +
                                   //" or lower(walletno) rlike '{2}' " +
                                   //" or lower(concat(fname,' ',mname)) rlike '{2}' " +
                                   " or lower(concat(fname,' ',lname)) = '{2}' " , txntype.ToUpper(), yrloop, custname.ToLower().Trim());
                                   //" or lower(concat(mname,' ', lname)) rlike '{2}' ", txntype.ToUpper(), yrloop, custname.ToLower().Trim());
                                }
                               
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
                                            var bdate = Reader["birthdate"].ToString().Trim().ToUpper();
                                            var gender = Reader["gender"].ToString().Trim().ToUpper();
                                            var email = Reader["emailadd"].ToString().Trim().ToUpper();


                                            var filtered = (from a in list
                                                            where   a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                                            && a.firstName.Trim().ToUpper() == fname.ToString().Trim().ToUpper()
                                                            && a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                                            && a.middleName.Trim().ToUpper() == mname.ToString().Trim().ToUpper()
                                                            select a);

                                            //var filtered =
                                            //    (i == 2 || i == 4 || i >= 6) ?
                                            //    (
                                            //        (fname.ToString().Trim().ToLower() == "null" || fname.ToString().Trim().ToLower() == "" || fname.ToString().Trim().ToLower() == null) ?
                                            //        (from a in list
                                            //         where a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                            //         && a.middleName.Trim().ToUpper() == mname.ToString().Trim().ToUpper()
                                            //         select a)
                                            //        :
                                            //        (
                                            //            (mname.ToString().Trim().ToLower() == "null" || mname.ToString().Trim().ToLower() == "" || mname.ToString().Trim().ToLower() == null) ?
                                            //            (from a in list
                                            //             where a.firstName.Trim().ToUpper() == fname.ToString().Trim().ToUpper()
                                            //             && a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                            //             select a)
                                            //            :
                                            //            (lname.ToString().Trim().ToLower() == "null" || lname.ToString().Trim().ToLower() == "" || lname.ToString().Trim().ToLower() == null) ?
                                            //                (from a in list
                                            //                 where a.firstName.Trim().ToUpper() == fname.ToString().Trim().ToUpper()
                                            //                 && a.middleName.Trim().ToUpper() == mname.ToString().Trim().ToUpper()
                                            //                 select a)
                                            //                 :
                                            //                (from a in list
                                            //                 where a.firstName.Trim().ToUpper() == fname.ToString().Trim().ToUpper()
                                            //                 && a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                            //                 && a.middleName.Trim().ToUpper() == mname.ToString().Trim().ToUpper()
                                            //                 select a)
                                            //        )
                                            //    )
                                            //    :
                                            //    ((i == 3 || i >= 5) ?
                                            //    ((username.ToString().Trim().ToLower() == "null" || username.ToString().Trim().ToLower() == "" || username.ToString().Trim().ToLower() == null) ?
                                            //    (from a in list
                                            //     where a.walletNo.Trim().ToUpper() == walletno.ToString().Trim().ToUpper()
                                            //     && a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                            //     && a.firstName.Trim().ToUpper() == fname.ToString().Trim().ToUpper()
                                            //     && a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                            //     select a)
                                            //    :
                                            //    (from a in list
                                            //     where a.userName.Trim().ToUpper() == username.ToString().Trim().ToUpper()
                                            //     && a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                            //     && a.firstName.Trim().ToUpper() == fname.ToString().Trim().ToUpper()
                                            //     && a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                            //     select a)
                                            //    )

                                            //    :
                                            //     ((i == 1) ?
                                            //     (((walletno.ToString().Trim().ToLower() == "null" || walletno.ToString().Trim().ToLower() == "" || walletno.ToString().Trim().ToLower() == null) && (username.ToString().Trim().ToLower() == "null" || username.ToString().Trim().ToLower() == "" || username.ToString().Trim().ToLower() == null)) ?
                                            //    (from a in list
                                            //     where a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                            //     && a.firstName.Trim().ToUpper() == fname.ToString().Trim().ToUpper()
                                            //     && a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                            //     && a.middleName.Trim().ToUpper() == mname.ToString().Trim().ToUpper()
                                            //     select a)
                                            //     :
                                            //     (from a in list
                                            //      where a.walletNo.Trim().ToUpper() == walletno.ToString().Trim().ToUpper()
                                            //      && a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                            //      && a.firstName.Trim().ToUpper() == fname.ToString().Trim().ToUpper()
                                            //      && a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                            //      && a.middleName.Trim().ToUpper() == mname.ToString().Trim().ToUpper()
                                            //      select a)
                                            //     )

                                            //     :
                                            //     (from a in list
                                            //      where a.userName.Trim().ToUpper() == username.ToString().Trim().ToUpper()
                                            //      && a.walletNo.Trim().ToUpper() == walletno.ToString().Trim().ToUpper()
                                            //      && a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                            //      && a.firstName.Trim().ToUpper() == fname.ToString().Trim().ToUpper()
                                            //      && a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                                            //      && a.middleName.Trim().ToUpper() == mname.ToString().Trim().ToUpper()
                                            //      select a)
                                            //    )
                                            //    );


                                            var count = filtered.Count();
                                            switch (i)
                                            {
                                                case 1:
                                                    cust.kpsocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 2:
                                                    cust.kppocount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 3:
                                                    cust.kprfccount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 4:
                                                    cust.kprtscount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 5:
                                                    cust.kpcsocount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 6:
                                                    cust.kpcpocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 7:
                                                    cust.walletsocount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 8:
                                                    cust.walletpocount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 9:
                                                    cust.walletrfccount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 10:
                                                    cust.walletrtscount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 11:
                                                    cust.walletcsocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 12:
                                                    cust.walletcpocount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 13:
                                                    cust.walletbpcount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 14:
                                                    cust.walleteloadcount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 15:
                                                    cust.walletcorppocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 16:
                                                    cust.expresssocount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 17:
                                                    cust.expresspocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 18:
                                                    cust.expressrfccount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 19:
                                                    cust.expressrtscount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 20:
                                                    cust.expresscsocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 21:
                                                    cust.expresscpocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 22:
                                                    cust.expressbpcount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 23:
                                                    cust.expresseloadcount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 24:
                                                    cust.expresscorppocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 26:
                                                    cust.apipocount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 30:
                                                    cust.apicpocount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 31:
                                                    cust.fusocount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 32:
                                                    cust.fupocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 33:
                                                    cust.furfccount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 34:
                                                    cust.furtscount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 35:
                                                    cust.fucsocount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 36:
                                                    cust.fucpocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 37:
                                                    cust.wscsocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 38:
                                                    cust.wscpocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 39:
                                                    cust.wscrfccount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 40:
                                                    cust.wscrtscount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 41:
                                                    cust.wsccsocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 42:
                                                    cust.wsccpocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 43:
                                                    cust.bpsocount =  Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 44:
                                                    cust.bprfccount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 46:
                                                    cust.bpcsocount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 47:
                                                    cust.prendacount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 48:
                                                    cust.lukatcount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 49:
                                                    cust.renewcount =   Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 50:
                                                    cust.reappraisecount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 51:
                                                    cust.globalsocount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 52:
                                                    cust.globalpocount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 53:
                                                    cust.globalrfccount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 54:
                                                    cust.globalrtscount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 55:
                                                    cust.globalcsocount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                                case 56:
                                                    cust.globalcpocount = Convert.ToInt64(Reader["totalcount"]);
                                                    break;
                                            }
                                            if (count == 0)
                                            {
                                                list.Add(new CustomerServiceModel
                                                {
                                                    custID = (Reader["custid"] == System.DBNull.Value) ? "" : Reader["custid"].ToString().Trim().ToUpper(),
                                                    firstName = (Reader["fname"] == System.DBNull.Value) ? "" : Reader["fname"].ToString().Trim().ToUpper(),
                                                    lastName = (Reader["lname"] == System.DBNull.Value) ? "" : Reader["lname"].ToString().Trim().ToUpper(),
                                                    middleName = (Reader["mname"] == System.DBNull.Value) ? "" : Reader["mname"].ToString().Trim().ToUpper(),
                                                    userName = (Reader["username"] == System.DBNull.Value) ? "" : Reader["username"].ToString().Trim().ToUpper(),
                                                    emailAdd = (Reader["emailadd"] == System.DBNull.Value) ? "" : Reader["emailadd"].ToString().Trim().ToUpper(),
                                                    birthDate = (Reader["birthdate"] == System.DBNull.Value) ? "" : Reader["birthdate"].ToString().Trim().ToUpper(),
                                                    mobileNo = (Reader["mobileno"] == System.DBNull.Value) ? "" : Reader["mobileno"].ToString().Trim().ToUpper(),
                                                    address = (Reader["address"] == System.DBNull.Value) ? "" : Reader["address"].ToString().Trim().ToUpper(),
                                                    gender = (Reader["gender"] == System.DBNull.Value) ? "" : Reader["gender"].ToString().Trim().ToUpper(),
                                                    walletNo = (Reader["walletno"] == System.DBNull.Value) ? "" : Reader["walletno"].ToString().Trim().ToUpper(),

                                                    street = (Reader["street"] == System.DBNull.Value) ? "" : Reader["street"].ToString().Trim().ToUpper(),
                                                    provincecity = (Reader["provincecity"] == System.DBNull.Value) ? "" : Reader["provincecity"].ToString().Trim().ToUpper(),
                                                    country = (Reader["country"] == System.DBNull.Value) ? "" : Reader["country"].ToString().Trim().ToUpper(),
                                                    zipcode = (Reader["zipcode"] == System.DBNull.Value) ? "" : Reader["zipcode"].ToString().Trim().ToUpper(),
                                                    branchid = (Reader["branchid"] == System.DBNull.Value) ? "" : Reader["branchid"].ToString().Trim().ToUpper(),
                                                    idtype = (Reader["idtype"] == System.DBNull.Value) ? "" : Reader["idtype"].ToString().Trim().ToUpper(),
                                                    idno = (Reader["idno"] == System.DBNull.Value) ? "" : Reader["idno"].ToString().Trim().ToUpper(),
                                                    expirydate = (Reader["expirydate"] == System.DBNull.Value) ? "" : Reader["expirydate"].ToString().Trim().ToUpper(),
                                                    dtcreated = (Reader["dtcreated"] == System.DBNull.Value) ? "" : Reader["dtcreated"].ToString().Trim().ToUpper(),
                                                    dtmodified = (Reader["dtmodified"] == System.DBNull.Value) ? "" : Reader["dtmodified"].ToString().Trim().ToUpper(),
                                                    createdby = (Reader["createdby"] == System.DBNull.Value) ? "" : Reader["createdby"].ToString().Trim().ToUpper(),
                                                    modifiedby = (Reader["modifiedby"] == System.DBNull.Value) ? "" : Reader["modifiedby"].ToString().Trim().ToUpper(),
                                                    phoneno = (Reader["phoneno"] == System.DBNull.Value) ? "" : Reader["phoneno"].ToString().Trim().ToUpper(),
                                                    cardno = (Reader["cardno"] == System.DBNull.Value) ? "" : Reader["cardno"].ToString().Trim().ToUpper(),
                                                    placeofbirth = (Reader["placeofbirth"] == System.DBNull.Value) ? "" : Reader["placeofbirth"].ToString().Trim().ToUpper(),
                                                    natureofwork = (Reader["natureofwork"] == System.DBNull.Value) ? "" : Reader["natureofwork"].ToString().Trim().ToUpper(),
                                                    permanentaddress = (Reader["permanentaddress"] == System.DBNull.Value) ? "" : Reader["permanentaddress"].ToString().Trim().ToUpper(),
                                                    nationality = (Reader["nationality"] == System.DBNull.Value) ? "" : Reader["nationality"].ToString().Trim().ToUpper(),
                                                    companyoremployer = (Reader["companyoremployer"] == System.DBNull.Value) ? "" : Reader["companyoremployer"].ToString().Trim().ToUpper(),
                                                    businessorprofession = (Reader["businessorprofession"] == System.DBNull.Value) ? "" : Reader["businessorprofession"].ToString().Trim().ToUpper(),
                                                    govtidtype = (Reader["govtidtype"] == System.DBNull.Value) ? "" : Reader["govtidtype"].ToString().Trim().ToUpper(),
                                                    govtidno = (Reader["govtidno"] == System.DBNull.Value) ? "" : Reader["govtidno"].ToString().Trim().ToUpper(),

                                                    kpsocount = (i!=1)?0:cust.kpsocount,
                                                    kppocount = (i != 2) ? 0 : cust.kppocount,
                                                    kprfccount = (i !=3) ? 0 : cust.kprfccount,
                                                    kprtscount = (i != 4) ? 0 : cust.kprtscount,
                                                    kpcsocount = (i != 5) ? 0 : cust.kpcsocount,
                                                    kpcpocount = (i != 6) ? 0 : cust.kpcpocount,
                                                    walletsocount = (i != 7) ? 0 : cust.walletsocount,
                                                    walletpocount = (i != 8) ? 0 : cust.walletpocount,
                                                    walletrfccount = (i != 9) ? 0 : cust.walletrfccount,
                                                    walletrtscount = (i != 10) ? 0 : cust.walletrtscount,
                                                    walletcsocount = (i != 11) ? 0 : cust.walletcsocount,
                                                    walletcpocount = (i != 12) ? 0 : cust.walletcpocount,
                                                    walletbpcount = (i != 13) ? 0 : cust.walletbpcount,
                                                    walleteloadcount = (i != 14) ? 0 : cust.walleteloadcount,
                                                    walletcorppocount = (i != 15) ? 0 : cust.walletcorppocount,
                                                    expresssocount = (i != 16) ? 0 : cust.expresssocount,
                                                    expresspocount = (i != 17) ? 0 : cust.expresspocount,
                                                    expressrfccount = (i != 18) ? 0 : cust.expressrfccount,
                                                    expressrtscount = (i != 19) ? 0 : cust.expressrtscount,
                                                    expresscsocount = (i != 20) ? 0 : cust.expresscsocount,
                                                    expresscpocount = (i != 21) ? 0 : cust.expresscpocount,
                                                    expressbpcount = (i != 22) ? 0 : cust.expressbpcount,
                                                    expresseloadcount = (i != 23) ? 0 : cust.expresseloadcount,
                                                    expresscorppocount = (i != 24) ? 0 : cust.expresscorppocount,
                                                    apipocount = (i != 26) ? 0 : cust.apipocount,
                                                    apicpocount = (i != 30) ? 0 : cust.apicpocount,
                                                    fusocount = (i != 31) ? 0 : cust.fusocount,
                                                    fupocount = (i != 32) ? 0 : cust.fupocount,
                                                    furfccount = (i != 33) ? 0 : cust.furfccount,
                                                    furtscount = (i != 34) ? 0 : cust.furtscount,
                                                    fucsocount = (i != 35) ? 0 : cust.fucsocount,
                                                    fucpocount = (i != 36) ? 0 : cust.fucpocount,
                                                    wscsocount = (i != 37) ? 0 : cust.wscsocount,
                                                    wscpocount = (i != 38) ? 0 : cust.wscpocount,
                                                    wscrfccount = (i != 39) ? 0 : cust.wscrfccount,
                                                    wscrtscount = (i != 40) ? 0 : cust.wscrtscount,
                                                    wsccsocount = (i != 41) ? 0 : cust.wsccsocount,
                                                    wsccpocount = (i != 42) ? 0 : cust.wsccpocount,
                                                    bpsocount = (i != 43) ? 0 : cust.bpsocount,
                                                    bprfccount = (i != 44) ? 0 : cust.bprfccount,
                                                    bpcsocount = (i != 46) ? 0 : cust.bpcsocount,

                                                    globalsocount = (i != 51) ? 0 : cust.globalsocount,
                                                    globalpocount = (i != 52) ? 0 : cust.globalpocount,
                                                    globalrfccount = (i != 53) ? 0 : cust.globalrfccount,
                                                    globalrtscount = (i != 54) ? 0 : cust.globalrtscount,
                                                    globalcsocount = (i != 55) ? 0 : cust.globalcsocount,
                                                    globalcpocount = (i != 56) ? 0 : cust.globalcpocount,

                                                    prendacount = (i != 47) ? 0 : cust.prendacount,
                                                    lukatcount = (i != 48) ? 0 : cust.lukatcount,
                                                    renewcount = (i != 49) ? 0 : cust.renewcount,
                                                    reappraisecount = (i != 50) ? 0 : cust.reappraisecount,
                                                    layawaycount = 0,
                                                    salescount = 0,
                                                    tradeincount = 0,
                                                    sblcount = 0,
                                                    eloadcount = 0,
                                                    insurancecount = 0,
                                                    goodscount = 0,
                                                });
                                            }
                                            else
                                            {
                                                var obj = (list.FirstOrDefault(a => a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                                    && a.lastName.Trim().ToUpper() == lname.Trim().ToUpper()
                                                    && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper()
                                                    && a.middleName.Trim().ToUpper() == mname.Trim().ToUpper() ));
                                                //var obj =
                                                //(i == 2 || i == 4 || i >= 6) ?
                                                //(
                                                //    (fname.ToString().Trim().ToLower() == "null" || fname.ToString().Trim().ToLower() == "" || fname.ToString().Trim().ToLower() == null) ?
                                                //        (list.FirstOrDefault(a => a.lastName.Trim().ToUpper() == lname.Trim().ToUpper()
                                                //        && a.middleName.Trim().ToUpper() == mname.Trim().ToUpper()))
                                                //    :
                                                //    (
                                                //        (mname.ToString().Trim().ToLower() == "null" || mname.ToString().Trim().ToLower() == "" || mname.ToString().Trim().ToLower() == null) ?
                                                //            (list.FirstOrDefault(a => a.lastName.Trim().ToUpper() == lname.Trim().ToUpper()
                                                //            && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper()))
                                                //        :
                                                //        (lname.ToString().Trim().ToLower() == "null" || lname.ToString().Trim().ToLower() == "" || lname.ToString().Trim().ToLower() == null) ?
                                                //            (list.FirstOrDefault(a => a.firstName.Trim().ToUpper() == fname.Trim().ToUpper()
                                                //            && a.middleName.Trim().ToUpper() == mname.Trim().ToUpper()))
                                                //            :
                                                //            (list.FirstOrDefault(a => a.lastName.Trim().ToUpper() == lname.Trim().ToUpper()
                                                //            && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper()
                                                //            && a.middleName.Trim().ToUpper() == mname.Trim().ToUpper()))
                                                //    )
                                                //)
                                                //:
                                                //((i == 3 || i >= 5) ?
                                                //((username.ToString().Trim().ToLower() == "null" || username.ToString().Trim().ToLower() == "" || username.ToString().Trim().ToLower() == null) ?
                                                //(list.FirstOrDefault(a => a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                                //    && a.lastName.Trim().ToUpper() == lname.Trim().ToUpper()
                                                //    && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper()
                                                //    && a.walletNo.Trim().ToUpper() == walletno.Trim().ToUpper()))
                                                //:
                                                //(list.FirstOrDefault(a => a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                                //    && a.lastName.Trim().ToUpper() == lname.Trim().ToUpper()
                                                //    && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper()
                                                //    && a.userName.Trim().ToUpper() == username.Trim().ToUpper()))
                                                //)

                                                //:
                                                //((i == 1) ?
                                                //(((walletno.ToString().Trim().ToLower() == "null" || walletno.ToString().Trim().ToLower() == "" || walletno.ToString().Trim().ToLower() == null) && (username.ToString().Trim().ToLower() == "null" || username.ToString().Trim().ToLower() == "" || username.ToString().Trim().ToLower() == null)) ?
                                                // (list.FirstOrDefault(a => a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                                //    && a.lastName.Trim().ToUpper() == lname.Trim().ToUpper()
                                                //    && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper()
                                                //    && a.middleName.Trim().ToUpper() == mname.Trim().ToUpper()))
                                                //:
                                                // (list.FirstOrDefault(a => a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                                //    && a.lastName.Trim().ToUpper() == lname.Trim().ToUpper()
                                                //    && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper()
                                                //    && a.middleName.Trim().ToUpper() == mname.Trim().ToUpper()
                                                //    && a.walletNo.Trim().ToUpper() == walletno.Trim().ToUpper()))
                                                //)

                                                //:
                                                //(list.FirstOrDefault(a => a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()
                                                //    && a.lastName.Trim().ToUpper() == lname.Trim().ToUpper()
                                                //    && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper()
                                                //    && a.middleName.Trim().ToUpper() == mname.Trim().ToUpper()
                                                //    && a.walletNo.Trim().ToUpper() == walletno.Trim().ToUpper()
                                                //    && a.userName.Trim().ToUpper() == username.Trim().ToUpper())))

                                                //);

                                                obj.walletNo = (obj.walletNo.ToString() == "" || obj.walletNo.ToString().ToLower() == null || obj.walletNo.ToString().ToLower() == "null" ? walletno : obj.walletNo);
                                                obj.userName = (obj.userName.ToString() == "" || obj.userName.ToString().ToLower() == null || obj.userName.ToString().ToLower() == "null" ? username : obj.userName);
                                                obj.custID = (obj.custID.ToString() == "" || obj.custID.ToString().ToLower() == null || obj.custID.ToString().ToLower() == "null" ? custid : obj.custID);
                                                obj.birthDate = (obj.birthDate.ToString() == "" || obj.birthDate.ToString().ToLower() == null || obj.birthDate.ToString().ToLower() == "null" ? bdate : obj.birthDate);
                                                obj.gender = (obj.gender.ToString() == "" || obj.gender.ToString().ToLower().ToLower() == null || obj.gender.ToString().ToLower() == "null" ? gender : obj.gender);
                                                obj.emailAdd = (obj.emailAdd.ToString() == "" || obj.emailAdd.ToString().ToLower().ToLower() == null || obj.emailAdd.ToString().ToLower() == "null" ? email : obj.emailAdd);

                                                obj.kpsocount = (i != 1) ? Convert.ToInt64(obj.kpsocount) : Convert.ToInt64(obj.kpsocount) + cust.kpsocount;
                                                obj.kppocount = (i != 2) ? Convert.ToInt64(obj.kppocount) : Convert.ToInt64(obj.kppocount) + cust.kppocount;
                                                obj.kprfccount = (i != 3) ? Convert.ToInt64(obj.kprfccount) : Convert.ToInt64(obj.kprfccount) + cust.kprfccount;
                                                obj.kprtscount = (i != 4) ? Convert.ToInt64(obj.kprtscount) : Convert.ToInt64(obj.kprtscount) + cust.kprtscount;
                                                obj.kpcsocount = (i != 5) ? Convert.ToInt64(obj.kpcsocount) : Convert.ToInt64(obj.kpcsocount) + cust.kpcsocount;
                                                obj.kpcpocount = (i != 6) ? Convert.ToInt64(obj.kpcpocount) : Convert.ToInt64(obj.kpcpocount) + cust.kpcpocount;
                                                obj.walletsocount = (i != 7) ? Convert.ToInt64(obj.walletsocount) : Convert.ToInt64(obj.walletsocount) + cust.walletsocount;
                                                obj.walletpocount = (i != 8) ? Convert.ToInt64(obj.walletpocount) : Convert.ToInt64(obj.walletpocount) + cust.walletpocount;
                                                obj.walletrfccount = (i != 9) ? Convert.ToInt64(obj.walletrfccount) : Convert.ToInt64(obj.walletrfccount) + cust.walletrfccount;
                                                obj.walletrtscount = (i != 10) ? Convert.ToInt64(obj.walletrtscount) : Convert.ToInt64(obj.walletrtscount) + cust.walletrtscount;
                                                obj.walletcsocount = (i != 11) ? Convert.ToInt64(obj.walletcsocount) : Convert.ToInt64(obj.walletcsocount) + cust.walletcsocount;
                                                obj.walletcpocount = (i != 12) ? Convert.ToInt64(obj.walletcpocount) : Convert.ToInt64(obj.walletcpocount) + cust.walletcpocount;
                                                obj.walletbpcount = (i != 13) ? Convert.ToInt64(obj.walletbpcount) : Convert.ToInt64(obj.walletbpcount) + cust.walletbpcount;
                                                obj.walleteloadcount = (i != 14) ? Convert.ToInt64(obj.walleteloadcount) : Convert.ToInt64(obj.walleteloadcount) + cust.walleteloadcount;
                                                obj.walletcorppocount = (i != 15) ? Convert.ToInt64(obj.walletcorppocount) : Convert.ToInt64(obj.walletcorppocount) + cust.walletcorppocount;
                                                obj.expresssocount = (i != 16) ? Convert.ToInt64(obj.expresssocount) : Convert.ToInt64(obj.expresssocount) + cust.expresssocount;
                                                obj.expresspocount = (i != 17) ? Convert.ToInt64(obj.expresspocount) : Convert.ToInt64(obj.expresspocount) + cust.expresspocount;
                                                obj.expressrfccount = (i != 18) ? Convert.ToInt64(obj.expressrfccount) : Convert.ToInt64(obj.expressrfccount) + cust.expressrfccount;
                                                obj.expressrtscount = (i != 19) ? Convert.ToInt64(obj.expressrtscount) : Convert.ToInt64(obj.expressrtscount) + cust.expressrtscount;
                                                obj.expresscsocount = (i != 20) ? Convert.ToInt64(obj.expresscsocount) : Convert.ToInt64(obj.expresscsocount) + cust.expresscsocount;
                                                obj.expresscpocount = (i != 21) ? Convert.ToInt64(obj.expresscpocount) : Convert.ToInt64(obj.expresscpocount) + cust.expresscpocount;
                                                obj.expressbpcount = (i != 22) ? Convert.ToInt64(obj.expressbpcount) : Convert.ToInt64(obj.expressbpcount) + cust.expressbpcount;
                                                obj.expresseloadcount = (i != 23) ? Convert.ToInt64(obj.expresseloadcount) : Convert.ToInt64(obj.expresseloadcount) + cust.expresseloadcount;
                                                obj.expresscorppocount = (i != 24) ? Convert.ToInt64(obj.expresscorppocount) : Convert.ToInt64(obj.expresscorppocount) + cust.expresscorppocount;
                                                obj.apipocount = (i != 26) ? Convert.ToInt64(obj.apipocount) : Convert.ToInt64(obj.apipocount) + cust.apipocount;
                                                obj.apicpocount = (i != 30) ? Convert.ToInt64(obj.apicpocount) : Convert.ToInt64(obj.apicpocount) + cust.apicpocount;
                                                obj.fusocount = (i != 31) ? Convert.ToInt64(obj.fusocount) : Convert.ToInt64(obj.fusocount) + cust.fusocount;
                                                obj.fupocount = (i != 32) ? Convert.ToInt64(obj.fupocount) : Convert.ToInt64(obj.fupocount) + cust.fupocount;
                                                obj.furfccount = (i != 33) ? Convert.ToInt64(obj.furfccount) : Convert.ToInt64(obj.furfccount) + cust.furfccount;
                                                obj.furtscount = (i != 34) ? Convert.ToInt64(obj.furtscount) : Convert.ToInt64(obj.furtscount) + cust.furtscount;
                                                obj.fucsocount = (i != 35) ? Convert.ToInt64(obj.fucsocount) : Convert.ToInt64(obj.fucsocount) + cust.fucsocount;
                                                obj.fucpocount = (i != 36) ? Convert.ToInt64(obj.fucpocount) : Convert.ToInt64(obj.fucpocount) + cust.fucpocount;
                                                obj.wscsocount = (i != 37) ? Convert.ToInt64(obj.wscsocount) : Convert.ToInt64(obj.wscsocount) + cust.wscsocount;
                                                obj.wscpocount = (i != 38) ? Convert.ToInt64(obj.wscpocount) : Convert.ToInt64(obj.wscpocount) + cust.wscpocount;
                                                obj.wscrfccount = (i != 39) ? Convert.ToInt64(obj.wscrfccount) : Convert.ToInt64(obj.wscrfccount) + cust.wscrfccount;
                                                obj.wscrtscount = (i != 40) ? Convert.ToInt64(obj.wscrtscount) : Convert.ToInt64(obj.wscrtscount) + cust.wscrtscount;
                                                obj.wsccsocount = (i != 41) ? Convert.ToInt64(obj.wsccsocount) : Convert.ToInt64(obj.wsccsocount) + cust.wsccsocount;
                                                obj.wsccpocount = (i != 42) ? Convert.ToInt64(obj.wsccpocount) : Convert.ToInt64(obj.wsccpocount) + cust.wsccpocount;
                                                obj.bpsocount = (i != 43) ? Convert.ToInt64(obj.bpsocount) : Convert.ToInt64(obj.bpsocount) + cust.bpsocount;
                                                obj.bprfccount = (i != 44) ? Convert.ToInt64(obj.bprfccount) : Convert.ToInt64(obj.bprfccount) + cust.bprfccount;
                                                obj.bpcsocount = (i != 46) ? Convert.ToInt64(obj.bpcsocount) : Convert.ToInt64(obj.bpcsocount) + cust.bpcsocount;
                                                obj.prendacount = (i != 47) ? Convert.ToInt64(obj.prendacount) : Convert.ToInt64(obj.prendacount) + cust.prendacount;
                                                obj.lukatcount = (i != 48) ? Convert.ToInt64(obj.lukatcount) : Convert.ToInt64(obj.lukatcount) + cust.lukatcount;
                                                obj.renewcount = (i != 49) ? Convert.ToInt64(obj.renewcount) : Convert.ToInt64(obj.renewcount) + cust.renewcount;
                                                obj.reappraisecount = (i != 50) ? Convert.ToInt64(obj.reappraisecount) : Convert.ToInt64(obj.reappraisecount) + cust.reappraisecount;
                                                obj.globalsocount = (i != 51) ? Convert.ToInt64(obj.globalsocount) : Convert.ToInt64(obj.globalsocount) + cust.globalsocount;
                                                obj.globalpocount = (i != 52) ? Convert.ToInt64(obj.globalpocount) : Convert.ToInt64(obj.globalpocount) + cust.globalpocount;
                                                obj.globalrfccount = (i != 53) ? Convert.ToInt64(obj.globalrfccount) : Convert.ToInt64(obj.globalrfccount) + cust.globalrfccount;
                                                obj.globalrtscount = (i != 54) ? Convert.ToInt64(obj.globalrtscount) : Convert.ToInt64(obj.globalrtscount) + cust.globalrtscount;
                                                obj.globalcsocount = (i != 55) ? Convert.ToInt64(obj.globalcsocount) : Convert.ToInt64(obj.globalcsocount) + cust.globalcsocount;
                                                obj.globalcpocount = (i != 56) ? Convert.ToInt64(obj.globalcpocount) : Convert.ToInt64(obj.globalcpocount) + cust.globalcpocount;
                                            }
                                        }
                                        Reader.Close();
                                    }
                                }
                            }
                        }
                        mycon.Close();
                        if (list.Count != 0)
                        {
                            list = list.OrderBy(x => x.lastName).ToList();
                            System.Web.HttpContext.Current.Session["custlist"] = list;
                            return Json(list, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("Empty", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch { return Json("Error", JsonRequestBehavior.AllowGet); }
        }

        public ActionResult getCustomerSummary(string custid, string uname, string walletno, string fname, string mname, string lname)
        {
            try {
                List<CustomerServiceModel> custlist = (List<CustomerServiceModel>)System.Web.HttpContext.Current.Session["custlist"];
                if (custlist == null) { return RedirectToAction("Index", "Logout"); }
                var list = new List<CustomerServiceModel>();
                custid = custid.Replace('_', ' ');
                uname = uname.Replace('_', ' ');
                walletno = walletno.Replace('_', ' ');
                fname = fname.Replace('_', ' ');
                mname = mname.Replace('_', ' ');
                lname = lname.Replace('_', ' ');

                var emailadd = string.Empty;
                var birthdate = string.Empty;
                var mobileno = string.Empty;
                var address = string.Empty;
                var gender = string.Empty;


                var cust = from a in custlist
                           where a.userName.Trim().ToUpper() == uname.Trim().ToUpper()
                            && a.walletNo.Trim().ToUpper() == walletno.Trim().ToUpper() && a.custID.Trim().ToUpper() == custid.Trim().ToUpper()
                            && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper() && a.lastName.Trim().ToUpper() == lname.Trim().ToUpper() && a.middleName.Trim().ToUpper() == mname.Trim().ToUpper()
                           select a;

                foreach (var a in cust)
                {
                    emailadd = (a.emailAdd.ToLower() == null || a.emailAdd.ToLower() == "null") ? "-" : a.emailAdd.ToString().ToUpper().Trim();
                    birthdate = (a.birthDate == null || a.birthDate == "null") ? "-" : a.birthDate.ToString().ToUpper().Trim();
                    mobileno = (a.mobileNo == null || a.mobileNo == "null") ? "-" : a.mobileNo.ToString().ToUpper().Trim();
                    address = (a.address.ToLower() == null || a.address.ToLower() == "null") ? "-" : a.address.ToString().ToUpper().Trim();
                    gender = (a.gender.ToLower() == null || a.gender.ToLower() == "null") ? "-" : a.gender.ToString().ToUpper().Trim();

                    list.Add(new CustomerServiceModel
                    {
                        custID = (custid == "NULL") ? "-" : custid,
                        firstName = fname,
                        lastName = lname,
                        middleName = mname,
                        userName = uname,
                        walletNo = walletno,
                        emailAdd = emailadd,
                        birthDate = birthdate,
                        mobileNo = mobileno,
                        address = address,
                        gender = gender,
                        kpsocount = a.kpsocount,
                        kppocount = a.kppocount,
                        walletsocount = a.walletsocount,
                        walletpocount = a.walletpocount,
                        expresssocount = a.expresssocount,
                        expresspocount = a.expresspocount,
                        globalsocount = a.globalsocount,
                        globalpocount = a.globalpocount,
                        apipocount = a.apipocount,
                        bpsocount = a.bpsocount,
                        fusocount = a.fusocount,
                        fupocount = a.fupocount,
                        wscsocount = a.wscsocount,
                        wscpocount = a.wscpocount,
                        prendacount = a.prendacount,
                        lukatcount = a.lukatcount,
                        renewcount = a.renewcount,
                        reappraisecount = a.reappraisecount,
                        layawaycount = a.layawaycount,
                        salescount = a.salescount,
                        tradeincount = a.tradeincount,
                        sblcount = a.sblcount,
                        eloadcount = a.eloadcount,
                        insurancecount = a.insurancecount,
                        goodscount = a.goodscount,
                    });
                }
                if (list.Count != 0)
                {
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CustomerServiceModel cust = new CustomerServiceModel();
                cust.WriteToFile("Error in Generating Customer Summary : " + ex.ToString());
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult getCustomerInfo(string custid, string uname, string walletno, string fname, string mname, string lname)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                List<CustomerServiceModel> custlist = (List<CustomerServiceModel>)System.Web.HttpContext.Current.Session["custlist"];
                if (custlist == null) { return RedirectToAction("Index", "Logout"); }
                var list = new List<CustomerServiceModel>();
                custid = custid.Replace('_', ' ');
                uname = uname.Replace('_', ' ');
                walletno = walletno.Replace('_', ' ');
                fname = fname.Replace('_', ' ');
                mname = mname.Replace('_', ' ');
                lname = lname.Replace('_', ' ');

                var emailadd = string.Empty;
                var birthdate = string.Empty;
                var mobileno = string.Empty;
                var address = string.Empty;
                var gender = string.Empty;


                var custs = from a in custlist
                            where a.userName.Trim().ToUpper() == uname.Trim().ToUpper()
                             && a.walletNo.Trim().ToUpper() == walletno.Trim().ToUpper() && a.custID.Trim().ToUpper() == custid.Trim().ToUpper()
                             && a.firstName.Trim().ToUpper() == fname.Trim().ToUpper() && a.lastName.Trim().ToUpper() == lname.Trim().ToUpper() && a.middleName.Trim().ToUpper() == mname.Trim().ToUpper()
                            select a;

                foreach (var a in custs)
                {
                    emailadd = (a.emailAdd.ToLower() == null || a.emailAdd.ToLower() == "null") ? "-" : a.emailAdd.ToString().ToUpper().Trim();
                    birthdate = (a.birthDate == null || a.birthDate == "null") ? "-" : a.birthDate.ToString().ToUpper().Trim();
                    mobileno = (a.mobileNo == null || a.mobileNo == "null") ? "-" : a.mobileNo.ToString().ToUpper().Trim();
                    address = (a.address.ToLower() == null || a.address.ToLower() == "null") ? "-" : a.address.ToString().ToUpper().Trim();
                    gender = (a.gender.ToLower() == null || a.gender.ToLower() == "null") ? "-" : a.gender.ToString().ToUpper().Trim();

                    string kycfront = string.Empty;
                    string kycback = string.Empty;
                    string id1 = string.Empty;
                    string id2 = string.Empty;
                    string id3 = string.Empty;
                    string fingerprint = string.Empty;
                    string customersimage = string.Empty;
                    string  imagefree1 = string.Empty;
                    string imagefree2 = string.Empty;
                    string imagefree3 = string.Empty ;
                    string topPath = string.Empty;

                    byte[] front = new byte[1000];
                    byte[]  back = new byte[1000];
                    byte[] idone = new byte[1000];
                    byte[] idtwo = new byte[1000];
                    byte[] idthree = new byte[1000];
                    byte[] fprint = new byte[1000];
                    byte[] custimage = new byte[1000];
                    byte[] image1 = new byte[1000];
                    byte[] image2 = new byte[1000];
                    byte[] image3 = new byte[1000];

                    try
                    {
                       
                        var tblimage = "customer" + a.firstName.Substring(0,1).ToUpper();
                        conn.connectdb("CustomerImg" + a.firstName.Substring(0, 1).ToUpper());
                        using (MySqlConnection mycon = conn.getConnection())
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                cmd.CommandText = string.Format("SELECT kycfront,kycback,id1,id2,id3,fingerprint,customerimage,imagefree1,imagefree2,imagefree3 from customerscanimages.{0} where custid='{1}'  limit 1;", tblimage, a.custID.Trim().ToLower());

                                using (MySqlDataReader Reader = cmd.ExecuteReader())
                                {
                                    topPath = string.Format("{0}Content\\images\\customerimages\\", AppDomain.CurrentDomain.BaseDirectory);
                                    if (Reader.HasRows)
                                    {

                                        bool directoryExists = Directory.Exists(topPath);
                                        if (directoryExists == true) {
                                            Directory.Delete(topPath,true ); 
                                        }


                                        Reader.Read();
                                        front = (Reader["kycfront"] == System.DBNull.Value) ? null : (byte[])Reader["kycfront"];
                                        back = (Reader["kycback"] == System.DBNull.Value) ? null : (byte[])Reader["kycback"];
                                        idone = (Reader["id1"] == System.DBNull.Value) ? null : (byte[])Reader["id1"];
                                        idtwo = (Reader["id2"] == System.DBNull.Value) ? null : (byte[])Reader["id2"];
                                        idthree = (Reader["id3"] == System.DBNull.Value) ? null : (byte[])Reader["id3"];
                                        fprint = (Reader["fingerprint"] == System.DBNull.Value) ? null : (byte[])Reader["fingerprint"];
                                        custimage = (Reader["customerimage"] == System.DBNull.Value) ? null : (byte[])Reader["customerimage"];
                                        image1 = (Reader["imagefree1"] == System.DBNull.Value) ? null : (byte[])Reader["imagefree1"];
                                        image2 = (Reader["imagefree2"] == System.DBNull.Value) ? null : (byte[])Reader["imagefree2"];
                                        image3 = (Reader["imagefree3"] == System.DBNull.Value) ? null : (byte[])Reader["imagefree3"];

                                        Directory.CreateDirectory(topPath);

                                        if (front != null)
                                        {
                                            kycfront = DisplayImages(front,"kycfront.jpg");
                                        }
                                        else
                                        {
                                            kycfront = string.Format("{0}Content\\images\\defaultimage\\noimage.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                        if (back != null)
                                        {
                                            kycback = DisplayImages(back, "kycback.jpg");
                                        }
                                        else {
                                            kycback = string.Format("{0}Content\\images\\defaultimage\\noimage.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                        if (idone != null) 
                                        {
                                            id1 = DisplayImages(idone, "id1.jpg");
                                        }
                                        else {
                                            id1 = string.Format("{0}Content\\images\\defaultimage\\id.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                        if (idtwo != null)
                                        {
                                            id2 = DisplayImages(idtwo, "id2.jpg");
                                        }
                                        else {
                                            id2 = string.Format("{0}Content\\images\\defaultimage\\id.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                        if (idthree != null)
                                        {
                                            id3 = DisplayImages(idthree, "id3.jpg");
                                        }
                                        else {
                                            id3 = string.Format("{0}Content\\images\\defaultimage\\id.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                        if (fprint != null)
                                        {
                                            fingerprint = DisplayImages(fprint, "fingerprint.jpg");
                                        }
                                        else {
                                            fingerprint = string.Format("{0}Content\\images\\defaultimage\\fingerprint.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                        if (image1 != null)
                                        {
                                            imagefree1 = DisplayImages(image1, "img1.jpg");
                                        }
                                        else {
                                            imagefree1 = string.Format("{0}Content\\images\\defaultimage\\signature.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                        if (image2 != null)
                                        {
                                            imagefree2 = DisplayImages(image2, "img2.jpg");
                                        }
                                        else {
                                            imagefree2 = string.Format("{0}Content\\images\\defaultimage\\noimage.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                        if (image3 != null)
                                        {
                                            imagefree3 = DisplayImages(image3, "img3.jpg");
                                        }
                                        else {
                                            imagefree3 = string.Format("{0}Content\\images\\defaultimage\\noimage.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                        if (custimage != null)
                                        {
                                            customersimage = DisplayImages(custimage, "custimg.jpg");
                                        }
                                        else {
                                            customersimage = string.Format("{0}Content\\images\\defaultimage\\custimage.png", AppDomain.CurrentDomain.BaseDirectory);
                                        }
                                    }
                                    else {
                                        kycfront = string.Format("{0}Content\\images\\defaultimage\\noimage.png", AppDomain.CurrentDomain.BaseDirectory);
                                        kycback = string.Format("{0}Content\\images\\defaultimage\\noimage.png", AppDomain.CurrentDomain.BaseDirectory);
                                        id1 = string.Format("{0}Content\\images\\defaultimage\\id.png", AppDomain.CurrentDomain.BaseDirectory);
                                        id2 = string.Format("{0}Content\\images\\defaultimage\\id.png", AppDomain.CurrentDomain.BaseDirectory);
                                        id3 = string.Format("{0}Content\\images\\defaultimage\\id.png", AppDomain.CurrentDomain.BaseDirectory);
                                        fingerprint = string.Format("{0}Content\\images\\defaultimage\\fingerprint.png", AppDomain.CurrentDomain.BaseDirectory);
                                        imagefree1 = string.Format("{0}Content\\images\\defaultimage\\signature.png", AppDomain.CurrentDomain.BaseDirectory);
                                        customersimage = string.Format("{0}Content\\images\\defaultimage\\custimage.png", AppDomain.CurrentDomain.BaseDirectory);
                                    }
                                    Reader.Close();
                                    mycon.Close();
                                }
                            }
                        }
                    }
                    catch (Exception ex) { cust.WriteToFile("Get KYC Image : " + ex.ToString()); }


                  


                    list.Add(new CustomerServiceModel
                    {
                        custID = (custid == "NULL") ? "-" : custid,
                        firstName = fname,
                        lastName = lname,
                        middleName = mname,
                        userName = uname,
                        walletNo = walletno,
                        emailAdd = emailadd,
                        birthDate = birthdate,
                        mobileNo = mobileno,
                        address = address,
                        gender = gender,
                        street = a.street,
                        provincecity = a.provincecity,
                        country = a.country,
                        zipcode = a.zipcode,
                        branchid = a.branchid,
                        idtype = a.idtype,
                        idno = a.idno,
                        expirydate = a.expirydate,
                        dtcreated = a.dtcreated,
                        dtmodified = a.dtmodified,
                        createdby = a.createdby,
                        modifiedby = a.modifiedby,
                        phoneno = a.phoneno,
                        cardno = a.cardno,
                        placeofbirth = a.placeofbirth,
                        natureofwork = a.natureofwork,
                        permanentaddress = a.permanentaddress,
                        nationality = a.nationality,
                        companyoremployer = a.companyoremployer,
                        businessorprofession = a.businessorprofession,
                        govtidno = a.govtidno,
                        govtidtype = a.govtidtype,
                        branchcreated = a.branchcreated,
                        branchmodified = a.branchmodified,
                        mlcardno = a.mlcardno,

                        kycfront = kycfront,
                        kycback = kycback,
                        id1 = id1,
                        id2 = id2,
                        id3 = id3,
                        fingerprint = fingerprint,
                        imagefree1 = imagefree1,
                        imagefree2 = imagefree2,
                        imagefree3 = imagefree3,
                        customersimage = customersimage,
                        path = topPath,
                        path1 =  string.Format("{0}Content\\images\\defaultimage\\", AppDomain.CurrentDomain.BaseDirectory),

                        imgkycfront = FindImage(kycfront),
                        imgkycback = FindImage(kycback),
                        imgid1 = FindImage(id1),
                        imgid2 = FindImage(id2),
                        imgid3 = FindImage(id3),
                        imgfingerprint = FindImage(fingerprint),
                        imgimagefree1 = FindImage(imagefree1),
                        imgimagefree2 = FindImage(imagefree2),
                        imgimagefree3 = FindImage(imagefree3),
                        imgcustomersimage = FindImage(customersimage),
                    });
                }
                if (list.Count != 0)
                {
                    System.Web.HttpContext.Current.Session["custinfo"] = list;
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Empty", JsonRequestBehavior.AllowGet);
                }
            }
            catch { return Json("Error", JsonRequestBehavior.AllowGet); }

        }
        public ActionResult getCustomerHistory(string custid, string uname, string walletno, string fname, string mname, string lname)
        {
            var list = new List<CustomerServiceModel>();
            list = (List<CustomerServiceModel>) System.Web.HttpContext.Current.Session["custlist"];
            

            long count = 0;

            custid = custid.Replace('_', ' ');
            uname = uname.Replace('_', ' ');
            walletno = walletno.Replace('_', ' ');
            fname = fname.Replace('_', ' ');
            mname = mname.Replace('_', ' ');
            lname = lname.Replace('_', ' ');

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dtmerge = new DataTable();
            dt = cust.dtable();
            dt1 = cust.dtable();
            dtmerge = cust.dtablereport();

            OdbcCommand cmd = new OdbcCommand();
            try
            {
                using (OdbcConnection mycon = new OdbcConnection(conn.hiveconnection()))
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        for (int yrloop = 2013; yrloop <= DateTime.Now.Year; yrloop++)
                        {
                            for (int i = 1; i <= 63; i++)
                            {
                                cmd.CommandText = "";
                                CustomerSummary custsum = new CustomerSummary();
                                var txntype = custsum.GetTxnType(i);

                                if (list != null)
                                {
                                    var obj = (list.FirstOrDefault(a => a.lastName.Trim().ToUpper() == lname.ToString().Trim().ToUpper()
                     && a.middleName.Trim().ToUpper() == mname.ToString().Trim().ToUpper()
                     && a.custID.Trim().ToUpper() == custid.ToString().Trim().ToUpper()));
                                    
                                    switch (i)
                                    {
                                        case 1: count = Convert.ToInt64(obj.kpsocount); break;
                                        case 2: count = Convert.ToInt64(obj.kppocount); break;
                                        case 3: count = Convert.ToInt64(obj.kprfccount); break;
                                        case 4: count = Convert.ToInt64(obj.kprtscount); break;
                                        case 5: count = Convert.ToInt64(obj.kpcsocount); break;
                                        case 6: count = Convert.ToInt64(obj.kpcpocount); break;
                                        case 7: count = Convert.ToInt64(obj.walletsocount); break;
                                        case 8: count = Convert.ToInt64(obj.walletpocount); break;
                                        case 9: count = Convert.ToInt64(obj.walletrfccount); break;
                                        case 10: count = Convert.ToInt64(obj.walletrtscount); break;
                                        case 11: count = Convert.ToInt64(obj.walletcsocount); break;
                                        case 12: count = Convert.ToInt64(obj.walletcpocount); break;
                                        case 13: count = Convert.ToInt64(obj.walletbpcount); break;
                                        case 14: count = Convert.ToInt64(obj.walleteloadcount); break;
                                        case 15: count = Convert.ToInt64(obj.walletcorppocount); break;
                                        case 16: count = Convert.ToInt64(obj.expresssocount); break;
                                        case 17: count = Convert.ToInt64(obj.expresspocount); break;
                                        case 18: count = Convert.ToInt64(obj.expressrfccount); break;
                                        case 19: count = Convert.ToInt64(obj.expressrtscount); break;
                                        case 20: count = Convert.ToInt64(obj.expresscsocount); break;
                                        case 21: count = Convert.ToInt64(obj.expresscpocount); break;
                                        case 22: count = Convert.ToInt64(obj.expressbpcount); break;
                                        case 23: count = Convert.ToInt64(obj.expresseloadcount); break;
                                        case 24: count = Convert.ToInt64(obj.expresscorppocount); break;
                                        case 26: count = Convert.ToInt64(obj.apipocount); break;
                                        case 30: count = Convert.ToInt64(obj.apicpocount); break;
                                        case 31: count = Convert.ToInt64(obj.fusocount); break;
                                        case 32: count = Convert.ToInt64(obj.fupocount); break;
                                        case 33: count = Convert.ToInt64(obj.furfccount); break;
                                        case 34: count = Convert.ToInt64(obj.furtscount); break;
                                        case 35: count = Convert.ToInt64(obj.fucsocount); break;
                                        case 36: count = Convert.ToInt64(obj.fucpocount); break;
                                        case 37: count = Convert.ToInt64(obj.wscsocount); break;
                                        case 38: count = Convert.ToInt64(obj.wscpocount); break;
                                        case 39: count = Convert.ToInt64(obj.wscrfccount); break;
                                        case 40: count = Convert.ToInt64(obj.wscrtscount); break;
                                        case 41: count = Convert.ToInt64(obj.wsccsocount); break;
                                        case 42: count = Convert.ToInt64(obj.wsccpocount); break;
                                        case 43: count = Convert.ToInt64(obj.bpsocount); break;
                                        case 44: count = Convert.ToInt64(obj.bprfccount); break;
                                        case 46: count = Convert.ToInt64(obj.bpcsocount); break;
                                        case 47: count = Convert.ToInt64(obj.prendacount); break;
                                        case 48: count = Convert.ToInt64(obj.lukatcount); break;
                                        case 49: count = Convert.ToInt64(obj.renewcount); break;
                                        case 50: count = Convert.ToInt64(obj.reappraisecount); break;
                                        case 51: count = Convert.ToInt64(obj.globalsocount); break;
                                        case 52: count = Convert.ToInt64(obj.globalpocount); break;
                                        case 53: count = Convert.ToInt64(obj.globalrfccount); break;
                                        case 54: count = Convert.ToInt64(obj.globalrtscount); break;
                                        case 55: count = Convert.ToInt64(obj.globalcsocount); break;
                                        case 56: count = Convert.ToInt64(obj.globalcpocount); break;
                                        case 57: count = Convert.ToInt64(obj.layawaycount); break;
                                        case 58: count = Convert.ToInt64(obj.salescount); break;
                                        case 59: count = Convert.ToInt64(obj.tradeincount); break;
                                        case 60: count = Convert.ToInt64(obj.sblcount); break;
                                        case 61: count = Convert.ToInt64(obj.eloadcount); break;
                                        case 62: count = Convert.ToInt64(obj.insurancecount); break;
                                        case 63: count = Convert.ToInt64(obj.goodscount); break;
                                    }
                                    if (count != 0)
                                    {
                                        cmd.CommandText = string.Format(" SELECT custid,walletno,username,fname,mname,lname,emailadd,birthdate, " +
                                      " mobileno as contactno, address, gender,amount,charge,zone,senderbranch,receivername,sendername,receiverbranch,transdate,claimeddate, " +
                                      " txntype,kptn,refno,ptn,servicetype as category," +
                                      " operatorname,branch,accountname,otherdetails,currency,otherdetails" +
                                      " FROM  customerservicedb.{0}{1} " +
                                      " where  lower(custid)='{2}' and lower(lname)='{3}' " +
                                      " and lower(mname) = '{4}' " +
                                      " and lower(fname) = '{5}' ",
                                      txntype.ToUpper(), yrloop, custid.ToLower().Trim(), lname.ToLower().Trim(), mname.ToLower().Trim(), fname.ToLower().Trim(),
                                      walletno.ToLower().Trim(), uname.ToLower().Trim());
                                    }
                                }
                                else {
                                    cmd.CommandText = string.Format(" SELECT custid,walletno,username,fname,mname,lname,emailadd,birthdate, " +
                                      " mobileno as contactno, address, gender,amount,charge,zone,senderbranch,receivername,sendername,receiverbranch,transdate,claimeddate, " +
                                      " txntype,kptn,refno,ptn,servicetype as category," +
                                      " operatorname,branch,accountname,otherdetails,currency,otherdetails" +
                                      " FROM  customerservicedb.{0}{1} " +
                                      " where  lower(custid)='{2}' and lower(lname)='{3}' " +
                                      " and lower(mname) = '{4}' " +
                                      " and lower(fname) = '{5}' ",
                                      txntype.ToUpper(), yrloop, custid.ToLower().Trim(), lname.ToLower().Trim(), mname.ToLower().Trim(), fname.ToLower().Trim(),
                                      walletno.ToLower().Trim(), uname.ToLower().Trim());
                                }
                                if (cmd.CommandText !="") {
                                    cmd.CommandTimeout = 0;
                                    using (OdbcDataReader Reader = cmd.ExecuteReader())
                                    {
                                        if (Reader.HasRows)
                                        {
                                            dt.Clear();
                                            dt.Load(Reader);
                                            dt1.Merge(dt);
                                            Reader.Close();
                                        }
                                    }
                                }
                            }
                        }
                        mycon.Close();

                        foreach (DataRow row in dt1.Rows)
                        {
                            DataRow newrow = dtmerge.NewRow();
                            newrow["custid"] = row["custid"].ToString();
                            newrow["lname"] = row["lname"].ToString();
                            newrow["mname"] = row["mname"].ToString();
                            newrow["fname"] = row["fname"].ToString();
                            newrow["address"] = row["address"].ToString();
                            newrow["emailadd"] = row["emailadd"].ToString();
                            newrow["contactno"] = row["contactno"].ToString();

                            newrow["date"] = row["claimeddate"].ToString() != "0000-00-00 00:00:00" ? row["claimeddate"].ToString() : row["transdate"].ToString();
                            newrow["txnno"] = row["ptn"].ToString() != "" ? row["ptn"].ToString() : (row["category"].ToString() == "CORPORATE" || row["txntype"].ToString() == "CORPPO" ? row["refno"].ToString() : row["kptn"].ToString());
                            newrow["senderreceiver"] = (row["txntype"].ToString() == "PAYOUT" || row["category"].ToString() == "BILLSPAY" ? row["sendername"].ToString() : row["receivername"].ToString());
                            newrow["category"] = row["category"].ToString() == "MONEY TRANSFER" ? "DOMESTIC" : row["category"].ToString();
                            newrow["txntype"] = row["txntype"].ToString();
                            newrow["currency"] = row["currency"].ToString();
                            newrow["amount"] = row["amount"].ToString();
                            newrow["charge"] = row["charge"].ToString();
                            newrow["description"] = (row["category"].ToString() == "BILLSPAY" ? row["otherdetails"].ToString() : (row["category"].ToString() == "MONEY CHANGER" ? row["currency"].ToString() : ""));
                            newrow["partner"] = row["accountname"].ToString() == "" ? row["partner"].ToString() : row["accountname"].ToString();
                            newrow["operator"] = row["operatorname"].ToString();
                            newrow["branch"] = row["branch"].ToString();

                            newrow["kpsocount"] = newrow["category"].ToString() == "DOMESTIC" && row["txntype"].ToString() == "SENDOUT" ? (newrow["kpsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["kpsocount"])) + 1 : 0;
                            newrow["kppocount"] = newrow["category"].ToString() == "DOMESTIC" && row["txntype"].ToString() == "PAYOUT" ? (newrow["kppocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["kppocount"])) + 1 : 0;
                            newrow["kprfccount"] = newrow["category"].ToString() == "DOMESTIC" && row["txntype"].ToString() == "REQUEST FOR CHANGE" ? (newrow["kprfccount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["kprfccount"])) + 1 : 0;
                            newrow["kprtscount"] = newrow["category"].ToString() == "DOMESTIC" && row["txntype"].ToString() == "RETURN TO SENDER" ? (newrow["kprtscount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["kprtscount"])) + 1 : 0;
                            newrow["kpcsocount"] = newrow["category"].ToString() == "DOMESTIC" && row["txntype"].ToString() == "CANCEL SENDOUT" ? (newrow["kpcsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["kpcsocount"])) + 1 : 0;
                            newrow["kpcpocount"] = newrow["category"].ToString() == "DOMESTIC" && row["txntype"].ToString() == "WRONG PAYOUT" ? (newrow["kpcpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["kpcpocount"])) + 1 : 0;
                            newrow["walletsocount"] = newrow["category"].ToString() == "WALLET" && row["txntype"].ToString() == "SENDOUT" ? (newrow["walletsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["walletsocount"])) + 1 : 0;
                            newrow["walletpocount"] = newrow["category"].ToString() == "WALLET" && row["txntype"].ToString() == "PAYOUT" ? (newrow["walletpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["walletpocount"])) + 1 : 0;
                            newrow["walletrfccount"] = newrow["category"].ToString() == "WALLET" && row["txntype"].ToString() == "REQUEST FOR CHANGE" ? (newrow["walletrfccount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["walletrfccount"])) + 1 : 0;
                            newrow["walletrtscount"] = newrow["category"].ToString() == "WALLET" && row["txntype"].ToString() == "RETURN TO SENDER" ? (newrow["walletrtscount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["walletrtscount"])) + 1 : 0;
                            newrow["walletcsocount"] = newrow["category"].ToString() == "WALLET" && row["txntype"].ToString() == "CANCEL SENDOUT" ? (newrow["walletcsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["walletcsocount"])) + 1 : 0;
                            newrow["walletcpocount"] = newrow["category"].ToString() == "WALLET" && row["txntype"].ToString() == "WRONG PAYOUT" ? (newrow["walletcpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["walletcpocount"])) + 1 : 0;
                            newrow["walletbpcount"] = newrow["category"].ToString() == "WALLET" && row["txntype"].ToString() == "BILLSPAY" ? (newrow["walletbpcount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["walletbpcount"])) + 1 : 0;
                            newrow["walleteloadcount"] = newrow["category"].ToString() == "WALLET" && row["txntype"].ToString() == "ELOAD" ? (newrow["walleteloadcount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["walleteloadcount"])) + 1 : 0;
                            newrow["walletcorppocount"] = newrow["category"].ToString() == "WALLET" && row["txntype"].ToString() == "CORPORATE PAYOUT" ? (newrow["walletcorppocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["walletcorppocount"])) + 1 : 0;
                            newrow["expresssocount"] = newrow["category"].ToString() == "EXPRESS" && row["txntype"].ToString() == "SENDOUT" ? (newrow["expresssocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["expresssocount"])) + 1 : 0;
                            newrow["expresspocount"] = newrow["category"].ToString() == "EXPRESS" && row["txntype"].ToString() == "PAYOUT" ? (newrow["expresspocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["expresspocount"])) + 1 : 0;
                            newrow["expressrfccount"] = newrow["category"].ToString() == "EXPRESS" && row["txntype"].ToString() == "REQUEST FOR CHANGE" ? (newrow["expressrfccount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["expressrfccount"])) + 1 : 0;
                            newrow["expressrtscount"] = newrow["category"].ToString() == "EXPRESS" && row["txntype"].ToString() == "RETURN TO SENDER" ? (newrow["expressrtscount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["expressrtscount"])) + 1 : 0;
                            newrow["expresscsocount"] = newrow["category"].ToString() == "EXPRESS" && row["txntype"].ToString() == "CANCEL SENDOUT" ? (newrow["expresscsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["expresscsocount"])) + 1 : 0;
                            newrow["expresscpocount"] = newrow["category"].ToString() == "EXPRESS" && row["txntype"].ToString() == "WRONG PAYOUT" ? (newrow["expresscpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["expresscpocount"])) + 1 : 0;
                            newrow["expressbpcount"] = newrow["category"].ToString() == "EXPRESS" && row["txntype"].ToString() == "BILLSPAY" ? (newrow["expressbpcount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["expressbpcount"])) + 1 : 0;
                            newrow["expresseloadcount"] = newrow["category"].ToString() == "EXPRESS" && row["txntype"].ToString() == "ELOAD" ? (newrow["expresseloadcount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["expresseloadcount"])) + 1 : 0;
                            newrow["expresscorppocount"] = newrow["category"].ToString() == "EXPRESS" && row["txntype"].ToString() == "CORPORATE PAYOUT" ? (newrow["expresscorppocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["expresscorppocount"])) + 1 : 0;
                            newrow["globalsocount"] = newrow["category"].ToString() == "GLOBAL" && row["txntype"].ToString() == "SENDOUT" ? (newrow["globalsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["globalsocount"])) + 1 : 0;
                            newrow["globalpocount"] = newrow["category"].ToString() == "GLOBAL" && row["txntype"].ToString() == "PAYOUT" ? (newrow["globalpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["globalpocount"])) + 1 : 0;
                            newrow["globalrfccount"] = newrow["category"].ToString() == "GLOBAL" && row["txntype"].ToString() == "REQUEST FOR CHANGE" ? (newrow["globalrfccount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["globalrfccount"])) + 1 : 0;
                            newrow["globalrtscount"] = newrow["category"].ToString() == "GLOBAL" && row["txntype"].ToString() == "RETURN TO SENDER" ? (newrow["globalrtscount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["globalrtscount"])) + 1 : 0;
                            newrow["globalcsocount"] = newrow["category"].ToString() == "GLOBAL" && row["txntype"].ToString() == "CANCEL SENDOUT" ? (newrow["globalcsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["globalcsocount"])) + 1 : 0;
                            newrow["globalcpocount"] = newrow["category"].ToString() == "GLOBAL" && row["txntype"].ToString() == "WRONG PAYOUT" ? (newrow["globalcpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["globalcpocount"])) + 1 : 0;
                            newrow["apipocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "PAYOUT" ? (newrow["apipocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["apipocount"])) + 1 : 0;
                            newrow["apicpocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "WRONG PAYOUT" ? (newrow["apicpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["apicpocount"])) + 1 : 0;
                            newrow["fusocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "SENDOUT" ? (newrow["fusocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["fusocount"])) + 1 : 0;
                            newrow["fupocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "PAYOUT" ? (newrow["fupocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["fupocount"])) + 1 : 0;
                            newrow["furfccount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "REQUEST FOR CHANGE" ? (newrow["furfccount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["furfccount"])) + 1 : 0;
                            newrow["furtscount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "RETURN TO SENDER" ? (newrow["furtscount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["furtscount"])) + 1 : 0;
                            newrow["fucsocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "CANCEL SENDOUT" ? (newrow["fucsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["fucsocount"])) + 1 : 0;
                            newrow["fucpocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "WRONG PAYOUT" ? (newrow["fucpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["fucpocount"])) + 1 : 0;
                            newrow["wscsocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "SENDOUT" ? (newrow["wscsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["wscsocount"])) + 1 : 0;
                            newrow["wscpocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "PAYOUT" ? (newrow["wscpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["wscpocount"])) + 1 : 0;
                            newrow["wscrfccount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "REQUEST FOR CHANGE" ? (newrow["wscrfccount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["wscrfccount"])) + 1 : 0;
                            newrow["wscrtscount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "RETURN TO SENDER" ? (newrow["wscrtscount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["wscrtscount"])) + 1 : 0;
                            newrow["wsccsocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "CANCEL SENDOUT" ? (newrow["wsccsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["wsccsocount"])) + 1 : 0;
                            newrow["wsccpocount"] = newrow["category"].ToString() == "CORPORATE" && row["txntype"].ToString() == "WRONG PAYOUT" ? (newrow["wsccpocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["wsccpocount"])) + 1 : 0;
                            newrow["bpsocount"] = newrow["category"].ToString() == "BILLSPAY" && row["txntype"].ToString() == "SENDOUT" ? (newrow["bpsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["bpsocount"])) + 1 : 0;
                            newrow["bprfccount"] = newrow["category"].ToString() == "BILLSPAY" && row["txntype"].ToString() == "CHANGE DETAILS" ? (newrow["bprfccount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["bprfccount"])) + 1 : 0;
                            newrow["bpcsocount"] = newrow["category"].ToString() == "BILLSPAY" && row["txntype"].ToString() == "CANCEL" ? (newrow["bpcsocount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["bpcsocount"])) + 1 : 0;
                            newrow["prendacount"] = newrow["category"].ToString() == "QCL" && row["txntype"].ToString() == "PRENDA" ? (newrow["prendacount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["prendacount"])) + 1 : 0;
                            newrow["lukatcount"] = newrow["category"].ToString() == "QCL" && row["txntype"].ToString() == "LUKAT" ? (newrow["lukatcount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["lukatcount"])) + 1 : 0;
                            newrow["renewcount"] = newrow["category"].ToString() == "QCL" && row["txntype"].ToString() == "RENEW" ? (newrow["renewcount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["renewcount"])) + 1 : 0;
                            newrow["reappraisecount"] = newrow["category"].ToString() == "QCL" && row["txntype"].ToString() == "REAPPRAISE" ? (newrow["reappraisecount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["reappraisecount"])) + 1 : 0;
                            newrow["layawaycount"] = newrow["category"].ToString() == "JEWELLERS" && row["txntype"].ToString() == "LAYAWAY" ? (newrow["layawaycount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["layawaycount"])) + 1 : 0;
                            newrow["salescount"] = newrow["category"].ToString() == "JEWELLERS" && row["txntype"].ToString() == "SALES" ? (newrow["salescount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["salescount"])) + 1 : 0;
                            newrow["tradeincount"] = newrow["category"].ToString() == "JEWELLERS" && row["txntype"].ToString() == "TRADEIN" ? (newrow["tradeincount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["tradeincount"])) + 1 : 0;
                            newrow["sblcount"] = newrow["category"].ToString() == "SBL" && row["txntype"].ToString() == "SBL" ? (newrow["sblcount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["sblcount"])) + 1 : 0;
                            newrow["eloadcount"] = newrow["category"].ToString() == "SBL" && row["txntype"].ToString() == "ELOAD" ? (newrow["eloadcount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["eloadcount"])) + 1 : 0;
                            newrow["insurancecount"] = newrow["category"].ToString() == "SBL" && row["txntype"].ToString() == "INSURANCE" ? (newrow["insurancecount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["insurancecount"])) + 1 : 0;
                            newrow["goodscount"] = newrow["category"].ToString() == "SBL" && row["txntype"].ToString() == "GOODS" ? (newrow["goodscount"] == DBNull.Value ? 0 : Convert.ToInt64(newrow["goodscount"])) + 1 : 0;


                            dtmerge.Rows.Add(newrow);
                        }

                        if (dtmerge.Rows.Count != 0)
                        {
                            System.Web.HttpContext.Current.Session["custhistory"] = dtmerge;
                            return Json("Success", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("Empty", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cust.WriteToFile("Error in Generating Customer Summary : " + ex.ToString());
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
       
        [HttpGet]
        public ActionResult ExportSummary()
        {
            DataTable dt = new DataTable();
            try
            {
                var operatorname = System.Web.HttpContext.Current.Session["userfullname"];
                List<CustomerServiceModel> custreport = (List<CustomerServiceModel>)System.Web.HttpContext.Current.Session["custlist"];
                if (custreport == null) { cust.WriteToFile("Session Expired"); return RedirectToAction("Index", "Logout"); }
                ListtoDataTableConverter converter = new ListtoDataTableConverter();

                 dt =  converter.ToDataTable(custreport);  
                var ReportLocation = String.Format("{0}Reports\\SummaryReport.rpt", AppDomain.CurrentDomain.BaseDirectory);
                ReportDocument rpt = new ReportDocument();
                rpt.Load(ReportLocation);
                rpt.Refresh();
                rpt.SetDataSource(dt);
                rpt.SetParameterValue(0, DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));
                rpt.SetParameterValue(1, operatorname == null ? "" : operatorname);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Customer Transaction Summary Report");
                rpt.Close();
                rpt.Dispose();
                //System.Web.HttpContext.Current.Session.Clear();
            }
            catch (Exception ex) { cust.WriteToFile("Export Report: " + ex.ToString()); }
            return new EmptyResult();
        }
        [HttpGet]
        public ActionResult ExportBOSSummary()
        {
            DataTable dt = new DataTable();
            try
            {
                var operatorname = System.Web.HttpContext.Current.Session["userfullname"];
                List<CustomerServiceModel> custreport = (List<CustomerServiceModel>)System.Web.HttpContext.Current.Session["custlist"];
                if (custreport == null) { cust.WriteToFile("Session Expired"); return RedirectToAction("Index", "Logout"); }
                ListtoDataTableConverter converter = new ListtoDataTableConverter();

                dt = converter.ToDataTable(custreport);
                var ReportLocation = String.Format("{0}Reports\\SummaryBOSReport.rpt", AppDomain.CurrentDomain.BaseDirectory);
                ReportDocument rpt = new ReportDocument();
                rpt.Load(ReportLocation);
                rpt.Refresh();
                rpt.SetDataSource(dt);
                rpt.SetParameterValue(0, DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));
                rpt.SetParameterValue(1, operatorname == null ? "" : operatorname);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Customer Transaction Summary Report");
                rpt.Close();
                rpt.Dispose();
                //System.Web.HttpContext.Current.Session.Clear();
            }
            catch (Exception ex) { cust.WriteToFile("Export Report: " + ex.ToString()); }
            return new EmptyResult();
        }
        [HttpGet]
        public ActionResult ExportKYC()
        {
            DataTable dt = new DataTable();
            try
            {
                var operatorname = System.Web.HttpContext.Current.Session["userfullname"];
                List<CustomerServiceModel> custinfo = (List<CustomerServiceModel>)System.Web.HttpContext.Current.Session["custinfo"];
                if (custinfo == null) { cust.WriteToFile("Session Expired"); return RedirectToAction("Index", "Logout"); }
                ListtoDataTableConverter converter = new ListtoDataTableConverter();

                dt = converter.ToDataTable(custinfo);
                var ReportLocation = String.Format("{0}Reports\\KYCReport.rpt", AppDomain.CurrentDomain.BaseDirectory);
                ReportDocument rpt = new ReportDocument();
                rpt.Load(ReportLocation);
                rpt.Refresh();
                rpt.SetDataSource(dt);
                rpt.SetParameterValue(0, DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));
                rpt.SetParameterValue(1, (operatorname == null?"":operatorname));
                string imgpath = string.Format("{0}Content\\images\\customerimages\\", AppDomain.CurrentDomain.BaseDirectory);
                rpt.SetParameterValue(2, imgpath);
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Customer KYC Information");
                rpt.Close();
                rpt.Dispose();
                //System.Web.HttpContext.Current.Session.Clear();
            }
            catch (Exception ex) { cust.WriteToFile("Export Report: " + ex.ToString()); }
            return new EmptyResult();
        }
        [HttpGet]
        public ActionResult ExportHistory()
        {
            try
            {
                var operatorname = System.Web.HttpContext.Current.Session["userfullname"];
                DataTable dt = new DataTable();
                dt = cust.dtablereport();
                dt = (DataTable)System.Web.HttpContext.Current.Session["custhistory"];
                var ReportLocation = String.Format("{0}Reports\\HistoryReport.rpt", AppDomain.CurrentDomain.BaseDirectory);
                ReportDocument rpt = new ReportDocument();
                rpt.Load(ReportLocation);
                rpt.Refresh();
                rpt.SetDataSource(dt);
                rpt.SetParameterValue(1, operatorname == null ? "" : operatorname);
                rpt.SetParameterValue(0, DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss"));
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "Customer Transaction History Report");
                rpt.Close();
                rpt.Dispose();
                //System.Web.HttpContext.Current.Session.Clear();
            }
            catch (Exception ex) { cust.WriteToFile("Export Report: " + ex.ToString()); }
            return new EmptyResult();
        }


        public string DisplayImages(byte[] img, string filename)
        {
            string path = string.Empty;
            using (MemoryStream ms = new MemoryStream(img, 0, img.Length))
            {
                ms.Write(img, 0, img.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                string savepath = string.Format("~/Content/images/customerimages/{0}", filename);
                path = Server.MapPath(savepath);
                image.Save(path);
            }
            return path;
        }
        public byte[] FindImage(string path)
        {
            byte[] imgbyte = null;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    // initialise the binary reader from file streamobject 
                    BinaryReader br = new BinaryReader(fs);
                    // define the byte array of filelength 
                    imgbyte = new byte[fs.Length + 1];
                    // read the bytes from the binary reader 
                    imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                }
            }
            catch { }
            return imgbyte;
        }
        public class ListtoDataTableConverter
        {

            public DataTable ToDataTable<T>(List<T> items)
            {

                DataTable dataTable = new DataTable(typeof(T).Name);

                //Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {

                    //Setting column names as Property names

                    dataTable.Columns.Add(prop.Name);

                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {

                        //inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);

                    }

                    dataTable.Rows.Add(values);

                }

                //put a breakpoint here and check datatable

                return dataTable;

            }

        }
    }
}