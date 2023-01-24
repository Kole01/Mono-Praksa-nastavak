using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.Model;
using Test.Model.Common;
using Test.Service.Common;
using Test.Repository;
using Test.Repository.Common;
using System.Runtime.InteropServices.ComTypes;

namespace Test.Service
{

    public class CustomerService : ICustomerService
    {
        //public CustomerRepository repository = new CustomerRepository(); 


        private ICustomerRepository repository{ get; set; }

        public CustomerService(ICustomerRepository _repository)
        {
            repository = _repository;
        }

        public async Task<Customer> FindCustomerByIdAsync(Guid id)
        {
            Customer foundCustomer =await repository.FindCustomerByIdAsync(id);
            if (foundCustomer != null)
            {
                return foundCustomer;
            }
            return null;
        }

        public async Task<List<Customer>> AllCustomersAsync()
        {
            List<Customer> customers =await repository.AllCustomersAsync();
            if (customers != null)
            {
                return customers;
            }
            return null;
        }


        public async Task<bool> SaveCustomerAsync(Customer newCustomer)
        {
            if (await repository.SaveCustomerAsync(newCustomer))
            {
                return true;
            }
            return false;
        }

        public async Task<int> RemoveCustomerAsync(Guid Id)
        {
            switch (await repository.RemoveCustomerAsync(Id))
            {
                case 1:
                    return 1;

                case 2:
                    return 2;

                case 3:
                    return 3;

                default:
                    return 4;
            }
        }
        public async Task<int> UpdateCustomerAsync(Customer newCustomerAge)
        {
            switch (await repository.UpdateCustomerAsync(newCustomerAge))
            {
                case 1:
                    return 1;

                case 2:
                    return 2;

                case 3:
                    return 3;

                default:
                    return 4;
            }
        }
    }
}
