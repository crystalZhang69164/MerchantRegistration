using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using Utilities;
using System.Security.Cryptography;
using System.Text;

namespace MerchantRegistration
{
    public partial class MerchantRegistration : System.Web.UI.Page
    {
        DBConnect dbConnect = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        string apiKey;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            //create a random number generator to generate
            //a apiKey
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);
            RemoveSpecialChars(apiKey);

            //make a str that contains all chars they u think should be
            //in a api key
            //iterate through the key and see if the key[i]
            //is a char that is in the new str
            //if it is then add that char into a new string

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_CreateMerchant";

            objCommand.Parameters.AddWithValue("@email", email);
            objCommand.Parameters.AddWithValue("@name", name);
            objCommand.Parameters.AddWithValue("@password", password);
            objCommand.Parameters.AddWithValue("@apiKey", apiKey);


            dbConnect.DoUpdateUsingCmdObj(objCommand);

            Response.Write("Acc created");

        }

        //method that removes special chars from a string
        public static string RemoveSpecialChars(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9') || (str[i] >= 'A' && str[i] <= 'Z' 
                    && str[i] >= 'a' && str[i] <= 'z'))
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }
    }
}