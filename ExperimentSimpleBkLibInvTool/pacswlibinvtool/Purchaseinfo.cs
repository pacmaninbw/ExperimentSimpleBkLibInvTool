using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Purchaseinfo
    {
        public int BookFkpurI { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public double? ListPrice { get; set; }
        public double? PaidPrice { get; set; }
        public string Vendor { get; set; }

        public Bookinfo BookFkpurINavigation { get; set; }
    }
}
