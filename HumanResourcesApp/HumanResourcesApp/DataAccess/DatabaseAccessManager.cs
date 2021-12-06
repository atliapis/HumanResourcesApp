using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourcesApp.DataAccess
{
    public class DatabaseAccessManager
    {
        private readonly HumanResourcesContext _dbContext;

        public DatabaseAccessManager(HumanResourcesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddDepartmentIfNotExists(Department department)
        {
            if (await _dbContext.Departments.FindAsync(department.Id) == null)
            {
                _dbContext.Departments.Add(department);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            var departments = _dbContext.Departments;

            return await departments.ToListAsync();
        }

        public async Task<bool> AddStatusIfNotExists(Status status)
        {
            if (await _dbContext.Statuses.FindAsync(status.Id) == null)
            {
                _dbContext.Statuses.Add(status);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<Status>> GetAllStatusesAsync()
        {
            var statuses = _dbContext.Statuses;

            return await statuses.ToListAsync();
        }

        public async Task<bool> AddEmployeeIfNotExists(Employee employee)
        {
            if (await _dbContext.Employees.FindAsync(employee.EmployeeNumber) == null)
            {
                _dbContext.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<IQueryable<Employee>> GetEmployeesAsync(int departmentId, int statusId)
        {
            var employees = new List<Employee>();
            try
            {
                using (var conn = new SqlConnection(_dbContext.Database.Connection.ConnectionString))
                {
                    using (var command = new SqlCommand("GetFilteredEmployees", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DepartmentId", departmentId);
                        command.Parameters.AddWithValue("@StatusId", statusId);

                        conn.Open();
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (reader.Read())
                        {
                            var department = new Department
                            {
                                Id = (int)reader["DepartmentId"],
                                Name = (string)reader["DepartmentName"]
                            };

                            var status = new Status
                            {
                                Id = (int)reader["StatusId"],
                                Name = (string)reader["StatusName"]
                            };

                            employees.Add(new Employee
                            {
                                EmployeeNumber = (int)reader["EmployeeNumber"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                Email = (string)reader["Email"],
                                Department = department.Id,
                                Status = status.Id,
                                Department1 = department,
                                Status1 = status
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var a = 1;
            }

            return employees.AsQueryable(); ;
        }
    }
}