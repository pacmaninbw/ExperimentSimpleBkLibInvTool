using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Author
{
    public class AuthorTableModel : CDataTableModel
    {
        const int IDColumnIndex = 0;
        const int LastNameColumnIndex = 1;
        const int FirstNameColumnIndex = 2;
        const int MiddleNameColumnIndex = 3;
        const int DobColumnIndex = 4;
        const int DodColumnIntex = 5;

        public AuthorTableModel()
        {
            _getTableStoredProcedureName = "getAllAuthorsData";
            _addItemStoredProcedureName = "addAuthor";
            _lastParameterName = "primaryKey";
            InitializeDataTable();
        }

        public DataTable AuthorTable
        {
            get { return DataTable; }
        }

        public bool AddAuthor(AuthorModel NewAuthor)
        {
            return addItem(NewAuthor);
        }

        public DataRow[] FindAuthors(string lastName, string firstname=null)
        {
            DataTable dt = AuthorTable;
            string filterString = "LastName LIKE '" + lastName + "*'";
            DataRow[] authors = dt.Select(filterString);

            return authors;
        }

        public AuthorModel FindAuthorByFirstAndLastNames(string lastName, string firstName=null)
        {
            AuthorModel authorModel = null;
            DataTable dt = AuthorTable;

            return authorModel;
        }
    }
}
