using System;
using System.Collections.Generic;

#nullable disable

namespace Quan_Ly_Sinh_Vien.Database
{
    public partial class LichHoc
    {
        public int MaLichHoc { get; set; }
        public int? MaMonHoc { get; set; }
        public int? MaLop { get; set; }
        public string ThuNgayThang { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
        public string PhongHoc { get; set; }

        public virtual LopHoc MaLopNavigation { get; set; }
        public virtual MonHoc MaMonHocNavigation { get; set; }
    }
}
