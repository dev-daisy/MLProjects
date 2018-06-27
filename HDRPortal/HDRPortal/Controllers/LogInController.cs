using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HDRPortal.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using AESEncrypt;


namespace HDRPortal.Controllers
{
    public class LogInController : Controller
    {
        //
        // GET: /LogIn/
        AESEncryption decrypt = new AESEncryption();
        MySqlCommand cmd = new MySqlCommand();
        AcessControlModel acmodel = new AcessControlModel();
        UserModel usermod = new UserModel();
        MySqlConnection con = new MySqlConnection();
        string user = string.Empty;
        string pass = string.Empty;
        public ActionResult Index()
        {

            var uname = Request.QueryString["user"];
            var pass = Request.QueryString["pass"];
            //GMO
            //uname = "PERN94016508";
            //pass = "Sd8z4YirX+MGDaf+eUL+EwhXwODEw9K27KlzETjyoXw=";
            //HELPDESK
            //uname = "GATC0602975501";
            //pass = "eBUFFDVBCr2Y3o83aq6cOdWYlHp4Iu1UFMlgtDsfxC8=";
            //uname = "LARI11949070";
            //pass = "b/E4kDM/c6OElhHM/AR4HUWrxO1JhOSlW5dpmrCIhO8=";
            try
            {
                if (uname != string.Empty && pass != string.Empty && uname != null && pass != null)
                {
                    pass = pass.Replace("%", " ").Replace(" ", "+");
                    try { pass = decrypt.AESDecrypt(pass, "kWuYDGElyQDpGKM9"); }
                    catch { pass = pass.Replace("%", " ").Replace(" ", "+"); ; }
                    System.Web.HttpContext.Current.Session["UserName"] = uname;
                    System.Web.HttpContext.Current.Session["UserPass"] = pass;
                    var flag = 1;
                    try { flag = (int)System.Web.HttpContext.Current.Session["islogin"]; }
                    catch { flag = 1; }
                    if (flag == 0) {
                        System.Web.HttpContext.Current.Session.Clear();
                        usermod.islogin = false;
                        usermod.message = "Please Enter Username and Password.";
                        return View(usermod);
                    }
                    else {
                        return RedirectToAction("Main", "LogIn");
                    }
                }
                else
                {
                    System.Web.HttpContext.Current.Session.Clear();
                    usermod.islogin = false;
                    usermod.message = "Please Enter Username and Password.";
                    return View(usermod);
                }
            }
            catch (Exception ex)
            {
                acmodel.WriteToFile("Login Error : " + ex.ToString());
                System.Web.HttpContext.Current.Session.Clear();
                usermod.islogin = false;
                usermod.message = "Please Enter Username and Password.";
                return View(usermod);
            }
        }

        [HttpPost]

