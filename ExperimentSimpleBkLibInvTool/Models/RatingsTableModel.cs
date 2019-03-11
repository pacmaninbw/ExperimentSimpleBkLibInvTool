using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ratings
{
    public class RatingsTableModel : CDataTableModel
    {
        public RatingsTableModel() : base("ratings", "getRatings", "addBookRatings")
        {
        }

        public DataTable PublishInfoTable { get { return DataTable; } }

        public bool AddRatings(IRatingsModel PublishingData, uint bookId)
        {
            RatingsModel ratings = (RatingsModel)PublishingData;
            if (bookId > 0 || ratings.getBookID() > 0)
            {
                ratings.setBookId(bookId);
                return addItem(ratings);
            }
            else
            {
                return false;
            }
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKRats"), parameters["@bookKey"]);
            _addSqlCommandParameter("My Rating", GetDBColumnData("MyRatings"), parameters["@myRatings"]);
            _addSqlCommandParameter("Amazon Rating", GetDBColumnData("AmazonRatings"), parameters["@amazonRatings"]);
            _addSqlCommandParameter("GoodReads Rating", GetDBColumnData("GoodReadsRatings"), parameters["@goodReadsRatings"]);
        }
    }
}
