using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Customer
    {
        public Wallet wallet;
        public BackPack backPack;
       

        public Customer()
        {
            wallet = new Wallet();
            backPack = new BackPack();
        }
        public double GetWalletBalance()
        {
            double totalAmount = 0;
            foreach(Coin item in wallet.coins)
            {
                totalAmount += item.Value;
            }

            //return Math.Round(totalAmount);
            return totalAmount;
        }

        public double GetBackPackCount()
        {
            return backPack.cans.Count();
        }

        public void DisplayBackPack()
        {
            foreach (Can item in backPack.cans)
            {
                Console.WriteLine($"{item.name} is in your backpack!");
            }
        }
        
    }
}
