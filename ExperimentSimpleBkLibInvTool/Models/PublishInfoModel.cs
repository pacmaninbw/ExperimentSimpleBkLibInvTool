using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class PublishInfoModel : DataTableItemBaseModel
    {

        public string ISBNumber
        {
            get { return GetParameterValue("ISB Number"); }
            set { SetParameterValue("ISB Number", value); }
        }

        public string CopyRight
        {
            get { return GetParameterValue("Copyright"); }
            set { SetParameterValue("Copyright", value); }
        }

        public string Publisher
        {
            get { return GetParameterValue("Publisher"); }
            set { SetParameterValue("Publisher", value); }
        }

        public string Printing
        {
            get { return GetParameterValue("Printing"); }
            set { SetParameterValue("Printing", value); }
        }

        public string Edition
        {
            get { return GetParameterValue("Edition"); }
            set { SetParameterValue("Edition", value); }
        }

        public bool OutOfPrint
        {
            get { return GetParameterBValue("OutOfPrint"); }
            set { SetParameterValue("OutOfPrint", value); }
        }

        public PublishInfoModel()
            : base(((App)Application.Current).Model.PublishingData)
        {
            BookId = 0;
            ISBNumber = string.Empty;
            CopyRight = string.Empty;
            Publisher = string.Empty;
            Printing = string.Empty;
            Edition = string.Empty;
            OutOfPrint = false;

            Modified = false;       // Initialization is not modification.
        }

        public PublishInfoModel(uint bookId, string iSBNumber, string copyRight, string publisher, string printing=null, string edition=null, bool outOfPrint=false)
            :base(((App)Application.Current).Model.PublishingData)
        {
            BookId = bookId;
            ISBNumber = iSBNumber;
            CopyRight = copyRight;
            Publisher = publisher;
            Printing = printing;
            Edition = edition;
            OutOfPrint = outOfPrint;

            Modified = false;       // Initialization is not modification.
        }

        public override bool AddToDb() => ((App)Application.Current).Model.PublishingData.AddPublishingInfo(this);
        public override bool DbUpdate() => ((App)Application.Current).Model.PublishingData.UpdatePublishingInfo(this);

        protected override bool _dataIsValid()
        {
            bool dataIsValid = _defaultIsValid();
            if (!dataIsValid)
            {
                return dataIsValid;
            }

            bool hasStringDataError = false;

            // If none of the fields have been updated the data is valid, if any of the fields
            // have been updated then at least the ISBN and CopyRight should have values
            if (string.IsNullOrEmpty(ISBNumber)  && string.IsNullOrEmpty(CopyRight) && string.IsNullOrEmpty(Publisher) && string.IsNullOrEmpty(Printing) && string.IsNullOrEmpty(Edition) && OutOfPrint == false)
            {
                return dataIsValid;
            }

            if (string.IsNullOrEmpty(ISBNumber))
            {
                hasStringDataError = true;
            }

            if (string.IsNullOrEmpty(CopyRight))
            {
                hasStringDataError = true;
            }

            if (hasStringDataError)
            {
                string errorMsg = "When using the publishing information the ISBN and Copyright are required fields";
                MessageBox.Show(errorMsg);
                dataIsValid = false;
            }

            return dataIsValid;
        }
    }
}
