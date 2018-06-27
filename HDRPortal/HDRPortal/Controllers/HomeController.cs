using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HDRPortal.Models;
using System.Data;
using System.Globalization;
using System.Configuration;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using AccessControl;
using System.Collections;
using System.IO;


namespace HDRPortal.Controllers
{
    public class HomeController : Controller
    {
        AccessControl.AccessControl ac = new AccessControl.AccessControl();
        MySqlCommand cmd = new MySqlCommand();
        MySqlConnection con = new MySqlConnection();
        DataTable dt1 = new DataTable();
        AcessControlModel ACmodel = new AcessControlModel();
        DataTable dtable = new DataTable();
        DataTable dtmerge = new DataTable();
        DataRow datarow = null;
        public int id = 0;
        UserModel usermodel = (UserModel)System.Web.HttpContext.Current.Session["UserModel"];
        MySqlTransaction tran;
        public ActionResult Index()
        {
            try {
                if (Session["fname"].ToString() != string.Empty && Session["userlogin"].ToString() != string.Empty && usermodel != null)
                {
                    ACmodel.Category = category();
                    ACmodel.Region = region();
                    ACmodel.Area = area("");
                    ACmodel.Branch = branch("", "");
                    ACmodel.user = Session["fname"].ToString().Trim();
                    ACmodel.role = Session["userrole"].ToString().Trim();
                    ACmodel.mappath = ConfigurationManager.AppSettings["MAP"] + "?user=" + System.Web.HttpContext.Current.Session["userlogin"] + "&pass=" + System.Web.HttpContext.Current.Session["userpass"];
                    ACmodel.trendpath = ConfigurationManager.AppSettings["TREND"] + "?user=" + System.Web.HttpContext.Current.Session["userlogin"] + "&pass=" + System.Web.HttpContext.Current.Session["userpass"];
                    ACmodel.customerstatpath = ConfigurationManager.AppSettings["CUSTOMERSTAT"] + "?UserName=" + System.Web.HttpContext.Current.Session["userlogin"] + "&UserPass=" + System.Web.HttpContext.Current.Session["userpass"];
                    ACmodel.offlinemonitoringpath = ConfigurationManager.AppSettings["OFFLINEMAP"] + "?user=" + System.Web.HttpContext.Current.Session["userlogin"] + "&pass=" + System.Web.HttpContext.Current.Session["userpass"];
                    ACmodel.customerservicepath = ConfigurationManager.AppSettings["CUSTOMERSERVICE"] + "?user=" + System.Web.HttpContext.Current.Session["userlogin"] + "&pass=" + System.Web.HttpContext.Current.Session["userpass"];
                    return View(ACmodel);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Home Index Error : " + ex.ToString());
                return RedirectToAction("Index", "Login"); }
            
        }
        public SelectList category()
        {
            var category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "- Please Select Category -", Value = "", Selected = true });
            category.Add(new SelectListItem { Text = "Access Control", Value = "activeusers", Selected = true });
            category.Add(new SelectListItem { Text = "Branch Profile", Value = "branchprofile", Selected = true });
            category.Add(new SelectListItem { Text = "Behavior Score", Value = "behaviorscore", Selected = true });
            return new SelectList(category, "Value", "Text");
        }
        public SelectList region()
        { 
        var item = new List<SelectListItem>();

        List<SelectListItem> regionList = new List<SelectListItem>();
        regionList.Add(new SelectListItem { Text = "- Please Select Region -", Value = "", Selected = true });
        try
        {
            string domesticB = ConfigurationManager.ConnectionStrings["domesticB"].ConnectionString;
            con = new MySqlConnection(domesticB);
            con.Open();
            cmd = new MySqlCommand(string.Format("SELECT regionname,IF(regionname='HO' AND (regioncode IS NULL OR regioncode =''),50,regioncode) AS regioncode FROM kpusers.branches WHERE STATUS=1 and zonecode='{0}' group by regionname order by regionname asc;", usermodel.zonecode), con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                regionList.Add(new SelectListItem { Text = reader["regionname"].ToString().ToUpper().Trim(), Value = reader["regioncode"].ToString() });
            }
            reader.Close();

        }
        catch (Exception ex)
        {
            ACmodel.WriteToFile("Getting Region Error : " + ex.ToString()); 
            throw;
        }
        item = regionList;
        return new SelectList(item, "Value", "Text");
        }
        public SelectList area(string rcode)
       
