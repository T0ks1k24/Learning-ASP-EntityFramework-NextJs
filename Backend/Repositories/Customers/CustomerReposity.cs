using Entity_Framework.Data;
using Entity_Framework.Models.Dtos.Categories;
using Entity_Framework.Models.Dtos.Customers;
using Entity_Framework.Models.Dtos.Products;
using Entity_Framework.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Repositories.Customers
{
    public class CustomerReposity : ICustomerReposity
    {
        private readonly LearningDbContext _context;

        public CustomerReposity(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> GetAll()
        {
            var customers = await _context.Customers
                    .AsNoTracking()
                    .ToListAsync();

            var customerDtos = customers.Select(customers => new CustomerDto
            {
                Id = customers.Id,
                FirstName = customers.FirstName,
                LastName = customers.LastName,
                PhoneNumber = customers.PhoneNumber,
                Email = customers.Email,
            }).ToList();

            return customerDtos;
        }

        public async Task<CustomerDto> GetById(Guid id)
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customers == null)
            {
                return null;
            }

            return new CustomerDto
            {
                Id = customers.Id,
                FirstName = customers.FirstName,
                LastName = customers.LastName,
                PhoneNumber = customers.PhoneNumber,
                Email = customers.Email,
            };
        }

        public async Task<CustomerDto> Create(AddUpdateCustomerDto customerDto)
        {
            var customerEntity = new CustomerEntity
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                PhoneNumber = customerDto.PhoneNumber,
                Email = customerDto.Email,
            };

            await _context.Customers.AddAsync(customerEntity);
            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = customerEntity.Id,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                PhoneNumber = customerDto.PhoneNumber,
                Email = customerDto.Email,
            };
        }

        public async Task<CustomerDto> Update(Guid id, AddUpdateCustomerDto customerDto)
        {
            var existingCustomer = await _context.Customers
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCustomer == null)
            {
                return null;
            }

            existingCustomer.FirstName = customerDto.FirstName;
            existingCustomer.LastName = customerDto.LastName;
            existingCustomer.PhoneNumber = customerDto.PhoneNumber;
            existingCustomer.Email = customerDto.Email;
            
            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = existingCustomer.Id,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                PhoneNumber = customerDto.PhoneNumber,
                Email = customerDto.Email,
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
