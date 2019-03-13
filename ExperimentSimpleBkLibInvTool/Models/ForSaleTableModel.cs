using System.Data;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale
{
    public class ForSaleTableModel : CDataTableModel
    {
        public ForSaleTableModel() : base("forsale", "getBookForSaleData", "addForSaleDataToBook")
        {
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

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKfs"), parameters["@bookKey"]);
            _addSqlCommandParameter("Is For Sale", GetDBColumnData("IsForSale"), parameters["@isForSale"]);
            _addSqlCommandParameter("Asking Price", GetDBColumnData("AskingPrice"), parameters["@askingPrice"]);
            _addSqlCommandParameter("Estimated Value", GetDBColumnData("EstimatedValue"), parameters["@estimatedValue"]);
        }
    }
}
