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
 
    public class DiemHocPhanController : Controller
    {
        
        private readonly QuanLySinhVienContext _context;
 
        public DiemHocPhanController(QuanLySinhVienContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Search(int maSV, int maMH, double? diemMin, double? diemMax)
        {
            var diems = _context.DiemHocPhans
                .Include(d => d.MaSinhVienNavigation)
                .Include(d => d.MaMonHocNavigation)
                .AsQueryable();

            if (maSV != 0)
            {
                diems = diems.Where(d => d.MaSinhVienNavigation.HoTen.Contains(maSV.ToString()));
            }
            if (maMH != 0)
            {
                diems = diems.Where(d => d.MaMonHocNavigation.TenMonHoc.Contains(maMH.ToString()));
            }

            // Thêm điều kiện tìm kiếm theo điểm
            if (diemMin.HasValue)
            {
                diems = diems.Where(d => d.Diem >= diemMin.Value);
            }

            if (diemMax.HasValue)
            {
                diems = diems.Where(d => d.Diem <= diemMax.Value);
            }

            // Các điều kiện khác tương tự

            return View(await diems.ToListAsync());
        }

        // Phương thức hành động GET: DiemHocPhan/Index
        public async Task<IActionResult> Index()
        {
            // Truy vấn điểm học phần kèm theo thông tin môn học và sinh viên và sắp xếp giảm dần theo điểm
            var quanLySinhVienContext = _context.DiemHocPhans
                .Include(d => d.MaMonHocNavigation)
                .Include(d => d.MaSinhVienNavigation)
                .OrderByDescending(d => d.Diem);

            // Chuyển dữ liệu thành danh sách và trả về view
            return View(await quanLySinhVienContext.ToListAsync());
        }

        // Phương thức hành động GET: DiemHocPhan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Kiểm tra nếu id không tồn tại
            if (id == null)
            {
                return NotFound();
            }

            // Truy vấn chi tiết điểm học phần kèm theo thông tin môn học và sinh viên
            var diemHocPhan = await _context.DiemHocPhans
                .Include(d => d.MaMonHocNavigation)
                .Include(d => d.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaDiem == id);

            // Kiểm tra nếu không tìm thấy điểm học phần
            if (diemHocPhan == null)
            {
                return NotFound();
            }

            // Trả về view chi tiết điểm học phần
            return View(diemHocPhan);
        }

        // Phương thức hành động GET: DiemHocPhan/Create
        public IActionResult Create()
        {
            // Tạo SelectList cho danh sách môn học và sinh viên
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "MaMonHoc");
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaSinhVien", "MaSinhVien");

            // Trả về view tạo mới điểm học phần
            return View();
        }

        // Phương thức hành động POST: DiemHocPhan/Create
        // Xử lý tạo mới điểm học phần sau khi submit form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDiem,MaSinhVien,MaMonHoc,Diem,LanThi,GhiChu")] DiemHocPhan diemHocPhan)
        {
            // Kiểm tra tính hợp lệ của dữ liệu nhập vào
            if (ModelState.IsValid)
            {
                // Thêm điểm học phần vào DbContext và lưu thay đổi
                _context.Add(diemHocPhan);
                await _context.SaveChangesAsync();

                // Chuyển hướng về trang danh sách điểm học phần
                return RedirectToAction(nameof(Index));
            }

            // Nếu dữ liệu không hợp lệ, tạo lại SelectList và hiển thị form tạo mới
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "TenMonHoc", diemHocPhan.MaMonHoc);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaSinhVien", "HoTen", diemHocPhan.MaSinhVien);
            return View(diemHocPhan);
        }

         // Phương thức hành động GET: DiemHocPhan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Kiểm tra nếu id không tồn tại
            if (id == null)
            {
                return NotFound();
            }

            // Truy vấn điểm học phần cần chỉnh sửa
            var diemHocPhan = await _context.DiemHocPhans.FindAsync(id);

            // Kiểm tra nếu không tìm thấy điểm học phần
            if (diemHocPhan == null)
            {
                return NotFound();
            }

            // Tạo SelectList cho danh sách môn học và sinh viên
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "MaMonHoc", diemHocPhan.MaMonHoc);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaSinhVien", "MaSinhVien", diemHocPhan.MaSinhVien);

            // Trả về view chỉnh sửa điểm học phần
            return View(diemHocPhan);
        }

        // Phương thức hành động POST: DiemHocPhan/Edit/5
        // Xử lý chỉnh sửa điểm học phần sau khi submit form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDiem,MaSinhVien,MaMonHoc,Diem,LanThi,GhiChu")] DiemHocPhan diemHocPhan)
        {
            // Kiểm tra xác nhận id của điểm học phần cần chỉnh sửa
            if (id != diemHocPhan.MaDiem)
            {
                return NotFound();
            }

            // Kiểm tra tính hợp lệ của dữ liệu nhập vào
            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật thông tin điểm học phần trong DbContext và lưu thay đổi
                    _context.Update(diemHocPhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Kiểm tra xem điểm học phần có tồn tại không
                    if (!DiemHocPhanExists(diemHocPhan.MaDiem))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Chuyển hướng về trang danh sách điểm học phần
                return RedirectToAction(nameof(Index));
            }

            // Nếu dữ liệu không hợp lệ, tạo lại SelectList và hiển thị form chỉnh sửa
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "MaMonHoc", diemHocPhan.MaMonHoc);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaSinhVien", "MaSinhVien", diemHocPhan.MaSinhVien);
            return View(diemHocPhan);
        }

        // Phương thức hành động GET: DiemHocPhan/Delete/5
        // Hiển thị form xác nhận xóa điểm học phần
        public async Task<IActionResult> Delete(int? id)
        {
            // Kiểm tra nếu id không tồn tại
            if (id == null)
            {
                return NotFound();
            }

            // Truy vấn điểm học phần cần xóa kèm theo thông tin môn học và sinh viên
            var diemHocPhan = await _context.DiemHocPhans
                .Include(d => d.MaMonHocNavigation)
                .Include(d => d.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaDiem == id);

            // Kiểm tra nếu không tìm thấy điểm học phần
            if (diemHocPhan == null)
            {
                return NotFound();
            }

            // Trả về view xác nhận xóa điểm học phần
            return View(diemHocPhan);
        }

        // Phương thức hành động POST: DiemHocPhan/Delete/5
        // Xác nhận và xử lý xóa điểm học phần sau khi submit form
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Truy vấn điểm học phần cần xóa
            var diemHocPhan = await _context.DiemHocPhans.FindAsync(id);

            // Xóa điểm học phần từ DbContext và lưu thay đổi
            _context.DiemHocPhans.Remove(diemHocPhan);
            await _context.SaveChangesAsync();

            // Chuyển hướng về trang danh sách điểm học phần
            return RedirectToAction(nameof(Index));
        }

        // Phương thức kiểm tra sự tồn tại của điểm học phần
        private bool DiemHocPhanExists(int id)
        {
            return _context.DiemHocPhans.Any(e => e.MaDiem == id);
        }
    }
}


