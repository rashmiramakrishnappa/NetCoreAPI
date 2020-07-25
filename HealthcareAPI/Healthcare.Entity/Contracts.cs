using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Healthcare.Entity
{
    public class Contracts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        [MaxLength(2)]
        public string CustomerGender { get; set; }
        public string CustomerCountry { get; set; }
        public DateTime CustomerDOB { get; set; }
        public DateTime SaleDate { get; set; }
        public long RateChartId { get; set; }
        [ForeignKey("RateChartId")]
        public RateChart RateChart { get; set; }
    }
}
