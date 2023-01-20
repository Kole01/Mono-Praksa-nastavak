using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;

namespace Test.Repository.Common
{
    public interface ICustomerRepository
    {
        List<Customer> AllCustomers();
        Customer FindCustomerById(Guid id);
        bool SaveCustomer(Customer newCustomer);
        int RemoveCustomer(Guid id);
        int ChangeAge(Customer newCustomerAge);
    }
}
