using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Publishinginfo
    {
        public int BookFkpubI { get; set; }
        public string Isbnumber { get; set; }
        public string Copyright { get; set; }
        public int? Edition { get; set; }
        public int? Printing { get; set; }
        public string Publisher { get; set; }
        public byte? OutOfPrint { get; set; }

        public Bookinfo BookFkpubINavigation { get; set; }
    }
}
