using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

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

            List<Coin> returnList = SetAmountToDispense();
            Console.ReadLine();
        }
    }
}
