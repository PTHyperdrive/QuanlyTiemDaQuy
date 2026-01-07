-- =====================================================
-- ADD TEST DATABASE SCRIPT
-- Thêm dữ liệu test vào database (không xóa dữ liệu cũ)
-- =====================================================

USE QuanlyTiemDaQuy;
GO

-- =====================================================
-- PHẦN 1: THÊM STONETYPES (nếu chưa có)
-- =====================================================
IF NOT EXISTS (SELECT 1 FROM StoneTypes WHERE Name = N'Kim cương')
    INSERT INTO StoneTypes (Name) VALUES (N'Kim cương');
IF NOT EXISTS (SELECT 1 FROM StoneTypes WHERE Name = N'Ruby')
    INSERT INTO StoneTypes (Name) VALUES (N'Ruby');
IF NOT EXISTS (SELECT 1 FROM StoneTypes WHERE Name = N'Sapphire')
    INSERT INTO StoneTypes (Name) VALUES (N'Sapphire');
IF NOT EXISTS (SELECT 1 FROM StoneTypes WHERE Name = N'Ngọc lục bảo')
    INSERT INTO StoneTypes (Name) VALUES (N'Ngọc lục bảo');

PRINT N'Đã thêm StoneTypes';

-- =====================================================
-- PHẦN 2: THÊM CHI NHÁNH
-- =====================================================
IF NOT EXISTS (SELECT 1 FROM Branches WHERE BranchCode = 'NRSP')
    INSERT INTO Branches (BranchCode, Name, Address, Phone, IsActive)
    VALUES ('NRSP', N'Chi nhánh chính', N'123 Đường ABC, Quận 1, TP.HCM', '0901234567', 1);

IF NOT EXISTS (SELECT 1 FROM Branches WHERE BranchCode = 'CN02')
    INSERT INTO Branches (BranchCode, Name, Address, Phone, IsActive)
    VALUES ('CN02', N'Chi nhánh Quận 3', N'456 Đường XYZ, Quận 3, TP.HCM', '0902345678', 1);

IF NOT EXISTS (SELECT 1 FROM Branches WHERE BranchCode = 'CN03')
    INSERT INTO Branches (BranchCode, Name, Address, Phone, IsActive)
    VALUES ('CN03', N'Chi nhánh Bình Thạnh', N'789 Đường DEF, Bình Thạnh, TP.HCM', '0903456789', 1);

PRINT N'Đã thêm 3 chi nhánh';

-- =====================================================
-- PHẦN 3: THÊM NHÂN VIÊN (dùng cột Name, không phải FullName)
-- =====================================================
DECLARE @branchId INT = (SELECT TOP 1 BranchId FROM Branches WHERE BranchCode = 'NRSP');
IF @branchId IS NULL SET @branchId = 1;

DECLARE @branch2Id INT = (SELECT TOP 1 BranchId FROM Branches WHERE BranchCode = 'CN02');
IF @branch2Id IS NULL SET @branch2Id = @branchId;

IF NOT EXISTS (SELECT 1 FROM Employees WHERE Username = 'admin')
    INSERT INTO Employees (Username, PasswordHash, Name, Email, Phone, Role, BranchId, IsActive)
    VALUES ('admin', 'admin123', N'Quản trị viên', 'admin@gem.vn', '0900000000', 'Admin', @branchId, 1);

IF NOT EXISTS (SELECT 1 FROM Employees WHERE Username = 'nv01')
    INSERT INTO Employees (Username, PasswordHash, Name, Email, Phone, Role, BranchId, IsActive)
    VALUES ('nv01', 'nv123', N'Nguyễn Văn A', 'nva@gem.vn', '0911111111', 'Sales', @branchId, 1);

IF NOT EXISTS (SELECT 1 FROM Employees WHERE Username = 'nv02')
    INSERT INTO Employees (Username, PasswordHash, Name, Email, Phone, Role, BranchId, IsActive)
    VALUES ('nv02', 'nv123', N'Trần Thị B', 'ttb@gem.vn', '0922222222', 'Sales', @branch2Id, 1);

