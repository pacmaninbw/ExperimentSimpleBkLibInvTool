using System.Data;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class ForSaleTableModel : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int IsForSaleColumnIndex;
        private int AskingPriceColumnIndex;
        private int EstimatedValueColumnIndex;

        public ForSaleTableModel() : base("forsale", "getBookForSaleData", "addForSaleDataToBook")
        {
            BookIDColumnIndex = GetDBColumnData("BookFKfs").IndexBasedOnOrdinal;
            IsForSaleColumnIndex = GetDBColumnData("IsForSale").IndexBasedOnOrdinal;
            AskingPriceColumnIndex = GetDBColumnData("AskingPrice").IndexBasedOnOrdinal;
            EstimatedValueColumnIndex = GetDBColumnData("EstimatedValue").IndexBasedOnOrdinal;
        }

        public DataTable ForSaleTable => DataTable;

        public bool AddForSaleData(ForSaleModel forSaleData)
        {
            return (forSaleData.BookId > 0) ? addItem(forSaleData) : false;
        }

        public ForSaleModel GetForSaleModel(uint bookId)
        {
            DataRow rawForSaleData = GetRawData(bookId);
            return (rawForSaleData != null) ? ConvertDataRowToForSaleModel(rawForSaleData) : null;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKfs"), parameters["@bookKey"]);
            _addSqlCommandParameter("Is For Sale", GetDBColumnData("IsForSale"), parameters["@isForSale"]);
            _addSqlCommandParameter("Asking Price", GetDBColumnData("AskingPrice"), parameters["@askingPrice"]);
            _addSqlCommandParameter("Estimated Value", GetDBColumnData("EstimatedValue"), parameters["@estimatedValue"]);
        }

        private ForSaleModel ConvertDataRowToForSaleModel(DataRow rawForSaleData)
        {
            uint bookId = uint.Parse(rawForSaleData[BookIDColumnIndex].ToString());
            string askingPrice = rawForSaleData[AskingPriceColumnIndex].ToString();
            string estimatedValue = rawForSaleData[EstimatedValueColumnIndex].ToString();
            bool isForSale = int.Parse(rawForSaleData[IsForSaleColumnIndex].ToString()) > 0;

            return new ForSaleModel(bookId, isForSale, askingPrice, estimatedValue);
        }
    }
}
