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
    public class MenuRep
    {
        private SqlConnection con;
        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddNewMenu(MenuModel obj)
        {
            try
            {
                //SendMail(obj.EmailID, RandomOTP, obj.Name);
                connection();
                SqlCommand com = new SqlCommand("spr_AddMenuList", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@FoodName", obj.FoodName);
                com.Parameters.AddWithValue("@Description", obj.Description);
                com.Parameters.AddWithValue("@Price", obj.Price);

                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MenuModel> GetMenuList()
        {
            connection();
            List<MenuModel> MenuList = new List<MenuModel>();
            SqlCommand com = new SqlCommand("spr_GetMenuList", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (var item in list)
            {
                MenuList.Add(
                    new MenuModel
                    {
                        FoodName = Convert.ToString(item["FoodName"]),
                        Description = Convert.ToString(item["Description"]),
                        Price = Convert.ToInt16(item["Price"]),
                        ID = Convert.ToInt16(item["ID"])
                    }
                    );
            }
            return MenuList;
        }

        public bool EditMenu(MenuModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("spr_EditMenu", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ID", obj.ID);
            com.Parameters.AddWithValue("@FoodName", obj.FoodName);
            com.Parameters.AddWithValue("@Description", obj.Description);
            com.Parameters.AddWithValue("@Price", obj.Price);
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
        //To delete Employee details
        public bool DeleteMenu(int Id)
        {
            connection();
            SqlCommand com = new SqlCommand("spr_DeleteMenuById", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", Id);
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