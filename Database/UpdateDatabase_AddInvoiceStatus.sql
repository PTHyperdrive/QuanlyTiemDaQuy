-- =========================================
-- Quản Lý Tiệm Đá Quý - Database Update Script
-- Thêm cột Status cho Invoices
-- Chạy script này trên database đã tồn tại
-- =========================================

USE QuanLyTiemDaQuy;
GO

-- Thêm cột Status vào bảng Invoices nếu chưa tồn tại
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Invoices') AND name = 'Status')
BEGIN
    ALTER TABLE Invoices
    ADD Status NVARCHAR(50) DEFAULT N'Đang chờ thanh toán' NOT NULL;
    
    PRINT N'Đã thêm cột Status vào bảng Invoices';
END
ELSE
BEGIN
    PRINT N'Cột Status đã tồn tại trong bảng Invoices';
END
GO

-- Cập nhật tất cả hoá đơn cũ thành "Đã xuất"
UPDATE Invoices 
SET Status = N'Đã xuất' 
WHERE Status = N'Đang chờ thanh toán' OR Status IS NULL;
GO

-- Tạo constraint CHECK cho Status (nếu chưa có)
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Invoices_Status')
BEGIN
    ALTER TABLE Invoices
    ADD CONSTRAINT CK_Invoices_Status 
    CHECK (Status IN (N'Đang chờ thanh toán', N'Đã xuất', N'Đã huỷ'));
    
    PRINT N'Đã thêm constraint CK_Invoices_Status';
END
GO

-- Thêm cột CancelledAt để lưu thời gian huỷ (nếu chưa có)
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Invoices') AND name = 'CancelledAt')
BEGIN
    ALTER TABLE Invoices
    ADD CancelledAt DATETIME NULL;
    
    PRINT N'Đã thêm cột CancelledAt vào bảng Invoices';
END
GO

-- Thêm cột CancelReason để lưu lý do huỷ (nếu chưa có)
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Invoices') AND name = 'CancelReason')
BEGIN
    ALTER TABLE Invoices
    ADD CancelReason NVARCHAR(500) NULL;
    
    PRINT N'Đã thêm cột CancelReason vào bảng Invoices';
END
GO

-- Tạo trigger để hoàn trả tồn kho khi huỷ hoá đơn
IF EXISTS (SELECT 1 FROM sys.triggers WHERE name = 'trg_InvoiceCancel_RestoreStock')
    DROP TRIGGER trg_InvoiceCancel_RestoreStock;
GO

CREATE TRIGGER trg_InvoiceCancel_RestoreStock
ON Invoices
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Nếu Status thay đổi từ khác sang 'Đã huỷ', hoàn trả tồn kho
    IF EXISTS (
        SELECT 1 FROM inserted i 
        INNER JOIN deleted d ON i.InvoiceId = d.InvoiceId
        WHERE i.Status = N'Đã huỷ' AND d.Status <> N'Đã huỷ'
    )
    BEGIN
        -- Cộng lại số lượng vào tồn kho
        UPDATE p
        SET p.StockQty = p.StockQty + id.Qty,
            p.Status = CASE WHEN p.StockQty + id.Qty > 0 THEN N'Còn hàng' ELSE p.Status END
        FROM Products p
        INNER JOIN InvoiceDetails id ON p.ProductId = id.ProductId
        INNER JOIN inserted i ON id.InvoiceId = i.InvoiceId
        INNER JOIN deleted d ON i.InvoiceId = d.InvoiceId
        WHERE i.Status = N'Đã huỷ' AND d.Status <> N'Đã huỷ';
    END
END
GO

PRINT N'Đã tạo trigger trg_InvoiceCancel_RestoreStock';
GO

-- Cập nhật view báo cáo doanh thu để chỉ tính hoá đơn "Đã xuất"
IF EXISTS (SELECT 1 FROM sys.views WHERE name = 'vw_SalesReport')
    DROP VIEW vw_SalesReport;
GO

CREATE VIEW vw_SalesReport AS
SELECT 
    CAST(i.InvoiceDate AS DATE) AS SaleDate,
    COUNT(DISTINCT i.InvoiceId) AS TotalInvoices,
    SUM(id.Qty) AS TotalItemsSold,
    SUM(i.Subtotal) AS GrossRevenue,
    SUM(i.DiscountAmount) AS TotalDiscount,
    SUM(i.VATAmount) AS TotalVAT,
    SUM(i.Total) AS NetRevenue
FROM Invoices i
INNER JOIN InvoiceDetails id ON i.InvoiceId = id.InvoiceId
WHERE i.Status = N'Đã xuất'  -- Chỉ tính hoá đơn đã xuất
GROUP BY CAST(i.InvoiceDate AS DATE);
GO

PRINT N'Đã cập nhật view vw_SalesReport';
GO

PRINT N'=== CẬP NHẬT DATABASE HOÀN TẤT ===';
GO
