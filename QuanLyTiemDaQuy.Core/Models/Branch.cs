namespace QuanLyTiemDaQuy.Core.Models;

/// <summary>
/// Chi nhánh cửa hàng
/// </summary>
public class Branch
{
    public int BranchId { get; set; }
    public string BranchCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
