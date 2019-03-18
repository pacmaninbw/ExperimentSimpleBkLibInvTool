using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pacsw.BookInventory.Models.DataTableModel;

namespace pacsw.BookInventory.Models.BookInfo.Ratings
{
    public class RatingsTableModel : CDataTableModel
    {
        public RatingsTableModel() : base("ratings", "getRatings", "addBookRatings")
        {
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
