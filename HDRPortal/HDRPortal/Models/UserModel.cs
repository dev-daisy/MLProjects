using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using MySql.Data.MySqlClient;
using System.Web.Mvc;
using System.Data;


namespace HDRPortal.Models
{
    public class UserModel
    {
        public string resourceid { get; set; }
        public string userlogin { get; set; }
        public string userpassword { get; set; }
        public string fullname { get; set; }
        public string branchcode { get; set; }
        public string branchname { get; set; }
        public string roleid { get; set; }
        public string zonecode { get; set; }
        public string regioncode { get; set; }
        public string regionname { get; set; }
        public string areacode { get; set; }
        public string areaname { get; set; }
        public string message { get; set; }
        public Boolean islogin { get; set; }
        public string rname { get; set; }
        public string category { get; set; }
        public DataTable dt { get; set; }
    }
}