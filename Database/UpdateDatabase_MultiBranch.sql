-- =============================================
-- Cập nhật Database: Thêm hỗ trợ đa chi nhánh
-- CHẠY SAU KHI ĐÃ TẠO CÁC BẢNG CHÍNH
-- =============================================

-- 1. Bảng chi nhánh (chạy đầu tiên)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Branches')
BEGIN
    CREATE TABLE Branches (
        BranchId INT IDENTITY PRIMARY KEY,
        BranchCode NVARCHAR(20) NOT NULL UNIQUE,
        Name NVARCHAR(100) NOT NULL,
        Address NVARCHAR(255),
        Phone NVARCHAR(20),
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETDATE()
    );

    -- Thêm chi nhánh mặc định
    INSERT INTO Branches (BranchCode, Name, Address) 
    VALUES ('CN01', N'Chi nhánh chính', N'Địa chỉ chi nhánh chính');
    
    PRINT N'Đã tạo bảng Branches'
END
ELSE
BEGIN
    PRINT N'Bảng Branches đã tồn tại'
END
GO

-- 2. Thêm BranchId vào bảng Invoices (nếu bảng tồn tại)
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Invoices')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Invoices') AND name = 'BranchId')
    BEGIN
        ALTER TABLE Invoices ADD BranchId INT;
        
        -- Set mặc định là chi nhánh chính (ID=1)
        UPDATE Invoices SET BranchId = 1 WHERE BranchId IS NULL;
        
        -- Thêm FK constraint (nếu Branches tồn tại)
        IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Branches')
        BEGIN
            ALTER TABLE Invoices ADD CONSTRAINT FK_Invoices_Branches 
                FOREIGN KEY (BranchId) REFERENCES Branches(BranchId);
        END
        
        PRINT N'Đã thêm BranchId vào Invoices'
    END
    ELSE
    BEGIN
        PRINT N'Cột BranchId đã tồn tại trong Invoices'
    END
END
ELSE
BEGIN
    PRINT N'Bảng Invoices chưa tồn tại - bỏ qua'
END
GO

-- 3. Thêm BranchId vào bảng Employees (nếu bảng tồn tại)
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Employees') AND name = 'BranchId')
    BEGIN
        ALTER TABLE Employees ADD BranchId INT;
        
        -- Set mặc định là chi nhánh chính
        UPDATE Employees SET BranchId = 1 WHERE BranchId IS NULL;
        
        -- Thêm FK constraint (nếu Branches tồn tại)
        IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Branches')
        BEGIN
            ALTER TABLE Employees ADD CONSTRAINT FK_Employees_Branches 
                FOREIGN KEY (BranchId) REFERENCES Branches(BranchId);
        END
        
        PRINT N'Đã thêm BranchId vào Employees'
    END
    ELSE
    BEGIN
        PRINT N'Cột BranchId đã tồn tại trong Employees'
    END
END
ELSE
BEGIN
    PRINT N'Bảng Employees chưa tồn tại - bỏ qua'
END
GO

PRINT N'====================================='
PRINT N'Hoàn thành script đa chi nhánh'
PRINT N'====================================='
