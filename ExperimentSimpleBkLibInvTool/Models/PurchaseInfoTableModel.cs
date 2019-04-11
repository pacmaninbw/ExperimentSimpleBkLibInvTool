﻿using System.Data;
using System;
using MySql.Data.MySqlClient;

namespace pacsw.BookInventory.Models
{
    public class PurchaseInfoTableModel : CDataTableModel
    {
        private int BookIDColumnIndex;
        private int VendorColumnIndex;
        private int PricePaidColumnIndex;
        private int ListPriceColumnIndex;
        private int DateOfPurchaseColumnIndex;

        public PurchaseInfoTableModel() : base("purchaseinfo", "getPurchaseInfo", "addPurchaseInfo")
        {
            BookIDColumnIndex = GetDBColumnData("BookFKPurI").IndexBasedOnOrdinal;
            VendorColumnIndex = GetDBColumnData("Vendor").IndexBasedOnOrdinal;
            DateOfPurchaseColumnIndex = GetDBColumnData("PurchaseDate").IndexBasedOnOrdinal;
            PricePaidColumnIndex = GetDBColumnData("PaidPrice").IndexBasedOnOrdinal;
            ListPriceColumnIndex = GetDBColumnData("ListPrice").IndexBasedOnOrdinal;
        }

        public DataTable PurchaseInfoTable => DataTable;

        public bool AddPurchaseInfo(IPuchaseInfoModel purchaseData)
        {
            PuchaseInfoModel purchaseInfoModel = (PuchaseInfoModel)purchaseData;
            return (purchaseInfoModel.BookId > 0) ? addItem(purchaseInfoModel) : false;
        }

        public PuchaseInfoModel GetPuchaseInfo(uint bookId)
        {
            DataRow rawPurchasingData = GetRawData(bookId);
            return (rawPurchasingData != null) ? ConvertDataRowToPurchaseInfoModel(rawPurchasingData) : null;
        }

        protected override void InitializeSqlCommandParameters()
        {
            MySqlParameterCollection parameters = AddItemParameters;

            _addSqlCommandParameter("ID", GetDBColumnData("BookFKPurI"), parameters["@bookKey"]);
            _addSqlCommandParameter("Date of Purchase", GetDBColumnData("PurchaseDate"), parameters["@purchaseDate"]);
            _addSqlCommandParameter("List Price", GetDBColumnData("ListPrice"), parameters["@listPrice"]);
            _addSqlCommandParameter("Price Paid", GetDBColumnData("PaidPrice"), parameters["@pricePaid"]);
            _addSqlCommandParameter("Vendor", GetDBColumnData("Vendor"), parameters["@vendor"]);
        }

        private PuchaseInfoModel ConvertDataRowToPurchaseInfoModel(DataRow rawPurchasingData)
        {
            uint bookId = uint.Parse(rawPurchasingData[BookIDColumnIndex].ToString());
            string vendor = rawPurchasingData[VendorColumnIndex].ToString();
            string listPrice = rawPurchasingData[ListPriceColumnIndex].ToString();
            string purchasePrice = rawPurchasingData[PricePaidColumnIndex].ToString();
            DateTime datePurchased = DateTime.Parse(rawPurchasingData[DateOfPurchaseColumnIndex].ToString());

            return new PuchaseInfoModel(bookId, vendor, listPrice, purchasePrice, datePurchased);
        }
    }
}