IF NOT EXISTS (SELECT 1 FROM Employees WHERE Username = 'manager')
    INSERT INTO Employees (Username, PasswordHash, Name, Email, Phone, Role, BranchId, IsActive)
    VALUES ('manager', 'manager123', N'Lê Văn C', 'lvc@gem.vn', '0933333333', 'Manager', @branchId, 1);

PRINT N'Đã thêm 4 nhân viên';

-- =====================================================
-- PHẦN 4: THÊM DISCOUNT RULES (dùng Name và ApplicableTier)
-- =====================================================
IF NOT EXISTS (SELECT 1 FROM DiscountRules WHERE ApplicableTier = 'VIP')
    INSERT INTO DiscountRules (Name, DiscountPercent, ApplicableTier, IsActive, Priority)
    VALUES (N'Giảm giá VIP', 10, 'VIP', 1, 10);

IF NOT EXISTS (SELECT 1 FROM DiscountRules WHERE ApplicableTier = 'VVIP')
    INSERT INTO DiscountRules (Name, DiscountPercent, ApplicableTier, IsActive, Priority)
    VALUES (N'Giảm giá VVIP', 25, 'VVIP', 1, 20);

PRINT N'Đã thêm DiscountRules (VIP 10%, VVIP 25%)';

-- =====================================================
-- PHẦN 5: THÊM 100 SẢN PHẨM
-- =====================================================
DECLARE @stoneTypeKC INT = (SELECT TOP 1 StoneTypeId FROM StoneTypes WHERE Name = N'Kim cương');
DECLARE @stoneTypeRB INT = (SELECT TOP 1 StoneTypeId FROM StoneTypes WHERE Name = N'Ruby');
DECLARE @stoneTypeSP INT = (SELECT TOP 1 StoneTypeId FROM StoneTypes WHERE Name = N'Sapphire');
DECLARE @stoneTypeNL INT = (SELECT TOP 1 StoneTypeId FROM StoneTypes WHERE Name = N'Ngọc lục bảo');

DECLARE @i INT = 1;
DECLARE @stoneType INT;
DECLARE @carat DECIMAL(5,2);
DECLARE @color NVARCHAR(20);
DECLARE @clarity NVARCHAR(20);
DECLARE @cut NVARCHAR(20);
DECLARE @costPrice DECIMAL(18,2);
DECLARE @sellPrice DECIMAL(18,2);
DECLARE @stockQty INT;
DECLARE @productCode NVARCHAR(20);
DECLARE @productName NVARCHAR(100);
DECLARE @displayLocation NVARCHAR(50);

