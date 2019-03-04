using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned
{
    public class OwnerShipTableModel : CDataTableModel
    {
        public OwnerShipTableModel() : base("owned", "getOwnerShipData", "addOwnerShipData")
        {
        }

        public DataTable OwnerShipTable { get { return DataTable; } }

        public bool AddOwnerShipData(IOwnerShipModel ownerShipData, uint bookId)
        {
            OwnerShipModel ownerShipModel = (OwnerShipModel)ownerShipData;
            if (bookId > 0 || ownerShipModel.getBookID() > 0)
            {
                ownerShipModel.setBookId(bookId);
                return addItem(ownerShipModel);
            }
            else
            {
                return false;
            }
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;
            _addSqlCommandParameter("Book Id", GetDBColumnData("BookFKo"), parameters["@bookKey"]);
            _addSqlCommandParameter("In Library", GetDBColumnData("IsOwned"), parameters["@isOwned"]);
            _addSqlCommandParameter("Wish Listed", GetDBColumnData("IsWishListed"), parameters["@isWishListed"]);
        }
    }
}
