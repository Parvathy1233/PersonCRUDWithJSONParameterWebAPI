using PersonCRUDWithJSONParameterWebAPI.Constant;
using PersonCRUDWithJSONParameterWebAPI.CoreFeature;
using PersonCRUDWithJSONParameterWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PersonCRUDWithJSONParameterWebAPI.Controllers
{
    public class PersonController : ApiController
    {
        // GET: Person
        
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Save(Person model)
        {
            DatabaseManagement<int, Person> databaseManage = new DatabaseManagement<int, Person>();
            var returnValue = databaseManage.DatabaseOpration(StoredProcedure.PersonsSave, model, Opration.Save);
            HttpRequestMessage request = new HttpRequestMessage();
            return returnValue == null
                    ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data save error")
                    : Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage UserCount(int ID)
        {
            Request request= new Request();
            request.ID = ID;
            DatabaseManagement<int, Request> databaseManage = new DatabaseManagement<int, Request>();
            var returnValue = databaseManage.DatabaseOpration(StoredProcedure.PersonsCount, request, Opration.ScalarOutput);
            return returnValue == null
                ? Request.CreateResponse(HttpStatusCode.NotFound)
                : Request.CreateResponse(HttpStatusCode.OK, returnValue);

        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage PersonList()
        {
            Request request = new Request();
            DatabaseManagement<List<Person>, Request> databaseManage = new DatabaseManagement<List<Person>, Request>();

            var response = databaseManage.DatabaseOpration(StoredProcedure.PersonsList, request, Opration.SimpleList);
            return response == null
                ? Request.CreateResponse(HttpStatusCode.NotFound)
                : Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Delete(int id, int status)
        {
            Request request = new Request();
            request.ID = id;
            //request.Status = status;

            DatabaseManagement<int, Request> databaseManage = new DatabaseManagement<int, Request>();
            var response = databaseManage.DatabaseOpration(StoredProcedure.PersonsDelete, request, Opration.Delete);
            return response == null
                ? Request.CreateResponse(HttpStatusCode.NotFound)
                : Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}