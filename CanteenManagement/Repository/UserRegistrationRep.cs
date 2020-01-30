using CanteenManagement.Models;
using CanteenManagementwithAdoDotNet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace CanteenManagementwithAdoDotNet.Repository
{
    public class UserRegistrationRep
    {
        private SqlConnection con;
        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddNewUser(UserRegistration obj)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string RandomOTP = GenerateRandomOTP(4, saAllowedCharacters);

            //SendMail(obj.EmailID, RandomOTP, obj.Name);
            connection();
            SqlCommand com = new SqlCommand("spr_UserRegistration", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Address", obj.Address);
            com.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
            com.Parameters.AddWithValue("@EmailID", obj.EmailID);
            com.Parameters.AddWithValue("@RandomOTP", RandomOTP);
            com.Parameters.AddWithValue("@Password", obj.Password);

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

        public string SendMail(string ToEmailID, string RandomOTP, string Name)
        {
            string FromEmailID = WebConfigurationManager.AppSettings["FromEmailID"];
            string FromEmailPassword = WebConfigurationManager.AppSettings["FromEmailPassword"];
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            string from = string.Empty;
            string subject = "User Registration with OTP " + RandomOTP;

            try
            {
                StringBuilder mailBody = new StringBuilder();
                mailBody.AppendFormat("Dear " + Name + " ,");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<p>Use below OTP to login for the first time</p>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat(RandomOTP);

                MailAddress fromAddress = new MailAddress(FromEmailID);
                message.From = fromAddress;
                message.To.Add(ToEmailID);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = Convert.ToString(mailBody);
                smtpClient.Host = "smtp.gmail.com";   // We use gmail as our smtp client
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(FromEmailID, FromEmailPassword);

                smtpClient.Send(message);
                msg = "Successful<BR>";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < iOTPLength; i++)

            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }

            return sOTP;
        }
        public bool IsConnectedToInternet()
        {
            string host = "https://www.google.com";
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return result;
        }
    }
}