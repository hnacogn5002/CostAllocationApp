using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class OverheadCostController : Controller
{
    private readonly CostAllocationDbContext _context;

    public OverheadCostController(CostAllocationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.OverheadCosts.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(OverheadCost cost)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cost);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(cost);
    }

    // FORM EDIT
    public async Task<IActionResult> Edit(int id)
    {
        var cost = await _context.OverheadCosts.FindAsync(id);
        if (cost == null) return NotFound();
        return View(cost);
    }

    // CẬP NHẬT
    [HttpPost]
    public async Task<IActionResult> Edit(int id, OverheadCost cost)
    {
        if (id != cost.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(cost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cost);
    }

    // CHI TIẾT
    public async Task<IActionResult> Details(int id)
    {
        var cost = await _context.OverheadCosts.FindAsync(id);
        if (cost == null) return NotFound();
        return View(cost);
    }

    // XÓA
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var cost = await _context.OverheadCosts.FindAsync(id);
        if (cost != null)
        {
            _context.OverheadCosts.Remove(cost);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
