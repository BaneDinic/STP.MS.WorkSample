using Moq;
using STP.MS.WorkSample.Core.Entities;
using STP.MS.WorkSample.Core.Interfaces;
using STP.MS.WorkSample.Services.Controllers;
using STP.MS.WorkSample.Services.ViewModels;
using System;
using System.Linq;
using Xunit;

namespace STP.MS.WorkSample.Test
{
    public class EmployeeTest
    {
        [Fact]
        public void Controller_Add()
        {
            var employee = new EmployeeVm
            {
                Name = "Igor",
                CompanyId = 2,
                ExperienceLevel = "C",
                Salary = 1000,
                StartingDate = new DateTime(2018, 11, 11),
                VacationDays = 21
            };

            var mockMapper = new Mock<IMapper<EmployeeVm>>();
            mockMapper.Setup(m => m.Insert(employee)).Callback((EmployeeVm e) =>
            {
                e.Id = new Random().Next();
            });

            var controller = new EmployeeController(mockMapper.Object);

            var result = controller.Post(employee);

            Assert.IsType<EmployeeVm>(result);
            Assert.True(employee.Id > 0);
            Assert.Equal(employee.Name, result.Name);
        }


        [Fact]
        public void Controller_Delete()
        {
            var mockMapper = new Mock<IMapper<EmployeeVm>>();
            var controller = new EmployeeController(mockMapper.Object);

            var result = controller.Delete(10);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }
    }
}
