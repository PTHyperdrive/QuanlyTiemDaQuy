using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyTiemDaQuy.Core.Interfaces;
using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Core.DAL.Repositories;

/// <summary>
/// Repository cho quản lý khách hàng
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    public List<Customer> GetAll()
    {
        string query = "SELECT * FROM Customers ORDER BY Name";
        var dt = DatabaseHelper.ExecuteQuery(query);
        return MapDataTableToList(dt);
    }

    public Customer? GetById(int customerId)
    {
        string query = "SELECT * FROM Customers WHERE CustomerId = @CustomerId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@CustomerId", customerId));
        var list = MapDataTableToList(dt);
        return list.Count > 0 ? list[0] : null;
    }

    public List<Customer> Search(string keyword)
    {
        string query = @"SELECT * FROM Customers 
            WHERE Name LIKE @Keyword OR Phone LIKE @Keyword OR Email LIKE @Keyword
            ORDER BY Name";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@Keyword", $"%{keyword}%"));
        return MapDataTableToList(dt);
    }

    public int Add(Customer customer)
    {
        string query = @"
            INSERT INTO Customers (Name, Phone, Email, Address, Tier, TotalPurchase)
            VALUES (@Name, @Phone, @Email, @Address, @Tier, @TotalPurchase);
            SELECT SCOPE_IDENTITY();";

        var result = DatabaseHelper.ExecuteScalar(query,
            DatabaseHelper.CreateParameter("@Name", customer.Name),
            DatabaseHelper.CreateParameter("@Phone", customer.Phone),
            DatabaseHelper.CreateParameter("@Email", customer.Email),
            DatabaseHelper.CreateParameter("@Address", customer.Address),
            DatabaseHelper.CreateParameter("@Tier", customer.Tier),
            DatabaseHelper.CreateParameter("@TotalPurchase", customer.TotalPurchase));

        return Convert.ToInt32(result);
    }

    public bool Update(Customer customer)
    {
        string query = @"
            UPDATE Customers SET 
                Name = @Name, Phone = @Phone, Email = @Email, 
                Address = @Address, Tier = @Tier, TotalPurchase = @TotalPurchase
            WHERE CustomerId = @CustomerId";

        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@CustomerId", customer.CustomerId),
            DatabaseHelper.CreateParameter("@Name", customer.Name),
            DatabaseHelper.CreateParameter("@Phone", customer.Phone),
            DatabaseHelper.CreateParameter("@Email", customer.Email),
            DatabaseHelper.CreateParameter("@Address", customer.Address),
            DatabaseHelper.CreateParameter("@Tier", customer.Tier),
            DatabaseHelper.CreateParameter("@TotalPurchase", customer.TotalPurchase));

        return affected > 0;
    }

    public bool Delete(int customerId)
    {
        string query = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@CustomerId", customerId));
        return affected > 0;
    }

    public bool UpdateTotalPurchase(int customerId, decimal amount)
    {
        string query = @"
            UPDATE Customers SET TotalPurchase = TotalPurchase + @Amount
            WHERE CustomerId = @CustomerId";
        
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@CustomerId", customerId),
            DatabaseHelper.CreateParameter("@Amount", amount));
        return affected > 0;
    }

    private static List<Customer> MapDataTableToList(DataTable dt)
    {
        var list = new List<Customer>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Customer
            {
                CustomerId = Convert.ToInt32(row["CustomerId"]),
                Name = row["Name"].ToString() ?? "",
                Phone = DatabaseHelper.GetString(row, "Phone"),
                Email = DatabaseHelper.GetString(row, "Email"),
                Address = DatabaseHelper.GetString(row, "Address"),
                Tier = row["Tier"].ToString() ?? "Thường",
                TotalPurchase = Convert.ToDecimal(row["TotalPurchase"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            });
        }
        return list;
    }
}

