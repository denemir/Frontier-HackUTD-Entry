using Microsoft.AspNetCore.Mvc;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _repository;

    public CustomerService(IRepository<Customer> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await _repository.AddAsync(customer);
    }

    public async Task UpdateCustomerAsync(int id, Customer customer)
    {
        var existingCustomer = await _repository.GetByIdAsync(id);
        if (existingCustomer != null)
        {
            await _repository.UpdateAsync(existingCustomer);
        }
    }

    public async Task DeleteCustomerAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}