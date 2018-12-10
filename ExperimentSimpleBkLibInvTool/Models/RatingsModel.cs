using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using MySql.Data.MySqlClient;


namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
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

        public uint ID
        {
            get { return GetKeyValue(); }
            set { SetKeyValue(value); }
        }

        public RatingsModel()
        {
            InitCommandParameters();
        }

        public RatingsModel(string myRating, string amazonRating, string goodReadsRating, uint iD)
        {
            InitCommandParameters();
            MyRating = myRating;
            AmazonRating = amazonRating;
            GoodReadsRating = goodReadsRating;
            ID = iD;
        }

        private void InitCommandParameters()
        {
            _addSqlCommandParameter("ID", "BookFKRats", "N/A", MySqlDbType.UInt32, false, ParameterDirection.Input, true);
            _addSqlCommandParameter("My Rating", "MyRatings", "myRating", MySqlDbType.Double, false, ParameterDirection.Input);
            _addSqlCommandParameter("Amazon Rating", "AmazonRatings", "amazonRating", MySqlDbType.Double, false, ParameterDirection.Input);
            _addSqlCommandParameter("GoodReads Rating", "GoodReadsRatings", "goodReadsRating", MySqlDbType.Double, false, ParameterDirection.Input);
        }

        protected override bool _dataIsValid()
        {
            return _defaultIsValid();
        }
    }
}
