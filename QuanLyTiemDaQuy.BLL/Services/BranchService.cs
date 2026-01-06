using System;
using System.Collections.Generic;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service cho quản lý chi nhánh
    /// </summary>
    public class BranchService
    {
        private readonly BranchRepository _branchRepository;

        public BranchService()
        {
            _branchRepository = new BranchRepository();
        }

        #region Branch CRUD

        /// <summary>
        /// Lấy tất cả chi nhánh
        /// </summary>
        public List<Branch> GetAllBranches()
        {
            return _branchRepository.GetAll();
        }

        /// <summary>
        /// Lấy chi nhánh theo ID
        /// </summary>
        public Branch GetBranchById(int branchId)
        {
            return _branchRepository.GetById(branchId);
        }

        /// <summary>
        /// Lấy danh sách chi nhánh đang hoạt động
        /// </summary>
        public List<Branch> GetActiveBranches()
        {
            return _branchRepository.GetActive();
        }

        /// <summary>
        /// Thêm chi nhánh mới
        /// </summary>
        public (bool Success, string Message, int BranchId) AddBranch(Branch branch)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(branch.Name))
                return (false, "Tên chi nhánh không được để trống", 0);

            // Tự động sinh mã nếu chưa có
            if (string.IsNullOrWhiteSpace(branch.BranchCode))
            {
                branch.BranchCode = _branchRepository.GenerateBranchCode();
            }
            else if (_branchRepository.IsBranchCodeExists(branch.BranchCode))
            {
                return (false, "Mã chi nhánh đã tồn tại", 0);
            }

            try
            {
                int id = _branchRepository.Insert(branch);
                return (true, $"Thêm chi nhánh {branch.Name} thành công", id);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", 0);
            }
        }

        /// <summary>
        /// Cập nhật thông tin chi nhánh
        /// </summary>
        public (bool Success, string Message) UpdateBranch(Branch branch)
        {
            if (string.IsNullOrWhiteSpace(branch.Name))
                return (false, "Tên chi nhánh không được để trống");

            if (string.IsNullOrWhiteSpace(branch.BranchCode))
                return (false, "Mã chi nhánh không được để trống");

            if (_branchRepository.IsBranchCodeExists(branch.BranchCode, branch.BranchId))
                return (false, "Mã chi nhánh đã tồn tại");

            try
            {
                bool success = _branchRepository.Update(branch);
                if (success)
                    return (true, "Cập nhật chi nhánh thành công");
                else
                    return (false, "Không tìm thấy chi nhánh");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Vô hiệu hóa chi nhánh
        /// </summary>
        public (bool Success, string Message) DeactivateBranch(int branchId)
        {
            // Kiểm tra còn nhân viên không
            var employees = _branchRepository.GetEmployeesByBranch(branchId);
            int activeCount = 0;
            foreach (var emp in employees)
            {
                if (emp.IsActive) activeCount++;
            }

            if (activeCount > 0)
            {
                return (false, $"Không thể vô hiệu hóa chi nhánh đang có {activeCount} nhân viên hoạt động");
            }

            try
            {
                bool success = _branchRepository.Deactivate(branchId);
                if (success)
                    return (true, "Đã vô hiệu hóa chi nhánh");
                else
                    return (false, "Không tìm thấy chi nhánh");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Kích hoạt lại chi nhánh
        /// </summary>
        public (bool Success, string Message) ActivateBranch(int branchId)
        {
            try
            {
                bool success = _branchRepository.Activate(branchId);
                if (success)
                    return (true, "Đã kích hoạt chi nhánh");
                else
                    return (false, "Không tìm thấy chi nhánh");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        #endregion

        #region Employee Management

        /// <summary>
        /// Lấy danh sách nhân viên của chi nhánh
        /// </summary>
        public List<Employee> GetEmployeesByBranch(int branchId)
        {
            return _branchRepository.GetEmployeesByBranch(branchId);
        }

        /// <summary>
        /// Xóa chi nhánh (soft delete - vô hiệu hóa)
        /// Không cho phép xóa chi nhánh chính (CN01)
        /// </summary>
        public (bool Success, string Message) DeleteBranch(int branchId)
        {
            var branch = _branchRepository.GetById(branchId);
            if (branch == null)
                return (false, "Không tìm thấy chi nhánh");

            // Không cho phép xóa chi nhánh chính
            if (BranchRepository.IsMainBranch(branch.BranchCode))
                return (false, "Không thể xóa chi nhánh chính (CN01)");

            // Kiểm tra còn nhân viên không
            var employees = _branchRepository.GetEmployeesByBranch(branchId);
            int activeCount = 0;
            foreach (var emp in employees)
            {
                if (emp.IsActive) activeCount++;
            }

            if (activeCount > 0)
            {
                return (false, $"Không thể xóa chi nhánh đang có {activeCount} nhân viên hoạt động. Vui lòng chuyển nhân viên sang chi nhánh khác trước.");
            }

            try
            {
                bool success = _branchRepository.Deactivate(branchId);
                if (success)
                    return (true, $"Đã xóa chi nhánh {branch.Name}");
                else
                    return (false, "Không thể xóa chi nhánh");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        #endregion
    }
}
