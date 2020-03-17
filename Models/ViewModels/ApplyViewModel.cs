using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EligibilityTool.Models.ViewModels
{
    public class ApplyViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat (ApplyFormatInEditMode =true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Income (GBP per annum)")]
        public decimal Income { get; set; }
    }






}