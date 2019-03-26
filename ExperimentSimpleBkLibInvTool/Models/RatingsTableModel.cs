using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class RatingsTableModel : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int MyRatingColumnIndex;
        private int AmazonRatingsColumnIndex;
        private int GoodReadsRatingsColumnIndex;

        public RatingsTableModel() : base("ratings", "getRatings", "addBookRatings")
        {
            BookIDColumnIndex = GetDBColumnData("BookFKRats").IndexBasedOnOrdinal;
            MyRatingColumnIndex = GetDBColumnData("MyRatings").IndexBasedOnOrdinal;
            AmazonRatingsColumnIndex = GetDBColumnData("AmazonRatings").IndexBasedOnOrdinal;
            GoodReadsRatingsColumnIndex = GetDBColumnData("GoodReadsRatings").IndexBasedOnOrdinal;
        }

        public DataTable PublishInfoTable { get { return DataTable; } }

        public bool AddRatings(RatingsModel ratings)
        {
            if (ratings.BookId > 0)
            {
                return addItem(ratings);
            }
            else
            {
                return false;
            }
        }

        public RatingsModel GetRatingsData(uint bookId)
        {
            RatingsModel ratingsData = null;
            DataRow rawRatingsData = GetRawData(bookId);

            if (rawRatingsData != null)
            {
                ratingsData = ConvertDataRowToRatingsModel(rawRatingsData);
            }

            return ratingsData;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKRats"), parameters["@bookKey"]);
            _addSqlCommandParameter("My Rating", GetDBColumnData("MyRatings"), parameters["@myRatings"]);
            _addSqlCommandParameter("Amazon Rating", GetDBColumnData("AmazonRatings"), parameters["@amazonRatings"]);
            _addSqlCommandParameter("GoodReads Rating", GetDBColumnData("GoodReadsRatings"), parameters["@goodReadsRatings"]);
        }

        private RatingsModel ConvertDataRowToRatingsModel(DataRow rawRatingsData)
        {
            uint bookId = uint.Parse(rawRatingsData[BookIDColumnIndex].ToString());
            string myRatings = rawRatingsData[MyRatingColumnIndex].ToString();
            string amazonRatings = rawRatingsData[AmazonRatingsColumnIndex].ToString();
            string goodReadsRatings = rawRatingsData[GoodReadsRatingsColumnIndex].ToString();

            return new RatingsModel(bookId, myRatings, amazonRatings, goodReadsRatings);
        }
    }
}
