using System;

namespace QuanLyTiemDaQuy.Models
{
    /// <summary>
    /// Nhà cung cấp đá quý
    /// </summary>
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public DateTime CreatedAt { get; set; }

        // Display property
        public string DisplayText { get { return Name + " - " + ContactPerson; } }
    }
}
