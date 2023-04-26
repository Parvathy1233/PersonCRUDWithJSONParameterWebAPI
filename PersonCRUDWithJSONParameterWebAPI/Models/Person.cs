using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonCRUDWithJSONParameterWebAPI.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}