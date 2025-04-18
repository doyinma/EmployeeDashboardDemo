using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeDashboardDemo.Models
{
    public class PositionDescription
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DivisionId { get; set; }
        public int DepartmentId { get; set; }

        public string Description { get; set; }

        public bool CheckboxParkingStall { get; set; }
        public bool RadioDeskPhone { get; set; }
        public bool RadioCellPhone { get; set; }

        public string RelatedDocument { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [ForeignKey("DivisionId")]
        public virtual Division Division { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
}