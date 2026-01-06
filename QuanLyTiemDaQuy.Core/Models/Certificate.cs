namespace QuanLyTiemDaQuy.Core.Models;

/// <summary>
/// Chứng nhận đá quý (GIA, IGI, AGS...)
/// </summary>
public class Certificate
{
    public int CertId { get; set; }
    public string CertCode { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
