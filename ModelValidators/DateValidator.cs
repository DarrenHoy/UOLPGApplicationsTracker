using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PGProgrammeApplications.ModelValidators
{
    public class DateValidator:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = DateTime.Now;
            return DateTime.TryParse(value as string, out date);
        }
    }
}