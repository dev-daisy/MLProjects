using AESEncrypt;
using CustomerService.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerService.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public string username = string.Empty;
        public string userpass = string.Empty;
        public string pass = string.Empty;
        AESEncryption decrypt = new AESEncryption();
        Connection conn = new Connection();
        CustomerServiceModel cs = new CustomerServiceModel();
        public ActionResult Index()
        {

            System.Web.HttpContext.Current.Session.Clear();
            username = Request.QueryString["user"];
            userpass = Request.QueryString["pass"];

            //username = "LARI11949070"; //123456
            //userpass = "11949070";

            //http://192.168.12.41/CustomerService/Login?user=admin&pass=123456ML

            //username = "admin"; //123456
            //userpass = "123456ML";
            try
            {
                if (username != string.Empty && userpass != string.Empty)
                {
                    userpass = userpass.Replace("%", " ").Replace(" ", "+");
                    try { userpass = decrypt.AESDecrypt(userpass, "kWuYDGElyQDpGKM9"); }
                    catch { userpass = userpass.Replace("%", " ").Replace(" ", "+"); ; }
                    System.Web.HttpContext.Current.Session["UserName"] = username;
                    System.Web.HttpContext.Current.Session["UserPass"] = userpass;
                    if (username == "admin" && userpass == "123456ML")
                    {
                        System.Web.HttpContext.Current.Session["userfullname"] = username;
                        System.Web.HttpContext.Current.Session["user"] = username;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Main", "Login");
                    }
                }
                else { cs.WriteToFile("Customer Service Login: User not found!"); return RedirectToAction("Index", "Logout"); }
            }
            catch (Exception ex) { cs.WriteToFile("Customer Service Login: " + ex.ToString()); return RedirectToAction("Index", "Logout"); }
        }
        public ActionResult Main()
        {
            username = (string)System.Web.HttpContext.Current.Session["UserName"];
            userpass = (string)System.Web.HttpContext.Current.Session["UserPass"];
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.connectdb("DomesticB");
                using (MySqlConnection mycon = conn.getConnection())
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        cmd.CommandText = String.Format("call kpusers.accesscontrollogin('{0}');", username);
                        using (MySqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                while (Reader.Read())
                                {
                                    try { pass = decrypt.AESDecrypt(Reader["userpassword"].ToString().Trim(), "kWuYDGElyQDpGKM9"); }
                                    catch { pass = Reader["userpassword"].ToString().Trim(); }
                                    if (pass == userpass)
                                    {
                                        System.Web.HttpContext.Current.Session["userfullname"] = Reader["fullname"].ToString().Trim();
                                    }
                                    else { return RedirectToAction("Index", "Logout"); }
                                }
                            }
                            else { return RedirectToAction("Index", "Logout"); }
                            Reader.Close();
                            mycon.Close();
                        }
                    }
                }
                return RedirectToAction("Index", "CustomerService");
            }
            catch (Exception ex) { cs.WriteToFile("Error in Customer Service Login :" + ex.ToString()); return RedirectToAction("Index", "Logout"); }
        }

    }
}
