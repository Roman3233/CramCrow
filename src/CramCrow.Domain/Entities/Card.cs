namespace CramCrow.Domain.Entities;

public class Card : BaseEntity
{
    public required string Word { get; set; }
    public required string Translate { get; set; }
    public string? ExampleSentence { get; set; }
    public int CurrentLevel { get; set; } = 0;
    public DateTime? LastReviewDate { get; set; }
    public DateTime? NextReviewDate { get; set; }
    public bool IsLearned { get; set; } = false;

    public required Guid UserId { get; set; }
    public required User User { get; set; }
}