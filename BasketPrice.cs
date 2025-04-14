using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saidyakov41
{
    internal class BasketPrice
    {

        public static decimal cost = 0;

        public static void increaseCost(decimal value)
        {
            cost += value;
        }

        public static void decreaseCost(decimal value)
        {
            cost -= value;
        }
    }
}
