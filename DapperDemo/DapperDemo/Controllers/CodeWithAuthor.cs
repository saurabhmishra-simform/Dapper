using ConsoleTables;
using Dapper;
using DapperDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo.Controllers
{
    public class CodeWithAuthor
    {
        private static string? _connectionString = ConfigurationManager.ConnectionStrings["SQLServerConnection"].ConnectionString;

        public static void InsertAutorDetails()
        {
            using (var connection  = new SqlConnection(_connectionString))
            {
                Author author = new Author()
                {
                    Name = "Ram Kumar",
                };
                var sqlString = "Insert into Authors values(@name)";
                int rowsAffected = connection.Execute(sqlString, author);
                if(rowsAffected > 0)
                {
                    Console.WriteLine("Insert Success");
                    DisplayAuthorDetails();
                }
                else
                {
                    Console.WriteLine("Record not inserted");
                }
            }
        }
        public static void DisplayAuthorDetails()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sqlString = "select * from Authors";
                IList<Author> authors= connection.Query<Author>(sqlString).ToList();
                var consoleTable = new ConsoleTable("AuthorID", "AuthorName");
                foreach(Author author in authors)
                {
                    consoleTable.AddRow(author.AuthorId, author.Name);
                }
                consoleTable.Write();
            }
        }
        public static void UpdateAuthorDetails()
        {
            using(var connection =new SqlConnection(_connectionString))
            {
                string sqlString = "update Authors set Name = @name where AuthorId=@Id";
                int rowsAffected = connection.Execute(sqlString, new { @name = "Ramesh", @Id = 4 });
                if(rowsAffected > 0)
                {
                    Console.WriteLine("Update success!");
                    DisplayAuthorDetails();
                }
                else
                {
                    Console.WriteLine("Record Not Updated!");
                }
            }
        }
        public static void DeleteAuthorDetails()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                string sqlString = "delete Authors where AuthorId = @Id";
                int rowAffected = connection.Execute(sqlString, new { @Id = 4 });
                if(rowAffected> 0)
                {
                    Console.WriteLine("Record deleted sucess!");
                    DisplayAuthorDetails();
                }
                else
                {
                    Console.WriteLine("Record not deleted!");
                }
            }
        }

    }
}
