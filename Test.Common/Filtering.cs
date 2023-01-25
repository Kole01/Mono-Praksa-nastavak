using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Common
{
    public class Filtering
    {
        
        public string SearchCustomer;
        public int? CustomerAgeMin;
        public int? CustomerAgeMax;

        public Filtering() { }
        public Filtering( string searchCustomer, int? customerAgeMin, int? customerAgeMax)
        {
            SearchCustomer = searchCustomer;
            CustomerAgeMin = customerAgeMin;
            CustomerAgeMax = customerAgeMax;
        }
    }
}
