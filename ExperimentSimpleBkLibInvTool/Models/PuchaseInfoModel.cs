using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo
{
    class PuchaseInfoModel : IPuchaseInfoModel
    {
        private int _bookId;
        private double _listPrice;
        private double _paidPrice;
        private string _vendor;
        private DateTime _datePurchased;

        public double ListPrice
        {
            get
            {
                return _listPrice;
            }
            set
            {
                _listPrice = value;
            }
        }

        public double PaidPrice
        {
            get
            {
                return _paidPrice;
            }
            set
            {
                _paidPrice = value;
            }
        }

        public string Vendor
        {
            get
            {
                return _vendor;
            }
            set
            {
                _vendor = value;
            }
        }

        public DateTime PurchaseDate
        {
            get
            {
                return _datePurchased;
            }
            set
            {
                _datePurchased = value;
            }
        }

        public PuchaseInfoModel()
        {
            _bookId = 0;
            _listPrice = 0.0;
            _paidPrice = 0.0;
            _vendor = null;
        }

        public PuchaseInfoModel(string vendor, double listPrice, double paidPrice, DateTime puchaseDate)
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
    }
}
