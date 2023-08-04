using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_.NET
{
    internal class Program
    {
        static string bankname = "MyBank";
        static SimpleDesign simpleDesign = new SimpleDesign();
        static List<User> users = new List<User> { };
        static int acc3Digits = 0;

        static bool Login() 
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine($"{users[i].GetAccNum()} - {users[i].GetAccName()}");
            //}
            try
            {
                Console.WriteLine("\nEnter the last 3-digits of your Bank Account Number: ");
                acc3Digits = Convert.ToInt32(Console.ReadLine().Trim()) - 1; // -1 as it gets the index of the list
                string accNum = $"MYBANKRAU030{1000 + (acc3Digits+1)}";
                bool accCheck = false ;

                foreach (var temp in users)
                {
                    if (temp.GetAccNum() == accNum)
                    {
                        accCheck = true;
                        //break;
                    }
                }

                //for (int i = 0; i < users.Count; i++)
                //{
                //    accCheck = true ;

                //}

                if (accCheck)
                {
                    //Console.WriteLine("\nAccount Found..");
                    return true;
                }
                else
                {
                    Console.WriteLine("Account Not Found!");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Account Not found. Error: {e.Message}");
                return false;
            }
        }

        static void Stop()
        {
            simpleDesign.ColorText("\nPress any key to continue...", 4);
            Console.ReadKey();
        }

        static void AccountCreation()
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    //users[i] = new User($"{(i + 2) * 567}", $"{i * 567}");
            //    users.Add(new User($"{(i + 2) * 567}", $"{i * 567}"));
            //    Console.WriteLine("Success");
            //}

            Console.Write("\nEnter the Account Holder Name: ");
            string accName = Console.ReadLine().ToUpper().Trim();
            string accNum = "";
            double accBalance = 0;
            try
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine("\nPlease deposit minimum Rs.500/- to initialize your Bank Account " +
                        "(Minimum Balance of Rs.200/- should be maintained):");
                    accBalance = Convert.ToDouble(Console.ReadLine().Trim());
                    if (accBalance >= 500)
                    {
                        break;
                    }
                    if (i == 0) Console.Write("\nLast Attempt");
                }
                //Console.WriteLine(accBalance);
            }
            catch (FormatException)
            {
                Console.WriteLine("Enter a numerical value for Account Balance");
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(accName) || accBalance < 500)
                {
                    throw new CustomException("\nInvalid Account Name or Account Balance less than 500.");
                }
                accNum = $"MYBANKRAU030{1000 + (users.Count + 1)}";
                users.Add(new User($"{accNum}", $"{accName}", accBalance));
                simpleDesign.CenterBox($"Welcome to {bankname}", '#', 0, 7);
                simpleDesign.ColorText($"\nYour Bank Account Created successfully.\n" +
                    $"Your credentials are:\n" +
                    $"\t1. Account Name = {users[users.Count - 1].GetAccName()}\n" +
                    $"\t2. Account Number = {users[users.Count - 1].GetAccNum()}\n" +
                    $"\t3. Balance in account = {users[users.Count - 1].GetAccBalance()}", 4, 11);
            }
            catch (CustomException)
            {
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Account creation failed. Error: {e.Message}");
            }
        }

        static void Main(/*string[] args*/)
        {

            try
            {
                while (true)
                {
                    simpleDesign.CenterBox($"Welcome to {bankname}", '#', 0, 7);
                    Console.WriteLine("A. Create a new Bank Account\n" +
                        "B. Login to Existing Bank Account\n" +
                        "C. List of accounts created (Admin Access Required)\n");
                    simpleDesign.ColorText("Enter the index Character of the option you want to select: ", 11);
                    Console.WriteLine("Else enter 'X' to exit the application\n");

                    switch (Console.ReadKey(true).Key.ToString().ToUpper())
                    {
                        case "A":
                            simpleDesign.CenterBox($"Welcome to {bankname}", '#', 0, 7);
                            //Console.WriteLine("Sign in page:");
                            AccountCreation();
                            Stop();
                            break;

                        case "B":
                            simpleDesign.CenterBox($"Welcome to {bankname}", '#', 0, 7);
                            //Console.WriteLine("Login Page:");
                            if (users.Count == 0)
                            {
                                Console.WriteLine("No Accounts created in this bank yet...");
                                Stop();
                                break;
                            }
                            if (Login())
                            {
                                try
                                {
                                    NextProg();
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine($"User is Logged Out. Enter a valid value to perform action.");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"User is Logged Out. Error - {e.Message}");
                                }
                            }

                            acc3Digits = 0;
                            Stop();
                            break;

                        case "C":
                            simpleDesign.CenterBox($"Welcome to {bankname}", '#', 0, 7);
                            if (users.Count == 0)
                            {
                                Console.WriteLine("No Accounts created in this bank yet...");
                                Stop();
                                break;
                            }
                            else
                            {
                                //simpleDesign.ColorText("Account Numbers\tAccount Names", 11);
                                //int pass = 5253;
                                Console.WriteLine("\nEnter Pin to get admin access: ");
                                ConsoleKeyInfo key;
                                string pass = "";
                                do
                                {
                                    key = Console.ReadKey(true);

                                    // Backspace Should Not Work
                                    if (key.Key != ConsoleKey.Backspace)
                                    {
                                        pass += key.KeyChar;
                                        Console.Write("*");
                                    }
                                    else
                                    {
                                        Console.Write("\b");
                                    }
                                }
                                // Stops Receving Keys Once Enter is Pressed
                                while (key.Key != ConsoleKey.Enter);
                                Console.WriteLine();
                                try
                                {
                                    if (Convert.ToInt32(pass) == 7890)
                                    {
                                        foreach (var item in users)
                                        {
                                            Console.WriteLine($"{item.GetAccName()} -> {item.GetAccNum()}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Admin Access denied!!");
                                    }
                                }
                                catch (FormatException) 
                                {
                                    Console.WriteLine("\nAdmin Access denied!!");
                                    Console.WriteLine("Enter a numerical value only");
                                }
                                pass = "";
                            }
                            Stop();
                            break;

                        case "X":
                            //simpleDesign.CenterBox($"Welcome to {bankname}", '#', 0, 7);
                            //Console.WriteLine("byeyee");
                            //break;
                            throw new CustomException("Application Terminated!");

                        default:
                            break;
                    }
                }
            }
            catch (CustomException)
            {
                //simpleDesign.CenterBox($"Welcome to {bankname}", '#', 0, 7);
                simpleDesign.ColorText("\nPress any key to exit the application...", 4, 11);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                //simpleDesign.CenterBox($"Welcome to {bankname}", '#', 0, 7);
                Console.Clear();
                simpleDesign.ColorText($"\nApplication crashed. Error - {e.Message}", 4, 11);
                Console.ReadKey();
            }
        }

        static void NextProg()
        {
            bool userLoginCheck;
            do
            {
                userLoginCheck = true;
                simpleDesign.CenterBox($"Welcome to {bankname}", '#', 0, 7);
                simpleDesign.DashPrint();
                simpleDesign.WinCenter($"Logged in to {users[acc3Digits].GetAccName()}'s Account");
                simpleDesign.DashPrint();
                Console.WriteLine("A. Deposit Amount\n" +
                            "B. Withdraw Amount\n" +
                            "C. Check Balance\n");
                simpleDesign.ColorText("Enter the index Character of the option you want to select: ", 11);
                Console.WriteLine("Else enter 'X' to logout from account\n");

                switch (Console.ReadKey(true).Key.ToString().ToUpper())
                {
                    case "A":
                        Console.WriteLine("\nHow much amount to deposit?");
                        double amtDeposit = Convert.ToDouble(Console.ReadLine().Trim());

                        users[acc3Digits].DepositBalance(amtDeposit);

                        simpleDesign.ColorText("Successfully Deposited amount\n" +
                            $"\nNew Balance is {users[acc3Digits].GetAccBalance()}", 10);

                        Stop();
                        break;

                    case "B":

                        for (int i = 0; i < 2; i++)
                        {
                            Console.WriteLine("\nHow much amount to withdraw?");
                            double amtWithdraw = Convert.ToDouble(Console.ReadLine().Trim());
                            double newBalance = users[acc3Digits].GetAccBalance() - amtWithdraw;

                            if (newBalance < 200)
                            {
                                Console.Clear();
                                Console.WriteLine("\nCan not withdraw, not having sufficient Balance.\n" +
                                    $"Current Balance is {users[acc3Digits].GetAccBalance()}\n" +
                                    "Minimum Rs.200 is to be kept as balance.");

                                simpleDesign.ColorText("\nLast Chance to Withdraw", 12);
                            }
                            else
                            {
                                users[acc3Digits].WithdrawBalance(newBalance);
                                simpleDesign.ColorText("Successfully Withdrawn amount\n" +
                                            $"\nNew Balance is {users[acc3Digits].GetAccBalance()}", 10);
                                break;
                            }
                        }
                        Stop();
                        break;

                    case "C":
                        Console.WriteLine($"\nCurrent Balance is {users[acc3Digits].GetAccBalance()}");
                        Stop();
                        break;

                    case "X":
                        simpleDesign.ColorText("Log out Successful", 4, 11);
                        userLoginCheck = false;
                        break;

                    default:
                        break;
                } 
            } while (userLoginCheck);
        }
        }
}
