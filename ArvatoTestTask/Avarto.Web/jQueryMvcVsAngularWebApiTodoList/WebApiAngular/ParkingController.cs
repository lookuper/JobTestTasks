using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using jQueryMvcVsAngularWebApiTodoList.Models;
using jQueryMvcVsAngularWebApiTodoList.MvcjQuery.ViewModels;
using jQueryMvcVsAngularWebApiTodoList.Services;
using jQueryMvcVsAngularWebApiTodoList.WebApiAngular.Filters;
using Arvato.Common;
using Arvato.BusinessLogic;
using System;
using System.Linq;

namespace jQueryMvcVsAngularWebApiTodoList.WebApiAngular
{
    public class ParkingController : ApiController
    {
        private static AbstractParkingPrices parkingPrice1 = new ParkingPrices();
        private readonly List<IParkingHouse> avaliableParking = new List<IParkingHouse>
        {
            new DefaultParingHouse("Parking 1", "Address 1", parkingPrice1),
            new DefaultParingHouse("Parking 2", "Address 2", parkingPrice1),

            // another parking with prices
        };

        public ParkingController()
        {
            var i = 5;
        }

        public void AddNewParking(IParkingHouse parking)
        {
            if (parking == null)
                throw new ArgumentNullException(nameof(parking));

            avaliableParking.Add(parking);
        }

        public List<Invoice> GetAllInvoicesForMonth(Moths month)
        {
            return avaliableParking.SelectMany(p => p.GetAllCustomersInvoicesForMonth(month))
                .ToList();
        }

        public IEnumerable<Object> Get()
        {
            return avaliableParking;
        }

        public TodoList Get(string id)
        {
            throw new NotImplementedException();
            //return _todoListService.Get(id);
        }

        //public HttpResponseMessage Post(TodoList todoList)
        //{
        //    _todoListService.Post(todoList);
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        //public HttpResponseMessage Delete(TodoList todoList)
        //{
        //    _todoListService.Delete(todoList.Id);
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}
    }
}