using System;
using BddWithSpecFlow.GeekPizza.Specs.Drivers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class OrderDetailsStepDefinitions
    {
        private readonly OrderApiDriver _orderApiDriver;

        public OrderDetailsStepDefinitions(OrderApiDriver orderApiDriver)
        {
            _orderApiDriver = orderApiDriver;
        }

        [When(@"the client specifies (.*) at (.*) as delivery time")]
        public void WhenTheClientSpecifiesDateAtTimeAsDeliveryTime(DateTime deliveryDate, TimeSpan? deliveryTime)
        {
            _orderApiDriver.UpdateOrderDetails(deliveryDate, deliveryTime);
        }

        [Then(@"the order should indicate that the delivery date is (.*)")]
        public void ThenTheOrderShouldIndicateThatTheDeliveryDateIsDate(DateTime expectedDate)
        {
            var myOrderResponse = _orderApiDriver.GetMyOrder();
            Assert.AreEqual(expectedDate, myOrderResponse.DeliveryDate.ToLocalTime());
        }

        [Then(@"the delivery time should be (.*)")]
        public void ThenTheDeliveryTimeShouldBe(TimeSpan expectedTime)
        {
            var myOrderResponse = _orderApiDriver.GetMyOrder();
            Assert.AreEqual(expectedTime, myOrderResponse.DeliveryTime);
        }
    }
}
