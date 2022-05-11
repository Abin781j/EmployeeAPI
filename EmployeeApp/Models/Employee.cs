using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    public class Employee
    {
        [Key]
        public Guid EmpId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeMail { get; set; }
        public int EmployeePhone { get; set; }
        public string EmployeePassword { get; set; }
    }
}
