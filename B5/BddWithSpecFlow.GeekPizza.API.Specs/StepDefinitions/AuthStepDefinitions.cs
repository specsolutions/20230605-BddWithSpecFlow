﻿using System;
using System.Net;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class AuthStepDefinitions
    {
        private readonly WebApiContext _webApiContext;
        private readonly AuthContext _authContext;

        public AuthStepDefinitions(WebApiContext webApiContext, AuthContext authContext)
        {
            _webApiContext = webApiContext;
            _authContext = authContext;
        }

        [Given(@"the client is logged in")]
        [BeforeScenario("login", Order = 200)]
        public void GivenTheClientIsLoggedIn()
        {
            // prepare JSON payload data
            var data = new LoginInputModel { Name = DomainDefaults.UserName, Password = DomainDefaults.UserPassword };

            // execute request
            var response = _webApiContext.ExecutePost("/api/auth", data);

            // functional check
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            _authContext.LoggedInUserName = DomainDefaults.UserName;
        }

        [Given(@"the client is logged in with user name '([^']*)' and password '([^']*)'")]
        public void GivenTheClientIsLoggedInWithUserNameAndPassword(string userName, string password)
        {
            //TODO: the code duplication will be eliminated in a later exercise

            // prepare JSON payload data
            var data = new LoginInputModel { Name = userName, Password = password };

            // execute request
            var response = _webApiContext.ExecutePost("/api/auth", data);

            // functional check
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            _authContext.LoggedInUserName = userName;
        }
    }
}
