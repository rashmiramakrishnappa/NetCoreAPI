using Healthcare.Data.UnitOfWork;
using Healthcare.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Services
{
    public class ContractService : IContractService
    {
        readonly IUnitOfWork _unitOfWork;
        public ContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// The API should accept customer name, address, date of birth, gender, and sale date.
        /// Based on customer country, age, gender, and sale date, the system should determine
        /// coverage plan from “Coverage Plan” table
        /// net rate from “Rate Chart” table
        /// save that to database along with other data
        /// </summary>
        public async Task AddContract(Contracts contract)
        {
            var customerAge = DateTime.Now.AddYears(-contract.CustomerDOB.Year).Year;
            var rateChart = (await _unitOfWork.RateChartRepository.Get(x => (x.FromAge >= customerAge && x.ToAge <= customerAge &&
            x.Gender == contract.CustomerGender)));
            if (rateChart != null)
                contract.RateChartId = rateChart.Id;
            else
                throw new Exception();
            _unitOfWork.ContractsRepository.Create(contract);
            await _unitOfWork.CommitAsync();
        }
        /// <summary>
        /// Provide API for updating an existing life insurance contract
        /// The system should update coverage plan and net rate based on latest customer information.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task UpdateContract(Contracts contract)
        {
            contract = await _unitOfWork.ContractsRepository.GetById(contract.Id);
            if (contract != null)
            {
                var customerAge = DateTime.Now.AddYears(-contract.CustomerDOB.Year).Year;
                var rateChart = await _unitOfWork.RateChartRepository.Get(x => (x.FromAge >= customerAge && x.ToAge <= customerAge &&
                x.Gender == contract.CustomerGender));
                if (rateChart != null)
                    contract.RateChartId = rateChart.Id;
                else
                    throw new Exception();
                _unitOfWork.ContractsRepository.Update(contract);
                _unitOfWork.Commit();
            }
        }
        /// <summary>
        /// Provide API for deleting an existing life insurance contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public async Task DeleteContract(int contractId)
        {
            if (await _unitOfWork.ContractsRepository.GetById(contractId) != null)
                _unitOfWork.ContractsRepository.Delete(x => x.Id == contractId);
            else
                throw new Exception();
        }
        /// <summary>
        /// Provide API for getting a list of life insurance contracts
        /// </summary>
        public Task<IEnumerable<Contracts>> GetContracts()
        {
            return _unitOfWork.ContractsRepository.GetAll();
        }
    }
}
