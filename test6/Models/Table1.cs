using System;
using System.Collections.Generic;

namespace test6.Models
{
    public partial class Table1
    {
        public Table1()
        {
            Table2 = new HashSet<Table2>();
        }

        public int Idaaa { get; set; }
        public string Sdsd { get; set; }

        public virtual ICollection<Table2> Table2 { get; set; }
    }
}
