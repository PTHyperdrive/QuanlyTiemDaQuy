# 💎 Quản Lý Tiệm Đá Quý

Hệ thống quản lý tiệm đá quý với các tính năng quản lý sản phẩm, khách hàng, bán hàng, nhập hàng, và báo cáo.

## 📋 Tính năng chính

- **Dashboard**: Thống kê tổng quan (doanh thu, sản phẩm, khách hàng)
- **Quản lý sản phẩm**: Thêm, sửa, xóa sản phẩm đá quý với thông tin chi tiết
- **Quản lý khách hàng**: Lưu trữ thông tin khách hàng, lịch sử mua hàng
- **Bán hàng**: Tạo hóa đơn, quản lý đơn hàng
- **Nhập hàng**: Quản lý phiếu nhập từ nhà cung cấp
- **Nhà cung cấp**: Quản lý thông tin nhà cung cấp
- **Báo cáo**: Báo cáo doanh thu theo ngày/tháng/năm
- **Quản lý hệ thống**: Quản lý tài khoản nhân viên và chi nhánh

## 🛠️ Công nghệ sử dụng

- **Framework**: .NET Framework 4.8
- **UI**: Windows Forms
- **Database**: SQL Server
- **Language**: C# 8.0
- **IDE**: Visual Studio 2022

## 🏗️ Kiến trúc dự án (3-Layer Architecture)

```
┌─────────────────────────────────────────────────────────┐
│                    UI / Forms                           │
│         (MainForm, ProductForm, SalesForm...)           │
│                         ↓ ↑                             │
├─────────────────────────────────────────────────────────┤
│                        BLL                              │
│              Business Logic Layer                       │
│       (EmployeeService, ProductService...)              │
│                         ↓ ↑                             │
├─────────────────────────────────────────────────────────┤
│                        DAL                              │
│               Data Access Layer                         │
│     (EmployeeRepository, ProductRepository...)          │
│                         ↓ ↑                             │
├─────────────────────────────────────────────────────────┤
│                     Database                            │
│                   (SQL Server)                          │
└─────────────────────────────────────────────────────────┘
```

### 📁 Cấu trúc thư mục

```
QuanLyTiemDaQuy/
├── QuanLyTiemDaQuy.Models/     ← Data models (Employee, Product, Customer...)
├── QuanLyTiemDaQuy.DAL/        ← Repositories (SQL queries)
├── QuanLyTiemDaQuy.BLL/        ← Services (Business logic)
└── QuanLyTiemDaQuy/Forms/      ← UI (Windows Forms)
```

### 🔷 DAL - Data Access Layer (Lớp truy cập dữ liệu)

**Vị trí:** `QuanLyTiemDaQuy.DAL/Repositories/`

**Nhiệm vụ:**
- Thực thi SQL queries (SELECT, INSERT, UPDATE, DELETE)
- Chuyển đổi dữ liệu từ `DataTable` → `Model objects`
- **KHÔNG** chứa logic nghiệp vụ

**Ví dụ:**
```csharp
// EmployeeRepository.cs - Chỉ lấy dữ liệu, không kiểm tra quyền
public Employee? GetById(int employeeId)
{
    string query = "SELECT * FROM Employees WHERE EmployeeId = @Id";
    var dt = DatabaseHelper.ExecuteQuery(query, 
        DatabaseHelper.CreateParameter("@Id", employeeId));
    var list = MapDataTableToList(dt);
    return list.Count > 0 ? list[0] : null;
}
```

### 🔶 BLL - Business Logic Layer (Lớp logic nghiệp vụ)

**Vị trí:** `QuanLyTiemDaQuy.BLL/Services/`

**Nhiệm vụ:**
- Kiểm tra quyền (Admin mới được đặt mật khẩu)
- Validation dữ liệu (email hợp lệ, password đủ mạnh)
- Xử lý nghiệp vụ phức tạp (tính giá, tạo hóa đơn)
- Gọi Repository để lấy/lưu dữ liệu

**Ví dụ:**
```csharp
// EmployeeService.cs - Chứa logic nghiệp vụ
public (bool Success, string Message) SetPassword(int employeeId, string newPassword)
{
    // 1. Kiểm tra quyền (Logic nghiệp vụ)
    if (!CurrentEmployee?.IsAdmin ?? true)
        return (false, "Chỉ Admin mới có quyền đặt mật khẩu");

    // 2. Validation (Logic nghiệp vụ)
    if (newPassword.Length < 6)
        return (false, "Mật khẩu phải có ít nhất 6 ký tự");

    // 3. Gọi DAL để thực hiện
    bool success = _employeeRepository.SetPassword(employeeId, newPassword);
    return (success, "Đặt mật khẩu thành công");
}
```

### 💡 Tại sao tách lớp?

| Lợi ích | Giải thích |
|---------|------------|
| **Dễ bảo trì** | Thay đổi DB? Chỉ sửa DAL. Thay đổi quy tắc? Chỉ sửa BLL |
| **Tái sử dụng** | Một Service có thể dùng cho WinForms, Web, Mobile |
| **Dễ test** | Test từng lớp riêng biệt |
| **Phân công** | Dev A làm DAL, Dev B làm BLL |

## 👥 Phân quyền người dùng

| Vai trò | Quyền hạn |
|---------|-----------|
| **Admin** | Toàn quyền: quản lý tài khoản, chi nhánh, báo cáo, nhà cung cấp |
| **Manager** | Xem báo cáo, quản lý nhà cung cấp, quản lý tài khoản |
| **Sales** | Bán hàng, quản lý khách hàng, xem sản phẩm |

## 🚀 Hướng dẫn cài đặt

### Yêu cầu
- Visual Studio 2022
- SQL Server 2019+
- .NET Framework 4.8

### Các bước

1. **Clone repository**
   ```bash
   git clone <repository-url>
   ```

2. **Tạo database**
   - Mở SQL Server Management Studio
   - Chạy script tạo database (nếu có)

3. **Cấu hình connection string**
   - Mở file `QuanLyTiemDaQuy.DAL/DatabaseHelper.cs`
   - Sửa `_connectionString` theo cấu hình SQL Server của bạn

4. **Build và chạy**
   - Mở solution trong Visual Studio
   - Build solution (Ctrl + Shift + B)
   - Chạy ứng dụng (F5)

## 📝 Tài khoản mặc định

| Username | Password | Vai trò |
|----------|----------|---------|
| admin | admin123 | Admin |

## 📄 License

© 2024 - Quản Lý Tiệm Đá Quý
