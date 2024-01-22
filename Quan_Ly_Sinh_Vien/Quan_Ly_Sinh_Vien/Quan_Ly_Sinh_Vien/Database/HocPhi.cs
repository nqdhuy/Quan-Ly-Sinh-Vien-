using System;
using System.Collections.Generic;

#nullable disable

namespace Quan_Ly_Sinh_Vien.Database
{
    public partial class HocPhi
    {
        public int MaHocPhi { get; set; }
        public int? MaSinhVien { get; set; }
        public string TenSinhVien { get; set; }
        public decimal SoTien { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string HinhThucThanhToan { get; set; }

        public virtual SinhVien MaSinhVienNavigation { get; set; }
    }
}
