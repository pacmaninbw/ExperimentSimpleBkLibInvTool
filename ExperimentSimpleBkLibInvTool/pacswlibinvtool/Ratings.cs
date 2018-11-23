using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Ratings
    {
        public int BookFkrats { get; set; }
        public double? MyRatings { get; set; }
        public double? AmazonRatings { get; set; }
        public double? GoodReadsRatings { get; set; }

        public Bookinfo BookFkratsNavigation { get; set; }
    }
}
