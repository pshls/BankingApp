using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace BankSemiFinal1
{
    class BankAccounts
    {



        public static decimal BalanceCalculation(int user_id)
        {
            string connectionString = @"Server=LENOVO-PC\SQLEXPRESS;Database=afdemp_csharp_1;Trusted_Connection=True;";
            SqlConnection sqlconn = new SqlConnection(connectionString);

            decimal balance = 0;
            try
            {
                sqlconn.Open();
                SqlCommand cmd = new SqlCommand("SELECT  TOP 1 amount FROM accounts where user_id=@user_id ORDER BY id DESC  ", sqlconn);
                cmd.Parameters.AddWithValue("@user_id", user_id);

                balance = (decimal)cmd.ExecuteScalar();
                sqlconn.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return balance;

        }




        public static int SelectUserIdToTransfer(string otherUser)
        {
            string connectionString = @"Server=LENOVO-PC\SQLEXPRESS;Database=afdemp_csharp_1;Trusted_Connection=True;";
            SqlConnection sqlconn = new SqlConnection(connectionString);

            int otherUser_id = 0;


            using (sqlconn)
            {
                try
                {

                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT users.id FROM users  WHERE username =@username", sqlconn);
                    cmd.Parameters.AddWithValue("@username", otherUser);

                    otherUser_id = (int)cmd.ExecuteScalar();
                    sqlconn.Close();

                }
                catch (Exception ex)
                {

                    //Console.WriteLine(ex.Message);
                }
                


            }


            return otherUser_id;

        }




        public static decimal DepositToOtherAccount(int user_id,decimal deposit_Amount,int otherUser_id)
        {          
            try
            {              
                DateTime transaction_Date = DateTime.Now;

                decimal balanceUser1 = BalanceCalculation(user_id);
                if(deposit_Amount > balanceUser1)
                {
                    Console.WriteLine("Not enough money in your account");
                    deposit_Amount = 0;
                }
                else
                {
                    string query1 = "INSERT INTO accounts VALUES(" +
                    user_id + ",'" + transaction_Date + "', " + (balanceUser1 - deposit_Amount) + ") ";
                    decimal balanceUser2 = BalanceCalculation(otherUser_id);
                    string query = "INSERT INTO accounts VALUES(" +
                        otherUser_id + ",'" + transaction_Date + "', " + (balanceUser2 + deposit_Amount) + ") ";
                    string connectionString = @"Server=LENOVO-PC\SQLEXPRESS;Database=afdemp_csharp_1;Trusted_Connection=True;";
                    SqlConnection sqlconn = new SqlConnection(connectionString);
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlconn);
                    SqlCommand cmd1 = new SqlCommand(query1, sqlconn);
                    int result = cmd.ExecuteNonQuery();
                    int result1 = cmd1.ExecuteNonQuery();
                   
                    sqlconn.Close();
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return deposit_Amount;


        }




        public static decimal WithdrawFromOtherAccount(int user_id, decimal withdraw_Amount, int otherUser_id)
        {
            try
            {
                DateTime transaction_Date = DateTime.Now;
                decimal balanceUser1 = BalanceCalculation(otherUser_id);
                if (withdraw_Amount > balanceUser1)
                {
                    Console.WriteLine("Not enough money from other user account");
                    withdraw_Amount = 0;
                }
                else
                {
                    string query1 = "INSERT INTO accounts VALUES(" +
                    otherUser_id + ",'" + transaction_Date + "', " + (balanceUser1 - withdraw_Amount) + ") ";
                    decimal balanceUser2 = BalanceCalculation(user_id);
                    string query = "INSERT INTO accounts VALUES(" +
                        user_id + ",'" + transaction_Date + "', " + (balanceUser2 + withdraw_Amount) + ") ";
                    string connectionString = @"Server=LENOVO-PC\SQLEXPRESS;Database=afdemp_csharp_1;Trusted_Connection=True;";
                    SqlConnection sqlconn = new SqlConnection(connectionString);
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlconn);
                    SqlCommand cmd1 = new SqlCommand(query1, sqlconn);
                    int result = cmd.ExecuteNonQuery();
                    int result1 = cmd1.ExecuteNonQuery();
                   

                    sqlconn.Close();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return withdraw_Amount;


        }





        public static void SaveTXTFile(string[] todaysTransactionsToTXT)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            string path = String.Format(@"c:\c#\Bootcamp banking project\Transactions\{0}\", time);
            string filename = "transaction.txt";

            if (!string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                File.WriteAllLines(path + filename, todaysTransactionsToTXT, Encoding.UTF8);
            }
            else
            {
                string[] appendText = todaysTransactionsToTXT ;
                File.AppendAllLines(path + filename, appendText, Encoding.UTF8);
            }
        }



        
            

    }
}
