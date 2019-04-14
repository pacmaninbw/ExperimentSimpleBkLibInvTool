using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class RatingsModel : DataTableItemBaseModel
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

        public RatingsModel(uint bookId=0, string myRating=null, string amazonRating=null, string goodReadsRating=null)
            : base(((App)Application.Current).Model.RatingsTable)
        {
            BookId = bookId;
            MyRating = myRating;
            AmazonRating = amazonRating;
            GoodReadsRating = goodReadsRating;

            Modified = false;       // Initialization is not modification.
        }

        public override bool AddToDb() => ((App)Application.Current).Model.RatingsTable.AddRatings(this);
        public override bool DbUpdate() => ((App)Application.Current).Model.RatingsTable.UpdateRatings(this);

        protected override bool _dataIsValid()
        {
            bool isValid = true;

            if (BookId > 0)
            {
                return _defaultIsValid();
            }

            if (!GetParameterIsValid("My Rating"))
            {
                isValid = false;
            }

            if (!GetParameterIsValid("Amazon Rating"))
            {
                isValid = false;
            }

            if (!GetParameterIsValid("GoodReads Rating"))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
