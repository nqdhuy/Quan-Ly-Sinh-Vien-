using System;
using System.Collections.Generic;

#nullable disable

namespace Quan_Ly_Sinh_Vien.Database
{
    public partial class LopHoc
    {
        public LopHoc()
        {
            LichHocs = new HashSet<LichHoc>();
            SinhViens = new HashSet<SinhVien>();
        }

        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public int? SiSo { get; set; }
        public string TenGiangVien { get; set; }
        public string PhongHoc { get; set; }
        public string NamHoc { get; set; }
        public int? KhoaHoc { get; set; }
        public string GioiTinhGiangVien { get; set; }

        public virtual ICollection<LichHoc> LichHocs { get; set; }
        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
