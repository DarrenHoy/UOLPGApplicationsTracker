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
    //Inheriting from Student - means we can just pass this object off to the DataContext for update
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
         * - with at least one word character as the last
         */
        [Required]
        [RegularExpression(@"^([\d|\w|\.])*@([\w|\d]*)[.]([\w|\d](\.?))*(\w)$", ErrorMessage = "Sorry, it looks like that isn't a valid email address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        //Pretty sure the model binder will reject invalid dates
        [Required]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public string DisplayDateOfBirth { get; set; }

        [Required]
        [RegularExpression("Y|N")]
        [Display(Name = "Resident in the UK")]
        public string IsUkResident { get; set; }

        public List<SelectListItem> ResidencyOptions { get; }
        public string IsUkResidentDisplay { get; internal set; }

        
    }
}