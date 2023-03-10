using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;
using Test.Model.Common;
using Test.Common;
namespace Test.Service.Common
{
    public interface ICustomerService
    {
        Task<List<Customer>> AllCustomersAsync(Paging paging, Sorting sorting, Filtering filtering);
        Task<Customer> FindCustomerByIdAsync(Guid id);
        Task<bool> SaveCustomerAsync(Customer newCustomer);
        Task<int> UpdateCustomerAsync(Customer newCustomerAge);
        Task<int> RemoveCustomerAsync(Guid id);
    }
}
