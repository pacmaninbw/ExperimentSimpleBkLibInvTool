using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class ForSaleModel : DataTableItemBaseModel
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

        public ForSaleModel(uint bookId=0, bool isForSale=false, string askingPrice=null, string estimatedPrice=null)
            : base(((App)Application.Current).Model.ForSaleTable)
        {
            BookId = bookId;
            AskingPrice = askingPrice;
            EstimatedValue = estimatedPrice;
            IsForSale = isForSale;

            Modified = false;       // Initialization is not modification.
        }

        public override bool AddToDb() => ((App)Application.Current).Model.ForSaleTable.AddForSaleData(this);
        public override bool DbUpdate() => ((App)Application.Current).Model.ForSaleTable.UpdateForSaleData(this);

        protected override bool _dataIsValid()
        {
            bool dataIsValid = _defaultIsValid();

            return dataIsValid;
        }
    }
}
