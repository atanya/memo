using System.Collections.Generic;
using System.Web.Http;
using SuperMemo.BL;
using SuperMemo.Models;

namespace SuperMemo.Controllers
{
    [Authorize]
    public class TranslatorController : ApiController
    {
        public ResponseObject Get(string id)
        {
            var result = new Translator().Translate(id);
            return ResponseObject.Success(new List<string>{result});
        }
    }
}
