using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class ConditionOptionsTableModel : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int StatusIDColumnIndex;
        private int ConditionIDColumnIndex;
        private int PhysicalDescriptionColumnIndex;
        private int AutographedColumnIndex;
        private int ReadColumnIndex;

        public ConditionOptionsTableModel() :
            base("bookcondition", "getBookCondition", "addConditionToBook", "insertOrUpdateBookCondition")
        {
            BookIDColumnIndex = GetDBColumnData("BookFKCond").IndexBasedOnOrdinal;
            StatusIDColumnIndex = GetDBColumnData("NewOrUsed").IndexBasedOnOrdinal;
            ConditionIDColumnIndex = GetDBColumnData("ConditionOfBook").IndexBasedOnOrdinal;
            PhysicalDescriptionColumnIndex = GetDBColumnData("PhysicalDescriptionStr").IndexBasedOnOrdinal;
            AutographedColumnIndex = GetDBColumnData("IsSignedByAuthor").IndexBasedOnOrdinal;
            ReadColumnIndex = GetDBColumnData("BookHasBeenRead").IndexBasedOnOrdinal;
        }

        public DataTable OptionsTable { get { return DataTable; } }
        public bool AddConditionsAndOptions(ConditionsAndOtherOptionsModel conditionsAndOtherOptions) => addItem(conditionsAndOtherOptions);
        public bool UpdateConditionsAndOptions(ConditionsAndOtherOptionsModel conditionsAndOtherOptions) => updateItem(conditionsAndOtherOptions);

        public ConditionsAndOtherOptionsModel GetConditionsAndOtherOptions(uint bookId)
        {
            DataRow rawConditionsAndOptions = GetRawData(bookId);
            return (rawConditionsAndOptions != null) ? ConvertDataRowConditionsAndOptions(rawConditionsAndOptions) : null;
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

        private ConditionsAndOtherOptionsModel ConvertDataRowConditionsAndOptions(DataRow rawConditionsAndOptionsData)
        {
            uint bookId = uint.Parse(rawConditionsAndOptionsData[BookIDColumnIndex].ToString());
            uint conditionId = uint.Parse(rawConditionsAndOptionsData[ConditionIDColumnIndex].ToString());
            uint statusId = uint.Parse(rawConditionsAndOptionsData[StatusIDColumnIndex].ToString());
            string physicalDescription = rawConditionsAndOptionsData[PhysicalDescriptionColumnIndex].ToString();
            bool signedByAuthor = int.Parse(rawConditionsAndOptionsData[AutographedColumnIndex].ToString()) > 0;
            bool isRead = int.Parse(rawConditionsAndOptionsData[ReadColumnIndex].ToString()) > 0;

            return new ConditionsAndOtherOptionsModel(bookId, conditionId, statusId, physicalDescription, signedByAuthor, isRead);
        }
    }
}
