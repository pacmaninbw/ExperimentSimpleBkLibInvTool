using System;
using System.Collections.Generic;

namespace BookLibToolModel.pacswlibinvtool
{
    public partial class Volumeinseries
    {
        public int BookFkvs { get; set; }
        public int SeriesFk { get; set; }
        public int? VolumeNumber { get; set; }

        public Bookinfo BookFkvsNavigation { get; set; }
        public Series SeriesFkNavigation { get; set; }
    }
}
