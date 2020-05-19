using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Quarter : Coin
    {
        public Quarter()
        {
            name = "quarter";
            Value = .25;
        }
    }
}