        {
            var areaname = new List<SelectListItem>();
            areaname.Add(new SelectListItem { Text = "- Please Select Area -", Value = "", Selected = true });
            try
            {
                string domesticB = ConfigurationManager.ConnectionStrings["domesticB"].ConnectionString;
                con = new MySqlConnection(domesticB);
                con.Open();
                cmd = new MySqlCommand(String.Format("SELECT areacode,areaname FROM kpusers.branches WHERE zonecode='{0}' AND regioncode='{1}' AND STATUS=1 GROUP BY areaname  ORDER BY areaname;", usermodel.zonecode, rcode), con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    areaname.Add(new SelectListItem { Text = reader["areaname"].ToString().ToUpper(), Value = reader["areacode"].ToString() });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Getting Area Error : " + ex.ToString());
                throw;
            }
            return new SelectList(areaname, "Value", "Text");
        }
        public SelectList branch(string rcode, string aname)
        {
            var branchname = new List<SelectListItem>();
            branchname.Add(new SelectListItem { Text = "- Please Select Branch -", Value = "", Selected = true });
            try
            {
                string domesticB = ConfigurationManager.ConnectionStrings["domesticB"].ConnectionString;
                con = new MySqlConnection(domesticB);
                con.Open();
                cmd = new MySqlCommand(String.Format("SELECT branchcode,branchname FROM kpusers.branches WHERE areaname='{0}' and zonecode='{1}' AND regioncode='{2}' and status=1 ORDER BY branchname;", aname, usermodel.zonecode, rcode), con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    branchname.Add(new SelectListItem { Text = reader["branchname"].ToString().ToUpper(), Value = reader["branchcode"].ToString() });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Getting Branch Error : " + ex.ToString());
                throw;
            }
            return new SelectList(branchname, "Value", "Text");
        }

        [HttpGet]
        public ActionResult getCode(string code, string name, string flag)
        {
            if (flag == "Area")
            {
                ACmodel.Area =   area(code);
                return Json(ACmodel.Area, JsonRequestBehavior.AllowGet);
            }
            else if (flag == "Branch")
            {
                ACmodel.Branch =   branch(code, name);
                return Json(ACmodel.Branch, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult insert_stat(string stat)
        {
            try
            {
                con = new MySqlConnection(ConfigurationManager.ConnectionStrings["domesticA"].ConnectionString);
                con.Open();
                tran = con.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = tran;
                cmd = new MySqlCommand(string.Format("INSERT INTO `kpforms`.`compliance`(STATUS,sysCreator,sysCreated) VALUES ({0},'{1}',NOW());", stat, usermodel.fullname), con);
                MySqlDataReader rd = cmd.ExecuteReader();
                rd.Close();
                tran.Commit();

            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Insert Stat Error : " + ex.ToString());
                tran.Rollback();
                throw;
            }

            return Json("Successfull", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Getstat()
        {
            List<AcessControlModel.cstatus> list = new List<AcessControlModel.cstatus>();
            try
            {
                con = new MySqlConnection(ConfigurationManager.ConnectionStrings["domesticB"].ConnectionString);
                con.Open();
                cmd = new MySqlCommand(string.Format("SELECT * FROM kpforms.compliance ORDER BY sysCreated DESC LIMIT 1;"), con);
                MySqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    rd.Read();
                    {
                        list.Add(new AcessControlModel.cstatus
                        {
                            status = rd["status"].ToString(),
                            syscreated = rd["sysCreated"].ToString()
                        });
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Get Stat Error : " + ex.ToString());
                throw;
            }
            ACmodel.lstatus = list;
            return Json(ACmodel.lstatus, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getUsers(string category,string rcode, string rname, string acode, string aname, string bcode, string bname)
        {
            usermodel.category = category;
            string barname = string.Empty;
            var zonename = getzonename(usermodel.zonecode);
            if (category == "activeusers")
            {
                try
                {
                    usermodel.rname = rname;
                    dtable = ACmodel.getusersdt();
                    dtable.Clear();
                    dtmerge = ACmodel.getusersdt();
                    dtmerge.Clear();

                    con = new MySqlConnection(ConfigurationManager.ConnectionStrings["domesticB"].ConnectionString);
                    con.Open();
                    cmd = new MySqlCommand(string.Format("CALL `accesscontrolgetactive`('{0}','{1}');", rcode, usermodel.zonecode), con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var mdate = reader["datemodified"] != System.DBNull.Value ? reader["datemodified"].ToString().Trim() : "0000-00-00 00:00:00";
                        var cdate = reader["datecreated"] != System.DBNull.Value ? reader["datecreated"].ToString().Trim() : "0000-00-00 00:00:00";
                        datarow = dtable.NewRow();
                        datarow["id"] = id++;
                        datarow["resourceid"] = reader["resourceid"] != System.DBNull.Value ? reader["resourceid"].ToString().Trim().ToUpper() : "-";
                        datarow["roleid"] = reader["roleid"] != System.DBNull.Value ? reader["roleid"].ToString().Trim().ToUpper() : "-";
                        datarow["userlogin"] = reader["userlogin"] != System.DBNull.Value ? reader["userlogin"].ToString().Trim().ToUpper() : "-";
                        datarow["lastname"] = reader["lastname"] != System.DBNull.Value ? reader["lastname"].ToString().Trim().ToUpper() : "-";
                        datarow["firstname"] = reader["firstname"] != System.DBNull.Value ? reader["firstname"].ToString().Trim().ToUpper() : "-";
                        datarow["middlename"] = reader["middlename"] != System.DBNull.Value ? reader["middlename"].ToString().Trim().ToUpper() : "-";
                        datarow["reliever"] = reader["reliever"] != System.DBNull.Value ? reader["reliever"].ToString().Trim().ToUpper() : "-";
                        datarow["origbranchcode"] = reader["origbranchcode"] != System.DBNull.Value ? reader["origbranchcode"].ToString().Trim().ToUpper() : "-";
                        datarow["origzonecode"] = reader["origzonecode"] != System.DBNull.Value ? reader["origzonecode"].ToString().Trim().ToUpper() : "-";
                        datarow["tempbranchcode"] = reader["tempbranchcode"] != System.DBNull.Value ? reader["tempbranchcode"].ToString().Trim().ToUpper() : "-";
                        datarow["tempzonecode"] = reader["tempzonecode"] != System.DBNull.Value ? reader["tempzonecode"].ToString().Trim().ToUpper() : "-";
                        datarow["zonecode"] = reader["zonecode"] != System.DBNull.Value ? reader["zonecode"].ToString().Trim().ToUpper() : "-";
                        datarow["regioncode"] = reader["regioncode"] != System.DBNull.Value ? reader["regioncode"].ToString().Trim().ToUpper() : "-";
                        datarow["areacode"] = reader["areacode"] != System.DBNull.Value ? reader["areacode"].ToString().Trim().ToUpper() : "-";
                        datarow["branchcode"] = reader["branchcode"] != System.DBNull.Value ? reader["branchcode"].ToString().Trim().ToUpper() : "-";
                        datarow["regionname"] = reader["regionname"] != System.DBNull.Value ? reader["regionname"].ToString().Trim().ToUpper() : "-";
                        datarow["areaname"] = reader["areaname"] != System.DBNull.Value ? reader["areaname"].ToString().Trim().ToUpper() : "-";
                        datarow["branchname"] = reader["branchname"] != System.DBNull.Value ? reader["branchname"].ToString().Trim().ToUpper() : "-";
                        datarow["datecreated"] = (reader["datecreated"] != System.DBNull.Value && cdate != "0000-00-00 00:00:00") ? Convert.ToDateTime(reader["datecreated"]).ToString("yyyy-MM-dd HH:mm:ss tt").Trim().ToUpper() : "-";
                        datarow["usercreator"] = reader["usercreator"] != System.DBNull.Value ? reader["usercreator"].ToString().Trim().ToUpper() : "-";
                        datarow["datemodified"] = (reader["datemodified"] != System.DBNull.Value && mdate != "0000-00-00 00:00:00") ? Convert.ToDateTime(reader["datemodified"]).ToString("yyyy-MM-dd HH:mm:ss tt").Trim().ToUpper() : "-";
                        datarow["usermodifier"] = reader["usermodifier"] != System.DBNull.Value ? reader["usermodifier"].ToString().Trim().ToUpper() : "-";
                        datarow["contactno"] = reader["contactno"] != System.DBNull.Value ? reader["contactno"].ToString().Trim().ToUpper() : "-";
                        datarow["emailadd"] = reader["emailadd"] != System.DBNull.Value ? reader["emailadd"].ToString().Trim().ToUpper() : "-";
                        datarow["userpassword"] = reader["userpassword"] != System.DBNull.Value ? reader["userpassword"].ToString().Trim().ToUpper() : "-";
                        dtable.Rows.Add(datarow);
                        dtable.AcceptChanges();
                        dtmerge.Merge(dtable);
                    }
                    reader.Close();
                    con.Close();

                    usermodel.dt = dtmerge;
                }
                catch (Exception ex)
                {
                    ACmodel.WriteToFile("Get Users Error : " + ex.ToString()); throw;
                }
                if (usermodel.dt.Rows.Count == 0) { return Json("", JsonRequestBehavior.AllowGet); }
                else { return Json("Successfull", JsonRequestBehavior.AllowGet); }
            }
            else if(category == "behaviorscore"){
                try {
                
                }
                catch (Exception ex)
                {
                    ACmodel.WriteToFile("Behavior Score Error : " + ex.ToString()); return Json("2, " + ex.ToString(), JsonRequestBehavior.AllowGet);
                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                string rpttitle = string.Empty;
                string reportpath = string.Empty;
                List<string> mergebranch = new List<string>();
                try
                {                    
                    usermodel.rname = rname;
                    string dir = String.Format("{0}Reports\\Export\\", AppDomain.CurrentDomain.BaseDirectory);
                    System.IO.DirectoryInfo myDirInfo = new DirectoryInfo(dir);
                    foreach (FileInfo file in myDirInfo.GetFiles())
                    {
                        if (file.Exists)
                        {
                            file.Refresh();
                            file.Delete(); 
                        } 
                    }
                    if (!System.IO.Directory.Exists(dir))
                    {
                        System.IO.Directory.CreateDirectory(dir);
                    }

                    if (rcode != null && rcode != "" && (acode == null || acode == "") && (bcode == null || bcode == "")) //by region
                    { 
                        barname = rname.Trim().Replace(' ', '-');
                        var GetAreaList = ac.GetAreaList(Convert.ToInt32(usermodel.zonecode), Convert.ToInt32(rcode));
                        if (GetAreaList.RespCode == 1)
                        {
                            foreach (DataRow row2 in ACmodel.ListToDataTable(GetAreaList.AreaList).Rows)
                            {
                                try
                                {
                                    var GetBranchList = ac.GetBranchList(Convert.ToInt32(usermodel.zonecode), Convert.ToInt32(rcode), row2[0].ToString());
                                    if (GetBranchList.RespCode == 1)
                                    {
                                        foreach (DataRow row1 in ACmodel.ListToDataTable(GetBranchList.BranchList).Rows)
                                        {
                                            List<string> mergereport = new List<string>();
                                            reportpath = getbranchinfo(category, rcode, rname, row2[0].ToString(), row2[1].ToString(), row1[0].ToString(), row1[1].ToString());
                                            if (reportpath != "") { mergereport.Add(reportpath); }
                                            reportpath = getbranchbandwidth(category, rcode, rname, row2[0].ToString(), row2[1].ToString(), row1[0].ToString(), row1[1].ToString());
                                            if (reportpath != "") { mergereport.Add(reportpath); }
                                            reportpath = getbranchasset(category, rcode, rname, row2[0].ToString(), row2[1].ToString(), row1[0].ToString(), row1[1].ToString());
                                            if (reportpath != "") { mergereport.Add(reportpath); }
                                            reportpath = getbranchIR(category, rcode, rname, row2[0].ToString(), row2[1].ToString(), row1[0].ToString(), row1[1].ToString());
                                            if (reportpath != "") { mergereport.Add(reportpath); }
                                            reportpath = getbranchWO(category, rcode, rname, row2[0].ToString(), row2[1].ToString(), row1[0].ToString(), row1[1].ToString());
                                            if (reportpath != "") { mergereport.Add(reportpath); }

                                            ACmodel.mergereportlist = mergereport;

                                            if (ACmodel.mergereportlist != null && ACmodel.mergereportlist.Count != 0)
                                            {
                                                string branchreport = String.Format("{0}Reports\\Export\\{1}-{2}.pdf", AppDomain.CurrentDomain.BaseDirectory, row1[1].ToString().Trim().Replace(' ', '-'), DateTime.Now.Ticks);
                                                ACmodel.MergePDF(branchreport, "mergereport");
                                                foreach (string filename in ACmodel.mergereportlist)
                                                {
                                                    if (System.IO.File.Exists(filename))
                                                    {
                                                        System.IO.File.Delete(filename);
                                                    }
                                                }

                                                mergebranch.Add(branchreport);
                                                ACmodel.mergebranchlist = mergebranch;
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ACmodel.WriteToFile(" Error : " + ex.ToString());
                                }
                            }
                        }
                    }
                    else if (rcode != null && rcode != "" && acode != null && acode != "" && (bcode == null || bcode == "")) //by area
                    { 
                        barname = aname.Trim().Replace(' ', '-');
                        try
                        {
                            var GetBranchList = ac.GetBranchList(Convert.ToInt32(usermodel.zonecode), Convert.ToInt32(rcode),acode);
                            if (GetBranchList.RespCode == 1)
                            {
                                foreach (DataRow row1 in ACmodel.ListToDataTable(GetBranchList.BranchList).Rows)
                                {
                                    List<string> mergereport = new List<string>();
                                    reportpath = getbranchinfo(category, rcode, rname, acode, aname, row1[0].ToString(), row1[1].ToString());
                                    if (reportpath != "") { mergereport.Add(reportpath); }
                                    reportpath = getbranchbandwidth(category, rcode, rname, acode, aname, row1[0].ToString(), row1[1].ToString());
                                    if (reportpath != "") { mergereport.Add(reportpath); }
                                    reportpath = getbranchasset(category, rcode, rname, acode, aname, row1[0].ToString(), row1[1].ToString());
                                    if (reportpath != "") { mergereport.Add(reportpath); }
                                    reportpath = getbranchIR(category, rcode, rname, acode, aname, row1[0].ToString(), row1[1].ToString());
                                    if (reportpath != "") { mergereport.Add(reportpath); }
                                    reportpath = getbranchWO(category, rcode, rname, acode, aname, row1[0].ToString(), row1[1].ToString());
                                    if (reportpath != "") { mergereport.Add(reportpath); }

                                    ACmodel.mergereportlist = mergereport;
                                    if (ACmodel.mergereportlist != null && ACmodel.mergereportlist.Count != 0)
                                    {
                                        string branchreport = String.Format("{0}Reports\\Export\\{1}-{2}.pdf", AppDomain.CurrentDomain.BaseDirectory, row1[1].ToString().Trim().Replace(' ', '-'), DateTime.Now.Ticks);
                                        ACmodel.MergePDF(branchreport, "mergereport");
                                        foreach (string filename in ACmodel.mergereportlist)
                                        {
                                            if (System.IO.File.Exists(filename))
                                            {
                                                System.IO.File.Delete(filename);
                                            }
                                        }

                                        mergebranch.Add(branchreport);
                                        ACmodel.mergebranchlist = mergebranch;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ACmodel.WriteToFile(" Error : " + ex.ToString());
                        }
                    }
                    else if (rcode != null && rcode != "" && acode != null && acode != "" && bcode != null && bcode != "") //by branch
                    { 
                        barname = bname.Trim().Replace(' ', '-');
                        List<string> mergereport = new List<string>();
                        reportpath = getbranchinfo(category, rcode, rname, acode, aname, bcode, bname);
                        if (reportpath != "") { mergereport.Add(reportpath); }
                        reportpath = getbranchbandwidth(category, rcode, rname, acode, aname, bcode, bname);
                        if (reportpath != "") { mergereport.Add(reportpath); }
                        reportpath = getbranchasset(category, rcode, rname, acode, aname, bcode, bname);
                        if (reportpath != "") { mergereport.Add(reportpath); }
                        reportpath = getbranchIR(category, rcode, rname, acode, aname, bcode, bname);
                        if (reportpath != "") { mergereport.Add(reportpath); }
                        reportpath = getbranchWO(category, rcode, rname, acode, aname, bcode, bname);
                        if (reportpath != "") { mergereport.Add(reportpath); }

                        ACmodel.mergereportlist = mergereport;

                        if (ACmodel.mergereportlist != null && ACmodel.mergereportlist.Count!=0) {
                            string branchreport = String.Format("{0}Reports\\Export\\{1}-{2}.pdf", AppDomain.CurrentDomain.BaseDirectory, bname.Trim().Replace(' ', '-'), DateTime.Now.Ticks);
                            ACmodel.MergePDF(branchreport, "mergereport");
                            foreach (string filename in ACmodel.mergereportlist)
                            {
                                if (System.IO.File.Exists(filename))
                                {
                                    System.IO.File.Delete(filename);
                                }
                            }
                            mergebranch.Add(branchreport);
                            ACmodel.mergebranchlist = mergebranch;
                        }
                    }
                    if (ACmodel.mergebranchlist != null && ACmodel.mergebranchlist.Count != 0)
                    {
                        rpttitle = String.Format("{0}Reports\\Export\\{1}-{2}-BranchProfileReport-{3}.pdf", AppDomain.CurrentDomain.BaseDirectory, zonename, barname, DateTime.Now.ToString("yyyy-MM-dd"));
                        ACmodel.MergePDF(rpttitle, "mergebranch");
                        foreach (string filename in ACmodel.mergebranchlist)
                        {
                            if (System.IO.File.Exists(filename))
                            {
                                System.IO.File.Delete(filename);
                            }
                        }
                        string reportfile = String.Format("{0}-{1}-BranchProfileReport-{2}.pdf", zonename, barname, DateTime.Now.ToString("yyyy-MM-dd"));
                        return Json(reportfile, JsonRequestBehavior.AllowGet);
                    }
                    else { 
                        if(System.Web.HttpContext.Current.Session["error"]==string.Empty || System.Web.HttpContext.Current.Session["error"]==null){return Json("2, No Records!", JsonRequestBehavior.AllowGet);}
                        else return Json("2, " + System.Web.HttpContext.Current.Session["error"], JsonRequestBehavior.AllowGet);}
                }
                catch (Exception ex)
                {
                    ACmodel.WriteToFile(" Error : " + ex.ToString()); return Json("2, " + ex.ToString(), JsonRequestBehavior.AllowGet);
                }
            }
        }

        public string getbranchinfo(string category, string rcode, string rname, string acode, string aname, string bcode, string bname)
        {
            string filename = string.Empty;
            try
            {
                dtable = ACmodel.getbranchprofiledt();
                dtable.Clear();
                dtmerge = ACmodel.getbranchprofiledt();
                dtmerge.Clear();

                var GetBranchProfile = ac.GetBranchProfile(bcode, Convert.ToInt32(usermodel.zonecode));
                if (GetBranchProfile.RespCode == 1)
                {
                    foreach (DataRow row in ACmodel.ListToDataTable(GetBranchProfile.Info).Rows)
                    {
                        datarow = dtable.NewRow();
                        datarow["id"] = id++;
                        try
                        {
                            //Branch Info
                            datarow["branchname"] = bname.ToUpper().Trim();
                            datarow["areaname"] = aname.ToUpper().Trim();
                            datarow["regionname"] = rname.Trim().ToUpper();
                            var GetBranchBasicData = ac.GetBranchBasicData(bcode, Convert.ToInt32(usermodel.zonecode));
                            if (GetBranchBasicData.RespCode == 1)
                            {
                                datarow["address"] = GetBranchBasicData.BC_address != null ? GetBranchBasicData.BC_address.Trim().ToUpper() : "-";
                                datarow["telno"] = GetBranchBasicData.BC_telno != null ? GetBranchBasicData.BC_telno.Trim().ToUpper() : "-";
                                datarow["celno"] = GetBranchBasicData.BC_contact != null ? GetBranchBasicData.BC_contact.Trim().ToUpper() : "-";
                                //PEOPLE
                                datarow["rmname"] = GetBranchBasicData.RM_name != null ? GetBranchBasicData.RM_name.Trim().ToUpper() : "-";
                                datarow["rmcontactno"] = GetBranchBasicData.RM_contact != null ? GetBranchBasicData.RM_contact.Trim().ToUpper() : "-";
                                datarow["amname"] = GetBranchBasicData.AM_name != null ? GetBranchBasicData.AM_name.Trim().ToUpper() : "-";
                                datarow["amcontactno"] = GetBranchBasicData.AM_contact != null ? GetBranchBasicData.AM_contact.Trim().ToUpper() : "-";
                                datarow["rctname"] = GetBranchBasicData.RCT != null ? GetBranchBasicData.RCT.Trim().ToUpper() : "-";
                                int loopteller = 1;
                                foreach (DataRow row3 in ACmodel.ListToDataTable(GetBranchBasicData.BC_employees).Rows)
                                {
                                    if (row3["RoleId"].ToString().Trim() == "KP-ABM")
                                    {
                                        datarow["abmname"] = row3["FullName"] != System.DBNull.Value ? row3["FullName"].ToString().Trim().ToUpper() : "-";
                                    }
                                    else if (row3["RoleId"].ToString().Trim() == "KP-BM")
                                    {
                                        datarow["bmname"] = row3["FullName"] != System.DBNull.Value ? row3["FullName"].ToString().Trim().ToUpper() : "-";
                                    }
                                    else if (row3["RoleId"].ToString().Trim() == "KP-TELLER")
                                    {
                                        datarow["teller" + loopteller] = row3["FullName"] != System.DBNull.Value ? row3["FullName"].ToString().Trim().ToUpper() : "-";
                                        loopteller++;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ACmodel.WriteToFile("Get Branch Info  Error : " + ex.ToString()); System.Web.HttpContext.Current.Session["error"] = ex.ToString();
                        }
                        //Computer Assets
                        if (row["StationCode"].ToString() != "_" && row["StationCode"].ToString() != "")
                        {
                            datarow["StationCode"] = row["StationCode"] != System.DBNull.Value ? "STATION " + row["StationCode"].ToString().Trim().ToUpper() : "-";
                            datarow["Processor"] = row["Processor"] != System.DBNull.Value ? row["Processor"].ToString().Trim().ToUpper() : "-";
                            datarow["HardDrives"] = row["HardDrives"] != System.DBNull.Value ? row["HardDrives"].ToString().Trim().ToUpper() : "-";
                            datarow["ComputerName"] = row["ComputerName"] != System.DBNull.Value ? row["ComputerName"].ToString().Trim().ToUpper() : "-";
                            datarow["OS"] = row["OS"] != System.DBNull.Value ? row["OS"].ToString().Trim().ToUpper() : "-";
                            datarow["Memory"] = row["Memory"] != System.DBNull.Value ? row["Memory"].ToString().Trim().ToUpper() : "-";
                            datarow["OSProdKey"] = row["OSProdKey"] != System.DBNull.Value ? row["OSProdKey"].ToString().Trim().ToUpper() : "-";
                            datarow["OSSerialKey"] = row["OSSerialKey"] != System.DBNull.Value ? row["OSSerialKey"].ToString().Trim().ToUpper() : "-";
                            var stationcode = row["StCode"] != System.DBNull.Value ? row["StCode"].ToString().Trim().ToUpper() : "-";
                            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["cmms"].ConnectionString);
                            con.Open();
                            cmd = new MySqlCommand(string.Format("select application,version from `cmms`.`branch_applications` where stationcode='{0}' and bcode='{1}' group by application,version;", stationcode, bcode), con);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                int i = 1;
                                while (reader.Read())
                                {
                                    datarow["app" + i] = reader["application"] != System.DBNull.Value ? reader["application"].ToString().Trim().ToUpper() + " " + reader["version"].ToString().Trim().ToUpper() : "";
                                    i++;
                                }
                            }
                            reader.Close();
                            datarow["compasset"] = "1";
                        }
                        else { datarow["compasset"] = "0"; }
                        dtable.Rows.Add(datarow);
                        dtable.AcceptChanges();
                        dtmerge.Merge(dtable);
                    }
                    filename = generatebranchprofilereport(dtmerge, "BranchProfileReport");
                }
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile(" Error : " + ex.ToString()); System.Web.HttpContext.Current.Session["error"] = ex.ToString();
            }
            return filename;
        }
        public string getbranchbandwidth(string category, string rcode, string rname, string acode, string aname, string bcode, string bname)
        {
            string filename = string.Empty;
            try
            {
                dtable = ACmodel.getbranchprofiledt();
                dtable.Clear();
                dtmerge = ACmodel.getbranchprofiledt();
                dtmerge.Clear();
                var zonename = getzonename(usermodel.zonecode);
                con = new MySqlConnection(ConfigurationManager.ConnectionStrings["cmms"].ConnectionString);
                con.Open();
                cmd = new MySqlCommand(string.Format("SELECT b.isp_name,a.bandwidth,c.bc_code,c.zone_code,b.sys_created FROM `cmms`.`cmms_entry_isp` a" +
                                                    " INNER JOIN `cmms`.`cmms_isp_list` b ON b.isp_code=a.isp_code" +
                                                    " INNER JOIN `cmms`.`cmms_entry_masterheader` c ON c.asset_inv_no=a.asset_inv_no" +
                                                    " WHERE c.bc_code='{0}' AND c.zone_code='{1}' and a.bandwidth!='' and a.bandwidth is not null;", bcode, zonename.Trim()), con);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        datarow = dtable.NewRow();
                        datarow["id"] = id++;
                        datarow["branchname"] = bname.ToUpper().Trim();
                        datarow["areaname"] = aname.ToUpper().Trim();
                        datarow["regionname"] = rname.Trim().ToUpper();
                        datarow["bwispname"] = reader["isp_name"] != System.DBNull.Value ? reader["isp_name"].ToString().Trim().ToUpper() : "";
                        datarow["bwbandwidth"] = reader["bandwidth"] != System.DBNull.Value ? reader["bandwidth"].ToString().Trim().ToUpper() : "";
                        datarow["bwdate"] = reader["sys_created"] != System.DBNull.Value ? reader["sys_created"].ToString().Trim().ToUpper() : "";
                        dtable.Rows.Add(datarow);
                        dtable.AcceptChanges();
                        dtmerge.Merge(dtable);
                    }
                    reader.Close();
                    filename = generatebranchprofilereport(dtmerge, "BranchBandwidthReport");
                }
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Branch BAndWidth Error : " + ex.ToString()); System.Web.HttpContext.Current.Session["error"] = ex.ToString();
            }
            return filename;
        }
        public string getbranchasset(string category, string rcode, string rname, string acode, string aname, string bcode, string bname)
        {
            string filename = string.Empty;
            try
            {
                dtable = ACmodel.getbranchprofiledt();
                dtable.Clear();
                dtmerge = ACmodel.getbranchprofiledt();
                dtmerge.Clear(); 
                var zonename = getzonename(usermodel.zonecode);
                con = new MySqlConnection(ConfigurationManager.ConnectionStrings["cmms"].ConnectionString);
                con.Open();
                cmd = new MySqlCommand(string.Format("SELECT asset_desc,DATE(delivery_date) AS deliverdate,serial_no FROM `cmms`.`cmms_entry_masterheader` WHERE bc_code='{0}' AND zone_code='{1}';", bcode, zonename.Trim()), con);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        datarow = dtable.NewRow();
                        datarow["id"] = id++; 
                        datarow["branchname"] = bname.ToUpper().Trim();
                        datarow["areaname"] = aname.ToUpper().Trim();
                        datarow["regionname"] = rname.Trim().ToUpper();
                        datarow["assetdesc"] = reader["asset_desc"] != System.DBNull.Value ? reader["asset_desc"].ToString().Trim().ToUpper() : "";
                        datarow["deliverydate"] = reader["deliverdate"] != System.DBNull.Value ? reader["deliverdate"].ToString().Trim().ToUpper() : "";
                        datarow["serialno"] = reader["serial_no"] != System.DBNull.Value ? reader["serial_no"].ToString().Trim().ToUpper() : "";
                        dtable.Rows.Add(datarow);
                        dtable.AcceptChanges();
                        dtmerge.Merge(dtable);
                    }
                    reader.Close();
                    filename = generatebranchprofilereport(dtmerge, "BranchAssetReport");
                }
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Branch Asset Error : " + ex.ToString()); System.Web.HttpContext.Current.Session["error"] = ex.ToString();
            }
            return filename;
        }
        public string getbranchIR(string category, string rcode, string rname, string acode, string aname, string bcode, string bname)
        {
            string filename = string.Empty;
            try
            {
                dtable = ACmodel.getbranchprofiledt();
                dtable.Clear();
                dtmerge = ACmodel.getbranchprofiledt();
                dtmerge.Clear();
                //history
                //ir
                var GetIRCount = ac.GetIRCount(bcode, Convert.ToInt32(usermodel.zonecode));
                for (int irloop = 0; irloop <= 2; irloop++)
                {
                    var irstr = string.Empty;
                    if (irloop == 0) { irstr = "OPEN"; } else if (irloop == 1) { irstr = "CLOSED"; } else if (irloop == 2) { irstr = "RECEIVED"; }
                    var GetIR = ac.GetIR(irstr, bcode, Convert.ToInt32(usermodel.zonecode), "", "", "");
                    if (GetIR.RespCode == 1)
                    {
                        foreach (DataRow row6 in ACmodel.ListToDataTable(GetIR.IRList).Rows)
                        {
                            datarow = dtable.NewRow();
                            datarow["id"] = id++;
                            datarow["branchname"] = bname.ToUpper().Trim();
                            datarow["areaname"] = aname.ToUpper().Trim();
                            datarow["regionname"] = rname.Trim().ToUpper();
                            datarow["irno"] = row6["IRNum"] != System.DBNull.Value ? row6["IRNum"].ToString().Trim().ToUpper() : "-";
                            datarow["irdate"] = row6["IRDate"] != System.DBNull.Value ? row6["IRDate"].ToString().Trim().ToUpper() : "-";
                            datarow["irincident"] = row6["IRSpecification"] != System.DBNull.Value ? row6["IRSpecification"].ToString().Trim().ToUpper() : "-";
                            datarow["irauthor"] = row6["AuthorName"] != System.DBNull.Value ? row6["AuthorName"].ToString().Trim().ToUpper() : "-";
                            datarow["irclose"] = GetIRCount.CountClosedIR;
                            datarow["iropen"] = GetIRCount.CountOpenIR;
                            datarow["irreceived"] = GetIRCount.CountReceivedIR;
                            datarow["irstatus"] = irstr;
                            datarow["compasset"] = "1";
                            dtable.Rows.Add(datarow);
                            dtable.AcceptChanges();
                            dtmerge.Merge(dtable);
                        }
                        filename = generatebranchprofilereport(dtmerge, "BranchIRReport");
                    }
                }
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Branch IR Error : " + ex.ToString()); System.Web.HttpContext.Current.Session["error"] = ex.ToString();
            }
            return filename;
        }
        public string getbranchWO(string category, string rcode, string rname, string acode, string aname, string bcode, string bname)
        {
            string filename = string.Empty;
            try
            {
                dtable = ACmodel.getbranchprofiledt();
                dtable.Clear();
                dtmerge = ACmodel.getbranchprofiledt();
                dtmerge.Clear();
                //history
                ////wo
                var GetWorkOrders = ac.GetWorkOrders(bname);
                if (GetWorkOrders.RespCode == 1)
                {
                    foreach (DataRow row5 in ACmodel.ListToDataTable(GetWorkOrders.WorkOrderList).Rows)
                    {
                        datarow = dtable.NewRow();
                        datarow["id"] = id++;
                        datarow["branchname"] = bname.ToUpper().Trim();
                        datarow["areaname"] = aname.ToUpper().Trim();
                        datarow["regionname"] = rname.Trim().ToUpper();
                        datarow["workno"] = row5["WONo"] != System.DBNull.Value ? row5["WONo"].ToString().Trim().ToUpper() : "-";
                        datarow["workdate"] = row5["WODate"] != System.DBNull.Value ? row5["WODate"].ToString().Trim().ToUpper() : "-";
                        datarow["workdesc"] = row5["Description"] != System.DBNull.Value ? row5["Description"].ToString().Trim().ToUpper() : "-";
                        datarow["workauthor"] = row5["Author"] != System.DBNull.Value ? row5["Author"].ToString().Trim().ToUpper() : "-";
                        datarow["workstatus"] = row5["Status"] != System.DBNull.Value ? row5["Status"].ToString().Trim().ToUpper() : "-";
                        datarow["compasset"] = "1"; 
                        dtable.Rows.Add(datarow);
                        dtable.AcceptChanges();
                        dtmerge.Merge(dtable);
                    }
                    filename = generatebranchprofilereport(dtmerge, "BranchWOReport");
                }
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Branch WO Error : " + ex.ToString()); System.Web.HttpContext.Current.Session["error"] = ex.ToString();
            }
            return filename;
        }
        private string generatebranchprofilereport(DataTable dt, string rptpath)
        {
            string filename = string.Empty;
            try
            {
                var zonename = getzonename(usermodel.zonecode);
               
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                ExportOptions objExOpt = default(ExportOptions);

                ReportDocument rpt = new ReportDocument();
                var ReportLocation = String.Format("{0}Reports\\{1}.rpt", AppDomain.CurrentDomain.BaseDirectory, rptpath);
                rpt.Load(ReportLocation);
                rpt.Refresh();

                rpt.SetDataSource(dt);
                rpt.SetParameterValue(0, usermodel.fullname.Trim().ToUpper());
                rpt.SetParameterValue(1, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                rpt.SetParameterValue(2, "ML Branch Profile Report");
                rpt.SetParameterValue(3, usermodel.rname);
                rpt.SetParameterValue(4, zonename);

                string dir = String.Format("{0}Reports\\Export\\", AppDomain.CurrentDomain.BaseDirectory);

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                filename = String.Format("{0}Reports\\Export\\{1}.pdf", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.Ticks);
                objDiskOpt.DiskFileName = filename;
                objExOpt = rpt.ExportOptions;
                objExOpt.ExportDestinationType = ExportDestinationType.DiskFile;
                objExOpt.ExportFormatType = ExportFormatType.PortableDocFormat;
                objExOpt.DestinationOptions = objDiskOpt;

                rpt.Export();
                rpt.Close();
                rpt.Dispose();
                
                return filename;
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Branch Profile Export Error : " + ex.ToString()); System.Web.HttpContext.Current.Session["error"] = "Error in exporting PDF File: " + ex.ToString(); return string.Empty;
            }
        }

        [HttpGet]
        public ActionResult loadReport()
        {
            var zonename = getzonename(usermodel.zonecode);
            try
            {
                if (usermodel.category == "activeusers")
                {
                    ReportDocument rpt = new ReportDocument();
                    string rptpath = "AccessControlReport.rpt";
                    string rpttitle = zonename + '-' + usermodel.rname.Trim().Replace(' ', '-') + "-UsersReport-" + DateTime.Now.ToString("yyyy-MM-dd");
                    var ReportLocation = String.Format("{0}Reports\\{1}", AppDomain.CurrentDomain.BaseDirectory, rptpath);
                    rpt.Load(ReportLocation);
                    rpt.Refresh();
                    rpt.SetDataSource(usermodel.dt);
                    rpt.SetParameterValue(0, usermodel.fullname.Trim().ToUpper());
                    rpt.SetParameterValue(1, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    rpt.SetParameterValue(2, "ML Access Control Report");
                    rpt.SetParameterValue(3, usermodel.rname);
                    rpt.SetParameterValue(4, zonename);
                    rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, rpttitle);
                    rpt.Close();
                    rpt.Dispose();
                }
            }
            catch (Exception ex)
            {
                ACmodel.WriteToFile("Load Report Error : " + ex.ToString());
            }
            return new EmptyResult();
        }
        public string getzonename(string zcode) {
            var zonename = string.Empty;
            if (zcode == "1") { zonename = "VISMIN"; }
            else if (zcode == "2") { zonename = "LNCR"; }
            else if (zcode == "3") { zonename = "US"; }
            else if (zcode == "4") { zonename = "SHOWROOM"; }
            else if (zcode == "5") { zonename = "VISAYAS"; }
            else if (zcode == "6") { zonename = "MINDANAO"; }
            else if (zcode == "7") { zonename = "LUZON"; }
            else if (zcode == "8") { zonename = "NCR"; }
            return zonename;
        }

	}
}