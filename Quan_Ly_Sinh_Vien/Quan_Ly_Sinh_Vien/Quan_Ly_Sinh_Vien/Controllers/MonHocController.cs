﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quan_Ly_Sinh_Vien.Database;

namespace Quan_Ly_Sinh_Vien.Controllers
{
    public class MonHocController : Controller
    {
        private readonly QuanLySinhVienContext _context;

        public MonHocController(QuanLySinhVienContext context)
        {
            _context = context;
        }

        // GET: MonHoc
        public async Task<IActionResult> Index()
        {
            return View(await _context.MonHocs.ToListAsync());
        }

        // GET: MonHoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs
                .FirstOrDefaultAsync(m => m.MaMonHoc == id);
            if (monHoc == null)
            {
                return NotFound();
            }

            return View(monHoc);
        }

        // GET: MonHoc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MonHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMonHoc,TenMonHoc,SoTinChi,HocKy,MaGiangVien,GiangVienPhuTrach")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monHoc);
        }

        // GET: MonHoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs.FindAsync(id);
            if (monHoc == null)
            {
                return NotFound();
            }
            return View(monHoc);
        }

        // POST: MonHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaMonHoc,TenMonHoc,SoTinChi,HocKy,MaGiangVien,GiangVienPhuTrach")] MonHoc monHoc)
        {
            if (id != monHoc.MaMonHoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonHocExists(monHoc.MaMonHoc))
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
            return View(monHoc);
        }

        // GET: MonHoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs
                .FirstOrDefaultAsync(m => m.MaMonHoc == id);
            if (monHoc == null)
            {
                return NotFound();
            }

            return View(monHoc);
        }

        // POST: MonHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monHoc = await _context.MonHocs.FindAsync(id);
            _context.MonHocs.Remove(monHoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonHocExists(int id)
        {
            return _context.MonHocs.Any(e => e.MaMonHoc == id);
        }
    }
}
