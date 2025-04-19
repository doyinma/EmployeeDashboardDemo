using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeDashboardDemo.Models
{
    public class PositionDescription
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Position name is required.")]
        [StringLength(100, ErrorMessage = "Position name must be 100 characters or less.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select a division.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid division.")]
        public int DivisionId { get; set; }

        [Required(ErrorMessage = "Please select a department.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid department.")]
        public int DepartmentId { get; set; }

        [StringLength(500, ErrorMessage = "Description must be 500 characters or less.")]
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