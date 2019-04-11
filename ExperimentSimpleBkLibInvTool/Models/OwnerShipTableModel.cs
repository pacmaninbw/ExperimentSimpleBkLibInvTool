using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class OwnerShipTableModel : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int InLibraryColumnIndex;
        private int WishListColumnIndex;

        public OwnerShipTableModel() : base("owned", "getOwnerShipData", "addOwnerShipData")
        {
            BookIDColumnIndex = GetDBColumnData("BookFKo").IndexBasedOnOrdinal;
            InLibraryColumnIndex = GetDBColumnData("IsOwned").IndexBasedOnOrdinal;
            WishListColumnIndex = GetDBColumnData("IsWishListed").IndexBasedOnOrdinal;
        }

        public DataTable OwnerShipTable => DataTable;

        public bool AddOwnerShipData(IOwnerShipModel ownerShipData)
        {
            OwnerShipModel ownerShipModel = (OwnerShipModel)ownerShipData;
            return (ownerShipModel.BookId > 0) ? addItem(ownerShipModel) : false;
        }

        public OwnerShipModel GetOwnerShipModel(uint bookId)
        {
            DataRow rawOwnerShipModel = GetRawData(bookId);
            return (rawOwnerShipModel != null) ? ConvertDataRowToOwnerShipModel(rawOwnerShipModel) : null ;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;
            _addSqlCommandParameter("ID", GetDBColumnData("BookFKo"), parameters["@bookKey"]);
            _addSqlCommandParameter("In Library", GetDBColumnData("IsOwned"), parameters["@isOwned"]);
            _addSqlCommandParameter("Wish Listed", GetDBColumnData("IsWishListed"), parameters["@isWishListed"]);
        }

        private OwnerShipModel ConvertDataRowToOwnerShipModel(DataRow rawForSaleData)
        {
            uint bookId = uint.Parse(rawForSaleData[BookIDColumnIndex].ToString());
            bool isInLibrary = int.Parse(rawForSaleData[InLibraryColumnIndex].ToString()) > 0;
            bool isWhishListed = int.Parse(rawForSaleData[WishListColumnIndex].ToString()) > 0;

            return new OwnerShipModel(bookId, isInLibrary, isWhishListed);
        }
    }
}
