using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public class Coin
    {
        public string name;
        protected double value;
        public double Value { get => value; set => this.value = value; }
    }
}
