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
        public List<Can> cans;
        public int quarterCount;

        public SodaMachineA()
        {
            register = new List<Coin>();
            cans = new List<Can>();

            Quarter quarter = new Quarter();
            SetStartingMoney(20, quarter);

            Nickel nickel = new Nickel();
            SetStartingMoney(20, nickel);

            Penny penny = new Penny();
            SetStartingMoney(50, penny);

            Dime dime = new Dime();
            SetStartingMoney(10, dime);

            Cola cola = new Cola();
            SetStartingCans(10, cola);

            OrangeSoda orange = new OrangeSoda();
            SetStartingCans(10, orange);

            RootBeer rootbeer = new RootBeer();
            SetStartingCans(10, rootbeer);
        }

        private void SetStartingCans(int amountOfCans, Can can)
        {
            for (int i = 0; i < amountOfCans; i++)
            {
                cans.Add(can);
            }
        }
       private void SetStartingMoney(int coinAmount, Coin coin)
        {
            for (int i = 0; i < coinAmount; i++)
            {
                register.Add(coin);
            }
        }
      
        public double GetRegisterTotal()
        {
            double total = 0;
            foreach(Coin item in register)
            {
                total += item.Value;
            }

            return total;
        }
    }
}
