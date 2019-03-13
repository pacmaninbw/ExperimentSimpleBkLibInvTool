using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale
{
    public class ForSaleModel : DataTableItemBaseModel, IForSaleModel
    {
        public bool IsForSale
        {
            get { return GetParameterBValue("Is For Sale"); }
            set { SetParameterValue("Is For Sale", value); }
        }

        public string AskingPrice
        {
            get { return GetParameterValue("Asking Price"); }
            set { SetParameterValue("Asking Price", value); }
        }

        public string EstimatedValue
        {
            get { return GetParameterValue("Estimated Value"); }
            set { SetParameterValue("Estimated Value", value); }
        }

        public ForSaleModel(bool isForSale=false, string askingPrice=null, string estimatedPrice=null)
            : base(((App)Application.Current).Model.ForSaleTable)
        {
            AskingPrice = askingPrice;
            EstimatedValue = estimatedPrice;
            IsForSale = isForSale;
        }

        public override bool AddToDb()
        {
            return ((App)Application.Current).Model.ForSaleTable.AddForSaleData(this);
        }

        protected override bool _dataIsValid()
        {
            bool dataIsValid = _defaultIsValid();

            return dataIsValid;
        }
    }
}
