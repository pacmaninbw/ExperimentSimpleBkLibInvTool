using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale
{
    public class ForSaleModel : IForSaleModel
    {
        private int _bookId;
        private double _askingPrice;
        private double _estimatedPrice;
        private bool _isForSale;

        public bool IsForSale
        {
            get
            {
                return _isForSale;
            }
            set
            {
                _isForSale = value;
            }
        }

        public double AskingPrice
        {
            get
            {
                return _askingPrice;
            }
            set
            {
                _askingPrice = value;
            }
        }

        public double EstimatedValue
        {
            get
            {
                return _estimatedPrice;
            }
            set
            {
                _estimatedPrice = value;
            }
        }

        public bool IsValid { get { return _dataIsValid(); } }

        public ForSaleModel(bool isForSale=false, double askingPrice=0.0, double estimatedPrice=0.0)
        {
            _bookId = 0;
            _askingPrice = askingPrice;
            _estimatedPrice = estimatedPrice;
            _isForSale = isForSale;
        }

        public void setBookId(int BookId)
        {
            _bookId = BookId;
        }

        public int getBookId()
        {
            return _bookId;
        }

        private bool _dataIsValid()
        {
            bool dataIsValid = true;

            if (_estimatedPrice < 0)
            {
                dataIsValid = false; ;
            }

            if (_askingPrice < 0)
            {
                dataIsValid = false; ;
            }

            return dataIsValid;
        }
    }
}
