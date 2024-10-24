namespace HeroTest.Models;

public partial class Hero
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Alias { get; set; }
    public bool? IsActive { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int BrandId { get; set; }
    public virtual Brand Brand { get; set; } = null!;
}