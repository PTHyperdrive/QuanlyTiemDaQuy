-- =============================================
-- Cập nhật Database: Thêm bảng giá thị trường
-- =============================================

-- Bảng giá cơ sở theo loại đá
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'GemstoneMarketPrices')
BEGIN
    CREATE TABLE GemstoneMarketPrices (
        Id INT IDENTITY PRIMARY KEY,
        StoneTypeId INT NOT NULL REFERENCES StoneTypes(StoneTypeId),
        BasePricePerCarat DECIMAL(18,2) NOT NULL, -- Giá cơ sở/carat (VNĐ)
        LastUpdated DATETIME DEFAULT GETDATE()
    );
END
GO

-- Bảng hệ số màu sắc (Color Grade)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ColorGrades')
BEGIN
    CREATE TABLE ColorGrades (
        Grade NVARCHAR(10) PRIMARY KEY,
        Multiplier DECIMAL(5,2) NOT NULL,
        Description NVARCHAR(100)
    );
    
    INSERT INTO ColorGrades VALUES 
    ('D', 1.50, N'Colorless - Không màu hoàn hảo'),
    ('E', 1.40, N'Colorless - Không màu'),
    ('F', 1.30, N'Colorless - Không màu'),
    ('G', 1.20, N'Near Colorless - Gần không màu'),
    ('H', 1.10, N'Near Colorless - Gần không màu'),
    ('I', 1.00, N'Near Colorless - Gần không màu'),
    ('J', 0.90, N'Near Colorless - Gần không màu'),
    ('K', 0.80, N'Faint - Màu nhạt'),
    ('L', 0.70, N'Faint - Màu nhạt'),
    ('M', 0.60, N'Faint - Màu nhạt');
END
GO

-- Bảng hệ số độ tinh khiết (Clarity Grade)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ClarityGrades')
BEGIN
    CREATE TABLE ClarityGrades (
        Grade NVARCHAR(10) PRIMARY KEY,
        Multiplier DECIMAL(5,2) NOT NULL,
        Description NVARCHAR(100)
    );
    
    INSERT INTO ClarityGrades VALUES
    ('FL', 1.60, N'Flawless - Hoàn hảo'),
    ('IF', 1.50, N'Internally Flawless - Gần hoàn hảo'),
    ('VVS1', 1.35, N'Very Very Slightly Included'),
    ('VVS2', 1.25, N'Very Very Slightly Included'),
    ('VS1', 1.15, N'Very Slightly Included'),
    ('VS2', 1.05, N'Very Slightly Included'),
    ('SI1', 0.95, N'Slightly Included'),
    ('SI2', 0.85, N'Slightly Included'),
    ('I1', 0.70, N'Included - Có tạp chất'),
    ('I2', 0.60, N'Included - Có tạp chất nhiều'),
    ('I3', 0.50, N'Included - Có tạp chất rất nhiều');
END
GO

-- Bảng hệ số cắt (Cut Grade)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CutGrades')
BEGIN
    CREATE TABLE CutGrades (
        Grade NVARCHAR(20) PRIMARY KEY,
        Multiplier DECIMAL(5,2) NOT NULL
    );
    
    INSERT INTO CutGrades VALUES
    ('Excellent', 1.20),
    ('Very Good', 1.10),
    ('Good', 1.00),
    ('Fair', 0.90),
    ('Poor', 0.80);
END
GO

-- Thêm giá cơ sở mẫu cho các loại đá
-- (Giá VNĐ/carat, chỉ để tham khảo - cần cập nhật theo giá thị trường thực tế)
INSERT INTO GemstoneMarketPrices (StoneTypeId, BasePricePerCarat) 
SELECT StoneTypeId, 
    CASE Name
        WHEN N'Kim cương' THEN 150000000   -- 150 triệu/carat
        WHEN N'Ruby' THEN 80000000         -- 80 triệu/carat
        WHEN N'Sapphire' THEN 60000000     -- 60 triệu/carat
        WHEN N'Emerald' THEN 70000000      -- 70 triệu/carat
        WHEN N'Opal' THEN 20000000         -- 20 triệu/carat
        ELSE 10000000                       -- 10 triệu mặc định
    END
FROM StoneTypes
WHERE NOT EXISTS (SELECT 1 FROM GemstoneMarketPrices WHERE GemstoneMarketPrices.StoneTypeId = StoneTypes.StoneTypeId);
GO

PRINT N'====================================='
PRINT N'Đã tạo bảng giá thị trường đá quý'
PRINT N'====================================='
