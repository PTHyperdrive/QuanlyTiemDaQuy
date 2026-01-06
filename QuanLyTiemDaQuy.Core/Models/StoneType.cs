namespace QuanLyTiemDaQuy.Core.Models;

/// <summary>
/// Loại đá quý (Kim cương, Ruby, Sapphire...)
/// </summary>
public class StoneType
{
    public int StoneTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
