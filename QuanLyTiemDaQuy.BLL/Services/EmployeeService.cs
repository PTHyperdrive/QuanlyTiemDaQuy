using System;
using System.Collections.Generic;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service cho quản lý nhân viên và xác thực
    /// </summary>
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly BranchRepository _branchRepository;
        
        // Lưu thông tin nhân viên đang đăng nhập
        public static Employee? CurrentEmployee { get; private set; }

        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
            _branchRepository = new BranchRepository();
        }

        #region Authentication

        /// <summary>
        /// Đăng nhập
        /// </summary>
        public (bool Success, string Message, Employee? Employee) Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return (false, "Vui lòng nhập tên đăng nhập", null);

            if (string.IsNullOrWhiteSpace(password))
                return (false, "Vui lòng nhập mật khẩu", null);

            var employee = _employeeRepository.Authenticate(username, password);
            
            if (employee == null)
                return (false, "Tên đăng nhập hoặc mật khẩu không đúng", null);

            if (!employee.IsActive)
                return (false, "Tài khoản đã bị vô hiệu hóa", null);

            CurrentEmployee = employee;
            return (true, $"Đăng nhập thành công. Xin chào {employee.Name}!", employee);
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        public void Logout()
        {
            CurrentEmployee = null;
        }

        /// <summary>
        /// Kiểm tra đã đăng nhập chưa
        /// </summary>
        public bool IsLoggedIn()
        {
            return CurrentEmployee != null;
        }

        /// <summary>
        /// Kiểm tra quyền
        /// </summary>
        public bool HasPermission(string requiredRole)
        {
            if (CurrentEmployee == null)
                return false;

            return requiredRole switch
            {
                EmployeeRoles.Admin => CurrentEmployee.IsAdmin,
                EmployeeRoles.Manager => CurrentEmployee.IsManager,
                EmployeeRoles.Sales => CurrentEmployee.IsSales,
                _ => false
            };
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        public (bool Success, string Message) ChangePassword(int employeeId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                return (false, "Mật khẩu mới không được để trống");

            if (newPassword.Length < 6)
                return (false, "Mật khẩu phải có ít nhất 6 ký tự");

            // Verify old password
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
                return (false, "Không tìm thấy nhân viên");

            string oldHash = EmployeeRepository.HashPassword(oldPassword);
            if (employee.PasswordHash != oldHash)
                return (false, "Mật khẩu cũ không đúng");

            try
            {
                bool success = _employeeRepository.UpdatePassword(employeeId, newPassword);
                if (success)
                    return (true, "Đổi mật khẩu thành công");
                else
                    return (false, "Không thể đổi mật khẩu");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        #endregion

        #region Employee CRUD

        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAll();
        }

        public List<Employee> GetActiveEmployees()
        {
            return _employeeRepository.GetActive();
        }

        public Employee? GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetById(employeeId);
        }

        public (bool Success, string Message, int EmployeeId) AddEmployee(Employee employee, string password)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(employee.Name))
                return (false, "Tên nhân viên không được để trống", 0);

            if (string.IsNullOrWhiteSpace(employee.Username))
                return (false, "Tên đăng nhập không được để trống", 0);

            if (_employeeRepository.IsUsernameExists(employee.Username))
                return (false, "Tên đăng nhập đã tồn tại", 0);

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return (false, "Mật khẩu phải có ít nhất 6 ký tự", 0);

            try
            {
                employee.PasswordHash = password; // Repository sẽ hash
                int id = _employeeRepository.Insert(employee);
                return (true, "Thêm nhân viên thành công", id);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", 0);
            }
        }

        public (bool Success, string Message) UpdateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Name))
                return (false, "Tên nhân viên không được để trống");

            if (string.IsNullOrWhiteSpace(employee.Username))
                return (false, "Tên đăng nhập không được để trống");

            if (_employeeRepository.IsUsernameExists(employee.Username, employee.EmployeeId))
                return (false, "Tên đăng nhập đã tồn tại");

            try
            {
                bool success = _employeeRepository.Update(employee);
                if (success)
                    return (true, "Cập nhật nhân viên thành công");
                else
                    return (false, "Không tìm thấy nhân viên");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public (bool Success, string Message) DeactivateEmployee(int employeeId)
        {
            // Không cho vô hiệu hóa chính mình
            if (CurrentEmployee?.EmployeeId == employeeId)
                return (false, "Không thể vô hiệu hóa tài khoản đang đăng nhập");

            try
            {
                bool success = _employeeRepository.Delete(employeeId); // Soft delete
                if (success)
                    return (true, "Vô hiệu hóa nhân viên thành công");
                else
                    return (false, "Không tìm thấy nhân viên");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public (bool Success, string Message) ActivateEmployee(int employeeId)
        {
            try
            {
                bool success = _employeeRepository.Activate(employeeId);
                if (success)
                    return (true, "Kích hoạt nhân viên thành công");
                else
                    return (false, "Không tìm thấy nhân viên");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Reset mật khẩu (chỉ Admin)
        /// </summary>
        public (bool Success, string Message) ResetPassword(int employeeId, string newPassword)
        {
            if (!CurrentEmployee?.IsAdmin ?? true)
                return (false, "Chỉ Admin mới có quyền reset mật khẩu");

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                return (false, "Mật khẩu phải có ít nhất 6 ký tự");

            try
            {
                bool success = _employeeRepository.UpdatePassword(employeeId, newPassword);
                if (success)
                    return (true, "Reset mật khẩu thành công");
                else
                    return (false, "Không tìm thấy nhân viên");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        #endregion

        #region Branch Operations

        /// <summary>
        /// Lấy danh sách tất cả chi nhánh
        /// </summary>
        public List<Branch> GetAllBranches()
        {
            return _branchRepository.GetAll();
        }

        /// <summary>
        /// Lấy danh sách chi nhánh đang hoạt động
        /// </summary>
        public List<Branch> GetActiveBranches()
        {
            return _branchRepository.GetActive();
        }

        #endregion

        #region Admin Employee Management

        /// <summary>
        /// Admin đặt mật khẩu tùy ý cho nhân viên
        /// </summary>
        public (bool Success, string Message) SetPassword(int employeeId, string newPassword, bool mustChangeOnLogin = false)
        {
            if (!CurrentEmployee?.IsAdmin ?? true)
                return (false, "Chỉ Admin mới có quyền đặt mật khẩu");

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                return (false, "Mật khẩu phải có ít nhất 6 ký tự");

            try
            {
                bool success = _employeeRepository.SetPassword(employeeId, newPassword, mustChangeOnLogin);
                if (success)
                {
                    string msg = mustChangeOnLogin 
                        ? "Đặt mật khẩu thành công. Nhân viên sẽ phải đổi mật khẩu khi đăng nhập."
                        : "Đặt mật khẩu thành công";
                    return (true, msg);
                }
                else
                    return (false, "Không tìm thấy nhân viên");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Cập nhật thông tin cá nhân (SĐT, Email)
        /// </summary>
        public (bool Success, string Message) UpdatePersonalInfo(int employeeId, string phone, string email)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
                return (false, "Không tìm thấy nhân viên");

            employee.Phone = phone;
            employee.Email = email;

            try
            {
                bool success = _employeeRepository.Update(employee);
                if (success)
                    return (true, "Cập nhật thông tin thành công");
                else
                    return (false, "Không thể cập nhật thông tin");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Đặt cờ yêu cầu đổi mật khẩu khi đăng nhập
        /// </summary>
        public (bool Success, string Message) SetMustChangePassword(int employeeId, bool mustChange)
        {
            try
            {
                bool success = _employeeRepository.SetMustChangePassword(employeeId, mustChange);
                if (success)
                {
                    string msg = mustChange 
                        ? "Đã đánh dấu yêu cầu đổi mật khẩu khi đăng nhập"
                        : "Đã bỏ yêu cầu đổi mật khẩu khi đăng nhập";
                    return (true, msg);
                }
                else
                    return (false, "Không tìm thấy nhân viên");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Chuyển nhân viên sang chi nhánh khác
        /// </summary>
        public (bool Success, string Message) UpdateEmployeeBranch(int employeeId, int branchId)
        {
            try
            {
                bool success = _employeeRepository.UpdateBranch(employeeId, branchId);
                if (success)
                    return (true, "Đã chuyển chi nhánh thành công");
                else
                    return (false, "Không tìm thấy nhân viên");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        #endregion
    }
}

