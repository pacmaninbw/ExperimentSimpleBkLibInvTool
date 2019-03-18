using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class FormatTableModel : DictionaryTableModel
    {
        public FormatTableModel() : base("bookformat", "getAllBookFormatsWithKeys", "addFormat")
        {
        }

        public DataTable FormatTable { get { return DataTable; } }

        public string FormatTitle(uint Key)
        {
            return KeyToName(Key);
        }

        public uint FormatKey(string FormatTitle)
        {
            return NameToKey(FormatTitle);
        }

        public void AddFormat(FormatModel Format)
        {
            AddItemToDictionary(Format);
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("Name", GetDBColumnData("FormatName"), parameters["@bookFormatStr"]);
            _addSqlCommandParameter("Primary Key", GetDBColumnData("idFormat"), parameters["@primaryKey"]);
        }
    }
}
