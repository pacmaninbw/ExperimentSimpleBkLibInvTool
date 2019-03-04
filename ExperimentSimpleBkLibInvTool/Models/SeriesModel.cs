using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Series
{
    public class SeriesModel : DataTableItemBaseModel, ISeriesModel
    {
        private AuthorModel _author;
        private uint _authorId;

        public AuthorModel Author {
            get { return _author; }
            set {
                _author = value;
                SetParameterValue("First Name", _author.FirstName);
                SetParameterValue("Last Name", _author.LastName);
            }
        }

        public string Title {
            get { return GetParameterValue("Series Title"); }
            set { SetParameterValue("Series Title", value); }
        }

        public uint AuthorId { get { return _authorId; } }

        public SeriesModel()
            : base(((App)Application.Current).Model.SeriesTable)
        {
            _author = null;
            _authorId = 0;
        }

        public SeriesModel(AuthorModel author)
            : base(((App)Application.Current).Model.SeriesTable)
        {
            InitAuthorDetails(author);
        }

        public SeriesModel(AuthorModel author, string title)
            : base(((App)Application.Current).Model.SeriesTable)
        {
            InitAuthorDetails(author);
            SetParameterValue("Series Title", title);
        }

        private void InitAuthorDetails(AuthorModel author)
        {
            _author = author;
            SetParameterValue("First Name", _author.FirstName);
            SetParameterValue("Last Name", _author.LastName);
        }

        protected override bool _dataIsValid()
        {
            bool dataIsValid = true;
            if (_author == null)
            {
                dataIsValid = false;
            }
            else
            {
                dataIsValid = _author.IsValid;
                if (!GetParameterIsValid("Series Title"))
                {
                    string errorMsg = "Add Series error: Missing title of series.";
                    MessageBox.Show(errorMsg);
                    dataIsValid = false;
                }
            }
            return dataIsValid;
        }
    }
}
