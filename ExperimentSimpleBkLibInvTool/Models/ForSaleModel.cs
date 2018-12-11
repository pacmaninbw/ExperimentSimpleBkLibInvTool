using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale
{
    public class ForSaleModel : DataTableItemBaseModel, IForSaleModel
    {
        private int _bookId;
        private string _askingPrice;
        private string _estimatedPrice;
        private bool _isForSale;

        public bool IsForSale
        {
            get { return _isForSale; }
            set { _isForSale = value; }
        }

        public string AskingPrice
        {
            get { return _askingPrice; }
            set {  _askingPrice = value; }
        }

        public string EstimatedValue
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

        public ForSaleModel(bool isForSale=false, string askingPrice=null, string estimatedPrice=null)
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

        protected override bool _dataIsValid()
        {
            bool dataIsValid = true;

            if (string.IsNullOrEmpty(_estimatedPrice))
            {
                dataIsValid = false; ;
            }

            if (string.IsNullOrEmpty(_askingPrice))
            {
                dataIsValid = false; ;
            }

            return dataIsValid;
        }
    }
}
