using RetailOrdering.Domain.Common;
using System.Collections.Generic;

namespace RetailOrdering.Domain.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
