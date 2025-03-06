using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterPol.Models;

public partial class Partner
{
    public int Id { get; set; }

    public int? PartnerTypeId { get; set; }

    public string PartnerName { get; set; } = null!;

    public string LegalAddress { get; set; } = null!;

    public long? Inn { get; set; }

    public string DirectorName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Rating { get; set; }

    public string? Logo { get; set; }

    public virtual PartnerType? PartnerType { get; set; }

    public virtual ICollection<PartnersProduct> PartnersProducts { get; set; } = new List<PartnersProduct>();

    public int Discount
    {
        get
        {
            int totalQuantity = PartnersProducts.Sum(p => p.Quantity);
            if (totalQuantity < 10_000) return 0;
            if (totalQuantity < 50_000) return 5;
            if (totalQuantity < 300_000) return 10;
            return 15;
        }
    }
}
