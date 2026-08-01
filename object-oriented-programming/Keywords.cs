using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming
{

    internal class Bank
    {
        private static int totalAccounts = 0;
        private static string bankName = "SBI";
        private string accountName;
        readonly private int accountNumber;

        internal static int GetTotalAccounts()
        {
            return totalAccounts;
        }
        internal Bank() : this("", 0)
        {
        }

        internal Bank(string accountName, int accountNumber)
        {
            this.accountName = accountName;
            this.accountNumber = accountNumber;
            totalAccounts++;
        }

        internal void Display()
        {
            Console.Write($"{accountName} has an account {accountNumber} in {bankName}");
        }
    }

    internal class Hospital
    {
        private static string hospitalName = "Neelam";
        private static int totalPatients = 0;
        private string name;
        private int age;
        private string ailment;
        internal static void TotalPatients()
        {
            Console.WriteLine(totalPatients);
        }

        internal Hospital() : this("", 0, "")
        {
            totalPatients++;
        }

        internal Hospital(string name, int age, string ailment)
        {
            this.name = name;
            this.age = age;
            this.ailment = ailment;
        }

        internal void Display()
        {
            Console.WriteLine($"patient name is {name} been admitted to {hospitalName}, age is {age} with ailment : {ailment}");
        }

    }
    internal class Keywords
    {
        public static void BankMethod()
        {
            Bank bank = new Bank("Balreet", 100);
            Bank bank2 = new Bank("Taggar", 101);
            Console.WriteLine(Bank.GetTotalAccounts());
            if(bank is Bank && bank2 is Bank)
            {
                bank.Display();
                bank2.Display();
            }
        } 

        public static void HospitalMethod()
        {
            Hospital hospital = new Hospital("Balreet", 22, "HeadStrain");
            Hospital.TotalPatients();
            if(hospital is Hospital)
            {
                hospital.Display();
            }
        }
    }
}
