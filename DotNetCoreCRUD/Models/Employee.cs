﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreCRUD.Models
{
    [Table("Employees", Schema = "HR")]
    public class Employee
    {
        [Key]
        [Display(Name = "ID")]  
        public int? EmployeeID { get; set; }

        [Display(Name ="Employee Name")]
        [Column(TypeName ="varchar(250)")]
        public string EmployeeName { get; set; } = string.Empty;

        [Display(Name = "Image User")]
        [Column(TypeName = "varchar(250)")]
        public string? ImageUser { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Salary")]
        [Column(TypeName = "decimal(12.2)")]
        public decimal Salary { get; set; }

        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MMMM-yyyy}")]
        public DateTime HiringDate { get; set; }

        [Required]
        [Display(Name = "National Id")]
        [MaxLength(14)]
        [MinLength(14)]
        [Column(TypeName ="varchar(14)")]
        public string VationalId { get; set; }

        public int DepartmentID{ get; set; }

        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }

    }
}
