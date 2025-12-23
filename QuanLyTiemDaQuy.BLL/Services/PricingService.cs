using System;
using System.Collections.Generic;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service tính giá thu mua đá quý tự động
    /// </summary>
    public class PricingService
    {
        private readonly MarketPriceRepository _marketPriceRepo;
        
        // Hệ số lợi nhuận khi thu mua (0.7 = lợi nhuận 30%)
        private const decimal PURCHASE_MARGIN = 0.7m;
        
        // Giới hạn điều chỉnh giá: ±30%
        private const decimal MAX_ADJUSTMENT_PERCENT = 0.30m;

        public PricingService()
        {
            _marketPriceRepo = new MarketPriceRepository();
        }

        #region Price Calculation

        /// <summary>
        /// Tính giá thu mua đề xuất dựa trên 4C
        /// </summary>
        public PurchasePriceResult CalculatePurchasePrice(
            int stoneTypeId, 
            decimal carat, 
            string colorGrade, 
            string clarityGrade, 
            string cutGrade)
        {
            var result = new PurchasePriceResult();

            // Lấy giá cơ sở
            var marketPrice = _marketPriceRepo.GetByStoneType(stoneTypeId);
            if (marketPrice == null)
            {
                result.SuggestedPrice = 0;
                result.PriceBreakdown = "Không tìm thấy giá thị trường cho loại đá này";
                return result;
            }

            decimal basePrice = marketPrice.BasePricePerCarat;

            // Lấy các hệ số
            decimal colorMult = _marketPriceRepo.GetColorMultiplier(colorGrade);
            decimal clarityMult = _marketPriceRepo.GetClarityMultiplier(clarityGrade);
            decimal cutMult = _marketPriceRepo.GetCutMultiplier(cutGrade);

            // Tính giá trị thị trường
            decimal marketValue = basePrice * carat * colorMult * clarityMult * cutMult;
            
            // Giá thu mua đề xuất (70% giá trị thị trường)
            decimal suggestedPrice = Math.Round(marketValue * PURCHASE_MARGIN, 0);

            // Giới hạn min/max (±30% so với giá đề xuất)
            decimal minPrice = Math.Round(suggestedPrice * (1 - MAX_ADJUSTMENT_PERCENT), 0);
            decimal maxPrice = Math.Round(suggestedPrice * (1 + MAX_ADJUSTMENT_PERCENT), 0);

            result.BasePrice = basePrice;
            result.SuggestedPrice = suggestedPrice;
            result.MinPrice = minPrice;
            result.MaxPrice = maxPrice;
            result.PriceBreakdown = FormatPriceBreakdown(
                marketPrice.StoneTypeName, basePrice, carat, 
                colorGrade, colorMult, 
                clarityGrade, clarityMult, 
                cutGrade, cutMult, 
                marketValue, suggestedPrice);

            return result;
        }

        /// <summary>
        /// Kiểm tra giá nhập có hợp lệ (trong phạm vi ±30%)
        /// </summary>
        public (bool IsValid, string Message) ValidatePurchasePrice(
            decimal inputPrice, 
            decimal suggestedPrice)
        {
            decimal minPrice = suggestedPrice * (1 - MAX_ADJUSTMENT_PERCENT);
            decimal maxPrice = suggestedPrice * (1 + MAX_ADJUSTMENT_PERCENT);

            if (inputPrice < minPrice)
            {
                return (false, $"Giá thu mua quá thấp! Tối thiểu: {minPrice:N0} VNĐ (-30%)");
            }

            if (inputPrice > maxPrice)
            {
                return (false, $"Giá thu mua quá cao! Tối đa: {maxPrice:N0} VNĐ (+30%)");
            }

            return (true, "Giá hợp lệ");
        }

        private string FormatPriceBreakdown(
            string stoneName, decimal basePrice, decimal carat,
            string color, decimal colorMult,
            string clarity, decimal clarityMult,
            string cut, decimal cutMult,
            decimal marketValue, decimal suggestedPrice)
        {
            return $@"=== BẢNG TÍNH GIÁ THU MUA ===
Loại đá: {stoneName}
Giá cơ sở: {basePrice:N0} VNĐ/carat
─────────────────────────────
Carat: {carat:N2}
Color ({color}): x{colorMult:N2}
Clarity ({clarity}): x{clarityMult:N2}
Cut ({cut}): x{cutMult:N2}
─────────────────────────────
Giá trị thị trường: {marketValue:N0} VNĐ
Hệ số thu mua: x0.70 (lợi nhuận 30%)
─────────────────────────────
GIÁ THU MUA ĐỀ XUẤT: {suggestedPrice:N0} VNĐ";
        }

        #endregion

        #region Lookup Data

        public List<GemstoneMarketPrice> GetAllMarketPrices()
        {
            return _marketPriceRepo.GetAllMarketPrices();
        }

        public List<ColorGrade> GetAllColorGrades()
        {
            return _marketPriceRepo.GetAllColorGrades();
        }

        public List<ClarityGrade> GetAllClarityGrades()
        {
            return _marketPriceRepo.GetAllClarityGrades();
        }

        public List<CutGrade> GetAllCutGrades()
        {
            return _marketPriceRepo.GetAllCutGrades();
        }

        public bool UpdateMarketPrice(int stoneTypeId, decimal basePricePerCarat)
        {
            return _marketPriceRepo.UpdateMarketPrice(stoneTypeId, basePricePerCarat);
        }

        #endregion
    }
}
