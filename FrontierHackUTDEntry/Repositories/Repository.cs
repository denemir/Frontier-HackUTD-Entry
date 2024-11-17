using Microsoft.EntityFrameworkCore;

public class Repository : IRepository<Customer>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Customer> _dbSet;


    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Customer>();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(Customer entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetCountWithWifiSecurityAsync(string acctId)
    {
        var city = await _context.Customers
            .Where(c => c.AcctId == acctId)
            .Select(c => c.City)
            .FirstOrDefaultAsync();

        var state = await _context.Customers
            .Where(c => c.AcctId == acctId)
            .Select(c => c.State)
            .FirstOrDefaultAsync();

        return await _context.Customers
            .Where(c => c.AcctId != acctId && c.City == city && c.State == state && c.WifiSecurity)
            .CountAsync();
    }

    public async Task<IEnumerable<Customer>> GetByAccountIdAsync(string acctId)
    {
        return await _dbSet
            .Where(c => c.AcctId == acctId)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountInCityStateAsync(string acctId)
    {
        var city = await _context.Customers
            .Where(c => c.AcctId == acctId)
            .Select(c => c.City)
            .FirstOrDefaultAsync();

        var state = await _context.Customers
            .Where(c => c.AcctId == acctId)
            .Select(c => c.State)
            .FirstOrDefaultAsync();

        return await _context.Customers
            .Where(c => c.AcctId != acctId && c.City == city && c.State == state)
            .CountAsync();
    }
}