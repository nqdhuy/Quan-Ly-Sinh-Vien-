USE MASTER
GO
IF EXISTS(SELECT * FROM SYSDATABASES WHERE NAME = 'QuanLySinhVien')
	DROP DATABASE QuanLySinhVien
GO
-- Create the database
CREATE DATABASE QuanLySinhVien
GO

-- Use the newly created database
USE QuanLySinhVien
GO

-- Bảng Lớp Học
CREATE TABLE LopHoc (
    MaLop INT PRIMARY KEY IDENTITY(1,1),
    TenLop NVARCHAR(255) NOT NULL,
    SiSo INT,
    TenGiangVien NVARCHAR(255),
    PhongHoc NVARCHAR(50),
    NamHoc NVARCHAR(20),
    KhoaHoc INT,
    GioiTinhGiangVien NVARCHAR(10),
    CONSTRAINT CHK_SiSo CHECK (SiSo >= 0),
    CONSTRAINT CHK_KhoaHoc CHECK (KhoaHoc > 0),
   
);

-- Bảng Môn Học
CREATE TABLE MonHoc (
    MaMonHoc INT PRIMARY KEY IDENTITY(1,1),
    TenMonHoc NVARCHAR(255) NOT NULL,
    SoTinChi INT,
    HocKy INT,
    MaGiangVien INT,
    GiangVienPhuTrach NVARCHAR(255),
    CONSTRAINT CHK_SoTinChi CHECK (SoTinChi > 0),
    CONSTRAINT CHK_HocKy CHECK (HocKy > 0)
);

-- Bảng Sinh Viên
CREATE TABLE SinhVien (
    MaSinhVien INT PRIMARY KEY IDENTITY(1,1),
	MaLop INT,
    HoTen NVARCHAR(255) NOT NULL,
    NgaySinh DATE,
	GioiTinh NVARCHAR(10),
    Email NVARCHAR(100),
    SoDienThoai NVARCHAR(15),
    DiaChi NVARCHAR(255),
    CONSTRAINT FK_LopHoc_SinhVien FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop),
    CONSTRAINT CHK_NgaySinh CHECK (NgaySinh <= GETDATE()),
);

-- Bảng Lịch Học
CREATE TABLE LichHoc (
    MaLichHoc INT PRIMARY KEY IDENTITY(1,1),
    MaMonHoc INT,
    MaLop INT,
    ThuNgayThang NVARCHAR(10) NOT NULL,
    ThoiGianBatDau TIME NOT NULL,
    ThoiGianKetThuc TIME NOT NULL,
    PhongHoc NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_MonHoc_LichHoc FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc),
    CONSTRAINT FK_LopHoc_LichHoc FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop),
    CONSTRAINT CHK_ThoiGian CHECK (ThoiGianBatDau < ThoiGianKetThuc)
);

-- Bảng Điểm
CREATE TABLE DiemHocPhan (
    MaDiem INT PRIMARY KEY IDENTITY(1,1),
    MaSinhVien INT,
    MaMonHoc INT,
    Diem FLOAT NOT NULL,
    LanThi INT,
    GhiChu NVARCHAR(255),
    CONSTRAINT FK_SinhVien_Diem FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaSinhVien),
    CONSTRAINT FK_MonHoc_Diem FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc),
    CONSTRAINT CHK_Diem CHECK (Diem >= 0 AND Diem <= 10),
    CONSTRAINT CHK_LanThi CHECK (LanThi > 0)
);

-- Bảng Học Phí
CREATE TABLE HocPhi (
    MaHocPhi INT PRIMARY KEY IDENTITY(1,1),
    MaSinhVien INT,
	TenSinhVien NVARCHAR(255),
    SoTien DECIMAL(10, 2) NOT NULL,
    NgayThanhToan DATE NOT NULL,
    HinhThucThanhToan NVARCHAR(50),
    CONSTRAINT FK_SinhVien_HocPhi FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaSinhVien),
    CONSTRAINT CHK_SoTien CHECK (SoTien > 0)
);


	SELECT * FROM LopHoc
	SELECT * FROM MonHoc
	SELECT * FROM SinhVien
	SELECT * FROM LichHoc
	SELECT * FROM DiemHocPhan
	SELECT * FROM HocPhi	
	SELECT * FROM Users
	SELECT * FROM Logins