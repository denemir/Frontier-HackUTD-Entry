using Microsoft.AspNetCore.Mvc;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(string id);
    Task AddCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(string id, Customer customer);
    Task DeleteCustomerAsync(string id);
}