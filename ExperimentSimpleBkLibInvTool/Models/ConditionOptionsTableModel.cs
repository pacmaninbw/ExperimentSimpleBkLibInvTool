using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class ConditionOptionsTableModel : CDataTableModel
    {
        public ConditionOptionsTableModel() :
            base("bookcondition", "getBookCondition", "addConditionToBook")
        {
        }

        public DataTable OptionsTable { get { return DataTable; } }

        public bool AddConditionsAndOptions(ConditionsAndOtherOptionsModel conditionsAndOtherOptions)
        {
            return addItem(conditionsAndOtherOptions);
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKCond"), parameters["@bookKey"]);
            _addSqlCommandParameter("Status Id", GetDBColumnData("NewOrUsed"), parameters["@statusKey"]);
            _addSqlCommandParameter("Condition Id", GetDBColumnData("ConditionOfBook"), parameters["@conditionKey"]);
            _addSqlCommandParameter("Physical Description", GetDBColumnData("PhysicalDescriptionStr"), parameters["@physicalDescStr"]);
            _addSqlCommandParameter("Autographed", GetDBColumnData("IsSignedByAuthor"), parameters["@signedByAuthor"]);
            _addSqlCommandParameter("Read", GetDBColumnData("BookHasBeenRead"), parameters["@haveRead"]);
        }
    }
}
