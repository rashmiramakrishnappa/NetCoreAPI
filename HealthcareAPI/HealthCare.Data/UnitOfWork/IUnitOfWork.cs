using Healthcare.Data.Infrastructure;
using Healthcare.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
        IRepository<Contracts> ContractsRepository { get; }
        IRepository<CoveragePlan> CoveragePlanRepository { get; }
        IRepository<RateChart> RateChartRepository { get; }
    }
}
