using System;
using System.Linq;
using System.Collections.Generic;
using QuanLyTiemDaQuy.Core.Models;
using QuanLyTiemDaQuy.Core.DAL.Repositories;
using QuanLyTiemDaQuy.Core.Interfaces;

namespace QuanLyTiemDaQuy.Core.BLL.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DiscountRepository _discountRepository;

        public DiscountService()
        {
            _discountRepository = new DiscountRepository();
        }

        public List<DiscountRule> GetAllRules() => _discountRepository.GetAll();

        public (bool Success, string Message) AddRule(DiscountRule rule)
        {
            if (string.IsNullOrWhiteSpace(rule.Name))
                return (false, "Tên chương trình không được để trống");
            
            if (rule.DiscountPercent < 0 || rule.DiscountPercent > 100)
                return (false, "Phần trăm giảm giá không hợp lệ");

            int id = _discountRepository.Add(rule);
            return id > 0 ? (true, "Thêm thành công") : (false, "Lỗi khi thêm");
        }

        public (bool Success, string Message) UpdateRule(DiscountRule rule)
        {
            if (string.IsNullOrWhiteSpace(rule.Name))
                return (false, "Tên chương trình không được để trống");

            bool success = _discountRepository.Update(rule);
            return success ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
        }

        public (bool Success, string Message) DeleteRule(int id)
        {
            bool success = _discountRepository.Delete(id);
            return success ? (true, "Xóa thành công") : (false, "Lỗi khi xóa");
        }

        /// <summary>
        /// Tính toán giảm giá tốt nhất cho khách hàng tại thời điểm hiện tại
        /// </summary>
        public (decimal DiscountPercent, string Reason) CalculateBestDiscount(Customer customer, DateTime date)
        {
            if (customer == null) return (0, "");

            var activeRules = _discountRepository.GetActiveRules(date);
            
            // Filter applicable rules based on Tier
            // Use Contains check for flexible matching "VIP" in "VVIP,VIP" rule etc.
            var applicableRules = activeRules
                .Where(r => r.IsValid(date, customer.Tier))
                .OrderByDescending(r => r.Priority) // Priority first
                .ThenByDescending(r => r.DiscountPercent) // Then highest discount
                .ToList();

            if (applicableRules.Any())
            {
                var bestRule = applicableRules.First();
                return (bestRule.DiscountPercent, bestRule.Name);
            }

            return (0, "");
        }
    }
}
