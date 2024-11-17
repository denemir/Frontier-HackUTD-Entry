using Microsoft.AspNetCore.Mvc;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _repository;
    private readonly HttpClient _httpClient;

    public CustomerService(IRepository<Customer> repository, HttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;   
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await _repository.AddAsync(customer);
    }

    public async Task UpdateCustomerAsync(string id, Customer customer)
    {
        var existingCustomer = await _repository.GetByIdAsync(id);
        if (existingCustomer != null)
        {
            await _repository.UpdateAsync(existingCustomer);
        }
    }

    public async Task DeleteCustomerAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}