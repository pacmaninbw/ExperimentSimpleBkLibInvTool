using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class BookInfoTableModel : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int AuthorIDColumnIndex;
        private int SeriesIDColumnIndex;
        private int TitleIDColumnIndex;
        private int GenreIDColumnIndex;
        private int FormatIDColumnIndex;

        public BookInfoTableModel() :
            base("bookinfo", "getBookInfo", "addBookToBookInfo")
        {
            BookIDColumnIndex = GetDBColumnData("idBookInfo").IndexBasedOnOrdinal;
            AuthorIDColumnIndex = GetDBColumnData("AuthorFKbi").IndexBasedOnOrdinal;
            SeriesIDColumnIndex = GetDBColumnData("SeriesFKBi").IndexBasedOnOrdinal;
            TitleIDColumnIndex = GetDBColumnData("TitleFKbi").IndexBasedOnOrdinal;
            GenreIDColumnIndex = GetDBColumnData("CategoryFKbi").IndexBasedOnOrdinal;
            FormatIDColumnIndex = GetDBColumnData("BookFormatFKbi").IndexBasedOnOrdinal;
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

        public BookInfoModel GetBookInfo(uint authorId, uint titleId, uint formatId)
        {

            DataRow rawBookInfo = GetBookInfo(_getBookId(authorId, titleId, formatId));
            BookInfoModel bookInfoModel = ConvertDataRowToBookInfo(rawBookInfo);

            return bookInfoModel;
        }

        public uint GetBookId(uint authorId, uint titleId, uint formatId)
        {
            return _getBookId(authorId, titleId, formatId);
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("Author Id", GetDBColumnData("AuthorFKbi"), parameters["@authorKey"]);
            _addSqlCommandParameter("Title Id", GetDBColumnData("TitleFKbi"), parameters["@titleKey"]);
            _addSqlCommandParameter("Genre Id", GetDBColumnData("CategoryFKbi"), parameters["@genreKey"]);
            _addSqlCommandParameter("Format Id", GetDBColumnData("BookFormatFKbi"), parameters["@formatKey"]);
            _addSqlCommandParameter("Series Id", GetDBColumnData("SeriesFKBi"), parameters["@seriesKey"]);
            _addSqlCommandParameter("ID", GetDBColumnData("idBookInfo"), parameters["@bookKey"]);
        }

        private DataRow GetBookInfo(uint bookId)
        {
            DataRow bookInfoData = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
                {
                    int ResultCount;
                    DataTable Dt = new DataTable();

                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "getBookInfo";
                        cmd.Parameters.AddWithValue("@bookKey", bookId);

                        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                        ResultCount = sda.Fill(Dt);
                        bookInfoData = Dt.Rows[0];
                    }
                }

            }
            catch (Exception ex)
            {
                string errorMsg = "Database Error: " + ex.Message;
                MessageBox.Show(errorMsg);
            }

            return bookInfoData;
        }

        private uint _getBookId(uint authorId, uint titleId, uint formatId)
        {
            uint bookId = 0;

            try
            {
                if (authorId > 0 && titleId > 0 && formatId > 0)
                {
                    using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
                    {

                        string SqlQuery = "SELECT bookinfo.idBookInfo FROM BookInfo WHERE bookinfo.AuthorFKbi = @authoirid AND bookinfo.TitleFKbi = @titleid AND bookinfo.BookFormatFKbi = @formatid;";
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = SqlQuery;
                            cmd.AddParameter("@authoirid", MySqlDbType.UInt32, authorId)
                               .AddParameter("@titleid", MySqlDbType.UInt32, titleId)
                               .AddParameter("@formatid", MySqlDbType.UInt32, formatId);

                            bookId = (uint)cmd.ExecuteScalar();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "Database Error: " + ex.Message;
                MessageBox.Show(errorMsg);
            }

            return bookId;
        }

        private BookInfoModel ConvertDataRowToBookInfo(DataRow rawBookInfo)
        {
            uint bookId = uint.Parse(rawBookInfo[BookIDColumnIndex].ToString());
            uint authorId = uint.Parse(rawBookInfo[AuthorIDColumnIndex].ToString());
            uint titleId = uint.Parse(rawBookInfo[TitleIDColumnIndex].ToString());
            uint formatId = uint.Parse(rawBookInfo[FormatIDColumnIndex].ToString());
            uint genreId = uint.Parse(rawBookInfo[GenreIDColumnIndex].ToString());
            uint seriesId = uint.Parse(rawBookInfo[SeriesIDColumnIndex].ToString());

            return new BookInfoModel(bookId, genreId, titleId, authorId, seriesId, formatId);
        }
    }
}
