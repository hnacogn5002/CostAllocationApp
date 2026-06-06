using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DepartmentController : Controller
{
    private readonly CostAllocationDbContext _context;

    public DepartmentController(CostAllocationDbContext context)
    {
        _context = context;
    }

    // HIỂN THỊ DANH SÁCH
    public async Task<IActionResult> Index()
    {
        return View(await _context.Departments.ToListAsync());
    }

    // FORM CREATE
    public IActionResult Create()
    {
        return View();
    }

    // THÊM DỮ LIỆU
    [HttpPost]
    public async Task<IActionResult> Create(Department department)
    {
        if (ModelState.IsValid)
        {
            _context.Add(department);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(department);
    }

    // FORM EDIT
    public async Task<IActionResult> Edit(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return NotFound();
        return View(department);
    }

    // CẬP NHẬT
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Department department)
    {
        if (id != department.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(department);
    }

    // CHI TIẾT
    public async Task<IActionResult> Details(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return NotFound();
        return View(department);
    }

    // XÓA
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department != null)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
