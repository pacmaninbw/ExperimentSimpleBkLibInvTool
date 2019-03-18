using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
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
    }
}
