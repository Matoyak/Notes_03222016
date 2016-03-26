using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_03222016 {

    public class Program {

        public static void Main(string[] args) {
            //AddNums();
            //Console.ReadLine();
            //CrazyMath();
            //Console.ReadLine();
            //ProductNotes();
            //Console.ReadLine();
            //CounterNotes();
            //Console.ReadLine();
            BankAcctNotes();
            Console.ReadLine();
        }

        public static void AddNums() {
            double result = 0;
            result = Math.MyAdd(3, 2);
            Debug.Assert(result == 5, "Bad maths.");
            Console.WriteLine("Result: " + result);

            result = Math.MyAdd(0, 2);
            Debug.Assert(result == 2, "Even bad-er-er maths.");
            Console.WriteLine("\nResult: " + result);

            result = Math.MyAdd(3, 3, 3);
            Debug.Assert(result == 9, "Just the worst maths.");
            Console.WriteLine("\nResult: " + result);
        }

        public static void CrazyMath() {
            double result = 0;
            result = Math.MyCrazyMath(6, 2);
            Debug.Assert(result == 3, "Bad maths.");
            Console.WriteLine("Result: " + result);

            result = Math.MyCrazyMath(num0: 3, num1: 3, num2: 3);
            Debug.Assert(result == 9, "Even bad-er-er maths.");
            Console.WriteLine("\nResult: " + result);

            result = Math.MyCrazyMath(2, 2, 2, 2);
            Debug.Assert(result == 16, "Just the worst maths.");
            Console.WriteLine("\nResult: " + result);
        }

        public static void ProductNotes() {
            Product gold = new Product("Gold Bullion", 268413975m, 5, "Gold in bullion form!");
            Product silver = new Product("Silver Dollars", 137926845m, 15);
            silver.Description = "Pure silver goodness.";
            Product copper = new Product(name: "Copper Chits", price: 26841379m, numUnits: 25);
            copper.Description = "Basic copper coinage.";
            gold.UnitsInStock += 5;
            StringBuilder sb0 = new StringBuilder();
            sb0.AppendFormat("We have {1} pieces of {0} at {2:c} per unit.\n\"{3}\"\n\n", gold.Name, gold.UnitsInStock, gold.Price, gold.Description);
            sb0.AppendFormat("We have {1} pieces of {0} at {2:c} per unit.\n\"{3}\"\n\n", silver.Name, silver.UnitsInStock, silver.Price, silver.Description);
            sb0.AppendFormat("We have {1} pieces of {0} at {2:c} per unit.\n\"{3}\"", copper.Name, copper.UnitsInStock, copper.Price, copper.Description);

            Console.WriteLine(sb0.ToString());
        }

        public static void CounterNotes() {
            Console.WriteLine(Counter.CountWords("Hello World!"));
            Console.WriteLine(Counter.CountWords("The Cat in the Hat"));
        }

        public static void BankAcctNotes() {
            Customer alice = new Customer("Alice", 100, 0);
            string isCheckingOpenMessage;
            string isSavingsOpenMessage;
            if (alice.CheckingAcct.IsOpen) {
                isCheckingOpenMessage = "open";
            } else {
                isCheckingOpenMessage = "closed";
            }
            if(alice.SavingsAcct.IsOpen) {
                isSavingsOpenMessage = "open";
            } else {
                isSavingsOpenMessage = "closed";
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}'s {1} account (Account ID: {4}) has {2:c} and is {3}.\n", alice.Name, alice.CheckingAcct.CustAcctType, alice.CheckingAcct.Amount, isCheckingOpenMessage, alice.CheckingAcct.Id);
            sb.AppendFormat("\n{0}'s {1} account (Account ID: {4}) has {2:c} and is {3}.\n", alice.Name, alice.SavingsAcct.CustAcctType, alice.SavingsAcct.Amount, isSavingsOpenMessage, alice.SavingsAcct.Id);

            Customer john = new Customer("John", 100, 0);
            if(john.CheckingAcct.IsOpen) {
                isCheckingOpenMessage = "open";
            } else {
                isCheckingOpenMessage = "closed";
            }
            if(john.SavingsAcct.IsOpen) {
                isSavingsOpenMessage = "open";
            } else {
                isSavingsOpenMessage = "closed";
            }

            sb.AppendFormat("\n{0}'s {1} account (Account ID: {4}) has {2:c} and is {3}.\n", john.Name, john.CheckingAcct.CustAcctType, john.CheckingAcct.Amount, isCheckingOpenMessage, john.CheckingAcct.Id);
            sb.AppendFormat("\n{0}'s {1} account (Account ID: {4}) has {2:c} and is {3}.\n", john.Name, john.SavingsAcct.CustAcctType, john.SavingsAcct.Amount, isSavingsOpenMessage, john.SavingsAcct.Id);

            decimal tranferAmount = 50;
            bool transFromSavings = false;
            alice.Transfer(transFromSavings, tranferAmount);
            sb.AppendFormat("\nTransferred {0:c} between {3}'s accounts.\nChecking Amount: {1:c}\nSavings Amount: {2:c}\n", tranferAmount, alice.CheckingAcct.Amount, alice.SavingsAcct.Amount, alice.Name);

            Console.WriteLine(sb.ToString());
        }
    }

    public class Math {
        public static double MyAdd(params double[] nums) {
            double result = 0;
            foreach(double num in nums) {
                result += num;
            }
            return result;
        }

        public static double MyCrazyMath(double num0, double num1) {
            return num0 / num1;
        }

        public static double MyCrazyMath(double num0, double num1, double num2) {
            return num0 + num1 + num2;
        }

        public static double MyCrazyMath(double num0, double num1, double num2, double num3) {
            return num0 * num1 * num2 * num3;
        }
    }

    public class Product {
        public string Name { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }

        private decimal _price;
        public decimal Price {
            get { return _price; }
            set {
                if(value < 0) {
                    Debug.Assert(value < 0, "Price cannot be less than free.");
                    _price = 0;
                } else {
                    _price = value;
                }
            }
        }

        public Product(string name, decimal price, int numUnits, string description="Default Value") {
            Name = name;
            Price = price;
            UnitsInStock = numUnits;
            Description = description;
        }
    }

    public class Counter {
        public static int CountWords(string str) {
            string[] words = str.Split(' ');
            return words.Length;
        }
    }

    public enum AccountType {
            Checking,
            Savings
    }

    public class BankAccount {
        private static int totalAcounts = 0;
        public string Id { get; private set; }
        public decimal Amount { get; set; }
        public bool IsOpen { get; set; }
        public AccountType CustAcctType { get; set; }

        /// <summary>
        /// Preferred Constructor: Generates account based on initialized values.
        /// </summary>
        /// <param name="amt">The initial amount to open the account with.</param>
        /// <param name="actType">The type of account to create.</param>
        /// <param name="isOpen">Whether the account is open or not.</param>
        public BankAccount(decimal amt, AccountType actType, bool isOpen = true) {
            if(actType == AccountType.Checking) {
                Id = totalAcounts.ToString() + 'c';
            } else {
                Id = totalAcounts.ToString() + 's';
            }
            ++totalAcounts;
            Amount = amt;
            CustAcctType = actType;
            if (amt >= 100) {
                IsOpen = true;
            } else {
                IsOpen = false;
                //message that you need more money for the account to truly open.
            }
        }
    }

    public class Customer {
        public string Name { get; set; }
        public BankAccount CheckingAcct { get; set; }
        public BankAccount SavingsAcct { get; set; }

        /// <summary>
        /// Preferred Constructor: generates accounts and customer with initialized values.
        /// </summary>
        /// <param name="name">Customer Name</param>
        /// <param name="openAmt">Starting Amount</param>
        /// <param name="actType">Account Type</param>
        public Customer(string name, decimal openAmtChecking, decimal openAmtSavings = 2000) {
            Name = name;
            CheckingAcct = new BankAccount(openAmtChecking, AccountType.Checking);
            SavingsAcct = new BankAccount(openAmtSavings, AccountType.Savings);
        }

        /// <summary>
        /// Transfers money from one Customer's account to the other.
        /// </summary>
        /// <param name="fromSavings">If true, transfers transAmt from savings to checking. Does opposite if false.</param>
        /// <param name="transAmt">The amount transferred.</param>
        public void Transfer(bool fromSavings, decimal transAmt) {
            if (fromSavings) {
                SavingsAcct.Amount -= transAmt;
                CheckingAcct.Amount += transAmt;
            } else {
                CheckingAcct.Amount -= transAmt;
                SavingsAcct.Amount += transAmt;
            }
        }
    }

    public class Employee {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + ' ' + LastName; } }
    }

    public class FullTimeEmployee : Employee {
        public int YearsEmployed { get; set; }
    }

    public class ContractEmployee: Employee {
        public int MonthsEmployed { get; set; }
    }
}
