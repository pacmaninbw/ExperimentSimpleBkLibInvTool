using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Author
{
    public class AuthorTableModel : CDataTableModel
    {
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
    }
}
