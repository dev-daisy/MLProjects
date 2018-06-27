using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerService.Models
{
    public class GetNameModel
    {
        Connection conn = new Connection();
        CustomerServiceModel cust = new CustomerServiceModel();

        public string fname { get; set; }
        public string mname { get; set; }
        public string lname { get; set; }
        public string mobileno { get; set; }
        public string username { get; set; }
        public string emailadd { get; set; }
        public string walletno { get; set; }
        public string birthdate { get; set; }
        public string custid { get; set; }
        public string gender { get; set; }


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
        public bool iscorporatepo = false;


        public string zone(int zcode){
            string zname = string.Empty;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.connectdb("DomesticB");
                using (MySqlConnection mycon = conn.getConnection())
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT zonename FROM kpusers.zonecodes WHERE  zonecode='{0}' limit 1;",zcode);
                        using (MySqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                Reader.Read();
                                zname = Reader["zonename"].ToString().ToUpper();
                            }
                            Reader.Close();
                            mycon.Close();
                        }
                    }
                }

                if (zname == string.Empty) {
                    conn.connectdb("GlobalB");
                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            cmd.CommandText = string.Format("SELECT zonename FROM kpusersglobal.zonecodes WHERE  zonecode='{0}' limit 1;", zcode);
                            using (MySqlDataReader Reader = cmd.ExecuteReader())
                            {
                                if (Reader.HasRows)
                                {
                                    Reader.Read();
                                    zname = Reader["zonename"].ToString().ToUpper();
                                }
                                Reader.Close();
                                mycon.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { cust.WriteToFile("Get Zone : " + ex.ToString()); }
            return zname;
        }

        public string bname(int zcode, string bcode)
        {
            string bname = string.Empty;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.connectdb("DomesticB");
                using (MySqlConnection mycon = conn.getConnection())
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT branchname FROM kpusers.branches WHERE  zonecode='{0}' and branchcode='{1}' limit 1;", zcode, bcode);
                        using (MySqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                Reader.Read();
                                bname = Reader["branchname"].ToString().ToUpper();
                            }
                            Reader.Close();
                            mycon.Close();
                        }
                    }
                }

                if (bname == string.Empty) {
                    conn.connectdb("GlobalB");
                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            cmd.CommandText = string.Format("SELECT branchname FROM kpusersglobal.branches WHERE  zonecode='{0}' and branchcode='{1}' limit 1;", zcode, bcode);
                            using (MySqlDataReader Reader = cmd.ExecuteReader())
                            {
                                if (Reader.HasRows)
                                {
                                    Reader.Read();
                                    bname = Reader["branchname"].ToString().ToUpper();
                                }
                                Reader.Close();
                                mycon.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { cust.WriteToFile("Get BranchName : " + ex.ToString()); }
            return bname;
        }
        public void kycinfo( string custno, string firstname, string middlename, string lastname)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                custid = "";
                gender = "";
                birthdate = "";
                mobileno = "";
                emailadd = "";
                fname = "";
                mname = "";
                lname = "";
                walletno = "";
                username = "";

                street = "";
                provincecity = "";
                country = "";
                zipcode = "";
                branchid = "";
                idtype = "";
                idno = "";
                expirydate = "";
                dtcreated = "";
                dtmodified = "";
                createdby = "";
                modifiedby = "";
                phoneno = "";
                cardno = "";
                placeofbirth = "";
                natureofwork = "";
                permanentaddress = "";
                nationality = "";
                companyoremployer = "";
                businessorprofession = "";
                govtidtype = "";
                govtidno = "";
                branchcreated = "";
                branchmodified = "";
                mlcardno = "";

                conn.connectdb("KYCText");
                using (MySqlConnection mycon = conn.getConnection())
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT a.custid,firstname,lastname,middlename,street,provincecity,country,  zipcode,gender,birthdate,branchid,idtype,idno,expirydate,dtcreated, " +
                        " a.dtmodified,a.createdby,a.modifiedby,  phoneno,mobile,cardno,email,placeofbirth,natureofwork,permanentaddress,nationality, " +
                        " companyoremployer,businessorprofession,govtidtype,govtidno,branchcreated,branchmodified,cardno " +
                        " FROM `kpcustomers`.`customers` a " +
                        " INNER JOIN `kpcustomers`.`CustomerInfo`  b ON b.custid=a.custid" +
                        " left JOIN `kpcustomers`.`customerGovtID`  c ON c.custid=a.custid" +
                        " left JOIN `kpcustomers`.`customerbranch`  d ON d.custid=a.custid" +
                        " where (  concat(lastname,', ',firstname,' ',middlename)=concat('{2}, ','{0} ','{1}')  or " +
                        " concat(firstname,' ',middlename,' ',lastname)=concat('{0} ','{1} ','{3}') ) or " +
                        " (if('{0}'=''||'{0}' is null,0,lower(firstname)='{0}') and " +
                        " if('{1}'=''||'{1}' is null,0,lower(middlename)='{1}')  and  " +
                        " if('{2}'=''||'{2}' is null,0,lower(lastname)='{2}') " +
                        " and  if('{3}' = '' || '{3}' is null,0,lower(a.custid)='{3}'))  limit 1;", firstname.Trim().ToLower(), middlename.Trim().ToLower(), lastname.Trim().ToLower(), custno.Trim().ToLower());
                       
                        using (MySqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                Reader.Read();
                                custid = (Reader["custid"] == System.DBNull.Value) ? "" : Reader["custid"].ToString().Trim().ToUpper();
                                gender = (Reader["gender"] == System.DBNull.Value) ? "" : Reader["gender"].ToString().Trim().ToUpper();
                                birthdate = (Reader["birthdate"] == System.DBNull.Value) ? "" : Convert.ToDateTime(Reader["birthdate"]).ToString("yyyy-MM-dd").Trim().ToUpper();
                                mobileno = (Reader["mobile"] == System.DBNull.Value) ? "" : Reader["mobile"].ToString().Trim().ToUpper();
                                emailadd = (Reader["email"] == System.DBNull.Value) ? "" : Reader["email"].ToString().Trim().ToUpper();
                                fname = (Reader["firstname"] == System.DBNull.Value) ? "" : Reader["firstname"].ToString().Trim().ToUpper().Replace("Ñ", "N");
                                mname = (Reader["middlename"] == System.DBNull.Value) ? "" : Reader["middlename"].ToString().Trim().ToUpper().Replace("Ñ", "N");
                                lname = (Reader["lastname"] == System.DBNull.Value) ? "" : Reader["lastname"].ToString().Trim().ToUpper().Replace("Ñ", "N");

                                street = (Reader["street"] == System.DBNull.Value) ? "" : Reader["street"].ToString().Trim().ToUpper();
                                provincecity = (Reader["provincecity"] == System.DBNull.Value) ? "" : Reader["provincecity"].ToString().Trim().ToUpper();
                                country = (Reader["country"] == System.DBNull.Value) ? "" : Reader["country"].ToString().Trim().ToUpper();
                                zipcode = (Reader["zipcode"] == System.DBNull.Value) ? "" : Reader["zipcode"].ToString().Trim().ToUpper();
                                branchid = (Reader["branchid"] == System.DBNull.Value) ? "" : Reader["branchid"].ToString().Trim().ToUpper();
                                idtype = (Reader["idtype"] == System.DBNull.Value) ? "" : Reader["idtype"].ToString().Trim().ToUpper();
                                idno = (Reader["idno"] == System.DBNull.Value) ? "" : Reader["idno"].ToString().Trim().ToUpper();
                                expirydate = (Reader["expirydate"] == System.DBNull.Value) ? "" : Reader["expirydate"].ToString().Trim().ToUpper();
                                dtcreated = (Reader["dtcreated"] == System.DBNull.Value) ? "" : Reader["dtcreated"].ToString().Trim().ToUpper();
                                dtmodified = (Reader["dtmodified"] == System.DBNull.Value) ? "" : Reader["dtmodified"].ToString().Trim().ToUpper();
                                createdby = (Reader["createdby"] == System.DBNull.Value) ? "" : Reader["createdby"].ToString().Trim().ToUpper();
                                modifiedby = (Reader["modifiedby"] == System.DBNull.Value) ? "" : Reader["modifiedby"].ToString().Trim().ToUpper();
                                phoneno = (Reader["phoneno"] == System.DBNull.Value) ? "" : Reader["phoneno"].ToString().Trim().ToUpper();
                                cardno = (Reader["cardno"] == System.DBNull.Value) ? "" : Reader["cardno"].ToString().Trim().ToUpper();
                                placeofbirth = (Reader["placeofbirth"] == System.DBNull.Value) ? "" : Reader["placeofbirth"].ToString().Trim().ToUpper();
                                natureofwork = (Reader["natureofwork"] == System.DBNull.Value) ? "" : Reader["natureofwork"].ToString().Trim().ToUpper();
                                permanentaddress = (Reader["permanentaddress"] == System.DBNull.Value) ? "" : Reader["permanentaddress"].ToString().Trim().ToUpper();
                                nationality = (Reader["nationality"] == System.DBNull.Value) ? "" : Reader["nationality"].ToString().Trim().ToUpper();
                                companyoremployer = (Reader["companyoremployer"] == System.DBNull.Value) ? "" : Reader["companyoremployer"].ToString().Trim().ToUpper();
                                businessorprofession = (Reader["businessorprofession"] == System.DBNull.Value) ? "" : Reader["businessorprofession"].ToString().Trim().ToUpper();
                                govtidtype = (Reader["govtidtype"] == System.DBNull.Value) ? "" : Reader["govtidtype"].ToString().Trim().ToUpper();
                                govtidno = (Reader["govtidno"] == System.DBNull.Value) ? "" : Reader["govtidno"].ToString().Trim().ToUpper();
                                branchcreated = (Reader["branchcreated"] == System.DBNull.Value) ? "" : Reader["branchcreated"].ToString().Trim().ToUpper();
                                branchmodified = (Reader["branchmodified"] == System.DBNull.Value) ? "" : Reader["branchmodified"].ToString().Trim().ToUpper();
                                cardno = (Reader["cardno"] == System.DBNull.Value) ? "" : Reader["cardno"].ToString().Trim().ToUpper();
                            }
                            Reader.Close();
                            mycon.Close();
                        }
                    }
                }


                try {

                    conn.connectdb("GlobalB");
                    using (MySqlConnection mycon = conn.getConnection())
                    {
                        mycon.Open();
                        using (cmd = mycon.CreateCommand())
                        {
                            cmd.CommandText = string.Format("SELECT a.custid,firstname,lastname,middlename,street,provincecity,country,   " +
                            " zipcode,gender,birthdate,'' AS branchid,idtype,idno,expirydate,dtcreated,  a.dtmodified,a.createdby,a.modifiedby,  " +
                            " phoneno,mobile,cardno,email,'' AS placeofbirth,'' AS natureofwork,homecity AS permanentaddress,'' AS nationality, '' AS companyoremployer, " +
                            " occupation AS businessorprofession,secondidtype AS govtidtype,secondidno AS govtidno,createdbybranch AS branchcreated,'' AS branchmodified,cardno  " +
                            " FROM `kpcustomersglobal`.`customers` a " +
                            " INNER JOIN `kpcustomersglobal`.`customersdetails` b ON b.custid=a.custid " +
                            " where (  concat(lastname,', ',firstname,' ',middlename)=concat('{2}, ','{0} ','{1}')  or " +
                            " concat(firstname,' ',middlename,' ',lastname)=concat('{0} ','{1} ','{3}') ) or " +
                            " (if('{0}'=''||'{0}' is null,0,lower(firstname)='{0}') and " +
                            " if('{1}'=''||'{1}' is null,0,lower(middlename)='{1}')  and  " +
                            " if('{2}'=''||'{2}' is null,0,lower(lastname)='{2}') " +
                            " and  if('{3}' = '' || '{3}' is null,0,lower(a.custid)='{3}'))  limit 1;", firstname.Trim().ToLower(), middlename.Trim().ToLower(), lastname.Trim().ToLower(), custno.Trim().ToLower());

                            using (MySqlDataReader Reader = cmd.ExecuteReader())
                            {
                                if (Reader.HasRows)
                                {
                                    Reader.Read();
                                    custid = (Reader["custid"] == System.DBNull.Value) ? "" : Reader["custid"].ToString().Trim().ToUpper();
                                    gender = (Reader["gender"] == System.DBNull.Value) ? "" : Reader["gender"].ToString().Trim().ToUpper();
                                    birthdate = (Reader["birthdate"] == System.DBNull.Value) ? "" : Convert.ToDateTime(Reader["birthdate"]).ToString("yyyy-MM-dd").Trim().ToUpper();
                                    mobileno = (Reader["mobile"] == System.DBNull.Value) ? "" : Reader["mobile"].ToString().Trim().ToUpper();
                                    emailadd = (Reader["email"] == System.DBNull.Value) ? "" : Reader["email"].ToString().Trim().ToUpper();
                                    fname = (Reader["firstname"] == System.DBNull.Value) ? "" : Reader["firstname"].ToString().Trim().ToUpper().Replace("Ñ", "N");
                                    mname = (Reader["middlename"] == System.DBNull.Value) ? "" : Reader["middlename"].ToString().Trim().ToUpper().Replace("Ñ", "N");
                                    lname = (Reader["lastname"] == System.DBNull.Value) ? "" : Reader["lastname"].ToString().Trim().ToUpper().Replace("Ñ", "N");

                                    street = (Reader["street"] == System.DBNull.Value) ? "" : Reader["street"].ToString().Trim().ToUpper();
                                    provincecity = (Reader["provincecity"] == System.DBNull.Value) ? "" : Reader["provincecity"].ToString().Trim().ToUpper();
                                    country = (Reader["country"] == System.DBNull.Value) ? "" : Reader["country"].ToString().Trim().ToUpper();
                                    zipcode = (Reader["zipcode"] == System.DBNull.Value) ? "" : Reader["zipcode"].ToString().Trim().ToUpper();
                                    branchid = (Reader["branchid"] == System.DBNull.Value) ? "" : Reader["branchid"].ToString().Trim().ToUpper();
                                    idtype = (Reader["idtype"] == System.DBNull.Value) ? "" : Reader["idtype"].ToString().Trim().ToUpper();
                                    idno = (Reader["idno"] == System.DBNull.Value) ? "" : Reader["idno"].ToString().Trim().ToUpper();
                                    expirydate = (Reader["expirydate"] == System.DBNull.Value) ? "" : Reader["expirydate"].ToString().Trim().ToUpper();
                                    dtcreated = (Reader["dtcreated"] == System.DBNull.Value) ? "" : Reader["dtcreated"].ToString().Trim().ToUpper();
                                    dtmodified = (Reader["dtmodified"] == System.DBNull.Value) ? "" : Reader["dtmodified"].ToString().Trim().ToUpper();
                                    createdby = (Reader["createdby"] == System.DBNull.Value) ? "" : Reader["createdby"].ToString().Trim().ToUpper();
                                    modifiedby = (Reader["modifiedby"] == System.DBNull.Value) ? "" : Reader["modifiedby"].ToString().Trim().ToUpper();
                                    phoneno = (Reader["phoneno"] == System.DBNull.Value) ? "" : Reader["phoneno"].ToString().Trim().ToUpper();
                                    cardno = (Reader["cardno"] == System.DBNull.Value) ? "" : Reader["cardno"].ToString().Trim().ToUpper();
                                    placeofbirth = (Reader["placeofbirth"] == System.DBNull.Value) ? "" : Reader["placeofbirth"].ToString().Trim().ToUpper();
                                    natureofwork = (Reader["natureofwork"] == System.DBNull.Value) ? "" : Reader["natureofwork"].ToString().Trim().ToUpper();
                                    permanentaddress = (Reader["permanentaddress"] == System.DBNull.Value) ? "" : Reader["permanentaddress"].ToString().Trim().ToUpper();
                                    nationality = (Reader["nationality"] == System.DBNull.Value) ? "" : Reader["nationality"].ToString().Trim().ToUpper();
                                    companyoremployer = (Reader["companyoremployer"] == System.DBNull.Value) ? "" : Reader["companyoremployer"].ToString().Trim().ToUpper();
                                    businessorprofession = (Reader["businessorprofession"] == System.DBNull.Value) ? "" : Reader["businessorprofession"].ToString().Trim().ToUpper();
                                    govtidtype = (Reader["govtidtype"] == System.DBNull.Value) ? "" : Reader["govtidtype"].ToString().Trim().ToUpper();
                                    govtidno = (Reader["govtidno"] == System.DBNull.Value) ? "" : Reader["govtidno"].ToString().Trim().ToUpper();
                                    branchcreated = (Reader["branchcreated"] == System.DBNull.Value) ? "" : Reader["branchcreated"].ToString().Trim().ToUpper();
                                    branchmodified = (Reader["branchmodified"] == System.DBNull.Value) ? "" : Reader["branchmodified"].ToString().Trim().ToUpper();
                                    cardno = (Reader["cardno"] == System.DBNull.Value) ? "" : Reader["cardno"].ToString().Trim().ToUpper();
                                }
                                Reader.Close();
                                mycon.Close();
                            }
                        }
                    }
                }
                catch (Exception ex) { cust.WriteToFile("Get KYC Global Info : " + ex.ToString()); }
            }
            catch (Exception ex) { cust.WriteToFile("Get KYC Info : " + ex.ToString()); }
        }
        public void walletinfo(string uname, string walletnumber, string custno, string firstname, string middlename, string lastname, bool iscorp)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                custid = "";
                gender = "";
                birthdate = "";
                mobileno = "";
                emailadd = "";
                fname = "";
                mname = "";
                lname = "";
                walletno = "";
                username ="";

                street = "";
                provincecity = "";
                country = "";
                zipcode = "";
                branchid = "";
                idtype = "";
                idno = "";
                expirydate = "";
                dtcreated = "";
                dtmodified = "";
                createdby = "";
                modifiedby = "";
                phoneno = "";
                cardno = "";
                placeofbirth = "";
                natureofwork = "";
                permanentaddress = "";
                nationality = "";
                companyoremployer = "";
                businessorprofession = "";
                conn.connectdb("MobileB");
                using (MySqlConnection mycon = conn.getConnection())
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        if (iscorp == true)
                        {
                            cmd.CommandText = string.Format("SELECT custid,a.walletno,mobileno,emailaddress,birthdate,gender,username, " +
                              " street,provincecity,country,  zipcode,DATE_FORMAT( dtcreated,'%Y-%m-%d %H:%i:%s') as dtcreated,DATE_FORMAT( a.dtmodified,'%Y-%m-%d %H:%i:%s') as dtmodified,  natureofwork,permanentaddress, " +
                              " nationality,firstname,middlename,lastname,branchname FROM kpmobile.mobileaccounts a " +
                              " inner join kpmobile.MobileAccountAddress b on b.walletno=a.walletno " +
                              " WHERE  (if('{1}'=''||'{1}' is null,0,lower(firstname)='{1}')  and  " +
                             " if('{2}'=''||'{2}' is null,0,lower(middlename)='{2}')  and  " +
                             " if('{3}'=''||'{3}' is null,0,lower(lastname)='{3}'))  ORDER BY dtcreated DESC  limit 1;", uname.Trim().ToLower(), firstname.Trim().ToLower(), middlename.Trim().ToLower(), lastname.Trim().ToLower());
                        }
                        else {
                            cmd.CommandText = string.Format("SELECT custid,a.walletno,mobileno,emailaddress,birthdate,gender,username, " +
                             " street,provincecity,country,  zipcode,DATE_FORMAT( dtcreated,'%Y-%m-%d %H:%i:%s') as dtcreated,DATE_FORMAT( a.dtmodified,'%Y-%m-%d %H:%i:%s') as dtmodified,  natureofwork,permanentaddress, " +
                             " nationality,firstname,middlename,lastname,branchname FROM kpmobile.mobileaccounts a " +
                             " inner join kpmobile.MobileAccountAddress b on b.walletno=a.walletno " +
                             " WHERE    (if('{1}'=''||'{1}' is null,0,lower(firstname)='{1}')  and  " +
                             " if('{2}'=''||'{2}' is null,0,lower(middlename)='{2}')  and  " +
                             " if('{3}'=''||'{3}' is null,0,lower(lastname)='{3}'))  or  " +
                             " if('{5}'=''||'{5}' is null,0,lower(custid)='{5}') and custid is not null  ORDER BY dtcreated DESC " +
                             " limit 1;", uname.Trim().ToLower(), firstname.Trim().ToLower(), middlename.Trim().ToLower(), lastname.Trim().ToLower(),
                             walletnumber.Trim().ToLower(), custno.Trim().ToLower());
                        }
                         using (MySqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                while (Reader.Read())
                                {
                                    custid = Reader["custid"].ToString().ToUpper().Trim();
                                    walletno = Reader["walletno"].ToString().ToUpper().Trim();
                                    mobileno = Reader["mobileno"].ToString().ToUpper().Trim();
                                    emailadd = Reader["emailaddress"].ToString().ToUpper().Trim();
                                    birthdate = Reader["birthdate"].ToString().ToUpper().Trim();
                                    gender = Reader["gender"].ToString().ToUpper().Trim();
                                    gender = (gender == "F" || gender == "2") ? "FEMALE" : "MALE";
                                    username = Reader["username"].ToString().ToUpper().Trim();
                                    fname = (Reader["firstname"] == System.DBNull.Value) ? "" : Reader["firstname"].ToString().Trim().ToUpper().Replace("Ñ", "N");
                                    mname = (Reader["middlename"] == System.DBNull.Value) ? "" : Reader["middlename"].ToString().Trim().ToUpper().Replace("Ñ", "N");
                                    lname = (Reader["lastname"] == System.DBNull.Value) ? "" : Reader["lastname"].ToString().Trim().ToUpper().Replace("Ñ", "N");

                                    street = (Reader["street"] == System.DBNull.Value) ? "" : Reader["street"].ToString().Trim().ToUpper();
                                    provincecity = (Reader["provincecity"] == System.DBNull.Value) ? "" : Reader["provincecity"].ToString().Trim().ToUpper();
                                    country = (Reader["country"] == System.DBNull.Value) ? "" : Reader["country"].ToString().Trim().ToUpper();
                                    zipcode = (Reader["zipcode"] == System.DBNull.Value) ? "" : Reader["zipcode"].ToString().Trim().ToUpper();
                                    branchid = (Reader["branchname"] == System.DBNull.Value) ? "" : Reader["branchname"].ToString().Trim().ToUpper();
                                    dtcreated = (Reader["dtcreated"] == System.DBNull.Value) ? "" : Reader["dtcreated"].ToString().Trim().ToUpper();
                                    dtmodified = (Reader["dtmodified"] == System.DBNull.Value) ? "" : Reader["dtmodified"].ToString().Trim().ToUpper();
                                    natureofwork = (Reader["natureofwork"] == System.DBNull.Value) ? "" : Reader["natureofwork"].ToString().Trim().ToUpper();
                                    permanentaddress = (Reader["permanentaddress"] == System.DBNull.Value) ? "" : Reader["permanentaddress"].ToString().Trim().ToUpper();
                                    nationality = (Reader["nationality"] == System.DBNull.Value) ? "" : Reader["nationality"].ToString().Trim().ToUpper();
                                    if (iscorp == true)
                                    {
                                        iscorporatepo = true;
                                    }
                                }
                            }
                            else { iscorporatepo = false; }
                            Reader.Close();
                            mycon.Close();
                        }
                    }
                }
            }
            catch (Exception ex) { cust.WriteToFile("Get Wallet Info : " + ex.ToString()); }
        }
        public string operatorname(string username,int flag)
        {
            string uname = string.Empty;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                if (username != "")
                {
                    if (username.Substring(4).All(Char.IsNumber))
                    {
                        conn.connectdb("DomesticB");
                        using (MySqlConnection mycon = conn.getConnection())
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                cmd.CommandText = string.Format("select resourceid,fullname from (SELECT resourceid,fullname FROM kpusers.branchusers WHERE  (resourceid='{0}' and '{0}' REGEXP '^[0-9]+$')   limit 1 " +
                                    " union SELECT resourceid,fullname FROM kpusers.adminbranchusers WHERE ( resourceid='{0}' and '{0}' REGEXP '^[0-9]+$')  limit 1)x group by resourceid limit 1;", username.Substring(4));
                                using (MySqlDataReader Reader = cmd.ExecuteReader())
                                {
                                    if (!Reader.HasRows)
                                    {
                                        uname = username.ToUpper();
                                    }
                                    else
                                    {
                                        Reader.Read();
                                        uname = "";
                                        if (flag == 1 && username != "") { uname = Reader["fullname"].ToString().Trim().Replace("Ñ", "N"); }

                                    }
                                    Reader.Close();
                                    mycon.Close();
                                }
                            }
                        }


                        conn.connectdb("GlobalB");
                        using (MySqlConnection mycon = conn.getConnection())
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                cmd.CommandText = string.Format("select resourceid,fullname from (SELECT resourceid,fullname FROM kpusersglobal.branchusers WHERE  (resourceid='{0}' and '{0}' REGEXP '^[0-9]+$')   limit 1 " +
                                    " union SELECT resourceid,fullname FROM kpusersglobal.adminusers WHERE ( resourceid='{0}' and '{0}' REGEXP '^[0-9]+$')  limit 1)x group by resourceid limit 1;", username.Substring(4));
                                using (MySqlDataReader Reader = cmd.ExecuteReader())
                                {
                                    if (!Reader.HasRows)
                                    {
                                        uname = username.ToUpper();
                                    }
                                    else
                                    {
                                        Reader.Read();
                                        uname = "";
                                        if (flag == 1 && username != "") { uname = Reader["fullname"].ToString().Trim().Replace("Ñ", "N"); }

                                    }
                                    Reader.Close();
                                    mycon.Close();
                                }
                            }
                        }
                         
                    }
                    else
                    {
                        conn.connectdb("MobileB");
                        using (MySqlConnection mycon = conn.getConnection())
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                cmd.CommandText = string.Format("SELECT concat(firstname,' ',middlename,' ',lastname) as fullname FROM kpmobile.mobileaccounts  " +
                                      " WHERE lower(username)='{0}'  limit 1;", username.Trim().ToLower());

                                using (MySqlDataReader Reader = cmd.ExecuteReader())
                                {
                                    if (!Reader.HasRows)
                                    {
                                        uname = username.ToUpper();
                                    }
                                    else
                                    {
                                        Reader.Read();
                                        uname = "";
                                        if (flag == 1 && username != "") { uname = Reader["fullname"].ToString().Trim().ToUpper().Replace("Ñ", "N"); }

                                    }
                                    Reader.Close();
                                    mycon.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { cust.WriteToFile("Get Operator Name : " + ex.ToString()); }
            return uname;
        }
        public string corpoperator(string username, int flag,string accountid)
        {
            string uname = string.Empty;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                if (username != null || username != "")
                {
                    for (int i = 0; i <= 2;i++ ) 
                    {
                        if (i == 0) { conn.connectdb("APIB"); }
                        else if (i == 1) { conn.connectdb("RuralNetB"); }
                        else { conn.connectdb("APINewB"); }

                        using (MySqlConnection mycon = conn.getConnection())
                        {
                            mycon.Open();
                            using (cmd = mycon.CreateCommand())
                            {
                                if (i == 1) {
                                    cmd.CommandText = string.Format("select accountid as fullname from kpadminpartners.accountcredential WHERE  userid='{0}' and accountid='{1}'  limit 1 ;", username, accountid);
                                }
                                else
                                {
                                    cmd.CommandText = string.Format("select concat(firstname,' ',middlename,' ',lastname) as fullname from kpadminpartners.partnersusers WHERE  userid='{0}' and accountid='{1}'  limit 1 ;", username, accountid);
                                }
                                using (MySqlDataReader Reader = cmd.ExecuteReader())
                                {
                                    if (!Reader.HasRows)
                                    {
                                        uname = username.ToUpper();
                                    }
                                    else
                                    {
                                        Reader.Read();
                                        uname = "";
                                        if (flag == 1 && username != "") { uname = Reader["fullname"].ToString().Trim().Replace("Ñ", "N"); }

                                    }
                                    Reader.Close();
                                    mycon.Close();
                                }
                            }
                        }
                    }
                }
                else
                {
                    uname = "";
                }
            }
            catch (Exception ex) { cust.WriteToFile("Get Operator Name : " + ex.ToString()); }
            return uname;
        }
    }

}