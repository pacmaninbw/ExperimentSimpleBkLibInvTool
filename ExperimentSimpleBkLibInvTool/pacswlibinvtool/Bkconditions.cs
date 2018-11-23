using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Bkconditions
    {
        public Bkconditions()
        {
            Bookcondition = new HashSet<Bookcondition>();
        }

        public int IdBkConditions { get; set; }
        public string ConditionOfBookStr { get; set; }

        public ICollection<Bookcondition> Bookcondition { get; set; }
    }
}
