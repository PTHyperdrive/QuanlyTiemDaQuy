using System;
using Microsoft.Win32;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// License key management and validation
    /// Supports 3 license types: Full, POS, POS Embedded
    /// </summary>
    public static class LicenseManager
    {
        private const string RegistryPath = @"Software\JewelryPOS\QuanLyTiemDaQuy";
        
        public enum LicenseType
        {
            Unknown,
            Full,       // QLTDQ-FULL-XXXX-XXXX - All features
            POS,        // QLTDQ-POS-XXXX-XXXX  - Sales, Products, Customers only
            POSEmbedded // QLTDQ-POSE-XXXX-XXXX - Optimized for embedded devices
        }

        public static class LicenseKeys
        {
            // Development keys (for testing only)
            public const string DevFull = "QLTDQ-FULL-DEV1-2026";
            public const string DevPOS = "QLTDQ-POS-DEV1-2026";
            public const string DevPOSEmbedded = "QLTDQ-POSE-DEV1-2026";
        }

        /// <summary>
        /// Get the current license type from registry
        /// </summary>
        public static LicenseType GetCurrentLicense()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(RegistryPath))
                {
                    if (key == null) return LicenseType.Unknown;
                    
                    var licenseKey = key.GetValue("LicenseKey") as string;
                    if (string.IsNullOrEmpty(licenseKey)) return LicenseType.Unknown;
                    
                    return ParseLicenseType(licenseKey);
                }
            }
            catch
            {
                return LicenseType.Unknown;
            }
        }

        /// <summary>
        /// Validate a license key format and type
        /// </summary>
        public static (bool IsValid, LicenseType Type, string Message) ValidateLicenseKey(string licenseKey)
        {
            if (string.IsNullOrWhiteSpace(licenseKey))
                return (false, LicenseType.Unknown, "License key cannot be empty");

            licenseKey = licenseKey.Trim().ToUpperInvariant();

            // Check format: QLTDQ-XXXX-XXXX-XXXX
            if (!licenseKey.StartsWith("QLTDQ-"))
                return (false, LicenseType.Unknown, "Invalid license key format");

            // Development keys always valid
            if (licenseKey == LicenseKeys.DevFull.ToUpperInvariant())
                return (true, LicenseType.Full, "Development Full License (Valid)");
            if (licenseKey == LicenseKeys.DevPOS.ToUpperInvariant())
                return (true, LicenseType.POS, "Development POS License (Valid)");
            if (licenseKey == LicenseKeys.DevPOSEmbedded.ToUpperInvariant())
                return (true, LicenseType.POSEmbedded, "Development POS Embedded License (Valid)");

            // Parse license type
            var type = ParseLicenseType(licenseKey);
            if (type == LicenseType.Unknown)
                return (false, LicenseType.Unknown, "Unknown license key type");

            // Validate checksum (simple validation - can be enhanced)
            if (!ValidateChecksum(licenseKey))
                return (false, type, "Invalid license key checksum");

            return (true, type, $"Valid {type} License");
        }

        /// <summary>
        /// Parse license type from key prefix
        /// </summary>
        public static LicenseType ParseLicenseType(string licenseKey)
        {
            if (string.IsNullOrEmpty(licenseKey)) return LicenseType.Unknown;

            licenseKey = licenseKey.ToUpperInvariant();

            if (licenseKey.StartsWith("QLTDQ-FULL-"))
                return LicenseType.Full;
            if (licenseKey.StartsWith("QLTDQ-POSE-"))
                return LicenseType.POSEmbedded;
            if (licenseKey.StartsWith("QLTDQ-POS-"))
                return LicenseType.POS;

            return LicenseType.Unknown;
        }

        /// <summary>
        /// Check if a feature is available for the current license
        /// </summary>
        public static bool IsFeatureAvailable(string feature)
        {
            var license = GetCurrentLicense();
            
            // Full license has all features
            if (license == LicenseType.Full)
                return true;

            // Define feature restrictions per license type
            switch (feature.ToLowerInvariant())
            {
                case "sales":
                case "products":
                case "customers":
                case "dashboard":
                    return true; // Available in all licenses

                case "reports":
                case "import":
                case "suppliers":
                case "marketprice":
                    return license == LicenseType.Full; // Full only

                case "users":
                case "system":
                case "discounts":
                    return license == LicenseType.Full; // Admin features - Full only

                default:
                    return license == LicenseType.Full;
            }
        }

        /// <summary>
        /// Save license key to registry
        /// </summary>
        public static bool SaveLicenseKey(string licenseKey)
        {
            try
            {
                using (var key = Registry.LocalMachine.CreateSubKey(RegistryPath))
                {
                    if (key == null) return false;
                    key.SetValue("LicenseKey", licenseKey);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get installed edition from registry
        /// </summary>
        public static string GetInstalledEdition()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(RegistryPath))
                {
                    return key?.GetValue("Edition") as string ?? "Unknown";
                }
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Simple checksum validation (can be enhanced with real algorithm)
        /// </summary>
        private static bool ValidateChecksum(string licenseKey)
        {
            // For demo purposes, accept development keys and any key with correct format
            // In production, implement proper checksum verification
            var parts = licenseKey.Split('-');
            if (parts.Length != 4) return false;
            
            // Each part should be 4-5 characters
            foreach (var part in parts)
            {
                if (part.Length < 3 || part.Length > 5) return false;
            }
            
            return true;
        }

        /// <summary>
        /// Generate a license key (for admin use only)
        /// </summary>
        public static string GenerateLicenseKey(LicenseType type)
        {
            string prefix = type switch
            {
                LicenseType.Full => "QLTDQ-FULL",
                LicenseType.POS => "QLTDQ-POS",
                LicenseType.POSEmbedded => "QLTDQ-POSE",
                _ => "QLTDQ-UNKN"
            };

            // Generate random parts
            var random = new Random();
            string part1 = random.Next(1000, 9999).ToString();
            string part2 = random.Next(1000, 9999).ToString();

            return $"{prefix}-{part1}-{part2}";
        }
    }
}
