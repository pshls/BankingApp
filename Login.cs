using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace BankSemiFinal1
{
    class Login
    {
        public static string EnterUserName()
        {

            Console.Write("Please enter User Name : ");

            string user_name=Console.ReadLine();      

            return user_name;
        }


        public static string EnterUserPassword()
        {
            Console.Write("Please enter Password : ");
            ConsoleKeyInfo key; string user_password = "";
            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    user_password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && user_password.Length > 0)
                    {
                        user_password = user_password.Substring(0, (user_password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return user_password;
        }



        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }



        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }



        public static int CheckUser(string user_name, string user_password)
        {

            string connectionString = @"Server=LENOVO-PC\SQLEXPRESS;Database=afdemp_csharp_1;Trusted_Connection=True;";
            SqlConnection sqlconn = new SqlConnection(connectionString);

            int checkUser_id = 0;

            using (sqlconn)
            {
                try
                {
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT users.id FROM users  WHERE username =@username AND password=@password", sqlconn);
                    cmd.Parameters.AddWithValue("@username", user_name);
                    cmd.Parameters.AddWithValue("@password", user_password);
                    checkUser_id = (int)cmd.ExecuteScalar();
                    sqlconn.Close();

                }
                catch (Exception ex)
                {

                    //Console.WriteLine(ex.Message);
                }

            }

            return checkUser_id;
        }



        public static int LoginValidation(int user_id, string user_name, string user_password)
        {

            int i = 0;

            while (user_id == 0 && i < 2)
            {

                Console.WriteLine("Invalid inputs, {0} tries left ", 2 - i);
                user_name = Login.EnterUserName();
                user_password = Login.EnterUserPassword();
                string hashPassword = Login.GenerateSHA256String(user_password);
                user_id = Login.CheckUser(user_name, hashPassword);


                i++;
            }


            return user_id;


        }

    }
}
