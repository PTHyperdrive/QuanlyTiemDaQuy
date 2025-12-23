using System;
using System.Collections.Generic;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service cho quản lý nhà cung cấp và chứng chỉ
    /// </summary>
    public class SupplierService
    {
        private readonly SupplierRepository _supplierRepo;
        private readonly CertificateRepository _certRepo;

        public SupplierService()
        {
            _supplierRepo = new SupplierRepository();
            _certRepo = new CertificateRepository();
        }

        #region Supplier Operations

        public List<Supplier> GetAllSuppliers()
        {
            return _supplierRepo.GetAll();
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return _supplierRepo.GetById(supplierId);
        }

        public List<Supplier> SearchSuppliers(string keyword)
        {
            return _supplierRepo.Search(keyword);
        }

        public (bool Success, string Message, int Id) AddSupplier(Supplier supplier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(supplier.Name))
                    return (false, "Tên nhà cung cấp không được để trống", 0);

                int newId = _supplierRepo.Insert(supplier);
                if (newId > 0)
                    return (true, "Thêm nhà cung cấp thành công", newId);
                else
                    return (false, "Không thể thêm nhà cung cấp", 0);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", 0);
            }
        }

        public (bool Success, string Message) UpdateSupplier(Supplier supplier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(supplier.Name))
                    return (false, "Tên nhà cung cấp không được để trống");

                bool success = _supplierRepo.Update(supplier);
                if (success)
                    return (true, "Cập nhật thành công");
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
                bool success = _supplierRepo.Delete(supplierId);
                if (success)
                    return (true, "Xóa thành công");
                else
                    return (false, "Không tìm thấy nhà cung cấp");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        #endregion

        #region Certificate Operations

        public List<Certificate> GetAllCertificates()
        {
            return _certRepo.GetAll();
        }

        public Certificate GetCertificateById(int certId)
        {
            return _certRepo.GetById(certId);
        }

        public Certificate GetCertificateByCode(string certCode)
        {
            return _certRepo.GetByCode(certCode);
        }

        /// <summary>
        /// Kiểm tra chứng chỉ có hợp lệ (đã được đăng ký) hay chưa
        /// </summary>
        public (bool IsValid, string Message, Certificate Cert) ValidateCertificate(string certCode)
        {
            if (string.IsNullOrWhiteSpace(certCode))
                return (false, "Mã chứng chỉ không được để trống", null);

            var cert = _certRepo.GetByCode(certCode.Trim());
            if (cert != null)
                return (true, $"Chứng chỉ hợp lệ - {cert.Issuer} ({cert.IssueDate:dd/MM/yyyy})", cert);
            else
                return (false, "Chứng chỉ chưa được đăng ký trong hệ thống", null);
        }

        public (bool Success, string Message, int Id) AddCertificate(Certificate cert)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cert.CertCode))
                    return (false, "Mã chứng chỉ không được để trống", 0);

                if (string.IsNullOrWhiteSpace(cert.Issuer))
                    return (false, "Đơn vị cấp không được để trống", 0);

                if (_certRepo.IsCodeExists(cert.CertCode.Trim()))
                    return (false, "Mã chứng chỉ đã tồn tại trong hệ thống", 0);

                int newId = _certRepo.Insert(cert);
                if (newId > 0)
                    return (true, "Đăng ký chứng chỉ thành công", newId);
                else
                    return (false, "Không thể đăng ký chứng chỉ", 0);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", 0);
            }
        }

        public (bool Success, string Message) DeleteCertificate(int certId)
        {
            try
            {
                bool success = _certRepo.Delete(certId);
                if (success)
                    return (true, "Xóa chứng chỉ thành công");
                else
                    return (false, "Không tìm thấy chứng chỉ");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        #endregion
    }
}
