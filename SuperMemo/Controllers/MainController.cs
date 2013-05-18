using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperMemo.BL;

namespace SuperMemo.Controllers
{
    public class ResponseObject
    {
        public string Data { get; set; }
    }
    public class MainController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            ITranslator t = new Translator();

            return new string[] { "hello", "value2" };
        }

        // GET api/<controller>/5
        public ResponseObject Get(string id)
        {
            ITranslator t = new Translator();
            return new ResponseObject {Data = t.Translate(id)};
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
        }
    }
}