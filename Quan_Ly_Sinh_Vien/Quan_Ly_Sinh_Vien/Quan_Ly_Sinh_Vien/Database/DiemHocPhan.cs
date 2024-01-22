using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Quan_Ly_Sinh_Vien.Database
{
    public partial class DiemHocPhan
    {
        [Display(Name = "Mã Điểm")]
        public int MaDiem { get; set; }

        [Display(Name = "Mã Sinh Viên")]
        public int? MaSinhVien { get; set; }

        [Display(Name = "Mã Môn Học")]
        public int? MaMonHoc { get; set; }

        [Display(Name = "Điểm")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(0, 10, ErrorMessage = "Điểm không lớn hơn 10.")]
        [Required(ErrorMessage = "Vui lòng nhập giá trị cho điểm.")]
        public double Diem { get; set; }

        [Display(Name = "Số Lần Thi")]
        [Range(1, 3, ErrorMessage = "Số lần thi Không quá 3 lần.")]
        [Required(ErrorMessage = "Vui lòng nhập giá trị cho số lần thi.")]
        public int? LanThi { get; set; }

        [Display(Name = "Thành Tích")]
        [MaxLength(15, ErrorMessage = "Ghi Chú không được quá 15 ký tự.")]
        public string GhiChu { get; set; }


        [Display(Name = "Tên Môn Học")]
        public virtual MonHoc MaMonHocNavigation { get; set; }

        [Display(Name = "Tên Sinh Viên")]
        public virtual SinhVien MaSinhVienNavigation { get; set; }
    }
}