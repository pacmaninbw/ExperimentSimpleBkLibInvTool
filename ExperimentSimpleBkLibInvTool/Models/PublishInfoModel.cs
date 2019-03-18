using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class PublishInfoModel : DataTableItemBaseModel, IPublishInfoModel
    {
        private string _isbnNumber;
        private string _copyRight;
        private string _publisher;
        private string _printing;
        private string _edition;
        private bool _outOfPrint;

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
            // InitializeSqlCommandParameters();
            _isbnNumber = null;
            _copyRight = null;
            _publisher = null;
            _printing = null;
            _edition = null;
            _outOfPrint = false;
        }

        public PublishInfoModel(string ISBNumber, string CopyRight, string Publisher, string Printing=null, string Edition=null, bool OutOfPrint=false)
            :base(((App)Application.Current).Model.PublishingData)
        {
            // InitializeSqlCommandParameters();
            _isbnNumber = ISBNumber;
            _copyRight = CopyRight;
            _publisher = Publisher;
            _printing = Printing;
            _edition = Edition;
            _outOfPrint = OutOfPrint;
        }

        public override bool AddToDb()
        {            
            return ((App)Application.Current).Model.PublishingData.AddPublishingInfo(this);
        }

        protected override bool _dataIsValid()
        {
            bool dataIsValid = _defaultIsValid();
            if (!dataIsValid)
            {
                return dataIsValid;
            }

            bool hasStringDataError = false;


            // If none of the fields have been updated the data is valid, if any of the fields
            // have been updated that at least the ISBN, CopyRight and Publisher should have values
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
