using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDashboardDemo.Models
{
    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<PositionDescription> PositionDescriptions { get; set; }
    }
}