﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            //SodaMachineA soda = new SodaMachineA();
            //Wallet wallet = new Wallet();
            //Customer customer = new Customer();
            //customer.GetWalletBalance();

            Simulation simulation = new Simulation();
            simulation.RunSimulation();
        }
    }
}
