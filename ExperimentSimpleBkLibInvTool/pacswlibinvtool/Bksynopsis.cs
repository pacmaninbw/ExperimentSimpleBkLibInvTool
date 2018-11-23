using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Bksynopsis
    {
        public int BookFksyop { get; set; }
        public string StoryLine { get; set; }

        public Bookinfo BookFksyopNavigation { get; set; }
    }
}
