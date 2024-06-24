using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementDb.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
      
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Address { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public string Division { get; set; }
        [Required]
        public string EducationalStatus { get; set; }
    }
}