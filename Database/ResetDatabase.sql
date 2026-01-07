-- =====================================================
-- RESET DATABASE SCRIPT
-- Xóa toàn bộ dữ liệu test và reset về trạng thái ban đầu
-- ⚠️ CẢNH BÁO: Script này sẽ XÓA TOÀN BỘ dữ liệu!
-- =====================================================

USE QuanlyTiemDaQuy;
GO

-- Tắt các ràng buộc FK tạm thời
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
GO

-- 1. Xóa chi tiết hoá đơn
DELETE FROM InvoiceDetails;
PRINT N'Đã xóa InvoiceDetails';

-- 2. Xóa hoá đơn  
DELETE FROM Invoices;
PRINT N'Đã xóa Invoices';

-- 3. Xóa khách hàng
DELETE FROM Customers;
PRINT N'Đã xóa Customers';

-- 4. Xóa sản phẩm
DELETE FROM Products;
PRINT N'Đã xóa Products';

-- 5. Xóa certificates
DELETE FROM Certificates;
PRINT N'Đã xóa Certificates';

-- 6. Xóa chi nhánh (giữ lại 1 chi nhánh chính)
DELETE FROM Branches WHERE BranchId > 1;
UPDATE Branches SET BranchCode = 'CN01', Name = N'Chi nhánh chính', Address = N'', Phone = '' WHERE BranchId = 1;
PRINT N'Đã reset Branches';

-- 7. Reset tài khoản nhân viên (giữ lại admin)
DELETE FROM Employees WHERE Username != 'admin';
UPDATE Employees SET PasswordHash = 'admin123', BranchId = 1 WHERE Username = 'admin';
PRINT N'Đã reset Employees (chỉ giữ admin)';

-- 8. Reset IDENTITY seeds
DBCC CHECKIDENT ('Invoices', RESEED, 0);
DBCC CHECKIDENT ('InvoiceDetails', RESEED, 0);
DBCC CHECKIDENT ('Customers', RESEED, 0);
DBCC CHECKIDENT ('Products', RESEED, 0);
DBCC CHECKIDENT ('Certificates', RESEED, 0);
PRINT N'Đã reset IDENTITY seeds';

-- Bật lại các ràng buộc FK
EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';
GO

PRINT N'========================================';
PRINT N'RESET DATABASE HOÀN TẤT!';
PRINT N'========================================';
