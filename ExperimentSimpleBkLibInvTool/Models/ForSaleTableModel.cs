using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (forSaleModel.getBookID() > 0)
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

            _addSqlCommandParameter("Book Id", GetDBColumnData("BookFKfs"), parameters["@bookKey"]);
            _addSqlCommandParameter("Is For Sale", GetDBColumnData("IsForSale"), parameters["@isForSale"]);
            _addSqlCommandParameter("Asking Price", GetDBColumnData("AskingPrice"), parameters["@askingPrice"]);
            _addSqlCommandParameter("Estimated Value", GetDBColumnData("EstimatedValue"), parameters["@estimatedValue"]);
        }
    }
}
