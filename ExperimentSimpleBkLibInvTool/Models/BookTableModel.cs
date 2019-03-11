using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public class BookTableModel : CDataTableModel
    {
        public DataTable BookTable
        {
            get { return DataTable; }
        }

        public BookTableModel() : base("bookinfo", "getallbooks", "") //"addBookToLibrary")
        {
        }

        public bool AddBook(BookModel NewBook)
        {
            return false;// addItem(NewBook);
        }

        public uint GetTitleKey(string Title)
        {
            uint titleKey = 0;

            return titleKey;
        }

        private bool _dbAddBookToLibrary(BookModel NewBook)
        {
            bool AddSeriesSuccess = true;
            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = _addItemStoredProcedureName;
                        cmd.Parameters.AddWithValue("authorFirst", NewBook.AuthorInfo.FirstName);
                        cmd.Parameters.AddWithValue("authorLast", NewBook.AuthorInfo.LastName);
                        cmd.Parameters.AddWithValue("seriesTitle", NewBook.SeriesInfo.Title);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                    AddSeriesSuccess = false;
                }
            }
            return AddSeriesSuccess;
        }
        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;
#if false
            _addSqlCommandParameter("Genre", GetDBColumnData("CategoryName"), parameters["@categoryName"]);
            _addSqlCommandParameter("Author Last Name", GetDBColumnData("LastName"), parameters["@authorLastName"]);
            _addSqlCommandParameter("Author First Name", GetDBColumnData("FirstName"), parameters["@authorFirstName"]);
            _addSqlCommandParameter("Title", GetDBColumnData("TitleStr"), parameters["@titleStr"]);
            _addSqlCommandParameter("Book Format", GetDBColumnData("FormatName"), parameters["@bookFormatStr"]);
            _addSqlCommandParameter("Copyright", GetDBColumnData("Copyright"), parameters["@copyright"]);
            _addSqlCommandParameter("ISB Number", GetDBColumnData("ISBNumber"), parameters["@iSBNumber"]);
            _addSqlCommandParameter("Edition", GetDBColumnData("Edition"), parameters["@edition"]);
            _addSqlCommandParameter("Printing", GetDBColumnData("Printing"), parameters["@printing"]);
            _addSqlCommandParameter("Publisher", GetDBColumnData("Publisher"), parameters["@publisher"]);
            _addSqlCommandParameter("Out of Print", GetDBColumnData("OutOfPrint"), parameters["@outOfPrint"]);
            _addSqlCommandParameter("Series Title", GetDBColumnData("SeriesName"), parameters["@seriesName"]);
            _addSqlCommandParameter("Volume in Series", GetDBColumnData("VolumeNumber"), parameters["@volumeNumber"]);
            _addSqlCommandParameter("Book Status", GetDBColumnData("BkStatusStr"), parameters["@bkStatus"]);
            _addSqlCommandParameter("Book Condition", GetDBColumnData("ConditionOfBookStr"), parameters["@bkCondition"]);
            _addSqlCommandParameter("Physical Description", GetDBColumnData("PhysicalDescriptionStr"), parameters["@physicalDescStr"]);
            _addSqlCommandParameter("Signed by Author", GetDBColumnData("IsSignedByAuthor"), parameters["@iSignedByAuthor"]);
            _addSqlCommandParameter("Read Book", GetDBColumnData("BookHasBeenRead"), parameters["@haveRead"]);
            _addSqlCommandParameter("In Library", GetDBColumnData("IsOwned"), parameters["@isOwned"]);
            _addSqlCommandParameter("Wish Listed", GetDBColumnData("IsWishListed"), parameters["@isWishListed"]);
            _addSqlCommandParameter("Is For Sale", GetDBColumnData("IsForSale"), parameters["@isForSale"]);
            _addSqlCommandParameter("Asking Price", GetDBColumnData("AskingPrice"), parameters["@askingPrice"]);
            _addSqlCommandParameter("Estimated Value", GetDBColumnData("EstimatedValue"), parameters["@estimatedValue"]);
            _addSqlCommandParameter("Synopsis of Book", GetDBColumnData("StoryLine"), parameters["@bookDescription"]);
            _addSqlCommandParameter("My Rating", GetDBColumnData("MyRatings"), parameters["@myRating"]);
            _addSqlCommandParameter("Amazon Rating", GetDBColumnData("AmazonRatings"), parameters["@amazonRating"]);
            _addSqlCommandParameter("GoodReads Rating", GetDBColumnData("GoodReadsRatings"), parameters["@goodReadsRating"]);
            _addSqlCommandParameter("Primary Key", PrimaryKey, parameters["@bookKey"]);
#endif
        }

    }
}
