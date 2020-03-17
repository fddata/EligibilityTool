using EligibilityTool.DAL;
using EligibilityTool.Models;
using EligibilityTool.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EligibilityTool.Controllers
{
    public class ApplyController : Controller
    {

        private EligibilityToolContext db = new EligibilityToolContext();

        // GET: Apply
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ApplyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {

                var eligibleCards = GetEligibleCards(model);

            }

        }


        private List<Card> GetEligibleCards(ApplyViewModel model)
        {
            var result = new List<Card>() { };
            try
            {
                if (model.DOB.AddYears(18) > DateTime.Now)
                {
                    //applicant is less than 18, return empty list
                    return result;
                }
                else if (model.Income < 30000m)
                {
                    result.Add(db.Cards.First(c => c.Name == "Vanquis"));
                    return result;
                }
                else
                {
                    result.Add(db.Cards.First(c => c.Name == "BarclayCard"));
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetEligibleCards failed with message: {e.Message}");
                //return empty list
                return result;
            }

        }






    }
}