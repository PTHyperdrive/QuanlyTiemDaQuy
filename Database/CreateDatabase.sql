-- ============================================
-- QUẢN LÝ TIỆM ĐÁ QUÝ - DATABASE SCRIPT
-- SQL Server Express 2025
-- ============================================

-- Tạo Database
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyTiemDaQuy')
BEGIN
    ALTER DATABASE QuanLyTiemDaQuy SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyTiemDaQuy;
END
GO

CREATE DATABASE QuanLyTiemDaQuy;
GO

USE QuanLyTiemDaQuy;
GO

-- ============================================
-- BẢNG LOẠI ĐÁ (StoneTypes)
-- ============================================
CREATE TABLE StoneTypes (
    StoneTypeId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL
);
GO

-- ============================================
-- BẢNG CHỨNG NHẬN (Certificates)
-- ============================================
CREATE TABLE Certificates (
    CertId INT IDENTITY(1,1) PRIMARY KEY,
    CertCode NVARCHAR(50) NOT NULL UNIQUE,
    Issuer NVARCHAR(100) NOT NULL, -- GIA, IGI, AGS, etc.
    IssueDate DATE NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- BẢNG SẢN PHẨM (Products)
-- ============================================
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductCode NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    StoneTypeId INT NOT NULL,
    Carat DECIMAL(10,2) NOT NULL,
    Color NVARCHAR(50) NULL, -- D, E, F, G... (cho kim cương)
    Clarity NVARCHAR(50) NULL, -- FL, IF, VVS1, VVS2... 
    Cut NVARCHAR(50) NULL, -- Excellent, Very Good, Good...
    CostPrice DECIMAL(18,2) NOT NULL,
    SellPrice DECIMAL(18,2) NOT NULL,
    StockQty INT NOT NULL DEFAULT 0,
    Status NVARCHAR(50) NOT NULL DEFAULT N'Còn hàng', -- Còn hàng, Đặt trước, Hết hàng
    ImagePath NVARCHAR(500) NULL,
    CertId INT NULL,
    DisplayLocation NVARCHAR(100) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Products_StoneTypes FOREIGN KEY (StoneTypeId) REFERENCES StoneTypes(StoneTypeId),
    CONSTRAINT FK_Products_Certificates FOREIGN KEY (CertId) REFERENCES Certificates(CertId),
    CONSTRAINT CK_Products_SellPrice CHECK (SellPrice >= 0),
    CONSTRAINT CK_Products_CostPrice CHECK (CostPrice >= 0),
    CONSTRAINT CK_Products_StockQty CHECK (StockQty >= 0)
);
GO

-- ============================================
-- BẢNG KHÁCH HÀNG (Customers)
-- ============================================
CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(500) NULL,
    Tier NVARCHAR(50) NOT NULL DEFAULT N'Thường', -- Thường, VIP, VVIP
    TotalPurchase DECIMAL(18,2) DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- BẢNG NHÀ CUNG CẤP (Suppliers)
