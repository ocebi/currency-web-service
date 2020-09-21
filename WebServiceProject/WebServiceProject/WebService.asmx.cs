using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

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
        string currencyNames = "";


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetCurrencyNames()
        {
            LoadCurrencyInfo();
            return currencyNames;
        }
        
        public void LoadCurrencyInfo()
        {

            string user_id = "SYSTEM";
            string user_password = "1234";
            string data_source = "localhost:1521 / xe";

            string conString = "User Id=" + user_id + "; password=" + user_password + ";" +
            "Data Source=" + data_source + "; Pooling=false;";

            OracleConnection con = new OracleConnection();
            con.ConnectionString = conString;
            con.Open();

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from currency";

            OracleDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
            {
                //Console.WriteLine("Result: " + odr.GetString(1)); //prints currency name only
                currencyNames += odr.GetString(1) + "\n";
            }
            //Console.WriteLine("Read succesfull.");
        }
    }
}
