using Healthcare.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Services
{
    public interface IContractService
    {
        Task AddContract(Contracts contract);
        Task UpdateContract(Contracts contract);
        Task DeleteContract(int contractId);
        Task<IEnumerable<Contracts>> GetContracts();
    }
}
