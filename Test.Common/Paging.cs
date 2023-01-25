using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Common
{
    public class Paging
    {
        public int Rpp;
        public int PageNumber;

        public Paging() { }
        public Paging(int Rpp, int PageNumber)
        {
            this.Rpp = Rpp;
            this.PageNumber = PageNumber;
        }
    }
}
