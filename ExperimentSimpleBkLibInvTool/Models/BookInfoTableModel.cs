using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public class BookInfoTableModel : CDataTableModel
    {
        public BookInfoTableModel() :
            base("bookinfo", "getBookInfo", "addBookToBookInfo")
        {
        }

        public DataTable BookInfoTable { get { return DataTable; } }

        public bool AddBookInfo(IBookInfoModel bookInfo, out uint bookId)
        {
            bool addBookWasSuccessful = false;

            addBookWasSuccessful = addItem((BookInfoModel)bookInfo);
            bookId = _newKeyValue;

            return addBookWasSuccessful;
        }

        public bool AddBookInfo(BookInfoModel bookInfo)
        {
            bool addBookWasSuccessful = false;

            addBookWasSuccessful = addItem(bookInfo);
            bookInfo.setBookId(_newKeyValue);

            return addBookWasSuccessful;
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

            _addSqlCommandParameter("Author Id", GetDBColumnData("AuthorFKbi"), parameters["@authorKey"]);
            _addSqlCommandParameter("Title Key", GetDBColumnData("TitleFKbi"), parameters["@titleKey"]);
            _addSqlCommandParameter("Genre Id", GetDBColumnData("CategoryFKbi"), parameters["@genreKey"]);
            _addSqlCommandParameter("Format Key", GetDBColumnData("BookFormatFKbi"), parameters["@formatKey"]);
            _addSqlCommandParameter("Series Key", GetDBColumnData("SeriesFKBi"), parameters["@seriesKey"]);
            _addSqlCommandParameter("Book Key", GetDBColumnData("idBookInfo"), parameters["@bookKey"]);
        }
    }
}
