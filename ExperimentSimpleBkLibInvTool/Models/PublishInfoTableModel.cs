using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;


namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo
{
    public class PublishInfoTableModel : CDataTableModel
    {
        public PublishInfoTableModel() : base("publishinginfo", "getPublishingInfo", "addPublishingInfo")
        {
        }

        public DataTable PublishInfoTable { get { return DataTable; } }

        public bool AddPublishingInfo(IPublishInfoModel PublishingData, uint bookId)
        {
            PublishInfoModel publishInfoModel = (PublishInfoModel)PublishingData;
            if (bookId > 0 || publishInfoModel.getBookID() > 0)
            {
                publishInfoModel.setBookId(bookId);
                return addItem(publishInfoModel);
            }
            else
            {
                return false;
            }
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("Book Id", GetDBColumnData("BookFKPubI"), parameters["@bookKey"]);
            _addSqlCommandParameter("Copyright", GetDBColumnData("Copyright"), parameters["@copyright"]);
            _addSqlCommandParameter("ISB Number", GetDBColumnData("ISBNumber"), parameters["@iSBNumber"]);
            _addSqlCommandParameter("Edition", GetDBColumnData("Edition"), parameters["@edition"]);
            _addSqlCommandParameter("Printing", GetDBColumnData("Printing"), parameters["@printing"]);
            _addSqlCommandParameter("Publisher", GetDBColumnData("Publisher"), parameters["@publisher"]);
            _addSqlCommandParameter("Out of Print", GetDBColumnData("OutOfPrint"), parameters["@outOfPrint"]);
        }

    }
}
