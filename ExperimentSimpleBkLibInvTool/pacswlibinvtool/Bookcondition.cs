using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Bookcondition
    {
        public int BookFkcond { get; set; }
        public int ConditionOfBook { get; set; }
        public int NewOrUsed { get; set; }
        public string PhysicalDescriptionStr { get; set; }
        public byte IsSignedByAuthor { get; set; }
        public byte BookHasBeenRead { get; set; }

        public Bookinfo BookFkcondNavigation { get; set; }
        public Bkconditions ConditionOfBookNavigation { get; set; }
        public Bkstatuses NewOrUsedNavigation { get; set; }
    }
}
