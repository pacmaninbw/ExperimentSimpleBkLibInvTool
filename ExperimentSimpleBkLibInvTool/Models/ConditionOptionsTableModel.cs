using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pacsw.BookInventory.Models.DataTableModel;

namespace pacsw.BookInventory.Models.Options
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
