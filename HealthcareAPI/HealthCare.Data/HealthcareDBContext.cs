using Healthcare.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Data
{
    public class HealthcareDBContext : DbContext
    {
        public HealthcareDBContext(DbContextOptions<HealthcareDBContext> options)
            : base(options)
        {
        }
        public DbSet<CoveragePlan> CoveragePlans { get; set; }
        public DbSet<RateChart> RateCharts { get; set; }
        public DbSet<Contracts> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CoveragePlan>().HasData(new CoveragePlan
            {
                Id = 1,
                CoveragePlanName = "Gold",
                EligibilityCountry = "USA",
                EligibilityDateFrom = new DateTime(2009, 01, 01),
                EligibilityDateTo = new DateTime(2021, 01, 01)
            });
            builder.Entity<RateChart>().HasData(new RateChart
            {
                Id = 1,
                CoveragePlanId = 1,
                FromAge = 0,
                ToAge = 40,
                Gender = "M",
                NetPrice = 1000
            });
            builder.Entity<RateChart>().HasData(new RateChart
            {
                Id = 2,
                CoveragePlanId = 1,
                FromAge = 40,
                ToAge = 100,
                Gender = "M",
                NetPrice = 2000
            });
            builder.Entity<Contracts>().HasData(new Contracts
            {
                Id = 1,
                CustomerCountry = "USA",
                CustomerName = "John",
                CustomerAddress = "Dixon",
                CustomerDOB = new DateTime(1975, 01, 01),
                SaleDate = new DateTime(2012, 01, 01),
                RateChartId = 1
            });
        }
    }
}
