using CramCrow.Domain.Entities;
using CramCrow.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CramCrow.Infrastructure.Persistence.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Card?> GetByIdAsync(Guid id)
    {
        return await _context.Cards.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<Card>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Cards.Where(c => c.UserId == userId).Include(c => c.User).ToListAsync();
    }
    public async Task<List<Card>> GetDueCardsByUserIdAsync(Guid userId)
    {
        return await _context.Cards
            .Where(c => c.UserId == userId && c.NextReviewDate <= DateTime.UtcNow && c.IsLearned == false)
            .Include(c => c.User)
            .ToListAsync();
    }

    public async Task<Card> AddAsync(Card entity)
    {
        await _context.Cards.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Cards.FindAsync(id);
        if (entity != null)
        {
            _context.Cards.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task UpdateAsync(Card entity)
    {
        _context.Cards.Update(entity);
        await _context.SaveChangesAsync();
    }
}