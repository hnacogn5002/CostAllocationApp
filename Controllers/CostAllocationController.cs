using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CostAllocationController : Controller
{
    private readonly CostAllocationDbContext _context;

    public CostAllocationController(CostAllocationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.CostAllocations
            .Include(c => c.OverheadCost)
            .Include(c => c.Department)
            .ToListAsync());
    }

    public IActionResult Create()
    {
        ViewBag.Departments = _context.Departments.ToList();
        ViewBag.OverheadCosts = _context.OverheadCosts.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CostAllocation allocation)
    {
        var cost = await _context.OverheadCosts
            .FindAsync(allocation.OverheadCostId);

        if (cost == null)
        {
            return NotFound();
        }

        allocation.AllocatedAmount =
            cost.TotalAmount * allocation.AllocationRate / 100;

        allocation.AllocatedDate = DateTime.Now;

        _context.Add(allocation);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // CHI TIẾT
    public async Task<IActionResult> Details(int id)
    {
        var allocation = await _context.CostAllocations
            .Include(c => c.OverheadCost)
            .Include(c => c.Department)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (allocation == null) return NotFound();
        return View(allocation);
    }

    // XÓA
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var allocation = await _context.CostAllocations.FindAsync(id);
        if (allocation != null)
        {
            _context.CostAllocations.Remove(allocation);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
