using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Title
    {
        public Title()
        {
            Bookinfo = new HashSet<Bookinfo>();
        }

        public int IdTitle { get; set; }
        public string TitleStr { get; set; }

        public ICollection<Bookinfo> Bookinfo { get; set; }
    }
}
