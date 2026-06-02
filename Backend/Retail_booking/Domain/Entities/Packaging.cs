using RetailOrdering.Domain.Common;
using System.Collections.Generic;

namespace RetailOrdering.Domain.Entities;

public class Packaging : BaseEntity
{
    public string Type { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
