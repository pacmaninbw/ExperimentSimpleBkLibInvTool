﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pacsw.BookInventory.Models.DataTableModel;


namespace pacsw.BookInventory.Models.BookInfo.PublishInfo
{
    public class PublishInfoTableModel : CDataTableModel
    {
        public PublishInfoTableModel() : base("publishinginfo", "getPublishingInfo", "addPublishingInfo")
        {
        }

        public DataTable PublishInfoTable { get { return DataTable; } }

        public bool AddPublishingInfo(IPublishInfoModel PublishingData, uint bookId = 0)
        {
            PublishInfoModel publishInfoModel = (PublishInfoModel)PublishingData;
            if (publishInfoModel.BookId > 0)
            {
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

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKPubI"), parameters["@bookKey"]);
            _addSqlCommandParameter("Copyright", GetDBColumnData("Copyright"), parameters["@copyright"]);
            _addSqlCommandParameter("ISB Number", GetDBColumnData("ISBNumber"), parameters["@iSBNumber"]);
            _addSqlCommandParameter("Edition", GetDBColumnData("Edition"), parameters["@edition"]);
            _addSqlCommandParameter("Printing", GetDBColumnData("Printing"), parameters["@printing"]);
            _addSqlCommandParameter("Publisher", GetDBColumnData("Publisher"), parameters["@publisher"]);
            _addSqlCommandParameter("Out of Print", GetDBColumnData("OutOfPrint"), parameters["@outOfPrint"]);
        }

    }
}
