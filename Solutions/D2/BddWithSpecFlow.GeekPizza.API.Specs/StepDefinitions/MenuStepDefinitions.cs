using System;
using System.Collections.Generic;
using BddWithSpecFlow.GeekPizza.Specs.Drivers;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    // [Scope(Tag = "webapi")] -- scoping is removed, because we apply dynamic switching
    public class MenuStepDefinitions
    {
        private readonly IMenuDriver _menuApiDriver;
        private PizzaMenuModel _menuModel;

        public MenuStepDefinitions(IMenuDriver menuApiDriver)
        {
            _menuApiDriver = menuApiDriver;
        }

        [When(@"the client checks the menu page")]
        public void WhenTheClientChecksTheMenuPage()
        {
            _menuModel = _menuApiDriver.GetPizzaMenu();
        }

        [Then(@"there should be (.*) pizzas listed")]
        public void ThenThereShouldBePizzasListed(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _menuModel.Items.Count);
        }

        [Then(@"the following pizzas should be listed in this order")]
        public void ThenTheFollowingPizzasShouldBeListedInThisOrder(Table expectedMenuItemsTable)
        {
            expectedMenuItemsTable.CompareToSet(_menuModel.Items, sequentialEquality: true);
        }
    }
}
