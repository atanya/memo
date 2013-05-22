using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMemo.BL
{
    public interface ITranslator
    {
        string Translate(string word);
    }
}
