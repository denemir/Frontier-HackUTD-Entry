﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class CustomersController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Json(customers);
    }

    [HttpGet]
    public async Task<IActionResult> DoesAccountExist(string acctId)
    {
        var customer = _customerService.GetAllCustomersAsync().Result.Where(x => x.AcctId == acctId).FirstOrDefault();

        return Ok(new { exists = (customer != null) });
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomerById(string id)
    {
        var customers = await _customerService.GetCustomerByIdAsync(id);
        return Json(customers);
    }
}