/// <summary>
/// Repository cho quản lý nhân viên
/// </summary>
public class EmployeeRepository : IEmployeeRepository
{
    public List<Employee> GetAll()
    {
        string query = @"SELECT e.*, b.Name AS BranchName 
            FROM Employees e 
            LEFT JOIN Branches b ON e.BranchId = b.BranchId 
            ORDER BY e.Name";
        var dt = DatabaseHelper.ExecuteQuery(query);
        return MapDataTableToList(dt);
    }

    public Employee? GetById(int employeeId)
    {
        string query = @"SELECT e.*, b.Name AS BranchName 
            FROM Employees e 
            LEFT JOIN Branches b ON e.BranchId = b.BranchId 
            WHERE e.EmployeeId = @EmployeeId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@EmployeeId", employeeId));
        var list = MapDataTableToList(dt);
        return list.Count > 0 ? list[0] : null;
    }

    public Employee? GetByUsername(string username)
    {
        string query = @"SELECT e.*, b.Name AS BranchName 
            FROM Employees e 
            LEFT JOIN Branches b ON e.BranchId = b.BranchId 
            WHERE e.Username = @Username";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@Username", username));
        var list = MapDataTableToList(dt);
        return list.Count > 0 ? list[0] : null;
    }

    public int Add(Employee employee)
    {
        string query = @"
            INSERT INTO Employees (Name, Username, PasswordHash, Role, Phone, Email, IsActive, MustChangePassword, BranchId)
            VALUES (@Name, @Username, @PasswordHash, @Role, @Phone, @Email, @IsActive, @MustChangePassword, @BranchId);
            SELECT SCOPE_IDENTITY();";

        var result = DatabaseHelper.ExecuteScalar(query,
            DatabaseHelper.CreateParameter("@Name", employee.Name),
            DatabaseHelper.CreateParameter("@Username", employee.Username),
            DatabaseHelper.CreateParameter("@PasswordHash", employee.PasswordHash),
            DatabaseHelper.CreateParameter("@Role", employee.Role),
            DatabaseHelper.CreateParameter("@Phone", employee.Phone),
            DatabaseHelper.CreateParameter("@Email", employee.Email),
            DatabaseHelper.CreateParameter("@IsActive", employee.IsActive),
            DatabaseHelper.CreateParameter("@MustChangePassword", employee.MustChangePassword),
            DatabaseHelper.CreateParameter("@BranchId", employee.BranchId));

        return Convert.ToInt32(result);
    }

    public bool Update(Employee employee)
    {
        string query = @"
            UPDATE Employees SET 
                Name = @Name, Username = @Username, Role = @Role, 
                Phone = @Phone, Email = @Email, IsActive = @IsActive, BranchId = @BranchId
            WHERE EmployeeId = @EmployeeId";

        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@EmployeeId", employee.EmployeeId),
            DatabaseHelper.CreateParameter("@Name", employee.Name),
            DatabaseHelper.CreateParameter("@Username", employee.Username),
            DatabaseHelper.CreateParameter("@Role", employee.Role),
            DatabaseHelper.CreateParameter("@Phone", employee.Phone),
            DatabaseHelper.CreateParameter("@Email", employee.Email),
            DatabaseHelper.CreateParameter("@IsActive", employee.IsActive),
            DatabaseHelper.CreateParameter("@BranchId", employee.BranchId));

        return affected > 0;
    }

    public bool Delete(int employeeId)
    {
        string query = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@EmployeeId", employeeId));
        return affected > 0;
    }

    public bool UpdatePassword(int employeeId, string passwordHash)
    {
        string query = @"UPDATE Employees SET PasswordHash = @PasswordHash, MustChangePassword = 0 
            WHERE EmployeeId = @EmployeeId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@EmployeeId", employeeId),
            DatabaseHelper.CreateParameter("@PasswordHash", passwordHash));
        return affected > 0;
    }

    public bool SetMustChangePassword(int employeeId, bool value)
    {
        string query = "UPDATE Employees SET MustChangePassword = @Value WHERE EmployeeId = @EmployeeId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@EmployeeId", employeeId),
            DatabaseHelper.CreateParameter("@Value", value));
        return affected > 0;
    }

    private static List<Employee> MapDataTableToList(DataTable dt)
    {
        var list = new List<Employee>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Employee
            {
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                Name = row["Name"].ToString() ?? "",
                Username = row["Username"].ToString() ?? "",
                PasswordHash = row["PasswordHash"].ToString() ?? "",
                Role = row["Role"].ToString() ?? "Sales",
                Phone = DatabaseHelper.GetString(row, "Phone"),
                Email = DatabaseHelper.GetString(row, "Email"),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                MustChangePassword = Convert.ToBoolean(row["MustChangePassword"]),
                BranchId = Convert.ToInt32(row["BranchId"]),
                BranchName = DatabaseHelper.GetString(row, "BranchName"),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            });
        }
        return list;
    }
}

