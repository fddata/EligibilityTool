using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EligibilityTool.Models
{
    public class CardApplication
    {

        public int CardApplicationID { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public decimal Income { get; set; }

        public virtual ICollection<Card> Cards { get; set; }

    }
}