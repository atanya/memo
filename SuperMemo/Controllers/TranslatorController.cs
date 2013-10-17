using System.Collections.Generic;
using System.Web.Http;
using SuperMemo.BL;

namespace SuperMemo.Controllers
{
    [Authorize]
    public class TranslatorController : ApiController
    {
        public List<string> Get(string id)
        {
            var result = new Translator().Translate(id);
            return new List<string>{result};
        }
    }
}
