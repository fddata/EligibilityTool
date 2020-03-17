using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EligibilityTool.Models.ViewModels
{
    public class ResultsViewModel
    {
        [Required]
        public ICollection<Card> Cards { get; set; }

    }
}