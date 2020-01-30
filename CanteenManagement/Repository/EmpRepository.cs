﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CanteenManagement.Models;
using System.Linq;

namespace CanteenManagement.Repository
{
    public class EmpRepository
    {

        private SqlConnection con;
        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }
        //To Add Employee details
        public bool AddEmployee(EmpModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddNewEmpDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Address);
          
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
        //To view employee details with generic list 
        public List<EmpModel> GetAllEmployees()
        {
            connection();
            List<EmpModel> EmpList =new List<EmpModel>();
            SqlCommand com = new SqlCommand("GetEmployees", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            //Uncooment if convert Datatable to list using AsEnumerable
            //List<DataRow> list = dt.AsEnumerable().ToList();
            //foreach (var item in list)
            //{
            //    EmpList.Add(

            //       new EmpModel
            //       {
            //           Empid = Convert.ToInt32(item["Id"]),
            //           Name = Convert.ToString(item["Name"]),
            //           City = Convert.ToString(item["City"]),
            //           Address = Convert.ToString(item["Address"])

            //       });
            //}

            //Uncomment if you wants to Bind EmpModel generic list using LINQ 
            //EmpList = (from DataRow dr in dt.Rows

            //        select new EmpModel()
            //        {
            //            Empid = Convert.ToInt32(dr["Id"]),
            //            Name = Convert.ToString(dr["Name"]),
            //            City = Convert.ToString(dr["City"]),
            //            Address = Convert.ToString(dr["Address"])
            //        }).ToList();


            //  Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
                {

                    EmpList.Add(

                        new EmpModel
                        {

                            Empid = Convert.ToInt32(dr["Id"]),
                            Name = Convert.ToString(dr["Name"]),
                            City = Convert.ToString(dr["City"]),
                            Address = Convert.ToString(dr["Address"])

                        }


                        );


                }

            return EmpList;


        }
        //To Update Employee details
        public bool UpdateEmployee(EmpModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdateEmpDetails", con);
           
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", obj.Empid);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@City", obj.City);
            com.Parameters.AddWithValue("@Address", obj.Address);
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
        public bool DeleteEmployee(int Id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeleteEmpById", con);

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