using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using QuanLyTiemDaQuy.Core.Interfaces;
using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Core.BLL.Services;

/// <summary>
/// Service cho quản lý nhân viên và xác thực
/// </summary>
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public Employee? CurrentEmployee { get; set; }

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public List<Employee> GetAllEmployees() => _employeeRepository.GetAll();

    public Employee? GetEmployeeById(int employeeId) => _employeeRepository.GetById(employeeId);

    public (bool Success, string Message, Employee? Employee) Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return (false, "Vui lòng nhập tài khoản và mật khẩu", null);

        var employee = _employeeRepository.GetByUsername(username);
        if (employee == null)
            return (false, "Tài khoản không tồn tại", null);

        if (!employee.IsActive)
            return (false, "Tài khoản đã bị vô hiệu hóa", null);

        string passwordHash = HashPassword(password);
        if (employee.PasswordHash != passwordHash)
            return (false, "Mật khẩu không đúng", null);

        CurrentEmployee = employee;
        return (true, "Đăng nhập thành công", employee);
    }

    public (bool Success, string Message) ChangePassword(int employeeId, string currentPassword, string newPassword)
    {
        var employee = _employeeRepository.GetById(employeeId);
        if (employee == null)
            return (false, "Không tìm thấy nhân viên");

        string currentHash = HashPassword(currentPassword);
        if (employee.PasswordHash != currentHash)
            return (false, "Mật khẩu hiện tại không đúng");

        var validation = ValidatePassword(newPassword);
        if (!validation.IsValid)
            return (false, validation.Message);

        string newHash = HashPassword(newPassword);
        bool success = _employeeRepository.UpdatePassword(employeeId, newHash);
        return success ? (true, "Đổi mật khẩu thành công") : (false, "Lỗi khi đổi mật khẩu");
    }

    public (bool Success, string Message) SetPassword(int employeeId, string newPassword)
    {
        if (CurrentEmployee == null || !CurrentEmployee.IsAdmin)
            return (false, "Chỉ Admin mới có quyền đặt mật khẩu");

        var validation = ValidatePassword(newPassword);
        if (!validation.IsValid)
            return (false, validation.Message);

        string newHash = HashPassword(newPassword);
        bool success = _employeeRepository.UpdatePassword(employeeId, newHash);
        
        if (success)
            _employeeRepository.SetMustChangePassword(employeeId, true);
        
        return success ? (true, "Đặt mật khẩu thành công") : (false, "Lỗi khi đặt mật khẩu");
    }

    public (bool Success, string Message, int EmployeeId) AddEmployee(Employee employee)
    {
        if (CurrentEmployee == null || !CurrentEmployee.IsManager)
            return (false, "Bạn không có quyền thêm nhân viên", 0);

        if (string.IsNullOrWhiteSpace(employee.Name))
            return (false, "Tên nhân viên không được để trống", 0);

        if (string.IsNullOrWhiteSpace(employee.Username))
            return (false, "Tên đăng nhập không được để trống", 0);

        var existing = _employeeRepository.GetByUsername(employee.Username);
        if (existing != null)
            return (false, "Tên đăng nhập đã tồn tại", 0);

        // Set default password
        employee.PasswordHash = HashPassword("123456");
        employee.MustChangePassword = true;

        int employeeId = _employeeRepository.Add(employee);
        return employeeId > 0 
            ? (true, "Thêm nhân viên thành công. Mật khẩu mặc định: 123456", employeeId) 
            : (false, "Lỗi khi thêm nhân viên", 0);
    }

    public (bool Success, string Message) UpdateEmployee(Employee employee)
    {
        if (CurrentEmployee == null || !CurrentEmployee.IsManager)
            return (false, "Bạn không có quyền cập nhật nhân viên");

        if (string.IsNullOrWhiteSpace(employee.Name))
            return (false, "Tên nhân viên không được để trống");

        var existing = _employeeRepository.GetByUsername(employee.Username);
        if (existing != null && existing.EmployeeId != employee.EmployeeId)
            return (false, "Tên đăng nhập đã tồn tại");

        bool success = _employeeRepository.Update(employee);
        return success ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
    }

    public (bool Success, string Message) DeleteEmployee(int employeeId)
    {
        if (CurrentEmployee == null || !CurrentEmployee.IsAdmin)
            return (false, "Chỉ Admin mới có quyền xóa nhân viên");

        if (employeeId == CurrentEmployee.EmployeeId)
            return (false, "Không thể xóa tài khoản đang đăng nhập");

        bool success = _employeeRepository.Delete(employeeId);
        return success ? (true, "Xóa thành công") : (false, "Lỗi khi xóa nhân viên");
    }

    public (bool Success, string Message) ToggleActiveStatus(int employeeId)
    {
        if (CurrentEmployee == null || !CurrentEmployee.IsManager)
            return (false, "Bạn không có quyền thay đổi trạng thái nhân viên");

        var employee = _employeeRepository.GetById(employeeId);
        if (employee == null)
            return (false, "Không tìm thấy nhân viên");

        employee.IsActive = !employee.IsActive;
        bool success = _employeeRepository.Update(employee);
        
        string status = employee.IsActive ? "kích hoạt" : "vô hiệu hóa";
        return success ? (true, $"Đã {status} tài khoản") : (false, "Lỗi khi cập nhật");
    }

    private static string HashPassword(string password)
    {
        // Use hex string format to match WinForms app
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }

    private static (bool IsValid, string Message) ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return (false, "Mật khẩu không được để trống");

        if (password.Length < 6)
            return (false, "Mật khẩu phải có ít nhất 6 ký tự");

        return (true, "");
    }
}

