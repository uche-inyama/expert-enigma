using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  public class PortfolioController : ControllerBase
  {
    private readonly IPortfolioRepository _portfolioRepo;
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepository;
    public PortfolioController(IPortfolioRepository portfolioRepo, UserManager<AppUser> userManager,
      IStockRepository stockRepository)
    {
      _portfolioRepo = portfolioRepo;
      _userManager = userManager;
      _stockRepository = stockRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);
      var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
      return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(string symbol)
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);
      var stockModel = await _stockRepository.GetBySymbolAsync(symbol);

      if (stockModel == null) return BadRequest("Stock does not exist");

      var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

      if(userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

      var portfolioModel = new Portfolio
      {
        StockId = stockModel.Id,
        AppUserId = appUser.Id
      };

      await _portfolioRepo.CreateAsync(portfolioModel);

      if(portfolioModel == null)
      {
        return StatusCode(500, "Could not create");
      }else 
      {
        return Created();
      }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
      var username = User.GetUsername();
      var appUser = await _userManager.FindByNameAsync(username);

      var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
      var filterStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower());

      if (filterStock.Count() == 1)
      {
        await _portfolioRepo.DeletePortfolio(appUser, symbol);
      }
      else
      {
        return BadRequest("Stock not in your Portfolio");
      }
      return Ok();
    }
  }
}