using System;
using System.Text.RegularExpressions;

namespace QuanLyTiemDaQuy.BLL
{
    /// <summary>
    /// Helper class để validate dữ liệu đầu vào và chống SQL Injection
    /// </summary>
    public static class InputValidator
    {
        // Các ký tự nguy hiểm có thể dùng cho SQL Injection
        private static readonly string[] SqlInjectionPatterns = 
        {
            "--", ";--", "/*", "*/", "@@", "@",
            "char", "nchar", "varchar", "nvarchar",
            "alter", "begin", "cast", "create", "cursor",
            "declare", "delete", "drop", "end", "exec",
            "execute", "fetch", "insert", "kill", "open",
            "select", "sys", "sysobjects", "syscolumns",
            "table", "update", "xp_"
        };

        /// <summary>
        /// Kiểm tra chuỗi có chứa ký tự nguy hiểm không
        /// </summary>
        public static bool ContainsSqlInjection(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            string lowerInput = input.ToLower();

            foreach (var pattern in SqlInjectionPatterns)
            {
                if (lowerInput.Contains(pattern))
                    return true;
            }

            // Kiểm tra các pattern đặc biệt
            if (Regex.IsMatch(input, @"['\x22\x27]|\b(or|and)\b\s*\d+\s*=\s*\d+", RegexOptions.IgnoreCase))
                return true;

            return false;
        }

        /// <summary>
        /// Làm sạch chuỗi đầu vào
        /// </summary>
        public static string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Loại bỏ các ký tự nguy hiểm
            input = input.Replace("'", "''");  // Escape single quotes
            input = input.Replace("--", "");   // Remove SQL comments
            input = input.Replace(";", "");    // Remove semicolons
            input = input.Trim();

            return input;
        }

        /// <summary>
        /// Validate Username - chỉ cho phép chữ, số, underscore
        /// </summary>
        public static (bool IsValid, string Message) ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return (false, "Tên đăng nhập không được để trống");

            if (username.Length < 3)
                return (false, "Tên đăng nhập phải có ít nhất 3 ký tự");

            if (username.Length > 50)
                return (false, "Tên đăng nhập không được quá 50 ký tự");

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
                return (false, "Tên đăng nhập chỉ được chứa chữ cái, số và dấu gạch dưới");

            if (ContainsSqlInjection(username))
                return (false, "Tên đăng nhập chứa ký tự không hợp lệ");

            return (true, string.Empty);
        }

        /// <summary>
        /// Validate Password
        /// </summary>
        public static (bool IsValid, string Message) ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, "Mật khẩu không được để trống");

            if (password.Length < 6)
                return (false, "Mật khẩu phải có ít nhất 6 ký tự");

            if (password.Length > 100)
                return (false, "Mật khẩu không được quá 100 ký tự");

            return (true, string.Empty);
        }

        /// <summary>
        /// Validate Name - chữ cái, khoảng trắng, dấu tiếng Việt
        /// </summary>
        public static (bool IsValid, string Message) ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Tên không được để trống");

            if (name.Length < 2)
                return (false, "Tên phải có ít nhất 2 ký tự");

            if (name.Length > 100)
                return (false, "Tên không được quá 100 ký tự");

            if (ContainsSqlInjection(name))
                return (false, "Tên chứa ký tự không hợp lệ");

            return (true, string.Empty);
        }

        /// <summary>
        /// Validate Email
        /// Cho phép format: name@domain.com hoặc name.surname@domain.com
        /// </summary>
        public static (bool IsValid, string Message) ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (true, string.Empty); // Email có thể để trống

            // Pattern cho phép: name@domain.ext hoặc name.surname@domain.ext
            // name/surname: chữ cái, số, dấu gạch dưới, dấu chấm
            // domain: chữ cái, số, có thể có subdomain
            // ext: ít nhất 2 ký tự
            string emailPattern = @"^[a-zA-Z0-9_]+(\.[a-zA-Z0-9_]+)*@[a-zA-Z0-9]+(\.[a-zA-Z0-9]+)*\.[a-zA-Z]{2,}$";
            
            if (!Regex.IsMatch(email, emailPattern))
                return (false, "Email không đúng định dạng (VD: ten@domain.com hoặc ten.ho@domain.com)");

            if (email.Length > 100)
                return (false, "Email không được quá 100 ký tự");

            return (true, string.Empty);
        }

        /// <summary>
        /// Validate Phone - chỉ số và dấu +, -, khoảng trắng
        /// </summary>
        public static (bool IsValid, string Message) ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return (true, string.Empty); // Phone có thể để trống

            if (!Regex.IsMatch(phone, @"^[\d\+\-\s]+$"))
                return (false, "Số điện thoại không đúng định dạng");

            if (ContainsSqlInjection(phone))
                return (false, "Số điện thoại chứa ký tự không hợp lệ");

            return (true, string.Empty);
        }

        /// <summary>
        /// Validate tất cả thông tin nhân viên
        /// </summary>
        public static (bool IsValid, string Message) ValidateEmployee(string name, string username, string password, string email, string phone)
        {
            var nameResult = ValidateName(name);
            if (!nameResult.IsValid) return nameResult;

            var usernameResult = ValidateUsername(username);
            if (!usernameResult.IsValid) return usernameResult;

            var passwordResult = ValidatePassword(password);
            if (!passwordResult.IsValid) return passwordResult;

            var emailResult = ValidateEmail(email);
            if (!emailResult.IsValid) return emailResult;

            var phoneResult = ValidatePhone(phone);
            if (!phoneResult.IsValid) return phoneResult;

            return (true, string.Empty);
        }
    }
}
