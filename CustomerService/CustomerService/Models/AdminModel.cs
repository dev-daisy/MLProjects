using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerService.Models
{
    public class AdminModel 
    {
        public IEnumerable<TxnCount> TXNCountList { get; set; }
        public IEnumerable<CustTxn> CustTxnList { get; set; }


        public List<SelectListItem> branch()
        {
            List<SelectListItem> BranchList = new List<SelectListItem>();

            Connection conn = new Connection();
            CustomerServiceModel cust = new CustomerServiceModel();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.connectdb("DomesticB");
                using (MySqlConnection mycon = conn.getConnection())
                {
                    mycon.Open();
                    using (cmd = mycon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT zonecode,branchcode FROM kpusers.branches group by zonecode,regioncode,areacode,branchcode";
                        using (MySqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                while (Reader.Read()){
                                    BranchList.Add(new SelectListItem { Text = Reader["branchcode"].ToString().ToUpper(), Value = Reader["zonecode"].ToString().ToUpper() });
                                }
                            }
                            Reader.Close();
                            mycon.Close();
                        }
                    }
                }
            }
            catch (Exception ex) { cust.WriteToFile("Get All Branch : " + ex.ToString()); }
            return BranchList;
        }

        public DataTable dtable()
        {
            DataTable dt = new DataTable();
            DataColumn id = new DataColumn( "id",System.Type.GetType("System.Int32"));
            id.AutoIncrement = true;
            id.AutoIncrementSeed = 1;
            id.AutoIncrementStep = 1;
            dt.Columns.Add(id);
            DataColumn emailadd = new DataColumn("emailadd", System.Type.GetType("System.String"));
            emailadd.AllowDBNull = true;
            dt.Columns.Add(emailadd);
            DataColumn birthdate = new DataColumn("birthdate", System.Type.GetType("System.String"));
            birthdate.AllowDBNull = true;
            dt.Columns.Add(birthdate);
            DataColumn mobileno = new DataColumn("mobileno", System.Type.GetType("System.String"));
            mobileno.AllowDBNull = true;
            dt.Columns.Add(mobileno);
            DataColumn address = new DataColumn("address", System.Type.GetType("System.String"));
            address.AllowDBNull = true;
            dt.Columns.Add(address);
            DataColumn gender = new DataColumn("gender", System.Type.GetType("System.String"));
            gender.AllowDBNull = true;
            dt.Columns.Add(gender);
            DataColumn refno = new DataColumn("refno", System.Type.GetType("System.String"));
            refno.AllowDBNull = true;
            dt.Columns.Add(refno);
            DataColumn kptn = new DataColumn("kptn", System.Type.GetType("System.String"));
            kptn.AllowDBNull = true;
            dt.Columns.Add(kptn);
            DataColumn transdate = new DataColumn("transdate", System.Type.GetType("System.String"));
            transdate.AllowDBNull = true;
            dt.Columns.Add(transdate);
            DataColumn claimeddate = new DataColumn("claimeddate", System.Type.GetType("System.String"));
            claimeddate.AllowDBNull = true;
            dt.Columns.Add(claimeddate);
            DataColumn amount = new DataColumn("amount", System.Type.GetType("System.String"));
            amount.AllowDBNull = true;
            dt.Columns.Add(amount);
            DataColumn charge = new DataColumn("charge", System.Type.GetType("System.String"));
            charge.AllowDBNull = true;
            dt.Columns.Add(charge);
            DataColumn commission = new DataColumn("commission", System.Type.GetType("System.String"));
            commission.AllowDBNull = true;
            dt.Columns.Add(commission);
            DataColumn zcode = new DataColumn("zcode", System.Type.GetType("System.String"));
            zcode.AllowDBNull = true;
            dt.Columns.Add(zcode);
            DataColumn zone = new DataColumn("zone", System.Type.GetType("System.String"));
            zone.AllowDBNull = true;
            dt.Columns.Add(zone);
            DataColumn senderbranch = new DataColumn("senderbranch", System.Type.GetType("System.String"));
            senderbranch.AllowDBNull = true;
            dt.Columns.Add(senderbranch);
            DataColumn receiverbranch = new DataColumn("receiverbranch", System.Type.GetType("System.String"));
            receiverbranch.AllowDBNull = true;
            dt.Columns.Add(receiverbranch);
            DataColumn receivername = new DataColumn("receivername", System.Type.GetType("System.String"));
            receivername.AllowDBNull = true;
            dt.Columns.Add(receivername);
            DataColumn partnername = new DataColumn("partnername", System.Type.GetType("System.String"));
            partnername.AllowDBNull = true;
            dt.Columns.Add(partnername);
            DataColumn datemodified = new DataColumn("datemodified", System.Type.GetType("System.String"));
            datemodified.AllowDBNull = true;
            dt.Columns.Add(datemodified);
            DataColumn sendername = new DataColumn("sendername", System.Type.GetType("System.String"));
            sendername.AllowDBNull = true;
            dt.Columns.Add(sendername);
            DataColumn custid = new DataColumn("custid", System.Type.GetType("System.String"));
            custid.AllowDBNull = true;
            dt.Columns.Add(custid);
            DataColumn lname = new DataColumn("lname", System.Type.GetType("System.String"));
            lname.AllowDBNull = true;
            dt.Columns.Add(lname);
            DataColumn fname = new DataColumn("fname", System.Type.GetType("System.String"));
            fname.AllowDBNull = true;
            dt.Columns.Add(fname);
            DataColumn mname = new DataColumn("mname", System.Type.GetType("System.String"));
            mname.AllowDBNull = true;
            dt.Columns.Add(mname);
            DataColumn username = new DataColumn("username", System.Type.GetType("System.String"));
            username.AllowDBNull = true;
            dt.Columns.Add(username);
            DataColumn walletno = new DataColumn("walletno", System.Type.GetType("System.String"));
            walletno.AllowDBNull = true;
            dt.Columns.Add(walletno);
            DataColumn txntype = new DataColumn("txntype", System.Type.GetType("System.String"));
            txntype.AllowDBNull = true;
            dt.Columns.Add(txntype);
            DataColumn oldkptn = new DataColumn("oldkptn", System.Type.GetType("System.String"));
            oldkptn.AllowDBNull = true;
            dt.Columns.Add(oldkptn);
            DataColumn ptn = new DataColumn("ptn", System.Type.GetType("System.String"));
            ptn.AllowDBNull = true;
            dt.Columns.Add(ptn);
            DataColumn oldptn = new DataColumn("oldptn", System.Type.GetType("System.String"));
            oldptn.AllowDBNull = true;
            dt.Columns.Add(oldptn);
            DataColumn accountcode = new DataColumn("accountcode", System.Type.GetType("System.String"));
            accountcode.AllowDBNull = true;
            dt.Columns.Add(accountcode);
            DataColumn cancelreason = new DataColumn("cancelreason", System.Type.GetType("System.String"));
            cancelreason.AllowDBNull = true;
            dt.Columns.Add(cancelreason);
            DataColumn cancelleddate = new DataColumn("cancelleddate", System.Type.GetType("System.String"));
            cancelleddate.AllowDBNull = true;
            dt.Columns.Add(cancelleddate);
            DataColumn controlno = new DataColumn("controlno", System.Type.GetType("System.String"));
            controlno.AllowDBNull = true;
            dt.Columns.Add(controlno);
            DataColumn irno = new DataColumn("irno", System.Type.GetType("System.String"));
            irno.AllowDBNull = true;
            dt.Columns.Add(irno);
            DataColumn orno = new DataColumn("orno", System.Type.GetType("System.String"));
            orno.AllowDBNull = true;
            dt.Columns.Add(orno);
            DataColumn operatorid = new DataColumn("operatorid", System.Type.GetType("System.String"));
            operatorid.AllowDBNull = true;
            dt.Columns.Add(operatorid);
            DataColumn isremote = new DataColumn("isremote", System.Type.GetType("System.String"));
            isremote.AllowDBNull = true;
            dt.Columns.Add(isremote);
            DataColumn remotebranch = new DataColumn("remotebranch", System.Type.GetType("System.String"));
            remotebranch.AllowDBNull = true;
            dt.Columns.Add(remotebranch);
            DataColumn remoteoperatorid = new DataColumn("remoteoperatorid", System.Type.GetType("System.String"));
            remoteoperatorid.AllowDBNull = true;
            dt.Columns.Add(remoteoperatorid);
            DataColumn othercharge = new DataColumn("othercharge", System.Type.GetType("System.String"));
            othercharge.AllowDBNull = true;
            dt.Columns.Add(othercharge);
            DataColumn branchcode = new DataColumn("branchcode", System.Type.GetType("System.String"));
            branchcode.AllowDBNull = true;
            dt.Columns.Add(branchcode);
            DataColumn cancelledbyoperatorid = new DataColumn("cancelledbyoperatorid", System.Type.GetType("System.String"));
            cancelledbyoperatorid.AllowDBNull = true;
            dt.Columns.Add(cancelledbyoperatorid);
            DataColumn cancelledbybranchcode = new DataColumn("cancelledbybranchcode", System.Type.GetType("System.String"));
            cancelledbybranchcode.AllowDBNull = true;
            dt.Columns.Add(cancelledbybranchcode);
            DataColumn cancelledbyzonecode = new DataColumn("cancelledbyzonecode", System.Type.GetType("System.String"));
            cancelledbyzonecode.AllowDBNull = true;
            dt.Columns.Add(cancelledbyzonecode);
            DataColumn canceldetails = new DataColumn("canceldetails", System.Type.GetType("System.String"));
            canceldetails.AllowDBNull = true;
            dt.Columns.Add(canceldetails);
            DataColumn cancelcharge = new DataColumn("cancelcharge", System.Type.GetType("System.String"));
            cancelcharge.AllowDBNull = true;
            dt.Columns.Add(cancelcharge);
            DataColumn chargeto = new DataColumn("chargeto", System.Type.GetType("System.String"));
            chargeto.AllowDBNull = true;
            dt.Columns.Add(chargeto);
            DataColumn remotezonecode = new DataColumn("remotezonecode", System.Type.GetType("System.String"));
            remotezonecode.AllowDBNull = true;
            dt.Columns.Add(remotezonecode);
            DataColumn currency = new DataColumn("currency", System.Type.GetType("System.String"));
            currency.AllowDBNull = true;
            dt.Columns.Add(currency);
            DataColumn stationid = new DataColumn("stationid", System.Type.GetType("System.String"));
            stationid.AllowDBNull = true;
            dt.Columns.Add(stationid);
            DataColumn maturitydate = new DataColumn("maturitydate", System.Type.GetType("System.String"));
            maturitydate.AllowDBNull = true;
            dt.Columns.Add(maturitydate);
            DataColumn expirydate = new DataColumn("expirydate", System.Type.GetType("System.String"));
            expirydate.AllowDBNull = true;
            dt.Columns.Add(expirydate);
            DataColumn advanceinteres = new DataColumn("advanceinterest", System.Type.GetType("System.String"));
            advanceinteres.AllowDBNull = true;
            dt.Columns.Add(advanceinteres);
            DataColumn itemdesc = new DataColumn("itemdesc", System.Type.GetType("System.String"));
            itemdesc.AllowDBNull = true;
            dt.Columns.Add(itemdesc);
            DataColumn quantity = new DataColumn("quantity", System.Type.GetType("System.String"));
            quantity.AllowDBNull = true;
            dt.Columns.Add(quantity);
            DataColumn weight = new DataColumn("weight", System.Type.GetType("System.String"));
            weight.AllowDBNull = true;
            dt.Columns.Add(weight);
            DataColumn karat = new DataColumn("karat", System.Type.GetType("System.String"));
            karat.AllowDBNull = true;
            dt.Columns.Add(karat);
            DataColumn carat = new DataColumn("carat", System.Type.GetType("System.String"));
            carat.AllowDBNull = true;
            dt.Columns.Add(carat);
            DataColumn appraiser = new DataColumn("appraiser", System.Type.GetType("System.String"));
            appraiser.AllowDBNull = true;
            dt.Columns.Add(appraiser);
            DataColumn operatorname = new DataColumn("operator", System.Type.GetType("System.String"));
            operatorname.AllowDBNull = true;
            dt.Columns.Add(operatorname);
            DataColumn discount = new DataColumn("discount", System.Type.GetType("System.String"));
            discount.AllowDBNull = true;
            dt.Columns.Add(discount);
            DataColumn itemcode = new DataColumn("itemcode", System.Type.GetType("System.String"));
            itemcode.AllowDBNull = true;
            dt.Columns.Add(itemcode);
            DataColumn accountname = new DataColumn("accountname", System.Type.GetType("System.String"));
            accountname.AllowDBNull = true;
            dt.Columns.Add(accountname);
            DataColumn payor = new DataColumn("payor", System.Type.GetType("System.String"));
            payor.AllowDBNull = true;
            dt.Columns.Add(payor);
            DataColumn otherdetails = new DataColumn("otherdetails", System.Type.GetType("System.String"));
            otherdetails.AllowDBNull = true;
            dt.Columns.Add(otherdetails);
            DataColumn purpose = new DataColumn("purpose", System.Type.GetType("System.String"));
            purpose.AllowDBNull = true;
            dt.Columns.Add(purpose);
            DataColumn sourceoffund = new DataColumn("sourceoffund", System.Type.GetType("System.String"));
            sourceoffund.AllowDBNull = true;
            dt.Columns.Add(sourceoffund);
            DataColumn servicetype = new DataColumn("servicetype", System.Type.GetType("System.String"));
            servicetype.AllowDBNull = true;
            dt.Columns.Add(servicetype);
            dt.PrimaryKey = new DataColumn[] { id };
            return dt;
        }
       
    }

    public class TxnCount : AdminModel
    {
        public string emailadd { get; set; }
        public string birthdate { get; set; }
        public string mobileno { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public Int64  totalcount { get; set; }
        public decimal totalamount { get; set; }
        public decimal totalcharge { get; set; }
        public decimal totalcommission { get; set; }
        public string datemodified { get; set; }
        public string custid { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string username { get; set; }
        public string walletno { get; set; }
        public string txntype { get; set; }
        public string categorytype { get; set; }
    }

    public class CustTxn : AdminModel
    {

        public string emailadd { get; set; }
        public string birthdate { get; set; }
        public string mobileno { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string refno { get; set; }
        public string kptn { get; set; }
        public string transdate { get; set; }
        public string claimeddate { get; set; }
        public decimal  amount { get; set; }
        public decimal charge { get; set; }
        public decimal commission { get; set; }
        public string zone { get; set; }
        public string senderbranch { get; set; }
        public string receiverbranch { get; set; }
        public string receivername { get; set; }
        public string partnername { get; set; }
        public string datemodified { get; set; }
        public string sendername { get; set; }
        public string custid { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string username { get; set; }
        public string walletno { get; set; }
        public string txntype { get; set; }
        public string oldkptn { get; set; }
        public string ptn { get; set; }
        public string oldptn { get; set; }
        public string accountcode { get; set; }
        public string cancelreason { get; set; }
        public string cancelleddate { get; set; }
        public string controlno { get; set; }
        public string irno { get; set; }
        public string orno { get; set; }
        public string operatorid { get; set; }
        public string isremote { get; set; }
        public string remotebranch { get; set; }
        public string remoteoperatorid { get; set; }
        public string othercharge { get; set; }
        public string branchcode { get; set; }
        public string cancelledbyoperatorid { get; set; }
        public string cancelledbybranchcode { get; set; }
        public string cancelledbyzonecode { get; set; }
        public string canceldetails { get; set; }
        public string cancelcharge { get; set; }
        public string chargeto { get; set; }
        public string remotezonecode { get; set; }
        public string currency { get; set; }
        public string stationid { get; set; }
        public string dateinserted { get; set; }
        public string zcode { get; set; }
        public string maturitydate { get; set; }
        public string expirydate { get; set; }
        public decimal advanceinterest { get; set; }
        public string itemdesc { get; set; }
        public int quantity { get; set; }
        public string weight { get; set; }
        public string karat { get; set; }
        public string carat { get; set; }
        public string appraiser { get; set; }
        public decimal discount { get; set; }
        public string itemcode { get; set; }
        public string accountname { get; set; }
        public string payor { get; set; }
        public string otherdetails { get; set; }
        public string mcno { get; set; }
        public string custname { get; set; }
        public string foreigncurrency { get; set; }
        public string exchangerate { get; set; }
        public string foreignamount { get; set; }
        public string purpose { get; set; }
        public string sourceoffund { get; set; }
        public string servicetype { get; set; }
    }


}