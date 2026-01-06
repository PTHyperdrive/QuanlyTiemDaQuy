-- Migration: Create DiscountRules table
-- Date: 2026-01-07
-- Description: Table for managing dynamic discounts (tiered, seasonal, events)

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DiscountRules') AND type in (N'U'))
BEGIN
    CREATE TABLE DiscountRules (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        DiscountPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
        ApplicableTier NVARCHAR(50) NULL, -- 'All', 'VIP', 'VVIP', etc.
        StartDate DATETIME NULL,
        EndDate DATETIME NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        Priority INT NOT NULL DEFAULT 0 -- Higher value = higher priority
    );

    PRINT 'Created DiscountRules table';
    
    -- Seed default data based on previous hardcoded logic + request
    -- Normal: 0% (No rule needed or explicit 0)
    -- VIP: 10%
    INSERT INTO DiscountRules (Name, DiscountPercent, ApplicableTier, IsActive, Priority)
    VALUES (N'Giảm giá VIP', 10, 'VIP', 1, 10);
    
    -- VVIP: User said currently no discount, but code had 25%. 
    -- I will add a placeholder rule for VVIP but set it to 0 or disabled as per request "hiện tại vvip đang không có giảm giá"
    -- Actually, to avoid confusion, I'll add it as 0% active rule, or disabled.
    -- Let's add it as disabled 25% so they can easily enable it, OR just don't add it.
    -- Request: "các mức giảm giá vip có thể chỉnh thông qua 1 trang khác" -> Providing a rule makes it editable.
    INSERT INTO DiscountRules (Name, DiscountPercent, ApplicableTier, IsActive, Priority)
    VALUES (N'Giảm giá VVIP', 0, 'VVIP', 1, 10);

    PRINT 'Seeded default discount rules';
END
GO
