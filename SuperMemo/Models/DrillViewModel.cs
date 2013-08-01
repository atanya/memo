using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMemo.Models
{
    public class DrillViewModel
    {
        public string Word { get; set; }
        public string Translation { get; set; }
        public long TotalWords { get; set; }
        public int LeftWords { get; set; }
    }
}