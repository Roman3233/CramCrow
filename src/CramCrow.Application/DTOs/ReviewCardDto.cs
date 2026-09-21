using CramCrow.Domain.Enums;

namespace CramCrow.Application.DTOs;

public class ReviewCardDto
{
    public required ReviewGrade Grade { get; set; }
}