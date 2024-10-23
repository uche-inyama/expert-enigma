using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel.DataAnnotations;


namespace api.Models
{
  public class Stock
  {
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public String CompanyName { get; set; } = string.Empty;
    [Column(TypeName ="decimal(18, 2)")]
    public decimal Purchase {get; set; }
    [Column(TypeName ="decimal(18, 2)")]
    public decimal Dividend {get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public int MarketCap { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>(); 
  }
}