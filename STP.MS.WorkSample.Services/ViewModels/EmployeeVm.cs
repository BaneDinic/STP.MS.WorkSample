using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STP.MS.WorkSample.Services.ViewModels
{
    public class EmployeeVm
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string ExperienceLevel { get; set; }

        public DateTime StartingDate { get; set; }

        public Nullable<decimal> Salary { get; set; }

        public Nullable<int> VacationDays { get; set; }

        public string CompanyName { get; set; }
    }
}
