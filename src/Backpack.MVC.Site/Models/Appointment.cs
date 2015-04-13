using System;
using System.ComponentModel.DataAnnotations;
using Backpack.WebApi.Validation;

namespace Backpack.MVC.Site.Models
{
    public class Appointment
    {
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(10, MinimumLength = 3)]
        public string ClientName { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter a date")]
        public DateTime Date { get; set; }
        [MustBeTrue(ErrorMessage = "You Must Accept the Terms")]
        public bool TermsAccepted { get; set; }
    }
}