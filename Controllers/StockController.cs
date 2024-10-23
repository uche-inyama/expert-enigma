using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/stock")]
  [ApiController]
  public class StockController : ControllerBase
  {
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepo;
    public StockController(ApplicationDbContext context, IStockRepository stockRepo)
    {
      _context = context;
      _stockRepo = stockRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
      if(!ModelState.IsValid)
        return BadRequest(ModelState);
      
      var stocks = await _stockRepo.GetAllAsync(query);
      var stockDto = stocks.Select(s => s.ToStockDto());

      return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      if(!ModelState.IsValid)
        return BadRequest(ModelState);
      
      var stock = await _context.Stocks.FindAsync(id);
      if(stock == null) return  NotFound();
      return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public  async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
      if(!ModelState.IsValid)
        return BadRequest(ModelState);

      var stockModel = stockDto.ToStockFromCreateDTO();
      await _stockRepo.CreateAsync(stockModel);
      return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
    {
      if(ModelState.IsValid)
        return BadRequest(ModelState);

      var updatedStock = await _stockRepo.UpdateAsync(id, updateStockDto);
      if(updatedStock == null) 
      {
        return NotFound();
      }

      return Ok(updatedStock.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      if(ModelState.IsValid) 
        return BadRequest(ModelState);

      var stockModel = await _stockRepo.DeleteAsync(id);
      
      if(stockModel == null)
      {
        return NotFound();
      }
      return NoContent();
    }
  }
} 