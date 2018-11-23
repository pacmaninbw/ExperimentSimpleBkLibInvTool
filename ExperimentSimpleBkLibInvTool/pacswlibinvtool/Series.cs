using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Series
    {
        public Series()
        {
            Volumeinseries = new HashSet<Volumeinseries>();
        }

        public int IdSeries { get; set; }
        public int AuthorOfSeries { get; set; }
        public string SeriesName { get; set; }

        public Authorstab AuthorOfSeriesNavigation { get; set; }
        public ICollection<Volumeinseries> Volumeinseries { get; set; }
    }
}
