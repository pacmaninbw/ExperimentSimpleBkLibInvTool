using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
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
                _authorId = _author.ID;
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
        {
            InitializeParameters();
            _author = null;
            _authorId = 0;
        }

        public SeriesModel(AuthorModel author)
        {
            InitializeParameters();
            InitAuthorDetails(author);
        }

        public SeriesModel(AuthorModel author, string title)
        {
            InitializeParameters();
            InitAuthorDetails(author);
            SetParameterValue("Series Title", title);
        }

        private void InitAuthorDetails(AuthorModel author)
        {
            _author = author;
            _authorId = _author.ID;
            SetParameterValue("First Name", _author.FirstName);
            SetParameterValue("Last Name", _author.LastName);
        }

        private void InitializeParameters()
        {
            _addSqlCommandParameter("ID", "idSeries", "N/A", MySqlDbType.UInt32, false, ParameterDirection.Input, true);
            _addSqlCommandParameter("First Name", "FirstName", "authorFirst", MySqlDbType.String, true, ParameterDirection.Input);
            _addSqlCommandParameter("Last Name", "LastName", "authorLast", MySqlDbType.String, true, ParameterDirection.Input);
            _addSqlCommandParameter("Series Title", "SeriesName", "seriesTitle", MySqlDbType.String, true, ParameterDirection.Input);
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