/// <summary>
/// Repository cho quản lý chi nhánh
/// </summary>
public class BranchRepository : IBranchRepository
{
    public List<Branch> GetAll()
    {
        string query = "SELECT * FROM Branches ORDER BY Name";
        var dt = DatabaseHelper.ExecuteQuery(query);
        return MapDataTableToList(dt);
    }

    public Branch? GetById(int branchId)
    {
        string query = "SELECT * FROM Branches WHERE BranchId = @BranchId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@BranchId", branchId));
        var list = MapDataTableToList(dt);
        return list.Count > 0 ? list[0] : null;
    }

    public int Add(Branch branch)
    {
        string query = @"
            INSERT INTO Branches (BranchCode, Name, Address, Phone, IsActive)
            VALUES (@BranchCode, @Name, @Address, @Phone, @IsActive);
            SELECT SCOPE_IDENTITY();";

        var result = DatabaseHelper.ExecuteScalar(query,
            DatabaseHelper.CreateParameter("@BranchCode", branch.BranchCode),
            DatabaseHelper.CreateParameter("@Name", branch.Name),
            DatabaseHelper.CreateParameter("@Address", branch.Address),
            DatabaseHelper.CreateParameter("@Phone", branch.Phone),
            DatabaseHelper.CreateParameter("@IsActive", branch.IsActive));

        return Convert.ToInt32(result);
    }

    public bool Update(Branch branch)
    {
        string query = @"
            UPDATE Branches SET 
                BranchCode = @BranchCode, Name = @Name, Address = @Address, 
                Phone = @Phone, IsActive = @IsActive
            WHERE BranchId = @BranchId";

        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@BranchId", branch.BranchId),
            DatabaseHelper.CreateParameter("@BranchCode", branch.BranchCode),
            DatabaseHelper.CreateParameter("@Name", branch.Name),
            DatabaseHelper.CreateParameter("@Address", branch.Address),
            DatabaseHelper.CreateParameter("@Phone", branch.Phone),
            DatabaseHelper.CreateParameter("@IsActive", branch.IsActive));

        return affected > 0;
    }

    public bool Delete(int branchId)
    {
        string query = "DELETE FROM Branches WHERE BranchId = @BranchId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@BranchId", branchId));
        return affected > 0;
    }

    private static List<Branch> MapDataTableToList(DataTable dt)
    {
        var list = new List<Branch>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Branch
            {
                BranchId = Convert.ToInt32(row["BranchId"]),
                BranchCode = row["BranchCode"].ToString() ?? "",
                Name = row["Name"].ToString() ?? "",
                Address = row["Address"].ToString() ?? "",
                Phone = row["Phone"].ToString() ?? "",
                IsActive = Convert.ToBoolean(row["IsActive"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            });
        }
        return list;
    }
}

