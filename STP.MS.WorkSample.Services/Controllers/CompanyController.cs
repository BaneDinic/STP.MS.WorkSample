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
    [Route("api/Company")]
    public class CompanyController : Controller
    {
        private IRepository<Company> repoCompany;

        public CompanyController(IRepository<Company> repoCompany)
        {
            this.repoCompany = repoCompany;
        }

        // POST: api/Company/get
        [HttpPost]
        [Route("Get")]
        public ICollectionJsonResult<Company> Get([FromBody]CollectionRequestParams reqParams)
        {
            int totalCount;
            var list = repoCompany.Get(out totalCount,
                pageClause: q=> q.Skip(reqParams.pageIndex * reqParams.pageSize).Take(reqParams.pageSize),
                orderBy: q => q.OrderBy(c => c.Name));

            return new CollectionJsonResult<Company> {
                Items = list,
                TotalCount = totalCount
            };
        }
        
        // POST: api/Company
        [HttpPost]
        public Company Post([FromBody]Company value)
        {
            repoCompany.Insert(value);

            return value;
        }
        
        // PUT: api/Company
        [HttpPut]
        public void Put([FromBody]Company value)
        {
            repoCompany.Update(value);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            repoCompany.Delete(id);

            return true;
        }
    }
}
