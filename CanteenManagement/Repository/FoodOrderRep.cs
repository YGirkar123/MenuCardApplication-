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
    public class FoodOrderRep
    {
        private SqlConnection con;
        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }
        //To delete Employee details
        public bool AddToCart(int Id, string UserName, int Quantity)
        {
            connection();
            MenuModel mMenuModel = new MenuModel();
            SqlCommand com = new SqlCommand("spr_AddCart", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@FoodID", Id);
            com.Parameters.AddWithValue("@UserName", UserName);
            com.Parameters.AddWithValue("@Quantity", Quantity);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<FoodOrderModel> GetOrderByStatus(string UserName, string Status, string UserType)
        {
            int Total = 0;
            connection();
            List<FoodOrderModel> FoodOrderModel = new List<FoodOrderModel>();
            SqlCommand com = new SqlCommand("spr_GetOrderByStatus", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserType", UserType);
            com.Parameters.AddWithValue("@Status", Status);
            com.Parameters.AddWithValue("@UserName", UserName);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (var item in list)
            {
                FoodOrderModel.Add(
                    new FoodOrderModel
                    {
                        FoodName = Convert.ToString(item["FoodName"]),
                        OtherDetails = Convert.ToString(item["Description"]),
                        OrderStatus = Convert.ToString(item["OrderStatus"]),
                        ID = Convert.ToInt16(item["ID"]),
                        Amount = Convert.ToInt16(item["Price"])
                    }
                    );
            }
            return FoodOrderModel;
        }

        public bool DeleteFromCart(int Id)
        {
            connection();
            SqlCommand com = new SqlCommand("spr_DeleteFromCart", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PlaceOrder(String UserName)
        {
            connection();
            SqlCommand com = new SqlCommand("spr_PlaceOrder", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserName", UserName);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}