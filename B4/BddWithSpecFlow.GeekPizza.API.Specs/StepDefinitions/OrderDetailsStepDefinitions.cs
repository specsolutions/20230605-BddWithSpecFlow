using System;
using System.Net;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class OrderDetailsStepDefinitions
    {
        private readonly WebApiContext _webApiContext;

        public OrderDetailsStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [When(@"the client specifies (.*) at (.*) as delivery time")]
        public void WhenTheClientSpecifiesDateAtTimeAsDeliveryTime(DateTime deliveryDate, string deliveryTimeString)
        {
            var orderChange = new Order
            {
                DeliveryDate = deliveryDate,
                DeliveryTime = TimeSpan.Parse(deliveryTimeString)
            };
            // execute request
            var response = _webApiContext.ExecutePut("/api/order", orderChange);
            // functional check
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.ResponseMessage);
        }

        [Then(@"the order should indicate that the delivery date is (.*)")]
        public void ThenTheOrderShouldIndicateThatTheDeliveryDateIsDate(DateTime expectedDate)
        {
            var myOrderResponse = _webApiContext.ExecuteGet<Order>("api/order");
            Assert.AreEqual(expectedDate, myOrderResponse.DeliveryDate.ToLocalTime());
        }

        [Then(@"the delivery time should be (.*)")]
        public void ThenTheDeliveryTimeShouldBe(TimeSpan expectedTime)
        {
            var myOrderResponse = _webApiContext.ExecuteGet<Order>("api/order");
            Assert.AreEqual(expectedTime, myOrderResponse.DeliveryTime);
        }
    }
}
