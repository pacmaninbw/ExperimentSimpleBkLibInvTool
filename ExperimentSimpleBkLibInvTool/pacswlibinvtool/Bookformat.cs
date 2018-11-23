using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Bookformat
    {
        public Bookformat()
        {
            Bookinfo = new HashSet<Bookinfo>();
        }

        public int IdFormat { get; set; }
        public string FormatName { get; set; }

        public ICollection<Bookinfo> Bookinfo { get; set; }
    }
}
