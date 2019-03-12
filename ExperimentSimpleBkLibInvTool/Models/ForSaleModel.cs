using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale
{
    public class ForSaleModel : DataTableItemBaseModel, IForSaleModel
    {
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
            get { return _estimatedPrice; }
            set { _estimatedPrice = value; }
        }

        public ForSaleModel(bool isForSale=false, string askingPrice=null, string estimatedPrice=null)
            : base(((App)Application.Current).Model.ForSaleTable)
        {
            _askingPrice = askingPrice;
            _estimatedPrice = estimatedPrice;
            _isForSale = isForSale;
        }

        public override bool AddToDb()
        {
            return ((App)Application.Current).Model.ForSaleTable.AddForSaleData(this);
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
