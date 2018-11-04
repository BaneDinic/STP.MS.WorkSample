using STP.MS.WorkSample.Core;
using STP.MS.WorkSample.Core.Entities;
using STP.MS.WorkSample.Core.Interfaces;
using STP.MS.WorkSample.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP.MS.WorkSample.Services.Mappers
{
    public class EmployeeMapper : IMapper<EmployeeVm>
    {
        private IRepository<Employee> repoEmployee;

        public EmployeeMapper(IRepository<Employee> repoEmployee)
        {
            this.repoEmployee = repoEmployee;
        }

        public ICollectionJsonResult<EmployeeVm> GetByParentId(int companyid, ICollectionRequestParams reqParams)
        {
            int totalCount;
            var vmResult = repoEmployee.Get(out totalCount, filter: e => e.CompanyId == companyid,
                pageClause: q => q.Skip(reqParams.pageIndex * reqParams.pageSize).Take(reqParams.pageSize),
                orderBy: q => q.OrderBy(c => c.Name)).Select(e => new EmployeeVm
            {
                Id = e.Id,
                Name = e.Name,
                CompanyId = e.CompanyId,
                ExperienceLevel = e.ExperienceLevel.ToString(),
                Salary = e.Salary,
                StartingDate = e.StartingDate,
                VacationDays = e.VacationDays
            });

            return new CollectionJsonResult<EmployeeVm>
            {
                Items = vmResult,
                TotalCount = totalCount
            };
        }

        public EmployeeVm Get(int id)
        {
            int totalCount;
            var dbEmployee = repoEmployee.Get(out totalCount, filter: e => e.Id == id, includeProperties: "Company").FirstOrDefault();

            var vmResult = new EmployeeVm
            {
                Id = dbEmployee.Id,
                Name = dbEmployee.Name,
                CompanyId = dbEmployee.CompanyId,
                CompanyName = dbEmployee.Company.Name,
                ExperienceLevel = dbEmployee.ExperienceLevel.ToString(),
                Salary = dbEmployee.Salary,
                StartingDate = dbEmployee.StartingDate,
                VacationDays = dbEmployee.VacationDays
            };

            return vmResult;
        }

        public void Update(EmployeeVm employeeVm)
        {
            var dbEmployee = new Employee
            {
                Id = employeeVm.Id,
                Name = employeeVm.Name,
                ExperienceLevel = (Enums.ExperienceLevel)Enum.Parse(typeof(Enums.ExperienceLevel), employeeVm.ExperienceLevel),
                CompanyId = employeeVm.CompanyId,
                Salary = employeeVm.Salary,
                StartingDate = employeeVm.StartingDate,
                VacationDays = employeeVm.VacationDays
            };

            repoEmployee.Update(dbEmployee);
        }

        public void Delete(int id)
        {
            repoEmployee.Delete(id);
        }

        public void Insert(EmployeeVm employeeVm)
        {
            var dbEmployee = new Employee
            {
                Name = employeeVm.Name,
                ExperienceLevel = (Enums.ExperienceLevel)Enum.Parse(typeof(Enums.ExperienceLevel), employeeVm.ExperienceLevel),
                CompanyId = employeeVm.CompanyId,
                Salary = employeeVm.Salary,
                StartingDate = employeeVm.StartingDate,
                VacationDays = employeeVm.VacationDays
            };

            repoEmployee.Insert(dbEmployee);

            employeeVm.Id = dbEmployee.Id;
        }
    }
}
