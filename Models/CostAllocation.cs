using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CostAllocation
{
    public int Id { get; set; }

    public int OverheadCostId { get; set; }
    public OverheadCost? OverheadCost { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    [Range(0, 100)]
    [Column(TypeName = "decimal(5,2)")]
    public decimal AllocationRate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal AllocatedAmount { get; set; }

    public DateTime AllocatedDate { get; set; }

    public string? Note { get; set; }

}