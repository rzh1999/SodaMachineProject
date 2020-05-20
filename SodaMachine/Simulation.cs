using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SodaMachine
{
    class Simulation
    {
        public SodaMachineA sodaMachineA;
        public Customer customer;

        public Simulation()
        {
            sodaMachineA = new SodaMachineA();
            customer = new Customer();
        }


        public void BuyASoda()
        {
            List<Can> sodaChoice = ChooseASoda();
            List<Coin> coinChoice = SetAmountToDispense();
            double totalAmountToDeposit = GetChoiceAmount(coinChoice);
            double machineBankTotal = sodaMachineA.GetRegisterTotal();

            //Remove deposited amount from wallet
            RemoveCoinsFromWallet(coinChoice);

            //Console.WriteLine($"Amount chose to deposit {totalAmountToDeposit}");

            //If not enough money is passed in, don’t complete transaction and give the money back
            if (sodaChoice[0].Cost > totalAmountToDeposit)
            {
                Console.WriteLine($"You did not deposit enough coins deposited: {String.Format("{0:0.00}",totalAmountToDeposit)} required: {String.Format("{0:0.00}",sodaChoice[0].Cost)}!");
                AddCoinsToWallet(coinChoice);
            }
           
            //If exact change is passed in, accept payment and dispense a soda instance that gets saved in my Backpack.
            if (sodaChoice[0].Cost == totalAmountToDeposit && machineBankTotal > totalAmountToDeposit)
            {  
                Console.WriteLine($"This is sodachoices name {sodaChoice[0].name}");
               
                Console.WriteLine($"Thank you for your purchase, your wallets balance is now {String.Format("{0:0.00}",customer.GetWalletBalance())}");

                customer.backPack.cans.Add(sodaChoice[0]);

                sodaMachineA.cans.Remove(sodaChoice[0]);
                customer.DisplayBackPack();
            }

            //If exact or too much money is passed in but there isn’t sufficient inventory for that soda,
            //don’t complete the transaction: give the money back
            //Coded this as per requirement but current design does not allow this to happen since the program will only display available coins
            //This is important to keep, in case we allowed multiple purchases but there was no reqirement to do so
            if (sodaChoice[0].Cost == totalAmountToDeposit && !sodaMachineA.cans.Contains(sodaChoice[0]))
            {
               
                Console.WriteLine($"There is no {sodaChoice[0].name} available");

                AddCoinsToWallet(coinChoice);
            }

            //If too much money is passed in but there isn’t sufficient change in the machine’s internal register,
            //don’t complete transaction: give the money back.
            if (totalAmountToDeposit > machineBankTotal)
            {
                AddCoinsToWallet(coinChoice);

                Console.WriteLine($"The machine cannot make change your money has b een refunded");
            }

            //If too much money is passed in, accept the payment, return change as a list of coins from intenal,
            //limited register, and dispense a soda instance that gets saved to my Backpack.
            if (totalAmountToDeposit > sodaChoice[0].Cost && machineBankTotal > totalAmountToDeposit) 
            {
                double changeDue = totalAmountToDeposit - sodaChoice[0].Cost;
                GiveChange(changeDue);

                customer.backPack.cans.Add(sodaChoice[0]);

                sodaMachineA.cans.Remove(sodaChoice[0]);
                customer.DisplayBackPack();

            }
        }
        
        public void RemoveCoinsFromWallet(List<Coin> coinChoice)
        {
            foreach (Coin item in coinChoice)
            {
                customer.wallet.coins.Remove(item);
            }
        }

        public void AddCoinsToWallet(List<Coin> coinChoice)
        {
            foreach (Coin item in coinChoice)
            {
                customer.wallet.coins.Add(item);
            }
        }
        public void GiveChange(double amount)
        {
            Console.WriteLine($"Amount is {amount}");
            double quarterValue = 0;
            double remainder = 0;
            double dimeValue = 0;
            int dimeValueToInt =0;
            int quarterValueToInt = 0;
            double nickelValue = 0;
            int nickelValueToInt = 0;
            double pennyValue = 0;
            int pennyValueToInt = 0;

            if (amount > .25 || amount < .25)
            {
                if (amount < .25)
                {
                    remainder = amount;
                }
                else
                {
                    quarterValue = amount / .25;
                    quarterValueToInt = (int)quarterValue;

                    remainder = amount % .25;
                    remainder = Math.Round(remainder, 2);

                    //Add quarters to wallet
                    Quarter quarter = new Quarter();
                    AddChangeToWallet(quarter, quarterValueToInt);
                    Console.WriteLine($"Number of Quarters returned {quarterValueToInt} remainder {remainder}");
                    
                }
            }

            if (remainder > .10 )
            {
                
                dimeValue = remainder / .10;
                dimeValueToInt = (int)dimeValue;

                remainder = remainder % .10;
                remainder = Math.Round(remainder, 2);

                //Add dimes to wallet
                Dime dime = new Dime();
                AddChangeToWallet(dime, dimeValueToInt);

                Console.WriteLine($"Number of dimes returned {dimeValueToInt} remainder {remainder}");
            }

            if (remainder > .05)
            {
                nickelValue = remainder / .05;
                nickelValueToInt = (int)nickelValue;

                remainder = remainder % .05;
                remainder = Math.Round(remainder, 2);

                //Add nickels to wllet
                Nickel nickel = new Nickel();
                AddChangeToWallet(nickel, nickelValueToInt);

                Console.WriteLine($"Number of nickels returned {nickelValueToInt} remainder {remainder}");
            }

            if (remainder >= .01)
            {
                pennyValue = remainder / .01;
                pennyValueToInt = (int)pennyValue;

                remainder = remainder % .01;
                remainder = Math.Round(remainder, 2);

                //Add pennies to wllet
                Penny penny = new Penny();
                AddChangeToWallet(penny,pennyValueToInt);

                Console.WriteLine($"Number of pennies returned {pennyValueToInt} remainder {remainder}");
            }
           
            
        }

        public void AddChangeToWallet(Coin coin, int numberOfCoins)
        {
            for (int i =0; i < numberOfCoins; i++)
            {
                customer.wallet.coins.Add(coin);
            }
          
        }
        public double GetChoiceAmount(List<Coin> coin)
        {
            double total = 0;
            foreach(Coin item in coin)
            {
                total += item.Value;
            }

            return total;
        }
        public List<Can> ChooseASoda()
        {
            List < Can > canChoice = new List<Can>();
            int index = 0;

            foreach(Can item in sodaMachineA.cans)
            {
                Console.WriteLine($"Choose {index}: for {item.name} Price: {item.Cost} ");
                index++;
            }
            Console.WriteLine($"Enter choice:");
            int userInput = int.Parse(Console.ReadLine());
            canChoice.Add(sodaMachineA.cans[userInput]);
            return canChoice;
        }
        public List<Coin> SetAmountToDispense()
        {
            int index = 0;
            List<Coin> changeSelection = new List<Coin>();
            foreach(Coin item in customer.wallet.coins)
            {
                Console.WriteLine($"Choose {index} {item.name}");
               
                index++;
            }
            int coinCount = customer.wallet.coins.Count + 1;
            Console.WriteLine($"Enter {coinCount} when done selecting coins");
            int choice = int.Parse(Console.ReadLine());


            while (choice != coinCount) {
                changeSelection.Add(customer.wallet.coins[choice]);
                Console.WriteLine("Enter another amount");
                choice = int.Parse(Console.ReadLine());
            }
            return changeSelection;
        }

        public void RunSimulation()
        {
            //SodaMachineA soda = new SodaMachineA();
            //Wallet wallet = new Wallet();
            //Customer customer = new Customer();

            //List<Coin> returnList = SetAmountToDispense();
            //List<Can>  sodaChoice = ChooseASoda();
            //GiveChange(.16);
            //GiveChange(.02);
            //GiveChange(.98);
            //GiveChange(.35);
            //GiveChange(.22);
            BuyASoda();
          

            Console.ReadLine();
        }
    }
}
