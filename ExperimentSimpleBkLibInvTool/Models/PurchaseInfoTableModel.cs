using System.Data;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo
{
    public class PurchaseInfoTableModel : CDataTableModel
    {
        public PurchaseInfoTableModel() : base("purchaseinfo", "getPurchaseInfo", "addPurchaseInfo")
        {
        }

        public DataTable PurchaseInfoTable { get { return DataTable; } }

        public bool AddPurchaseInfo(IPuchaseInfoModel purchaseData, uint bookId)
        {
            PuchaseInfoModel purchaseInfoModel = (PuchaseInfoModel)purchaseData;
            if (bookId > 0 || purchaseInfoModel.getBookID() > 0)
            {
                purchaseInfoModel.setBookId(bookId);
                return addItem(purchaseInfoModel);
            }
            else
            {
                return false;
            }
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKPurI"), parameters["@bookKey"]);
            _addSqlCommandParameter("Date of Purchase", GetDBColumnData("PurchaseDate"), parameters["@purchaseDate"]);
            _addSqlCommandParameter("List Price", GetDBColumnData("ListPrice"), parameters["@listPrice"]);
            _addSqlCommandParameter("Price Paid", GetDBColumnData("pricePaid"), parameters["@pricePaid"]);
            _addSqlCommandParameter("Vendor", GetDBColumnData("Vendor"), parameters["@vendor"]);
        }
    }
}
