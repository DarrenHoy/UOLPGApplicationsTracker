using PGProgrammeApplications.DataContext;
using PGProgrammeApplications.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGProgrammeApplications.Models
{
    public class StudentDetailViewModel
    {
     
        [Key]
        public Guid Id { get; set; }
        public StudentDetailViewModel()
        {
            ResidencyOptions = new List<SelectListItem>() { new SelectListItem() { Text = "Yes", Value = "Y" }, new SelectListItem() { Text = "No", Value = "N" } };
        }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /*
         * My effort at an email regex - 
         * - any combination of characters and dots
         * - followed by an @
         * - followed by any combination of characters, plus at least one dot
         * - followed by any combination of characters (with an optional dot at the end of the group)
         * - at least one word character as the last
         */
        [Required]
        [RegularExpression(@"^([\d|\w|\.])*@([\w|\d]*)[.]([\w|\d](\.?))*(\w)$")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        //Just let DateTime.TryParse handle this one!
        [Required]
        [DateValidator]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }

        [Required]
        [RegularExpression("Y|N")]
        [Display(Name = "Resident in the UK")]
        public string IsUkResident { get; set; }

        public List<SelectListItem> ResidencyOptions { get; }
        public string IsUkResidentDisplay { get; internal set; }
    }
}