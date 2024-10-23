using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class Portfolio
  {
    public string AppUserId { get; set; }
    public int StockId { get; set;}
    public AppUser AppUser { get; set; }
    public Stock Stock { get; set; }
  }
}