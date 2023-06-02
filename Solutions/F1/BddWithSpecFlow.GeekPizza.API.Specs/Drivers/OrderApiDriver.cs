﻿using System;
using System.Net;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using BddWithSpecFlow.GeekPizza.Web.Models;
using BddWithSpecFlow.GeekPizza.Web.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BddWithSpecFlow.GeekPizza.Specs.Drivers
{
    public class OrderApiDriver
    {
        private readonly WebApiContext _webApiContext;
        private WebApiResponse _lastResponse;

        public string LastError => _lastResponse?.ResponseMessage;

        public OrderApiDriver(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        public Order GetMyOrder()
        {
            return _webApiContext.ExecuteGet<Order>("api/order");
        }

        public bool AttemptAddToOrder(AddToOrderInputModel addToOrderInput)
        {
            _lastResponse = _webApiContext.ExecutePost("/api/order", addToOrderInput);
            return _lastResponse.StatusCode == HttpStatusCode.OK;
        }

        public void EnsureAddToOrder(string name, OrderItemSize size)
        {
            EnsureAddToOrder(new AddToOrderInputModel
            {
                Name = name,
                Size = size
            });
        }

        public void EnsureAddToOrder(AddToOrderInputModel addToOrderInput)
        {
            var response = AttemptAddToOrder(addToOrderInput);
            Assert.IsTrue(response, LastError);
        }

        public void UpdateOrderDetails(DateTime deliveryDate, TimeSpan? deliveryTime)
        {
            var orderChange = new Order
            {
                DeliveryDate = deliveryDate,
                DeliveryTime = deliveryTime ?? TimeSpan.Zero
            };
            _lastResponse = _webApiContext.ExecutePut("/api/order", orderChange);
            Assert.AreEqual(HttpStatusCode.OK, _lastResponse.StatusCode, _lastResponse.ResponseMessage);
        }
    }
}
