using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDashboardDemo.Models
{
    public class DashboardViewModel
    {
        public List<Division> Divisions { get; set; }
        public List<Department> Departments { get; set; }
        public List<PositionDescription> Positions { get; set; }
    }
}