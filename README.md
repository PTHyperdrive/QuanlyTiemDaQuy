# 💎 QuanLyTiemDaQuy - Hệ Thống Quản Lý Tiệm Đá Quý

Hệ thống quản lý cửa hàng kim hoàn và đá quý toàn diện, hỗ trợ nền tảng desktop (Windows) và di động (Android).

---

## 📦 Phiên Bản

| Phiên bản | Nền tảng | Framework | Mục đích sử dụng |
|-----------|----------|-----------|------------------|
| **Mainline** | Windows Desktop | .NET Framework 4.8 | Ứng dụng desktop đầy đủ tính năng |
| **POS Mobile** | Android | .NET MAUI | Ứng dụng di động cho nhân viên bán hàng |
| **POS Embedded** | Windows Embedded 10 | .NET MAUI (WinUI) | Máy POS tại quầy |

---

## ✨ Tính Năng

### 🏠 Bảng Điều Khiển
- Thống kê bán hàng thời gian thực
- Theo dõi doanh thu (ngày/tháng)
- Cảnh báo hàng tồn kho thấp
- Truy cập nhanh tất cả module

### 📦 Quản Lý Sản Phẩm
- Danh mục đá quý đầy đủ (Kim cương, Ruby, Sapphire, Emerald, v.v.)
- Hệ thống phân loại 4C (Carat, Color, Clarity, Cut)
- Quản lý chứng chỉ (GIA, IGI, HRD, AGS, Gübelin)
- Tự động sinh mã sản phẩm (KC-XXX, RB-XXX, v.v.)
- Theo dõi vị trí trưng bày

### 💰 Bán Hàng & Hóa Đơn
- Xử lý bán hàng nhanh
- Tra cứu khách hàng
- Quản lý giảm giá (hạng VIP)
- In hóa đơn
- Theo dõi thanh toán

### 📥 Quản Lý Nhập Kho
- Thu mua từ nhà cung cấp
- Tự động tạo sản phẩm khi nhập
- Xác thực chứng chỉ
- Tích hợp giá thị trường
- Theo dõi chi phí nhập

### 👥 Quản Lý Khách Hàng
- Cơ sở dữ liệu khách hàng
- Hệ thống hạng VIP/VVIP
- Lịch sử mua hàng
- Giảm giá thành viên

### 📊 Báo Cáo
- Báo cáo hóa đơn (theo ngày, trạng thái)
- Báo cáo nhập kho
- Phân tích doanh thu
- Xuất báo cáo nhiều định dạng

### 🔐 Bảo Mật & Phân Quyền
- Phân quyền theo vai trò (Admin, Manager, Sales)
- Quản lý mật khẩu
- Ghi log hoạt động
- Hiển thị module theo quyền

---

## 🛠️ Công Nghệ Sử Dụng

| Thành phần | Công nghệ |
|------------|-----------|
| Ứng dụng Desktop | Windows Forms (.NET 4.8) |
| Ứng dụng Mobile | .NET MAUI (Android) |
| Cơ sở dữ liệu | SQL Server |
| Kiến trúc | 3 tầng (DAL → BLL → UI) |
| ORM | ADO.NET với stored procedures |

---

## 📁 Cấu Trúc Dự Án

```
QuanlyTiemDaQuy/
├── Forms/                      # Giao diện WinForms (Mainline)
├── QuanLyTiemDaQuy.BLL/        # Tầng nghiệp vụ
├── QuanLyTiemDaQuy.DAL/        # Tầng truy cập dữ liệu
├── QuanLyTiemDaQuy.Models/     # Models dùng chung
├── QuanLyTiemDaQuy.Maui/       # Ứng dụng Mobile & POS
├── QuanLyTiemDaQuy.Core/       # Core dùng chung (MAUI)
├── QuanLyTiemDaQuy.Core.BLL/   # Nghiệp vụ Core
├── QuanLyTiemDaQuy.Core.DAL/   # Truy cập dữ liệu Core
└── Database/                   # SQL Scripts
```

---

## 🚀 Cài Đặt

### Yêu Cầu Hệ Thống

- **Windows 10/11** (cho Desktop & POS Embedded)
- **SQL Server 2019+** hoặc Azure SQL
- **.NET Framework 4.8** (Mainline)
- **.NET 10.0 SDK** (cho phát triển)
- **Android 8.0+** (POS Mobile)

### Bắt Đầu Nhanh

1. **Clone repository**
   ```bash
   git clone https://github.com/your-org/QuanlyTiemDaQuy.git
   ```

2. **Thiết lập cơ sở dữ liệu**
   ```bash
   # Chạy các script database theo thứ tự
   sqlcmd -S localhost -i Database/01_CreateTables.sql
   sqlcmd -S localhost -i Database/02_SeedData.sql
   ```

3. **Cấu hình connection string**
   Chỉnh sửa `App.config` và cập nhật connection string.

4. **Build và chạy**
   ```bash
   # Mainline (Desktop)
   dotnet build QuanlyTiemDaQuy.csproj
   
   # POS Mobile (APK)
   dotnet build QuanLyTiemDaQuy.Maui -f net10.0-android
   
   # POS Embedded (Windows)
   dotnet build QuanLyTiemDaQuy.Maui -f net10.0-windows10.0.19041.0
   ```

---

## 🔑 Mã Bản Quyền

Ứng dụng hỗ trợ 3 loại bản quyền:

| Bản quyền | Mã | Tính năng |
|-----------|------|----------|
| **Full** | `QLTDQ-FULL-2505-2004` | Đầy đủ tính năng, không giới hạn thiết bị |
| **POS Mobile** | `QLTDQ-POS-2505-2004` | Chỉ Bán hàng, Sản phẩm, Khách hàng |
| **POS Embedded** | `QLTDQ-POSE-2505-2004` | Tối ưu cho thiết bị embedded |

---

## 📥 Tạo Bộ Cài Đặt

Sử dụng **Inno Setup** để tạo installer Windows.

### Cài đặt Inno Setup
Tải từ: https://jrsoftware.org/isinfo.php

### Build tất cả phiên bản

```powershell
# Build tất cả editions
.\build-all.ps1 -All

# Tạo unified installer
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" installer\unified.iss
```

### Kết quả
```
dist/
├── QuanLyTiemDaQuy-Setup-v3.0.1.exe     (Bộ cài đặt hợp nhất)
├── QuanLyTiemDaQuy-Mobile-v1.0.apk      (APK Android)
```

---

## 👥 Tài Khoản Mặc Định

| Vai trò | Tên đăng nhập | Mật khẩu |
|---------|---------------|----------|
| Admin | `admin` | `admin123` |
| Manager | `manager` | `manager123` |
| Sales | `nv01` | `nv123` |

---

## 📞 Hỗ Trợ

- **Email**: support@notrespond.com

---

## 📄 Bản Quyền

Copyright © 2026 Jewelry POS Solutions. Bảo lưu mọi quyền.

Xem [LICENSE](./LICENSE) để biết chi tiết.
