using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ArticleNumber { get; set; } = null!;

    public int? ProductTypeId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Image { get; set; }

    public decimal MinPartnerPrice { get; set; }

    public double? Length { get; set; }

    public double? Width { get; set; }

    public double? Height { get; set; }

    public virtual ICollection<PartnersProduct> PartnersProducts { get; set; } = new List<PartnersProduct>();

    public virtual ProductType? ProductType { get; set; }
}
