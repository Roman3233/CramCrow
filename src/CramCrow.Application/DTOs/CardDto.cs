namespace CramCrow.Application.DTOs;

public class CardDto
{
    public required Guid Id { get; set; }
    public required string Word { get; set; }
    public required string Translation { get; set; }
    public string? ExampleSentence { get; set; }
    public int CurrentLevel { get; set; }
    public DateTime? NextReviewDate { get; set; }
    public bool IsLearned { get; set; }
    public DateTime? LastReviewDate { get; set; }
}