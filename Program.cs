using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace BankSemiFinal1
{
    class Program
    {
        static void Main(string[] args)
        {
           
            string userName = Login.EnterUserName();
            string userPassword = Login.EnterUserPassword();
            string hashPassword = Login.GenerateSHA256String(userPassword);            
            int checkUser_id = Login.CheckUser(userName, hashPassword);
            int user_id = Login.LoginValidation(checkUser_id, userName, hashPassword);
            decimal userBalance = BankAccounts.BalanceCalculation(user_id);
          
            User user1 = new User()
            {            
                User_account = new Account() { UserId = user_id, DateTransaction = DateTime.Now, Balance = userBalance}
            };
           
            bool flag = true;
            while(flag)
            {               
                if (user1.User_account.UserId == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Sorry invalid inputs");
                    flag = false;
                }
                else if (user1.User_account.UserId == 1)
                {
                    
                    Console.Clear();
                    string choice = AppMenu.AdminUserMenu(user_id);

                    while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5" && choice != "6")
                    {
                        Console.Write("you picked wrong No pls re-enter : ");
                        choice = (Console.ReadLine());
                    }
                    switch (choice)
                    {
                        case "6":
                            Console.WriteLine("Exit the application ");

                            flag=false;                          
                            break;

                        case "5":
                            var userTransactions = user1.User_account.transactions;

                            if (userTransactions.Count != 0)
                            {
                                Console.WriteLine("Your today's transactions are the following and sent to txt file: ");

                                foreach (var transaction in userTransactions)
                                {
                                    
                                    Console.WriteLine(transaction.ToString());
                                }
                            }
                          
                            List<string> trans = userTransactions.Select(tr => Convert.ToString(tr)).ToList();
                            string[] todaysTransactionsToTXT = trans.ToArray();

                            BankAccounts.SaveTXTFile(todaysTransactionsToTXT);
                            Console.Write("Press 'E' for exit app or any other key for back to Menu:");
                            var menu5 = Console.ReadLine();
                            flag = AppMenu.ExitMenu(menu5);
                            break;

                        case "4":

                            Console.Write("enter user name to withdraw:");
                            string otherUser = Console.ReadLine();

                            int otherUser_id = BankAccounts.SelectUserIdToTransfer(otherUser);

                            if (otherUser_id == 0 || otherUser_id == user_id)
                            {
                                Console.WriteLine("The user name does not exist :");
                            }
                            else
                            {
                                Console.Write("The amount you want to withdraw to {0} : ", otherUser);
                                decimal withdrawAmountFromSimpleMember = decimal.Parse(Console.ReadLine());
                                if (withdrawAmountFromSimpleMember <= 0  )
                                {
                                    Console.WriteLine("invalid amount :");
                                }
                                else
                                {
                                   var withdrawl= BankAccounts.WithdrawFromOtherAccount(user_id, withdrawAmountFromSimpleMember, otherUser_id);
                                    user1.User_account.Withdraw(withdrawl);
                                }
                                
                            }

                            Console.Write("Press 'E' for exit app or any other key for back to Menu:");
                            var menu4 = Console.ReadLine();
                            flag = AppMenu.ExitMenu(menu4);                           
                            break;

                        case "3":
                            Console.Write("enter user name to deposit:");
                             otherUser = Console.ReadLine();

                            otherUser_id = BankAccounts.SelectUserIdToTransfer(otherUser);

                            if (otherUser_id == 0 || otherUser_id == user_id)
                            {
                                Console.WriteLine("The user name does not exist :");
                            }
                            else
                            {
                                Console.Write("The amount you want to deposit to {0} : ", otherUser);
                                decimal depositAmountToSimpleMember = decimal.Parse(Console.ReadLine());
                                if (depositAmountToSimpleMember <= 0 )
                                {
                                    Console.WriteLine("invalid amount :");
                                }
                                else
                                {
                                    
                                    var deposit =BankAccounts.DepositToOtherAccount(user_id, depositAmountToSimpleMember, otherUser_id);
                                    user1.User_account.Deposit(deposit);
                                }

                            }

                            Console.Write("Press 'E' for exit app or any other key for back to Menu:");
                            var menu3 = Console.ReadLine();
                            flag = AppMenu.ExitMenu(menu3);                            
                            break;

                        case "2":
                            Console.Write("enter user name to see acc balance:");
                            otherUser = Console.ReadLine();

                            otherUser_id = BankAccounts.SelectUserIdToTransfer(otherUser);

                            if (otherUser_id == 0 || otherUser_id == user_id)
                            {
                                Console.WriteLine("The user name does not exist :");
                            }
                            else
                            {
                                decimal otherUserBalance=BankAccounts.BalanceCalculation(otherUser_id);
                                Console.WriteLine("{0} acc. balance is:{1}",otherUser, otherUserBalance);
                            }

                            Console.Write("Press 'E' for exit app or any other key for back to Menu:");
                            var menu2 = Console.ReadLine();
                            flag = AppMenu.ExitMenu(menu2);
                            break;

                        case "1":
                            Console.WriteLine("Your current balance is: " + user1.User_account.Balance);
                            Console.Write("Press 'E' for exit app or any other key for back to Menu:");
                            var menu1 = Console.ReadLine();

                            flag = AppMenu.ExitMenu(menu1);

                            break;

                        default:
                            Console.WriteLine("you picked wrong No");
                            flag = false;
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    string choice = AppMenu.SimpleUserMenu(user_id);
                   
                    while (choice != "1" && choice != "2" && choice != "3" && choice != "4" )
                    {
                        Console.Write("you picked wrong No pls re-enter : ");
                        choice = (Console.ReadLine());
                    }

                    switch (choice)
                    {
                        case "4":
                            Console.WriteLine("Exit the application ");

                            flag = false;                          
                            break;

                        case "3":
                            var userTransactions = user1.User_account.transactions;

                            if (userTransactions.Count != 0)
                            {
                                Console.WriteLine("Your today's transactions are the following and sent to txt file: ");

                                foreach (var transaction in userTransactions)
                                {
                                    Console.WriteLine(transaction.ToString());
                                }
                            }
                                                        
                            List<string> trans = userTransactions.Select(tr => Convert.ToString(tr)).ToList();
                            string[] todaysTransactionsToTXT = trans.ToArray();

                            BankAccounts.SaveTXTFile(todaysTransactionsToTXT);

                            Console.Write("Press 'E' for exit app or any other key for back to Menu:");
                            var menu4 = Console.ReadLine();
                            flag = AppMenu.ExitMenu(menu4);
                            break;
                                                                             
                        case "2":
                            Console.Write("enter user name to deposit:");
                            string otherUser = Console.ReadLine();

                            int otherUser_id = BankAccounts.SelectUserIdToTransfer(otherUser);

                            if (otherUser_id == 0 || otherUser_id == user_id)
                            {
                                Console.WriteLine("The user name does not exist :");
                            }
                            else
                            {
                                Console.Write("The amount you want to deposit to {0} : ", otherUser);
                                decimal depositAmountToSimpleMember = decimal.Parse(Console.ReadLine());
                                if(depositAmountToSimpleMember<=0 && depositAmountToSimpleMember > user1.User_account.Balance)
                                {
                                    Console.WriteLine("invalid amount :");

                                }
                                else
                                {
                                    user1.User_account.Deposit(depositAmountToSimpleMember);
                                    BankAccounts.DepositToOtherAccount(user_id, depositAmountToSimpleMember, otherUser_id);
                                }
                                
                            }

                            Console.Write("Press 'E' for exit app or any other key for back to Menu:");

                            var menu2 = Console.ReadLine();
                            flag = AppMenu.ExitMenu(menu2);
                            break;
                            

                        case "1":
                            Console.WriteLine("Your current balance is: " + user1.User_account.Balance);

                            Console.Write("Press 'E' for exit app or any other key for back to Menu:");
                            
                            var menu1 = Console.ReadLine();
                            
                            flag=AppMenu.ExitMenu(menu1);

                            break;

                        default:
                            Console.WriteLine("you picked wrong No");
                            flag = false;
                            break;


                    }


                }

            }

            








        }
    }
}
