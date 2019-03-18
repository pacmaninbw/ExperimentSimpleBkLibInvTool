using System.Data;
using MySql.Data.MySqlClient;
using pacsw.BookInventory.Models.DataTableModel;

namespace pacsw.BookInventory.Models.SynopsisNs
{
    public class SynopsisTable : CDataTableModel
    {
        public SynopsisTable() :
            base("bksynopsis", "getSynopsis", "insertOrUpdateSynopsis")
        {
        }

        public DataTable BookInfoTable { get { return DataTable; } }

        public bool AddSynopsis(Synopsis synopsis)
        {
            if (synopsis.BookId > 0)
            {
                return addItem(synopsis);
            }
            else
            {
                return false;
            }
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("Synopsis", GetDBColumnData("StoryLine"), parameters["@bookDescription"]);
            _addSqlCommandParameter("ID", GetDBColumnData("BookFKsyop"), parameters["@bookKey"]);
        }
    }
}
