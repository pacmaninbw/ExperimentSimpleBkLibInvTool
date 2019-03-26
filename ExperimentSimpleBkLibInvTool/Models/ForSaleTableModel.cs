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

        public DataTable ForSaleTable { get { return DataTable; } }

        public bool AddForSaleData(IForSaleModel forSaleData)
        {
            ForSaleModel forSaleModel = (ForSaleModel)forSaleData;
            if (forSaleModel.BookId > 0)
            {
                return addItem(forSaleModel);
            }
            else
            {
                return false;
            }
        }

        public ForSaleModel GetForSaleModel(uint bookId)
        {
            DataRow rawForSaleData = GetRawData(bookId);
            ForSaleModel forSaleModel = null;

            if (rawForSaleData != null)
            {
                forSaleModel = ConvertDataRowToForSaleModel(rawForSaleData);
            }

            return forSaleModel;
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
