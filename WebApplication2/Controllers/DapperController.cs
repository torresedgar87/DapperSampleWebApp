using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    public class DapperController : ControllerBase
    {
        private string connectionString = "server=localhost\\SQLEXPRESS;database=summerfun;Trusted_Connection=True;";

        [HttpPost]
        [Route("/Persons")]
        public async Task<ActionResult> Post()
        {
            var rows = 0;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                rows = await db.ExecuteAsync("INSERT INTO dbo.Persons([Name],[CreateDateTime]) VALUES(@Name,@CreateDateTime)", new { Name = "Edgar", CreateDateTime = DateTime.Now });
            }

            return new OkObjectResult(rows);
        }

        [HttpGet]
        [Route("/Persons")]
        public async Task<ActionResult<object>> Get()
        {
            List<Person> persons = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                persons = (await db.QueryAsync<Person>("SELECT * FROM dbo.Persons")).ToList();
            }

            return new OkObjectResult(persons);
        }
    }
}
