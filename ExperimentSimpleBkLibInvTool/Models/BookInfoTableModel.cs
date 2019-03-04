using System.Data;
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

        public bool AddBookInfo(IBookModel bookInfo, uint bookId)
        {
            bool addBookWasSuccessful = false;

            BookModel BookInfo = (BookModel)bookInfo;
            addBookWasSuccessful = addItem(BookInfo);
            bookId = _newKeyValue;

            return addBookWasSuccessful;
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
