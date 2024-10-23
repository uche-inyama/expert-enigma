using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Dtos.Stock
{
  public class StockDto
  {
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public String CompanyName { get; set; } = string.Empty;
    public decimal Purchase {get; set; }
    public decimal Dividend {get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public int MarketCap { get; set; }
    public List<CommentDto>? Comments { get; set; }
  }
}