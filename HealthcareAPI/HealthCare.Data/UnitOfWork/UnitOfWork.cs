using Healthcare.Data.Infrastructure;
using Healthcare.Entity;
using HealthCare.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Data.UnitOfWork
{
    public class UnitOfWork : Disposable, IUnitOfWork
    {
        readonly HealthcareDBContext _dataContext;
        public UnitOfWork(HealthcareDBContext dataContext)
        {
            _dataContext = dataContext;
        }
        public virtual int Commit()
        {
            return _dataContext.SaveChanges();
        }
        public async virtual Task<int> CommitAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
        private IRepository<Contracts> _ContractsRepository;
        public IRepository<Contracts> ContractsRepository => _ContractsRepository = new RepositoryBase<Contracts>(_dataContext);
        private IRepository<CoveragePlan> _CoveragePlanRepository;
        public IRepository<CoveragePlan> CoveragePlanRepository => _CoveragePlanRepository = new RepositoryBase<CoveragePlan>(_dataContext);
        private IRepository<RateChart> _RateChartRepository;
        public IRepository<RateChart> RateChartRepository => _RateChartRepository = new RepositoryBase<RateChart>(_dataContext);
    }
}
