using System;

namespace QuanLyTiemDaQuy.Models
{
    /// <summary>
    /// Nhân viên với phân quyền theo Role
    /// </summary>
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Sales"; // Admin, Manager, Sales
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        // Role checking helpers
        public bool IsAdmin { get { return Role == "Admin"; } }
        public bool IsManager { get { return Role == "Manager" || IsAdmin; } }
        public bool IsSales { get { return Role == "Sales" || IsManager; } }
    }

    /// <summary>
    /// Constants for employee roles
    /// </summary>
    public static class EmployeeRoles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Sales = "Sales";

        public static readonly string[] AllRoles = { Admin, Manager, Sales };
    }
}