WHILE @i <= 100
BEGIN
    -- Stone type theo thứ tự: KC, RB, SP, NL
    SET @stoneType = CASE ((@i - 1) % 4)
        WHEN 0 THEN @stoneTypeKC
        WHEN 1 THEN @stoneTypeRB
        WHEN 2 THEN @stoneTypeSP
        ELSE @stoneTypeNL
    END;
    
    -- Random carat (0.3 - 5.0)
    SET @carat = CAST(0.3 + (RAND(CHECKSUM(NEWID())) * 4.7) AS DECIMAL(5,2));
    
    -- Colors theo loại đá
    SET @color = CASE ((@i - 1) % 4)
        WHEN 0 THEN CASE (@i % 6) WHEN 0 THEN 'D' WHEN 1 THEN 'E' WHEN 2 THEN 'F' WHEN 3 THEN 'G' WHEN 4 THEN 'H' ELSE 'I' END
        WHEN 1 THEN CASE (@i % 3) WHEN 0 THEN N'Đỏ tươi' WHEN 1 THEN N'Đỏ đậm' ELSE N'Đỏ hồng' END
        WHEN 2 THEN CASE (@i % 3) WHEN 0 THEN N'Xanh dương' WHEN 1 THEN N'Xanh đậm' ELSE N'Xanh nhạt' END
        ELSE CASE (@i % 3) WHEN 0 THEN N'Xanh lục' WHEN 1 THEN N'Xanh đậm' ELSE N'Xanh tươi' END
    END;
    
    -- Clarity
    SET @clarity = CASE (@i % 8)
        WHEN 0 THEN 'IF' WHEN 1 THEN 'VVS1' WHEN 2 THEN 'VVS2' WHEN 3 THEN 'VS1'
        WHEN 4 THEN 'VS2' WHEN 5 THEN 'SI1' WHEN 6 THEN 'SI2' ELSE 'Eye Clean'
    END;
    
    -- Cut
    SET @cut = CASE (@i % 5)
        WHEN 0 THEN 'Excellent' WHEN 1 THEN 'Very Good' WHEN 2 THEN 'Good' WHEN 3 THEN 'Oval' ELSE 'Cushion'
    END;
    
    -- Price based on carat
    SET @costPrice = CAST((@carat * 50000000 + (RAND(CHECKSUM(NEWID())) * 30000000)) AS DECIMAL(18,2));
    SET @sellPrice = CAST(@costPrice * 1.35 AS DECIMAL(18,2));
    
    -- Stock
    SET @stockQty = 1 + (ABS(CHECKSUM(NEWID())) % 5);
    
    -- Product code
    SET @productCode = CASE ((@i - 1) % 4)
        WHEN 0 THEN 'KC-' WHEN 1 THEN 'RB-' WHEN 2 THEN 'SP-' ELSE 'NL-'
    END + RIGHT('000' + CAST(@i AS NVARCHAR(4)), 3);
    
    -- Product name
    SET @productName = CASE ((@i - 1) % 4)
        WHEN 0 THEN N'Kim cương ' + @cut + ' ' + CAST(@carat AS NVARCHAR(5)) + ' carat ' + @color + ' ' + @clarity
        WHEN 1 THEN N'Ruby ' + @color + ' ' + CAST(@carat AS NVARCHAR(5)) + ' carat ' + @clarity
        WHEN 2 THEN N'Sapphire ' + @color + ' ' + CAST(@carat AS NVARCHAR(5)) + ' carat ' + @clarity
        ELSE N'Ngọc lục bảo ' + @color + ' ' + CAST(@carat AS NVARCHAR(5)) + ' carat ' + @clarity
    END;
    
    -- Display location: Tủ A1, B2, C3, ...
    SET @displayLocation = N'Tủ ' + CHAR(65 + ((@i - 1) % 5)) + CAST(((@i - 1) / 5) + 1 AS NVARCHAR(3));
    
    -- Insert product nếu chưa tồn tại
    IF NOT EXISTS (SELECT 1 FROM Products WHERE ProductCode = @productCode)
    BEGIN
        INSERT INTO Products (ProductCode, Name, StoneTypeId, Carat, Color, Clarity, Cut, 
            CostPrice, SellPrice, StockQty, Status, ImagePath, CertId, DisplayLocation)
        VALUES (@productCode, @productName, @stoneType, @carat, @color, @clarity, @cut, 
            @costPrice, @sellPrice, @stockQty, N'Còn hàng', NULL, NULL, @displayLocation);
    END
    
    SET @i = @i + 1;
END

PRINT N'Đã thêm 100 sản phẩm';

