using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Bookinfo
    {
        public int IdBookInfo { get; set; }
        public int TitleFkbi { get; set; }
        public int AuthorFkbi { get; set; }
        public int CategoryFkbi { get; set; }
        public int BookFormatFkbi { get; set; }
        public int SeriesFkbi { get; set; }

        public Authorstab AuthorFkbiNavigation { get; set; }
        public Bookformat BookFormatFkbiNavigation { get; set; }
        public Bookcategories CategoryFkbiNavigation { get; set; }
        public Title TitleFkbiNavigation { get; set; }
        public Bksynopsis Bksynopsis { get; set; }
        public Bookcondition Bookcondition { get; set; }
        public Forsale Forsale { get; set; }
        public Owned Owned { get; set; }
        public Publishinginfo Publishinginfo { get; set; }
        public Purchaseinfo Purchaseinfo { get; set; }
        public Ratings Ratings { get; set; }
        public Volumeinseries Volumeinseries { get; set; }
    }
}
