﻿using System;
using BddWithSpecFlow.GeekPizza.Web.Controllers;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class GeekPizzaStepDefinitions
    {
        private HomePageModel _homePageModel;
        private PizzaMenuModel _menuModel;

        [Given(@"the menu has been configured to contain (.*) active and (.*) inactive pizzas")]
        public void GivenTheMenuHasBeenConfiguredToContainActiveAndInactivePizzas(int activePizzaCount, int inactivePizzaCount)
        {
            // We ensure the preconditions by setting the menu records directly to the database (in a pretty verbose way).
            // Alternatively we could also ensure the preconditions by using the AdminController class...

            // create a database connection
            var db = new DataContext();
            
            // clear menu
            db.MenuItems.Clear();

            // add active pizzas
            for (int i = 0; i < activePizzaCount; i++)
            {
                var pizzaMenuItem = new PizzaMenuItem();
                pizzaMenuItem.Name = "Pizza " + i;
                pizzaMenuItem.Ingredients = "[default ingredients]";
                pizzaMenuItem.Calories = 1000;
                pizzaMenuItem.Inactive = false;
                db.MenuItems.Add(pizzaMenuItem);
            }

            // add inactive pizzas (ignore the code duplication for now)
            for (int i = 0; i < inactivePizzaCount; i++)
            {
                var pizzaMenuItem = new PizzaMenuItem();
                pizzaMenuItem.Name = "Old Pizza " + i;
                pizzaMenuItem.Ingredients = "[default ingredients]";
                pizzaMenuItem.Calories = 1000;
                pizzaMenuItem.Inactive = true;
                db.MenuItems.Add(pizzaMenuItem);
            }

            // save changed to the database
            db.SaveChanges();
        }

        [When(@"the client checks the home page")]
        public void WhenTheClientChecksTheHomePage()
        {
            var controller = new HomeController();
            _homePageModel = controller.GetHomePageModel();
        }

        [Then(@"the home page main message should be: ""(.*)""")]
        public void ThenTheHomePageMainMessageShouldBe(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, _homePageModel.MainMessage);
        }

        [When(@"the client checks the menu page")]
        public void WhenTheClientChecksTheMenuPage()
        {
            var controller = new MenuController();
            _menuModel = controller.GetPizzaMenu();
        }

        [Then(@"there should be (.*) pizzas listed")]
        public void ThenThereShouldBePizzasListed(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _menuModel.Items.Count);
        }
    }
}
