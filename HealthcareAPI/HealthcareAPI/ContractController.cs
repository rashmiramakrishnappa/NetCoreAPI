using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Healthcare.Entity;
using Healthcare.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api
{
    [Route("api/[controller]")]
    [EnableCors]
    public class ContractController : Controller
    {
        readonly IContractService _contractService;
        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpPost]
        [Route("AddContract")]
        public async Task<IActionResult> AddContract([FromBody] Contracts contract)
        {
            await _contractService.AddContract(contract);
            return Ok("Record added successfully");
        }
        [HttpPatch]
        [Route("UpdateContract")]
        public async Task<IActionResult> UpdateContract([FromBody] Contracts contract)
        {
            await _contractService.UpdateContract(contract);
            return Ok("Record Updated successfully");
        }
        [HttpDelete]
        [Route("DeleteContract")]
        public async Task<IActionResult> DeleteContract(int contractId)
        {
            await _contractService.DeleteContract(contractId);
            return Ok("Record Deleted successfully");
        }
        [HttpGet]
        [Route("GetContracts")]
        public async Task<IActionResult> GetContracts()
        {
            return Ok(await _contractService.GetContracts());
        }
    }
}

