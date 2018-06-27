using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MLAccessControl.Models
{
    public class Connection
    {
        private SqlConnection conn;
        //IniConfig conn = new IniConfig(AppDomain.CurrentDomain.BaseDirectory + "MapReportConfig.ini");
        IniConfig ini = new IniConfig("C:\\kpconfig\\ReportsMapConfig.ini");

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
                conn = QCLDBConnect(Server, DB, UID, Password, Pool, MaxCon, MinCon, Timeout, DBTimeout);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private SqlConnection mycon;
        private Boolean pool = false;

        private SqlConnection QCLDBConnect(String Serv, String DB, String UID, String Password, String pooling, Int32 maxcon, Int32 mincon, Int32 tout, Int32 DBTimeOut)
        {
            if (pooling.Equals("1"))
            {
                pool = true;
            }

            string myconstring = "server = " + Serv + "; database = " + DB + "; uid = " + UID + ";password= " + Password + "; pooling=" + pool + ";min pool size=" + mincon + ";max pool size=" + maxcon + ";connection lifetime=0; connection timeout=" + tout + ";";
            mycon = new SqlConnection(myconstring);
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

        public SqlConnection getConnection()
        {
            return mycon;
        }

        public void dispose()
        {
            mycon.Dispose();
        }

    }
}