using HumanResourcesApp.Models;
using Microsoft.Owin;
using Owin;
using System.Threading.Tasks;
using HumanResourcesApp.DataAccess.Services;
using HumanResourcesApp.Services;
using System;

[assembly: OwinStartupAttribute(typeof(HumanResourcesApp.Startup))]
namespace HumanResourcesApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


            DataBaseSeed(app).Wait();
        }

        /// <summary>
        /// This method sets up all the required database entries during the startup, so the application is usable.
        /// </summary>
        /// <param name="context"></param>
        private static async Task DataBaseSeed(IAppBuilder app)
        {
            var dbContext = new HumanResourcesContext();

            //Seed default data
            Status defaultStatus1 = new Status
            {
                Id = 1,
                Name = "Approved"
            };

            Status defaultStatus2 = new Status
            {
                Id = 2,
                Name = "Pending"
            };

            Status defaultStatus3 = new Status
            {
                Id = 3,
                Name = "Disabled"
            };

            var statusesService = new StatusesService(dbContext);
            await statusesService.AddStatusIfNotExists(defaultStatus1);
            await statusesService.AddStatusIfNotExists(defaultStatus2);
            await statusesService.AddStatusIfNotExists(defaultStatus3);

            //Seed department test data - only for demo
            var departmentService = new DepartmentsService(dbContext);
            var departments = await departmentService.GetAllDepartmentsAsync();
            if(departments.Count > 0)
            {
                return;
            }
            for (int i = 1; i < 4; i++)
            {
                Department department = new Department
                {
                    Id = i,
                    Name = $"Test Department {i}"
                };

                await departmentService.AddDepartmentIfNotExists(department);
            }

            //Seed employee test data - only for demo
            var employeesService = new EmployeesService(dbContext);
            departments = await departmentService.GetAllDepartmentsAsync();
            var statuses = await statusesService.GetAllStatusesAsync();
            var random = new Random();
            for (int i = 1; i < 40; i++)
            {
                var department = departments[random.Next(1, departments.Count)];
                var status = statuses[random.Next(1, statuses.Count)];

                Employee employee = new Employee
                {
                    EmployeeNumber = i,
                    FirstName = $"FirstName {i}",
                    LastName = $"LastName {i}",
                    DateOfBirth = System.DateTime.Now,
                    Email = $"mail{i}@mail.com",
                    Department = department.Id,
                    Status = status.Id,
                    Department1 = department,
                    Status1 = status
                };

                await employeesService.AddEmployeeIfNotExists(employee);
            }
        }        
    }
}
