using FIsrtMVCapp.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//BLAGO MODIFIKOVANA da bi bio izvrsen CODE FIRST pristup
namespace FIsrtMVCapp.Models
{
    public partial class Person
    {
        public int Id { get; set; }

        [Display(Name="First Name")] // izmjenjeno name u Name zbog lokalizacije
        [Required(ErrorMessage = "First name is required")] // Please enter your first name zamenicemo za key-om zbog lokalizacije
        public string FirstName { get; set; }


        [Display(Name ="Last Name")] // izmjenjeno name u Name zbog lokalizacije
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }


        [Display(Name ="Date of Birth")]
        [DataRange]
        public DateTime DateOfBirth { get; set; }

        public int? IsDeleted { get; set; }
    }
}
