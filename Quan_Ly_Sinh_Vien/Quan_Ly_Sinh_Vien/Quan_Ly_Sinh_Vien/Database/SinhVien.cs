using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Quan_Ly_Sinh_Vien.Database
{
    public partial class SinhVien
    {
        public SinhVien()
        {
            DiemHocPhans = new HashSet<DiemHocPhan>();
            HocPhis = new HashSet<HocPhi>();
        }

        [Display(Name = "Mã Sinh Viên")]
        public int MaSinhVien { get; set; }

        [Display(Name = "Mã Lớp")]
        public int? MaLop { get; set; }

        [Display(Name = "Tên Sinh Viên")]
        public string HoTen { get; set; }

        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Giới Tính")]
        public string GioiTinh { get; set; }
        public string Email { get; set; }

        [Display(Name = "Số Điện Thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Mã Lớp")]
        public virtual LopHoc MaLopNavigation { get; set; }
        [Display(Name = "Điểm Học Phần")]
        public virtual ICollection<DiemHocPhan> DiemHocPhans { get; set; }
        [Display(Name = "Học Phí")]
        public virtual ICollection<HocPhi> HocPhis { get; set; }
    }
}
