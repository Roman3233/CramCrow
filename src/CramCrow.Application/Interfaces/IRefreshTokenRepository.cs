using CramCrow.Domain.Entities;

namespace CramCrow.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string tokenHash);
    Task<RefreshToken> AddAsync(RefreshToken refreshToken);
    Task<bool> RevokeAsync(string tokenHash);
}