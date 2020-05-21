﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
                Console.WriteLine($"Thank you for your purchase, your wallets balance is now {String.Format("{0:0.00}",customer.GetWalletBalance())}");

                customer.backPack.cans.Add(sodaChoice[0]);

                Console.WriteLine($"{sodaChoice[0].name} has been added to your backpack");

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

                Console.WriteLine($"The machine cannot make change your money has been refunded");
            }

            //If too much money is passed in, accept the payment, return change as a list of coins from intenal,
            //limited register, and dispense a soda instance that gets saved to my Backpack.
            if (totalAmountToDeposit > sodaChoice[0].Cost && machineBankTotal > totalAmountToDeposit) 
            {
                double changeDue = totalAmountToDeposit - sodaChoice[0].Cost;
                GiveChange(changeDue);

                Console.WriteLine($"Your change in the amount of {changeDue} has been dispensed");

                customer.backPack.cans.Add(sodaChoice[0]);

                Console.WriteLine($"{sodaChoice[0].name} has been added to your backpack");

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

        //public List<Can> ChooseASoda()
        //{
        //    List<Can> canChoice = new List<Can>();
        //    int index = 0;

        //    foreach (Can item in sodaMachineA.cans)
        //    {
        //        Console.WriteLine($"Choose {index}: for {item.name} Price: {item.Cost} ");
        //        index++;
        //    }

        //    Console.WriteLine($"Enter choice:");
        //    int userInput = int.Parse(Console.ReadLine());

        //    canChoice.Add(sodaMachineA.cans[userInput]);

        //    return canChoice;
        //}

        //public List<Can> ChooseASoda()
        //{
        //    List<Can> canChoice = new List<Can>();

        //    Console.WriteLine($"1. Root Beer");
        //    Console.WriteLine($"2. Cola");
        //    Console.WriteLine($"3. Orange Soda");

        //    Console.WriteLine($"Enter choice:");
        //    int userInput = int.Parse(Console.ReadLine());


        //    // get indexes of sodas
        //    int rootBeer = 0;
        //    int cola = 0;
        //    int orangeSoda = 0;

        //    s

        //    rootBeer = sodaMachineA.cans.FindIndex(r => r.name.Equals("root beer"));
        //    cola = sodaMachineA.cans.FindIndex(r => r.name.Equals("cola"));
        //    orangeSoda = sodaMachineA.cans.FindIndex(r => r.name.Equals("orange soda"));

        //    switch (userInput)
        //    {
        //        case 1:
        //            canChoice.Add(sodaMachineA.cans[rootBeer]);
        //            break;
        //        case 2:
        //            canChoice.Add(sodaMachineA.cans[cola]);
        //            break;
        //        case 3:
        //            canChoice.Add(sodaMachineA.cans[orangeSoda]);
        //            break;
        //        default:
        //            break;
        //    }

        //}

        public List<Can> ChooseASoda()
        {
            List<Can> canChoice = new List<Can>();
            //var x = sodaMachineA.cans.Distinct();

            //foreach (Can item in x)
            //{
            //    Console.WriteLine(item.name);
            //}

            Console.WriteLine($"Enter root beer");
            Console.WriteLine($"Enter cola");
            Console.WriteLine($"Enter orange soda");
            Console.WriteLine();
            Console.WriteLine($"Please Enter A choice:");
            string userInput = Console.ReadLine();
            userInput = userInput.ToLower();

            switch (userInput)
            {
                case "root beer":
                    canChoice = AddChosenCan(userInput);
                    break;
                case "cola":
                    canChoice = AddChosenCan(userInput);
                    break;
                case "orange soda":
                    canChoice = AddChosenCan(userInput);
                    break;
                default:
                    Console.WriteLine($"You must enter a valid choice root beer, cola, orange soda");
                    Console.WriteLine();
                    userInput = Console.ReadLine();
                    break;
            }
           
            //switch ()
            //{
            //    case 1:
            //        canIndex = sodaMachineA.cans.Where(r => r.name == userInput).FirstOrDefault();
            //        canChoice.Add(canIndex);
            //        break;
            //    case 2:
            //        canIndex = sodaMachineA.cans.Where(r => r.name == userInput).FirstOrDefault();
            //        canChoice.Add(canIndex);
            //        break;
            //    case 3:
            //        canIndex = sodaMachineA.cans.Where(r => r.name == userInput).FirstOrDefault();
            //        canChoice.Add(canIndex);
            //        break;
            //}
            return canChoice;
        }

        public List<Can> AddChosenCan(string userInput)
        {
            List<Can> canChoice = new List<Can>();
            foreach (Can can in sodaMachineA.cans)
            {
                if (can.name == userInput)
                {
                    canChoice.Add(can);
                    break;
                }
            }
            return canChoice;
        }
        ////original
        //public List<Coin> SetAmountToDispense()
        //{
        //    int index = 0;
        //    List<Coin> changeSelection = new List<Coin>();
        //    foreach (Coin item in customer.wallet.coins)
        //    {
        //        Console.WriteLine($"Choose {index} {item.name}");

        //        index++;
        //    }

        //    int coinCount = customer.wallet.coins.Count + 1;

        //    Console.WriteLine($"Enter {coinCount} when done selecting coins");
        //    int choice = int.Parse(Console.ReadLine());


        //    while (choice != coinCount)
        //    {
        //        changeSelection.Add(customer.wallet.coins[choice]);
        //        Console.WriteLine("Enter another amount");
        //        choice = int.Parse(Console.ReadLine());
        //    }
        //    return changeSelection;
        //}

        public List<Coin> SetAmountToDispense()
        {
            bool stop = true;

            

            List<Coin> changeSelection = new List<Coin>();

            Quarter quarter = new Quarter();
            Dime dime = new Dime();
            Nickel nickel = new Nickel();
            Penny penny = new Penny();

            Wallet newWallet = new Wallet();

            int quarterCount = 0;
            int dimeCount = 0;
            int nickelCount = 0;
            int pennyCount = 0;

            while (stop)
            {
                Console.WriteLine($"Enter coins:");
                if (newWallet.coins.Count != 0)
                {
                    foreach (Coin coin in newWallet.coins)
                    {
                        if (coin.name == "quarter")
                        {
                            quarterCount = quarterCount + 1;
                        }
                    }
                    if (quarterCount > 0)
                    {
                        Console.WriteLine($"Enter quarter");
                        quarterCount = 0;
                    }

                    foreach(Coin coin in newWallet.coins)
                    {
                        if (coin.name == "dime")
                        {
                            dimeCount = dimeCount + 1;
                        }
                    }
                    if (dimeCount > 0)
                    {
                        Console.WriteLine($"Enter dime");
                    }

                    foreach(Coin coin in newWallet.coins)
                    {
                        if (coin.name == "nickel")
                        {
                            nickelCount = nickelCount + 1;
                        }
                    }
                    if (nickelCount > 0)
                    {
                        Console.WriteLine($"Enter nickel");
                    }
                    
                    foreach(Coin coin in newWallet.coins)
                    {
                        if (coin.name == "penny")
                        {
                            pennyCount = pennyCount + 1;
                        }
                    }
                    if (pennyCount > 0)
                    {
                        Console.WriteLine($"Enter penny");
                    }
                    
                }
                Console.WriteLine($"Enter done when finished");

                string userInput = Console.ReadLine();
                userInput = userInput.ToLower();

                switch (userInput)
                {
                    case "quarter":
                        Coin coin = newWallet.coins.Where(u => u.name == "quarter").FirstOrDefault();
                         
                        changeSelection.Add(quarter);
                        customer.wallet.coins.Remove(coin);
                        break;
                    case "dime":
                        changeSelection.Add(dime);
                        break;
                    case "nickel":
                        changeSelection.Add(nickel);
                        break;
                    case "penny":
                        changeSelection.Add(penny);
                        break;
                    case "done":
                        stop = false;
                        break;
                    default:
                        Console.WriteLine($"Please enter a valid coinage");
                        break;
                }
            }
            return changeSelection;
        }

        //public List<Coin> SetAmountToDispense()
        //{

        //    List<Coin> coinChoice = new List<Coin>();

        //    //foreach (Coin coin in customer.wallet.coins)
        //    //{


        //    //    if (can.name == userInput)
        //    //    {
        //    //        canChoice.Add(can);
        //    //        break;
        //    //    }
        //    //}
        //    return coinChoice;
        //}


        public void RunSimulation()
        {
           
            BuyASoda();
            double result1 = customer.GetWalletBalance();
            Console.WriteLine($"Wallet balance after purchase {result1}");



            Console.ReadLine();
        }
    }
}
