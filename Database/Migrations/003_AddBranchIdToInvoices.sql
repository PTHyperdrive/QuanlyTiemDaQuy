-- =====================================================
-- Migration: Add BranchId to Invoices table
-- Date: 2026-01-07
-- Description: Support branch-based invoicing
-- =====================================================

-- Step 1: Add BranchId column to Invoices table (nullable for existing data)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'Invoices') AND name = 'BranchId')
BEGIN
    ALTER TABLE Invoices ADD BranchId INT NULL;
    PRINT 'Added BranchId column to Invoices table';
END
GO

-- Step 2: Add foreign key constraint
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Invoices_Branches')
BEGIN
    ALTER TABLE Invoices 
    ADD CONSTRAINT FK_Invoices_Branches 
    FOREIGN KEY (BranchId) REFERENCES Branches(BranchId);
    PRINT 'Added foreign key constraint FK_Invoices_Branches';
END
GO

-- Step 3: Update existing invoices with employee's branch
UPDATE i
SET i.BranchId = e.BranchId
FROM Invoices i
INNER JOIN Employees e ON i.EmployeeId = e.EmployeeId
WHERE i.BranchId IS NULL AND e.BranchId > 0;
PRINT 'Updated existing invoices with employee branch';
GO

-- Step 4: Update existing branches with proper naming (if needed)
-- Ensure first branch is CN01 (Chi nhánh chính)
UPDATE Branches 
SET BranchCode = 'CN01' 
WHERE BranchId = (SELECT MIN(BranchId) FROM Branches) 
AND BranchCode NOT LIKE 'CN%';
GO

-- Renumber other branches as CN02, CN03, etc.
;WITH NumberedBranches AS (
    SELECT BranchId, BranchCode,
           ROW_NUMBER() OVER (ORDER BY BranchId) + 1 AS NewNumber
    FROM Branches
    WHERE BranchId > (SELECT MIN(BranchId) FROM Branches)
)
UPDATE nb
SET BranchCode = 'CN' + RIGHT('0' + CAST(NewNumber AS VARCHAR(2)), 2)
FROM NumberedBranches nb
INNER JOIN Branches b ON nb.BranchId = b.BranchId
WHERE b.BranchCode NOT LIKE 'CN%';
GO

PRINT 'Migration completed successfully!';
GO
