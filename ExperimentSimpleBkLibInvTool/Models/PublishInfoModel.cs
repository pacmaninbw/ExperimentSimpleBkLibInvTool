using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo
{
    public class PublishInfoModel : IPublishInfoModel
    {
        private int _bookId;
        private string _isbnNumber;
        private string _copyRight;
        private string _publisher;
        private int _printing;
        private int _edition;
        private bool _outOfPrint;

        public string ISBNumber
        {
            get
            {
                return _isbnNumber;
            }
            set
            {
                _isbnNumber = value;
            }
        }

        public string CopyRight
        {
            get
            {
                return _copyRight;
            }
            set
            {
                _copyRight = value;
            }
        }

        public string Publisher
        {
            get
            {
                return _publisher;
            }
            set
            {
                _publisher = value;
            }
        }

        public int Printing
        {
            get
            {
                return _printing;
            }
            set
            {
                _printing = value;
            }
        }

        public int Edition
        {
            get
            {
                return _edition;
            }
            set
            {
                _edition = value;
            }
        }

        public bool OutOfPrint
        {
            get
            {
                return _outOfPrint;
            }
            set
            {
                _outOfPrint = value;
            }
        }

        public bool IsValid { get { return _dataIsValid(); } }

        public PublishInfoModel()
        {
            _bookId = 0;
            _isbnNumber = null;
            _copyRight = null;
            _publisher = null;
            _printing = 0;
            _edition = 0;
            _outOfPrint = false;
        }

        public PublishInfoModel(string ISBNumber, string CopyRight, string Publisher, int Printing=1, int Edition=1, bool OutOfPrint=false)
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

        private bool _dataIsValid()
        {
            bool dataIsValid = true;
            bool hasStringDataError = false;


            // If none of the fields have been updated the data is valid, if any of the fields
            // have been updated that at least the ISBN, CopyRight and Publisher should have values
            if (_isbnNumber == null && _copyRight == null && _publisher == null && _printing == 0 && _edition == 0 && _outOfPrint == false)
            {
                return dataIsValid;
            }

            if (_isbnNumber == null || _isbnNumber.Length < 1)
            {
                hasStringDataError = true;
            }

            if (_copyRight == null || _copyRight.Length < 1)
            {
                hasStringDataError = true;
            }

            if (_publisher == null || _publisher.Length < 1)
            {
                hasStringDataError = true;
            }

            if (_printing < 1 || _edition < 1)
            {
                string errorMsg = "When using the publishing the Printing and Edition must be greater than or equal to 1";
                MessageBox.Show(errorMsg);
                dataIsValid = false;
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
