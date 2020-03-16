using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EligibilityTool.Models
{
    public class Card
    {
        public int CardID { get; set; }

        public string Name { get; set; }

        public decimal APR { get; set; }

        public string PromoMessage { get; set; }

    }
}