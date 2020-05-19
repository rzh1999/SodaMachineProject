using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        public List<Coin> coins;
        public Card card;

        public Wallet()
        {
            coins = new List<Coin>();

            Quarter quarter = new Quarter();
            PopulateDefaultWallet(12, quarter);

            Dime dime = new Dime();
            PopulateDefaultWallet(10, dime);

            Nickel nickel = new Nickel();
            PopulateDefaultWallet(50, nickel);

            Penny penny = new Penny();
            PopulateDefaultWallet(50, penny);
        }

        private void PopulateDefaultWallet(int amountOfCoin, Coin coin)
        {
           // coins = new List<Coin>();
            for (int i =0; i < amountOfCoin; i++)
            {
                coins.Add(coin);
            }
        }
    }
}
