using System.Collections.Generic;
using System.Web.Http;
using SuperMemo.BL;
using SuperMemo.Filters;
using SuperMemo.Models;

namespace SuperMemo.Controllers
{
    [ErrorHandler]
    public class TranslatorController : ApiController
    {
        public ResponseObject Get(string id)
        {
            var result = new Translator().Translate(id);
            return ResponseObject.Success(new List<string>{result});
        }
    }
}
