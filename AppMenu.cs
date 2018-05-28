using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSemiFinal1
{
    class AppMenu
    {


        public static string SimpleUserMenu(int user_id)
        {
            Console.WriteLine("S I M P L E  U S E R   M E N U ");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("(1) : View your bank account ");
            Console.WriteLine("(2) : Deposit to another Member’s bank account ");
            Console.WriteLine("(3) : today’s transactions ");
            Console.WriteLine("(4) : Exit the application ");
            Console.WriteLine("-------------------------------");


            Console.Write("pick a number for relevant action:");
            string choice = Console.ReadLine();



            return choice;
        }



        public static string AdminUserMenu(int user_id)
        {
            Console.WriteLine("A D M I N I S T R A T O R   M E N U");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("(1) : View Cooperative’s (super admin) internal bank account ");
            Console.WriteLine("(2) : View Members’ bank accounts ");
            Console.WriteLine("(3) : Deposit to Member’s bank account ");
            Console.WriteLine("(4) : Withdraw from Member’s bank account ");
            Console.WriteLine("(5) : today’s transactions ");
            Console.WriteLine("(6) : Exit the application ");
            Console.WriteLine("-----------------------------------");

            Console.Write("pick a number for relevant action:");
            string choice = Console.ReadLine();


          
            return choice;

        }


        public static bool ExitMenu(string exitMenu)
        {
            bool flag = true;
           
            if (exitMenu == "e" || exitMenu == "E")
            {
                
                flag = false;

            }
            else
            {
               flag = true;

            }

            return flag;

        }




        





    }
}
