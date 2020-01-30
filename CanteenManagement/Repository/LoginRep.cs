using CanteenManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CanteenManagement.Repository
{
    public class LoginRep
    {
        private SqlConnection con;
        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }
        public List<LoginModel> GetMenuList()
        {
            connection();
            List<LoginModel> mLoginModel = new List<LoginModel>();
            SqlCommand com = new SqlCommand("spr_LoginDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (var item in list)
            {
                mLoginModel.Add(
                    new LoginModel
                    {
                        UserName = Convert.ToString(item["UserName"]),
                        Password = Convert.ToString(item["Password"]),
                        UserType = Convert.ToString(item["UserType"])
                    }
                    );
            }
            return mLoginModel;
        }

    }
}