using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pacsw.BookInventory.Models.DataTableModel;

namespace pacsw.BookInventory.Models.BookInfo.Ownned
{
    public class OwnerShipTableModel : CDataTableModel
    {
        public OwnerShipTableModel() : base("owned", "getOwnerShipData", "addOwnerShipData")
        {
        }

        public DataTable OwnerShipTable { get { return DataTable; } }

        public bool AddOwnerShipData(IOwnerShipModel ownerShipData)
        {
            OwnerShipModel ownerShipModel = (OwnerShipModel)ownerShipData;
            if (ownerShipModel.BookId > 0)
            {
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
            _addSqlCommandParameter("ID", GetDBColumnData("BookFKo"), parameters["@bookKey"]);
            _addSqlCommandParameter("In Library", GetDBColumnData("IsOwned"), parameters["@isOwned"]);
            _addSqlCommandParameter("Wish Listed", GetDBColumnData("IsWishListed"), parameters["@isWishListed"]);
        }
    }
}
