using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly IRepository<Customer> _repository;

    // Inject the repository using the constructor
    public HomeController(IRepository<Customer> repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        // Use the repository to fetch all data
        var entities = await _repository.GetAllAsync();
        return View(entities); // Pass data to the view
    }

    public async Task<IActionResult> Details(int id)
    {
        // Use the repository to fetch a specific item by ID
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
        return View(entity); // Pass data to the view
    }

    public async Task<IActionResult> Create(Customer entity)
    {
        if (ModelState.IsValid)
        {
            await _repository.AddAsync(entity); // Add a new record
            return RedirectToAction("Index");
        }
        return View(entity);
    }
}