-- =====================================================
-- PHẦN 6: THÊM 30 KHÁCH HÀNG
-- =====================================================
INSERT INTO Customers (Name, Phone, Email, Address, Tier, TotalPurchase)
SELECT * FROM (VALUES 
    (N'Nguyễn Văn Minh', '0901111111', 'minh@email.com', N'123 Lê Lợi, Q1', N'Thường', 0),
    (N'Trần Thị Hoa', '0902222222', 'hoa@email.com', N'456 Nguyễn Huệ, Q1', N'Thường', 0),
    (N'Lê Văn Tùng', '0903333333', 'tung@email.com', N'789 Đồng Khởi, Q1', N'VIP', 600000000),
    (N'Phạm Thị Mai', '0904444444', 'mai@email.com', N'321 Hai Bà Trưng, Q3', N'VVIP', 1500000000),
    (N'Hoàng Văn Nam', '0905555555', 'nam@email.com', N'654 Võ Văn Tần, Q3', N'Thường', 0),
    (N'Đỗ Thị Lan', '0906666666', 'lan@email.com', N'987 Pasteur, Q3', N'Thường', 150000000),
    (N'Vũ Văn Hùng', '0907777777', 'hung@email.com', N'147 CMT8, Q10', N'VIP', 750000000),
    (N'Bùi Thị Ngọc', '0908888888', 'ngoc@email.com', N'258 Lý Thường Kiệt, Q10', N'Thường', 50000000),
    (N'Đặng Văn Long', '0909999999', 'long@email.com', N'369 Điện Biên Phủ, BT', N'VVIP', 2000000000),
    (N'Ngô Thị Hạnh', '0910000000', 'hanh@email.com', N'741 Xô Viết Nghệ Tĩnh, BT', N'Thường', 0),
    (N'Lý Văn Đức', '0911111112', 'duc@email.com', N'852 Phan Xích Long, PN', N'VIP', 550000000),
    (N'Trương Thị Yến', '0912222223', 'yen@email.com', N'963 Nguyễn Văn Đậu, BT', N'Thường', 200000000),
    (N'Cao Văn Thắng', '0913333334', 'thang@email.com', N'159 Lê Quang Định, BT', N'Thường', 0),
    (N'Đinh Thị Thu', '0914444445', 'thu@email.com', N'357 Hoàng Văn Thụ, TB', N'VVIP', 1200000000),
    (N'Tạ Văn Phong', '0915555556', 'phong@email.com', N'468 Cộng Hòa, TB', N'Thường', 100000000),
    (N'Phan Thị Oanh', '0916666667', 'oanh@email.com', N'579 Lạc Long Quân, Q11', N'VIP', 800000000),
    (N'Hồ Văn Khoa', '0917777778', 'khoa@email.com', N'680 Ba Tháng Hai, Q10', N'Thường', 0),
    (N'Lâm Thị Diễm', '0918888889', 'diem@email.com', N'791 Sư Vạn Hạnh, Q10', N'Thường', 300000000),
    (N'Châu Văn Bình', '0919999990', 'binh@email.com', N'802 Trần Hưng Đạo, Q5', N'VIP', 650000000),
    (N'Mai Thị Xuân', '0920000001', 'xuan@email.com', N'913 An Dương Vương, Q5', N'Thường', 0),
    (N'Trịnh Văn Tài', '0921111111', 'tai@email.com', N'100 Nguyễn Trãi, Q5', N'VIP', 520000000),
    (N'Võ Thị Kim', '0922222222', 'kim@email.com', N'200 Hùng Vương, Q6', N'Thường', 80000000),
    (N'Dương Văn Sơn', '0923333333', 'son@email.com', N'300 Kinh Dương Vương, Q6', N'VVIP', 1800000000),
    (N'Lưu Thị Hồng', '0924444444', 'hong@email.com', N'400 Bình Phú, Q6', N'Thường', 0),
    (N'Tô Văn Quang', '0925555555', 'quang@email.com', N'500 Tân Hòa Đông, Q6', N'VIP', 700000000),
    (N'Phùng Thị Linh', '0926666666', 'linh@email.com', N'600 Phạm Văn Chí, Q6', N'Thường', 250000000),
    (N'Kiều Văn Hải', '0927777777', 'hai@email.com', N'700 Hậu Giang, Q6', N'Thường', 0),
    (N'Doãn Thị Thảo', '0928888888', 'thao@email.com', N'800 Bình Tiên, Q6', N'VIP', 580000000),
    (N'Mạc Văn Tú', '0929999999', 'tu@email.com', N'900 Minh Phụng, Q6', N'Thường', 120000000),
    (N'Ông Thị Quyên', '0930000000', 'quyen@email.com', N'1000 Cao Văn Lầu, Q6', N'VVIP', 1100000000)
) AS NewCustomers(Name, Phone, Email, Address, Tier, TotalPurchase)
WHERE NOT EXISTS (SELECT 1 FROM Customers c WHERE c.Phone = NewCustomers.Phone);

PRINT N'Đã thêm 30 khách hàng';

