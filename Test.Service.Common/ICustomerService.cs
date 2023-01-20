using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;
using Test.Model.Common;

namespace Test.Service.Common
{
    public interface ICustomerService
    {
        List<Customer> AllCustomers();
        Customer FindCustomerById(Guid id);
        bool SaveCustomer(Customer newCustomer);
        int ChangeAge(Customer newCustomerAge);
        int RemoveCustomer(Guid id);
    }
}
