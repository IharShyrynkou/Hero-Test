using System.ComponentModel.DataAnnotations.Schema;

namespace HeroTest.Models;

public partial class Brand
{
    public Brand()
    {
        Heroes = new HashSet<Hero>();
    }
    
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool? IsActive { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public virtual ICollection<Hero> Heroes { get; set; }
}