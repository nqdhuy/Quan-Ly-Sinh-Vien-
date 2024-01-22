using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Quan_Ly_Sinh_Vien.Database
{
    public partial class QuanLySinhVienContext : DbContext
    {
        public QuanLySinhVienContext()
        {
        }

        public QuanLySinhVienContext(DbContextOptions<QuanLySinhVienContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DiemHocPhan> DiemHocPhans { get; set; }
        public virtual DbSet<HocPhi> HocPhis { get; set; }
        public virtual DbSet<LichHoc> LichHocs { get; set; }
        public virtual DbSet<LopHoc> LopHocs { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-HE2SLRB;Initial Catalog=QuanLySinhVien;User ID=QuanLySinhVien;Password=quanlysinhvien;Trust Server Certificate=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DiemHocPhan>(entity =>
            {
                entity.HasKey(e => e.MaDiem)
                    .HasName("PK__DiemHocP__33326025469584EA");

                entity.ToTable("DiemHocPhan");

                entity.Property(e => e.GhiChu).HasMaxLength(255);

                entity.HasOne(d => d.MaMonHocNavigation)
                    .WithMany(p => p.DiemHocPhans)
                    .HasForeignKey(d => d.MaMonHoc)
                    .HasConstraintName("FK_MonHoc_Diem");

                entity.HasOne(d => d.MaSinhVienNavigation)
                    .WithMany(p => p.DiemHocPhans)
                    .HasForeignKey(d => d.MaSinhVien)
                    .HasConstraintName("FK_SinhVien_Diem");
            });

            modelBuilder.Entity<HocPhi>(entity =>
            {
                entity.HasKey(e => e.MaHocPhi)
                    .HasName("PK__HocPhi__929232A298C2FEDF");

                entity.ToTable("HocPhi");

                entity.Property(e => e.HinhThucThanhToan).HasMaxLength(50);

                entity.Property(e => e.NgayThanhToan).HasColumnType("date");

                entity.Property(e => e.SoTien).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TenSinhVien).HasMaxLength(255);

                entity.HasOne(d => d.MaSinhVienNavigation)
                    .WithMany(p => p.HocPhis)
                    .HasForeignKey(d => d.MaSinhVien)
                    .HasConstraintName("FK_SinhVien_HocPhi");
            });

            modelBuilder.Entity<LichHoc>(entity =>
            {
                entity.HasKey(e => e.MaLichHoc)
                    .HasName("PK__LichHoc__150EBC21C0E6A1FC");

                entity.ToTable("LichHoc");

                entity.Property(e => e.PhongHoc)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ThuNgayThang)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.LichHocs)
                    .HasForeignKey(d => d.MaLop)
                    .HasConstraintName("FK_LopHoc_LichHoc");

                entity.HasOne(d => d.MaMonHocNavigation)
                    .WithMany(p => p.LichHocs)
                    .HasForeignKey(d => d.MaMonHoc)
                    .HasConstraintName("FK_MonHoc_LichHoc");
            });

            modelBuilder.Entity<LopHoc>(entity =>
            {
                entity.HasKey(e => e.MaLop)
                    .HasName("PK__LopHoc__3B98D2734BB7252E");

                entity.ToTable("LopHoc");

                entity.Property(e => e.GioiTinhGiangVien).HasMaxLength(10);

                entity.Property(e => e.NamHoc).HasMaxLength(20);

                entity.Property(e => e.PhongHoc).HasMaxLength(50);

                entity.Property(e => e.TenGiangVien).HasMaxLength(255);

                entity.Property(e => e.TenLop)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MonHoc>(entity =>
            {
                entity.HasKey(e => e.MaMonHoc)
                    .HasName("PK__MonHoc__4127737FF85541C1");

                entity.ToTable("MonHoc");

                entity.Property(e => e.GiangVienPhuTrach).HasMaxLength(255);

                entity.Property(e => e.TenMonHoc)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.MaSinhVien)
                    .HasName("PK__SinhVien__939AE775F9243E96");

                entity.ToTable("SinhVien");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.HoTen)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SoDienThoai).HasMaxLength(15);

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.SinhViens)
                    .HasForeignKey(d => d.MaLop)
                    .HasConstraintName("FK_LopHoc_SinhVien");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
