using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class SynopsisTable : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int SummaryColumnIndex;

        public SynopsisTable() :
            base("bksynopsis", "getSynopsis", "insertOrUpdateSynopsis", "insertOrUpdateSynopsis")
        {
            BookIDColumnIndex = GetDBColumnData("BookFKsyop").IndexBasedOnOrdinal;
            SummaryColumnIndex = GetDBColumnData("StoryLine").IndexBasedOnOrdinal;
        }

        public bool AddSynopsis(Synopsis synopsis)
        {
            return (synopsis.BookId > 0) ? addItem(synopsis) : false;
        }

        public Synopsis GetSynopsisData(uint bookId)
        {
            DataRow rawSynopsisData = GetRawData(bookId);
            return (rawSynopsisData != null) ? ConvertDataRowToSynopsis(rawSynopsisData) : null;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("Synopsis", GetDBColumnData("StoryLine"), parameters["@bookDescription"]);
            _addSqlCommandParameter("ID", GetDBColumnData("BookFKsyop"), parameters["@bookKey"]);
        }

        private Synopsis ConvertDataRowToSynopsis(DataRow rawSynopsisData)
        {
            uint bookId = uint.Parse(rawSynopsisData[BookIDColumnIndex].ToString());
            string summary = rawSynopsisData[SummaryColumnIndex].ToString();

            return new Synopsis(bookId, summary);
        }
    }
}
