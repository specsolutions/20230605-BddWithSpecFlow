using System;
using System.Linq;
using BddWithSpecFlow.GeekPizza.Specs.Drivers;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class MyOrderStepDefinitions
    {
        private readonly OrderApiDriver _orderApiDriver;

        private Order _myOrderResponse;
        private Table _orderedItems;

        public MyOrderStepDefinitions(OrderApiDriver orderApiDriver)
        {
            _orderApiDriver = orderApiDriver;
        }

        [Given(@"the client has the following items in the basket")]
        public void GivenTheClientHasTheFollowingItemsInTheBasket(Table orderItemsTable)
        {
            var orderItems = orderItemsTable.CreateSet(DomainDefaults.CreateAddToOrderInputModel).ToArray();
            foreach (var orderItem in orderItems)
            {
                _orderApiDriver.EnsureAddToOrder(orderItem);
            }
            _orderedItems = orderItemsTable;
        }

        [Given(@"the client has items in the basket")]
        public void GivenTheClientHasItemsInTheBasket()
        {
            // add a "default" pizza to the basket
            _orderApiDriver.EnsureAddToOrder(DomainDefaults.MenuItemName, DomainDefaults.OrderItemSize);
        }

        [Given(@"the client has a (.*) item in the basket")]
        public void GivenTheClientHasASizeItemInTheBasket(OrderItemSize size)
        {
            _orderApiDriver.EnsureAddToOrder(DomainDefaults.MenuItemName, size);
        }

        [Given(@"the client has (\d+) (.*) items? in the basket")]
        public void GivenTheClientHasASizeItemInTheBasket(int count, OrderItemSize size)
        {
            for (int i = 0; i < count; i++)
            {
                _orderApiDriver.EnsureAddToOrder(DomainDefaults.MenuItemName, size);
            }
        }

        [When(@"the client checks the my order page")]
        public void WhenTheClientChecksTheMyOrderPage()
        {
            _myOrderResponse = _orderApiDriver.GetMyOrder();
        }

        [Then(@"the following items should be listed on the my order page")]
        public void ThenTheFollowingItemsShouldBeListedOnTheMyOrderPage(Table expectedOrderItemsTable)
        {
            expectedOrderItemsTable.CompareToSet(_myOrderResponse.OrderItems);
        }

        [Then(@"the ordered items should be listed on the my order page")]
        public void ThenTheOrderedItemsShouldBeListedOnTheMyOrderPage()
        {
            _orderedItems.CompareToSet(_myOrderResponse.OrderItems);
        }
    }
}
