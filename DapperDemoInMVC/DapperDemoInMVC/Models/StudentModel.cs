using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperDemoInMVC.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string StudentAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}