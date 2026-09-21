using CramCrow.Application.DTOs;
using CramCrow.Application.Interfaces;
using CramCrow.Domain.Entities;
using CramCrow.Domain.Enums;

namespace CramCrow.Application.Services;

public class CardService
{
    private readonly ICardRepository _cardRepository;
    public CardService(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public async Task<CardDto> CreateCardAsync(CreateCardDto dto, Guid userId)
    {
        var card = new Card
        {
            Id = Guid.NewGuid(),
            Word = dto.Word,
            Translate = dto.Translate,
            ExampleSentence = dto.ExampleSentence,
            CurrentLevel = 0,
            LastReviewDate = null,
            NextReviewDate = DateTime.UtcNow,
            IsLearned = false,
            UserId = userId,
            User = null!
        };
        var createdCard = await _cardRepository.AddAsync(card);
        
        return MapToDto(createdCard);
    }

    public async Task<List<CardDto>> GetAllByUserIdAsync(Guid userId)
    {
        var cards = await _cardRepository.GetAllByUserIdAsync(userId);
        return cards.Select(card => MapToDto(card)).ToList();
    }

    public async Task<CardDto> ReviewAsync(Guid cardId, ReviewGrade grade)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null)
        {
            throw new KeyNotFoundException($"Card with id {cardId} not found");
        }
        const int MaxLevel = 10;

        switch (grade)
        {
            case ReviewGrade.DontRemember:
                card.CurrentLevel = 0;
                break;
            case ReviewGrade.Hard:
                break;
            case ReviewGrade.Good:
                if (card.CurrentLevel == MaxLevel)
                {
                    card.IsLearned = true;
                }
                else
                {
                    card.CurrentLevel += 1;
                }
                break;
            case ReviewGrade.Easy:
                if (card.CurrentLevel == MaxLevel)
                {
                    card.IsLearned = true;
                }
                else
                {
                    card.CurrentLevel = Math.Min(card.CurrentLevel + 2, MaxLevel);
                }
                break;
        }
        
        if (card.IsLearned)
        {
            card.NextReviewDate = null;
        }
        else
        {
            card.NextReviewDate = DateTime.UtcNow + LevelIntervals[card.CurrentLevel];
        }

        card.LastReviewDate = DateTime.UtcNow;

        await _cardRepository.UpdateAsync(card);
        return MapToDto(card);
    }

    private static readonly TimeSpan[] LevelIntervals =
    {
        TimeSpan.Zero,
        TimeSpan.FromMinutes(20),
        TimeSpan.FromHours(1),
        TimeSpan.FromHours(9),
        TimeSpan.FromDays(1),
        TimeSpan.FromDays(2),
        TimeSpan.FromDays(6),
        TimeSpan.FromDays(14),
        TimeSpan.FromDays(30),
        TimeSpan.FromDays(60),
        TimeSpan.FromDays(180)
    };

    private static CardDto MapToDto(Card card) => new()
    {
        Id = card.Id,
        Word = card.Word,
        Translate = card.Translate,
        ExampleSentence = card.ExampleSentence,
        CurrentLevel = card.CurrentLevel,
        NextReviewDate = card.NextReviewDate,
        IsLearned = card.IsLearned,
        LastReviewDate = card.LastReviewDate
    };
}