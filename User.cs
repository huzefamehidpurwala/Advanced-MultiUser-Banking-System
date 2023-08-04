using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_.NET
{
    internal class User
    {
        private string accNum, accName;
        protected double accBalance;

        public User(string accNum, string accName, double accBalance) 
        {
            this.accName = accName;
            this.accNum = accNum;
            this.accBalance = accBalance;
        }

        public string GetAccNum()
        {
            return this.accNum;
        }

        public string GetAccName()
        {
            return this.accName;
        }

        public double GetAccBalance()
        {
            return this.accBalance;
        }

        public void DepositBalance(double balance)
        {
            this.accBalance += balance;
        }

        public void WithdrawBalance(double balance)
        {
            this.accBalance = balance;
        }
    }
}
