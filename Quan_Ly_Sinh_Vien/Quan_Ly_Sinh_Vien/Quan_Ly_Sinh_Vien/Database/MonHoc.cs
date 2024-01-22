using System;
using System.Collections.Generic;

#nullable disable

namespace Quan_Ly_Sinh_Vien.Database
{
    public partial class MonHoc
    {
        public MonHoc()
        {
            DiemHocPhans = new HashSet<DiemHocPhan>();
            LichHocs = new HashSet<LichHoc>();
        }

        public int MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int? SoTinChi { get; set; }
        public int? HocKy { get; set; }
        public int? MaGiangVien { get; set; }
        public string GiangVienPhuTrach { get; set; }

        public virtual ICollection<DiemHocPhan> DiemHocPhans { get; set; }
        public virtual ICollection<LichHoc> LichHocs { get; set; }
    }
}
