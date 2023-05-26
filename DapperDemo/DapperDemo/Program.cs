using Dapper;
using DapperDemo.Controllers;
using DapperDemo.Models;

using System.Data;
using System.Data.SqlClient;

namespace DapperDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int option;
            while (true)
            {
                Console.WriteLine("\n\t\t************Student Menu************");
                Console.WriteLine("\t\t1.Insert");
                Console.WriteLine("\t\t2.Display");
                Console.WriteLine("\t\t3.Update");
                Console.WriteLine("\t\t4.Delete");
                Console.WriteLine("\t\t0.Exit");
                UserData:
                Console.Write("Select option:");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            CodeWithAuthor.InsertAutorDetails();
                            break;
                        case 2:
                            CodeWithAuthor.DisplayAuthorDetails();
                            break;
                        case 3:
                            CodeWithAuthor.UpdateAuthorDetails();
                            break;
                        case 4:
                            CodeWithAuthor.DeleteAuthorDetails();
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format!");
                    goto UserData;
                }
                catch (Exception)
                {
                    Console.WriteLine("Some exception found!");
                    goto UserData;
                }

            }


        }
    }
}