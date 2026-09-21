namespace CramCrow.Application.DTOs;

public class CreateCardDto
{
    public required string Word { get; set; }
    public required string Translate { get; set; }
    public string? ExampleSentence { get; set; }
}