using Dapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCrudWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _config;
        public EmployeeController(IConfiguration config) 
        { 
            _config = config;
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<Employee> emp = await SelectAllEmployee(connection);
            return Ok(emp);
        }

        private static async Task<IEnumerable<Employee>> SelectAllEmployee(SqlConnection connection)
        {
            return await connection.QueryAsync<Employee>("Select * from Employee");
        }

        [HttpGet("{empId}")]
        public async Task<ActionResult<List<Employee>>> GetEmployeeById(int empId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var queryString = "select * from Employee where EmployeeId = @EmpId";
            DynamicParameters dynamicParamiter = new DynamicParameters();
            dynamicParamiter.Add("@EmpId", empId);
            var emp = await connection.QueryFirstAsync<Employee>(queryString, dynamicParamiter);
            return Ok(emp);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee(Employee emp)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var queryString = "Insert into Employee (EmployeeName,DepartmentId,Experience,Salary) values (@EmployeeName,@DepartmentId,@Experience,@Salary)";
            await connection.ExecuteAsync(queryString,emp);
            return Ok(await SelectAllEmployee(connection)); 
        }
        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee emp)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var queryString = "update Employee set EmployeeName=@EmployeeName,DepartmentId=@DepartmentId,Experience=@Experience,Salary=@Salary where EmployeeId = @EmployeeId";
            await connection.ExecuteAsync(queryString, emp);
            return Ok(await SelectAllEmployee(connection));
        }
        [HttpDelete("{empId}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int empId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var queryString = "delete Employee where EmployeeId = @EmpId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmpId", empId);
            await connection.ExecuteAsync(queryString, dynamicParameters);
            return Ok(await SelectAllEmployee(connection));
        }

    }
}
