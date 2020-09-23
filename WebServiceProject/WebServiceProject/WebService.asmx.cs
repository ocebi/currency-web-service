using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Newtonsoft.Json;

namespace WebServiceProject
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        DataTable dtCurrencies = new DataTable();

        [WebMethod]
        public string GetCurrencyNames(string date)
        {
            LoadCurrencyInfo(date);
            return JsonConvert.SerializeObject(dtCurrencies);
        }
        
        public void LoadCurrencyInfo(string date) //check if date has numbers only
        {
            dtCurrencies.Reset();
            dtCurrencies.Columns.Add("cname");
            dtCurrencies.Columns.Add("unit");
            dtCurrencies.Columns.Add("forexbuy");
            dtCurrencies.Columns.Add("forexsell");
            dtCurrencies.Columns.Add("banknotebuy");
            dtCurrencies.Columns.Add("banknotesell");
            dtCurrencies.Columns.Add("crossrateusd");

            string user_id = "SYSTEM";
            string user_password = "1234";
            string data_source = "localhost:1521 / xe";

            string conString = "User Id=" + user_id + "; password=" + user_password + ";" +
            "Data Source=" + data_source + "; Pooling=false;";

            OracleConnection con = new OracleConnection();
            con.ConnectionString = conString;
            con.Open();

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from currency where cdate=to_date('" + date + "' , 'dd.mm.yyyy')";

            OracleDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
            {
                List<string> tempStringList = new List<string>();
                for(int i=1;i<8;++i)
                {
                    tempStringList.Add(odr.IsDBNull(i) ? "null" : odr.GetString(i));
                }
                dtCurrencies.Rows.Add(tempStringList[0],
                    tempStringList[1],
                    tempStringList[2],
                    tempStringList[3],
                    tempStringList[4],
                    tempStringList[5],
                    tempStringList[6]);
            }
            
            if(dtCurrencies.Rows.Count == 0)
            {
                dtCurrencies.Rows.Add("null", "null", "null", "null", "null", "null", "null");
            }
            
        }
    }
}
