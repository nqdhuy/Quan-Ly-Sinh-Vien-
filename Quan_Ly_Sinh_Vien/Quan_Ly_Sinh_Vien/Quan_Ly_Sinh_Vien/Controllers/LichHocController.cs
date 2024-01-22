using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quan_Ly_Sinh_Vien.Database;

namespace Quan_Ly_Sinh_Vien.Controllers
{
    public class LichHocController : Controller
    {
        private readonly QuanLySinhVienContext _context;

        public LichHocController(QuanLySinhVienContext context)
        {
            _context = context;
        }

        // GET: LichHoc
        public async Task<IActionResult> Index()
        {
            var quanLySinhVienContext = _context.LichHocs.Include(l => l.MaLopNavigation).Include(l => l.MaMonHocNavigation);
            return View(await quanLySinhVienContext.ToListAsync());
        }

        // GET: LichHoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichHoc = await _context.LichHocs
                .Include(l => l.MaLopNavigation)
                .Include(l => l.MaMonHocNavigation)
                .FirstOrDefaultAsync(m => m.MaLichHoc == id);
            if (lichHoc == null)
            {
                return NotFound();
            }

            return View(lichHoc);
        }

        // GET: LichHoc/Create
        public IActionResult Create()
        {
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "TenLop");
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "TenMonHoc");
            return View();
        }

        // POST: LichHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLichHoc,MaMonHoc,MaLop,ThuNgayThang,ThoiGianBatDau,ThoiGianKetThuc,PhongHoc")] LichHoc lichHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "TenLop", lichHoc.MaLop);
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "TenMonHoc", lichHoc.MaMonHoc);
            return View(lichHoc);
        }

        // GET: LichHoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichHoc = await _context.LichHocs.FindAsync(id);
            if (lichHoc == null)
            {
                return NotFound();
            }
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "TenLop", lichHoc.MaLop);
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "TenMonHoc", lichHoc.MaMonHoc);
            return View(lichHoc);
        }

        // POST: LichHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLichHoc,MaMonHoc,MaLop,ThuNgayThang,ThoiGianBatDau,ThoiGianKetThuc,PhongHoc")] LichHoc lichHoc)
        {
            if (id != lichHoc.MaLichHoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichHocExists(lichHoc.MaLichHoc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLop"] = new SelectList(_context.LopHocs, "MaLop", "TenLop", lichHoc.MaLop);
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "TenMonHoc", lichHoc.MaMonHoc);
            return View(lichHoc);
        }

        // GET: LichHoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichHoc = await _context.LichHocs
                .Include(l => l.MaLopNavigation)
                .Include(l => l.MaMonHocNavigation)
                .FirstOrDefaultAsync(m => m.MaLichHoc == id);
            if (lichHoc == null)
            {
                return NotFound();
            }

            return View(lichHoc);
        }

        // POST: LichHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lichHoc = await _context.LichHocs.FindAsync(id);
            _context.LichHocs.Remove(lichHoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichHocExists(int id)
        {
            return _context.LichHocs.Any(e => e.MaLichHoc == id);
        }
    }
}
