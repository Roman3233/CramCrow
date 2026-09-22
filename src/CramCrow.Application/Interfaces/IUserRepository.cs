using CramCrow.Domain.Entities;

namespace CramCrow.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task DeleteAsync(Guid id);
}