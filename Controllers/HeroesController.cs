using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HeroTest.Models;

namespace HeroTest.Controllers;
[ApiController]
[Route("[controller]")]
public class HeroesController : ControllerBase
{
    private readonly SampleContext _context;

    public HeroesController(SampleContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromForm]string name, [FromForm]string alias, [FromForm]string brand)
    {
        if (string.IsNullOrWhiteSpace(name))  return BadRequest($"Null or empty {nameof(name)} field");
        if (string.IsNullOrWhiteSpace(alias)) return BadRequest($"Null or empty {nameof(alias)} field");
        if (string.IsNullOrWhiteSpace(brand)) return BadRequest($"Null or empty {nameof(brand)} field");
            
        var existingBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Name.ToLower() == brand.ToLower()) 
                            ?? new Brand { Name = brand };

        var hero = new Hero
        {
            Name = name,
            Alias = alias,
            Brand = existingBrand,
            BrandId = existingBrand.Id
        };
        
        await _context.Heroes.AddAsync(hero);
        await _context.SaveChangesAsync();
    
        return Ok(hero.Id);
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var heroes = _context.Heroes
            .Where(h => (bool)h.IsActive!)
            .Include(x => x.Brand).Select(x => new 
            {
                x.Id,
                x.Name,
                x.Alias,
                Brand = x.Brand.Name
            });
        
        return Ok(heroes);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        if (id == default) return BadRequest($"Null or negative {nameof(id)} field");
        
        await _context.Heroes.Where(x => x.Id == id).ExecuteUpdateAsync(x => 
            x.SetProperty(p => p.IsActive, false));
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}