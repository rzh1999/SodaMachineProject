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

            //Console.WriteLine($"Amount chose to deposit {totalAmountToDeposit}");

            //If not enough money is passed in, don’t complete transaction and give the money back
            if (sodaChoice[0].Cost > totalAmountToDeposit)
            {
                Console.WriteLine($"You did not deposit enough coins deposited: {String.Format("{0:0.00}",totalAmountToDeposit)} required: {String.Format("{0:0.00}",sodaChoice[0].Cost)}!");
            }
           
            //If exact change is passed in, accept payment and dispense a soda instance that gets saved in my Backpack.
            if (sodaChoice[0].Cost == totalAmountToDeposit)
            {
                //Console.WriteLine($"Your Current Wallets balance is {customer.GetWalletBalance()}");
               
                Console.WriteLine($"This is sodachoices name {sodaChoice[0].name}");
                foreach (Coin item in coinChoice)
                {
                    customer.wallet.coins.Remove(item);
                }
                Console.WriteLine($"Thank you for your purchase, your wallets balance is now {String.Format("{0:0.00}",customer.GetWalletBalance())}");

                customer.backPack.cans.Add(sodaChoice[0]);

                sodaMachineA.cans.Remove(sodaChoice[0]);
                customer.DisplayBackPack();
            }

            //If too much money is passed in, accept the payment, return change as a list of coins from internal,
            //limited register, and dispense a soda instance that gets saved to my Backpack.
            //if (totalAmountToDeposit > sodaChoice[0].Cost)
            //{
            //    double changeDue = 0;

            //}

            //If exact or too much money is passed in but there isn’t sufficient inventory for that soda,
            //don’t complete the transaction: give the money back
            //if (totalAmountToDeposit > sodaChoice[0].Cost || totalAmountToDeposit == sodaChoice[0].Cost)
            //{
            //    Console.WriteLine($"The soda ");
            //}
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
            SodaMachineA soda = new SodaMachineA();
            Wallet wallet = new Wallet();
            Customer customer = new Customer();

            //List<Coin> returnList = SetAmountToDispense();
            //List<Can>  sodaChoice = ChooseASoda();
            BuyASoda();
            Console.ReadLine();
        }
    }
}