/// <summary>
/// Repository cho quản lý nhà cung cấp
/// </summary>
public class SupplierRepository : ISupplierRepository
{
    public List<Supplier> GetAll()
    {
        string query = "SELECT * FROM Suppliers WHERE IsActive = 1 ORDER BY Name";
        var dt = DatabaseHelper.ExecuteQuery(query);
        return MapDataTableToList(dt);
    }

    public Supplier? GetById(int supplierId)
    {
        string query = "SELECT * FROM Suppliers WHERE SupplierId = @SupplierId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@SupplierId", supplierId));
        var list = MapDataTableToList(dt);
        return list.Count > 0 ? list[0] : null;
    }

    public List<Supplier> Search(string keyword)
    {
        string query = @"SELECT * FROM Suppliers 
            WHERE IsActive = 1 AND (Name LIKE @Keyword OR ContactPerson LIKE @Keyword OR Phone LIKE @Keyword)
            ORDER BY Name";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@Keyword", $"%{keyword}%"));
        return MapDataTableToList(dt);
    }

    public int Add(Supplier supplier)
    {
        string query = @"
            INSERT INTO Suppliers (Name, ContactPerson, Phone, Email, Address, TaxCode, BankAccount, Note, IsActive)
            VALUES (@Name, @ContactPerson, @Phone, @Email, @Address, @TaxCode, @BankAccount, @Note, @IsActive);
            SELECT SCOPE_IDENTITY();";

        var result = DatabaseHelper.ExecuteScalar(query,
            DatabaseHelper.CreateParameter("@Name", supplier.Name),
            DatabaseHelper.CreateParameter("@ContactPerson", supplier.ContactPerson),
            DatabaseHelper.CreateParameter("@Phone", supplier.Phone),
            DatabaseHelper.CreateParameter("@Email", supplier.Email),
            DatabaseHelper.CreateParameter("@Address", supplier.Address),
            DatabaseHelper.CreateParameter("@TaxCode", supplier.TaxCode),
            DatabaseHelper.CreateParameter("@BankAccount", supplier.BankAccount),
            DatabaseHelper.CreateParameter("@Note", supplier.Note),
            DatabaseHelper.CreateParameter("@IsActive", supplier.IsActive));

        return Convert.ToInt32(result);
    }

    public bool Update(Supplier supplier)
    {
        string query = @"
            UPDATE Suppliers SET 
                Name = @Name, ContactPerson = @ContactPerson, Phone = @Phone, 
                Email = @Email, Address = @Address, TaxCode = @TaxCode, 
                BankAccount = @BankAccount, Note = @Note, IsActive = @IsActive
            WHERE SupplierId = @SupplierId";

        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@SupplierId", supplier.SupplierId),
            DatabaseHelper.CreateParameter("@Name", supplier.Name),
            DatabaseHelper.CreateParameter("@ContactPerson", supplier.ContactPerson),
            DatabaseHelper.CreateParameter("@Phone", supplier.Phone),
            DatabaseHelper.CreateParameter("@Email", supplier.Email),
            DatabaseHelper.CreateParameter("@Address", supplier.Address),
            DatabaseHelper.CreateParameter("@TaxCode", supplier.TaxCode),
            DatabaseHelper.CreateParameter("@BankAccount", supplier.BankAccount),
            DatabaseHelper.CreateParameter("@Note", supplier.Note),
            DatabaseHelper.CreateParameter("@IsActive", supplier.IsActive));

        return affected > 0;
    }

    public bool Delete(int supplierId)
    {
        // Soft delete
        string query = "UPDATE Suppliers SET IsActive = 0 WHERE SupplierId = @SupplierId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@SupplierId", supplierId));
        return affected > 0;
    }

    private static List<Supplier> MapDataTableToList(DataTable dt)
    {
        var list = new List<Supplier>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Supplier
            {
                SupplierId = Convert.ToInt32(row["SupplierId"]),
                Name = row["Name"].ToString() ?? "",
                ContactPerson = row["ContactPerson"].ToString() ?? "",
                Phone = row["Phone"].ToString() ?? "",
                Email = row["Email"].ToString() ?? "",
                Address = row["Address"].ToString() ?? "",
                TaxCode = row["TaxCode"].ToString() ?? "",
                BankAccount = row["BankAccount"].ToString() ?? "",
                Note = row["Note"].ToString() ?? "",
                IsActive = Convert.ToBoolean(row["IsActive"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            });
        }
        return list;
    }
}

