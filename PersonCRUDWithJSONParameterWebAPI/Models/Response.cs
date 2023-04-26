using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonCRUDWithJSONParameterWebAPI.Models
{
    public class Response<T>
    {
        public int RetVal { get; set; }
        public string Message { get; set; }
        public int ErrorNumber { get; set; }


        public T Model { get; set; }
    }
}