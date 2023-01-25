using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Common
{
    public class Sorting
    {
        public string OrderBy;
        public string OrderDirection;

        public Sorting() { }
        public Sorting(string orderBy, string orderDirection)
        {
            OrderBy = orderBy;
            OrderDirection = orderDirection;
        }
    }

}