-- =====================================================
-- PHẦN 7: TẠO CHỨNG CHỈ QUỐC TẾ VỀ ĐÁ QUÝ
-- International Gemstone Certificates:
-- GIA (Gemological Institute of America) - Uy tín nhất thế giới
-- IGI (International Gemological Institute) - Phổ biến châu Âu/Á
-- HRD (Hoge Raad voor Diamant) - Antwerp, Bỉ
-- AGS (American Gem Society) - Mỹ
-- Gübelin - Thụy Sĩ, chuyên đá màu
-- =====================================================

DECLARE @certId INT;
DECLARE @productId INT;
DECLARE @certCode NVARCHAR(50);
DECLARE @issuer NVARCHAR(100);
DECLARE @issueDate DATETIME;
DECLARE @k INT = 1;

-- Tạo chứng chỉ cho mỗi sản phẩm
DECLARE product_cursor CURSOR FOR 
    SELECT ProductId FROM Products WHERE CertId IS NULL;

OPEN product_cursor;
FETCH NEXT FROM product_cursor INTO @productId;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Xác định tổ chức cấp chứng chỉ theo loại sản phẩm
    SET @issuer = CASE (@k % 5)
        WHEN 0 THEN 'GIA - Gemological Institute of America'
        WHEN 1 THEN 'IGI - International Gemological Institute'
        WHEN 2 THEN 'HRD Antwerp'
        WHEN 3 THEN 'AGS - American Gem Society'
        ELSE N'Gübelin Gem Lab (Switzerland)'
    END;
    
    -- Tạo mã chứng chỉ theo format quốc tế: {Issuer Code}{Year}{Sequence}
    SET @certCode = CASE (@k % 5)
        WHEN 0 THEN 'GIA' + FORMAT(GETDATE(), 'yy') + RIGHT('000000' + CAST(@productId AS VARCHAR(10)), 6)
        WHEN 1 THEN 'IGI' + FORMAT(GETDATE(), 'yy') + RIGHT('000000' + CAST(@productId AS VARCHAR(10)), 6)
        WHEN 2 THEN 'HRD' + FORMAT(GETDATE(), 'yy') + RIGHT('000000' + CAST(@productId AS VARCHAR(10)), 6)
        WHEN 3 THEN 'AGS' + FORMAT(GETDATE(), 'yy') + RIGHT('000000' + CAST(@productId AS VARCHAR(10)), 6)
        ELSE 'GBL' + FORMAT(GETDATE(), 'yy') + RIGHT('000000' + CAST(@productId AS VARCHAR(10)), 6)
    END;
    
    -- Ngày cấp ngẫu nhiên trong 2 năm gần đây
    SET @issueDate = DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 730), GETDATE());
    
    -- Tạo chứng chỉ
    INSERT INTO Certificates (CertCode, Issuer, IssueDate)
    VALUES (@certCode, @issuer, @issueDate);
    
    SET @certId = SCOPE_IDENTITY();
    
    -- Gán chứng chỉ cho sản phẩm
    UPDATE Products SET CertId = @certId WHERE ProductId = @productId;
    
    SET @k = @k + 1;
    FETCH NEXT FROM product_cursor INTO @productId;
END

CLOSE product_cursor;
DEALLOCATE product_cursor;

PRINT N'Đã tạo chứng chỉ quốc tế và gán cho sản phẩm';
PRINT N'- GIA (Gemological Institute of America)';
PRINT N'- IGI (International Gemological Institute)';
PRINT N'- HRD Antwerp (Bỉ)';
PRINT N'- AGS (American Gem Society)';
PRINT N'- Gübelin Gem Lab (Thụy Sĩ)';

-- =====================================================
-- HOÀN TẤT
-- =====================================================
PRINT N'========================================';
PRINT N'ADD TEST DATABASE HOÀN TẤT!';
PRINT N'- 100 sản phẩm (KC, RB, SP, NL)';
PRINT N'- 100 chứng chỉ quốc tế (GIA, IGI, HRD, AGS, Gübelin)';
PRINT N'- 30 khách hàng (Thường, VIP, VVIP)';
PRINT N'- 3 chi nhánh (NRSP, CN02, CN03)';
PRINT N'- 4 nhân viên (admin, nv01, nv02, manager)';
PRINT N'- Discount Rules (VIP 10%, VVIP 25%)';
PRINT N'========================================';

