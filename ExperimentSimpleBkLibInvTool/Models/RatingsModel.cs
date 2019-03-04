using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ratings
{
    public class RatingsModel : DataTableItemBaseModel, IRatingsModel
    {

        public string MyRating
        {
            get { return GetParameterValue("My Rating"); }
            set { SetParameterValue("My Rating", value); }
        }

        public string AmazonRating
        {
            get { return GetParameterValue("Amazon Rating"); }
            set { SetParameterValue("Amazon Rating", value); }
        }

        public string GoodReadsRating
        {
            get { return GetParameterValue("GoodReads Rating"); }
            set { SetParameterValue("GoodReads Rating", value); }
        }

        public uint ID
        {
            get { return GetKeyValue(); }
            set { SetKeyValue(value); }
        }

        public RatingsModel(string myRating=null, string amazonRating=null, string goodReadsRating=null, uint iD=0)
            : base(((App)Application.Current).Model.RatingsTable)
        {
            MyRating = myRating;
            AmazonRating = amazonRating;
            GoodReadsRating = goodReadsRating;
            ID = iD;
        }

        protected override bool _dataIsValid()
        {
            return _defaultIsValid();
        }
    }
}
