using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
  public static class CommentMappers
  {
    public static CommentDto ToCommentDto (this Comment commentModel)
    {
      return new CommentDto {
        Title = commentModel.Title,
        Content = commentModel.Content,
        CreatedOn = commentModel.CreatedOn,
      };
    }

    public static Comment DtoToComment (this CommentDto commentDto)
    {
      return new Comment 
      {
        Title = commentDto.Title,
        CreatedOn = commentDto.CreatedOn,
        Content = commentDto.Content,
      };
    }
 
    public static Comment ToCommentFromCreate(this CreateCommentDto createCommentDto,  int stockId)
    {
      return new Comment {
        Title = createCommentDto.Title,
        Content = createCommentDto.Content,
        StockId = stockId
      };
    }

     public static Comment ToCommentFromUpdate(this UpdateCommentDto updateCommentDto)
    {
      return new Comment {
        Title = updateCommentDto.Title,
        Content = updateCommentDto.Content,
      };
    }
  }
}