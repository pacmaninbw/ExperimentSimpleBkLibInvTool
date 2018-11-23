using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo
{
    class PublishInfoModel : IPublishInfoModel
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
            _isbnNumber = null;
            _copyRight = null;
            _publisher = null;
            _printing = 0;
            _edition = 0;
            _outOfPrint = false;
        }

        public int getBookID()
        {
            return _bookId;
        }

        public void setBookId(int BookId)
        {
            _bookId = BookId;
        }
    }
}
