namespace CramCrow.Application.DTOs;

public class CreateCardDto
{
    public required string Word { get; set; }
    public required string Translation { get; set; }
    public string? ExampleSentence { get; set; }
}