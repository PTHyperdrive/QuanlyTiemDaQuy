using System;
using System.Collections.Generic;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service cho quản lý khách hàng
    /// </summary>
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer? GetCustomerById(int customerId)
        {
            return _customerRepository.GetById(customerId);
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            return _customerRepository.Search(keyword);
        }

        public List<Customer> GetCustomersByTier(string tier)
        {
            return _customerRepository.GetByTier(tier);
        }

        public (bool Success, string Message, int CustomerId) AddCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                return (false, "Tên khách hàng không được để trống", 0);

            try
            {
                int id = _customerRepository.Insert(customer);
                return (true, "Thêm khách hàng thành công", id);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", 0);
            }
        }

        public (bool Success, string Message) UpdateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                return (false, "Tên khách hàng không được để trống");

            try
            {
                bool success = _customerRepository.Update(customer);
                if (success)
                    return (true, "Cập nhật khách hàng thành công");
                else
                    return (false, "Không tìm thấy khách hàng");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public (bool Success, string Message) DeleteCustomer(int customerId)
        {
            try
            {
                bool success = _customerRepository.Delete(customerId);
                if (success)
                    return (true, "Xóa khách hàng thành công");
                else
                    return (false, "Không tìm thấy khách hàng");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE"))
                    return (false, "Không thể xóa khách hàng đã có lịch sử mua hàng");
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Tự động nâng cấp tier dựa trên tổng mua hàng
        /// VVIP: ≥ 1 tỷ (25% chiết khấu), VIP: ≥ 500 triệu (10% chiết khấu)
        /// </summary>
        public (bool Upgraded, string NewTier, decimal DiscountPercent) CheckAndUpgradeTier(int customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null)
                return (false, "", 0);

            string oldTier = customer.Tier;
            string newTier = Customer.DetermineTier(customer.TotalPurchase);

            if (newTier != oldTier)
            {
                customer.Tier = newTier;
                _customerRepository.Update(customer);
                return (true, newTier, Customer.GetDiscountByTier(newTier));
            }

            return (false, customer.Tier, Customer.GetDiscountByTier(customer.Tier));
        }

        /// <summary>
        /// Lấy % chiết khấu cho khách hàng
        /// </summary>
        public decimal GetCustomerDiscount(int customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null) return 0;
            return customer.DiscountPercent;
        }
    }
}
