using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class JobModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Title { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int IndustryId { get; set; }
        public int LocationId { get; set; }
        public int TypeEmploymentId { get; set; }
        public string IndustryName { get; set; }
        public string LocationName { get; set; }
        public string TypeEmployments { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(600, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Requirement { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(600, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Responsibilities { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string AppProcedures { get; set; }
        public bool Status { get; set; }
        public float? Salary { get; set; }

        [Required(ErrorMessage ="This file is required\n[min - today] | [max - up to a month]")]
        public DateTime Deadline { get; set; }
        public bool IsApplaied { get; set; }
    }
}
