using EligibilityTool.DAL;
using EligibilityTool.Models;
using EligibilityTool.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

                model.DOB = CheckDOBIsValidForSQL(model.DOB); // 'required to handle conversion of datetime2 error'


                var eligibleCards = GetEligibleCards(model);
                var cardApplication = new CardApplication
                {
                    Cards = eligibleCards.ToList(),
                    DOB = model.DOB,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Income = model.Income
                };

                db.CardApplications.Add(cardApplication);
                db.SaveChanges();

                Int32 id = cardApplication.CardApplicationID;

                return RedirectToAction("Results", new { cardApplicationID = id });
            }

        }

        [HttpGet]
        public ActionResult Results(int cardApplicationID)
        {
            if (!ModelState.IsValid)
            {
                //todo add a view bag error
                return RedirectToAction("Index", "Apply");
            }
            else
            {
                try
                {
                    var application = db.CardApplications.First(c => c.CardApplicationID == cardApplicationID);

                    var model = new ResultsViewModel()
                    {
                        Cards = application.Cards
                    };
                
                    return View(model);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error in Apply/Results:  {e.Message}");
                    return RedirectToAction("Index", "Apply");
                }
            }   
        }




        private DateTime CheckDOBIsValidForSQL(DateTime dob)
        {
            if (dob < SqlDateTime.MinValue.Value)
            {
                return SqlDateTime.MinValue.Value;
            }
            else if (dob > SqlDateTime.MaxValue.Value)
            {
                return SqlDateTime.MaxValue.Value;
            }
            else
            {
                return dob;
            }
        }



        private IEnumerable<Card> GetEligibleCards(ApplyViewModel model)
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