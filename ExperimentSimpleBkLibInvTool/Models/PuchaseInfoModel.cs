using System;
using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class PuchaseInfoModel : DataTableItemBaseModel
    {
        public string ListPrice
        {
            get { return GetParameterValue("List Price"); ; }
            set { SetParameterValue("List Price", value); }
        }

        public string PaidPrice
        {
            get { return GetParameterValue("Price Paid"); ; }
            set { SetParameterValue("Price Paid", value); }
        }

        public string Vendor
        {
            get { return GetParameterValue("Vendor"); ; }
            set { SetParameterValue("Vendor", value); }
        }

        public DateTime PurchaseDate
        {
            get { return DateTime.Parse(GetParameterValue("Date of Purchase")); }
            set { SetParameterValue("Date of Purchase", value.ToString("yyyy/MM/dd")); }
        }

        public PuchaseInfoModel()
            : base(((App)Application.Current).Model.PurchaseData)
        {
            BookId = 0;
            ListPrice = string.Empty;
            PaidPrice = string.Empty;
            Vendor = string.Empty;
            SetParameterValue("Date of Purchase", string.Empty);
        }

        public PuchaseInfoModel(uint bookId, string vendor, string listPrice, string paidPrice, DateTime puchaseDate)
            : base(((App)Application.Current).Model.PurchaseData)
        {
            BookId = bookId;
            ListPrice = listPrice;
            PaidPrice = paidPrice;
            Vendor = vendor;
            PurchaseDate = puchaseDate;
        }

        public override bool AddToDb()
        {
            return ((App)Application.Current).Model.PurchaseData.AddPurchaseInfo(this);
        }

        protected override bool _dataIsValid()
        {
            bool dataIsValid = _defaultIsValid();

            return dataIsValid;
        }
    }
}
