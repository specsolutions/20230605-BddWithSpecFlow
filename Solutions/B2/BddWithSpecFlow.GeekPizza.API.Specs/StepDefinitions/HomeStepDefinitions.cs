﻿using System;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class HomeStepDefinitions
    {
        private readonly WebApiContext _webApiContext;
        private readonly AuthContext _authContext;
        private HomePageModel _homePageModel;

        public HomeStepDefinitions(WebApiContext webApiContext, AuthContext authContext)
        {
            _webApiContext = webApiContext;
            _authContext = authContext;
        }

        [When(@"the client checks the home page")]
        public void WhenTheClientChecksTheHomePage()
        {
            _homePageModel = _webApiContext.ExecuteGet<HomePageModel>("/api/home");
        }

        [Then(@"the home page main message should be: ""(.*)""")]
        public void ThenTheHomePageMainMessageShouldBe(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, _homePageModel.MainMessage);
        }

        [Then(@"the user name of the client should be on the home page")]
        public void ThenTheUserNameOfTheClientShouldBeOnTheHomePage()
        {
            Assert.AreEqual(_authContext.AssertLoggedInUser(), _homePageModel.UserName);
        }
    }
}
