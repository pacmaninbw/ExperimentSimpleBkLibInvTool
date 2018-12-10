using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo
{
    public class PublishInfoModel : DataTableItemBaseModel, IPublishInfoModel
    {
        private int _bookId;
        private string _isbnNumber;
        private string _copyRight;
        private string _publisher;
        private string _printing;
        private string _edition;
        private bool _outOfPrint;

        public string ISBNumber
        {
            get { return _isbnNumber; }
            set { _isbnNumber = value; }
        }

        public string CopyRight
        {
            get { return _copyRight; }
            set { _copyRight = value; }
        }

        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        public string Printing
        {
            get { return _printing; }
            set { _printing = value; }
        }

        public string Edition
        {
            get { return _edition; }
            set { _edition = value; }
        }

        public bool OutOfPrint
        {
            get { return _outOfPrint; }
            set { _outOfPrint = value; }
        }

        public PublishInfoModel()
        {
            _bookId = 0;
            _isbnNumber = null;
            _copyRight = null;
            _publisher = null;
            _printing = null;
            _edition = null;
            _outOfPrint = false;
        }

        public PublishInfoModel(string ISBNumber, string CopyRight, string Publisher, string Printing=null, string Edition=null, bool OutOfPrint=false)
        {
            _bookId = 0;
            _isbnNumber = ISBNumber;
            _copyRight = CopyRight;
            _publisher = Publisher;
            _printing = Printing;
            _edition = Edition;
            _outOfPrint = OutOfPrint;
        }

        public int getBookID()
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
            bool hasStringDataError = false;


            // If none of the fields have been updated the data is valid, if any of the fields
            // have been updated that at least the ISBN, CopyRight and Publisher should have values
            if (_isbnNumber == null && _copyRight == null && _publisher == null && _printing == null && _edition == null && _outOfPrint == false)
            {
                return dataIsValid;
            }

            if (string.IsNullOrEmpty(_isbnNumber))
            {
                hasStringDataError = true;
            }

            if (string.IsNullOrEmpty(_copyRight))
            {
                hasStringDataError = true;
            }

            if (string.IsNullOrEmpty(_publisher))
            {
                hasStringDataError = true;
            }

            if (hasStringDataError)
            {
                string errorMsg = "When using the publishing information the ISBN, Copyright and Publisher are required fields";
                MessageBox.Show(errorMsg);
                dataIsValid = false;
            }

            return dataIsValid;
        }
    }
}
