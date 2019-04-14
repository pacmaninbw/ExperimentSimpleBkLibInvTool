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

        public bool AddRatings(RatingsModel ratings) => (ratings.BookId > 0) ? addItem(ratings) : false;
        public bool UpdateRatings(RatingsModel ratings) => (ratings.BookId > 0) ? updateItem(ratings) : false;

        public RatingsModel GetRatingsData(uint bookId)
        {
            DataRow rawRatingsData = GetRawData(bookId);
            return (rawRatingsData != null) ? ConvertDataRowToRatingsModel(rawRatingsData) : null;
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
