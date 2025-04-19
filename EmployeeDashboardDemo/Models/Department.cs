using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeDashboardDemo.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a department name.")]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please select a division.")]
        [Display(Name = "Division")]
        public int DivisionId { get; set; }

        [ForeignKey("DivisionId")]
        public virtual Division Division { get; set; }

        public virtual ICollection<PositionDescription> PositionDescriptions { get; set; }
    }
}