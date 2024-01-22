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
    public class HocPhiController : Controller
    {
        private readonly QuanLySinhVienContext _context;

        public HocPhiController(QuanLySinhVienContext context)
        {
            _context = context;
        }

        // GET: HocPhi
        public async Task<IActionResult> Index()
        {
            var quanLySinhVienContext = _context.HocPhis.Include(h => h.MaSinhVienNavigation);
            return View(await quanLySinhVienContext.ToListAsync());
        }

        // GET: HocPhi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocPhi = await _context.HocPhis
                .Include(h => h.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHocPhi == id);
            if (hocPhi == null)
            {
                return NotFound();
            }

            return View(hocPhi);
        }

        // GET: HocPhi/Create
        public IActionResult Create()
        {
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaSinhVien", "HoTen");
            return View();
        }

        // POST: HocPhi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHocPhi,MaSinhVien,TenSinhVien,SoTien,NgayThanhToan,HinhThucThanhToan")] HocPhi hocPhi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hocPhi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaSinhVien", "HoTen", hocPhi.MaSinhVien);
            return View(hocPhi);
        }

        // GET: HocPhi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocPhi = await _context.HocPhis.FindAsync(id);
            if (hocPhi == null)
            {
                return NotFound();
            }
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaSinhVien", "HoTen", hocPhi.MaSinhVien);
            return View(hocPhi);
        }

        // POST: HocPhi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHocPhi,MaSinhVien,TenSinhVien,SoTien,NgayThanhToan,HinhThucThanhToan")] HocPhi hocPhi)
        {
            if (id != hocPhi.MaHocPhi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocPhi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocPhiExists(hocPhi.MaHocPhi))
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
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaSinhVien", "HoTen", hocPhi.MaSinhVien);
            return View(hocPhi);
        }

        // GET: HocPhi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocPhi = await _context.HocPhis
                .Include(h => h.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHocPhi == id);
            if (hocPhi == null)
            {
                return NotFound();
            }

            return View(hocPhi);
        }

        // POST: HocPhi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hocPhi = await _context.HocPhis.FindAsync(id);
            _context.HocPhis.Remove(hocPhi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocPhiExists(int id)
        {
            return _context.HocPhis.Any(e => e.MaHocPhi == id);
        }
    }
}
