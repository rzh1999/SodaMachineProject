using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachineA
    {
        public List<Coin> register;
        public List<Can> can;
        public int quarterCount;

        public SodaMachineA()
        {
            register = new List<Coin>();

            Quarter quarter = new Quarter();
            SetStartingMoney(20, quarter);

            Nickel nickel = new Nickel();
            SetStartingMoney(20, nickel);

            Penny penny = new Penny();
            SetStartingMoney(50, penny);

            Dime dime = new Dime();
            SetStartingMoney(10, dime);
        }

       private void SetStartingMoney(int coinAmount, Coin coin)
        {
            for (int i = 0; i < coinAmount; i++)
            {
                register.Add(coin);
            }
        }
      
    }
}
