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

        public uint InsertTitleIfNotInTable(string title)
        {
            uint titleKey = GetTitleKey(title);

            if (titleKey < 1)
            {
                InsertTitleString(title);
                titleKey = GetTitleKey(title);
            }

            return titleKey;
        }

        public void InsertTitleString(string title)
        {
            string SqlInsert = "INSERT INTO title (title.TitleStr) VALUES('" + title + "');";

            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlInsert;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                }
            }
        }

        public uint GetTitleKey(string title)
        {
            uint titleKey = 0;
            string SqlQuery = "SELECT title.idTitle FROM title WHERE title.TitleStr = '" + title + "';";

            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                int ResultCount = 0;
                DataTable Dt = new DataTable();
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlQuery;
                        cmd.ExecuteNonQuery();
                        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                        ResultCount = sda.Fill(Dt);
                        if (ResultCount > 0)
                        {
                            titleKey = Dt.Rows[0].Field<uint>(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                    titleKey = 0;
                }
            }

            return titleKey;
        }

        public string GetTitle(uint titleKey)
        {
            string title = "";
            string SqlQuery = "SELECT title.TitleStr FROM title WHERE title.idTitle = " + titleKey.ToString() + ";";

            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                try
                {
                    int ResultCount = 0;
                    DataTable Dt = new DataTable();
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlQuery;
                        cmd.ExecuteNonQuery();
                        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                        ResultCount = sda.Fill(Dt);
                    }
                    if (ResultCount > 0)
                    {
                        title = Dt.Rows[0].ToString();
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                }
            }

            return title;
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
