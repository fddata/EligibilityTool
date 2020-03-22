using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EligibilityTool;
using EligibilityTool.Controllers;
using EligibilityTool.Models;
using EligibilityTool.DAL;
using EligibilityTool.Models.ViewModels;

namespace EligibilityTool.Tests.Controllers
{
    [TestClass]
    public class ApplyControllerTest
    {


        private void SeedDB(EligibilityToolContext context)
        {
            var cards = new List<Card>()
            {
                new Card{Name= "BarclayCard", APR= 21.9m, PromoMessage = "We have the most competative APR in town!" },
                new Card{Name= "Vanquis", APR= 23.5m, PromoMessage = "Get exclusive access to events around the UK through our member deals!" }
            };

            if (context.Cards.Count() == 0)
            {
                context.Cards.AddRange(cards);
                context.SaveChanges();
            }
        }

        private EligibilityToolContext context = new EligibilityToolContext()
        {
        };


        [TestMethod]
        public void TestCanReturnIndexView()
        {
            // Arrange
            ApplyController controller = new ApplyController();


            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }


        [TestMethod]
        public void TestUnder18ReturnsNoCard()
        {
            // Arrange
            ApplyController controller = new ApplyController();

            var model = new ApplyViewModel()
            {
                DOB = DateTime.Now,
                FirstName = "Uder18Test",
                LastName = "Uder18Test",
                Income = 20000m
            };

            SeedDB(context);

            //Act
            var result = (RedirectToRouteResult)controller.Index(model);
            result.RouteValues.TryGetValue("action", out object routeNameObj);
            result.RouteValues.TryGetValue("cardApplicationID", out object cardApplicationIDObj);

            var cardApplicationID = Int16.Parse(cardApplicationIDObj.ToString());
            var cardApplication = context.CardApplications.First(c => c.CardApplicationID == cardApplicationID);

            //Assert
            Assert.AreEqual("Results", routeNameObj.ToString());
            Assert.AreEqual(0, cardApplication.Cards.Count);
        }


        [TestMethod]
        public void TestOver30000ReturnsBarclayCard()
        {
            // Arrange
            ApplyController controller = new ApplyController();

            var model = new ApplyViewModel()
            {
                DOB = new DateTime(1980, 1, 1),
                FirstName = "Over30kTest",
                LastName = "Over30kTest",
                Income = 30001m
            };

            SeedDB(context);

            //Act
            var result = (RedirectToRouteResult)controller.Index(model);
            result.RouteValues.TryGetValue("action", out object routeNameObj);
            result.RouteValues.TryGetValue("cardApplicationID", out object cardApplicationIDObj);

            var cardApplicationID = Int16.Parse(cardApplicationIDObj.ToString());
            var cardApplication = context.CardApplications.First(c => c.CardApplicationID == cardApplicationID);

            //Assert
            Assert.AreEqual("Results", routeNameObj.ToString());
            Assert.AreEqual(1, cardApplication.Cards.Count);
            Assert.AreEqual("BarclayCard", cardApplication.Cards.First().Name);
        }

        [TestMethod]
        public void TestEqualTo30000ReturnsVanquis()
        {
            // Arrange
            ApplyController controller = new ApplyController();

            var model = new ApplyViewModel()
            {
                DOB = new DateTime(1980, 1, 1),
                FirstName = "30kTest",
                LastName = "30kTest",
                Income = 30000m
            };

            SeedDB(context);

            //Act
            var result = (RedirectToRouteResult)controller.Index(model);
            result.RouteValues.TryGetValue("action", out object routeNameObj);
            result.RouteValues.TryGetValue("cardApplicationID", out object cardApplicationIDObj);

            var cardApplicationID = Int16.Parse(cardApplicationIDObj.ToString());
            var cardApplication = context.CardApplications.First(c => c.CardApplicationID == cardApplicationID);

            //Assert
            Assert.AreEqual("Results", routeNameObj.ToString());
            Assert.AreEqual(1, cardApplication.Cards.Count);
            Assert.AreEqual("Vanquis", cardApplication.Cards.First().Name);
        }

        [TestMethod]
        public void TestUnder30000ReturnsVanquis()
        {
            // Arrange
            ApplyController controller = new ApplyController();

            var model = new ApplyViewModel()
            {
                DOB = new DateTime(1980, 1, 1),
                FirstName = "30kTest",
                LastName = "30kTest",
                Income = 29999m
            };

            SeedDB(context);

            //Act
            var result = (RedirectToRouteResult)controller.Index(model);
            result.RouteValues.TryGetValue("action", out object routeNameObj);
            result.RouteValues.TryGetValue("cardApplicationID", out object cardApplicationIDObj);

            var cardApplicationID = Int16.Parse(cardApplicationIDObj.ToString());
            var cardApplication = context.CardApplications.First(c => c.CardApplicationID == cardApplicationID);


            //Assert
            Assert.AreEqual("Results", routeNameObj.ToString());
            Assert.AreEqual(cardApplication.Cards.Count, 1);
            Assert.AreEqual(cardApplication.Cards.First().Name, "Vanquis");
        }

    }
}
