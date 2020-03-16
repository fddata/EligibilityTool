using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EligibilityTool.Models;
using System.Data.Entity;

namespace EligibilityTool.DAL
{
    public class EligibilityToolInitializer : DropCreateDatabaseIfModelChanges<EligibilityToolContext>
    {

        protected override void Seed(EligibilityToolContext context)
        {
            var cards = new List<Card>
            {
                new Card{Name= "BarclayCard", APR= 21.9m, PromoMessage = "We have the most competative APR in town!" },
                new Card{Name= "Vanquis", APR= 23.5m, PromoMessage = "Get exclusive access to events around the UK through our member deals!" }

            };

            cards.ForEach(c => context.Cards.Add(c));
            context.SaveChanges();

        }
    }
}