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
        /// </summary>
        public (bool Upgraded, string NewTier) CheckAndUpgradeTier(int customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null)
                return (false, "");

            string newTier = customer.Tier;

            // Logic nâng tier
            if (customer.TotalPurchase >= 500000000) // 500 triệu -> VVIP
                newTier = "VVIP";
            else if (customer.TotalPurchase >= 100000000) // 100 triệu -> VIP
                newTier = "VIP";
            else
                newTier = "Thường";

            if (newTier != customer.Tier)
            {
                customer.Tier = newTier;
                _customerRepository.Update(customer);
                return (true, newTier);
            }

            return (false, customer.Tier);
        }
    }

    /// <summary>
    /// Service cho quản lý nhà cung cấp
    /// </summary>
    public class SupplierService
    {
        private readonly SupplierRepository _supplierRepository;

        public SupplierService()
        {
            _supplierRepository = new SupplierRepository();
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAll();
        }

        public Supplier? GetSupplierById(int supplierId)
        {
            return _supplierRepository.GetById(supplierId);
        }

        public List<Supplier> SearchSuppliers(string keyword)
        {
            return _supplierRepository.Search(keyword);
        }

        public (bool Success, string Message, int SupplierId) AddSupplier(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Name))
                return (false, "Tên nhà cung cấp không được để trống", 0);

            try
            {
                int id = _supplierRepository.Insert(supplier);
                return (true, "Thêm nhà cung cấp thành công", id);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", 0);
            }
        }

        public (bool Success, string Message) UpdateSupplier(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Name))
                return (false, "Tên nhà cung cấp không được để trống");

            try
            {
                bool success = _supplierRepository.Update(supplier);
                if (success)
                    return (true, "Cập nhật nhà cung cấp thành công");
                else
                    return (false, "Không tìm thấy nhà cung cấp");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public (bool Success, string Message) DeleteSupplier(int supplierId)
        {
            try
            {
                bool success = _supplierRepository.Delete(supplierId);
                if (success)
                    return (true, "Xóa nhà cung cấp thành công");
                else
                    return (false, "Không tìm thấy nhà cung cấp");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE"))
                    return (false, "Không thể xóa nhà cung cấp đã có phiếu nhập");
                return (false, $"Lỗi: {ex.Message}");
            }
        }
    }
}
