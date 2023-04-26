using PersonCRUDWithJSONParameterWebAPI.Constant;
using PersonCRUDWithJSONParameterWebAPI.CoreFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonCRUDWithJSONParameterWebAPI.Models;


namespace PersonCRUDWithJSONParameterWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [HttpPost]
        public HttpResponseMessage Save(Person model)
        {
            DatabaseManagement<int, Person> databaseManage = new DatabaseManagement<int, Person>();
            var returnValue = databaseManage.DatabaseOpration(StoredProcedure.PersonsSave, model, Opration.Save);
            HttpRequestMessage request =new HttpRequestMessage();
            return returnValue == null
                    ? request.CreateErrorResponse(HttpStatusCode.NotFound, "Data save error")
                    : request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

    }
}
