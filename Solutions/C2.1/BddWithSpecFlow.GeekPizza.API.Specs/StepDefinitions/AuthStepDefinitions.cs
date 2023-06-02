﻿using System;
using BddWithSpecFlow.GeekPizza.Specs.Drivers;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class AuthStepDefinitions
    {
        private readonly AuthApiDriver _authApiDriver;
        private readonly WebApiContext _webApiContext;
        private readonly AuthContext _authContext;
        private bool _loginResult;

        public AuthStepDefinitions(WebApiContext webApiContext, AuthContext authContext, AuthApiDriver authApiDriver)
        {
            _webApiContext = webApiContext;
            _authContext = authContext;
            _authApiDriver = authApiDriver;
        }

        [Given(@"the client is logged in")]
        [BeforeScenario("login", Order = 200)]
        public void GivenTheClientIsLoggedIn()
        {
            var result = _authApiDriver.AttemptLogin(DomainDefaults.UserName, DomainDefaults.UserPassword);
            Assert.IsTrue(result, _authApiDriver.LastError);
            _authContext.LoggedInUserName = DomainDefaults.UserName;
        }

        [Given(@"the client is logged in with user name '([^']*)' and password '([^']*)'")]
        public void GivenTheClientIsLoggedInWithUserNameAndPassword(string userName, string password)
        {
            var result = _authApiDriver.AttemptLogin(userName, password);
            Assert.IsTrue(result, _authApiDriver.LastError);
            _authContext.LoggedInUserName = userName;
        }

        [When(@"the client attempts to log in with user name ""([^""]*)"" and password ""([^""]*)""")]
        public void WhenTheClientAttemptsToLogInWithUserNameAndPassword(string userName, string password)
        {
            _loginResult = _authApiDriver.AttemptLogin(userName, password);
        }

        [Then(@"the login attempt should be successful")]
        public void ThenTheLoginAttemptShouldBeSuccessful()
        {
            Assert.IsTrue(_loginResult, _authApiDriver.LastError);
        }

        [Then(@"the client should be able to access member-only services")]
        public void ThenTheClientShouldBeAbleToAccessMember_OnlyServices()
        {
            // we use the "my order" api as an example of a member-only service
            _webApiContext.ExecuteGet<Order>("api/order");
        }
    }
}
