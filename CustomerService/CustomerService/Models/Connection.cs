using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerService.Models
{
    public class Connection
    {
        private MySqlConnection conn;
        //IniConfig conn = new IniConfig(AppDomain.CurrentDomain.BaseDirectory + "MapReportConfig.ini");
        IniConfig ini = new IniConfig("C:\\kpconfig\\ReportsCustomerServiceConfig.ini");

        public void connectdb(string flag)
        {
            try
            {
                String Server = ini.IniReadValue(flag, "server");
                String DB = ini.IniReadValue(flag, "database"); ;
                String UID = ini.IniReadValue(flag, "uid"); ;
                String Password = ini.IniReadValue(flag, "password"); ;
                String Pool = ini.IniReadValue(flag, "pool");
                Int32 DBTimeout = Convert.ToInt32(ini.IniReadValue(flag, "DBTimeOut"));
                Int32 MaxCon = Convert.ToInt32(ini.IniReadValue(flag, "maxcon"));
                Int32 MinCon = Convert.ToInt32(ini.IniReadValue(flag, "mincon"));
                Int32 Timeout = Convert.ToInt32(ini.IniReadValue(flag, "tout"));
                conn = DBConnect(Server, DB, UID, Password, Pool, MaxCon, MinCon, Timeout, DBTimeout);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private MySqlConnection mycon;
        private Boolean pool = false;

        private MySqlConnection DBConnect(String Serv, String DB, String UID, String Password, String pooling, Int32 maxcon, Int32 mincon, Int32 tout, Int32 DBTimeOut)
        {
            if (pooling.Equals("1"))
            {
                pool = true;
            }

            string myconstring = "server = " + Serv + "; database = " + DB + "; uid = " + UID + ";password= " + Password + "; pooling=" + pool + ";min pool size=" + mincon + ";max pool size=" + maxcon + ";connection lifetime=0; connection timeout=" + tout + ";Allow Zero Datetime=true";
            mycon = new MySqlConnection(myconstring);
            return mycon;
        }
        public bool OpenConnection()
        {
            try
            {
                mycon.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                mycon.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public MySqlConnection getConnection()
        {
            return mycon;
        }

        public void dispose()
        {
            mycon.Dispose();
        }

        public string hiveconnection()
        {
            return ini.IniReadValue("Hadoop", "Constring");
        }


        public string parameters(string flag)
        {
            return  ini.IniReadValue("Parameters", flag);
        }
    }
}