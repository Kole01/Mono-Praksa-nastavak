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
        public CustomerRepository repository = new CustomerRepository(); 
        public Customer FindCustomerById(Guid id)
        {
            Customer foundCustomer = repository.FindCustomerById(id);
            if (foundCustomer != null)
            {
                return foundCustomer;
            }
            return null;
        }

        public List<Customer> AllCustomers()
        {
            List<Customer> customers = repository.AllCustomers();
            if (customers != null)
            {
                return customers;
            }
            return null;
        }


        public bool SaveCustomer(Customer newCustomer)
        {
            if (repository.SaveCustomer(newCustomer))
            {
                return true;
            }
            return false;
        }

        public int RemoveCustomer(Guid Id)
        {
            switch (repository.RemoveCustomer(Id))
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
        public int ChangeAge(Customer newCustomerAge)
        {
            switch (repository.ChangeAge(newCustomerAge))
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
