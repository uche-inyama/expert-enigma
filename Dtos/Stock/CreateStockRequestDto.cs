using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
  public class CreateStockRequestDto
  {
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
    public string Symbol { get; set; } = string.Empty;

    [Required]
    [MaxLength(10, ErrorMessage = "CompanyName cannot be over 10 characters")]
    public String CompanyName { get; set; } = string.Empty;

    [Required]
    [Range(1, 10000000000)]
    public decimal Purchase {get; set; }

    [Required]
    [Range(0.001, 100)]
    public decimal Dividend {get; set; }

    [Required]
    [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
    public decimal LastDiv { get; set; }

    [Required]
    [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
    public string Industry { get; set; } = string.Empty;

    [Range(1, 50000000000)]
    public int MarketCap { get; set; }
  }
}