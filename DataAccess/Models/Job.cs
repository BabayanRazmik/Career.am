using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public int IndustryId { get; set; }
        [ForeignKey("IndustryId")]
        public Industry Industry { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int TypeEmploymentId { get; set; }
        [ForeignKey("TypeEmploymentId")]
        public TypeEmployment TypeEmployment { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Requirement { get; set; }
        public string Responsibilities { get; set; }
        public string AppProcedures { get; set; }
        public float? Salary { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
