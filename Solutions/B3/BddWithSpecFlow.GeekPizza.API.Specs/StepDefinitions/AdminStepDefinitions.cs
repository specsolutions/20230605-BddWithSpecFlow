﻿using System;
using System.Collections.Generic;
using BddWithSpecFlow.GeekPizza.Web.Controllers;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class AdminStepDefinitions
    {
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

        [Given(@"the menu has been configured to contain the following pizzas")]
        public void GivenTheMenuHasBeenConfiguredToContainTheFollowingPizzas(Table menuItemsTable)
        {
            var db = new DataContext();
            db.MenuItems.Clear();

            for (int i = 0; i < menuItemsTable.RowCount; i++)
            {
                var pizzaMenuItem = new PizzaMenuItem();
                pizzaMenuItem.Name = menuItemsTable.Rows[i]["name"];
                pizzaMenuItem.Ingredients = menuItemsTable.ContainsColumn("ingredients") ? menuItemsTable.Rows[i]["ingredients"] : "[default ingredients]";
                pizzaMenuItem.Calories = menuItemsTable.ContainsColumn("calories") ? int.Parse(menuItemsTable.Rows[i]["calories"]) : 1000;
                pizzaMenuItem.Inactive = menuItemsTable.ContainsColumn("inactive") ? bool.Parse(menuItemsTable.Rows[i]["inactive"]) : false;
                db.MenuItems.Add(pizzaMenuItem);
            }
            db.SaveChanges();
        }
    }
}
