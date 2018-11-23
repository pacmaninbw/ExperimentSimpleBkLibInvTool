using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Owned
    {
        public int BookFko { get; set; }
        public byte IsOwned { get; set; }
        public byte IsWishListed { get; set; }

        public Bookinfo BookFkoNavigation { get; set; }
    }
}
