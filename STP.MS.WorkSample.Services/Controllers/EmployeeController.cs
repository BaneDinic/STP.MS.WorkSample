using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STP.MS.WorkSample.Core.Entities;
using STP.MS.WorkSample.Core.Interfaces;
using STP.MS.WorkSample.Services.ViewModels;

namespace STP.MS.WorkSample.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private IMapper<EmployeeVm> mapperEmployee;

        public EmployeeController(IMapper<EmployeeVm> mapperEmployee)
        {
            this.mapperEmployee = mapperEmployee;
        }

        // POST: api/Employee/getbycompany?companyid=7
        [HttpPost]
        [Route("GetByCompany")]
        public ICollectionJsonResult<EmployeeVm> GetByCompany([FromQuery]int companyid, [FromBody]CollectionRequestParams reqParams)
        {
            return mapperEmployee.GetByParentId(companyid, reqParams);
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public EmployeeVm Get(int id)
        {
            return mapperEmployee.Get(id);
        }

        // POST: api/Employee
        [HttpPost]
        public EmployeeVm Post([FromBody]EmployeeVm value)
        {
            mapperEmployee.Insert(value);

            return value;
        }

        // PUT: api/Employee
        [HttpPut]
        public bool Put([FromBody]EmployeeVm value)
        {
            mapperEmployee.Update(value);

            return true;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            mapperEmployee.Delete(id);

            return true;
        }
    }
}
