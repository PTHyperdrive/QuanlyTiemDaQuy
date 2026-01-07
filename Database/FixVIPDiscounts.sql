-- Fix VIP/VVIP discount percentages in existing DiscountRules table
-- Run this script to update existing records if the table already exists
-- Based on user's WinForms discount management form: VIP=10%, VVIP=25%

-- Update VIP discount to 10%
UPDATE DiscountRules 
SET DiscountPercent = 10, Priority = 10
WHERE ApplicableTier = 'VIP' AND Name LIKE N'%VIP%';

-- Update VVIP discount to 25%
UPDATE DiscountRules 
SET DiscountPercent = 25, Priority = 20
WHERE ApplicableTier = 'VVIP' AND Name LIKE N'%VVIP%';

-- Verify the changes
SELECT * FROM DiscountRules ORDER BY Priority DESC;
