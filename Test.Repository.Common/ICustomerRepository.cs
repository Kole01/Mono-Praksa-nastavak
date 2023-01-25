using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Common;
using Test.Model;

namespace Test.Repository.Common
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> AllCustomersAsync(Paging paging, Sorting sorting, Filtering filtering);
        Task<Customer> FindCustomerByIdAsync(Guid id);
        Task<bool> SaveCustomerAsync(Customer newCustomer);
        Task<int> RemoveCustomerAsync(Guid id);
        Task<int> UpdateCustomerAsync(Customer newCustomerAge);
    }
}
