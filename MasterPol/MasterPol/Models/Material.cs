using System;
using System.Collections.Generic;

namespace MasterPol.Models;

public partial class Material
{
    public int Id { get; set; }

    public string MaterialType { get; set; } = null!;

    public decimal DefectPercentage { get; set; }
}
