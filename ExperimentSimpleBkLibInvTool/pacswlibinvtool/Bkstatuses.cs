using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Bkstatuses
    {
        public Bkstatuses()
        {
            Bookcondition = new HashSet<Bookcondition>();
        }

        public int IdBkStatus { get; set; }
        public string BkStatusStr { get; set; }

        public ICollection<Bookcondition> Bookcondition { get; set; }
    }
}
