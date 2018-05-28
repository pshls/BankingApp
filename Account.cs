using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSemiFinal1
{
    public class Account
    {     
        //"Public properties"
        public int UserId { get; set; }
        public DateTime DateTransaction { get; set; }
        public decimal Balance { get; set; }
               
        public List<decimal> transactions = new List<decimal>();

        public void Deposit(decimal amount)
        {
            this.Balance = this.Balance - amount;
            if (amount > 0)
            {
                transactions.Add(-amount);
            }

        }
        public void Withdraw(decimal amount)
        {

            this.Balance = this.Balance + amount;
            if (amount > 0)
            {
                transactions.Add((amount));
            }
        }





    }


}
