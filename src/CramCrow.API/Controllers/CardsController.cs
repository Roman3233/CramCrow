using CramCrow.Application.Services;
using CramCrow.Application.DTOs;

using Microsoft.AspNetCore.Mvc;

namespace CramCrow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly CardService _cardService;

    private static readonly Guid TempUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    public CardsController(CardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cards = await _cardService.GetAllByUserIdAsync(TempUserId);
        return Ok(cards);
    }

    [HttpPost]
    public async Task<ActionResult<CardDto>> CreateCard(CreateCardDto dto)
    {
        var card = await _cardService.CreateCardAsync(dto, TempUserId);
        return Ok(card);
    }

    [HttpPost("{id}/review")]
    public async Task<IActionResult> ReviewCard(Guid id, ReviewCardDto dto)
    {
        var updatedCard = await _cardService.ReviewAsync(id, dto.Grade);
        return Ok(updatedCard);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCard(Guid id)
    {
        await _cardService.DeleteCardAsync(id);
        return NoContent();
    }
}