-- ============================================
CREATE TABLE Suppliers (
    SupplierId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(500) NULL,
    ContactPerson NVARCHAR(100) NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- BẢNG NHÂN VIÊN (Employees)
-- ============================================
CREATE TABLE Employees (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role NVARCHAR(50) NOT NULL DEFAULT 'Sales', -- Admin, Manager, Sales
    Phone NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- BẢNG PHIẾU NHẬP HÀNG (ImportReceipts)
-- ============================================
CREATE TABLE ImportReceipts (
    ImportId INT IDENTITY(1,1) PRIMARY KEY,
    ImportCode NVARCHAR(50) NOT NULL UNIQUE,
    SupplierId INT NOT NULL,
    EmployeeId INT NOT NULL,
    ImportDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalCost DECIMAL(18,2) NOT NULL DEFAULT 0,
    Note NVARCHAR(500) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_ImportReceipts_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers(SupplierId),
    CONSTRAINT FK_ImportReceipts_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);
GO

-- ============================================
-- BẢNG CHI TIẾT NHẬP HÀNG (ImportDetails)
-- ============================================
CREATE TABLE ImportDetails (
    ImportDetailId INT IDENTITY(1,1) PRIMARY KEY,
    ImportId INT NOT NULL,
    ProductId INT NOT NULL,
    Qty INT NOT NULL,
    UnitCost DECIMAL(18,2) NOT NULL,
    LineTotal DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_ImportDetails_ImportReceipts FOREIGN KEY (ImportId) REFERENCES ImportReceipts(ImportId) ON DELETE CASCADE,
    CONSTRAINT FK_ImportDetails_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    CONSTRAINT CK_ImportDetails_Qty CHECK (Qty > 0)
);
GO

-- ============================================
-- BẢNG HÓA ĐƠN BÁN HÀNG (Invoices)
-- ============================================
CREATE TABLE Invoices (
    InvoiceId INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceCode NVARCHAR(50) NOT NULL UNIQUE,
    CustomerId INT NULL, -- NULL = Khách lẻ
    EmployeeId INT NOT NULL,
    InvoiceDate DATETIME NOT NULL DEFAULT GETDATE(),
    Subtotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    DiscountPercent DECIMAL(5,2) DEFAULT 0,
    DiscountAmount DECIMAL(18,2) DEFAULT 0,
    VAT DECIMAL(5,2) DEFAULT 10, -- % VAT
    VATAmount DECIMAL(18,2) DEFAULT 0,
    Total DECIMAL(18,2) NOT NULL DEFAULT 0,
    PaymentMethod NVARCHAR(50) NOT NULL DEFAULT N'Tiền mặt', -- Tiền mặt, Thẻ, Chuyển khoản
    Note NVARCHAR(500) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Invoices_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    CONSTRAINT FK_Invoices_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);
GO

-- ============================================
-- BẢNG CHI TIẾT HÓA ĐƠN (InvoiceDetails)
-- ============================================
CREATE TABLE InvoiceDetails (
    InvoiceDetailId INT IDENTITY(1,1) PRIMARY KEY,
    InvoiceId INT NOT NULL,
    ProductId INT NOT NULL,
    Qty INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    LineTotal DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_InvoiceDetails_Invoices FOREIGN KEY (InvoiceId) REFERENCES Invoices(InvoiceId) ON DELETE CASCADE,
    CONSTRAINT FK_InvoiceDetails_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    CONSTRAINT CK_InvoiceDetails_Qty CHECK (Qty > 0)
);
GO

-- ============================================
-- DỮ LIỆU MẪU (Sample Data)
-- ============================================

-- Loại đá
INSERT INTO StoneTypes (Name, Description) VALUES
(N'Kim cương', N'Diamond - Loại đá quý cao cấp nhất, độ cứng 10 Mohs'),
(N'Ruby', N'Hồng ngọc - Đá quý màu đỏ, độ cứng 9 Mohs'),
(N'Sapphire', N'Lam ngọc - Đá quý màu xanh, độ cứng 9 Mohs'),
(N'Ngọc lục bảo', N'Emerald - Đá quý màu xanh lục'),
(N'Ngọc trai', N'Pearl - Đá quý hữu cơ từ trai biển'),
(N'Thạch anh', N'Quartz - Đá bán quý phổ biến');
GO

-- Nhân viên mặc định (password: admin123 - SHA256 hash)
INSERT INTO Employees (Name, Username, PasswordHash, Role, Phone, Email) VALUES
(N'Quản trị viên', 'admin', '240be518fabd2724ddb6f04eeb9d7af4ebc3b2d6c8fe8e9d7f5f4b5a7e8c9d0e', 'Admin', '0901234567', 'admin@daquy.vn'),
(N'Nguyễn Văn Minh', 'manager01', '240be518fabd2724ddb6f04eeb9d7af4ebc3b2d6c8fe8e9d7f5f4b5a7e8c9d0e', 'Manager', '0912345678', 'minh@daquy.vn'),
(N'Trần Thị Hoa', 'sales01', '240be518fabd2724ddb6f04eeb9d7af4ebc3b2d6c8fe8e9d7f5f4b5a7e8c9d0e', 'Sales', '0923456789', 'hoa@daquy.vn');
GO

-- Khách hàng mẫu
INSERT INTO Customers (Name, Phone, Email, Address, Tier) VALUES
(N'Nguyễn Thị Mai', '0901111111', 'mai@gmail.com', N'123 Lê Lợi, Q1, TP.HCM', N'VIP'),
(N'Trần Văn Tùng', '0902222222', 'tung@gmail.com', N'456 Nguyễn Huệ, Q1, TP.HCM', N'Thường'),
(N'Lê Hồng Nhung', '0903333333', 'nhung@gmail.com', N'789 Đồng Khởi, Q1, TP.HCM', N'VVIP');
GO

-- Nhà cung cấp mẫu
INSERT INTO Suppliers (Name, Phone, Email, Address, ContactPerson) VALUES
(N'Công ty Kim Cương ABC', '02812345678', 'abc@diamond.com', N'100 Hai Bà Trưng, Q1, TP.HCM', N'Nguyễn Văn A'),
(N'Đá Quý Hoàng Gia', '02887654321', 'hoanggia@gem.vn', N'200 Lý Tự Trọng, Q1, TP.HCM', N'Trần Thị B');
GO

-- Chứng nhận mẫu
INSERT INTO Certificates (CertCode, Issuer, IssueDate) VALUES
('GIA-2024-001234', 'GIA', '2024-01-15'),
('IGI-2024-005678', 'IGI', '2024-02-20'),
('GIA-2024-009999', 'GIA', '2024-03-10');
GO

-- Sản phẩm mẫu
INSERT INTO Products (ProductCode, Name, StoneTypeId, Carat, Color, Clarity, Cut, CostPrice, SellPrice, StockQty, Status, CertId, DisplayLocation) VALUES
('KC-001', N'Kim cương tròn 1 carat D VVS1', 1, 1.00, 'D', 'VVS1', 'Excellent', 150000000, 200000000, 5, N'Còn hàng', 1, N'Tủ A1'),
('KC-002', N'Kim cương tròn 0.5 carat E VS1', 1, 0.50, 'E', 'VS1', 'Very Good', 50000000, 70000000, 10, N'Còn hàng', 2, N'Tủ A2'),
('RB-001', N'Ruby đỏ huyết bồ câu 2 carat', 2, 2.00, N'Đỏ đậm', 'Eye Clean', 'Oval', 80000000, 120000000, 3, N'Còn hàng', NULL, N'Tủ B1'),
('SP-001', N'Sapphire xanh Kashmir 1.5 carat', 3, 1.50, N'Xanh đậm', 'Eye Clean', 'Cushion', 100000000, 150000000, 2, N'Còn hàng', 3, N'Tủ B2'),
('EM-001', N'Ngọc lục bảo Colombia 1 carat', 4, 1.00, N'Xanh lục', 'Minor Inclusions', 'Emerald Cut', 60000000, 90000000, 4, N'Còn hàng', NULL, N'Tủ C1');
GO

-- ============================================
-- TRIGGER: Cập nhật tồn kho khi nhập hàng
-- ============================================
CREATE TRIGGER trg_ImportDetails_Insert
ON ImportDetails
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Products
    SET StockQty = StockQty + i.Qty,
        UpdatedAt = GETDATE()
    FROM Products p
    INNER JOIN inserted i ON p.ProductId = i.ProductId;
END;
GO

-- ============================================
-- TRIGGER: Trừ tồn kho khi bán hàng
-- ============================================
CREATE TRIGGER trg_InvoiceDetails_Insert
ON InvoiceDetails
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Products
    SET StockQty = StockQty - i.Qty,
        UpdatedAt = GETDATE()
    FROM Products p
    INNER JOIN inserted i ON p.ProductId = i.ProductId;
    
    -- Cập nhật status nếu hết hàng
    UPDATE Products
    SET Status = N'Hết hàng'
    WHERE StockQty <= 0;
END;
GO

-- ============================================
-- VIEW: Báo cáo tồn kho
-- ============================================
CREATE VIEW vw_Inventory AS
SELECT 
    p.ProductId,
    p.ProductCode,
    p.Name,
    st.Name AS StoneType,
    p.Carat,
    p.CostPrice,
    p.SellPrice,
    p.StockQty,
    p.Status,
    CASE 
        WHEN p.StockQty <= 0 THEN N'Hết hàng'
        WHEN p.StockQty <= 5 THEN N'Tồn thấp'
        ELSE N'Bình thường'
    END AS StockStatus
FROM Products p
INNER JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId;
GO

-- ============================================
-- VIEW: Báo cáo doanh thu
-- ============================================
CREATE VIEW vw_SalesReport AS
SELECT 
    i.InvoiceId,
    i.InvoiceCode,
    i.InvoiceDate,
    c.Name AS CustomerName,
    e.Name AS EmployeeName,
    i.Subtotal,
    i.DiscountAmount,
    i.VATAmount,
    i.Total,
    i.PaymentMethod
FROM Invoices i
LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
INNER JOIN Employees e ON i.EmployeeId = e.EmployeeId;
GO

PRINT N'Database QuanLyTiemDaQuy đã được tạo thành công!';
GO