/// <summary>
/// Service cho quản lý khách hàng
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public List<Customer> GetAllCustomers() => _customerRepository.GetAll();

    public Customer? GetCustomerById(int customerId) => _customerRepository.GetById(customerId);

    public List<Customer> SearchCustomers(string keyword) => _customerRepository.Search(keyword);

    public (bool Success, string Message, int CustomerId) AddCustomer(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.Name))
            return (false, "Tên khách hàng không được để trống", 0);

        if (!string.IsNullOrWhiteSpace(customer.Email) && !IsValidEmail(customer.Email))
            return (false, "Email không hợp lệ", 0);

        int customerId = _customerRepository.Add(customer);
        return customerId > 0 
            ? (true, "Thêm khách hàng thành công", customerId) 
            : (false, "Lỗi khi thêm khách hàng", 0);
    }

    public (bool Success, string Message) UpdateCustomer(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.Name))
            return (false, "Tên khách hàng không được để trống");

        if (!string.IsNullOrWhiteSpace(customer.Email) && !IsValidEmail(customer.Email))
            return (false, "Email không hợp lệ");

        bool success = _customerRepository.Update(customer);
        return success ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
    }

    public (bool Success, string Message) DeleteCustomer(int customerId)
    {
        bool success = _customerRepository.Delete(customerId);
        return success ? (true, "Xóa thành công") : (false, "Lỗi khi xóa khách hàng");
    }

    public (bool TierChanged, string OldTier, string NewTier) UpdateTotalPurchaseAndCheckTier(int customerId, decimal purchaseAmount)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer == null)
            return (false, "", "");

        string oldTier = customer.Tier;
        _customerRepository.UpdateTotalPurchase(customerId, purchaseAmount);
        
        // Recalculate tier
        decimal newTotal = customer.TotalPurchase + purchaseAmount;
        string newTier = Customer.DetermineTier(newTotal);
        
        if (newTier != oldTier)
        {
            customer.Tier = newTier;
            customer.TotalPurchase = newTotal;
            _customerRepository.Update(customer);
            return (true, oldTier, newTier);
        }

        return (false, oldTier, newTier);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
        catch
        {
            return false;
        }
    }
}

/// <summary>
/// Service cho quản lý chi nhánh
/// </summary>
public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;

    public BranchService(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public List<Branch> GetAllBranches() => _branchRepository.GetAll();

    public Branch? GetBranchById(int branchId) => _branchRepository.GetById(branchId);

    public (bool Success, string Message, int BranchId) AddBranch(Branch branch)
    {
        if (string.IsNullOrWhiteSpace(branch.Name))
            return (false, "Tên chi nhánh không được để trống", 0);

        if (string.IsNullOrWhiteSpace(branch.BranchCode))
            return (false, "Mã chi nhánh không được để trống", 0);

        int branchId = _branchRepository.Add(branch);
        return branchId > 0 
            ? (true, "Thêm chi nhánh thành công", branchId) 
            : (false, "Lỗi khi thêm chi nhánh", 0);
    }

    public (bool Success, string Message) UpdateBranch(Branch branch)
    {
        if (string.IsNullOrWhiteSpace(branch.Name))
            return (false, "Tên chi nhánh không được để trống");

        bool success = _branchRepository.Update(branch);
        return success ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
    }

    public (bool Success, string Message) DeleteBranch(int branchId)
    {
        bool success = _branchRepository.Delete(branchId);
        return success ? (true, "Xóa thành công") : (false, "Lỗi khi xóa chi nhánh");
    }
}

/// <summary>
/// Service cho quản lý nhà cung cấp
/// </summary>
public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public List<Supplier> GetAllSuppliers() => _supplierRepository.GetAll();

    public Supplier? GetSupplierById(int supplierId) => _supplierRepository.GetById(supplierId);

    public List<Supplier> SearchSuppliers(string keyword) => _supplierRepository.Search(keyword);

    public (bool Success, string Message, int SupplierId) AddSupplier(Supplier supplier)
    {
        if (string.IsNullOrWhiteSpace(supplier.Name))
            return (false, "Tên nhà cung cấp không được để trống", 0);

        int supplierId = _supplierRepository.Add(supplier);
        return supplierId > 0 
            ? (true, "Thêm nhà cung cấp thành công", supplierId) 
            : (false, "Lỗi khi thêm nhà cung cấp", 0);
    }

    public (bool Success, string Message) UpdateSupplier(Supplier supplier)
    {
        if (string.IsNullOrWhiteSpace(supplier.Name))
            return (false, "Tên nhà cung cấp không được để trống");

        bool success = _supplierRepository.Update(supplier);
        return success ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
    }

    public (bool Success, string Message) DeleteSupplier(int supplierId)
    {
        bool success = _supplierRepository.Delete(supplierId);
        return success ? (true, "Xóa thành công") : (false, "Lỗi khi xóa nhà cung cấp");
    }
}
