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
}