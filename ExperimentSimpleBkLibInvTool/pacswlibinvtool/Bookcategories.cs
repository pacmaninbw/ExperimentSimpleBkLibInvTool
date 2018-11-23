using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Bookcategories
    {
        public Bookcategories()
        {
            Bookinfo = new HashSet<Bookinfo>();
        }

        public int IdBookCategories { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Bookinfo> Bookinfo { get; set; }
    }
}
