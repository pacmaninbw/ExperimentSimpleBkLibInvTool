using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Options
{
    public class ConditionOptionsTableModel : CDataTableModel
    {
        public ConditionOptionsTableModel() :
            base("bookcondition", "getBookCondition", "addConditionToBook")
        {
        }

        public DataTable OptionsTable { get { return DataTable; } }

        public bool AddConditionsAndOptions()
        {
            bool wasAdded = false;

            return wasAdded;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("Book Id", GetDBColumnData("BookFKCond"), parameters["@bookKey"]);
            _addSqlCommandParameter("Status Id", GetDBColumnData("NewOrUsed"), parameters["@statusKey"]);
            _addSqlCommandParameter("Condition Id", GetDBColumnData("ConditionOfBook"), parameters["@conditionKey"]);
            _addSqlCommandParameter("Physical Description", GetDBColumnData("PhysicalDescriptionStr"), parameters["@physicalDescStr"]);
            _addSqlCommandParameter("Autographed", GetDBColumnData("IsSignedByAuthor"), parameters["@signedByAuthor"]);
            _addSqlCommandParameter("Read", GetDBColumnData("BookHasBeenRead"), parameters["@haveRead"]);
        }
    }
}
