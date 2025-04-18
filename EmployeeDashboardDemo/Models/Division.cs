using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeDashboardDemo.Models
{
    public class Division
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a division name.")] 
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<PositionDescription> PositionDescriptions { get; set; }
    }
}