using System;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
  [Route("api/comments")]
  [ApiController]
  public class CommentController : ControllerBase
  {
    private readonly ICommentRepository _commentRepository;
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepo;

    public CommentController(ApplicationDbContext context, 
                            ICommentRepository commentRepository, IStockRepository stockRepo)
    {
      _context = context;
      _commentRepository = commentRepository;
      _stockRepo = stockRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
      if(!ModelState.IsValid)
        return BadRequest(ModelState);

      var comments = await _commentRepository.GetAllAsync();
      var commentsDto = comments.Select(c => c.ToCommentDto());
      return Ok(commentsDto);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
      if (!ModelState.IsValid) 
        return BadRequest(ModelState);
      

      var comment = await _commentRepository.GetByIdAsync(id);
      if (comment == null)
      {
        return NotFound();
      }
      return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
    {
      if(!ModelState.IsValid)
        return BadRequest(ModelState);

      if(!await _stockRepo.IfStockExists(stockId))
        return BadRequest("Stock does not exist");

      var commentModel = commentDto.ToCommentFromCreate(stockId);
      var newComment = await _commentRepository.CreateAsync(commentModel);
      return Ok(newComment);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCommentDto updateDto, [FromRoute] int id)
    {
      if(!ModelState.IsValid)
        return BadRequest(ModelState); 

      var updatedComment = await _commentRepository.UpdateAsync(id, updateDto.ToCommentFromUpdate());
      return Ok(updatedComment);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
      if(!ModelState.IsValid)
        return BadRequest(ModelState);
      
      var comment = await _commentRepository.DeleteAsync(id);
      return NoContent();
    }
  }
}