using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class SynopsisTable : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int SummaryColumnIndex;

        public SynopsisTable() :
            base("bksynopsis", "getSynopsis", "insertOrUpdateSynopsis")
        {
            BookIDColumnIndex = GetDBColumnData("BookFKsyop").IndexBasedOnOrdinal;
            SummaryColumnIndex = GetDBColumnData("StoryLine").IndexBasedOnOrdinal;
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

        public Synopsis GetSynopsisData(uint bookId)
        {
            Synopsis bookSummary = null;
            DataRow rawSynopsisData = GetRawData(bookId);

            if (rawSynopsisData != null)
            {
                bookSummary = ConvertDataRowToSynopsis(rawSynopsisData);
            }

            return bookSummary;
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
