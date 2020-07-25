using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Healthcare.Entity
{
    public class RateChart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long CoveragePlanId { get; set; }
        [ForeignKey("CoveragePlanId")]
        public CoveragePlan CoveragePlan { get; set; }
        [MaxLength(1)]
        public string Gender { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public decimal NetPrice { get; set; }
    }
}
