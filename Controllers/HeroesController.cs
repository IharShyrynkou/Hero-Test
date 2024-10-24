using HeroTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HeroTest.Controllers;
[ApiController]
[Route("[controller]")]
public class HeroesController : ControllerBase
{
    private readonly ILogger<HeroesController> _logger;
    private readonly SampleContext _context;

    public HeroesController(ILogger<HeroesController> logger, SampleContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpPost]
    public async Task<int> Create(string name, string alias, string brand )
    {
        var existingBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == brand);

        if (existingBrand == null)
        {
            existingBrand = new Brand()
            {
                Name = brand
            };
        }
        
        var hero = new Hero()
        {
            Name = name,
            Alias = alias,
            Brand = existingBrand,
            BrandId = existingBrand.Id
        };
        
        await _context.Heroes.AddAsync(hero);
        await _context.SaveChangesAsync();
    
        return hero.Id;
    }
    
    [HttpGet]
    public async Task<IActionResult>  Get()
    {
        var heroes = _context.Heroes
            .Where(h => (bool)h.IsActive!)
            .Include(x => x.Brand).Select(x => new 
            {
                Id = x.Id,
                Name = x.Name,
                Alias = x.Alias,
                Brand = x.Brand.Name
            });
        
        return Ok(heroes);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _context.Heroes.Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsActive, false));
        
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}