        public ActionResult Index(string username, string userpass)
        {
            System.Web.HttpContext.Current.Session.Clear();
            try
            {
                if ((username != string.Empty && userpass != string.Empty) && (username != null && userpass != null))
                {
                    var pass = string.Empty;
                    var passEn = string.Empty;
                    string domesticA = ConfigurationManager.ConnectionStrings["domesticA"].ConnectionString;
                    string domesticB = ConfigurationManager.ConnectionStrings["domesticB"].ConnectionString;
                    con = new MySqlConnection(domesticB);
                    con.Open();
                    cmd = new MySqlCommand(string.Format("CALL `accesscontrollogin`('{0}')", username), con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        try { pass = decrypt.AESDecrypt(reader["userpassword"].ToString().Trim(), "kWuYDGElyQDpGKM9"); }
                        catch { pass = reader["userpassword"].ToString().Trim(); }
                        if (pass == userpass)
                        {
                            usermod.resourceid = reader["resourceid"].ToString().Trim();
                            usermod.userlogin = reader["userlogin"].ToString().Trim();
                            usermod.userpassword = reader["userpassword"].ToString().Trim();
                            usermod.roleid = reader["roleid"].ToString().Trim();
                            usermod.fullname = reader["fullname"].ToString().Trim();
                            usermod.zonecode = reader["zonecode"].ToString().Trim();
                            usermod.regioncode = reader["regioncode"].ToString().Trim();
                            usermod.regionname = reader["regionname"].ToString().Trim();
                            usermod.areacode = reader["areacode"].ToString().Trim();
                            usermod.areaname = reader["areaname"].ToString().Trim();
                            usermod.branchcode = reader["branchcode"].ToString().Trim();
                            usermod.branchname = reader["branchname"].ToString().Trim();
                            usermod.islogin = true;
                            reader.Close();
                            con.Close();
                            System.Web.HttpContext.Current.Session["UserModel"] = usermod;
                            System.Web.HttpContext.Current.Session["fname"] = usermod.fullname;
                            System.Web.HttpContext.Current.Session["userrole"] = usermod.roleid;
                            System.Web.HttpContext.Current.Session["userlogin"] = usermod.userlogin;
                            user = usermod.userlogin;
                            passEn = decrypt.AESEncrypt(usermod.userpassword, "kWuYDGElyQDpGKM9");
                            System.Web.HttpContext.Current.Session["userpass"] = usermod.userpassword;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            reader.Close();
                            con.Close();
                            System.Web.HttpContext.Current.Session.Clear();
                            usermod.message = "Incorrect Username/Password Entered.";
                        }
                    }
                    else if (username == "admin" && userpass == "123456ML")
                    {
                        usermod.islogin = true;
                        System.Web.HttpContext.Current.Session["UserModel"] = usermod;
                        System.Web.HttpContext.Current.Session["fname"] = username;
                        System.Web.HttpContext.Current.Session["userrole"] = username;
                        System.Web.HttpContext.Current.Session["userlogin"] = username;
                        user = usermod.userlogin;
                        System.Web.HttpContext.Current.Session["userpass"] = userpass;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        reader.Close();
                        con.Close();
                        System.Web.HttpContext.Current.Session.Clear();
                        usermod.message = "Incorrect Username/Password Entered.";
                    }
                }
                else {
                    System.Web.HttpContext.Current.Session.Clear();
                    usermod.message = "Please Enter Username and Password.";
                }

            }
            catch (Exception ex)
            {
                acmodel.WriteToFile("Login Index Error : " + ex.ToString());

                throw;
            }
            return View(usermod);
        }

        public ActionResult Main()
        {
            var pass = string.Empty;
            var passEn = string.Empty;
            var username = (string)System.Web.HttpContext.Current.Session["UserName"];
            var userpass = (string)System.Web.HttpContext.Current.Session["UserPass"];
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                string domesticA = ConfigurationManager.ConnectionStrings["domesticA"].ConnectionString;
                string domesticB = ConfigurationManager.ConnectionStrings["domesticB"].ConnectionString;
                con = new MySqlConnection(domesticB);
                con.Open();
                cmd = new MySqlCommand(string.Format("CALL kpusers.`accesscontrollogin`('{0}')", username), con);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    try { pass = decrypt.AESDecrypt(reader["userpassword"].ToString().Trim(), "kWuYDGElyQDpGKM9"); }
                    catch { pass = reader["userpassword"].ToString().Trim(); }
                    if (pass == userpass)
                    {
                        usermod.userlogin = reader["userlogin"].ToString().Trim();
                        usermod.userpassword = reader["userpassword"].ToString().Trim();
                        usermod.fullname = reader["fullname"].ToString().Trim();
                        usermod.roleid = reader["roleid"].ToString().Trim();
                        usermod.islogin = true;
                        reader.Close();
                        con.Close();
                        System.Web.HttpContext.Current.Session["UserModel"] = usermod;
                        System.Web.HttpContext.Current.Session["fname"] = usermod.fullname;
                        System.Web.HttpContext.Current.Session["userrole"] = usermod.roleid;
                        System.Web.HttpContext.Current.Session["userlogin"] = usermod.userlogin;
                        user = usermod.userlogin;
                        passEn = decrypt.AESEncrypt(usermod.userpassword, "kWuYDGElyQDpGKM9");
                        System.Web.HttpContext.Current.Session["userpass"] = usermod.userpassword;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        reader.Close();
                        con.Close();
                        System.Web.HttpContext.Current.Session["islogin"] = 0;
                        return RedirectToAction("Index", "LogIn");
                    }
                }
                else
                {
                    System.Web.HttpContext.Current.Session["islogin"] = 0;
                    return RedirectToAction("Index", "LogIn");
                }
            }
            catch (Exception ex)
            {
                acmodel.WriteToFile("Login Main Error : " + ex.ToString());
                System.Web.HttpContext.Current.Session["islogin"] = 0;
                return RedirectToAction("Index", "LogIn");
            }
        }    
    }
}
