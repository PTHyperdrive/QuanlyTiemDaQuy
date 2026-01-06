using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyTiemDaQuy.DAL;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    public class DiscountService
    {
        private readonly DiscountRepository _discountRepository;

        public DiscountService()
        {
            _discountRepository = new DiscountRepository();
            // Auto-ensure table exists when service is initialized for the first time
            // Or ideally call this from Program.cs, but putting it here ensures it works if accessed
            try 
            {
                _discountRepository.EnsureTableExists();
            }
            catch { /* Ignore if fails due to permissions, though invalid */ }
        }

        public List<DiscountRule> GetAllRules() => _discountRepository.GetAll();

        public bool AddRule(DiscountRule rule, out string message)
        {
            if (string.IsNullOrEmpty(rule.Name))
            {
                message = "Tên chương trình không được để trống";
                return false;
            }
            
            if (rule.DiscountPercent < 0 || rule.DiscountPercent > 100)
            {
                message = "Phần trăm giảm giá không hợp lệ";
                return false;
            }

            int id = _discountRepository.Add(rule);
            if (id > 0)
            {
                message = "Thêm thành công";
                return true;
            }
            
            message = "Lỗi khi thêm";
            return false;
        }

        public bool UpdateRule(DiscountRule rule, out string message)
        {
            if (string.IsNullOrEmpty(rule.Name))
            {
                message = "Tên chương trình không được để trống";
                return false;
            }

            if (_discountRepository.Update(rule))
            {
                message = "Cập nhật thành công";
                return true;
            }
            
            message = "Lỗi khi cập nhật";
            return false;
        }

        public bool DeleteRule(int id, out string message)
        {
            if (_discountRepository.Delete(id))
            {
                message = "Xóa thành công";
                return true;
            }
            
            message = "Lỗi khi xóa";
            return false;
        }
    }
}
