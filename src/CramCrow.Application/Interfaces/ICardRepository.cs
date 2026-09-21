using CramCrow.Domain.Entities;

namespace CramCrow.Application.Interfaces;

public interface ICardRepository
{
    Task<Card?> GetByIdAsync(Guid id);
    Task<List<Card>> GetAllByUserIdAsync(Guid userId);
    Task<List<Card>> GetDueCardsByUserIdAsync(Guid userId);
    Task<Card> AddAsync(Card card);
    Task UpdateAsync(Card card);
    Task DeleteAsync(Guid id);
}