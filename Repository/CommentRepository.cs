using System;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class CommentRepository : ICommentRepository
  {
    private readonly ApplicationDbContext _context;
    public CommentRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Comment> CreateAsync(Comment CommentModel)
    {
      await _context.Comments.AddAsync(CommentModel);
      await _context.SaveChangesAsync();
      return CommentModel;
    }

    public async Task<Comment?> DeleteAsync(int Id)
    {
      var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == Id);
      _context.Comments.Remove(existingComment);
      await _context.SaveChangesAsync();
      return existingComment;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
      return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int Id)
    {
      var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == Id);
      if (comment == null)
      {
        return null;
      }
      return comment;
    }

    public async Task<Comment?> UpdateAsync(int Id, Comment updateComment)
    {
      var existingComment =  _context.Comments.FirstOrDefault(c => c.Id == Id);
      if(existingComment == null)
      {
        return null;
      }

      existingComment.Title = updateComment.Title;
      existingComment.Title = updateComment.Content;

      await _context.SaveChangesAsync();
      return existingComment;
    }
  }
}