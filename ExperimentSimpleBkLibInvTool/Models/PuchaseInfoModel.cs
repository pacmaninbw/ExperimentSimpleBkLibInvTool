using System;
using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo
{
    public class PuchaseInfoModel : DataTableItemBaseModel, IPuchaseInfoModel
    {
        private int _bookId;
        private string _listPrice;
        private string _paidPrice;
        private string _vendor;
        private DateTime _datePurchased;

        public string ListPrice
        {
            get { return _listPrice; }
            set { _listPrice = value; }
        }

        public string PaidPrice
        {
            get {  return _paidPrice; }
            set { _paidPrice = value; }
        }

        public string Vendor
        {
            get { return _vendor; }
            set { _vendor = value; }
        }

        public DateTime PurchaseDate
        {
            get { return _datePurchased; }
            set { _datePurchased = value; }
        }

        public PuchaseInfoModel()
        {
            _bookId = 0;
            _listPrice = null;
            _paidPrice = null;
            _vendor = null;
            _datePurchased = new DateTime(1970,1,1);
        }

        public PuchaseInfoModel(string vendor, string listPrice, string paidPrice, DateTime puchaseDate)
        {
            _bookId = 0;
            _listPrice = listPrice;
            _paidPrice = paidPrice;
            _vendor = vendor;
            _datePurchased = puchaseDate;
        }

        public int getBookId()
        {
            return _bookId;
        }

        public void setBookId(int BookId)
        {
            _bookId = BookId;
        }

        protected override bool _dataIsValid()
        {
            bool dataIsValid = true;

            // If the vendor is null then the user has not set this information.
            // This information is not required but if it has been set then the
            // values should be valid.
            if (_vendor != null)
            {
                if (_vendor.Length < 1)
                {
                    string errorMsg = "The vendor name is empty";
                    MessageBox.Show(errorMsg);
                    dataIsValid = false;
                }

#if false
                if (_paidPrice < 0.01)
                {
                    string errorMsg = "The price paid for the book is less than one cent.";
                    MessageBox.Show(errorMsg);
                    dataIsValid = false;
                }

                if (_listPrice < 0.01)
                {
                    string errorMsg = "The list price for the book is less than one cent.";
                    MessageBox.Show(errorMsg);
                    dataIsValid = false;
                }
#endif
            }

            return dataIsValid;
        }
    }
}
