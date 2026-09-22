namespace CramCrow.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public required string TokenHash { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;

    public required Guid UserId { get; set; }
    public required User User { get; set; }
}