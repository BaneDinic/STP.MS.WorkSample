using STP.MS.WorkSample.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static STP.MS.WorkSample.Core.Enums;

namespace STP.MS.WorkSample.Core.Entities
{
    [Table("Employee")]
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public DateTime StartingDate { get; set; }

        public Nullable<decimal> Salary { get; set; }

        public Nullable<int> VacationDays { get; set; }

        [ForeignKey("CompanyId")]
        public /*virtual*/ Company Company { get; set; }
    }
}