/// <summary>
/// Repository cho StoneType
/// </summary>
public class StoneTypeRepository : IStoneTypeRepository
{
    public List<StoneType> GetAll()
    {
        string query = "SELECT * FROM StoneTypes ORDER BY Name";
        var dt = DatabaseHelper.ExecuteQuery(query);
        return MapDataTableToList(dt);
    }

    public StoneType? GetById(int stoneTypeId)
    {
        string query = "SELECT * FROM StoneTypes WHERE StoneTypeId = @StoneTypeId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId));
        var list = MapDataTableToList(dt);
        return list.Count > 0 ? list[0] : null;
    }

    public int Add(StoneType stoneType)
    {
        string query = @"
            INSERT INTO StoneTypes (Name, Description)
            VALUES (@Name, @Description);
            SELECT SCOPE_IDENTITY();";

        var result = DatabaseHelper.ExecuteScalar(query,
            DatabaseHelper.CreateParameter("@Name", stoneType.Name),
            DatabaseHelper.CreateParameter("@Description", stoneType.Description));

        return Convert.ToInt32(result);
    }

    public bool Update(StoneType stoneType)
    {
        string query = @"UPDATE StoneTypes SET Name = @Name, Description = @Description 
            WHERE StoneTypeId = @StoneTypeId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@StoneTypeId", stoneType.StoneTypeId),
            DatabaseHelper.CreateParameter("@Name", stoneType.Name),
            DatabaseHelper.CreateParameter("@Description", stoneType.Description));
        return affected > 0;
    }

    private static List<StoneType> MapDataTableToList(DataTable dt)
    {
        var list = new List<StoneType>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new StoneType
            {
                StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                Name = row["Name"].ToString() ?? "",
                Description = DatabaseHelper.GetString(row, "Description")
            });
        }
        return list;
    }
}

/// <summary>
/// Repository cho Certificate
/// </summary>
public class CertificateRepository : ICertificateRepository
{
    public List<Certificate> GetAll()
    {
        string query = "SELECT * FROM Certificates ORDER BY CertCode";
        var dt = DatabaseHelper.ExecuteQuery(query);
        return MapDataTableToList(dt);
    }

    public Certificate? GetById(int certId)
    {
        string query = "SELECT * FROM Certificates WHERE CertId = @CertId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@CertId", certId));
        var list = MapDataTableToList(dt);
        return list.Count > 0 ? list[0] : null;
    }

    public int Add(Certificate certificate)
    {
        string query = @"
            INSERT INTO Certificates (CertCode, Issuer, IssueDate)
            VALUES (@CertCode, @Issuer, @IssueDate);
            SELECT SCOPE_IDENTITY();";

        var result = DatabaseHelper.ExecuteScalar(query,
            DatabaseHelper.CreateParameter("@CertCode", certificate.CertCode),
            DatabaseHelper.CreateParameter("@Issuer", certificate.Issuer),
            DatabaseHelper.CreateParameter("@IssueDate", certificate.IssueDate));

        return Convert.ToInt32(result);
    }

    private static List<Certificate> MapDataTableToList(DataTable dt)
    {
        var list = new List<Certificate>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Certificate
            {
                CertId = Convert.ToInt32(row["CertId"]),
                CertCode = row["CertCode"].ToString() ?? "",
                Issuer = row["Issuer"].ToString() ?? "",
                IssueDate = Convert.ToDateTime(row["IssueDate"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            });
        }
        return list;
    }
}
