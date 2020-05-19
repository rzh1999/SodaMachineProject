using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    public abstract class Can
    {
        public string name;
        private double cost;

        public double Cost { get => cost; set => cost = value; }
    }
}
