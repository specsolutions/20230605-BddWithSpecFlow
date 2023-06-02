﻿using System;
using System.Collections.Generic;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Controllers;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using BddWithSpecFlow.GeekPizza.Web.Services;
using Microsoft.AspNetCore.Mvc;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class HomeStepDefinitions
    {
        private HomePageModel _homePageModel;
        private string _authToken;

        //TODO: Move this step definition method to AuthStepDefinitions.cs
        [Given(@"the client is logged in")]
        public void GivenTheClientIsLoggedIn()
        {
            // The login process generates an authentication token that has to be passed in
            // for all subsequent controller method calls in the same user session.
            // You can see this in the method below, where we call the HomeController.GetHomePageModel
            // method.
            var controller = new AuthController();
            _authToken = controller.Login(new LoginInputModel { Name = "Marvin", Password = "1234" });
        }

        [When(@"the client checks the home page")]
        public void WhenTheClientChecksTheHomePage()
        {
            var controller = new HomeController();
            _homePageModel = controller.GetHomePageModel(_authToken);
        }

        [Then(@"the home page main message should be: ""(.*)""")]
        public void ThenTheHomePageMainMessageShouldBe(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, _homePageModel.MainMessage);
        }

        [Then(@"the user name of the client should be on the home page")]
        public void ThenTheUserNameOfTheClientShouldBeOnTheHomePage()
        {
            Assert.AreEqual("Marvin", _homePageModel.UserName);
        }
    }
}
