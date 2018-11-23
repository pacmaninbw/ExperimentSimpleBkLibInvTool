using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Forsale
    {
        public int BookFkfs { get; set; }
        public byte IsForSale { get; set; }
        public double AskingPrice { get; set; }
        public double EstimatedValue { get; set; }

        public Bookinfo BookFkfsNavigation { get; set; }
    }
}
