using System;
using System.Collections.Generic;

namespace test6.Models
{
    public partial class Table2
    {
        public int Intnew { get; set; }
        public int Idaa { get; set; }

        public virtual Table1 IdaaNavigation { get; set; }
    }
}
